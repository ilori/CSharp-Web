using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace FDMC.App.Controllers
{
    using Models.ViewModels;
    using Services.Contracts;

    public class HomeController : Controller
    {
        private readonly ICatService service;

        public HomeController(ICatService service)
        {
            this.service = service;
        }

        public IActionResult Index()
        {
            IEnumerable<CatIndexViewModel> cats = this.service.GetAll().Select(x => new CatIndexViewModel()
            {
                Id = x.Id,
                Name = x.Name
            });


            return View(cats);
        }
    }
}