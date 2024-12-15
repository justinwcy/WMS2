using Application.DTO.BaseDTO;

namespace Application.DTO.Request
{
    public class UpdateBinRequestDTO : BinBaseDTO
    {
        public Guid Id { get; set; }
    }
}
