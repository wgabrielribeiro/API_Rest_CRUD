using Microsoft.EntityFrameworkCore;
using RestWithAspNET.API.Model;
using RestWithAspNET.API.Model.Context;
using RestWithAspNET.API.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestWithAspNET.API.Repository
{
    public class PersonRepository : Genericrepository<Person>, IPersonRepository
    {
        public PersonRepository(AppDbContext context) : base(context) { }

        public Person Disable(long id)
        {
            if (!_dbContext.Persons.Any(p => p.Id.Equals(id))) return null;

            var user = _dbContext.Persons.SingleOrDefault(p => p.Id.Equals(id));

            if (user != null)
            {
                user.Enabled = false;
                try
                {
                    _dbContext.Entry(user).CurrentValues.SetValues(user);
                    _dbContext.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return user;
        }

        public List<Person> FindByName(string firstname, string lastname)
        {
            if (!string.IsNullOrWhiteSpace(firstname) && !string.IsNullOrWhiteSpace(lastname))
            {
                return _dbContext.Persons.AsNoTracking()
                .Where(p => p.Firstname.Contains(firstname) && p.LastName.Contains(lastname))
                    .ToList();
            }
            else if (string.IsNullOrWhiteSpace(firstname) && !string.IsNullOrWhiteSpace(lastname))
            {
                return _dbContext.Persons.AsNoTracking()
                .Where(p => p.LastName.Contains(lastname)).ToList();
            }
            else if (!string.IsNullOrWhiteSpace(firstname) && string.IsNullOrWhiteSpace(lastname))
            {
                return _dbContext.Persons.AsNoTracking()
                .Where(p => p.Firstname.Contains(firstname)).ToList();
            }

            return null;
        }
    }
}
