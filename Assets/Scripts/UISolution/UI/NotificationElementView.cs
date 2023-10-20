using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UISolution.UI
{
    public class NotificationElementView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _descriptionText;
        [SerializeField] private Image _notificationIcon;

        public void Display(string description)
        {
            _descriptionText.text = description;
        }

        public void ChangeNotificationIconEnableState(bool isEnable)
        {
            _notificationIcon.enabled = isEnable;
        }
    }
}