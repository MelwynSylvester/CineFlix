using Microsoft.Data.SqlClient;
using CineFlix.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Azure.Core;


namespace CineFlix.Models
{
    public class HomeRepository : IHomeRepository
    {
        private readonly CineFlixDbContext _context;

        public HomeRepository(CineFlixDbContext cineFlixDbContext)
        {
            this._context = cineFlixDbContext;
        }


        public int RegisterUser(User userObj)
        {
            int userId = 0;
            int result = -1;
            int returnResult = 0;
            string membershipEndDate = "";
            string membershipStartDate = DateTime.Now.ToString("yyyy-MM-dd");
            if (userObj.PlanType == "Basic")
            {
                membershipEndDate = DateTime.Now.AddDays(30).ToString("yyyy-MM-dd");
            }
            else if (userObj.PlanType == "Gold")
            {
                membershipEndDate = DateTime.Now.AddDays(90).ToString("yyyy-MM-dd");
            }
            else if (userObj.PlanType == "Platinum")
            {
                membershipEndDate = DateTime.Now.AddDays(180).ToString("yyyy-MM-dd");
            }

            try
            {
                SqlParameter prmFirstName = new SqlParameter("@FirstName", userObj.FirstName);
                SqlParameter prmLastName = new SqlParameter("@LastName", userObj.LastName);
                SqlParameter prmPhoneNumber = new SqlParameter("@PhoneNumber", userObj.PhoneNumber);
                SqlParameter prmEmailId = new SqlParameter("@EmailId", userObj.EmailId);
                SqlParameter prmPassword = new SqlParameter("@UserPassword", userObj.UserPassword);
                SqlParameter prmPlanType = new SqlParameter("@PlanType", userObj.PlanType);
                SqlParameter prmMembershipStartDate = new SqlParameter("@MembershipStartDate", membershipStartDate);
                SqlParameter prmMembershipEndDate = new SqlParameter("@MembershipEndDate", membershipEndDate);
                SqlParameter prmUserId = new SqlParameter("@UserId", System.Data.SqlDbType.BigInt);
                prmUserId.Direction = System.Data.ParameterDirection.Output;
                SqlParameter prmReturnResult = new SqlParameter("@Result", System.Data.SqlDbType.BigInt);
                prmReturnResult.Direction = System.Data.ParameterDirection.Output;

                result = _context.Database.ExecuteSqlRaw("EXEC dbo.usp_RegisterUser @FirstName,@LastName,@PhoneNumber,@EmailId,@UserPassword,@PlanType,@MembershipStartDate,@MembershipEndDate,@UserId OUT,@Result OUT", new[]
                { prmFirstName, prmLastName,prmPhoneNumber, prmEmailId, prmPassword, prmPlanType, prmMembershipStartDate,prmMembershipEndDate,prmUserId,prmReturnResult});
                _context.SaveChanges();
                returnResult = Convert.ToInt32(prmReturnResult.Value);

                if (returnResult > 0)
                {
                    userId = Convert.ToInt32(prmUserId.Value);
                }
                else
                {
                    userId = 0;
                }

            }
            catch (Exception ex)
            {
                userId = 0;
                result = -99;
                returnResult = -99;
            }
            return returnResult;

        }

        public bool DeleteAccount(int userId)
        {
            bool status = false;
            try
            {
                if(userId==null || userId==0)
                {
                    status = true;
                }
                else{
                var userlist = _context.Users.Find(userId);
                userlist.IsDeleted = true;
                _context.Users.Update(userlist);
                _context.SaveChanges();
                status = true;
                }
            }
            catch (Exception ex)
            {
                status = false;
            }
            return status;
        }

        public string ValidateCredentials(string email, string password)
        {
            //int roleId = 0;
            string status = "";
            try
            {
               var userDetails = _context.Users.Where(x => x.EmailId == email && x.UserPassword == password).ToList();

                if (userDetails.Count == 1)
                {
                    if(userDetails[0].IsDeleted==true)
                    {
                        status = "Your Account is Inactive.";
                    }
                    else if(userDetails[0].UserPassword== password && userDetails[0].MembershipEndDate.Date<DateTime.Today)
                    {
                        status = "Membership Expired Please Renew to continue!...";
                    }
                    else if(userDetails[0].UserPassword!=password)
                    {
                        status = "Either username or password is incorrect!.";
                    }
                    else
                    {
                        status = Convert.ToInt32(userDetails[0].RoleId).ToString();
                    }           
                }
                else{
                    status="0";
                }

            }
            catch
            {
                status = "0";
            }
            return status;
        }

