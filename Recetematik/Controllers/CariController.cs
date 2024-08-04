using Microsoft.AspNetCore.Mvc;
using Recetematik.Models;

namespace Recetematik.Controllers
{
    public class CariController : Controller
    {
        private readonly RmContext _c;

        public CariController(RmContext c)
        {
            _c = c;
        }

        public IActionResult Index()
        {
            return View(_c.TblCaris.ToList());
        }
        public IActionResult CariForm(int id)
        {
            var cari= new TblCari();
            if(id>0)
            {
                cari= _c.TblCaris.Find(id);
            }

            return View(cari);
        }
        [HttpPost]
        public IActionResult CariForm(TblCari model) {
            if (model.Id > 0)
            {
                _c.TblCaris.Update(model);
            }
            else
            {
                model.Bakiye = 0;
                model.OlusturmaTarihi=DateTime.Now;
                _c.TblCaris.Add(model);
            }
            _c.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult CariSil(int id)
        {
            _c.TblCaris.Remove(_c.TblCaris.Find(id));
            _c.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
