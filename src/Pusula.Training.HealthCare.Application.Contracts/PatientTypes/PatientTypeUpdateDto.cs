using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.PatientTypes;

public class PatientTypeUpdateDto : IHasConcurrencyStamp
{
    [Required]
    [StringLength(PatientTypeContst.NameMaxLength)]
    public string Name { get; set; } = null!;

    public string ConcurrencyStamp { get; set; } = null!;
}