using Common;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.Query.UserLogin
{
    public interface IUsersLoginService
    {
        Task<ResultMessage> ExecuteAsynce(RequestUsersDto request);
    }
    public class RequestUsersDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class UsersLoginService : IUsersLoginService
    {
        private readonly DapperUtility _utility;
        public UsersLoginService(DapperUtility utility)
        {
            _utility = utility;
        }
        async Task<ResultMessage> IUsersLoginService.ExecuteAsynce(RequestUsersDto request)
        {
            string query = "SP_UserLogin";
            try
            {
                using (var connection = _utility.GetConnection())
                {
                    //request.Password = ReturnPasswordHashed(request.Password); // password Must Be hashed
                    var result = await connection.QueryAsync(query, request, commandType: System.Data.CommandType.StoredProcedure);
                    foreach (var item in result.First())
                    {
                        if (((KeyValuePair<string, object>)item).Value.ToString() == "true")
                        {
                            connection.Close();
                            return new ResultMessage
                            {
                                Success = true
                            };
                        }
                    }
                    connection.Close();
                    return new ResultMessage
                    {
                        Success = false
                    };

                }
            }
            catch
            {
                return new ResultMessage
                {
                    Success = false,
                    Message = ""
                };
            }
        }
        /*private bool ReturnPasswordHashed(string Password,string Hashed)
        {
            Common.PasswordHasher Hasher = new PasswordHasher();
            return Hasher.VerifyPassword(Hashed,Password);
        }*/
    }
}
