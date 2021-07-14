using System;
using Android.Content;
using Android.Widget;

namespace AlarmXamarinFoms.Droid
{
    [BroadcastReceiver]
    public class AlarmReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            var dateTime = DateTime.Now;
            Toast.MakeText(context, dateTime.ToString("hh:mm:ss"), ToastLength.Short).Show();
        }
    }
}
