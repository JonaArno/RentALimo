using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace RentALimo.EF
{
    public class RentALimoConfig : DbConfiguration
    {
        public RentALimoConfig()
        {
            SetProviderServices(SqlProviderServices.ProviderInvariantName,
                SqlProviderServices.Instance);
        }
    }
}
