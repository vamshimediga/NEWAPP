using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface IHRMGER
    {
        List<HRMGER> GetHRMGER();

        HRMGER GetHRMGERById(int id);

        int InsertHRMGER(HRMGER hrmGER);

        int UpdateHRMGER(HRMGER hrmGER);


        int DeleteHRMGER(int id);
    }
}
