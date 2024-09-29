using Application.DTO.BaseDTO;

namespace Application.DTO.Response
{
    public class GetCourierResponseDTO : CourierBaseDTO
    {
        public Guid Id { get; set; }
    }
}