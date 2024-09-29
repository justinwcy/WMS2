namespace Application.DTO.BaseDTO
{
    public class CustomerBaseDTO : BaseDTO
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Address { get; set; }
        public string Email { get; set; }
    }
}
