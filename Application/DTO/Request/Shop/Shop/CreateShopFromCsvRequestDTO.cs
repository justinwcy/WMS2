namespace Application.DTO.Request
{
    public class CreateShopFromCsvRequestDTO : BaseDTO.BaseDTO
    {
        public Stream CsvFileStream { get; set; }
    }
}
