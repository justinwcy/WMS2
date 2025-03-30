using Application.DTO.BaseDTO;

namespace Application.DTO.Response
{
    public class CreateBinResponseDTO : BinBaseDTO
    {
        public Guid Id { get; set; }
    }
}
