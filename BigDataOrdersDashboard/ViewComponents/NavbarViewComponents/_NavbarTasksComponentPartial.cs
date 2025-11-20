using Microsoft.AspNetCore.Mvc;

namespace BigDataOrdersDashboard.ViewComponents.NavbarViewComponents
{
    public class _NavbarTasksComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
