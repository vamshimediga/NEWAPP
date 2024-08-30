using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface IRestaurant
    {
        List<Restaurant> GetAll();
        int insert(Restaurant restaurant);
    }
}
