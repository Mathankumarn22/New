using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineMobileShop.Entity;
using System.Data.SqlClient;

namespace OnlineMobileShop.Respository
{
    public class BrandRepository
    {
        public void AddBrand(Entity.Brand brand)
        {
            using (DBContext context = new DBContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {


                        SqlParameter BrandName = new SqlParameter("@BrandName", brand.BrandName);
                        var result = context.Database.ExecuteSqlCommand("Brand_Insert @BrandName ", BrandName);
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                    }

                }

            }
        }
        public IEnumerable<Brand> ViewBrand()
        {
            using (DBContext dBContext = new DBContext())
            {
                return dBContext.brand.ToList();
            }
        }
        public IEnumerable<Mobile> GetProductCategory(int productNumber)
        {
            using (DBContext dbConnect = new DBContext())
            {
                List<Mobile> result = dbConnect.mobile.
                              Where(x => x.BrandID == productNumber).ToList();
                return result;
            }
        }
    }
}
