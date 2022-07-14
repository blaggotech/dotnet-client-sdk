using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaggoBlackbox
{
    public class Options
    {
        public string AuthURL;
        public Credentials Credentials;
        public Authenticator AuthenticatorFn;
        public HttpClient HttpClient;
    }

    public class Credentials
    {
        public string Username;
        public string Password;
    }

    public delegate Task<AuthResponse> Authenticator(string url, Credentials creds);
}
