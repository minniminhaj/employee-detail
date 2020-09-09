using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeDetail.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        List<Employee> _employees;
        public MockEmployeeRepository()
        {
            _employees = new List<Employee>
            {
                new Employee(){Id=2 ,EmployeeName="MInhaj",Department=Dept.HR,Email ="minhah@yopmail.com"  },
                new Employee(){Id=3 ,EmployeeName="Don",Department=Dept.IT,Email="minhah@yopmail.com"  },
                new Employee(){Id=6 ,EmployeeName="King",Department=Dept.Marketing,Email="minhah@yopmail.com"   },
                new Employee(){Id=1 ,EmployeeName="Pawn",Department=Dept.HR,Email="minhah@yopmail.com"   },
            };
        }

        public Employee Add(Employee newEmployee)

        {
            newEmployee.Id = _employees.Max(e => e.Id) + 1;
            _employees.Add(newEmployee);
            return newEmployee;
        }

        public Employee Delete(int id)
        {
          Employee employee=  _employees.FirstOrDefault(e => e.Id == id);
            if (employee != null)
            {
                _employees.Remove(employee);

            }
            return employee;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _employees;
        }

        public Employee GetEmployee(int id)
        {
            return _employees.FirstOrDefault(e => e.Id == id);
        }

        public Employee Update(Employee employeeChanges)
        {
            Employee employee = _employees.FirstOrDefault(e => e.Id == employeeChanges.Id);
            if (employee != null)
            {
                employeeChanges.EmployeeName = employee.EmployeeName;
                employeeChanges.Department = employee.Department;
                employeeChanges.Email = employee.Email;
            }
            return employee;
        }
    }
}
