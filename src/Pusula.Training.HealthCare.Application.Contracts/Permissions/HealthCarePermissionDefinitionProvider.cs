using Pusula.Training.HealthCare.Localization;
using System.Text.RegularExpressions;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;
using static Pusula.Training.HealthCare.Permissions.HealthCarePermissions;

namespace Pusula.Training.HealthCare.Permissions;

public class HealthCarePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(GroupName);

        myGroup.AddPermission(Dashboard.Host, L("Permission:Dashboard"), MultiTenancySides.Host);
        myGroup.AddPermission(Dashboard.Tenant, L("Permission:Dashboard"), MultiTenancySides.Tenant);

        // Define other permissions here
        SetPatientPermissions(myGroup);
        SetCountryPermissions(myGroup);
        SetCityPermissions(myGroup);
        SetDistrictPermissions(myGroup);
        SetPatientTypePermissions(myGroup);
        SetProtocolsPermissions(myGroup);
        SetPatientProtocolsPermissions(myGroup);
        SetProtocolTypesPermissions(myGroup);
        SetInsurancesPermissions(myGroup);
        SetExaminationsPermissions(myGroup);

        var appointmentPermission =
            myGroup.AddPermission(HealthCarePermissions.Appointments.Default, L("Permission:Appointments"));
        appointmentPermission.AddChild(HealthCarePermissions.Appointments.Create, L("Permission:Create"));
        appointmentPermission.AddChild(HealthCarePermissions.Appointments.Edit, L("Permission:Edit"));
        appointmentPermission.AddChild(HealthCarePermissions.Appointments.Delete, L("Permission:Delete"));

        var appointmentReportPermission = myGroup.AddPermission(
            HealthCarePermissions.AppointmentReports.Default, L("Permission:AppointmentReports")
        );
        appointmentReportPermission.AddChild(HealthCarePermissions.AppointmentReports.Create, L("Permission:Create"));
        appointmentReportPermission.AddChild(HealthCarePermissions.AppointmentReports.Edit, L("Permission:Edit"));
        appointmentReportPermission.AddChild(HealthCarePermissions.AppointmentReports.Delete, L("Permission:Delete"));

        var appointmentTypePermission = myGroup.AddPermission(
            HealthCarePermissions.AppointmentTypes.Default, L("Permission:AppointmentTypes")
        );
        appointmentTypePermission.AddChild(HealthCarePermissions.AppointmentTypes.Create, L("Permission:Create"));
        appointmentTypePermission.AddChild(HealthCarePermissions.AppointmentTypes.Edit, L("Permission:Edit"));
        appointmentTypePermission.AddChild(HealthCarePermissions.AppointmentTypes.Delete, L("Permission:Delete"));

        var departmentPermission = myGroup.AddPermission(
            HealthCarePermissions.Departments.Default, L("Permission:Departments")
        );
        departmentPermission.AddChild(HealthCarePermissions.Departments.Create, L("Permission:Create"));
        departmentPermission.AddChild(HealthCarePermissions.Departments.Edit, L("Permission:Edit"));
        departmentPermission.AddChild(HealthCarePermissions.Departments.Delete, L("Permission:Delete"));

        var hospitalPermission = myGroup.AddPermission(
            HealthCarePermissions.Hospitals.Default, L("Permission:Hospitals")
        );
        hospitalPermission.AddChild(HealthCarePermissions.Hospitals.Create, L("Permission:Create"));
        hospitalPermission.AddChild(HealthCarePermissions.Hospitals.Edit, L("Permission:Edit"));
        hospitalPermission.AddChild(HealthCarePermissions.Hospitals.Delete, L("Permission:Delete"));

        var doctorPermission = myGroup.AddPermission(HealthCarePermissions.Doctors.Default, L("Permission:Doctors"));
        doctorPermission.AddChild(HealthCarePermissions.Doctors.Create, L("Permission:Create"));
        doctorPermission.AddChild(HealthCarePermissions.Doctors.Edit, L("Permission:Edit"));
        doctorPermission.AddChild(HealthCarePermissions.Doctors.Delete, L("Permission:Delete"));

        var titlePermission = myGroup.AddPermission(HealthCarePermissions.Titles.Default, L("Permission:Titles"));
        titlePermission.AddChild(HealthCarePermissions.Titles.Create, L("Permission:Create"));
        titlePermission.AddChild(HealthCarePermissions.Titles.Edit, L("Permission:Edit"));
        titlePermission.AddChild(HealthCarePermissions.Titles.Delete, L("Permission:Delete"));

        var testPermission = myGroup.AddPermission(HealthCarePermissions.Tests.Default, L("Permission:Tests"));
        testPermission.AddChild(HealthCarePermissions.Tests.Create, L("Permission:Create"));
        testPermission.AddChild(HealthCarePermissions.Tests.Edit, L("Permission:Edit"));
        testPermission.AddChild(HealthCarePermissions.Tests.Delete, L("Permission:Delete"));

        var testTypePermission = myGroup.AddPermission(
            HealthCarePermissions.TestTypes.Default, L("Permission:TestTypes")
        );
        testTypePermission.AddChild(HealthCarePermissions.TestTypes.Create, L("Permission:Create"));
        testTypePermission.AddChild(HealthCarePermissions.TestTypes.Edit, L("Permission:Edit"));
        testTypePermission.AddChild(HealthCarePermissions.TestTypes.Delete, L("Permission:Delete"));

        var testGroupPermission = myGroup.AddPermission(
            HealthCarePermissions.TestGroups.Default, L("Permission:TestGroups")
        );
        testTypePermission.AddChild(HealthCarePermissions.TestGroups.Create, L("Permission:Create"));
        testTypePermission.AddChild(HealthCarePermissions.TestGroups.Edit, L("Permission:Edit"));
        testTypePermission.AddChild(HealthCarePermissions.TestGroups.Delete, L("Permission:Delete"));

        var testProcessPermission = myGroup.AddPermission(
            HealthCarePermissions.TestProcesses.Default, L("Permission:TestProcesses")
        );
        testProcessPermission.AddChild(HealthCarePermissions.TestProcesses.Create, L("Permission:Create"));
        testProcessPermission.AddChild(HealthCarePermissions.TestProcesses.Edit, L("Permission:Edit"));
        testProcessPermission.AddChild(HealthCarePermissions.TestProcesses.Delete, L("Permission:Delete"));

        var workListPermission = myGroup.AddPermission(
            HealthCarePermissions.WorkLists.Default, L("Permission:WorkLists")
        );
        workListPermission.AddChild(HealthCarePermissions.WorkLists.Create, L("Permission:Create"));
        workListPermission.AddChild(HealthCarePermissions.WorkLists.Edit, L("Permission:Edit"));
        workListPermission.AddChild(HealthCarePermissions.WorkLists.Delete, L("Permission:Delete"));

