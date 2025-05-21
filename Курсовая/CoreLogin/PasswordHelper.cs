namespace Курсовая.CoreLogin
{
    public class PasswordHelper
    {
        readonly ICore _passHasher;

        public PasswordHelper(ICore passHasher)
        {
            _passHasher = passHasher;
        }

        public async Task Register(string username, string password, string email)
        {
            var hasshedpass = _passHasher.Generate(password);

        }
    }
}
