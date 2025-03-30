namespace Application.DTO.Request
{
    public class CreateCustomerOrderDetailFromCsvRequestDTO : BaseDTO.BaseDTO
    {
        public Stream CsvFileStream { get; set; }
    }
}