#region Radiology

        var radiologyExaminationGroupPermission = myGroup.AddPermission(
            HealthCarePermissions.RadiologyExaminationGroups.Default, L("Permission:RadiologyExaminationGroups")
        );
        radiologyExaminationGroupPermission.AddChild(
            HealthCarePermissions.RadiologyExaminationGroups.Create, L("Permission:Create")
        );
        radiologyExaminationGroupPermission.AddChild(
            HealthCarePermissions.RadiologyExaminationGroups.Edit, L("Permission:Edit")
        );
        radiologyExaminationGroupPermission.AddChild(
            HealthCarePermissions.RadiologyExaminationGroups.Delete, L("Permission:Delete")
        );

        var radiologyExaminationPermission = myGroup.AddPermission(
            HealthCarePermissions.RadiologyExaminations.Default, L("Permission:RadiologyExaminations")
        );
        radiologyExaminationPermission.AddChild(
            HealthCarePermissions.RadiologyExaminations.Create, L("Permission:Create")
        );
        radiologyExaminationPermission.AddChild(HealthCarePermissions.RadiologyExaminations.Edit, L("Permission:Edit"));
        radiologyExaminationPermission.AddChild(
            HealthCarePermissions.RadiologyExaminations.Delete, L("Permission:Delete")
        );

        var radiologyExaminationProcedurePermission = myGroup.AddPermission(
            HealthCarePermissions.RadiologyExaminationProcedures.Default, L("Permission:RadiologyExaminationProcedures")
        );
        radiologyExaminationProcedurePermission.AddChild(
            HealthCarePermissions.RadiologyExaminationProcedures.Create, L("Permission:Create")
        );
        radiologyExaminationProcedurePermission.AddChild(
            HealthCarePermissions.RadiologyExaminationProcedures.Edit, L("Permission:Edit")
        );
        radiologyExaminationProcedurePermission.AddChild(
            HealthCarePermissions.RadiologyExaminationProcedures.Delete, L("Permission:Delete")
        );

        var radiologyExaminationDocumentPermission = myGroup.AddPermission(
            HealthCarePermissions.RadiologyExaminationDocuments.Default, L("Permission:RadiologyExaminationDocuments")
        );
        radiologyExaminationDocumentPermission.AddChild(
            HealthCarePermissions.RadiologyExaminationDocuments.Create, L("Permission:Create")
        );
        radiologyExaminationDocumentPermission.AddChild(
            HealthCarePermissions.RadiologyExaminationDocuments.Edit, L("Permission:Edit")
        );
        radiologyExaminationDocumentPermission.AddChild(
            HealthCarePermissions.RadiologyExaminationDocuments.Delete, L("Permission:Delete")
        );

