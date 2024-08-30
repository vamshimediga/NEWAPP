using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface ILeads
    {
        List<Lead> Leads();

        int insert(Lead lead);

        Lead LeadById(int id);  

        int update(Lead lead);  

        int delete(string[] leadIds); 
    }
}
