using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface IOrdersWithCustomer
    {
        List<OrdersWithCustomer> GetAll();

        OrdersWithCustomer GetById(int id);

        int insert (OrdersWithCustomer customer);
        int update (OrdersWithCustomer customer);
        int delete (int id);    
    }
}
