﻿using RestWithAspNET.API.Model;
using RestWithAspNET.API.Model.Base;
using System.Collections.Generic;

namespace RestWithAspNET.API.Business
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Create(T item);
        T FindById(long id);
        List<T> FindAll();
        T Update(T item);
        void Delete(long id);
        bool Exists(long id);
        List<T> FindWithPagedSearch(string query);
        int getCount(string query);
    }
}
