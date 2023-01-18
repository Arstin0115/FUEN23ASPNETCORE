using Microsoft.AspNetCore.Mvc;
using prjMvcDemo.Models;

namespace prjMvcCoreDemo.Controllers
{
    public class AController : Controller
    {
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
