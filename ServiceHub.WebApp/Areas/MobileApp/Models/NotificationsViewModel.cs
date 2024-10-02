using System.ComponentModel.DataAnnotations;

namespace ServiceHub.WebApp.Models
{
    public class NotificationsViewModel
    {
        public NotificationList notificationList = new NotificationList();
    }

    public class NotificationList
    {
        public string NotificationType { get; set; } = string.Empty;
        public string NotificationIcon { get; set; } = string.Empty;
        public string NotificationText { get; set; } = string.Empty;
    }
}