namespace Application.DTO.Request
{
    public class CreateIncomingOrderProductFromCsvRequestDTO : BaseDTO.BaseDTO
    {
        public Stream CsvFileStream { get; set; }
    }
}
