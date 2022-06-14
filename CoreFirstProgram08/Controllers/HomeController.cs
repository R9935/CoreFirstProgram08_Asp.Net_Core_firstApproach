using CoreFirstProgram08.DB_Content;
using CoreFirstProgram08.Migrations;
using CoreFirstProgram08.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CoreFirstProgram08.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly Add_G _db;

        public HomeController(Add_G Db)
        {
            _db = Db;
        }
       
        public IActionResult Index()
        {
            var res = _db.Employees.ToList();

            return View(res);
        }
        [HttpGet]
        
        public IActionResult Join()
        {
            return View();
        }
        [HttpPost]
 
        public IActionResult Join(Employee emp)
        {
           
            

                if (emp.ID == 0)
                {
                    _db.Employees.Add(emp);

                    _db.SaveChanges();
                }
                else
                {
                    _db.Employees.Update(emp);
                    _db.SaveChanges();
                }

                return RedirectToAction("Index");
            



            return View();
        }
      
        public IActionResult Edit(int ID)
        {
            var edit = _db.Employees.Where(m => m.ID == ID).First();
            ViewBag.ID = edit.ID;
            _db.SaveChanges();


            return View("Join",edit);
        }


      
        public IActionResult Delete(int ID)
        {
            var delete = _db.Employees.Where(m => m.ID == ID).First();
            _db.Employees.Remove(delete);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(UserInfo ur)
        {
            var res= _db.Users.Where(c=>c.UserName==ur.UserName).FirstOrDefault();
            if (res == null)
            {
                TempData["t"] = "Email is Not Found";
            }
            else
            {
                if(res.UserName==ur.UserName && res.Password == ur.Password)
                {
                    var claims = new[] {new Claim(ClaimTypes.Name,res.UserName),
                    new Claim(ClaimTypes.Email,res.Password)};

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var AuthProperties = new AuthenticationProperties
                    {
                        IsPersistent = false
                    };

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,new ClaimsPrincipal(identity),AuthProperties);

                    //Apply Session.....

                    HttpContext.Session.SetString("Name", res.UserName);
                    HttpContext.Session.SetString("Password", res.Password);

                    return RedirectToAction("Index");

                }
                else
                {
                    TempData["wrong"] = "PassWord inccorect ";
                    return View();
                }
            }




            return View();



           
           
        }
        
        public IActionResult LogOut()
        {

            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("Name");
            return RedirectToAction("Login");
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult SignUp(UserInfo ur)
        {

           
            if (ur.ID == 0)
            {


                _db.Users.Add(ur);
                _db.SaveChanges();
                ViewBag.msg = "Successfull Register User..";
            }
            else
            {
                _db.Users.Update(ur);
                _db.SaveChanges();
            }


            return RedirectToAction("UserList");
        }


        public IActionResult UserList()
        {
            var r = _db.Users.ToList();

            return View(r);
        }

        public IActionResult UDelete(int ID)
        {
            var dele = _db.Users.Where(m => m.ID == ID).First();
            _db.Users.Remove(dele);
            _db.SaveChanges();

            return RedirectToAction("UserList");
        }


        public IActionResult UEdit(int ID)
        {
            var edit = _db.Users.Where(m => m.ID == ID).First();
            ViewBag.t=edit.ID;

            _db.SaveChanges();


            return View("SignUp", edit);
        }

      

       
    }
}
