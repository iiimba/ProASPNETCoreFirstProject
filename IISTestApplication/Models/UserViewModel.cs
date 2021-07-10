namespace IISTestApplication.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }
    }

    public class CreateUserViewModel
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }

    public class UpdateUserViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }

    public class LoginUserViewModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
