using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface IStockoverflow
    {
        List<Stockoverflow> GetStockoverflows();

        Stockoverflow GetStockoverflowById(int id);

        int insertStockoverflow(Stockoverflow stockoverflow);
        int updateStockoverflow(Stockoverflow stockoverflow);
        int DeleteStockoverflow(int id);

        Stockoverflow IsEmailExistsAsync(string email);

    }
}
