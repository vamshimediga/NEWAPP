using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public  interface ITab
    {
        List<Tab> GetTabs();
        Tab GetTabById(int id);
        int insertTabs(Tab tab);    
        int updateTabs(Tab tab);    
        bool deleteTabs(int id);
    }
}
