namespace FDMC.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface ICatService
    {
        IEnumerable<Cat> GetAll();

        Cat GetCatById(int id);
        void CreateCat(string modelName, int modelAge, string modelBreed, string modelImageUrl);
    }
}