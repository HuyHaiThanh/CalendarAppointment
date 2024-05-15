namespace DTO
{
    public class DTO_Account
    {
        private int _id;
        private string _username;
        private string _password;

        public int Id { get => _id; set => _id = value; }
        public string Username { get => _username; set => _username = value; }
        public string Password { get => _password; set => _password = value; }

        public DTO_Account()
        {

        }

    }
}
