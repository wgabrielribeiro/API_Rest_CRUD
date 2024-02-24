using RestWithAspNET.API.Data.Converter.Contract;
using RestWithAspNET.API.Data.VO;
using RestWithAspNET.API.Model;
using System.Collections.Generic;
using System.Linq;

namespace RestWithAspNET.API.Data.Converter.Implementation
{
    public class PersonConverter : IParser<PersonVO, Person>, IParser<Person, PersonVO>
    {
        public Person Parse(PersonVO origem)
        {
            if (origem == null)
                return null;

            return new Person
            {
                Id = origem.Id,
                Firstname = origem.Firstname,
                LastName = origem.LastName,
                Address = origem.Address,
                Gender = origem.Gender              
            };
        }

        public PersonVO Parse(Person origem)
        {
            if (origem == null)
                return null;

            return new PersonVO
            {
                Id = origem.Id,
                Firstname = origem.Firstname,
                LastName = origem.LastName,
                Address = origem.Address,
                Gender = origem.Gender
            };
        }

        public List<PersonVO> Parse(List<Person> origem)
        {
            if (origem == null)
                return null;

            return origem.Select(item => Parse(item)).ToList();
        }

        public List<Person> Parse(List<PersonVO> origem)
        {
            return origem.Select(item => Parse(item)).ToList();
        }
    }
}
