using Application.DTO.BaseDTO;

namespace Application.DTO.Request
{
    public class UpdateCourierRequestDTO : CourierBaseDTO
    {
        public Guid Id { get; set; }
    }
}
