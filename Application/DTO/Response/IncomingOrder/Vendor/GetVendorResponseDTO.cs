using Application.DTO.BaseDTO;

namespace Application.DTO.Response
{
    public class GetVendorResponseDTO : VendorBaseDTO
    {
        public Guid Id { get; set; }
    }
}