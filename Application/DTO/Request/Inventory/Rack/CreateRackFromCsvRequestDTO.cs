namespace Application.DTO.Request
{
    public class CreateRackFromCsvRequestDTO : BaseDTO.BaseDTO
    {
        public Stream CsvFileStream { get; set; }
    }
}
