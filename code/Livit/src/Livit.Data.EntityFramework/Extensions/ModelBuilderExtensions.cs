
namespace Livit.Data.EntityFramework.Extensions
{
    using Livit.Infrastructure.Helpers;
    using Microsoft.EntityFrameworkCore;

    public static class ModelBuilderExtensions
    {
        public static void ScanAssemblies(this ModelBuilder modelBuilder)
        {
            AssemblyHelper.LoadCodingAssemblies();
        }
    }

    public abstract class ModelConfiguration
    {
        public abstract void Execute(ModelBuilder builder);
    }
}
