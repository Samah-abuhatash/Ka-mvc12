using Azure.Core;
using KaShop1.Data;
using KaShop1.Models;
using Microsoft.AspNetCore.Mvc;

namespace KaShop1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();

        public IActionResult Index()
        {
            var cats = context.catgeries.ToList();
            return View("Index", cats);
        }
        public IActionResult Remove(int id)
        {
            var cat = context.catgeries.Find(id);
            context.catgeries.Remove(cat);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Creat()
        {
            return View(new Catgery());
        }
        public IActionResult Store(Catgery request)
        {
            if (!ModelState.IsValid)
            {
                return View("Creat", request);
            }
            context.catgeries.Add(request);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(int id)
        {
            var cat = context.catgeries.Find(id);
            return View(cat);
        }
        public IActionResult Update(Catgery request)
        {
            if (!ModelState.IsValid)
            {
                return View("edit", request);
               
            }
            var cat = context.catgeries.Find(request.Id);
            cat.Name = request.Name;
          //  context.catgeries.Update(request);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}