using ExamplePast1.Data;
using ExamplePast1.Data.Bo;
using ExamplePast1.Filter;
using ExamplePast1.Shared;
using ExamplePast1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExamplePast1.Controllers
{
    [AuthFilter]
    public class HomeController : Controller
    {
        private DatabaseBo databaseBo = null;
        public HomeController()
        {
            databaseBo = new DatabaseBo();
        }
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetAllDatabase()
        {
            IEnumerable<DataListViewModel> list = databaseBo.getAll();
            return Json(new
            {
                data = list
            });
        }
        [HttpPost]
        public JsonResult CreateDatabase(string dataName)
        {
            bool status = false;
            var message = "";
            var result = databaseBo.CreateDatabase(dataName);
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
        public JsonResult deleteDatabase(string dataName)
        {
            var status = false;
            string message = string.Empty;
            var result = databaseBo.DeleteDatabase(dataName);
            if (result)
                status = true;
            else
                status = false;

            return Json(new
            {
                status = status
            });
        }

    }
}