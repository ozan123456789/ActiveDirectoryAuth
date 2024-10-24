using System.DirectoryServices.Protocols;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ActiveDirectoryAuth.Controllers
{
    public class LdapController : Controller
    {
        private const string LdapServer = "ldap://your-ldap-server";
        private const string Domain = "yourdomain.com";
        private const string Username = "your-username";
        private const string Password = "your-password";

        public IActionResult Index()
        {
            try
            {
                // LDAP Bağlantısını Yapılandır
                var ldapConnection = new LdapConnection(LdapServer);
                var credentials = new NetworkCredential(Username, Password, Domain);
                ldapConnection.Credential = credentials;
                ldapConnection.Bind();

                // Kullanıcı bilgilerini çekme
                var searchRequest = new SearchRequest(
                    "DC=yourdomain,DC=com",
                    "(sAMAccountName=your-username)",
                    SearchScope.Subtree,
                    "cn", "mail", "displayName"
                );

                var searchResponse = (SearchResponse)ldapConnection.SendRequest(searchRequest);

                var userAttributes = searchResponse.Entries[0].Attributes;

                var userInfo = new
                {
                    Name = userAttributes["displayName"][0].ToString(),
                    Email = userAttributes["mail"][0].ToString()
                };

                return View(userInfo);
            }
            catch (Exception ex)
            {
                // Hata durumunda hata mesajı gönder
                return View("Error", ex.Message);
            }
        }
    }
}
