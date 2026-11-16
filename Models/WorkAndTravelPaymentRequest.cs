using System.ComponentModel.DataAnnotations;

public class WorkAndTravelPaymentRequest
{
    [Range(
      typeof(decimal),
      "0.01",
      "999999999.99",
      ErrorMessage = "Enter a valid payment amount."
  )]
    public decimal AmountPaid { get; set; }
    [Required]
    [StringLength(3, MinimumLength = 3)]
    public string CurrencyCode { get; set; }
        = "USD";
    [Required]
    [StringLength(100)]
    public string PaymentReference { get; set; }
        = string.Empty;
    [StringLength(500)]

    public DateTime? SubmittedAt { get; set; }
    [StringLength(1000)]
    public string? Notes { get; set; }

}