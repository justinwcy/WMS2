using Application.DTO.BaseDTO;

namespace Application.DTO.Response
{
    public class GetIncomingOrderProductResponseDTO : IncomingOrderProductBaseDTO
    {
        public Guid Id { get; set; }
    }
}