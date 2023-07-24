using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HireMeNowWebApi.Models;

public partial class HireMeNowDbContext : DbContext
{
    public HireMeNowDbContext()
    {
    }

    public HireMeNowDbContext(DbContextOptions<HireMeNowDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Application> Applications { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Experience> Experiences { get; set; }

    public virtual DbSet<Interview> Interviews { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<Qualification> Qualifications { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    public virtual DbSet<User> Users { get; set; }

	//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
	//        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-EV8O311;Initial Catalog=JobPortalDB;Integrated Security=True;Trusted_Connection=True;TrustServerCertificate=true;");

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{

		modelBuilder.Entity<Application>(entity =>
		{
			//entity.HasKey(e => e.Id).HasName("PK__Applicat__3214EC07A0456D4C");

			//entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
			//entity.Property(e => e.AppliedDate).HasColumnType("date");
			//entity.Property(e => e.Status)
			//    .HasMaxLength(50)
			//    .IsUnicode(false);

			entity.HasOne(d => d.Job).WithMany(p => p.Applications)
				.HasForeignKey(d => d.JobId)
				.HasConstraintName("FK__Applicati__JobId__33D4B598");

			entity.HasOne(d => d.User).WithMany(p => p.Applications)
				.HasForeignKey(d => d.UserId)
				.HasConstraintName("FK__Applicati__UserI__32E0915F");
		});

		modelBuilder.Entity<Company>(entity =>
		{
			//entity.HasKey(e => e.Id).HasName("PK__Companie__3214EC075C9ACB91");

			//entity.HasIndex(e => e.Email, "UQ__Companie__A9D10534E9BC3D4E").IsUnique();

			//entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
			//entity.Property(e => e.About)
			//    .HasMaxLength(100)
			//    .IsUnicode(false);
			//entity.Property(e => e.Address)
			//    .HasMaxLength(50)
			//    .IsUnicode(false);
			//entity.Property(e => e.CreatedDate).HasColumnType("date");
			//entity.Property(e => e.Email)
			//    .HasMaxLength(50)
			//    .IsUnicode(false);
			//entity.Property(e => e.Location)
			//    .HasMaxLength(50)
			//    .IsUnicode(false);
			//entity.Property(e => e.Logo)
			//    .HasMaxLength(50)
			//    .IsUnicode(false);
			//entity.Property(e => e.Mission)
			//    .HasMaxLength(100)
			//    .IsUnicode(false);
			//entity.Property(e => e.Name)
			//    .HasMaxLength(100)
			//    .IsUnicode(false);
			//entity.Property(e => e.Phone)
			//    .HasMaxLength(50)
			//    .IsUnicode(false);
			//entity.Property(e => e.Status)
			//    .HasMaxLength(50)
			//    .IsUnicode(false);
			//entity.Property(e => e.Vision)
			//    .HasMaxLength(100)
			//    .IsUnicode(false);
			//entity.Property(e => e.Website)
			//    .HasMaxLength(50)
			//    .IsUnicode(false);
		});

		modelBuilder.Entity<Experience>(entity =>
		{
			//entity.HasKey(e => e.Id).HasName("PK__Experien__3214EC077278D626");

			//entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
			//entity.Property(e => e.Company)
			//    .HasMaxLength(50)
			//    .IsUnicode(false);
			//entity.Property(e => e.Duration)
			//    .HasMaxLength(50)
			//    .IsUnicode(false);
			//entity.Property(e => e.JobTitle)
			//    .HasMaxLength(50)
			//    .IsUnicode(false);
			//entity.Property(e => e.Year)
			//    .HasMaxLength(50)
			//    .IsUnicode(false);

			entity.HasOne(d => d.User).WithMany(p => p.Experiences)
				.HasForeignKey(d => d.UserId);
			//.HasConstraintName("FK__Experienc__UserI__37A5467C");
		});

		modelBuilder.Entity<Interview>(entity =>
		{
			//entity.HasKey(e => e.Id).HasName("PK__Intervie__3214EC070182B074");

			//entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
			//entity.Property(e => e.CreatedDate).HasColumnType("date");
			//entity.Property(e => e.Date).HasColumnType("date");
			//entity.Property(e => e.Location)
			//    .HasMaxLength(50)
			//    .IsUnicode(false);
			//entity.Property(e => e.Status)
			//    .HasMaxLength(50)
			//    .IsUnicode(false);

			entity.HasOne(d => d.CreatedUser).WithMany(p => p.InterviewCreatedByNavigations)
				.HasForeignKey(d => d.CreatedBy)
				.HasConstraintName("FK__Interview__Creat__3D5E1FD2");

			entity.HasOne(d => d.Job).WithMany(p => p.Interviews)
				.HasForeignKey(d => d.JobId)
				.HasConstraintName("FK__Interview__JobId__3B75D760");

			entity.HasOne(d => d.Jobseeker).WithMany(p => p.InterviewJobseekers)
				.HasForeignKey(d => d.JobseekerId)
				.HasConstraintName("FK__Interview__Jobse__3C69FB99");
		});

		modelBuilder.Entity<Job>(entity =>
		{
			//entity.HasKey(e => e.Id).HasName("PK__Jobs__3214EC07024D6053");

			//entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
			//entity.Property(e => e.CreatedDate).HasColumnType("date");
			//entity.Property(e => e.Description)
			//    .HasMaxLength(50)
			//    .IsUnicode(false);
			//entity.Property(e => e.Experience)
			//    .HasMaxLength(50)
			//    .IsUnicode(false);
			//entity.Property(e => e.JobType)
			//    .HasMaxLength(50)
			//    .IsUnicode(false);
			//entity.Property(e => e.Location)
			//    .HasMaxLength(50)
			//    .IsUnicode(false);
			//entity.Property(e => e.Responsibilities)
			//    .HasMaxLength(50)
			//    .IsUnicode(false);
			//entity.Property(e => e.Salary)
			//    .HasMaxLength(50)
			//    .IsUnicode(false);
			//entity.Property(e => e.Status)
			//    .HasMaxLength(50)
			//    .IsUnicode(false);
			//entity.Property(e => e.Title)
			//    .HasMaxLength(50)
			//    .IsUnicode(false);
			//entity.Property(e => e.TypeOfWorkPlace)
			//    .HasMaxLength(50)
			//    .IsUnicode(false);

			entity.HasOne(d => d.Company).WithMany(p => p.Jobs)
				.HasForeignKey(d => d.CompanyId)
				.HasConstraintName("FK__Jobs__CompanyId__2E1BDC42");

			entity.HasOne(d => d.CreatedUser).WithMany(p => p.Jobs)
				.HasForeignKey(d => d.CreatedBy)
				.HasConstraintName("FK__Jobs__CreatedBy__2F10007B");
		});

		modelBuilder.Entity<Qualification>(entity =>
		{
			//entity.HasKey(e => e.Id).HasName("PK__Qualific__3214EC07ABC132FC");

			//entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
			//entity.Property(e => e.Mark)
			//    .HasMaxLength(50)
			//    .IsUnicode(false);
			//entity.Property(e => e.Status)
			//    .HasMaxLength(50)
			//    .IsUnicode(false);
			//entity.Property(e => e.Title)
			//    .HasMaxLength(50)
			//    .IsUnicode(false);
			//entity.Property(e => e.University)
			//    .HasMaxLength(50)
			//    .IsUnicode(false);
			//entity.Property(e => e.YearOfPassout)
			//    .HasMaxLength(50)
			//    .IsUnicode(false);

			entity.HasOne(d => d.User).WithMany(p => p.Qualifications)
				.HasForeignKey(d => d.UserId)
				.HasConstraintName("FK__Qualifica__UserI__412EB0B6");
		});

		modelBuilder.Entity<Skill>(entity =>
		{
			//entity.HasKey(e => e.Id).HasName("PK__Skills__3214EC0722774215");

			//entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
			//entity.Property(e => e.Status)
			//    .HasMaxLength(50)
			//    .IsUnicode(false);
			//entity.Property(e => e.Title)
			//    .HasMaxLength(50)
			//    .IsUnicode(false);

			entity.HasOne(d => d.User).WithMany(p => p.Skills)
				.HasForeignKey(d => d.UserId)
				.HasConstraintName("FK__Skills__UserId__44FF419A");
		});

		modelBuilder.Entity<User>(entity =>
		{
			//entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07DCEC237A");

			//entity.HasIndex(e => e.Email, "UQ__Users__A9D105340BB73107").IsUnique();

			//entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
			//entity.Property(e => e.About)
			//    .HasMaxLength(50)
			//    .IsUnicode(false);
			//entity.Property(e => e.CreatedDate).HasColumnType("date");
			//entity.Property(e => e.Designation)
			//    .HasMaxLength(50)
			//    .IsUnicode(false);
			//entity.Property(e => e.Email)
			//    .HasMaxLength(50)
			//    .IsUnicode(false);
			//entity.Property(e => e.FirstName)
			//    .HasMaxLength(50)
			//    .IsUnicode(false);
			//entity.Property(e => e.Gender)
			//    .HasMaxLength(50)
			//    .IsUnicode(false);
			//entity.Property(e => e.Image)
			//    .HasMaxLength(50)
			//    .IsUnicode(false);
			//entity.Property(e => e.LastName)
			//    .HasMaxLength(50)
			//    .IsUnicode(false);
			//entity.Property(e => e.Location)
			//    .HasMaxLength(50)
			//    .IsUnicode(false);
			//entity.Property(e => e.Password)
			//    .HasMaxLength(50)
			//    .IsUnicode(false);
			//entity.Property(e => e.Phone)
			//    .HasMaxLength(50)
			//    .IsUnicode(false);
			//entity.Property(e => e.Role)
			//    .HasMaxLength(50)
			//    .IsUnicode(false);
			//entity.Property(e => e.Status)
			//    .HasMaxLength(50)
			//    .IsUnicode(false);

			entity.HasOne(d => d.Company).WithMany(p => p.Users)
				.HasForeignKey(d => d.CompanyId)
				.HasConstraintName("FK__Users__CreatedDa__2A4B4B5E");
		});

		OnModelCreatingPartial(modelBuilder);
	}

	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

	public override int SaveChanges()
	{
		var entries = ChangeTracker.Entries()
			.Where(e => e.State==EntityState.Added||
			e.State==EntityState.Modified);
		foreach(var entry in entries)
		{
			if (entry.Entity.GetType() == typeof(Job) ||
                entry.Entity.GetType() == typeof(Application) ||
                entry.Entity.GetType() == typeof(Interview))
			{
				entry.Property("UpdatedDate").CurrentValue=DateTime.Now;
				if (entry.State==EntityState.Added)
				{
					entry.Property("CreatedDate").CurrentValue= DateTime.Now;
				}
			}
		}
		return base.SaveChanges();
	}
}
