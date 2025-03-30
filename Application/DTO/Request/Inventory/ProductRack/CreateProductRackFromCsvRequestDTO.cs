namespace Application.DTO.Request
{
    public class CreateProductRackFromCsvRequestDTO : BaseDTO.BaseDTO
    {
        public Stream CsvFileStream { get; set; }
    }
}
