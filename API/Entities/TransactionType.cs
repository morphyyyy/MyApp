namespace API.Entities
{
    public class TransactionType
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool Recurring { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
