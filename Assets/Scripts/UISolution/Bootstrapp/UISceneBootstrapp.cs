using UI.Notifications;
using UI.UI;
using UnityEngine;

namespace UI.Bootstrapp
{
    public class UISceneBootstrapp : MonoBehaviour
    {
        [SerializeField] private NotificationDisplayer _notificationDisplayerPrefab;
        [SerializeField] private HUD _hudPrefab;
        [SerializeField] private int _notificationsCount = 20;

        private void Awake()
        {
            NotificationDisplayer notificationDisplayer = Instantiate(_notificationDisplayerPrefab);
            HUD hud = Instantiate(_hudPrefab);
            hud.Construct(notificationDisplayer);

            NotificationsContainer notificationsContainer = new NotificationsContainer(_notificationsCount);
            notificationDisplayer.Construct(notificationsContainer);
        }
    }
}
