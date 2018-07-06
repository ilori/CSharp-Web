namespace FDMC.App.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models.ViewModels;
    using Services.Contracts;
    using FDMC.Models;
    using Models.BindingModels;

    public class CatsController : Controller
    {
        private readonly ICatService service;

        public CatsController(ICatService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            Cat cat = this.service.GetCatById(id);
            if (cat == null)
            {
                return this.Redirect("/");
            }

            CatDetailsViewModel catModel = new CatDetailsViewModel()
            {
                Name = cat.Name,
                Breed = cat.Breed,
                Age = cat.Age,
                Image = cat.ImageUrl
            };

            return this.View(catModel);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Add(CatBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            this.service.CreateCat(model.Name, model.Age, model.Breed, model.ImageUrl);

            return this.Redirect("/");
        }
    }
}