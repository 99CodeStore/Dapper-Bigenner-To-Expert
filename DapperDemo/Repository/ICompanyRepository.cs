using DapperDemo.Models;
using System.Collections.Generic;

namespace DapperDemo.Repository
{
    public interface ICompanyRepository
    {
        Company Find(int id);
        Company Add(Company company);
        Company Update(Company company);
        List<Company> GetAll();
        void Remove(int id);
        public bool Exists(int id);
    }
}
