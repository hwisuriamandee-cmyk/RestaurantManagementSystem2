using System.Collections.Generic;
using RestaurantManagementSystem2.Models;

namespace RestaurantManagementSystem2.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        List<T> GetAll();
        void Add(T entity);
        void Delete(int id);
    }
}