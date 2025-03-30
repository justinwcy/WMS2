namespace Application.DTO.Request
{
    public class CreateProductShopFromCsvRequestDTO : BaseDTO.BaseDTO
    {
        public Stream CsvFileStream { get; set; }
    }
}
