using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public DateTime Date { get; set; }
        public string? Description{ get; set; }
        public double Amount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

        [ForeignKey("TypeId")]
        public TransactionType TransactionType { get; set; }
    }
}