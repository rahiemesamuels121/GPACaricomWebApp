using System.ComponentModel.DataAnnotations;

namespace GPACARICOM.Models;

/// <summary>
/// Data for the Summer Work and Travel registration form.
/// Nullable values preserve unanswered fields; persistence and submission are configured separately.
/// </summary>
public class WATApplications : IValidatableObject
{
    // Personal information
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public string? ParishOfBirth { get; set; }
    public string? HomeAddress { get; set; }
    [EmailAddress]
    public string? Email { get; set; }
    public string? CellPhone { get; set; }
    public string? HomePhone { get; set; }
    [RegularExpression("^(F|M)$")]
    public string? Gender { get; set; }
    public string? Instagram { get; set; }
    public string? Facebook { get; set; }
    public string? TikTok { get; set; }
    public string? OtherSocialMedia { get; set; }

    // University information
    public string? University { get; set; }
    public string? SchoolAddress { get; set; }
    public string? ProgrammeOfStudy { get; set; }
    [Range(1, 4)]
    public int? AcademicYear { get; set; }
    public DateOnly? DepartureDate { get; set; }
    public DateOnly? ReturnDate { get; set; }

    // Emergency contacts
    public string? Emergency1Name { get; set; }
    public string? Emergency1Relation { get; set; }
    public string? Emergency1Address { get; set; }
    public string? Emergency1Phone { get; set; }
    [EmailAddress]
    public string? Emergency1Email { get; set; }
    public string? Emergency2Name { get; set; }
    public string? Emergency2Relation { get; set; }
    public string? Emergency2Address { get; set; }
    public string? Emergency2Phone { get; set; }
    [EmailAddress]
    public string? Emergency2Email { get; set; }

    // Programme history. Null means unanswered; false means No.
    public bool? PreviousJ1 { get; set; }
    public string? PreviousJ1Details { get; set; }
    [Range(0, int.MaxValue)]
    public int? PreviousJ1Count { get; set; }
    public string? OverseasSponsors { get; set; }
    public string? LocalAgency { get; set; }
    // Identifiers stay strings to preserve leading zeros and non-numeric characters.
    public string? PassportNumber { get; set; }
    public string? Ssn { get; set; }
    public bool? VisaRefused { get; set; }
    public string? VisaRefusedCategory { get; set; }
    public string? ParticipationSummary { get; set; }

    // Section 5: true only when every acknowledgement has been signed or approved.
    // Defaults to false; individual initials, signature and date are not stored here.
    public bool AllAcknowledgementsApproved { get; set; }

    // Office use only; set by authorized staff when submission is implemented.
    public string? ReceivedBy { get; set; }
    public DateOnly? ReceivedDate { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (DepartureDate.HasValue && ReturnDate.HasValue && ReturnDate.Value < DepartureDate.Value)
        {
            yield return new ValidationResult(
                "Return date must be on or after the departure date.",
                new[] { nameof(ReturnDate) });
        }
    }
}
