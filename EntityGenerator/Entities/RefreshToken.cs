using System;
using System.Collections.Generic;

namespace EntityGenerator.Entities;

public partial class RefreshToken
{
    public int Id { get; set; }

    public string UserId { get; set; } = null!;

    public string Token { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual User User { get; set; } = null!;
}
