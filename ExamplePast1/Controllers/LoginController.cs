using ExamplePast1.Data;
using ExamplePast1.Shared;
using ExamplePast1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExamplePast1.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            ConnectDatabase conn = null;
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }
            else
            {
                conn = new ConnectDatabase(model.ServerName, model.UserId, model.Password);
                var connectResult = conn.Connect();
                if (connectResult)
                {
                    Session[Keys.session_Login] = model;
                    return Redirect("/home/index");
                }
                else
                {
                    ViewBag.error = "Đăng nhập không thành công!";
                    return View("Index", model);
                }
            }
          
        }
    }
}