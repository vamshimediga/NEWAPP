using Dapper;
using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Implemention
{
    public class TabRepository : ITab
    {
        public readonly IDbConnection _db;
        public TabRepository(IConfiguration configuration)
        {
            _db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public bool deleteTabs(int id)
        {
            int rowsAffected = _db.Execute("UPDATE Tabs SET tabPosition = tabPosition - 1 WHERE tabID > @Id; DELETE FROM Tabs WHERE tabID = @Id",
               new { Id = id }, commandType: CommandType.Text);

            // Assuming you want to return true if at least one row was affected, otherwise false
            return rowsAffected > 0;
        }

        public Tab GetTabById(int id)
        {
            Tab tab = (Tab)_db.QueryFirstOrDefault<Tab>("SELECT * FROM Tabs WHERE tabID =tabID", new { tabID = id }, commandType: CommandType.Text);
            return tab;
        }

        public List<Tab> GetTabs()
        {
            List<Tab> tabs = (List<Tab>)_db.Query<Tab>("SELECT * FROM Tabs", commandType: CommandType.Text);
            return tabs;
        }

        public int insertTabs(Tab tab)
        {
            int rowsAffected = _db.Execute("INSERT INTO Tabs (tabID, tabName, tabPosition) VALUES (@TabID, @TabName, @TabPosition)",
              new { TabID = tab.tabID, TabName = tab.tabName, TabPosition = tab.tabPosition }, commandType: CommandType.Text);
            return rowsAffected;
        }

        public int updateTabs(Tab tab)
        {
            int id = _db.Execute("Update Tabs set tabName=@TabName,tabPosition=@TabPosition WHERE tabID= @TabID", new { TabID = tab.tabID, TabName = tab.tabName, TabPosition = tab.tabPosition }, commandType: CommandType.Text);
            return id;
        }
    }
}
