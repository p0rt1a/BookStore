using BookStore.DbOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommand
    {
        private readonly BookStoreDbContext _dbContext;
        public UpdateGenreModel Model { get; set; }
        public int GenreId { get; set; }

        public UpdateGenreCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var genre = _dbContext.Genres.SingleOrDefault(x => x.Id == GenreId);

            if (genre is null)
                throw new InvalidOperationException("Kitap türü bulunamadı");

            if (_dbContext.Genres.Any(x => x.Name.ToLower() == Model.Name.ToLower() && x.Id != GenreId))
                throw new InvalidOperationException("Kitap türü zaten mevcut");

            genre.Name = string.IsNullOrEmpty(Model.Name.Trim()) ? genre.Name : Model.Name;
            genre.IsActive = Model.IsActive;

            _dbContext.SaveChanges();
        }
    }

    public class UpdateGenreModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
