using System.Collections.Generic;
using WebApplication.Models;

namespace WebApplication.Services
{
    public class StorageGenres : IStorageGenres
    {
        private readonly List<GenreModel> _storage = new List<GenreModel>();
        private int _curId = 1; // bug! fixed
        
        public GenreModel Add(GenreModel genreModel, int parent)
        {
            var parentGenre = Get(parent);
            lock (_storage)
            {
                genreModel.Id = _curId;
                _curId++;
                genreModel.Parent = parent;
                if (parentGenre != null)
                {
                    if (parentGenre.Subgenres == null)
                    {
                        parentGenre.Subgenres = new List<int>();
                    }
                    parentGenre.Subgenres.Add(genreModel.Id);
                }
                _storage.Add(genreModel);
            }

            return genreModel;
        }

        public GenreModel Get(int id)
        {
            lock (_storage)
            {
                for (var i = 0; i < _storage.Count; i++)
                {
                    if (_storage[i].Id == id) return _storage[i];//fixed!!
                }
            }

            return null;
        }
    }
}