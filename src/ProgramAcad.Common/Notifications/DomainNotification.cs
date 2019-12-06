namespace ProgramAcad.Common.Notifications
{
    public class DomainNotification
    {
        public DomainNotification(string reason, string details)
        {
            Reason = reason;
            Details = details;
        }

        public string Reason { get; protected set; }
        public string Details { get; protected set; }
    }
}
