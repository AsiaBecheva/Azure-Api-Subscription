
namespace ASE.Server
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ASE.Data;

    public static class DbInitialize 
    {
        public static void Initialize(AzureApiSubDbContext context)
        {
            try
            {
                context.Database.EnsureCreated();
 
            }
            catch (Exception ex)
            {
                throw new Exception("{1}", ex.InnerException);
            }
        }
    }
}
