﻿using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface IUserradiochkdrop
    {
        List<Userchkradio> GetUserchkradio();

        int InsertUserchkradio (Userchkradio userchkradio);

    }
}
