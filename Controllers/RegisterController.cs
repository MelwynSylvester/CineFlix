using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CineFlix.Interfaces;
using CineFlix.Models;
using Microsoft.AspNetCore.Mvc;

namespace CineFlix.Controllers
{
    public class RegisterController : Controller
    {
        
        private readonly IHomeRepository _repObj;

        public RegisterController(IHomeRepository repos){
            _repObj = repos;
        }

        public IActionResult Index(){        
            return View();
        }

        [HttpPost]
        public IActionResult CreateUser(User userObj)
        {
            
            int result = _repObj.RegisterUser(userObj);
            if (result == 1)
            {
                //Console.WriteLine("User details added successfully! UserId = {0}\n", userId);
                return RedirectToAction("Index", "Login");
            }
            else if (result == -1)
            {
                return Ok("Provide a valid email. \n");
            }
            else if (result == -2)
            {
                return Ok("Provide a valid password\n");
            }
            else if (result == -3)
            {
                return Ok("Provide a valid planType\n");
            }
            else if (result == -4)
            {
                return Ok("Provide a valid MembershipStartDate\n");
            }
            else if (result == -5)
            {
                return Ok("Provide a valid MembershipEndDate\n");
            }
            else if (result == -6)
            {
                return Ok("Provide a valid first name\n");
            }
            else if (result == -7)
            {
                return Ok("Provide a valid last name\n");
            }
            else if (result == -8)
            {
                return Ok("Provide a valid password\n");
            }
            else
            {
                return Ok("Some error occurred! Please try again later.\n");
            }    
           
        }

        [HttpPut(Name="DeleteUserAccount")]
        public IActionResult DeleteUserAccount(int userId)
        {
           string status="";
            try
            {
            bool result = _repObj.DeleteAccount(userId);
            if(result)
            {
                status="Account is deleted";
            }
            }
            catch (Exception ex)
            {
                 throw new Exception("", ex);
            }
            return Ok(status);
        }

    }
}