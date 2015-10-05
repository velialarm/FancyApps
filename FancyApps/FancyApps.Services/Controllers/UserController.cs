namespace FancyApps.Services.Controllers
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using Data;
    using Model;
    using Models;
    using Models.DataBind;
    using Models.Request;
    using Models.Response;

    public class UserController : AbstractApiController
    {
 
        [Route("api/sing-up")]
        [HttpPost]
        public ResponseModel RegisterUser(SignUpRequestModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return new ResponseModel
                {
                    Status = new Status
                    {
                        Code = 101,
                        Message = this.ModelState.Values.First().Errors.First().ErrorMessage
                    }
                };
            }

            if (input == null)
            {
                return new ResponseModel
                {
                    Status = new Status(900, "Missing parameters")
                };
            }

            //check for exist user in db by email
            var existUser = this.Users.SingleOrDefault(a => a.Email == input.Email);
            if (existUser != null)
            {
                return new ResponseModel
                {
                    Status = new Status
                    {
                        Code = 0,
                        Message = "You have account. In Family Apps and You can authenticate with it."
                    }
                };
            }

            this.Users.Add(new User
            {
                FirstName = input.FirstName,
                LastName = input.LastName,
                Email = input.Email,
                Phone = input.Phone,
                FanApps = input.FanApp,
                Gender = input.Gender,
                City = input.City,
                Address = input.Address,
                CreateDatetime = DateTime.Now,
                Token = AuthenticationUtil.Encrypt(input.Password)

            });

            return new ResponseModel(new Status(0, "You are singup succesfull"));
        }

        [Route("api/login")]
        [HttpPost]
        public LoginResponseModel AuthenticateUser(LoginRequestModel input)
        {
            if (!this.ModelState.IsValid)
            {
                var errorMessage = this.ModelState.Values.First().Errors.First().ErrorMessage;
                return new LoginResponseModel(new Status(102, errorMessage));
            }

            if (input == null)
            {
                return new LoginResponseModel(new Status(900, "Missing parameters"));
            }

            //get user by email
            var existUser = this.Users.SingleOrDefault(a => a.Email == input.Email);
            if (existUser != null)
            {
                bool isValidPassword = AuthenticationUtil.isValidPassword(existUser.Token, input.Password);
                if(!isValidPassword)
                {
                    return new LoginResponseModel(new Status(104, "Invalid password"), null);
                }

            }
            else
            {
                //not exist user
                return new LoginResponseModel(new Status(104, "Not exist user"), null);
            }

            var recreateToken = AuthenticationUtil.Encrypt(input.Password);
            existUser.Token = recreateToken;
            if (!String.IsNullOrEmpty(input.FanApp))
            {
                existUser.FanApps = input.FanApp;
            }
            this.Users.Update(existUser);
            return new LoginResponseModel(new Status(0, "You are logged succesfull"), recreateToken);
        }

        [Route("api/get-users")]
        [HttpPost]
        public GetUsersResponse SearchUsers(GetUsersRequest input)
        {
            if (!this.ModelState.IsValid)
            {
                return new GetUsersResponse
                {
                    Status = new Status
                    {
                        Code = 102,
                        Message = this.ModelState.Values.First().Errors.First().ErrorMessage
                    }
                };
            }

            if (input == null)
            {
                return new GetUsersResponse
                {
                    Status = new Status(900, "Missing parameters")
                };
            }

            //check for register user by Token
            User currentUser = null;
            try
            {
                currentUser = this.Users.SingleOrDefault(a => a.Token == input.Token); //TODO TOKEN must be unique 
                if (currentUser == null)
                {
                    return new GetUsersResponse(new Status(103, "Invalid Token"));
                }
            }
            catch (Exception)
            {
                return new GetUsersResponse(new Status(105, "Internal Error"));
            }


            List<User> allUsers = this.Users.Where(a => a.FanApps == currentUser.FanApps).AsQueryable().ToList();
            List<UserModel> usersResponseList = new List<UserModel>();
            foreach (User item in allUsers)
            {
                usersResponseList.Add(new UserModel
                {
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Email = item.Email
                });
            }

            var response =  new GetUsersResponse(new Status(0, "Success"), usersResponseList);
            response.FanAppName = currentUser.FanApps;
            return response;
        }

    }
}
