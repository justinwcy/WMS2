namespace Application.DTO.Request
{
    public class CreateCustomerFromCsvRequestDTO : BaseDTO.BaseDTO
    {
        public Stream CsvFileStream { get; set; }
    }
}
