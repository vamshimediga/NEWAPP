using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface IPsplCompany
    {
        List<PsplCompany>   GetPsplCompanies();
        PsplCompany GetPsplCompany(int id);

        int Insert(PsplCompany psplCompany);
        int update(PsplCompany psplCompany);

        int Delete(int id);
    }
}
