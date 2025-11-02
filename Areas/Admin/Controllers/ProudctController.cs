using KaShop1.Data;
using KaShop1.Models;
using KaShop1.viewMoadels;
using Microsoft.AspNetCore.Mvc;

namespace KaShop1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProudctController : Controller
    {
        ApplicationDbContext Context = new ApplicationDbContext();
        public IActionResult Index() {
            var products = Context.Proudcts.Join(Context.catgeries, p => p.categoryId, c => c.Id, (p, c) => new {
                p.Name, p.Id,p.Description,p.Price, p.Image,CategoryName = c.Name

            });

            var productsVm = new List<ProudctsviewMoadels>();

           
            foreach (var item in products)
            {
                var vm = new ProudctsviewMoadels
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Price = item.Price,
                    imgurl = $"{Request.Scheme}://{Request.Host}/images/{item.Image}",
                    categoryName = item.CategoryName
                };

                productsVm.Add(vm);
            }

            // تمرير البيانات إلى الـ View
            return View(productsVm);
        }

        public IActionResult Creat()
        {
            ViewBag.catgeries = Context.catgeries.ToList();
            return View(new Proudct());
        }
        public IActionResult Store(Proudct request, IFormFile file)
        {
            ViewBag.catgeries = Context.catgeries.ToList();

            ModelState.Remove("File");
            if (!ModelState.IsValid)
            {
                return View("Creat", request);
            }

            if (file == null || file.Length == 0)
            {
                ModelState.AddModelError("Image", "please upload an image");
                return View("Creat", request);

            }
            var allowedExtensions = new[] { ".jpg", ".webp" };
            var extension = Path.GetExtension(file.FileName).ToLower();
            if (!allowedExtensions.Contains(extension))
            {
                ModelState.AddModelError("Image", "only jpg or webp are allowed");
                return View("Creat", request);
            }

            if (file.Length > 2 * 1024 * 1024)
            {
                ModelState.AddModelError("Image", "image size must be less than 2MB");
                return View("Creat", request);


            }
            var fileName = Guid.NewGuid().ToString();
            fileName += Path.GetExtension(file.FileName);
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images", fileName);
            using (var stream = System.IO.File.Create(filepath))
            {
                file.CopyTo(stream);
            }
            request.Image = fileName;
            Context.Proudcts.Add(request);
            Context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(int id)
        {
            var product = Context.Proudcts.Find(id);
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images", product.Image);
            System.IO.File.Delete(filepath);
            Context.Proudcts.Remove(product);
            Context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

      
        public IActionResult Edit(int id)

        {
            var pro = Context.Proudcts.Find(id);
            ViewBag.catgeries = Context.catgeries.ToList();
            return View(pro);
        }
        public IActionResult Update(Proudct request, IFormFile? file)
        {
            var product = Context.Proudcts.Find(request.Id);

            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;
            product.Quantity = request.Quantity;
            product.categoryId = request.categoryId;

            if (file != null && file.Length > 0)
            {
                var oldfilePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images", product.Image);
                System.IO.File.Delete(oldfilePath);

                var fileName = Guid.NewGuid().ToString(); // rewrw432wrwrw
                fileName += Path.GetExtension(file.FileName); // rewrw432wrwrw.png
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images", fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    file.CopyTo(stream);
                }

                product.Image = fileName;
            }

            Context.SaveChanges();

            return RedirectToAction("Index");
        }



    }
}