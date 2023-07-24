using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HireMeNowWebApi.Models;

public partial class Interview
{
	[Key]
	[Required]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public Guid Id { get; set; }
	[ForeignKey(nameof(Job))]
	public Guid? JobId { get; set; }
	[ForeignKey(nameof(Jobseeker))]
	public Guid? JobseekerId { get; set; }

	[ForeignKey(nameof(Company))]
	public Guid? CompanyId { get; set; }
	public DateTime? Date { get; set; }

    public TimeSpan? Time { get; set; }

    public string? Location { get; set; }

    public string? Status { get; set; }
	[ForeignKey(nameof(CreatedUser))]
	public Guid? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }

    public virtual User? CreatedUser { get; set; }

    public virtual Job? Job { get; set; }
	public virtual Company? Company { get; set; }

	public virtual User? Jobseeker { get; set; }
}
