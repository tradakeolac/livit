namespace Livit.Data.EntityFramework
{
    using Livit.Model.Entities;
    using Microsoft.EntityFrameworkCore;

    public class LivitDbContext : DbContext
    {
        public DbSet<EmployeeEntity> Employees { get; set; }
        public DbSet<RequestedLeaveEntity> Leaves { get; set; }        
    }
}
