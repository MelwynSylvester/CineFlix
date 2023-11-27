using System.Net;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CineFlix.Models;
using CineFlix.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Net.Http.Headers;
using MessagePack.Resolvers;
using CineFlix.Handlers;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace CineFlix.Controllers
{
public class SecurityController: Controller
{
    private readonly CineFlixDbContext _cineObj;    
    private readonly JWTSettings _jwtSettings;
    public SecurityController(CineFlixDbContext _context, IOptions<JWTSettings> options)
    {
        _cineObj = _context;
        _jwtSettings = options.Value;
    }

    // [AllowAnonymous]
    // [HttpPost("GenerateJSONWebToken")]
    public string GenerateJSONWebToken(string emailId, string password)
    {
        var users = _cineObj.Users.FirstOrDefault(u=> u.EmailId==emailId && u.UserPassword==password);
        // if(users==null)
        // {
        //     return Unauthorized();
        // }

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity
            (
                new Claim[]
                {
                    new Claim(ClaimTypes.Name, users.EmailId),
                }
            ),
            Expires = DateTime.Now.AddSeconds(60),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),SecurityAlgorithms.HmacSha256)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        string finalToken = tokenHandler.WriteToken(token);

        return finalToken;
    }

    [HttpGet]
    public string Get()
    {
        return GenerateJSONWebToken("damonsalvatore@yahoo.com","Damon@123");
    }
}
}