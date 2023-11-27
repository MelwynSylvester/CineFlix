using System.Net;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CineFlix.Models;
using CineFlix.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Net.Http.Headers;

namespace CineFlix.Controllers
{
public class HomeController : Controller
{
    private readonly IHomeRepository _repObj;

    public HomeController(IHomeRepository repos)
    {
       _repObj = repos;
    }

    public IActionResult Index()
    {
        // using Microsoft.Net.Http.Headers
        
        return View();
    }

    public IActionResult AboutUs()
    {
        return View();
    }
}
}
