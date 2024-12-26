using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{

    //public interface IMovieService
    //{
    //    public IQueryable<MovieModel> Query();

    //    public ServiceBase Create(Movie record);

    //    public ServiceBase Update(Movie record);

    //    public ServiceBase Delete(int id);
    //}
    public class MovieService : ServiceBase, IService<Movie,MovieModel>
    {
        public MovieService(Db db) : base(db)
        {
        }

        public ServiceBase Create(Movie record)
        {
            if (_db.Movies.Any(m => m.Name.ToLower() == record.Name.ToLower().Trim() && m.ReleaseDate == record.ReleaseDate))
                return Error("Movies with the same name and release dates are existed!! ");
            record.Name = record.Name?.Trim();
            _db.Movies.Add(record);
            _db.SaveChanges();
            return Success("Movies Created Successfully");

        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.Movies.Include(m => m.MovieGenres).SingleOrDefault(m => m.Id == id);
            if (entity == null)
                return Error("Movie cant be found");
            _db.MovieGenres.RemoveRange(entity.MovieGenres);
            _db.Movies.Remove(entity);
            _db.SaveChanges();
            return Success("Movie deleted successfully");
        }

        public IQueryable<MovieModel> Query()
        {
            return _db.Movies.Include(m =>m.Director).Include(m => m.MovieGenres).ThenInclude(mg => mg.Genre).OrderByDescending(m => m.ReleaseDate).ThenByDescending(m => m.TotalRevenue).Select(m => new MovieModel() { Record = m });

        }

        public ServiceBase Update(Movie record)
        {
            if (_db.Movies.Any(m =>m.Id != record.Id && m.Name.ToLower() == record.Name.ToLower().Trim() && m.ReleaseDate == record.ReleaseDate))
                return Error("Movies with the same name and release dates are existed!! ");
            var entity = _db.Movies.Include(m => m.MovieGenres).SingleOrDefault(m => m.Id == record.Id);
            if (entity is null)
                return Error("Movie NOT Found!");
            _db.MovieGenres.RemoveRange(entity.MovieGenres);
            entity.Name = record.Name?.Trim();
            entity.ReleaseDate = record.ReleaseDate;
            entity.TotalRevenue = record.TotalRevenue;
            entity.DirectorId = record.DirectorId;
            entity.MovieGenres = record.MovieGenres;
            _db.Movies.Update(entity);
            _db.SaveChanges();
            return Success("Movies Updated Successfully");
        }
    }
}
