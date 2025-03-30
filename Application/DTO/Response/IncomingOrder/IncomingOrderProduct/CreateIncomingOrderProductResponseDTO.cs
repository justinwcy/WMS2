using Application.DTO.BaseDTO;

namespace Application.DTO.Response
{
    public class CreateIncomingOrderProductResponseDTO : IncomingOrderProductBaseDTO
    {
        public Guid Id { get; set; }
    }
}
