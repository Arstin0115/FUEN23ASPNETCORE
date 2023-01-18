using Microsoft.AspNetCore.Mvc;
using prjMvcCoreDemo.Models;
using prjMvcCoreDemo.ViewModels;

namespace prjMvcCoreDemo.Controllers
{
    public class CustomerController : SuperController
    {
        public IActionResult List(CKeywordViewModel vm)
        {
            IEnumerable<TCustomer> datas = null;
            dbDemoContext db = new dbDemoContext();
            if (string.IsNullOrEmpty(vm.txtKeyword))
                datas = from t in db.TCustomers
                        select t;
            else
                datas = db.TCustomers.Where(t => t.FName.Contains(vm.txtKeyword) ||
                t.FPhone.Contains(vm.txtKeyword) ||
                t.FEmail.Contains(vm.txtKeyword) ||
                t.FAddress.Contains(vm.txtKeyword));
            return View(datas);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(TCustomer p)
        {
            dbDemoContext db = new dbDemoContext();
            db.TCustomers.Add(p);
            db.SaveChanges();
            return RedirectToAction("List");
        }
        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                dbDemoContext db = new dbDemoContext();
                TCustomer delCustomer = db.TCustomers.FirstOrDefault(t => t.FId == id);
                if (delCustomer != null)
                {
                    db.TCustomers.Remove(delCustomer);
                    db.SaveChangesAsync();
                }
            }
            return RedirectToAction("List");
        }
        [HttpPost]
        public ActionResult Edit(TCustomer p)
        {
            dbDemoContext db = new dbDemoContext();
            TCustomer x = db.TCustomers.FirstOrDefault(t => t.FId == p.FId);
            if (x != null)
            { 
                x.FName = p.FName;
                x.FPhone = p.FPhone;
                x.FEmail = p.FEmail;
                x.FAddress = p.FAddress;
                x.FPassword = p.FPassword;
                db.SaveChangesAsync();
            }
            return RedirectToAction("List");
        }

        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                dbDemoContext db = new dbDemoContext();
                TCustomer x = db.TCustomers.FirstOrDefault(t => t.FId == id);
                if (x != null)
                    return View(x);
            }
            return RedirectToAction("List");
        }
    }
}
