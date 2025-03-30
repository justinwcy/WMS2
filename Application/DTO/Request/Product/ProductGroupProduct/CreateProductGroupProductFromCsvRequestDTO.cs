namespace Application.DTO.Request
{
    public class CreateProductGroupProductFromCsvRequestDTO : BaseDTO.BaseDTO
    {
        public Stream CsvFileStream { get; set; }
    }
}
