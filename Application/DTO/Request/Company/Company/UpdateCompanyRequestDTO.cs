using Application.DTO.BaseDTO;

namespace Application.DTO.Request
{
    public class UpdateCompanyRequestDTO : CompanyBaseDTO
    {
        public Guid Id { get; set; }
    }
}
