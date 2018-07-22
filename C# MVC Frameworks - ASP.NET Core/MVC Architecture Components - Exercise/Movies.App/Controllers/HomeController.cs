namespace Movies.App.Controllers
{

    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Models.ViewModels;
    using Movies.Models;
    using Services.Contracts;

    public class HomeController : Controller
    {

        private readonly IMovieService movieService;

        private readonly IDirectorService directorService;

        public HomeController(IDirectorService directorService, IMovieService movieService)
        {
            this.movieService = movieService;
            this.directorService = directorService;
        }

        public async Task<IActionResult> Index(string searchTerm)
        {
            //TODO Create ViewModel class and try to optimize this couse its not very good right now

            if (searchTerm != null)
            {
                return this.RedirectToAction("Search", new {searchTerm});
            }

            IEnumerable<Movie> movies = await this.movieService.GetAllMoviesAsync();

            return View(movies);
        }

        [HttpGet]
        public async Task<IActionResult> Search(string searchTerm)
        {
            SearchViewModel model = new SearchViewModel()
            {
                Movies = await this.movieService.GetMoviesBySearchTermAsync(searchTerm),
                Directors = await this.directorService.GetDirectorsBySearchTermAsync(searchTerm),
                SearchTerm = searchTerm
            };

            return this.View(model);
        }

    }

}