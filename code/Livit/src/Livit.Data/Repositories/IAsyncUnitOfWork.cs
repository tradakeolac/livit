namespace Livit.Data.Repositories
{
    using System;
    using System.Threading.Tasks;

    public interface IAsyncUnitOfWork : IDisposable
    {
        /// <summary>
        /// Save change as asynonous
        /// </summary>
        /// <returns></returns>
        Task SaveChangeAsync();
    }
}