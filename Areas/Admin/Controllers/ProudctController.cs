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
            if (!ModelState.IsValid)
            {
                return View("Creat", request);
            }

            if (file != null && file.Length > 0)
            {
                // إنشاء اسم فريد للملف
                var fileName = Guid.NewGuid().ToString();
                fileName += Path.GetExtension(file.FileName); // مثال: 1234abcd.png

                // تحديد المسار الكامل داخل مجلد wwwroot/images
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);

                // حفظ الملف فعليًا على السيرفر
                using (var stream = System.IO.File.Create(filePath))
                {
                    file.CopyTo(stream);
                }

                // حفظ اسم الصورة في قاعدة البيانات
                request.Image = fileName;

                // إضافة المنتج إلى قاعدة البيانات
                Context.Proudcts.Add(request);
                Context.SaveChanges();

                // العودة إلى صفحة Index بعد الإضافة
                return RedirectToAction(nameof(Index));
            }
            ViewBag.catgeries = Context.catgeries.ToList();
            return View("Creat", request);

        }
        public IActionResult Remove(int id)
        {
            var pro = Context.Proudcts.Find(id);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", pro.Image);
            System.IO.File.Delete(filePath);
            Context.Proudcts.Remove(pro);
            Context.SaveChanges();
            return RedirectToAction("Index");
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