using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramAcad.Common.Notifications
{
    public class DomainNotificationManager
    {
        private readonly List<DomainNotification> _notifications;

        public DomainNotificationManager()
        {
            _notifications = new List<DomainNotification>();
        }

        public Task Notify(DomainNotification notification)
        {
            _notifications.Add(notification);
            return Task.CompletedTask;
        }

        public Task Notify(string reason, string details)
        {
            var notification = new DomainNotification(reason, details);
            _notifications.Add(notification);
            return Task.CompletedTask;
        }

        public virtual List<DomainNotification> GetNotifications()
        {
            return _notifications;
        }

        public virtual bool HasNotifications()
        {
            return GetNotifications().Any();
        }
    }
}
