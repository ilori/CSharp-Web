using Microsoft.AspNetCore.Mvc;

namespace Movies.App.Controllers
{

    using System.Threading.Tasks;
    using Movies.Models;
    using Services.Contracts;

    public class DirectorsController : Controller
    {

        private readonly IDirectorService service;

        public DirectorsController(IDirectorService service)
        {
            this.service = service;
        }


        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)
            {
                return this.Redirect("/");
            }

            Director director = await this.service.GetDirectorByIdAsync(id.Value);

            if(director == null)
            {
                return this.Redirect("/");
            }



            return this.View(director);
        }

    }

}