namespace Movies.App.Controllers
{

    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Models.BindingModels;
    using Services.Contracts;

    public class BorrowersController : Controller
    {

        private readonly IBorrowerService service;

        public BorrowersController(IBorrowerService service)
        {
            this.service = service;
        }


        [HttpGet]
        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBorrowerBindingModel model)
        {
            if(!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            if(await this.service.ContainsBorrower(model.Name))
            {
                this.ViewData["name"] = "Borrower already exist!";

                return this.View(model);
            }

            await this.service.AddBorrowerAsync(model.Name, model.Address);

            return this.Redirect("/");
        }

    }

}