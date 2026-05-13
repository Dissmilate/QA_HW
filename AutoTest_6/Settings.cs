using System.Xml;

namespace SeleniumTests
{
    public static class Settings
    {
        public static string file = "Settings.xml";
        private static XmlDocument document;
        private static string baseURL;
        private static string login;
        private static string password;

        static Settings()
        {
            if (!System.IO.File.Exists(file))
            {
                throw new Exception("Problem: settings file not found: " + file);
            }
            document = new XmlDocument();
            document.Load(file);
        }

        public static string BaseURL
        {
            get
            {
                if (baseURL == null)
                {
                    XmlNode node = document.DocumentElement.SelectSingleNode("BaseUrl");
                    baseURL = node.InnerText;
                }
                return baseURL;
            }
        }

        public static string Login
        {
            get
            {
                if (login == null)
                {
                    XmlNode node = document.DocumentElement.SelectSingleNode("Login");
                    login = node.InnerText;
                }
                return login;
            }
        }

        public static string Password
        {
            get
            {
                if (password == null)
                {
                    XmlNode node = document.DocumentElement.SelectSingleNode("Password");
                    password = node.InnerText;
                }
                return password;
            }
        }
    }
}