namespace PayFlowX.Models
{
    public class Transaction
    {

public int Id { get; set; }
        public string Reference { get; set; } = string.Empty;
        public decimal Amount { get; set; } 
        public string Currency { get; set; } = "GBP";
        public string Status { get; set;  } = "Pending";
        public DateTime CreatedAT { get; set; } = DateTime.UtcNow;


    }

}
