namespace Application.DTO.Request
{
    public class CreateWarehouseFromCsvRequestDTO : BaseDTO.BaseDTO
    {
        public Stream CsvFileStream { get; set; }
    }
}
