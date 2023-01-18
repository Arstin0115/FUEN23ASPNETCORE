using Microsoft.AspNetCore.Mvc;
using prjMvcCoreDemo.Models;
using prjMvcCoreDemo.ViewModels;

namespace prjMvcCoreDemo.Controllers
{
    public class ProductController : SuperController
    {
        IWebHostEnvironment _eviroment;

        public ProductController(IWebHostEnvironment p)
        {
            _eviroment = p;
        }
        public ActionResult List(CKeywordViewModel vm)
        {
            
            IEnumerable<TProduct> datas = null;
            
            dbDemoContext db = new dbDemoContext();
            if (string.IsNullOrEmpty(vm.txtKeyword))
                datas = from t in db.TProducts select t;
            else
                datas = db.TProducts.Where(t => t.FName.Contains(vm.txtKeyword));

            return View(datas);
        }

        [HttpPost]
        public ActionResult Edit(CProductViewModel p)
        {
            dbDemoContext db = new dbDemoContext();
            TProduct x = db.TProducts.FirstOrDefault(t => t.FId == p.FId);
            if (x != null)
            {
                if(p.photo != null)
                {
                    string photoName = Guid.NewGuid().ToString() + ".jpg";
                    string path = _eviroment.WebRootPath + "/images/" + photoName;
                    x.FImagePath = photoName;
                    p.photo.CopyTo(new FileStream(path, FileMode.Create));
                }
                x.FName = p.FName;
                x.FCost = p.FCost;
                x.FPrice = p.FPrice;
                x.FQty = p.FQty;
                db.SaveChangesAsync();
            }
            return RedirectToAction("List");
        }

        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                dbDemoContext db = new dbDemoContext();
                TProduct x = db.TProducts.FirstOrDefault(t => t.FId == id);
                if (x != null)
                    return View(x);
            }
            return RedirectToAction("List");

        }

        public ActionResult Delete(int? id)
        {
            dbDemoContext db = new dbDemoContext();
            TProduct delProduct = db.TProducts.FirstOrDefault(t => t.FId == id);
            if (delProduct != null)
            {
                db.TProducts.Remove(delProduct);
                db.SaveChangesAsync();
            }
            return RedirectToAction("List");
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(TProduct p)
        {
            dbDemoContext db = new dbDemoContext();
            db.TProducts.Add(p);
            db.SaveChanges();
            return RedirectToAction("List");


        }
    }
}
