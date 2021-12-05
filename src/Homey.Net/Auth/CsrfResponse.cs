namespace Homey.Net.Auth.Auth
{
    internal class CsrfResponse
    {
        internal string Csrf
        {
            get; set;
        }

        internal string Cookie
        {
            get; set;
        }
    }
}
