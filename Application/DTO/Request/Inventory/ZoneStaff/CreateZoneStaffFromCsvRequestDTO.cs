namespace Application.DTO.Request
{
    public class CreateZoneStaffFromCsvRequestDTO : BaseDTO.BaseDTO
    {
        public Stream CsvFileStream { get; set; }
    }
}
