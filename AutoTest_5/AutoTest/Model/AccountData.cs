namespace SeleniumTests;

public class AccountData
{
    public AccountData()
    {
        Username = "";
        Password = "";
        FirstName = "";
        LastName = "";
        Email = "";
        Address = "";
        City = "";
        State = "";
        Zip = "";
        Country = "";
        Subscribe = false;
    }

    public AccountData(string username, string password)
    {
        Username = username;
        Password = password;
        FirstName = "";
        LastName = "";
        Email = "";
        Address = "";
        City = "";
        State = "";
        Zip = "";
        Country = "";
        Subscribe = false;
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Zip { get; set; }
    public string Country { get; set; }
    public bool Subscribe { get; set; }
    
    public string Username { get; set; }
    public string Password { get; set; }
}