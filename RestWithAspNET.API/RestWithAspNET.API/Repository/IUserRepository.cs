using RestWithAspNET.API.Data.VO;
using RestWithAspNET.API.Model;

namespace RestWithAspNET.API.Repository
{
    public interface IUserRepository
    {
        User ValidateCredentials(UserVO user);
        User ValidateCredentials(string username);
        bool RevokeToken(string username);
        User RefreshUserInfo(User user);
    }
}
