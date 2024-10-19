using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Implemention
{
    public class ContectRepository : BaseRepository, IContect
    {
        public ContectRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public Task<int> delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Contect> GetContectById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Contect>> GetContects()
        {
            List<Contect> contects = await QueryAsync<Contect>("[dbo].[GetAllContects]");
            return contects;
        }

        public Task<int> insert(Contect contect)
        {
            throw new NotImplementedException();
        }

        public Task<int> update(Contect contect)
        {
            throw new NotImplementedException();
        }
    }
}
