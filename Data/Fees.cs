using System.ComponentModel.DataAnnotations;

public class Fees
{
    [Key]
    public int FeeID { get; set; }
    public int StudentID { get; set; }
    public double TotalAmount { get; set; }
    public double PaidAmount { get; set; }
    public double DueAmount { get; set; }
    public DateOnly LastPaymentDate { get; set; }
    public string Status { get; set; }
}