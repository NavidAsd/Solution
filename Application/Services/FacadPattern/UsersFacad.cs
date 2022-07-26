using Application.Interface.FacadPattern;
using Application.Services.Query.ReturnUsers;
using Application.Services.Query.UserLogin;
using Common;

namespace Application.Services.FacadPattern
{
    public class UsersFacad : IUsersFacad
    {
        private readonly DapperUtility _utility;
        public UsersFacad(DapperUtility utility)
        {
            _utility = utility;
        }
        private IUsersLoginService _UsersLogin;
        public IUsersLoginService UsersLoginService
        {
            get
            {
                return _UsersLogin = _UsersLogin ?? new UsersLoginService(_utility);
            }
        }
        private IReturnUsersDataService _ReturnUsers;
        public IReturnUsersDataService ReturnUsersDataService
        {
            get
            {
                return _ReturnUsers = _ReturnUsers ?? new ReturnUsersDataService(_utility);
            }
        }
    }
}
