using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace CineFlix.Models;

public partial class Movie
{
    [DisplayName("Movie Id")]
    public int MovieId { get; set; }

    [Required]

    [DisplayName("User Id")]
    public int? UserId { get; set; }

    [Required]
    [DisplayName("Movie Name")]
    public string MovieName { get; set; } = null!;

    [Required]
    [DisplayName("Movie Description")]
    public string MovieDesc { get; set; } = null!;

    [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
    [Required]
    [StringLength(30)]
    public string Genres { get; set; } = null!;

    [Required]
    [DisplayName("Image Url")]
    public string ImageUrl { get; set; } = null!;

    [Required]
    [DisplayName("Video Url")]
    public string VideoUrl { get; set; } = null!;

    [Required]
    [DisplayName("Movie Language")]
    public string MovieLanguage { get; set; } = null!;

    [Required]
    public int Duration { get; set; }

    [DisplayName("Deleted")]
    public bool? IsDeleted { get; set; }

    public virtual User? User { get; set; }
}
