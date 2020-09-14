using WebApplication.Models;

namespace WebApplication.Services
{
    public interface IStorageGenres
    {
        public GenreModel Add(GenreModel genreModel, int parent);
        public GenreModel Get(int id);
    }
}