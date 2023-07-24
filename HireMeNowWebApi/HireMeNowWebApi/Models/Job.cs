using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HireMeNowWebApi.Models;

public partial class Job
{
	[Key]
	[Required]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public Guid Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Location { get; set; }

    public string? Experience { get; set; }

    public string? TypeOfWorkPlace { get; set; }

    public string? Salary { get; set; }

    public string? Responsibilities { get; set; }

    public string? JobType { get; set; }

    public int? VacanciesCount { get; set; }

    public int? AppliedCount { get; set; }

    public string? Status { get; set; }
	[ForeignKey(nameof(Company))]
	public Guid? CompanyId { get; set; }

    public DateTime? CreatedDate { get; set; }
	public DateTime? UpdatedDate { get; set; }
	[ForeignKey(nameof(CreatedUser))]
	public Guid? CreatedBy { get; set; }

    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();

    public virtual Company? Company { get; set; }

    public virtual User? CreatedUser { get; set; }

    public virtual ICollection<Interview> Interviews { get; set; } = new List<Interview>();
}
