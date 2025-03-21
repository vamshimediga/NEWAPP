﻿using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface ICompany
    {
        Task<List<Company>> GetAll();

        Task<Company> GetById(int id);

        Task<int> insert(Company company);
        Task<int> update(Company company);
        Task<bool> delete(List<int> ids);

    }
}
