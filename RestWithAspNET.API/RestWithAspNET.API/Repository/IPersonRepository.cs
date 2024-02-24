using RestWithAspNET.API.Business;
using RestWithAspNET.API.Model;
using System.Collections.Generic;

namespace RestWithAspNET.API.Repository
{
    public interface IPersonRepository : IRepository<Person>
    {
        Person Disable(long id);
        List<Person> FindByName(string firstname, string secondname);
    }
}
