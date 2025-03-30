namespace Application.DTO.Request
{
    public class CreateInventoryFromCsvRequestDTO : BaseDTO.BaseDTO
    {
        public Stream CsvFileStream { get; set; }
    }
}
