using DapperDemo.Models;
using System.Collections.Generic;

namespace DapperDemo.Repository.Dapper
{
    public interface IAdvanceDapperRepository
    {
        List<Employee> GetEmployeeWithCompany(int id=0);

        Company GetCompanyWithEmployees(int id);

        List<Company> GetAllCompanyWithEmployees();

        void AddTestCompanyWithEmployees(Company objComp);

        void AddTestCompanyWithEmployeesWithTransaction(Company objComp);

        void RemoveRange(int[] companyId);

        List<Company> FilterCompanyByName(string name);
    }
}
