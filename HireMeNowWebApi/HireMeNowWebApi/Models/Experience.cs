using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HireMeNowWebApi.Models;

public partial class Experience
{
	[Key]
	[Required]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public Guid Id { get; set; }
	[ForeignKey(nameof(User))]
	public Guid? UserId { get; set; }

    public string? JobTitle { get; set; }

    public string? Company { get; set; }

    public string? Duration { get; set; }

    public string? Year { get; set; }

    public virtual User? User { get; set; }
}
