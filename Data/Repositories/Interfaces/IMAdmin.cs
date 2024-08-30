using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface IMAdmin
    {
        List<MAdmin> GetMAdmins();

        int Insert(MAdmin madmin);  

        MAdmin GetMAdminByid(int id);
    }
}
