using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Implemention
{
    using Dapper;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;

    public class DoctorRepository : IDoctorRepository
    {
        private readonly DbConnection _connection;

        public DoctorRepository(IConfiguration configuration)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        // Add a new doctor
        public async Task AddAsync(Doctor doctor)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@DoctorName", doctor.DoctorName);

            // Use stored procedure InsertDoctor and return inserted DoctorID
            var doctorId = await _connection.QuerySingleAsync<int>("dbo.InsertDoctor", parameters, commandType: CommandType.StoredProcedure);

            if (doctorId > 0)
            {
                doctor.DoctorID = doctorId;
            }
            else
            {
                throw new Exception("Failed to insert doctor");
            }
        }

        // Delete a doctor by ID
        public async Task DeleteAsync(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@DoctorID", id);

            var result = await _connection.ExecuteAsync("dbo.sp_DeleteDoctor", parameters, commandType: CommandType.StoredProcedure);

            if (result == 0)
            {
                throw new Exception("Failed to delete doctor");
            }
        }

        // Get all doctors (using the GetDoctors stored procedure)
        public async Task<List<Doctor>> GetAllAsync()
        {
            var result = await _connection.QueryAsync<Doctor>("dbo.GetDoctors", commandType: CommandType.StoredProcedure);
            return result.AsList(); // Convert the result to a List<Doctor>
        }

        // Get a doctor by ID (using the GetDoctorById stored procedure)
        public async Task<Doctor> GetByIdAsync(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@DoctorID", id);

            var doctor = await _connection.QueryFirstOrDefaultAsync<Doctor>("dbo.GetDoctorById", parameters, commandType: CommandType.StoredProcedure);

            if (doctor == null)
            {
                throw new Exception("Doctor not found");
            }

            return doctor;
        }

        // Update a doctor's information
        public async Task UpdateAsync(Doctor doctor)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@DoctorID", doctor.DoctorID);
            parameters.Add("@DoctorName", doctor.DoctorName);

            var result = await _connection.ExecuteAsync("dbo.UpdateDoctor", parameters, commandType: CommandType.StoredProcedure);

            if (result == 0)
            {
                throw new Exception("Failed to update doctor");
            }
        }
    }

}
