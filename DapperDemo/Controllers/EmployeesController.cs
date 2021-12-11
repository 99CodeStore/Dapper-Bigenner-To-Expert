using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DapperDemo.Models;
using DapperDemo.Repository;
using DapperDemo.Repository.Dapper;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace DapperDemo.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ICompanyRepository _compRepo;
        private readonly IEmployeeRepository _empRepo;
        private readonly IAdvanceDapperRepository advanceDapperRepository;

        [BindProperty]
        public Employee Employee { get; set; }

        public EmployeesController(ICompanyRepository compRepo, IEmployeeRepository employeeRepository, IAdvanceDapperRepository advanceDapperRepository)
        {
            this._compRepo = compRepo;
            this._empRepo = employeeRepository;
            this.advanceDapperRepository = advanceDapperRepository;
        }

        // GET: Employees
        public async Task<IActionResult> Index(int? CompanyId)
        {

            //List<Employee> employees = _empRepo.GetAll();
            //foreach (Employee obj in employees)
            //{
            //    obj.Company = _compRepo.Find(obj.CompanyId);
            //}
            List<Employee> employees = advanceDapperRepository.GetEmployeeWithCompany(CompanyId.GetValueOrDefault());
            return View(employees);

        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = _empRepo.Find(id.GetValueOrDefault());

            var company = _compRepo.Find(employee.CompanyId);

            employee.Company = company;


            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            IEnumerable<SelectListItem> companyList = _compRepo.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.CompanyId.ToString()
            });
            ViewBag.CompanyList = companyList;

            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("create")]
        public async Task<IActionResult> CreateEmployee()
        {
            if (ModelState.IsValid)
            {
                _empRepo.Add(Employee);
                return RedirectToAction(nameof(Index));
            }
            return View(Employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Employee = _empRepo.Find(id.GetValueOrDefault());
            if (Employee == null)
            {
                return NotFound();
            }

            IEnumerable<SelectListItem> companyList = _compRepo.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.CompanyId.ToString()
            });
            ViewBag.CompanyList = companyList;

            return View(Employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            if (id != Employee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _empRepo.Update(Employee);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(Employee.EmployeeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(Employee);
        }

        // GET: Companies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = _empRepo.Find(id.GetValueOrDefault());

            var company = _compRepo.Find(employee.CompanyId);

            employee.Company = company;

            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _empRepo.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _empRepo.Exists(id);
        }
    }
}