#endregion
    }

    private static LocalizableString L(string name) => LocalizableString.Create<HealthCareResource>(name);

    private void SetPatientPermissions(PermissionGroupDefinition group)
    {
        var permission = group.AddPermission(HealthCarePermissions.Patients.Default, L("Permission:Patients"));
        permission.AddChild(HealthCarePermissions.Patients.Create, L("Permission:Create"));
        permission.AddChild(HealthCarePermissions.Patients.Edit, L("Permission:Edit"));
        permission.AddChild(HealthCarePermissions.Patients.Delete, L("Permission:Delete"));
    }

    private void SetPatientTypePermissions(PermissionGroupDefinition group)
    {
        var permission = group.AddPermission(HealthCarePermissions.PatientTypes.Default, L("Permission:PatientTypes"));
        permission.AddChild(HealthCarePermissions.PatientTypes.Create, L("Permission:Create"));
        permission.AddChild(HealthCarePermissions.PatientTypes.Edit, L("Permission:Edit"));
        permission.AddChild(HealthCarePermissions.PatientTypes.Delete, L("Permission:Delete"));
    }

    private void SetCountryPermissions(PermissionGroupDefinition group)
    {
        var permission = group.AddPermission(HealthCarePermissions.Countries.Default, L("Permission:Countries"));
        permission.AddChild(HealthCarePermissions.Countries.Create, L("Permission:Create"));
        permission.AddChild(HealthCarePermissions.Countries.Edit, L("Permission:Edit"));
        permission.AddChild(HealthCarePermissions.Countries.Delete, L("Permission:Delete"));
    }

    private void SetCityPermissions(PermissionGroupDefinition group)
    {
        var permission = group.AddPermission(HealthCarePermissions.Cities.Default, L("Permission:Cities"));
        permission.AddChild(HealthCarePermissions.Cities.Create, L("Permission:Create"));
        permission.AddChild(HealthCarePermissions.Cities.Edit, L("Permission:Edit"));
        permission.AddChild(HealthCarePermissions.Cities.Delete, L("Permission:Delete"));
    }

    private void SetDistrictPermissions(PermissionGroupDefinition group)
    {
        var permission = group.AddPermission(HealthCarePermissions.Districts.Default, L("Permission:Districts"));
        permission.AddChild(HealthCarePermissions.Districts.Create, L("Permission:Create"));
        permission.AddChild(HealthCarePermissions.Districts.Edit, L("Permission:Edit"));
        permission.AddChild(HealthCarePermissions.Districts.Delete, L("Permission:Delete"));
    }

    private void SetTestPermissions(PermissionGroupDefinition group)
    {
        var permission = group.AddPermission(HealthCarePermissions.Tests.Default, L("Permission:Tests"));
        permission.AddChild(HealthCarePermissions.Tests.Create, L("Permission:Create"));
        permission.AddChild(HealthCarePermissions.Tests.Edit, L("Permission:Edit"));
        permission.AddChild(HealthCarePermissions.Tests.Delete, L("Permission:Delete"));
    }

    private void SetExaminationsPermissions(PermissionGroupDefinition group)
    {
        var examinations = group.AddPermission(
            HealthCarePermissions.Examinations.Default, L("Permission:Examinations")
        );
        examinations.AddChild(HealthCarePermissions.Examinations.Create, L("Permission:Create"));
        examinations.AddChild(HealthCarePermissions.Examinations.Edit, L("Permission:Edit"));
        examinations.AddChild(HealthCarePermissions.Examinations.Delete, L("Permission:Delete"));
    }

    private void SetTestTypePermissions(PermissionGroupDefinition group)
    {
        var permission = group.AddPermission(HealthCarePermissions.TestTypes.Default, L("Permission:TestTypes"));
        permission.AddChild(HealthCarePermissions.TestTypes.Create, L("Permission:Create"));
        permission.AddChild(HealthCarePermissions.TestTypes.Edit, L("Permission:Edit"));
        permission.AddChild(HealthCarePermissions.TestTypes.Delete, L("Permission:Delete"));
    }

    private void SetTestGroupPermissions(PermissionGroupDefinition group)
    {
        var permission = group.AddPermission(HealthCarePermissions.TestGroups.Default, L("Permission:TestGroups"));
        permission.AddChild(HealthCarePermissions.TestGroups.Create, L("Permission:Create"));
        permission.AddChild(HealthCarePermissions.TestGroups.Edit, L("Permission:Edit"));
        permission.AddChild(HealthCarePermissions.TestGroups.Delete, L("Permission:Delete"));
    }

    private void SetTestProcessPermissions(PermissionGroupDefinition group)
    {
        var permission = group.AddPermission(
            HealthCarePermissions.TestProcesses.Default, L("Permission:TestProcesses")
        );
        permission.AddChild(HealthCarePermissions.TestProcesses.Create, L("Permission:Create"));
        permission.AddChild(HealthCarePermissions.TestProcesses.Edit, L("Permission:Edit"));
        permission.AddChild(HealthCarePermissions.TestProcesses.Delete, L("Permission:Delete"));
    }

    private void SetWorkListPermissions(PermissionGroupDefinition group)
    {
        var permission = group.AddPermission(HealthCarePermissions.WorkLists.Default, L("Permission:WorkLists"));
        permission.AddChild(HealthCarePermissions.WorkLists.Create, L("Permission:Create"));
        permission.AddChild(HealthCarePermissions.WorkLists.Edit, L("Permission:Edit"));
        permission.AddChild(HealthCarePermissions.WorkLists.Delete, L("Permission:Delete"));
    }

    private void SetProtocolsPermissions(PermissionGroupDefinition group)
    {
        var permission = group.AddPermission(
            HealthCarePermissions.Protocols.Default, L("Permission:Protocols")
        );
        permission.AddChild(HealthCarePermissions.Protocols.Create, L("Permission:Create"));
        permission.AddChild(HealthCarePermissions.Protocols.Edit, L("Permission:Edit"));
        permission.AddChild(HealthCarePermissions.Protocols.Delete, L("Permission:Delete"));
    }

    private void SetPatientProtocolsPermissions(PermissionGroupDefinition group)
    {
        var permission = group.AddPermission(
            PatientProtocols.Default, L("Permission:PatientProtocols")
        );
        permission.AddChild(PatientProtocols.Create, L("Permission:Create"));
        permission.AddChild(PatientProtocols.Edit, L("Permission:Edit"));
        permission.AddChild(PatientProtocols.Delete, L("Permission:Delete"));
    }

    private void SetProtocolTypesPermissions(PermissionGroupDefinition group)
    {
        var permission = group.AddPermission(
            HealthCarePermissions.ProtocolTypes.Default, L("Permission:ProtocolTypes")
        );
        permission.AddChild(HealthCarePermissions.ProtocolTypes.Create, L("Permission:Create"));
        permission.AddChild(HealthCarePermissions.ProtocolTypes.Edit, L("Permission:Edit"));
        permission.AddChild(HealthCarePermissions.ProtocolTypes.Delete, L("Permission:Delete"));
    }

    private void SetInsurancesPermissions(PermissionGroupDefinition group)
    {
        var permission = group.AddPermission(
            HealthCarePermissions.Insurances.Default, L("Permission:Insurances")
        );
        permission.AddChild(HealthCarePermissions.Insurances.Create, L("Permission:Create"));
        permission.AddChild(HealthCarePermissions.Insurances.Edit, L("Permission:Edit"));
        permission.AddChild(HealthCarePermissions.Insurances.Delete, L("Permission:Delete"));
    }
}