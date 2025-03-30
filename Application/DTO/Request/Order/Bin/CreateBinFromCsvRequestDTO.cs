namespace Application.DTO.Request
{
    public class CreateBinFromCsvRequestDTO : BaseDTO.BaseDTO
    {
        public Stream CsvFileStream { get; set; }
    }
}
