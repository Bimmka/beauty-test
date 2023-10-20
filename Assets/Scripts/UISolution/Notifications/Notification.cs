namespace UISolution.Notifications
{
    public class Notification
    {
        public bool IsViewed { get; private set; }

        public void ChangeViewedState(bool isViewed)
        {
            IsViewed = isViewed;
        }
    }
}