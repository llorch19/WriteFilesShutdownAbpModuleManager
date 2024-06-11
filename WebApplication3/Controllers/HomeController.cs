using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
           
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uriCodeBase = new UriBuilder(codeBase);
            string pathCodeBase = Uri.UnescapeDataString(uriCodeBase.Path);
            var dir = Path.GetDirectoryName(pathCodeBase);
            var baseDirectory = new DirectoryInfo(dir);

            var bmp = new Bitmap(600, 800);
            var dirs = Enumerable.Range(50, new Random().Next(100) + 50).Select(x =>
            {
                var str = x.ToString("000");
                var parent = Path.Combine(baseDirectory.FullName,str);
                if (!Directory.Exists(parent))
                {
                    Directory.CreateDirectory(parent);
                }

                return parent;
            });

            foreach (var item in dirs)
            {
                bmp.Save(Path.Combine(item, "test.bmp"));
            }

            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}