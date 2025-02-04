using Dapper;
using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Implemention
{
    public class PatientRepository : IPatientRepository
    {
        public readonly DbConnection _connection;
        public PatientRepository(IConfiguration configuration)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
        // Add a new patient
        public async Task AddAsync(Patient patient)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@PatientName", patient.PatientName);
            parameters.Add("@DoctorID", patient.DoctorID);

            var result = await _connection.ExecuteAsync("[dbo].[InsertPatient]", parameters, commandType: CommandType.StoredProcedure);
            if (result == 0)
            {
                throw new Exception("Failed to insert patient");
            }
        }

        // Delete a patient by ID
        public async Task DeleteAsync(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@PatientID", id);

            var result = await _connection.ExecuteAsync("sp_DeletePatient", parameters, commandType: CommandType.StoredProcedure);
            if (result == 0)
            {
                throw new Exception("Failed to delete patient");
            }
        }

        // Get all patients
        public async Task<List<Patient>> GetAllAsync()
        {
            var result = await _connection.QueryAsync<Patient>("GetPatients", commandType: CommandType.StoredProcedure);
            return result.ToList();
        }

        // Get a patient by ID
        public async Task<Patient> GetByIdAsync(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@PatientID", id);

            var result = await _connection.QuerySingleOrDefaultAsync<Patient>("[dbo].[GetPatientById]", parameters, commandType: CommandType.StoredProcedure);
            return result;
        }

        // Update a patient
        public async Task UpdateAsync(Patient patient)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@PatientID", patient.PatientID);
            parameters.Add("@PatientName", patient.PatientName);
            parameters.Add("@DoctorID", patient.DoctorID);

            var result = await _connection.ExecuteAsync("[dbo].[UpdatePatient]", parameters, commandType: CommandType.StoredProcedure);
            if (result == 0)
            {
                throw new Exception("Failed to update patient");
            }
        }
    }
}
