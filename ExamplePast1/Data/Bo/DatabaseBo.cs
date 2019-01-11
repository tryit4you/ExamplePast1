using ExamplePast1.Data.Dao;
using ExamplePast1.Data.Models;
using ExamplePast1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamplePast1.Data.Bo
{   public class DatabaseBo
    {
            DataBaseDao database = new DataBaseDao();

        public IEnumerable<DataListViewModel> getAll()
        {
            return database.GetListDataBase();            
        }
        public bool CreateDatabase(string dataName)
        {
            return database.CreateDatabase(dataName);
        }
        public bool CreateTable(string dataName, string tableName)
        {
            return database.CreateTable(dataName,tableName);
        }
        public IEnumerable<TableListViewModel> GetAllTable(string dataName)
        {
            return database.GetListTable(dataName);
        }
        public ImportResultViewModel ImportData(string tableName,string dataName, List<Map4D> map4Ds)
        {
            return database.ImportData(tableName,dataName,map4Ds);
        }
        public IEnumerable<Items> Fiters(string dataName,string tableName,double lat,double lng,double bk)
        {
            return database.Filter(dataName,tableName,lat,lng,bk);
        }
        public IEnumerable<Items> FitersCicle(string dataName,string tableName,double lat,double lng,double bk)
        {
            return database.FilterCircle(dataName,tableName,lat,lng,bk);
        }
        public bool DeleteDatabase(string databaseName)
        {
            return database.DeleteDatabase(databaseName);
        }
        public bool DeleteTable(string tableName,string dataName)
        {
            return database.DeleteTable(tableName,dataName);
        }
    }
}