using Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Implemention
{
    public class CommonServiceRepository : ICommonService
    {
       
        public void ExecutedCommonTask()
        {
            Debug.WriteLine("Executed common task.");
        }

        public void ExecuteingCommonTask()
        {
            Debug.WriteLine("Executing common task.");
        }
    }
}
