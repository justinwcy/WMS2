using Application.DTO.BaseDTO;

namespace Application.DTO.Response
{
    public class CreateCourierResponseDTO : CourierBaseDTO
    {
        public Guid Id { get; set; }
    }
}
