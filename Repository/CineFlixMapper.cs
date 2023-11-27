using AutoMapper;
using CineFlix.Models;

public class CineFlixMapper:Profile
    {
        public CineFlixMapper()
        {
            CreateMap<Movie, CineFlix.Models.Movie>();
        }
    }