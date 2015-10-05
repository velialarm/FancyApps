namespace FancyApps.Services.Models
{
    public class Status
    {
        public static readonly int INTERNAL_MESSAGE_CODE = 601;

        public static readonly Status MISSING_PARRAMETERS = new Status(900, "Missign parameters");
        public static readonly Status INVALID_TOKEN = new Status(103, "Invalid Token");
        public static readonly Status INTERNAL_ERROR = new Status(105, "Internal Error");

        //SIGN UP
        public static readonly Status SIGNUP_HAS_ACCOUNT_YET = new Status(229, "You have account. In Family Apps and You can authenticate with it.");
        public static readonly Status SIGNUP_SUCCESS = new Status(0, "You are singup succesfull");

        //GET USERS
        public static readonly Status SUCCESS_FETCH_USERS =  new Status(0, "Successfully fetch users");

        //LOGIN
        public static readonly Status INVALID_PASSWORD = new Status(104, "Invalid password");
        public static readonly Status LOGIN_SUCCESFULLY = new Status(0, "You are logged succesfull");


        //ADD FRIEND
        public static readonly Status ADDFRIEND_EXIST = new Status(603, "Friend is exist in your list");
        public static readonly Status ADDFRIEND_SUCCESSFULL_ADDED = new Status(0, "Friend was successfully added");

        //Remove Friend 
        public static readonly Status REMOVEFRIEND_SUCCESS = new Status(0, "Success remove friend from your list");
        public static readonly Status REMOVEFRIEND_NOT_EXIST_IN_LIST = new Status(603, "Friend is not exist in your list.");

        public Status()
        {
        }

        public Status(int code, string message)
        {
            this.Code = code;
            this.Message = message;
        }

        public int Code { get; set; }

        public string Message { get; set; }
    }
}