        public List<Movie> GetMovies()
        {
            try
            {
                return _context.Movies.ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<Tvshow> GetTvshows()
        {
            try
            {
                return _context.Tvshows.ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Movie GetMoviesDetailUsingMovieId(int movieId)
        {
            try
            {
                return _context.Movies.Find(movieId);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Tvshow GetTvShowsDetailUsingTvShowId(int tvShowId)
        {
            try
            {
                return _context.Tvshows.Find(tvShowId);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool AddMovieDetails(Movie movieObj)
        {
            bool status;
            try
            {
                _context.Movies.Add(movieObj);
                _context.SaveChanges();
                status = true;
            }
            catch (Exception ex)
            {
                status = false;
            }
            return status;
        }

        public bool DeleteMovieDetails(int movieId)
        {
            bool status = false;

            var movie_list = _context.Movies.Find(movieId);
            try
            {
                if (movie_list != null)
                {
                    movie_list.IsDeleted = true;
                    _context.Movies.Update(movie_list);
                    _context.SaveChanges();
                    //status="Deleted Successfully";
                    status = true;
                }
            }
            catch (Exception ex)
            {
                status = false;
            }
            return status;
        }

        public bool AddTvShowDetails(Tvshow tvShowObj)
        {
            bool status;
            try
            {
                _context.Tvshows.Add(tvShowObj);
                _context.SaveChanges();
                status = true;
            }
            catch (Exception ex)
            {
                status = false;
            }
            return status;
        }

        public bool DeleteTvShowDetails(int showId)
        {
            bool status = false;

            var tvShow_list = _context.Tvshows.Find(showId);
            try
            {
                if (tvShow_list != null)
                {
                    tvShow_list.IsDeleted = true;
                    _context.Tvshows.Update(tvShow_list);
                    _context.SaveChanges();
                    status = true;
                }
            }
            catch (Exception ex)
            {
                status = false;
            }
            return status;
        }

        public User GetUserDetailsUsingUserEmail(string userEmail)
        {
            try
            {
                return _context.Users.Where(u => u.EmailId == userEmail).FirstOrDefault();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public string EditUserDetails(string firstName, string lastName, string phoneNumber, string password, string userEmail)
        {
            string status="User not found";
            List<User> userdt;

            try
            {
                List<User> userlist = _context.Users.Where(u=> u.EmailId==userEmail).ToList();
                userdt = _context.Users.Where(x => x.EmailId!=userEmail && x.PhoneNumber == phoneNumber).ToList();
                if (userdt.Count == 1)
                {
                    status = "Phone number exists!.. Please try a new one";
                }
                else if (userlist != null && userdt.Count == 0)
                {
                    userlist[0].FirstName = firstName;
                    userlist[0].LastName = lastName;
                    userlist[0].PhoneNumber = phoneNumber;
                    userlist[0].UserPassword = password;
                    _context.Users.UpdateRange(userlist);
                    _context.SaveChanges();
                    status = "Saved Successfully";
                }
            }
            catch (Exception ex)
            {
                status = "Error";
            }
            return status;

        }

        public List<Tvshow> GetByTvShowName(string tvshowname)
        {
            List<Tvshow> tvshow;
            string pattern=tvshowname+"%";
            try
            {
                tvshow =_context.Tvshows.Where(a=> EF.Functions.Like(a.Show, pattern)
                                                               || EF.Functions.Like(a.Genres,pattern)).ToList();
            }
            catch(Exception ex)
            {
                 tvshow=null;
            }
            return tvshow;
        }

        public List<Movie> GetByMovieName(string moviename)
        {
            List<Movie> movies;
            string pattern=moviename+"%";
            try
            {
                movies =_context.Movies.Where(a=> EF.Functions.Like(a.MovieName, pattern)
                                                               || EF.Functions.Like(a.Genres,pattern)).ToList();
               
 
            }
            catch(Exception ex)
            {
                 movies=null;
            }
            return movies;
        }
    }
}