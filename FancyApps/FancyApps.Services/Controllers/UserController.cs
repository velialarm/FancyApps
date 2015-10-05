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
            var response = new ResponseModel();

            if (!this.ModelState.IsValid)
            {
                response.Status = new Status
                {
                    Code = Status.INTERNAL_MESSAGE_CODE,
                    Message = this.ModelState.Values.First().Errors.First().ErrorMessage
                };

                return response;
            }

            if (input == null)
            {
                response.Status = Status.MISSING_PARRAMETERS;
                return response;
            }

            ////check for exist user in db by email
            var existUser = this.Users.SingleOrDefault(a => a.Email == input.Email);
            if (existUser != null)
            {
                response.Status = Status.SIGNUP_HAS_ACCOUNT_YET;
                return response;
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

            response.Status = Status.SIGNUP_SUCCESS;
            return response;
        }

        [Route("api/login")]
        [HttpPost]
        public LoginResponseModel AuthenticateUser(LoginRequestModel input)
        {
            var response = new LoginResponseModel();

            if (!this.ModelState.IsValid)
            {
                var errorMessage = this.ModelState.Values.First().Errors.First().ErrorMessage;
                response.Status = new Status(102, errorMessage);
                return response;
            }

            if (input == null)
            {
                response.Status = Status.MISSING_PARRAMETERS;
                return response;
            }

            ////get user by email
            var existUser = this.Users.SingleOrDefault(a => a.Email == input.Email);
            bool isValidPassword = false;
            if (existUser != null)
            {
                isValidPassword = AuthenticationUtil.IsValidPassword(existUser.Token, input.Password);
               
            }
            if (!isValidPassword)
            {
                response.Status = Status.INVALID_PASSWORD;
                return response;
            }

            var recreateToken = AuthenticationUtil.Encrypt(input.Password);
            existUser.Token = recreateToken;
            if (!String.IsNullOrEmpty(input.FanApp))
            {
                existUser.FanApps = input.FanApp;
            }

            this.Users.Update(existUser);
            response.Status = Status.LOGIN_SUCCESFULLY;
            response.Token = recreateToken;
            return response;
        }

        [Route("api/get-users")]
        [HttpPost]
        public GetUsersResponse SearchUsers(GetUsersRequest input)
        {
            GetUsersResponse response = new GetUsersResponse();

            if (!this.ModelState.IsValid)
            {
                response.Status = new Status
                {
                    Code = Status.INTERNAL_MESSAGE_CODE,
                    Message = this.ModelState.Values.First().Errors.First().ErrorMessage
                };

                return response;
            }

            if (input == null)
            {
                response.Status = Status.MISSING_PARRAMETERS;
                return response;
            }

            ////check for register user by Token
            User currentUser = null;
            try
            {
                currentUser = this.Users.SingleOrDefault(a => a.Token == input.Token); ////TOKEN must be unique 
                if (currentUser == null)
                {
                    response.Status = Status.INVALID_TOKEN;
                    return response;
                }
            }
            catch (Exception)
            {
                response.Status = Status.INTERNAL_ERROR;
                return response;
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

            response.Status = Status.SUCCESS_FETCH_USERS;
            response.Users = usersResponseList;
            response.FanAppName = currentUser.FanApps;
            return response;
        }
    }
}