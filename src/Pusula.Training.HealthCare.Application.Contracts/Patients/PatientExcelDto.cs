using System;

namespace Pusula.Training.HealthCare.Patients;

public class PatientExcelDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    public string IdentityNumber { get; set; } = null!;
    public string EmailAddress { get; set; } = null!;
    public string MobilePhoneNumber { get; set; } = null!;
    public string? HomePhoneNumber { get; set; }
    public EnumGender Gender { get; set; }
    public EnumBloodType BloodType { get; set; }
    public EnumMaritalStatus MaritalStatus { get; set; }
    public string Country { get; set; } = null!;
}