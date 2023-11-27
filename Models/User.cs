using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace CineFlix.Models;

public partial class User
{
    public int UserId { get; set; }

    [Required(ErrorMessage = "First Name is required")]
    [DisplayName("First Name")]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "Last Name is required")]
    [DisplayName("Last Name")]
    public string LastName { get; set; } = null!;

    [Required(ErrorMessage = "Phone number is required")]
    [DisplayName("Phone Number")]
    [RegularExpression(@"[789][0-9]{9}", ErrorMessage ="Must start with 7 or 8 or 9 and must contain 10 digits")]
    public string? PhoneNumber { get; set; }

    [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Invalid email address.")]
    //[Required(ErrorMessage = "EmailId is mandatory.")]
    [DisplayName("Email Id")]
    public string? EmailId { get; set; }

    [StringLength(15, MinimumLength = 8)]
    [Required(ErrorMessage = "Password is mandatory.")]
    [RegularExpression(@"(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}", ErrorMessage = "Must contain at least one  number and one uppercase and lowercase letter, and at least 8 or more characters")]
    [DisplayName("User password")]
    public string? UserPassword { get; set; }

    public byte? RoleId { get; set; }

    [Required(ErrorMessage = "Plan Type is mandatory.")]
    [RegularExpression(@"Basic|Platinum|Gold")]
    public string PlanType { get; set; } = null!;

    public DateTime MembershipStartDate { get; set; }

    public DateTime MembershipEndDate { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();

    public virtual Role? Role { get; set; }

    public virtual ICollection<Tvshow> Tvshows { get; set; } = new List<Tvshow>();
}
