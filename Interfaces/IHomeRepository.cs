
using System.Data;
using CineFlix.Models;

namespace CineFlix.Interfaces
{
    public interface IHomeRepository 
    {
         public string ValidateCredentials(string email, string password);

         public int RegisterUser(User userObj);

         public bool DeleteAccount(int userId);

         public List<Movie> GetMovies();

         public List<Tvshow> GetTvshows();

         public Movie GetMoviesDetailUsingMovieId(int movieId);

         public Tvshow GetTvShowsDetailUsingTvShowId(int tvShowId);

         public bool AddMovieDetails(Movie movieObj);

         public bool DeleteMovieDetails(int movieId);

         public bool AddTvShowDetails(Tvshow tvShowObj);

         public bool DeleteTvShowDetails(int tvShowId);

         public User GetUserDetailsUsingUserEmail(string userEmail);

         public string EditUserDetails(string firstName, string LastName, string phoneNumber, string password, string userEmail);

         public List<Movie> GetByMovieName(string moviename);
         
         public List<Tvshow> GetByTvShowName(string tvshowname);
    }
}