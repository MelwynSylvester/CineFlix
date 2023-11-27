using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CineFlix.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using CineFlix.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using CineFlix.Handlers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace CineFlix.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHomeRepository _repObj;

        private readonly JWTSettings _jwtSettings;

        public LoginController(IHomeRepository repos, IOptions<JWTSettings> options){
            _repObj = repos;
            _jwtSettings = options.Value;
        }

        public IActionResult Index(){        
            return View();
        }                      
        
        [HttpGet]
        public IActionResult CheckRole(User userObj)
        {
            string userId = userObj.EmailId;
            string password = userObj.UserPassword;
            HttpContext.Session.SetString("userID", userObj.UserId.ToString());
            HttpContext.Session.SetString("password", userObj.UserPassword.ToString());



            try
            {
                if (userId!=null && password!=null)
                {
                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Append("UserId", userId, option);
                Response.Cookies.Append("Password", password, option);
                }
                else
                {
                    return null;
                }

                string username = userId.Split('@')[0];

                string status = _repObj.ValidateCredentials(userId, password);

                if(status=="1" || status=="2")
                {
                    var accessToken = GenerateJSONWebToken(status);
                    SetJWTCookie(accessToken);
                }

                    if (status == "1")
                    {
                        return RedirectToAction("AdminHome", "Admin");
                    }
                    else if (status == "2")
                    {
                        return RedirectToAction("CustomerHome","Customer",username);
                    }
                    else if(status == "Your Account is Inactive.")
                    {
                        TempData["Message"] = status;
                    }
                    else if(status == "Membership Expired Please Renew to continue!...")
                    {
                        TempData["Message"] = status;
                    }
                    else
                    {
                        TempData["Message"] = "Login failed. User name or password supplied doesn't exist.";
                    }  
            }
            catch(Exception ex)
            {
                TempData["Message"] = "Login failed. Error - " + ex.Message;
            }
            return RedirectToAction("Index");                   
        }   

    public string GenerateJSONWebToken(string status)
    {
        string role = "";
        if(status=="1")
        {
            role = "Admin";
        }
        else if(status=="2")
        {
            role = "Customer";
        }
        else
        {
            role="";
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity
            (
                new Claim[]
                {
                    new Claim(ClaimTypes.Role, role),
                }
            ),
            Expires = DateTime.Now.AddMinutes(30),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),SecurityAlgorithms.HmacSha256)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        string finalToken = tokenHandler.WriteToken(token);

        return finalToken;
    }

    private void SetJWTCookie(string token)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddMinutes(30),
        };
        Response.Cookies.Append("jwtCookie", token, cookieOptions);
    }
   

        public IActionResult Logout()
        {
            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Delete("UserId",option);
            Response.Cookies.Delete("Password",option);
            Response.Cookies.Delete("jwtCookie",option);
            return RedirectToAction("Index","Home");
        } 
    }
}