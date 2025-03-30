namespace Application.DTO.Request
{
    public class CreateCompanyFromCsvRequestDTO : BaseDTO.BaseDTO
    {
        public Stream CsvFileStream { get; set; }
    }
}
