namespace Application.DTO.Request
{
    public class CreateZoneFromCsvRequestDTO : BaseDTO.BaseDTO
    {
        public Stream CsvFileStream { get; set; }
    }
}
