using System;
using System.Collections.Generic;

namespace CineFlix.Models;

public partial class Role
{
    public byte RoleId { get; set; }

    public string? RoleName { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
