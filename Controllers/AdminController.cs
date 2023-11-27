using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using CineFlix.Interfaces;
using CineFlix.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CineFlix.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IHomeRepository _repObj;

        private readonly IMapper _mapper;

        public AdminController(IHomeRepository repos,IMapper mapper)
        {
            _repObj = repos;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult AddMovieDetails()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddMovieDetails(Movie movieObj)
        {
            try
            {
                
                if(_repObj.AddMovieDetails(movieObj))
                {
                    return RedirectToAction("Movies","Admin");
                }            
            }
            catch(Exception ex)
            {
                return RedirectToAction("AddMovieDetails","Admin");
            }
            return RedirectToAction();
        }

        public string AddProduct()
        {
            return "This is Admin Home Page";
        }

        // public async Task<IActionResult> AdminHome()
        // {
        //     var jwt = Request.Cookies["jwtCookie"];

        //     using (var httpClient = new HttpClient())
        //     {
        //         httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
        //         using (var response = await httpClient.GetAsync("https://localhost:7056/Admin/AdminHome")) 
        //         {
        //             if (response.StatusCode == System.Net.HttpStatusCode.OK)
        //             {
        //                 return View();
        //             }
 
        //             if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        //             {
        //                 return RedirectToAction("Index", "Login");
        //             }
        //             return View();
        //         }
        //     }
        // }

        public IActionResult AdminHome()
        {
            return View();
        }

        public IActionResult Profile(){
            return View();
        }

        public IActionResult Movies()
        {
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
        public IActionResult Movies(string moviename)
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

        public IActionResult Tvshows()
        {
            var lstEntityMovies = _repObj.GetTvshows();

            List<Models.Tvshow> lstModelTvShow = new List<Models.Tvshow>();

            foreach (var tvshows in lstEntityMovies)
            {
                if(tvshows.IsDeleted!=true){
                   lstModelTvShow.Add(_mapper.Map<Models.Tvshow>(tvshows));
                }
            }
            return View(lstModelTvShow);
        }

        [HttpGet]
        public IActionResult TvShows(string tvshowname)
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
        public IActionResult MovieDetails(int id)
        {
             var entityMovieDetail = _repObj.GetMoviesDetailUsingMovieId(id);

            return View(entityMovieDetail);
        }

        [HttpGet]
        public IActionResult TvShowDetails(int id)
        {
             var entityTvShowDetail = _repObj.GetTvShowsDetailUsingTvShowId(id);

            return View(entityTvShowDetail);
        }

        [HttpGet]
        public IActionResult DeleteMovieDetails(int id)
        {
            var entityMovieDetail = _repObj.GetMoviesDetailUsingMovieId(id);
            return View(entityMovieDetail);
        }

        [HttpPost]
        public IActionResult SaveMovieDeletion(int movieId)
        {
            try{         
                if(_repObj.DeleteMovieDetails(movieId))
                    {
                        return RedirectToAction("Movies","Admin");
                    }     
                    else
                    {
                        return RedirectToAction("DeleteMovieDetails","Admin");
                    }       
                }
            catch(Exception ex)
            {
                return RedirectToAction("DeleteMovieDetails","Admin");
            }
            
        }

        [HttpGet]
        public IActionResult AddTvShowDetails()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddTvShowDetails(Tvshow tvShowObj)
        {
            try
            {          
                if(_repObj.AddTvShowDetails(tvShowObj))
                {
                    return RedirectToAction("Tvshows","Admin");
                }            
            }
            catch(Exception ex)
            {
                return RedirectToAction("AddMovieDetails","Admin");
            }
            return RedirectToAction();
        }

        [HttpGet]
        public IActionResult DeleteTvShowDetails(int id)
        {
            var entityTvShowDetail = _repObj.GetTvShowsDetailUsingTvShowId(id);
            return View(entityTvShowDetail);
        }

        [HttpPost]
        public IActionResult SaveTvShowDeletion(int tvShowId)
        {
            try{         
                if(_repObj.DeleteTvShowDetails(tvShowId))
                    {
                        return RedirectToAction("TvShows","Admin");
                    }     
                    else
                    {
                        return RedirectToAction("DeleteTvShowDetails","Admin");
                    }       
                }
            catch(Exception ex)
            {
                return RedirectToAction("DeleteTvShowDetails","Admin");
            }
            
        }
    }
}