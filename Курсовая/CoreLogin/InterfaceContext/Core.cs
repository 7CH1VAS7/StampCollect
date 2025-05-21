

namespace Курсовая.CoreLogin.InterfaceContext
{
    public class Core : ICore
    {
        public string Generate(string password) => BCrypt.Net.BCrypt.EnhancedHashPassword(password);

        public bool Verify(string password, string hashPass) => BCrypt.Net.BCrypt.EnhancedVerify(password, hashPass);
    }
}
