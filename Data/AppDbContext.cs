using Microsoft.EntityFrameworkCore;

public sealed class AppDbContext : DbContext
{
    public DbSet<Students> Students { get; init; }
    public DbSet<Subjects> Subjects { get; init; }
    public DbSet<Faculty> Faculty { get; init; }
    public DbSet<Courses> Courses { get; init; }
    public DbSet<Enrollments> Enrollments { get; init;}
    public DbSet<Attendence> Attendence { get; init;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=IRSH\\SQLEXPRESS;Database=SIMS;Trusted_Connection=True;TrustServerCertificate=True"
        );
        base.OnConfiguring(optionsBuilder);
    }
}