using InternFlow.DAL.Context;
using InternFlow.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternFlow.DAL.Repositories
{
    //
    //Burası aYnı efcore mantığıyla calısır listele,sil fln. ancak Irepository den kalıtım alır ve onun metodlarını implement eder.
    //Bu sayede her entity için ayrı repository yazmak yerine tek bir repository ile tüm entityler için islemler yaptırız.
    //Bu repository pattern olarak adlandırılır ve kodun tekrarını azaltır 
    //
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public List<T> GetAll() => _dbSet.ToList();

        public T GetById(int id) => _dbSet.Find(id) ?? throw new Exception($"Entity with id {id} not found."); 

        public void Add(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                _context.SaveChanges();
            }
        }
    }
}
