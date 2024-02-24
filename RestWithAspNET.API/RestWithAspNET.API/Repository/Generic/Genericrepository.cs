using Microsoft.EntityFrameworkCore;
using RestWithAspNET.API.Business;
using RestWithAspNET.API.Model.Base;
using RestWithAspNET.API.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestWithAspNET.API.Repository.Generic
{
    public class Genericrepository<T> : IRepository<T> where T : BaseEntity
    {
        protected AppDbContext _dbContext;
        private DbSet<T> dataset;

        public Genericrepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            dataset = _dbContext.Set<T>();
        }

        public T Create(T item)
        {
            try
            {
                dataset.Add(item);
                _dbContext.SaveChanges();

                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<T> FindAll()
        {
            try
            {
                return dataset.ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public T FindById(long id)
        {
            try
            {
                return dataset.SingleOrDefault(p => p.Id.Equals(id));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public T Update(T item)
        {
            try
            {
                var result = dataset.SingleOrDefault(p => p.Id.Equals(item.Id));

                if (result != null)
                {
                    _dbContext.Entry(result).CurrentValues.SetValues(item);
                    _dbContext.SaveChanges();

                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Delete(long id)
        {
            try
            {
                var result = dataset.SingleOrDefault(p => p.Id.Equals(id));

                if(result != null)
                {
                    dataset.Remove(result);
                    _dbContext.SaveChanges();
                }                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Exists(long id)
        {
            return dataset.Any(p => p.Id.Equals(id));
        }          
        public List<T> FindWithPagedSearch(string query)
        {
            return dataset.FromSqlRaw<T>(query).ToList();
        }
        public int getCount(string query)
        {
            var result = "";
            using(var connection = _dbContext.Database.GetDbConnection())
            {
                connection.Open();
                using(var command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    result = command.ExecuteScalar().ToString();
                }
            }

            return int.Parse(result);
        }
    }
}
