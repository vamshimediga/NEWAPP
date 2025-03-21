﻿using Data.Repositories.Interfaces;
using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface IUnitOfWork
    { 
        IClient clientRepository { get; }
        ICompanyUsers   companyUsers { get; }
        IEmployees employees { get; }

        ITab tab { get; }
        IManager manager { get; }
        IDefectiveProducts defectiveProducts { get; }

        IAdminUsers adminUsers { get; }

        IFile file { get; }
        IProductChart productChart { get; }

        IHRMGER hRMGER { get; }

        IUserradiochkdrop userradiochkdrop { get; }

        IProjectInfo projectInfo { get; }
        IOrdersWithCustomer orders { get; }

        IStockoverflow stockoverflow { get; }

        IUserAdmin uAdmin { get; }

        IMAdmin mAdmin { get; }

        IPsplCompany psplCompany { get; }

        IMediaWithCategory media { get; }
        IRestaurant restaurant { get; }
        ILeads leads { get; }

        ICourse course { get; }
        ISampleStudent smpleStudent { get; }

        ICustomerDATARepository<CustomerData> CustomerDATARepository { get; }

        IHumanResources humanResources { get; }

        ILeadSource leadSource { get; }
        IUserData UserData { get; }
        IPersonData PersonData { get; }

        ICompany company { get; }

        IContect Contect { get; }

        Ionline_retailUserLogin ionline_RetailUserLogin { get; }

        IAccessTable accessTable { get; }

        IUserLogin UserLogin { get; }

        ICookingRecipe CookingRecipe { get; }

        IITInstitute ITInstitute { get; }

        IAuthor Author { get; }
        IPsplCustomer PsplCustomer { get; }

        IPsplClient PsplClient { get; }

        IAuthor_US Author_US { get; }
        IBodyguard Bodyguard { get; }

        IUsersList UsersList { get; }

        IUserRoles UsersRoles { get; }

        IMeeting meeting { get; }

        ICampaign Campaign { get; }

        IBuildingOwner BuildingOwner { get; }

        IConstructionBuilder ConstructionBuilder { get; }

        IFlat Flat {  get; }

        IPropertyOwners propertyOwners { get; }
    }
}
