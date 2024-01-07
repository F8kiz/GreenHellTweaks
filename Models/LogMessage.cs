
namespace GHTweaks.Models
{
    internal class LogMessage
    {
        public LogType Type { get; set; }

        public string Message { get; set; }


        public LogMessage(LogType type, string message)
        {
            Type = type;
            Message = message;
        }   
    }
}
