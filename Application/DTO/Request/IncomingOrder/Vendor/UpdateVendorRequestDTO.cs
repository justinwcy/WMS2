using Application.DTO.BaseDTO;

namespace Application.DTO.Request
{
    public class UpdateVendorRequestDTO : VendorBaseDTO
    {
        public Guid Id { get; set; }
    }
}
