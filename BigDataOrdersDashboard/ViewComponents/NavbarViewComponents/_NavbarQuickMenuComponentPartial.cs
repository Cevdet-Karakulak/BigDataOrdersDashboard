using Microsoft.AspNetCore.Mvc;

namespace BigDataOrdersDashboard.ViewComponents.NavbarViewComponents
{
    public class _NavbarQuickMenuComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
