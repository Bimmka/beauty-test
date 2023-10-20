using System.Collections.Generic;
using UISolution.Notifications;
using UnityEngine;
using UnityEngine.UI;

namespace UISolution.UI
{
    public class NotificationDisplayer : MonoBehaviour
    {
        [SerializeField] private NotificationElementView _notificationPrefab;
        [SerializeField] private RectTransform _spawnParent;
        [SerializeField] private ScrollRect _scrollView;
        [SerializeField] private Button _resetButton;
        [SerializeField] private VerticalLayoutGroup _contentLayout;

        private readonly Dictionary<int, NotificationElementView> _spawnedNotifications = new Dictionary<int, NotificationElementView>();

        private NotificationsContainer _notificationsContainer;
        
        private int _countElementsForShow;

        public void Construct(NotificationsContainer notificationsContainer)
        {
            _notificationsContainer = notificationsContainer;
            _notificationsContainer.OnNotificationChanged += OnNotificationChanged;
            InitializeStartNotifications();
        }

        private void Awake()
        {
            _resetButton.onClick.AddListener(OnResetClick);
            _countElementsForShow = CalculateElementShowCount();
            _scrollView.onValueChanged.AddListener(OnScrollChanged);
        }

        private void OnDestroy()
        {
            _notificationsContainer.OnNotificationChanged -= OnNotificationChanged;
            _resetButton.onClick.RemoveListener(OnResetClick);
            _scrollView.onValueChanged.RemoveListener(OnScrollChanged);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)_contentLayout.transform);
            CheckNotificationVisibile();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            _scrollView.verticalNormalizedPosition = 1f;
        }

        private void InitializeStartNotifications()
        {
            foreach (KeyValuePair<int,Notification> notification in _notificationsContainer.Notifications)
            {
                NotificationElementView notificationView = Instantiate(_notificationPrefab, _spawnParent);
                notificationView.ChangeNotificationIconEnableState(true);
                _spawnedNotifications.Add(notification.Key, notificationView);
            }
        }

        private void CheckNotificationVisibile()
        {
            int skipIndex = _countElementsForShow / 2;
            bool isFoundTopVisible = false;
            int topVisibleIndex = -1;
            int bottomVisibleIndex = -1;
            foreach (KeyValuePair<int,NotificationElementView> notification in _spawnedNotifications)
            {
                if (isFoundTopVisible && skipIndex > 0)
                {
                    skipIndex--;
                    continue;
                }
                
                if (RectTransformUtility.RectangleContainsScreenPoint(_scrollView.viewport,
                        notification.Value.transform.position, null))
                {
                    if (isFoundTopVisible == false)
                    {
                        topVisibleIndex = notification.Key;
                        isFoundTopVisible = true;
                    }
                }
                else if (isFoundTopVisible)
                {
                    if (notification.Key < _notificationsContainer.Notifications.Count - 1)
                        bottomVisibleIndex = notification.Key - 1;
                    else
                        bottomVisibleIndex = notification.Key;
                    break;
                }
            }

            for (int i = topVisibleIndex; i <= bottomVisibleIndex; i++)
            {
                _notificationsContainer.MarkNotificationViewed(i);
            }
        }

        private void OnResetClick()
        {
            _notificationsContainer.Reset();
            CheckNotificationVisibile();
        }

        private void OnScrollChanged(Vector2 value)
        {
            CheckNotificationVisibile();
        }

        private void OnNotificationChanged(int index)
        {
            Notification notification = _notificationsContainer.GetNotification(index);
            if (notification != null && _spawnedNotifications.TryGetValue(index, out NotificationElementView view))
                view.ChangeNotificationIconEnableState(notification.IsViewed == false);
        }

        private int CalculateElementShowCount()
        {
            return (int)( (_scrollView.viewport.rect.height + _contentLayout.spacing) / ((RectTransform)_notificationPrefab.transform).rect.height);
        }
    }
}
