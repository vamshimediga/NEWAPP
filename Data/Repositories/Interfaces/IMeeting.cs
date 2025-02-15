using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface IMeeting
    {
        Task<List<MeetingModel>> GetAll();

        Task<MeetingModel> GetById(int id);

        Task<int> insert(MeetingModel model);

        Task<int> update(MeetingModel model);

        Task<int> deleteById(int id);
    }
}
