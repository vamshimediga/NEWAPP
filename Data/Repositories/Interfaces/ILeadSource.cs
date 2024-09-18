using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface ILeadSource
    {
        Task<List<LeadSource>> GetleadSources();
        Task<int> Insert(LeadSource leadSource);
    }
}
