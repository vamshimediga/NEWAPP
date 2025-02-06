using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface ICampaign
    {
        Task<List<CampaignModel>> GetAll();

        Task<CampaignModel> GetById(int id);

        Task<int> insert(CampaignModel model);

        Task<int> update(CampaignModel model);  

        Task<int> deleteById(int id);
    }
}
