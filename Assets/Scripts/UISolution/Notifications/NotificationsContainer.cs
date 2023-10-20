using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace UISolution.Notifications
{
    public class NotificationsContainer
    {
        private readonly int _maxNotificationsCount;
        private readonly Dictionary<int, Notification> _notifications = new Dictionary<int, Notification>();

        public event Action<int> OnNotificationChanged;

        public IReadOnlyDictionary<int, Notification> Notifications => _notifications;

        public NotificationsContainer(int notificationsCount)
        {
            _maxNotificationsCount = notificationsCount;

            for (int i = 0; i < notificationsCount; i++)
            {
                _notifications.Add(i, new Notification());
            }
        }

        public void MarkNotificationViewed(int index)
        {
            if (_notifications.TryGetValue(index, out Notification notification))
            {
                notification.ChangeViewedState(true);
                NotifyAboutNotificationChange(index);
            }
            else
                Debug.LogError($"Try mark notification with index {index} viewed that is not presented");
        }

        [CanBeNull]
        public Notification GetNotification(int index)
        {
            if (_notifications.TryGetValue(index, out Notification notification))
                return notification;
            
            Debug.LogError($"Try take notification with index {index} that is not presented");
            return null;
        }

        public void Reset()
        {
            foreach (KeyValuePair<int,Notification> notification in _notifications)
            {
                notification.Value.ChangeViewedState(false);
                NotifyAboutNotificationChange(notification.Key);
            }
        }

        private void NotifyAboutNotificationChange(int index)
        {
            OnNotificationChanged?.Invoke(index);
        }
    }
}