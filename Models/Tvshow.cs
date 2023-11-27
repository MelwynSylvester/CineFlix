using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CineFlix.Models;

public partial class Tvshow
{
    public int ShowId { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public string Show { get; set; } = null!;

    [Required]
    public string ShowDesc { get; set; } = null!;

    [Required]
    public string Genres { get; set; } = null!;

    [Required]
    public string ImageUrl { get; set; } = null!;

    [Required]
    public string VideoUrl { get; set; } = null!;

    [Required]
    public string ShowLanguage { get; set; } = null!;

    [Required]
    public int EpisodeCount { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual User User { get; set; } = null!;
}
