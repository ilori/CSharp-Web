namespace Movies.App.Controllers
{

    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using Google.Apis.Services;
    using Google.Apis.YouTube.v3;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.BindingModels;
    using Models.ViewModels;
    using Movies.Models;
    using Services.Contracts;

    public class MoviesController : Controller
    {

        private readonly IMovieService movieService;

        private readonly IBorrowerService borrowerService;

        private readonly IBorrowService borrowService;

        private readonly IHistoryService historyService;

        public MoviesController(IMovieService movieService, IBorrowerService borrowerService,
            IBorrowService borrowService, IHistoryService historyService)
        {
            this.movieService = movieService;
            this.borrowerService = borrowerService;
            this.borrowService = borrowService;
            this.historyService = historyService;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddMovieBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.movieService.AddMovieAsync(model.Title, model.Director, model.Description, model.CoverImageUrl);

            return this.Redirect("/");
        }

        //TODO Using YouTube API if you want to watch the trailers put ur API key into GetVideoId Method

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.Redirect("/");
            }

            Movie movie = await this.movieService.GetMovieByIdAsync(id.Value);

            if (movie == null)
            {
                return this.Redirect("/");
            }

            //TODO If u want the movie trailers you need to enter your API key in the GetVideoId method
            //TODO otherwise everything will work fine except that you wont be able to see the trailer video
            string youTubeId = default(string);

            try
            {
                youTubeId = this.GetVideoId(movie.Title);
            }
            catch (Exception)
            {
                // ignored
            }

            MovieDetailsViewModel model = new MovieDetailsViewModel
            {
                Description = movie.Description,
                Title = movie.Title,
                MovieId = movie.Id,
                Director = movie.Director.Name,
                DirectorId = movie.Director.Id,
                Status = movie.Status,
                YouTubeVideo = youTubeId,
                CoverImage = movie.CoverImageUrl
            };

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Rent(int? id)
        {
            if (id == null)
            {
                return this.Redirect("/");
            }

            Movie movie = await this.movieService.GetMovieByIdAsync(id.Value);

            if (movie == null || !movie.Status)
            {
                return this.Redirect("/");
            }

            IEnumerable<Borrower> borrowers = await this.borrowerService.GetAllBorrowersAsync();
            MovieBorrowViewModel model = new MovieBorrowViewModel {CoverImageUrl = movie.CoverImageUrl, Id = movie.Id};

            model.Borrowers.AddRange(borrowers.Select(x =>
                new SelectListItem {Text = x.Name, Value = x.Id.ToString()}));

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Rent(MovieBorrowViewModel model)
        {
            Movie movie = await this.movieService.GetMovieByIdAsync(model.Id);
            IEnumerable<Borrower> borrowers = await this.borrowerService.GetAllBorrowersAsync();
            model.CoverImageUrl = movie.CoverImageUrl;

            model.Borrowers.AddRange(borrowers.Select(x =>
                new SelectListItem {Text = x.Name, Value = x.Id.ToString()}));

            DateTime borrowDate;
            DateTime? returnDate = null;

            if (model.RentDate == null)
            {
                borrowDate = DateTime.Today;
            }
            else
            {
                if (!DateTime.TryParseExact(model.RentDate, "dd/MM/yyyy", CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out borrowDate))
                {
                    this.ViewData["borrowDate"] = "Invalid Rent Date please try again.";

                    return this.View(model);
                }
            }

            if (model.ReturnDate != null)
            {
                if (!DateTime.TryParseExact(model.ReturnDate, "dd/MM/yyyy", CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out DateTime test))
                {
                    this.ViewData["returnDate"] = "Invalid Return Date please try again.";

                    return this.View(model);
                }

                returnDate = test;
            }

            if (borrowDate > returnDate)
            {
                this.ViewData["date"] = "Return Date must be after Rent Date please try again.";

                return this.View(model);
            }

            if (!model.Borrowers.Any())
            {
                this.ViewData["borrower"] = "Please add a Borrower before u can rent a movie";

                return this.View(model);
            }

            await this.borrowService.BorrowMovieAsync(movie, borrowDate, returnDate, model.BorrowerId);

            return this.Redirect("/");
        }

        [HttpGet]
        public async Task<IActionResult> History(int? id)
        {
            if (id == null)
            {
                return this.Redirect("/");
            }

            if (!await this.movieService.ContainsMovieAsync(id.Value))
            {
                return this.Redirect("/");
            }

            Movie movie = await this.historyService.GetMovieHistoryByIdAsync(id.Value);

            return this.View(movie);
        }


        [HttpGet]
        public async Task<IActionResult> Return(int? id)
        {
            if (id == null)
            {
                return this.Redirect("/");
            }

            if (!await this.movieService.ContainsMovieAsync(id.Value))
            {
                return this.Redirect("/");
            }

            await this.movieService.ReturnBookByIdAsync(id.Value);

            return this.Redirect("/");
        }

        private string GetVideoId(string searchTerm)
        {
            YouTubeService youtubeService = new YouTubeService(new BaseClientService.Initializer
            {
                //TODO Add Your API KEY 
                ApiKey = "ENTER_YOUR_YOUTUBE_API_HERE",
                ApplicationName = this.GetType().ToString()
            });

            SearchResource.ListRequest searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = searchTerm;
            searchListRequest.MaxResults = 50;

            //TODO You can search for everything basically. I just need the video Id so that i can show the trailer in the /Movies/Details/{id} View
            string id = searchListRequest.ExecuteAsync()
                .Result.Items.Where(x => x.Id.Kind == "youtube#video")
                .FirstOrDefault(x =>
                    x.Snippet.Title.ToLower().Contains("official trailer") ||
                    x.Snippet.Title.ToLower().Contains("trailer"))
                ?.Id.VideoId;

            return id;
        }

    }

}