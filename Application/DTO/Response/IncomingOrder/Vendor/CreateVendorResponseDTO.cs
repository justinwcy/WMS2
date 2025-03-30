using Application.DTO.BaseDTO;

namespace Application.DTO.Response
{
    public class CreateVendorResponseDTO : VendorBaseDTO
    {
        public Guid Id { get; set; }
    }
}
