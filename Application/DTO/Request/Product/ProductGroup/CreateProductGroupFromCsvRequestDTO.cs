namespace Application.DTO.Request
{
    public class CreateProductGroupFromCsvRequestDTO : BaseDTO.BaseDTO
    {
        public Stream CsvFileStream { get; set; }
    }
}
