using RestWithAspNET.API.Data.Converter.Implementation;
using RestWithAspNET.API.Data.VO;
using RestWithAspNET.API.Hypermedia.Utils;
using RestWithAspNET.API.Repository;
using SQLitePCL;
using System.Collections.Generic;
using System.IO;

namespace RestWithAspNET.API.Business.Implementations
{
    public class PersonBusinessImplementation : IPersonBusiness
    {
        private readonly IPersonRepository _repository;
        private readonly PersonConverter personConverter;
        public PersonBusinessImplementation(IPersonRepository repository)
        {
            _repository = repository;
            personConverter = new PersonConverter();
        }
        public List<PersonVO> FindAll()
        {
            return personConverter.Parse(_repository.FindAll());
        }
        public PagedSearchVO<PersonVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page)
        {
            var sort = (!string.IsNullOrWhiteSpace(sortDirection)) && !sortDirection.Equals("desc") ? "asc" : "desc";

            var size = (pageSize < 1) ? 10 : pageSize;

            var offset = page > 0 ? (page - 1) * size : 0;

            string query = @"SELECT * FROM person p WHERE 1 = 1 ";

            if (!string.IsNullOrWhiteSpace(name)) query = query + $"AND p.Firstname LIKE '%{name}%' ";
            query += $" ORDER BY p.Firstname {sort}";
            query += $" OFFSET {offset} ROWS FETCH NEXT {size} ROWS ONLY;";

            string countQuery = @"select count(*) from person p where 1 = 1 ";
            if (!string.IsNullOrWhiteSpace(name)) countQuery += $" AND p.Firstname LIKE '%{name}%' ";

            var persons = _repository.FindWithPagedSearch(query);

            int totalresults = _repository.getCount(countQuery);

            return new PagedSearchVO<PersonVO> { 
            CurrentPage = page,
            List = personConverter.Parse(persons),
            PageSize = size,
            SortDirections = sort,
            TotalResults = totalresults
            };
        }
        public PersonVO FindById(long id)
        {
            return personConverter.Parse(_repository.FindById(id));
        }
        public List<PersonVO> FindByName(string firstname, string lastname)
        {
            return personConverter.Parse(_repository.FindByName(firstname, lastname));
        }
        public PersonVO Create(PersonVO person)
        {
            var personEntity = personConverter.Parse(person);
            personEntity = _repository.Create(personEntity);

            return personConverter.Parse(personEntity);
        }
        public PersonVO Update(PersonVO person)
        {
            var personEntity = personConverter.Parse(person);
            personEntity = _repository.Update(personEntity);

            return personConverter.Parse(personEntity);
        }
        public PersonVO Disable(long id)
        {
            var personEntity = _repository.Disable(id);

            return personConverter.Parse(personEntity);
        }
        public void Delete(long id)
        {
            _repository.Delete(id);
        }
        public bool Exists(long id)
        {
            return _repository.Exists(id);
        }

    }
}
