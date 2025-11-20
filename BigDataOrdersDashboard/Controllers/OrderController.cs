using BigDataOrdersDashboard.Context;
using BigDataOrdersDashboard.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BigDataOrdersDashboard.Controllers
{
    public class OrderController : Controller
    {
        private readonly BigDataOrdersDbContext _context;
        public OrderController(BigDataOrdersDbContext context)
        {
            _context = context;
        }
        public IActionResult OrderList(int page = 1)
        {
            int pageSize = 12; // her sayfada 12 kayıt
            var values = _context.Orders
                                 .OrderBy(p => p.OrderId)
                                 .Skip((page - 1) * pageSize)
                                 .Take(pageSize)
                                 .Include(x => x.Product)
                                 .Include(y => y.Customer)
                                 .ToList();

            int totalCount = _context.Orders.Count();
            ViewBag.TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            ViewBag.CurrentPage = page;

            return View(values);
        }

        [HttpGet]
        public IActionResult CreateOrder()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateOrder(Order order)
        {
            order.OrderDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            _context.Orders.Add(order);
            _context.SaveChanges();
            return RedirectToAction("OrderList");
        }

        public IActionResult DeleteOrder(int id)
        {
            var value = _context.Orders.Find(id);
            _context.Orders.Remove(value);
            _context.SaveChanges();
            return RedirectToAction("OrderList");
        }

        [HttpGet]
        public IActionResult UpdateOrder(int id)
        {
            var value = _context.Orders.Find(id);
            return View(value);
        }

        [HttpPost]
        public IActionResult UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
            _context.SaveChanges();
            return RedirectToAction("OrderList");
        }
    }
}
