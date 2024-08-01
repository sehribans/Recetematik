using Microsoft.AspNetCore.Mvc;



namespace Recetematik.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        //private readonly StokTakipContext _context;

        //public HeaderViewComponent(StokTakipContext context)
        //{
        //    _context = context;
        //}

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
