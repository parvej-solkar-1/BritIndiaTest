namespace Products.DataAccess.Entities;

public partial class User
{
    public string UserId { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Role { get; set; } = null!;
}
