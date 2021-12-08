using DapperDemo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DapperDemo.Repository.Dapper
{
    public interface IEmployeeRepository
    {
        Employee Add(Employee employee);
        Task<Employee> AddAsync(Employee employee);
        Employee Find(int id);
        List<Employee> GetAll();
        void Remove(int id);
        Employee Update(Employee employee);

        public bool Exists(int id);
    }
}