namespace FancyApps.Services.Models
{
    public class Status
    {
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