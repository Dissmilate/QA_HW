namespace SeleniumTests
{
    public class AuthBase : TestBase
    {
        [SetUp]
        public void SetupAuth()
        {
            app.Auth.Login(new AccountData(Settings.Login, Settings.Password));
        }
    }
}