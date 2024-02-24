using RestWithAspNET.API.Data.VO;
using RestWithAspNET.API.Model;
using System.Collections.Generic;

namespace RestWithAspNET.API.Repository
{
    public interface IBookBusiness
    {
        BookVO Create(BookVO person);
        BookVO FindById(long id);
        List<BookVO> FindAll();
        BookVO Update(BookVO person);
        void Delete(long id);
        bool Exists(long id);
    }
}
