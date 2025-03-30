namespace Application.DTO.Request
{
    public class CreateVendorFromCsvRequestDTO : BaseDTO.BaseDTO
    {
        public Stream CsvFileStream { get; set; }
    }
}
