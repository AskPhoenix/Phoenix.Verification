using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Phoenix.DataHandle.Identity;
using Phoenix.DataHandle.Main.Models;

namespace Phoenix.Verification.Base
{
    public abstract class ContextTestsBase : ConfigurationTestsBase, IDisposable
    {
        protected readonly PhoenixContext _phoenixContext;
        protected readonly ApplicationDbContext _applicationContext;

        public ContextTestsBase()
            : base()
        {
            string phoenixConnection = _configuration.GetConnectionString("PhoenixConnection");

            _phoenixContext = new PhoenixContext(new DbContextOptionsBuilder<PhoenixContext>()
                .UseLazyLoadingProxies()
                .UseSqlServer(phoenixConnection)
                .Options);

            _applicationContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseLazyLoadingProxies()
                .UseSqlServer(phoenixConnection)
                .Options);
        }

        public void Dispose()
        {
            _phoenixContext.Dispose();
            _applicationContext.Dispose();
        }
    }
}
