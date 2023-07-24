using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HireMeNowWebApi.Models;

public partial class Qualification
{
	[Key]
	[Required]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public Guid Id { get; set; }
	[Required]
	[ForeignKey(nameof(User))]
	public Guid? UserId { get; set; }

    public string? Title { get; set; }

    public string? Mark { get; set; }

    public string? YearOfPassout { get; set; }

    public string? University { get; set; }

    public string? Status { get; set; }

    public virtual User? User { get; set; }
}
