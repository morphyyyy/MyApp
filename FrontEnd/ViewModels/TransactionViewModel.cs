namespace FrontEnd.ViewModels
{
    public class TransactionViewModel
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public string? Description{ get; set; }
        public double? Amount { get; set; }
        public double? Balance { get; set; }
    }
}
