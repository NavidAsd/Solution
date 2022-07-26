using Application.Services.Query.ReturnUsers;
using Application.Services.Query.UserLogin;

namespace Application.Interface.FacadPattern
{
    public interface IUsersFacad
    {
        IUsersLoginService UsersLoginService { get; }
        IReturnUsersDataService ReturnUsersDataService { get; }
    }
}
