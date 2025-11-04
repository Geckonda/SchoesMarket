using Microsoft.EntityFrameworkCore;
using ShoesMarket.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoesMarket.DAL.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly AppDbContext _db;
        private readonly DbSet<T> _dbset;

        public BaseRepository(AppDbContext db)
        {
            _db = db;
            _dbset = db.Set<T>();
        }
        public void Add(T entity)
        {
            _dbset.Add(entity);
            _db.SaveChanges();
        }

        public void Delete(T entity)
        {
            _dbset.Remove(entity);
            _db.SaveChanges();
        }

        public virtual List<T>? GetAll()
        {
            return [.. _dbset];
        }

        public virtual T? GetOneById(int id)
        {
            return _dbset.Find(id);
        }

        public virtual void Update(T entity)
        {
            _dbset.Update(entity);
            _db.SaveChanges();
        }
    }
}
