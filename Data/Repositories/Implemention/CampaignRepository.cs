using Dapper;
using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Implemention
{
    public class CampaignRepository : BaseRepository, ICampaign
    {
        public CampaignRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<bool> deleteById(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CampaignID", id, DbType.Int32);
            parameters.Add("@DeleteStatus", dbType: DbType.Boolean, direction: ParameterDirection.Output);

          await  ExecuteAsync("[dbo].[DeleteCampaign]", parameters);

            bool isDeleted = parameters.Get<bool>("@DeleteStatus");
            return isDeleted;
        }

        public async Task<List<CampaignModel>> GetAll()
        {
            List<CampaignModel> campaigns = await QueryAsync<CampaignModel>("sp_GetCampaign");
            return campaigns;   
        }

        public async Task<CampaignModel> GetById(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CampaignID", id, DbType.Int32, ParameterDirection.Input);
            CampaignModel campaignModel = await QueryFirstOrDefaultAsync<CampaignModel>("[dbo].[GetCampaignById]", parameters);
            return campaignModel;
        }

        public async Task<int> insert(CampaignModel model)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CampaignName", model.CampaignName, DbType.String, ParameterDirection.Input);
            parameters.Add("@StartDate", model.StartDate, DbType.Date, ParameterDirection.Input);
            parameters.Add("@EndDate", model.EndDate, DbType.Date, ParameterDirection.Input);
            parameters.Add("@Budget", model.Budget, DbType.Decimal, ParameterDirection.Input);
            parameters.Add("@MeetingID", model.MeetingID);
            parameters.Add("@ID", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await ExecuteAsync("sp_InsertCampaign", parameters);

            // Retrieve the output CampaignID
            return parameters.Get<int>("@ID");
        }

        public async Task<int> update(CampaignModel model)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CampaignID", model.CampaignID, DbType.Int32);
            parameters.Add("@CampaignName", model.CampaignName, DbType.String);
            parameters.Add("@StartDate", model.StartDate, DbType.Date);
            parameters.Add("@EndDate", model.EndDate, DbType.Date);
            parameters.Add("@Budget", model.Budget, DbType.Decimal);
            parameters.Add("@MeetingID", model.MeetingID, DbType.String);
            parameters.Add("@UpdateStatus", dbType: DbType.Int32, direction: ParameterDirection.Output);

          await  ExecuteAsync("[dbo].[UpdateCampaign]", parameters);

            // Retrieve the output parameter value
            int updateStatus = parameters.Get<int>("@UpdateStatus");

            return updateStatus;
        }
    }
}
