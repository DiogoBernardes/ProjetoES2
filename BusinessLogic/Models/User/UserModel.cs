namespace BusinessLogic.Models.User;

public class UserModel
{
    public Guid ID { get; set; }
    public string Name { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public RoleModel Role { get; set; } 
    
}