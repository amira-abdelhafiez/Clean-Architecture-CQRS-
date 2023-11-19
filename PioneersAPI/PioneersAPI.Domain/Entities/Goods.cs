using PioneersAPI.Domain.Common;
namespace PioneersAPI.Domain
{
    public class Goods : BaseEntity
    {
        public string? TransactionId { get; set; }
        public DateTime? TransactionDate { get; set; }
        public double? Amount { get; set; }
        public string? Direction { get; set; }
        public string? Comments { get; set; }
    }
}
