namespace Database.Common.Models
{
    public class TstModel : BaseModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Role { get; set; }
    }
}
