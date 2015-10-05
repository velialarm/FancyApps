namespace FancyApps.Services.Models.Response
{
    using System.Collections.Generic;
    using DataBind;

    public class GetUsersResponse : ResponseModel
    {
        public GetUsersResponse(){}

        public GetUsersResponse(Status status) 
            : base(status){}

        public GetUsersResponse(Status status, List<UserModel> userModels)
            :base(status)
        {
            this.Users = userModels;
        }

        public List<UserModel> Users { get; set; }

        public string FanAppName { get; set; }
    }
}