namespace Livit.Data.EntityFramework
{
    using Livit.Model.Entities;
    using Microsoft.EntityFrameworkCore;

    public class LivitDbContext : DbContext
    {
        public DbSet<EmployeeEntity> Employees { get; set; }
        public DbSet<RequestedLeaveEntity> Leaves { get; set; }
        public DbSet<TokenResponseEntity> Tokens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("FileName=./livit.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeEntity>()
                .HasKey(m => m.Id);

            modelBuilder.Entity<EmployeeEntity>()
                .Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(300);

            modelBuilder.Entity<EmployeeEntity>()
                .Property(m => m.Email)
                .IsRequired()
                .HasMaxLength(300);

            modelBuilder.Entity<EmployeeEntity>()
                .HasMany(m => m.Leaves)
                .WithOne(l => l.Employee)
                .HasForeignKey(ma => ma.EmployeeId);

            // Requested entity
            modelBuilder.Entity<RequestedLeaveEntity>()
                .HasKey(m => m.Id);

            modelBuilder.Entity<RequestedLeaveEntity>()
                .Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(300);

            modelBuilder.Entity<RequestedLeaveEntity>()
                .Property(m => m.Description)
                .IsRequired()
                .HasMaxLength(1000);

            modelBuilder.Entity<RequestedLeaveEntity>()
               .Property(m => m.CreatedDate)
               .IsRequired();

            modelBuilder.Entity<RequestedLeaveEntity>()
                .Property(m => m.EmployeeId)
                .IsRequired();

            // Token
            modelBuilder.Entity<TokenResponseEntity>()
                .HasKey(m => m.Id);

            modelBuilder.Entity<TokenResponseEntity>()
                .Property(m => m.AccessToken)
                .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
