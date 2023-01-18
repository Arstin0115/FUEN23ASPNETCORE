using Microsoft.AspNetCore.Mvc;
using prjMvcCoreDemo.Models;
using prjMvcDemo.Models;
using System.Text.Json;

namespace prjMvcCoreDemo.Controllers
{
    public class AController : Controller
    {
        public IActionResult showCountBySession()
        {
            int count = 0;
            if (HttpContext.Session.Keys.Contains("COUNT"))
                count = (int)HttpContext.Session.GetInt32("COUNT");
            count++;
            HttpContext.Session.SetInt32("COUNT", count);

            ViewBag.COUNT = count;
            return View();
        }
        public string demoObj2Json()
        {
            TCustomer x = new TCustomer()
            {
                FId = 1,
                FName = "Marco",
                FPhone = "0952447987",
                FEmail = "marco@gmail.com",
                FAddress = "Taipei",
                FPassword = "123"
            };
            string json = JsonSerializer.Serialize(x);
            return json;
        }
        public string demoJson2Obj()
        {
            string json = demoObj2Json();
            TCustomer x = JsonSerializer.Deserialize<TCustomer>(json);
            return x.FName + "<br/>" + x.FPhone;
        }

        public string sayHello()
        {
            return "Hello ASP.NET MVC Core";
        }
        public string lotto()
        {
            CLottoGen x = new CLottoGen();
            return x.getNumbers();
        }
    }
}
