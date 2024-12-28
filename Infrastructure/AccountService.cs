using System.Security.Claims;

using Application.Constants;
using Application.DTO.Request;
using Application.DTO.Request.Identity;
using Application.DTO.Response;
using Application.DTO.Response.Identity;
using Application.Interface.Identity;

using Domain.Entities;

using Infrastructure.Data;
using Infrastructure.Extensions.Identity;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class AccountService(UserManager<WmsStaff> userManager,
        SignInManager<WmsStaff> signInManager,
        RoleManager<IdentityRole> roleManager,
        IWmsDbContextFactory<WmsDbContext> contextFactory) : IAccountService
    {
        public async Task<ServiceResponse> CheckStaffLoginAsync(CheckLoginStaffRequestDTO model)
        {
            var user = await FindUserByEmail(model.Email);
            if (user == null)
            {
                return new ServiceResponse(false, "User not found");
            }

            var verifyPassword = await signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!verifyPassword.Succeeded)
            {
                return new ServiceResponse(false, "Incorrect Password");
            }

            return new ServiceResponse(true, $"Welcome {user.FirstName} {user.LastName}");
        }

        public async Task<ServiceResponse> StaffLoginAsync(LoginStaffRequestDTO model)
        {
            var checkLoginStaffRequestDTO = new CheckLoginStaffRequestDTO()
            {
                Email = model.Email,
                Password = model.Password,
                RememberMe = model.RememberMe,
            };

            var result = await CheckStaffLoginAsync(checkLoginStaffRequestDTO);

            if (!result.Success)
            {
                return new ServiceResponse(false, result.Message);
            }

            var user = await FindUserByEmail(model.Email);
            if (user == null)
            {
                return new ServiceResponse(false, $"Unable to find email {model.Email}");
            }

            var signInResult = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
            if (!signInResult.Succeeded)
            {
                return new ServiceResponse(false, "Unknown error while logging in");
            }

            return new ServiceResponse(true, $"Welcome {user.FirstName} {user.LastName}");
        }

        public async Task<ServiceResponse> StaffLogoutAsync()
        {
            await signInManager.SignOutAsync();
            return new ServiceResponse(true, "Logout success. Bye!");
        }

        public async Task<ServiceResponse> CreateStaffAsync(CreateStaffRequestDTO model)
        {
            var result = await CreateStaffIdentityAsync(model);
            if (!result.Success)
            {
                return result;
            }

            result = await CreateStaffApplicationAsync(model);
            return result;
        }

        public async Task<IEnumerable<GetStaffWithClaimResponseDTO>> GetAllStaffWithClaimsAsync(Guid companyId)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var company = wmsDbContext.Companies
                .Include(company => company.Staffs)
                .First(company => company.Id == companyId);

            var staffIds = company.Staffs.Select(staff => staff.Id.ToString());
            var allUsers = await userManager.Users
                .Where(user=> staffIds.Contains(user.Id))
                .ToListAsync();

            var userList = new List<GetStaffWithClaimResponseDTO>();
            if (allUsers.Count == 0)
            {
                return userList;
            }

            foreach (var user in allUsers)
            {
                var currentUser = await userManager.FindByIdAsync(user.Id);
                var currentUserClaims = await userManager.GetClaimsAsync(currentUser);
                var customClaims = currentUserClaims.ToDictionary(
                    claim => claim.Type, claim => claim.Value);
                var roles = await userManager.GetRolesAsync(currentUser);

                var getStaffWithClaimResponseDTO = new GetStaffWithClaimResponseDTO()
                {
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Roles = roles,
                    CustomClaims = customClaims
                };
                userList.Add(getStaffWithClaimResponseDTO);
            }

            return userList;
        }

        public async Task<GetStaffWithClaimResponseDTO?> GetStaffWithClaimsByIdAsync(Guid staffId)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();

            var currentUser = await userManager.FindByIdAsync(staffId.ToString());
            if (currentUser == null)
            {
                return null;
            }
            
            var currentUserClaims = await userManager.GetClaimsAsync(currentUser);
            var customClaims = currentUserClaims.ToDictionary(
                claim => claim.Type, claim => claim.Value);
            var roles = await userManager.GetRolesAsync(currentUser);

            var getStaffWithClaimResponseDTO = new GetStaffWithClaimResponseDTO()
            {
                Email = currentUser.Email,
                FirstName = currentUser.FirstName,
                LastName = currentUser.LastName,
                Roles = roles,
                CustomClaims = customClaims
            };

            return getStaffWithClaimResponseDTO;
        }

        public async Task SetUpAsync()
        {
            var createStaffRequestDTO = new CreateStaffRequestDTO()
            {
                CompanyId = DebugConstants.CompanyId,
                CreatedBy = DebugConstants.AdminId,
                FirstName = "Master",
                LastName = "Control",
                Email = "master@control.com",
                Password = "123asd!@#ASD",
                Roles = new List<string> { StaffRole.MasterControl },
                ConfirmPassword = "123asd!@#ASD",
            };

            await CreateStaffAsync(createStaffRequestDTO);
        }

        public async Task<ServiceResponse> UpdateStaffAsync(ChangeStaffClaimRequestDTO model)
        {
            var user = await userManager.FindByIdAsync(model.StaffId);
            if (user == null)
            {
                return new ServiceResponse(false, "User not found");
            }

            // update user attributes
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            var result = await userManager.UpdateAsync(user);
            var response = CheckResult(result);
            if (!response.Success)
            {
                return new ServiceResponse(false, response.Message);
            }

            // update roles and claims
            foreach (var role in model.Roles)
            {
                var roleExists = await roleManager.RoleExistsAsync(role);
                if (!roleExists)
                {
                    return new ServiceResponse(false, $"Role {role} is not found");
                }
            }

            var oldUserClaims = await userManager.GetClaimsAsync(user);
            result = await userManager.RemoveClaimsAsync(user, oldUserClaims);
            response = CheckResult(result);
            if (!response.Success)
            {
                return new ServiceResponse(false, response.Message);
            }

            var oldUserRoles = await userManager.GetRolesAsync(user);
            result = await userManager.RemoveFromRolesAsync(user, oldUserRoles);
            response = CheckResult(result);
            if (!response.Success)
            {
                return new ServiceResponse(false, response.Message);
            }

            result = await userManager.AddToRolesAsync(user, model.Roles);
            response = CheckResult(result);
            if (!response.Success)
            {
                return new ServiceResponse(false, response.Message);
            }

            foreach (var claimValuePair in model.CustomClaims)
            {
                var claim = new Claim(claimValuePair.Key, claimValuePair.Value);
                var addNewClaims = await userManager.AddClaimAsync(user, claim);
                response = CheckResult(addNewClaims);
                if (!response.Success)
                {
                    return new ServiceResponse(false, response.Message);
                }
            }

            return new ServiceResponse(true, "User updated");
        }

        public async Task<ServiceResponse> DeleteStaffAsync(string staffEmail)
        {
            var user = await FindUserByEmail(staffEmail);
            if (user == null)
            {
                return new ServiceResponse(false, $"User {staffEmail} not found");
            }

            var result = await userManager.DeleteAsync(user);
            var response = CheckResult(result);
            if (!response.Success)
            {
                return new ServiceResponse(response.Success, response.Message);
            }

            await using var wmsDbContext = contextFactory.CreateDbContext();
            var foundStaff = await wmsDbContext.Staffs.FirstOrDefaultAsync(staff => staff.Id.ToString() == user.Id);
            if (foundStaff != null)
            {
                wmsDbContext.Staffs.Remove(foundStaff);
                await wmsDbContext.SaveChangesAsync();
            }

            return new ServiceResponse(true, $"User {staffEmail} deleted");
        }

        public async Task<ServiceResponse> DeleteStaffAsync(Guid staffId)
        {
            var user = await userManager.FindByIdAsync(staffId.ToString());
            if (user == null)
            {
                return new ServiceResponse(false, $"User {staffId} not found");
            }

            var result = await userManager.DeleteAsync(user);
            var response = CheckResult(result);
            if (!response.Success)
            {
                return new ServiceResponse(response.Success, response.Message);
            }

            await using var wmsDbContext = contextFactory.CreateDbContext();
            var foundStaff = await wmsDbContext.Staffs.FirstOrDefaultAsync(staff => staff.Id.ToString() == user.Id);
            if (foundStaff != null)
            {
                wmsDbContext.Staffs.Remove(foundStaff);
                await wmsDbContext.SaveChangesAsync();
            }

            return new ServiceResponse(true, $"User {staffId} deleted");
        }

        private async Task<WmsStaff?> FindUserByEmail(string email)
        {
            return await userManager.FindByEmailAsync(email) ?? null;
        }

        private static ServiceResponse CheckResult(IdentityResult identityResult)
        {
            if (identityResult.Succeeded)
            {
                return new ServiceResponse(true, string.Empty);
            }

            var errorList = identityResult.Errors.Select(error => error.Description);
            var errors = string.Join(Environment.NewLine, errorList);
            return new ServiceResponse(false, errors);
        }

        private async Task<ServiceResponse> AddStaffRole(CreateStaffRequestDTO model)
        {
            if (model.Roles.Any(string.IsNullOrEmpty) || !model.Roles.Any())
            {
                return new ServiceResponse(false, "No role specified");
            }

            foreach (var role in model.Roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    return new ServiceResponse(false, $"Role {role} does not exist");
                }
            }

            var user = await FindUserByEmail(model.Email);
            if (user == null)
            {
                return new ServiceResponse(false, $"User {model.Email} not found");
            }

            var result = new ServiceResponse(false, "User roles not added");
            foreach (var role in model.Roles)
            {
                var identityResult = await userManager.AddToRoleAsync(user, role);
                result = CheckResult(identityResult);
                if (!result.Success)
                {
                    return result;
                }
            }

            return result;
        }

        private async Task<ServiceResponse> CreateStaffIdentityAsync(CreateStaffRequestDTO model)
        {
            var user = await FindUserByEmail(model.Email);
            if (user != null)
            {
                return new ServiceResponse(false, "User already exists");
            }

            var newUser = new WmsStaff()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Email,
                Email = model.Email,
            };

            var serviceResponse = await userManager.CreateAsync(newUser, model.Password);
            var result = CheckResult(serviceResponse);
            if (!result.Success)
            {
                return result;
            }

            result = await AddStaffRole(model);
            if (!result.Success)
            {
                return result;
            }

            return new ServiceResponse(true, $"User {model.FirstName} {model.LastName} added");
        }

        private async Task<ServiceResponse> CreateStaffApplicationAsync(CreateStaffRequestDTO model)
        {
            var user = await FindUserByEmail(model.Email);
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var foundStaff = await wmsDbContext.Staffs.FirstOrDefaultAsync(
                    staff => string.Equals(staff.Id.ToString(), user.Id));
                if (foundStaff != null)
                {
                    return GeneralDbResponses.ItemAlreadyExist($"{model.Email}");
                }

                var data = new Staff()
                {
                    Id = new Guid(user.Id),
                    CompanyId = model.CompanyId,
                    CreatedBy = model.CreatedBy,
                };

                wmsDbContext.Staffs.Add(data);
                await wmsDbContext.SaveChangesAsync();
                return new ServiceResponse(true, $"User {model.FirstName} {model.LastName} added");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
