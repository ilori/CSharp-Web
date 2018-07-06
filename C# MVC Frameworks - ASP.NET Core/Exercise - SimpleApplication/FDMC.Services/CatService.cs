namespace FDMC.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Data;
    using Models;

    public class CatService : ICatService
    {
        private readonly FluffyContext context;

        public CatService(FluffyContext context)
        {
            this.context = context;
        }

        public IEnumerable<Cat> GetAll()
        {
            List<Cat> cats = new List<Cat>();

            using (context)
            {
                var a = context.Cats.ToList();
                cats.AddRange(a);
            }

            return cats;
        }

        public Cat GetCatById(int id)
        {
            using (context)
            {
                Cat cat = context.Cats.Find(id);
                return cat;
            }
        }

        public void CreateCat(string modelName, int modelAge, string modelBreed, string modelImageUrl)
        {
            using (context)
            {
                context.Cats.Add(new Cat()
                {
                    Name = modelName,
                    Age = modelAge,
                    Breed = modelBreed,
                    ImageUrl = modelImageUrl
                });

                context.SaveChanges();
            }
        }
    }
}