namespace Application.DTO.Request
{
    public class CreateCourierFromCsvRequestDTO : BaseDTO.BaseDTO
    {
        public Stream CsvFileStream { get; set; }
    }
}
