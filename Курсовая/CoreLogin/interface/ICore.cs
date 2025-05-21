
public interface ICore
{
    public string Generate(string password);
    public bool Verify(string password, string hashPass);

}
