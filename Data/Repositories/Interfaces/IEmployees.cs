using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface IEmployees
    {
        Employees GetEmployeesByid(int Id);
        List<Employees> GetEmployees();

        int InsertEmployees(Employees employees);
        int UpdateEmployees(Employees employees);
        bool DeleteEmployees(int id);

      

    }
}
