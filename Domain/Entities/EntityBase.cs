namespace Domain.Entities
{
    public class EntityBase
    {
        public Guid Id { get; set; }
        public Guid CreatedBy { get; set; }
    }
}