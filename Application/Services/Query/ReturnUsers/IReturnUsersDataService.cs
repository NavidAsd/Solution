using Common;
using System.Collections.Generic;
using Domain.Entities;
using Dapper;
using System.Threading.Tasks;
using System.Linq;

namespace Application.Services.Query.ReturnUsers
{
    public interface IReturnUsersDataService
    {
        Task<ResultMessage<ResultUsersDataDto>> GetUsersAsynce();
    }
    public class ResultUsersDataDto
    {
        public List<Users> Users { set; get; }
    }
    public class ReturnUsersDataService : IReturnUsersDataService
    {
        private readonly DapperUtility _utility;
        public ReturnUsersDataService(DapperUtility utility)
        {
            _utility = utility;
        }
        async Task<ResultMessage<ResultUsersDataDto>> IReturnUsersDataService.GetUsersAsynce()
        {
            string query = "SP_GetUsers";
            //ToDo: Add Pagination to Response
            using (var connection = _utility.GetConnection())
            {
                var result = await connection.QueryAsync<Users>(query, commandType: System.Data.CommandType.StoredProcedure);
                return new ResultMessage<ResultUsersDataDto>
                {
                    Success = result.Count() > 0 ? true : false,
                    Enything = new ResultUsersDataDto() { Users = result.ToList() }
                };
            }
        }

    }
}
