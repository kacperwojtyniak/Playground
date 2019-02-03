namespace Database.Common.Models
{
    public class UserModel : BaseModel
    {
        public string UserLogin { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Role { get; set; }
    }
}

