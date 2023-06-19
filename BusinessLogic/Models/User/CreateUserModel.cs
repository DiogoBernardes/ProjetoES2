namespace BusinessLogic.Models.User;

public class CreateUserModel
{
    public Guid ID { get; set; }
    public string Name { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public Guid Role_id { get; set; }

}