using ExamplePast1.Data;
using ExamplePast1.Data.Bo;
using ExamplePast1.Data.Models;
using ExamplePast1.Filter;
using ExamplePast1.Shared;
using ExamplePast1.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExamplePast1.Controllers
{
    [AuthFilter]
    public class TableController : Controller
    {
        private readonly DatabaseBo databaseBo = null;
        public TableController()
        {
            databaseBo = new DatabaseBo();
        }
        // GET: Table
        public ActionResult Index(string dataName)
        {
            ViewBag.dataName = dataName;
            return View();
        }

        [HttpPost]
        public JsonResult createTable(string dataName, string tableName)
        {
            bool status = false;
            var message = "";
            var result = databaseBo.CreateTable(dataName, tableName);
            if (result)
            {
                status = true;
                message = ResultState.Add_Success;
            }
            else
            {
                status = false;
                message = ResultState.Add_False;
            }
            return Json(new
            {
                status = status,
                message = message
            });
        }

        [HttpPost]
        public JsonResult ViewTable(string dataName)
        {
            var listTable = databaseBo.GetAllTable(dataName);
            if (listTable != null)
            {
                return Json(new
                {
                    status = true,
                    data = listTable
                });
            }
            else
            {
                return Json(new
                {
                    status = false,
                    data = ""
                });
            }
        }

        public JsonResult deleteTable(string tableName, string dataName)
        {
            var status = false;
            string message = string.Empty;
            var result = databaseBo.DeleteTable(tableName, dataName);
            if (result)
                status = true;
            else
                status = false;

            return Json(new
            {
                status = status
            });
        }

        public JsonResult importData(string tableName,string dataName, string fileUrl)
        {
            List<Map4D> data = null;
            ImportResultViewModel result =new ImportResultViewModel {
                TimeQuery=0,
                RowQuery=""
            };
            string message = "";
            var status = false;
            try
            {
                StreamReader sr = new StreamReader(fileUrl);
                string jsonString = sr.ReadToEnd();
                data = JsonConvert.DeserializeObject<List<Map4D>>(jsonString);
                status = true;
                result = databaseBo.ImportData(tableName, dataName, data);
            }
            catch(FileNotFoundException ex)
            {
                message = ex.Message;
                status = false;
            }
          
            
            return Json(new
            {
                status=status,
                message=message,
                timequery = result.TimeQuery,
                rowquery = result.RowQuery
            });

        }
        [HttpGet]
        public ActionResult filter(string data,string table)
        {
            ViewBag.dataName = data;
            ViewBag.tableName = table;
            return View();
        }

        public ActionResult SearchRecord(string dataName,string tableName, double lat=0, double lng=0, double bk=0)
        {

            //lấy ra tất cả danh sách
            var listResult = databaseBo.Fiters(dataName, tableName,lat,lng,bk);

            return Json(new
            {
                data=listResult
            });
        }
        
        public ActionResult SearchRecordCircle(string dataName,string tableName, double lat, double lng, double bk)
        {

            var listResult = databaseBo.FitersCicle(dataName,tableName,lat,lng,bk);
            return Json(new
            {
                data = listResult
            });
        }
    }
}