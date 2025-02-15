using Dapper;
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
    public class MeetingRepository : BaseRepository, IMeeting
    {
        public MeetingRepository(IConfiguration configuration) : base(configuration)
        {
        }
        public async Task<int> deleteById(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@MeetingID", id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@RowsAffected", dbType: DbType.Int32, direction: ParameterDirection.Output);
            int rowsAffected= await  ExecuteAsync("sp_DeleteMeeting", parameters);
            rowsAffected = parameters.Get<int>("@RowsAffected");
            return rowsAffected;
        }

        public async Task<List<MeetingModel>> GetAll()
        {
            List<MeetingModel> meetingModels = await QueryAsync<MeetingModel>("sp_GetAllMeetings");
            return meetingModels;   
        }

        public async Task<MeetingModel> GetById(int id)
        {
            // Using DynamicParameters
            var parameters = new DynamicParameters();
            parameters.Add("@MeetingID", id, DbType.Int32, ParameterDirection.Input);
            // Execute stored procedure and get the meeting details
            MeetingModel meeting = await QueryFirstOrDefaultAsync<MeetingModel>("sp_GetMeetingByID", parameters);
            return meeting;
        }

        public async Task<int> insert(MeetingModel model)
        {
            int newMeetingId = 0;
            try
            {
                // Define the parameters for the stored procedure
                var parameters = new DynamicParameters();
                parameters.Add("@CampaignID", model.CampaignID, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@MeetingDate", model.MeetingDate, DbType.DateTime, ParameterDirection.Input);
                parameters.Add("@Location", model.Location, DbType.String, ParameterDirection.Input);
                parameters.Add("@Description", model.Description, DbType.String, ParameterDirection.Input);
                parameters.Add("@NewMeetingID", dbType: DbType.Int32, direction: ParameterDirection.Output);

                // Call the stored procedure
                await ExecuteAsync("sp_InsertMeeting", parameters);

                // Get the output parameter value
                 newMeetingId = parameters.Get<int>("@NewMeetingID");
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return newMeetingId;
        }

        public async Task<int> update(MeetingModel model)
        {
            int rowsAffected = 0;
            try
            {
                // Define the parameters for the stored procedure
                var parameters = new DynamicParameters();
                parameters.Add("@MeetingID", model.MeetingID, DbType.Int32, ParameterDirection.Input); // Example MeetingID
                parameters.Add("@CampaignID", model.CampaignID, DbType.Int32, ParameterDirection.Input); // New CampaignID
                parameters.Add("@MeetingDate", model.MeetingDate, DbType.DateTime, ParameterDirection.Input); // New Meeting Date
                parameters.Add("@Location", model.Location, DbType.String, ParameterDirection.Input); // New Location
                parameters.Add("@Description", model.Description, DbType.String, ParameterDirection.Input); // New Description
                parameters.Add("@RowsAffected", dbType: DbType.Int32, direction: ParameterDirection.Output); // Output parameter for RowsAffected

                // Call the stored procedure to execute the update
              await  ExecuteAsync("sp_UpdateMeeting", parameters);

                // Get the output parameter value (number of rows affected)
                 rowsAffected = parameters.Get<int>("@RowsAffected");

              
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return rowsAffected;
        }
    }
}
