
using GPACARICOM.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace GPACARICOM.Services.PDF;

public sealed class WorkAndTravelPdfService
{
    private const string Navy = "#1A2E3B";
    private const string Cyan = "#00A9CE";
    private const string Yellow = "#FDB813";
    private const string Background = "#F5F7FA";
    private const string Muted = "#667785";
    private const string Border = "#E5EBEF";

    private readonly IWebHostEnvironment _environment;

    public WorkAndTravelPdfService(
        IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public byte[] Generate(
        WorkAndTravelApplication application,
        int programmeYear = 2027)
    {
        ArgumentNullException.ThrowIfNull(application);

        string fullName = string.Join(" ", new[]
        {
            application.FirstName,
            application.MiddleName,
            application.LastName
        }.Where(x => !string.IsNullOrWhiteSpace(x)));

        byte[]? logo = LoadLogo();

        return Document.Create(document =>
        {
            document.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(35);
                page.PageColor(Colors.White);

                page.DefaultTextStyle(x => x
    .FontFamily("Lato")
    .FontSize(9)
    .FontColor(Navy));

                // Header repeated on every page
                page.Header().Column(header =>
                {
                    header.Item().Row(row =>
                    {
                        row.RelativeItem().Column(left =>
                        {
                            if (logo is not null)
                            {
                                left.Item()
                                    .Height(46)
                                    .Image(logo)
                                    .FitArea();
                            }
                            else
                            {
                                left.Item()
                                    .Text("GPA CARIBBEAN")
                                    .Bold()
                                    .FontSize(17)
                                    .FontColor(Cyan);
                            }
                        });

                        row.ConstantItem(145)
                            .AlignRight()
                            .Column(right =>
                            {
                                right.Item()
                                    .AlignRight()
                                    .Text($"SUMMER {programmeYear}")
                                    .Bold()
                                    .FontSize(11);

                                right.Item()
                                    .AlignRight()
                                    .Text("OFFICIAL APPLICATION")
                                    .FontSize(8)
                                    .FontColor(Muted);
                            });
                    });

                    header.Item()
                        .PaddingTop(10)
                        .LineHorizontal(2)
                        .LineColor(Yellow);
                });

                page.Content()
                    .PaddingTop(18)
                    .Column(content =>
                    {
                        content.Spacing(12);

                        content.Item()
                            .Background(Navy)
                            .Padding(18)
                            .Column(title =>
                            {
                                title.Item()
                                    .Text("WORK & TRAVEL PROGRAMME")
                                    .Bold()
                                    .FontSize(9)
                                    .FontColor(Yellow);

                                title.Item()
                                    .PaddingTop(4)
                                    .Text("Application Record")
                                    .Bold()
                                    .FontSize(20)
                                    .FontColor(Colors.White);

                                title.Item()
                                    .PaddingTop(6)
                                    .Text(
                                        $"Summer {programmeYear} | GPA Caribbean")
                                    .FontSize(9)
                                    .FontColor(Colors.White);
                            });

                        content.Item().Element(c =>
                            Section(c, "APPLICATION SUMMARY", col =>
                            {
                                Field(col, "Applicant", fullName);
                                Field(col, "Received Date",
                                    Date(application.ReceivedDate));
                            }));

                        content.Item().Element(c =>
                            Section(c, "01  PERSONAL INFORMATION", col =>
                            {
                                Field(col, "First Name",
                                    application.FirstName);
                                Field(col, "Middle Name",
                                    application.MiddleName);
                                Field(col, "Last Name",
                                    application.LastName);
                                Field(col, "Date of Birth",
                                    Date(application.DateOfBirth));
                                Field(col, "Parish of Birth",
                                    application.ParishOfBirth);
                                Field(col, "Gender",
                                    Gender(application.Gender));
                                Field(col, "Email",
                                    application.Email);
                                Field(col, "Cell Phone",
                                    application.CellPhone);
                                Field(col, "Home Phone",
                                    application.HomePhone);
                                Field(col, "Address",
                                    application.HomeAddress);
                            }));

                        content.Item().Element(c =>
                            Section(c, "02  SOCIAL MEDIA", col =>
                            {
                                Field(col, "Instagram",
                                    application.Instagram);
                                Field(col, "Facebook",
                                    application.Facebook);
                                Field(col, "TikTok",
                                    application.TikTok);
                                Field(col, "Other",
                                    application.OtherSocialMedia);
                            }));

                        content.Item().Element(c =>
                            Section(c, "03  UNIVERSITY & TRAVEL", col =>
                            {
                                Field(col, "University",
                                    application.University);
                                Field(col, "Programme of Study",
                                    application.ProgrammeOfStudy);
                                Field(col, "Academic Year",
                                    AcademicYear(application.AcademicYear));
                                Field(col, "School Address",
                                    application.SchoolAddress);
                                Field(col, "Departure Date",
                                    Date(application.DepartureDate));
                                Field(col, "Return Date",
                                    Date(application.ReturnDate));
                            }));

                        content.Item().Element(c =>
                            Section(c, "04  EMERGENCY CONTACT 1", col =>
                            {
                                Field(col, "Name",
                                    application.Emergency1Name);
                                Field(col, "Relationship",
                                    application.Emergency1Relation);
                                Field(col, "Phone",
                                    application.Emergency1Phone);
                                Field(col, "Email",
                                    application.Emergency1Email);
                                Field(col, "Address",
                                    application.Emergency1Address);
                            }));

                        content.Item().Element(c =>
                            Section(c, "05  EMERGENCY CONTACT 2", col =>
                            {
                                Field(col, "Name",
                                    application.Emergency2Name);
                                Field(col, "Relationship",
                                    application.Emergency2Relation);
                                Field(col, "Phone",
                                    application.Emergency2Phone);
                                Field(col, "Email",
                                    application.Emergency2Email);
                                Field(col, "Address",
                                    application.Emergency2Address);
                            }));

                        content.Item().Element(c =>
                            Section(c, "06  PREVIOUS J-1 EXPERIENCE", col =>
                            {
                                Field(col, "Previously Participated",
                                    YesNo(application.PreviousJ1));
                                Field(col, "Number of Programmes",
                                    application.PreviousJ1Count?.ToString());
                                Field(col, "Previous Details",
                                    application.PreviousJ1Details);
                                Field(col, "Overseas Sponsors",
                                    application.OverseasSponsors);
                                Field(col, "Local Agency",
                                    application.LocalAgency);
                            }));

                        content.Item().Element(c =>
                            Section(c, "07  PASSPORT & VISA", col =>
                            {
                                Field(col, "Passport Number",
                                    Mask(application.PassportNumber));
                                Field(col, "SSN",
                                    "Redacted for privacy");
                                Field(col, "Visa Previously Refused",
                                    YesNo(application.VisaRefused));
                                Field(col, "Refusal Category",
                                    application.VisaRefusedCategory);
                            }));

                        content.Item().Element(c =>
                            Section(c, "08  PARTICIPATION SUMMARY", col =>
                            {
                                Field(col, "Applicant Statement",
                                    application.ParticipationSummary);
                            }));

                        content.Item().Element(c =>
                            Section(c, "09  ACKNOWLEDGEMENTS", col =>
                            {
                                Field(col, "Acknowledgement Status",
                                    application.AllAcknowledgementsApproved
                                        ? "Approved"
                                        : "Not completed");
                            }));

                        content.Item().Element(c =>
                            Section(c, "10  APPLICATION PROCESSING", col =>
                            {
                                Field(col, "Received By",
                                    application.ReceivedBy);
                                Field(col, "Received Date",
                                    Date(application.ReceivedDate));
                            }));
                    });

                page.Footer().Column(footer =>
                {
                    footer.Item()
                        .LineHorizontal(1)
                        .LineColor(Border);

                    footer.Item().PaddingTop(7).Row(row =>
                    {
                        row.RelativeItem()
                            .Text("GPA Caribbean | Work & Travel")
                            .FontSize(8)
                            .FontColor(Muted);

                        row.RelativeItem()
                            .AlignRight()
                            .Text(text =>
                            {
                                text.Span("Page ");
                                text.CurrentPageNumber();
                                text.Span(" of ");
                                text.TotalPages();
                            });
                    });
                });
            });
        }).GeneratePdf();
    }

    private byte[]? LoadLogo()
    {
        string path = Path.Combine(
            _environment.WebRootPath,
            "images",
            "logomain.png");

        return File.Exists(path)
            ? File.ReadAllBytes(path)
            : null;
    }

    private static void Section(
        IContainer container,
        string title,
        Action<ColumnDescriptor> fields)
    {
        container.Column(column =>
        {
            column.Spacing(7);

            column.Item()
                .Background(Background)
                .BorderBottom(1)
                .BorderColor(Cyan)
                .Padding(9)
                .Text(title)
                .Bold()
                .FontSize(10)
                .FontColor(Navy);

            column.Item()
                .PaddingHorizontal(5)
                .Column(fields);
        });
    }

    private static void Field(
        ColumnDescriptor column,
        string label,
        string? value)
    {
        column.Item().PaddingVertical(5).Row(row =>
        {
            row.ConstantItem(155)
                .Text(label.ToUpperInvariant())
                .FontSize(7.5f)
                .FontColor(Muted);

            row.RelativeItem()
                .Text(string.IsNullOrWhiteSpace(value)
                    ? "Not provided"
                    : value)
                .FontSize(9)
                .FontColor(Navy);
        });

        column.Item()
            .LineHorizontal(0.5f)
            .LineColor(Border);
    }

    private static string Date(DateOnly? value) =>
        value?.ToString("MMMM d, yyyy") ?? "Not provided";

    private static string YesNo(bool? value) =>
        value switch
        {
            true => "Yes",
            false => "No",
            _ => "Not provided"
        };

    private static string Gender(string? value) =>
        value?.ToUpperInvariant() switch
        {
            "M" => "Male",
            "F" => "Female",
            _ => "Not provided"
        };

    private static string AcademicYear(int? value) =>
        value.HasValue
            ? $"Year {value.Value}"
            : "Not provided";

    private static string Mask(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return "Not provided";

        return value.Length <= 4
            ? "****"
            : "****" + value[^4..];
    }


    public byte[] GenerateTestPdf()
    {
        return Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(30);

                page.Content()
                    .Text("GPA Caribbean PDF Test")
                    .FontFamily("Lato")
                    .FontSize(20);
            });
        }).GeneratePdf();
    }
}
