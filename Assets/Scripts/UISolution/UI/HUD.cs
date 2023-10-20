using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.UI
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private Button _windowButton;
        
        private NotificationDisplayer _notificationDisplayer;

        public void Construct(NotificationDisplayer notificationDisplayer)
        {
            _notificationDisplayer = notificationDisplayer;
        }

        private void Awake()
        {
            _windowButton.onClick.AddListener(OnWindowButtonClick);
        }

        private void OnDestroy()
        {
            _windowButton.onClick.AddListener(OnWindowButtonClick);
        }

        private void OnWindowButtonClick()
        {
            if (_notificationDisplayer.gameObject.activeInHierarchy)
                _notificationDisplayer.Hide();
            else
                _notificationDisplayer.Show();
        }
    }
}