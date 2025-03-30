namespace Application.DTO.Request
{
    public class CreateCustomerOrderFromCsvRequestDTO : BaseDTO.BaseDTO
    {
        public Stream CsvFileStream { get; set; }
    }
}
