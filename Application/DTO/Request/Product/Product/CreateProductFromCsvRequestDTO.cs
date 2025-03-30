namespace Application.DTO.Request
{
    public class CreateProductFromCsvRequestDTO : BaseDTO.BaseDTO
    {
        public Stream CsvFileStream { get; set; }
    }
}
