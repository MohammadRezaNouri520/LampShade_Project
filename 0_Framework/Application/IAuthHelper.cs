namespace _0_Framework.Application
{
    public interface IAuthHelper
    {
        void SignIn(AuthViewModel account);
        void SignOut();
        bool IsAuthenticated();
        AuthViewModel CurrentUserInfo();
        string CurrentUserRole();
    }
}
