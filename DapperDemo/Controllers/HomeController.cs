using DapperDemo.Models;
using DapperDemo.Repository.Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DapperDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAdvanceDapperRepository advanceDapperRepository;

        public HomeController(ILogger<HomeController> logger, IAdvanceDapperRepository advanceDapperRepository)
        {
            _logger = logger;
            this.advanceDapperRepository = advanceDapperRepository;
        }

        public IActionResult Index()
        {
            var companies = advanceDapperRepository.GetAllCompanyWithEmployees();
            return View(companies);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AddTestRecords()
        {

            Company company = new Company()
            {
                Name = "Test" + Guid.NewGuid().ToString(),
                Address = "test address",
                City = "test city",
                PostalCode = "test postalCode",
                State = "test state",
                Employees = new List<Employee>()
            };

            company.Employees.Add(new Employee()
            {
                Email = "test Email",
                Name = "Test Name " + Guid.NewGuid().ToString(),
                Phone = " test phone",
                Title = "Test Manager"
            });

            company.Employees.Add(new Employee()
            {
                Email = "test Email 2",
                Name = "Test Name 2" + Guid.NewGuid().ToString(),
                Phone = " test phone 2",
                Title = "Test Manager 2"
            });
            advanceDapperRepository.AddTestCompanyWithEmployeesWithTransaction(company);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoveTestRecords()
        {
            int[] companyIdToRemove = advanceDapperRepository.FilterCompanyByName("Test").Select(i => i.CompanyId).ToArray();
            advanceDapperRepository.RemoveRange(companyIdToRemove);
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
