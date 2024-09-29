using Application.DTO.BaseDTO;

namespace Application.DTO.Response
{
    public class GetCompanyResponseDTO : CompanyBaseDTO
    {
        public Guid Id { get; set; }
    }
}