using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace ClipboardCompanion.Services
{
    public class NotificationService : INotificationService
    {
        private readonly string _applicationTitle;
        private readonly NotifyIcon _notifyIcon;
        private DateTime _lastBalloonTipShownUtc = DateTime.MinValue;
        private readonly TimeSpan _timeBetweenBalloonTips = new TimeSpan(0, 0, 0, 5);

        private const int BalloonTipTimeoutMillisecondsPreVista = 3000;

        public NotificationService(string applicationTitle)
        {
            _applicationTitle = applicationTitle;
            _notifyIcon = new NotifyIcon
            {
                Icon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location)
            };

            _notifyIcon.BalloonTipClosed += (sender, e) => _notifyIcon.Visible = false;
        }

        public void ShowNotification(string message)
        {
            var timeSinceLastBalloonTip = DateTime.UtcNow - _lastBalloonTipShownUtc;
            
            // If too many balloons are shown in a short amount of time, Windows Explorer crashes
            if (timeSinceLastBalloonTip > _timeBetweenBalloonTips)
            {
                _notifyIcon.Visible = true;
                _notifyIcon.ShowBalloonTip(BalloonTipTimeoutMillisecondsPreVista, _applicationTitle, message, ToolTipIcon.Info);
                _lastBalloonTipShownUtc = DateTime.UtcNow;
            }
        }
    }
}
