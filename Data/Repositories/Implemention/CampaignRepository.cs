using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Implemention
{
    public class CampaignRepository : BaseRepository, ICampaign
    {
        public CampaignRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public Task<int> deleteById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CampaignModel>> GetAll()
        {
            List<CampaignModel> campaigns = await QueryAsync<CampaignModel>("sp_GetCampaign");
            return campaigns;   
        }

        public Task<CampaignModel> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> insert(CampaignModel model)
        {
            throw new NotImplementedException();
        }

        public Task<int> update(CampaignModel model)
        {
            throw new NotImplementedException();
        }
    }
}
