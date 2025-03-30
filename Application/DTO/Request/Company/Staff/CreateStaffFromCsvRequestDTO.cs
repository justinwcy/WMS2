namespace Application.DTO.Request
{
    public class CreateStaffFromCsvRequestDTO : BaseDTO.BaseDTO
    {
        public Stream CsvFileStream { get; set; }
    }
}
