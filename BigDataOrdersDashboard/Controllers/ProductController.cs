using BigDataOrdersDashboard.Context;
using BigDataOrdersDashboard.Entities;
using BigDataOrdersDashboard.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BigDataOrdersDashboard.Controllers
{
    public class ProductController : Controller
    {
        private readonly BigDataOrdersDbContext _context;
        private readonly CurrencyService _currencyService;

        public ProductController(BigDataOrdersDbContext context, CurrencyService currencyService)
        {
            _context = context;
            _currencyService = currencyService;
        }

        public async Task<IActionResult> ProductList(int page = 1)
        {
            int pageSize = 12;

            var values = await _context.Products
                                      .Include(p => p.Category)
                                      .OrderBy(p => p.ProductId)
                                      .Skip((page - 1) * pageSize)
                                      .Take(pageSize)
                                      .ToListAsync();

            decimal euroRate = await _currencyService.GetEuroRateAsync();
            ViewBag.EuroRate = euroRate;

            foreach (var item in values)
            {
                item.PriceTL = item.UnitPrice * euroRate;
            }
            var eurData = await _currencyService.GetEuroRateWithTimeAsync();

            ViewBag.EuroRate = eurData.rate;
            ViewBag.EuroTime = eurData.time; 

            int totalCount = await _context.Products.CountAsync();
            ViewBag.TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            ViewBag.CurrentPage = page;

            return View(values);
        }

        [HttpGet]
        public IActionResult CreateProduct()
        {
            var categoryList = _context.Categories
                .Select(x => new SelectListItem
                {
                    Text = x.CategoryName,
                    Value = x.CategoryId.ToString()
                })
                .ToList();

            ViewBag.CategoryList = categoryList;
            return View();
        }

        [HttpPost]
        public IActionResult CreateProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return RedirectToAction("ProductList");
        }

        public IActionResult DeleteProduct(int id)
        {
            var value = _context.Products.Find(id);
            _context.Products.Remove(value);
            _context.SaveChanges();
            return RedirectToAction("ProductList");
        }

        [HttpGet]
        public IActionResult UpdateProduct(int id)
        {
            var categoryList = _context.Categories
                .Select(x => new SelectListItem
                {
                    Text = x.CategoryName,
                    Value = x.CategoryId.ToString()
                })
                .ToList();

            ViewBag.CategoryList = categoryList;

            var value = _context.Products.Find(id);
            return View(value);
        }

        [HttpPost]
        public IActionResult UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
            return RedirectToAction("ProductList");
        }
    }
}
