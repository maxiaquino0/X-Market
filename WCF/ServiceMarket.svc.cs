using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class ServiceMarket : IServiceMarket
    {
        public Products GetProduct(int productID)
        {
            var db = new MarketEntities();
            var product = db.Products.Find(productID);
            db.Dispose();
            return product;
        }

        public List<Products> GetProducts()
        {
            var db = new MarketEntities();
            var products = db.Products.ToList();
            db.Dispose();
            return products;
        }
    }
}
