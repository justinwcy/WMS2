namespace Application.DTO.Request
{
    public class CreateRefundOrderProductFromCsvRequestDTO : BaseDTO.BaseDTO
    {
        public Stream CsvFileStream { get; set; }
    }
}
