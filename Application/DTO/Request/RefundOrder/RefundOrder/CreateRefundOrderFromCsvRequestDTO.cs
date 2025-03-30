namespace Application.DTO.Request
{
    public class CreateRefundOrderFromCsvRequestDTO : BaseDTO.BaseDTO
    {
        public Stream CsvFileStream { get; set; }
    }
}
