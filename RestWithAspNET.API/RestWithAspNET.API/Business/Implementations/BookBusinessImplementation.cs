using RestWithAspNET.API.Data.Converter.Implementation;
using RestWithAspNET.API.Data.VO;
using RestWithAspNET.API.Model;
using RestWithAspNET.API.Model.Context;
using RestWithAspNET.API.Repository;
using RestWithAspNET.API.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace RestWithAspNET.API.Business.Implementations
{
    public class BookBusinessImplementation : IBookBusiness
    {
        private readonly IRepository<Book> _repository;
        private readonly BookConverter bookConverter;

        public BookBusinessImplementation(IRepository<Book> repository)
        {
            _repository = repository;
            bookConverter = new BookConverter();
        }

        public BookVO Create(BookVO book)
        {
            try
            {
                var bookEntity = bookConverter.Parse(book);

                bookEntity = _repository.Create(bookEntity);

                return bookConverter.Parse(bookEntity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public BookVO Update(BookVO book)
        {
            try
            {
                var bookEntity = bookConverter.Parse(book);

                bookEntity = _repository.Update(bookEntity);

                return bookConverter.Parse(bookEntity);
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
                _repository.Delete(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BookVO> FindAll()
        {

            return bookConverter.Parse(_repository.FindAll());
        }
        public BookVO FindById(long id)
        {
            try
            {
                return bookConverter.Parse(_repository.FindById(id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Exists(long id)
        {
            return _repository.Exists(id);
        }

    }
}
