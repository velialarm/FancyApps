namespace FancyApps.Services.Models.Response
{
    public class LoginResponseModel : ResponseModel
    {
        public LoginResponseModel(Status status):base(status){}

        public LoginResponseModel(Status status, string token) : base(status)
        {
            this.Token = token;
        }

        //[IgnoreDataMember]
        public string Token { get; set; }
    }
}