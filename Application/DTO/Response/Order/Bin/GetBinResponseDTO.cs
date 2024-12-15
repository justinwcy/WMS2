using Application.DTO.BaseDTO;

namespace Application.DTO.Response
{
    public class GetBinResponseDTO : BinBaseDTO
    {
        public Guid Id { get; set; }
    }
}