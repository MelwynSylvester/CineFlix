using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CineFlix.Interfaces;
using CineFlix.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace CineFlix.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IHomeRepository _repObj;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        public CustomerController(IHomeRepository repos,IMapper mapper,IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
            _repObj = repos;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CustomerHome()
        {
            return View();
        }

        [HttpGet]
        public IActionResult EditUserDetails(){
            string userEmail= Request.Cookies["UserId"];
            var userDetails = _repObj.GetUserDetailsUsingUserEmail(userEmail);
            return View(userDetails);
        }

        [HttpPost]
        public IActionResult EditUserDetails(User userObj){
            string status = "";
            string userEmail= Request.Cookies["UserId"];
            var userDetails = _repObj.EditUserDetails(userObj.FirstName, userObj.LastName, userObj.PhoneNumber, userObj.UserPassword,userEmail);
            try{
                if(userDetails=="Saved Successfully")
                {
                    ViewBag.SuccessMessage = "User has been updated successfully!";
                }
                else
                {
                ViewBag.ErrorMessage = "There has been some issue. Try some other time."; 
                }                 
            }
            catch(Exception ex)
            {
                status = ex.Message;
            }
            return View(userObj);
        }

        [HttpGet]
        public IActionResult DeleteUserAccount(User userObj)
        {
            string userEmail= Request.Cookies["UserId"];
            var user = _repObj.GetUserDetailsUsingUserEmail(userEmail);
            return View(user);
        }

        [HttpPost]
        public IActionResult DeleteAccount(User userObj)
        {
            string userEmail= Request.Cookies["UserId"];
            var user = _repObj.GetUserDetailsUsingUserEmail(userEmail);
            var userAccount = _repObj.DeleteAccount(user.UserId);
            
            if(userAccount)
            {
                Response.Cookies.Delete("UserId");
                Response.Cookies.Delete("Password");
                return RedirectToAction("Index","Login");
            }
            else
            {
                return RedirectToAction("DeleteUserAccount","Customer");
            }
        }

        public IActionResult ViewMovies(){
            var lstEntityMovies = _repObj.GetMovies();

            List<Models.Movie> lstModelMovies = new List<Models.Movie>();

            foreach (var movie in lstEntityMovies)
            {
                if(movie.IsDeleted!=true){
                   lstModelMovies.Add(_mapper.Map<Models.Movie>(movie));
                }
            }
            return View(lstModelMovies);
        }

        [HttpGet]
        public IActionResult ViewMovies(string moviename)
        {
            var movie=_repObj.GetByMovieName(moviename);
            if(movie.Count==0)
            {
                   ViewBag.ErrorMessage="No Movie Found";
                    return View(movie);
            }
            else
            {
                  ViewBag.SuccessMessage = movie.Count+" result found";
                  return View(movie);
            }          
        }

        public IActionResult ViewTvShows(){
            var lstEntityTvShows = _repObj.GetTvshows();

            List<Models.Tvshow> lstModelTVShow = new List<Models.Tvshow>();

            foreach (var tvShow in lstEntityTvShows)
            {
                if(tvShow.IsDeleted!=true){
                   lstModelTVShow.Add(_mapper.Map<Models.Tvshow>(tvShow));
                }
            }

            return View(lstModelTVShow);
        }

        [HttpGet]
        public IActionResult ViewTvShows(string tvshowname)
        {
            var tvshow=_repObj.GetByTvShowName(tvshowname);
            if(tvshow.Count==0)
            {      
                    ViewBag.ErrorMessage="Not Found";
                    return View(tvshow);
            }
            else
            {
                  ViewBag.SuccessMessage = tvshow.Count+" result found";
                  return View(tvshow);
            }
           
        }

        [HttpGet]
        public IActionResult MoviesDetail(int id)
        {
             var entityMovieDetail = _repObj.GetMoviesDetailUsingMovieId(id);

            return View(entityMovieDetail);
        }

        [HttpGet]
        public IActionResult TvShowsDetail(int id)
        {
             var entityTvShowDetail = _repObj.GetTvShowsDetailUsingTvShowId(id);

            return View(entityTvShowDetail);
        }

    }
}