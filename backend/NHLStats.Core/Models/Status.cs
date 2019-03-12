namespace NHLStats.Core.Models
{
    public class Status
    {
        public StatusType StatusType {get; set; } = StatusType.None;

        public string Message  {get; set; } = string.Empty;

        public int Id {get;set;}

        public Status()
        {
            
        }
        public Status(StatusType type, string msg = "") 
        {
            StatusType = type;
            Message = msg;
        }
    }

    public enum StatusType
    {
        None,
        Deleted,
        Error
    }
}