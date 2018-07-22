namespace Movies.Services
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class MovieService : IMovieService
    {

        private readonly MovieContext context;

        private readonly IHistoryService service;

        public MovieService(MovieContext context, IHistoryService service)
        {
            this.context = context;
            this.service = service;
        }


        public async Task AddMovieAsync(string title, string directorName, string description, string image)
        {
            Director director =
                await this.context.Directors.SingleOrDefaultAsync(x => x.Name.ToLower() == directorName.ToLower());

            if (director == null)
            {
                director = new Director
                {
                    Name = directorName
                };

                director.Movies.Add(new Movie
                {
                    Title = title,
                    Description = description,
                    CoverImageUrl = image
                });

                await this.context.Directors.AddAsync(director);

                await this.context.SaveChangesAsync();

                return;
            }

            director.Movies.Add(new Movie
            {
                Title = title,
                Description = description,
                CoverImageUrl = image
            });

            await this.context.SaveChangesAsync();
        }

        public async Task<List<Movie>> GetAllMoviesAsync()
        {
            return await this.context.Movies.ToListAsync();
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            return await this.context.Movies.Include(x => x.Director).SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> ContainsMovieAsync(int id)
        {
            return await this.context.Movies.AnyAsync(x => x.Id == id);
        }

        public async Task ReturnBookByIdAsync(int id)
        {
            Movie movie = await this.service.GetMovieHistoryByIdAsync(id);

            movie.BorrowerId = null;
            movie.Status = true;

            if (movie.Histories.Last().ReturnDate == null)
            {
                movie.Histories.Last().ReturnDate = DateTime.Today;
            }

            await this.context.SaveChangesAsync();
        }

        public async Task<List<Movie>> GetMoviesBySearchTermAsync(string searchTerm)
        {
            return await this.context.Movies.Where(x => x.Title.ToLower().Contains(searchTerm.ToLower()))
                .ToListAsync();
        }

    }

}