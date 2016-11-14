using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X_Market.ViewModels;

namespace X_Market.Models
{
    public class OrdersController : Controller
    {
        X_MarketContext db = new X_MarketContext();

        // GET: New Order
        public ActionResult NewOrder()
        {
            var orderView = new OrderView();
            orderView.Customer = new Customer();
            orderView.Products = new List<ProductOrder>();

            var listC = GetCustomers();
            ViewBag.CustomerID = new SelectList(listC, "CustomerID", "FullName");

            Session["orderView"] = orderView;

            return View(orderView);
        }

        [HttpPost]
        public ActionResult NewOrder(OrderView orderView)
        {
            orderView = Session["orderView"] as OrderView;

            var customerID = int.Parse(Request["CustomerID"]);
            if (customerID == 0)
            {
                var listSC = GetCustomers();
                ViewBag.CustomerID = new SelectList(listSC, "CustomerID", "FullName");
                ViewBag.Error = "You must select a Customer.";
                return View(orderView);
            }

            var customer = db.Customers.Find(customerID);
            if(customer == null)
            {
                var listCNE = GetCustomers();
                ViewBag.CustomerID = new SelectList(listCNE, "CustomerID", "FullName");
                ViewBag.Error = "Customer not exists.";
                return View(orderView);
            }

            if(orderView.Products.Count == 0)
            {
                var listCPNE = GetCustomers();
                ViewBag.CustomerID = new SelectList(listCPNE, "CustomerID", "FullName");
                ViewBag.Error = "You must enter details.";
                return View(orderView);
            }

            int orderID = 0;
            
            using(var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var order = new Order
                    {
                        CustomerID = customerID,
                        DateOrder = DateTime.Now,
                        OrderStatus = OrderStatus.Created
                    };
                    db.Orders.Add(order);
                    db.SaveChanges();

                    orderID = db.Orders.ToList().Select(o => o.OrderID).Max();
                    foreach (var item in orderView.Products)
                    {
                        var orderDetail = new OrderDetail
                        {
                            OrderID = orderID,
                            ProductID = item.ProductID,
                            Description = item.Description,
                            Price = item.Price,
                            Quantity = item.Quantity
                        };
                        db.OrderDetails.Add(orderDetail);
                        var product = db.Products.Find(item.ProductID);
                        product.Stock -= item.Quantity;
                        db.SaveChanges();
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            
            ViewBag.Message = string.Format("The order {0} save OK", orderID);

            var listC = GetCustomers();
            ViewBag.CustomerID = new SelectList(listC, "CustomerID", "FullName");
            orderView = new OrderView();
            orderView.Customer = new Customer();
            orderView.Products = new List<ProductOrder>();
            Session["orderView"] = orderView;
            return View(orderView);
        }


        // GET: AddProduc
        public ActionResult AddProduct()
        {
            var listP = GetProducts();
            ViewBag.ProductID = new SelectList(listP, "ProductID", "Description");

            return View();
        }

        // POST: AddProduct
        [HttpPost]
        public ActionResult AddProduct(ProductOrder productOrder)
        {
            var orderView = Session["orderView"] as OrderView;

            var productID = int.Parse(Request["ProductID"]);
            if (productID == 0)
            {
                var listP = GetProducts();
                ViewBag.ProductID = new SelectList(listP, "ProductID", "Description");
                ViewBag.Error = "You must select a Product.";

                return View(productOrder);
            }

            var product = db.Products.Find(productID);
            if (product == null)
            {
                var listP = GetProducts();
                ViewBag.ProductID = new SelectList(listP, "ProductID", "Description");
                ViewBag.Error = "Product not exists.";

                return View(productOrder);
            }

            var quantity = Request["Quantity"];
            if(quantity == "")
            {
                var listP = GetProducts();
                ViewBag.ProductID = new SelectList(listP, "ProductID", "Description");

                return View(productOrder);
            }

            productOrder = orderView.Products.Find(p => p.ProductID == productID);
            if(productOrder == null)
            {
                if (float.Parse(Request["Quantity"]) > product.Stock)
                {
                    var listP = GetProducts();
                    ViewBag.ProductID = new SelectList(listP, "ProductID", "Description");
                    ViewBag.Error = string.Format("You can not enter an amount greater than {0}", product.Stock);
                    return View(productOrder);
                }

                productOrder = new ProductOrder
                {
                    Description = product.Description,
                    Price = product.Price,
                    ProductID = product.ProductID,
                    Quantity = float.Parse(Request["Quantity"])
                };
                orderView.Products.Add(productOrder);
            }else
            {
                if ((float.Parse(Request["Quantity"]) + productOrder.Quantity) > product.Stock)
                {
                    var listP = GetProducts();
                    ViewBag.ProductID = new SelectList(listP, "ProductID", "Description");
                    ViewBag.Error = string.Format("You can not enter an amount greater than {0}", product.Stock);
                    return View(productOrder);
                }

                productOrder.Quantity += float.Parse(Request["Quantity"]);
            }

            if(productOrder.Quantity > product.Stock)
            {
                var listP = GetProducts();
                ViewBag.ProductID = new SelectList(listP, "ProductID", "Description");
                ViewBag.Error = string.Format("You can not enter more catidad to {0}", product.Stock);
                return View(productOrder);
            }

            var listC = GetCustomers();
            ViewBag.CustomerID = new SelectList(listC, "CustomerID", "FullName");

            return View("NewOrder", orderView);

        }

        // Obtengo lista de Document Types
        public List<Customer> GetCustomers()
        {
            var listC = db.Customers.ToList();
            listC.Add(new Customer { CustomerID = 0, Name = "[Select a Customer]" });
            listC = listC.OrderBy(c => c.LastName).ToList();

            return listC;
        }

        // Obtengo lista de Products
        public List<Product> GetProducts()
        {
            var listP = db.Products.ToList();
            listP.Add(new Product { ProductID = 0, Description = "[Select a Product]" });
            listP = listP.OrderBy(c => c.Description).ToList();

            return listP;
        }
    }
}