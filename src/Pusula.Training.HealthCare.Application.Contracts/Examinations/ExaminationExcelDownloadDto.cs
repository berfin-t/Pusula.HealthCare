using System;

namespace Pusula.Training.HealthCare.Examinations;

public class ExaminationExcelDownloadDto
{
    public string DownloadToken { get; set; } = null!;
    public string? FilterText { get; set; }
    public virtual string? IdentityNumber { get; set; }
    public DateTime? VisitDate { get; set; }
    public virtual string? Notes { get; set; }
    public string? ChronicDiseases { get; set; }
    public string? Allergies { get; set; }
    public string? Medications { get; set; }
    public string? Diagnosis { get; set; }
    public string? Prescription { get; set; }
    public string? ImagingResults { get; set; }
    public Guid? PatientId { get; set; }
    public Guid? DoctorId { get; set; }

    public int ProtocolNo { get; set; }

    public ExaminationExcelDownloadDto()
    {
    }
}