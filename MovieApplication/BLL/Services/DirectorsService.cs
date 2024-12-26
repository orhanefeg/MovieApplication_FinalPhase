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
    public interface IDirectorsService
    {
        public IQueryable<DirectorModel> Query();

        public ServiceBase Create(Director record);

        public ServiceBase Update(Director record);

        public ServiceBase Delete(int id);

       

    }
    public class DirectorsService : ServiceBase, IDirectorsService
    {
        public DirectorsService(Db db) : base(db)
        {
        }

        public ServiceBase Create(Director record)
        {
            if (_db.Directors.Any(d => d.Name.ToUpper() == record.Name.ToUpper().Trim()))
                return Error("Directors with the same name exists!");
            record.Name = record.Name?.Trim();
            _db.Directors.Add(record);
            _db.SaveChanges();
            return Success("Director created :)");
        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.Directors.Include(d => d.Movies).SingleOrDefault(d => d.Id == id);
            if (entity == null)
                return Error("Directors can not be found");
            if (entity.Movies.Any())
                return Error("Directors has relational movies");
            _db.Directors.Remove(entity);
            _db.SaveChanges();
            return Success("Directors deleted successfully");


        }

        public IQueryable<DirectorModel> Query()
        {
            return _db.Directors.OrderBy(d => d.Name).Select(d => new DirectorModel() { Record = d });

        }

        public ServiceBase Update(Director record)
        {
            if (_db.Directors.Any(d => d.Id != record.Id && d.Name.ToUpper() == record.Name.ToUpper().Trim()))
                return Error("Directors with the same name exists!");
            var entity = _db.Directors.SingleOrDefault(d => d.Id == record.Id);
            if (entity == null)
                return Error("Directors can not be found");
            entity.Name = record.Name?.Trim();
            _db.Directors.Update(entity);
            _db.SaveChanges();
            return Success("Directors updated successfully");
        }
    }
}
