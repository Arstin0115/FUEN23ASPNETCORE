using Microsoft.AspNetCore.Mvc;
using prjMvcCoreDemo.Models;
using prjMvcCoreDemo.ViewModels;
using System.Text.Json;

namespace prjMvcCoreDemo.Controllers
{
    public class ShoppingController : SuperController
    {
        public IActionResult CartView()
        {
            if (!HttpContext.Session.Keys.Contains(CDictionary.SK_PURCHASED_PRODUCTS_LIST))
                return RedirectToAction("List");


            string json = HttpContext.Session.GetString(CDictionary.SK_PURCHASED_PRODUCTS_LIST);
            List<CShoppingCartItem> cart = JsonSerializer.Deserialize<List<CShoppingCartItem>>(json);
            if(cart == null)
                return RedirectToAction("List");
            return View(cart);

        }
        public IActionResult List()
        {
            dbDemoContext db = new dbDemoContext();
            var datas = from t in db.TProducts
                        select t;
            return View(datas);
        }

        public ActionResult AddToCart(int? id)
        {
            ViewBag.fId = id;
            return View();
        }
        [HttpPost]
        public ActionResult AddToCart(CAddToCartViewModel vm)
        {
            dbDemoContext db = new dbDemoContext();
            TProduct p = db.TProducts.FirstOrDefault(t => t.FId == vm.txtFID);
            if (p == null)
                return RedirectToAction("List");


            List<CShoppingCartItem> cart = null;
            string json = "";
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_PURCHASED_PRODUCTS_LIST))
            {
                json = HttpContext.Session.GetString(CDictionary.SK_PURCHASED_PRODUCTS_LIST);
                cart = JsonSerializer.Deserialize<List<CShoppingCartItem>>(json);
            }
            else
            {
                cart = new List<CShoppingCartItem>();
            }
            CShoppingCartItem item = new CShoppingCartItem();
            item.price = (decimal)p.FPrice;
            item.productId = vm.txtFID;
            item.count = vm.txtCount;
            item.product = p;
            cart.Add(item);
            json = JsonSerializer.Serialize(cart);
            HttpContext.Session.SetString(CDictionary.SK_PURCHASED_PRODUCTS_LIST, json);

            return RedirectToAction("List");
        }
    }
}
