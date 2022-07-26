using Application.Interface.FacadPattern;
using Endpoint.Controllers;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using Xunit;
using Application.Services.Query.UserLogin;
using System.Collections.Generic;
using Common;
using System.Data.SqlClient;
using Dapper;
using System.Linq;
using Domain.Entities;

namespace Solution_Test
{
    public class User_UnitTesting : IDisposable
    {
        private readonly string Connection = @"Data Source=.; Initial Catalog=DbTest; Integrated Security=True; ";
        /* private readonly IConfiguration _configuration;
         private readonly IUsersFacad _userFacad;

         public User_UnitTesting(IUsersFacad userFacad, IConfiguration configuration)
         {
             _userFacad = userFacad;
             _configuration = configuration;
         }*/

        [Fact]
        public async void UnitTest_UserLogin() //Injection is fake so 'IUsersLogin.ExecuteAsynce(...)' Not Working
        {
            var config = A.Fake<IConfiguration>();
            var userFacad = A.Fake<IUsersFacad>();
            var controller = new UserController(userFacad, config);
            var model = new Endpoint.Models.UserLoginViewModel()
            {
                UserName = "navid",
                Password = "963963"
            };// currect user data
            var result = await controller.Login(model);
            Assert.IsType<OkObjectResult>(result);
        }
        
        [Fact]
        public async void UnitTest_UserInfo()
        {
            var config = A.Fake<IConfiguration>();
            var userFacad = A.Fake<IUsersFacad>();
            var controller = new UserController(userFacad, config);
            var result = await controller.UsersInfo();
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void UnitTest_GetUserInfoService()
        {
            //Service: Application.Services.Query.ReturnUser\\ 
            ResultMessage Out = new ResultMessage() { Success = false };
            string query = "SP_GetUsers";
            //ToDo: Add Pagination to Response
            using (var connection = GetNewConnection())
            {
                connection.Open();
                var result = await connection.QueryAsync<Users>(query, commandType: System.Data.CommandType.StoredProcedure);
                Out.Success = result.Count() > 0 ? true : false;
            }
            Assert.Equal(Out.Success, true);
        }

        [Fact]
        public async void UnitTest_LoginService()
        {
            /*var userFacad = A.Fake<IUsersFacad>();
            RequestUsersDto users = new RequestUsersDto()
            {
                UserName = "navid",
                Password = "963963"
            };
            var resultt = await userFacad.UsersLoginService.ExecuteAsynce(users);*/

            //Service: Application.Services.Query.UserLogin\\
            ResultMessage Out = new ResultMessage() { Success = false };
            string query = "SP_UserLogin";
            RequestUsersDto request = new RequestUsersDto()
            {
                UserName = "navid",
                Password = "963963"
            };// currect user data
            try
            {
                using (var connection = GetNewConnection())
                {
                    var result = await connection.QueryAsync(query, request, commandType: System.Data.CommandType.StoredProcedure);
                    foreach (var item in result.First())
                    {
                        if (((KeyValuePair<string, object>)item).Value.ToString() == "true")
                            Out.Success = true;
                        else
                            Out.Success = false;
                        connection.Close();
                    }
                }
            }
            catch
            {
                Out.Success = false;
            }
            Assert.Equal(Out.Success, true);
        }

        private SqlConnection GetNewConnection()
        {
            return new SqlConnection(Connection);
        }
        public void Dispose(){ }
    }
}
