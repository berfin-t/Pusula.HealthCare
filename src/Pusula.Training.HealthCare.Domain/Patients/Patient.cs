using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Pusula.Training.HealthCare.Addresses;
using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.Insurances;
using Pusula.Training.HealthCare.PatientNotes;
using Pusula.Training.HealthCare.PatientTypes;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.Patients;

public class Patient : FullAuditedAggregateRoot<Guid>
{
    public int No { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string FullName { get; private set; }
    public DateTime BirthDate { get; private set; }
    public string? IdentityNumber { get; private set; }
    public string? PassportNumber { get; private set; }
    public string EmailAddress { get; private set; }
    public string MobilePhoneNumberCode { get; private set; }
    public string MobilePhoneNumber { get; private set; }
    public string? HomePhoneNumberCode { get; private set; }
    public string? HomePhoneNumber { get; private set; }
    public EnumGender Gender { get; private set; }
    public EnumBloodType BloodType { get; private set; }
    public EnumMaritalStatus MaritalStatus { get; private set; }
    public Guid CountryId { get; private set; }
    public Country Country { get; set; }
    public Guid PatientTypeId { get; private set; }
    public PatientType PatientType { get; set; }

    public Guid InsuranceId { get; set; }
    public Insurance Insurance { get; set; }

    public ICollection<Address> Addresses { get; set; }
    public ICollection<PatientNote> PatientNotes { get; set; }

    protected Patient()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        FullName = string.Empty;
        EmailAddress = string.Empty;
        MobilePhoneNumber = string.Empty;
        MobilePhoneNumberCode = string.Empty;
        Addresses = [];
        PatientNotes = [];
    }

    public Patient(
        Guid id,
        Guid countryId,
        Guid patientTypeId,
        Guid insuranceId,
        string firstName,
        string lastName,
        DateTime birthDate,
        string? identityNumber,
        string? passportNumber,
        string emailAddress,
        string mobilePhoneNumberCode,
        string mobilePhoneNumber,
        string? homePhoneNumberCode,
        string? homePhoneNumber,
        EnumGender gender,
        EnumBloodType bloodType,
        EnumMaritalStatus maritalStatus
    ) : base(id)
    {
        SetCountryId(countryId);
        SetPatientTypeId(patientTypeId);
        SetInsuranceId(insuranceId);
        SetName(firstName, lastName);
        SetBirthDate(birthDate);
        SetIdentityNumber(identityNumber);
        SetPassportNumber(passportNumber);
        SetEmailAddress(emailAddress);
        SetMobilePhoneNumberCode(mobilePhoneNumberCode);
        SetMobilePhoneNumber(mobilePhoneNumber);
        SetHomePhoneNumberCode(homePhoneNumberCode);
        SetHomePhoneNumber(homePhoneNumber);
        SetGender(gender);
        SetBloodType(bloodType);
        SetMaritalStatus(maritalStatus);
    }

    private void SetFirstName(string firstName) =>
        FirstName = Check.NotNullOrWhiteSpace(firstName, nameof(firstName), PatientConsts.FirstNameMaxLength);

    private void SetLastName(string lastName) =>
        LastName = Check.NotNullOrWhiteSpace(lastName, nameof(lastName), PatientConsts.LastNameMaxLength);

    private void SetFullName(string firstName, string lastName) => FullName = $"{firstName} {lastName}";

    public void SetName(string firstName, string lastName)
    {
        SetFirstName(firstName);
        SetLastName(lastName);
        SetFullName(firstName, lastName);
    }

    public void SetCountryId(Guid countryId) => CountryId = Check.NotDefaultOrNull<Guid>(countryId, nameof(countryId));

    public void SetPatientTypeId(Guid patientTypeId) =>
        PatientTypeId = Check.NotDefaultOrNull<Guid>(patientTypeId, nameof(patientTypeId));

    public void SetInsuranceId(Guid insurance) =>
        InsuranceId = Check.NotDefaultOrNull<Guid>(insurance, nameof(insurance));

    public void SetBirthDate(DateTime birthDate) => BirthDate = birthDate;

    public void SetIdentityNumber(string? identityNumber) =>
        IdentityNumber =
            Check.Length(identityNumber, nameof(identityNumber), PatientConsts.IdentityNumberMaxLength);

    public void SetPassportNumber(string? passportNumber) =>
        PassportNumber = Check.Length(passportNumber, nameof(passportNumber), PatientConsts.PassportNumberMaxLength);

    public void SetEmailAddress(string emailAddress) =>
        EmailAddress = Check.NotNullOrWhiteSpace(
            emailAddress,
            nameof(emailAddress),
            PatientConsts.EmailAddressMaxLength
        );

    public void SetMobilePhoneNumberCode(string mobilePhoneNumberCode) =>
        MobilePhoneNumberCode = Check.NotNullOrWhiteSpace(
            mobilePhoneNumberCode,
            nameof(mobilePhoneNumberCode),
            CountryConsts.PhoneCodeMaxLength
        );

    public void SetMobilePhoneNumber(string mobilePhoneNumber) =>
        MobilePhoneNumber = Check.NotNullOrWhiteSpace(
            mobilePhoneNumber,
            nameof(mobilePhoneNumber),
            PatientConsts.PhoneNumberMaxLength
        );

    public void SetHomePhoneNumberCode(string? homePhoneNumberCode) =>
        HomePhoneNumberCode = Check.Length(
            homePhoneNumberCode,
            nameof(homePhoneNumberCode),
            CountryConsts.PhoneCodeMaxLength
        );

    public void SetHomePhoneNumber(string? homePhoneNumber) =>
        HomePhoneNumber = Check.Length(homePhoneNumber, nameof(homePhoneNumber), PatientConsts.PhoneNumberMaxLength);

    public void SetGender(EnumGender gender) => Gender = gender;

    public void SetBloodType(EnumBloodType bloodType) => BloodType = bloodType;

    public void SetMaritalStatus(EnumMaritalStatus maritalStatus) => MaritalStatus = maritalStatus;
}