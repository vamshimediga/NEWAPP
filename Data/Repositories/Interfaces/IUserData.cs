using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface IUserData
    {
        Task<List<UserData>> UserDataAsync(); 
        Task InsertUserData(UserData userData); 

        Task<UserData> UserDataById(int id);

        Task UpdateUserData(UserData userData);
    }
}
