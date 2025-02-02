﻿using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pusula.Training.HealthCare.Appointments;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Content;

namespace Pusula.Training.HealthCare.Controllers.Appointments;

[RemoteService]
[Area("app")]
[ControllerName("Appointment")]
[Route("api/app/appointments")]
public class AppointmentController : HealthCareController, IAppointmentsAppService
{
    protected IAppointmentsAppService _appointmentsAppService;

    public AppointmentController(IAppointmentsAppService appointmentsAppService) =>
        _appointmentsAppService = appointmentsAppService;

    [HttpGet("all")]
    public virtual Task<PagedResultDto<AppointmentDto>> GetListAsync(GetAppointmentsInput input) =>
        _appointmentsAppService.GetListAsync(input);

    [HttpGet("all/with-navigation-properties")]
    public virtual Task<List<AppointmentWithNavigationPropertiesDto>> GetListWithNavigationPropertiesAsync(
        GetAppointmentsInput input
    ) =>
        _appointmentsAppService.GetListWithNavigationPropertiesAsync(input);

    [HttpGet("patient-waiting-appointments/{patientId:guid}")]
    public async Task<List<AppointmentWithNavigationPropertiesDto>>
        GetPatientWaitingAppointmentsAsync(Guid patientId) =>
        await _appointmentsAppService.GetPatientWaitingAppointmentsAsync(patientId);

    [HttpGet("doctor-appointments")]
    public async Task<PagedResultDto<AppointmentWithNavigationPropertiesDto>>
        GetDoctorAppointmentListWithNavigationPropertiesAsync(GetDoctorAppointmentListInput input) =>
        await _appointmentsAppService.GetDoctorAppointmentListWithNavigationPropertiesAsync(input);

    [HttpGet]
    [Route("with-navigation-properties/{id}")]
    public virtual Task<AppointmentWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id) =>
        _appointmentsAppService.GetWithNavigationPropertiesAsync(id);

    [HttpGet]
    [Route("{id}")]
    public virtual Task<AppointmentDto> GetAsync(Guid id) => _appointmentsAppService.GetAsync(id);
    
    [HttpGet]
    [Route("patient-lookup")]
    public virtual Task<PagedResultDto<LookupDto<Guid>>> GetPatientLookupAsync(LookupRequestDto input) =>
        _appointmentsAppService.GetPatientLookupAsync(input);

    [HttpPost]
    public virtual Task<AppointmentDto> CreateAsync(AppointmentCreateDto input) =>
        _appointmentsAppService.CreateAsync(input);

    [HttpPut]
    [Route("{id}")]
    public virtual Task<AppointmentDto> UpdateAsync(Guid id, AppointmentUpdateDto input) =>
        _appointmentsAppService.UpdateAsync(id, input);

    [HttpPut]
    [Route("date/{id}")]
    public virtual Task<AppointmentDto> UpdateDateAsync(Guid id, [FromQuery]DateTime startTime, [FromQuery]DateTime endTime) =>
        _appointmentsAppService.UpdateDateAsync(id,startTime,endTime);

    [HttpDelete]
    [Route("{id}")]
    public virtual Task DeleteAsync(Guid id) => _appointmentsAppService.DeleteAsync(id);

    [HttpGet]
    [Route("as-excel-file")]
    public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(AppointmentExcelDownloadDto input) =>
        _appointmentsAppService.GetListAsExcelFileAsync(input);

    [HttpGet]
    [Route("download-token")]
    public virtual Task<DownloadTokenResultDto> GetDownloadTokenAsync() =>
        _appointmentsAppService.GetDownloadTokenAsync();

    [HttpDelete]
    [Route("")]
    public virtual Task DeleteByIdsAsync(List<Guid> appointmentIds) =>
        _appointmentsAppService.DeleteByIdsAsync(appointmentIds);

    [HttpDelete]
    [Route("all")]
    public virtual Task DeleteAllAsync(GetAppointmentsInput input) => _appointmentsAppService.DeleteAllAsync(input);

    [HttpGet]
    [Route("list-doctor-appointment/{id}")]
    public Task<List<AppointmentDto>> GetListAppointmentsAsync(Guid id) =>
        _appointmentsAppService.GetListAppointmentsAsync(id);
}