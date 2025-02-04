﻿using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface IDoctorRepository
    {
        Task<Doctor> GetByIdAsync(int id);
        Task<List<Doctor>> GetAllAsync();
        Task AddAsync(Doctor doctor);
        Task UpdateAsync(Doctor doctor);
        Task DeleteAsync(int id);
    }
}
