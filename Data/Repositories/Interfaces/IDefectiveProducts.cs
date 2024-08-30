using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface IDefectiveProducts
    {
         List<DefectiveProducts> GetProducts();

        DefectiveProducts GetProductsById(int id);

        int insert(DefectiveProducts defectiveProducts);

        int update(DefectiveProducts defectiveProducts);

        Boolean delete(int id);

    }
}
