using RestWithAspNET.API.Data.Converter.Contract;
using RestWithAspNET.API.Data.VO;
using RestWithAspNET.API.Model;
using System.Collections.Generic;
using System.Linq;

namespace RestWithAspNET.API.Data.Converter.Implementation
{
    public class BookConverter : IParser<BookVO, Book>, IParser<Book, BookVO>
    {
        public Book Parse(BookVO origem)
        {
            if (origem == null) return null;

            return new Book
            { 
                Id = origem.Id,
                Author = origem.Author,
                Title = origem.Title,
                Price = origem.Price,
                LaunchDate = origem.LaunchDate            
            };
        }
        public BookVO Parse(Book origem)
        {
            if (origem == null) return null;

            return new BookVO
            {
                Id = origem.Id,
                Author = origem.Author,
                Title = origem.Title,
                Price = origem.Price,
                LaunchDate = origem.LaunchDate
            };
        }
        public List<Book> Parse(List<BookVO> origem)
        {
            return origem.Select(item => Parse(item)).ToList();
        }
        public List<BookVO> Parse(List<Book> origem)
        {
            return origem.Select(item => Parse(item)).ToList();
        }
    }
}
