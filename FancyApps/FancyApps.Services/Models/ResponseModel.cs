namespace FancyApps.Services.Models.Response
{
    public class ResponseModel
    {

        public ResponseModel(){}

        public ResponseModel(Status status)
        {
            this.Status = status;
        }

        public Status Status { get; set; }


    }
}