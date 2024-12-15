using System.Reflection;

using Application.Constants;
using Application.Service.Queries;

using MediatR;

namespace Infrastructure.Repository
{
    public class GetAllStaffRoles : 
        IRequestHandler<GetAllStaffRolesQuery, IEnumerable<string>>
    {
        public async Task<IEnumerable<string>> Handle(GetAllStaffRolesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var staffRoleType = typeof(StaffRole);
                var fieldInfos = staffRoleType.GetFields();

                var staffRoles = new List<string>();
                fieldInfos.ToList().ForEach(fieldInfo =>
                {
                    staffRoles.Add(fieldInfo.GetValue(null).ToString());

                });

                return staffRoles;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
