﻿using DomainModels;
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
        Task<LeadSource> GetById(int id);
        Task<int> Insert(LeadSource leadSource);

        Task<int> Update(LeadSource leadSource);
        Task<List<LeadSource>> SearchLeadSourcesAsync(string searchTerm);

        Task<bool> DeleteMultipleAsync(string leadSourceIds); // For bulk delete

        Task<List<LeadSource>> GetleadSourcespaging(int startindex,int endindex);

    }
}
