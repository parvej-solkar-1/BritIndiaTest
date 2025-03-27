using System;
using System.Collections.Generic;

namespace EntityGenerator.Entities;

public partial class User
{
    public string UserId { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Role { get; set; } = null!;

    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
}
