using Microsoft.AspNetCore.Mvc;
using Recetematik.Models;
using Recetematik.Models.Vm;

namespace Recetematik.Controllers
{
    public class SatisController : Controller
    {
        private readonly RmContext _c;

        public SatisController(RmContext c)
        {
            _c = c;
        }

        public IActionResult Index()
        {
            var vm = new SatisVm();
            vm.SatisListe= _c.TblSatis.ToList();
            vm.CariListe = _c.TblCaris.ToList();
            vm.UrunListe = _c.TblUruns.ToList();

            return View(vm);
        }
        public IActionResult SatisForm()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SatisForm(SatisVm model) {
            if (model.Satis.Id > 0)
            {
                _c.TblSatis.Update(model.Satis);

            }
            else
            {
                var urunBilgi= _c.TblUrunbilgis.Where(x=> model.Satis.UrunId==x.UrunId).ToList();
                var hammadde = _c.TblHammaddes.Where(x => urunBilgi.Select(m=> m.HammaddeId).Contains(x.Id)).ToList();
                var toplam = 0;
                foreach (var item in hammadde)
                {
                
                    var urunadet= urunBilgi.FirstOrDefault(x=>x.HammaddeId== item.Id);
                  var eksilenmadde= urunadet.Miktar*model.Satis.Miktar;
                    var m= _c.TblHammaddes.FirstOrDefault(x=> x.Id== item.Id);
                    m.Adet = m.Adet - eksilenmadde;
                    _c.TblHammaddes.Update(m);
                    _c.SaveChanges();
                }   
                foreach(var item in urunBilgi)
                {
                    var fiyat = hammadde.FirstOrDefault(x => x.Id == item.HammaddeId);
                    var maddemaliyet = item.Miktar *((int?)fiyat.Fiyat);
                    toplam = (toplam + maddemaliyet) ?? 0;
                }
                var urun= _c.TblUruns.FirstOrDefault(x=> x.Id == model.Satis.UrunId);
                model.Satis.Fiyat = urun.Fiyat * model.Satis.Miktar;
                var maliyet = toplam;
                model.Satis.Maliyet= maliyet*model.Satis.Miktar;
                model.Satis.Tarih = DateTime.Now; 
                _c.TblSatis.Add(model.Satis);
            }
            _c.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult SatisSil(int id)
        {
            _c.TblSatis.Remove(_c.TblSatis.Find(id));
            _c.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
