namespace Application.DTO.Request
{
    public class CreateIncomingOrderFromCsvRequestDTO : BaseDTO.BaseDTO
    {
        public Stream CsvFileStream { get; set; }
    }
}
