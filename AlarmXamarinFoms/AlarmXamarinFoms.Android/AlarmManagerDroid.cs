using System;
using AlarmXamarinFoms.Droid;
using Android.App;
using Android.Content;
using Android.Icu.Util;
using Android.OS;
using Xamarin.Forms;

[assembly:Dependency(typeof(AlarmManagerDroid))]
namespace AlarmXamarinFoms.Droid
{
    public class AlarmManagerDroid : IAlarm
    {
        public void Alarm(int seconds)
        {
            try
            {
                var dateTime = DateTime.Now;
                Android.Widget.Toast.MakeText(MainActivity.Current, dateTime.ToString("hh:mm:ss"), Android.Widget.ToastLength.Short).Show();
                //SE MUESTRA UN TOAST PARA SABER QUE TIEMPO EMPEZARA 

                var alarmManager = MainActivity.Current.GetSystemService(Context.AlarmService) as AlarmManager;
                var calendar = Calendar.Instance;//SE CREA INSTANCIA DE CALENDAR
                calendar.Add(CalendarField.Minute, 1);//SE AGREGA UN MINUTO; SE PUEDE CAMBIAR A CONVENENCIA
                //calendar.Add(CalendarField.Hour,1)
                //calendar.Add(CalendarField.Month, 1);
                var requestCode = (int)calendar.TimeInMillis / 1000;
                var intent = new Intent(MainActivity.Current, typeof(AlarmReceiver));
                intent.PutExtra("REQUEST_CODE", requestCode);
                intent.AddFlags(ActivityFlags.IncludeStoppedPackages);
                intent.AddFlags(ActivityFlags.ReceiverForeground);
                var pi = PendingIntent.GetBroadcast(MainActivity.Current, requestCode, intent, 0);
                if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.M)
                {
                    alarmManager.SetExactAndAllowWhileIdle(AlarmType.RtcWakeup, calendar.TimeInMillis, pi);
                }
                else
                    alarmManager.SetExact(AlarmType.RtcWakeup, calendar.TimeInMillis, pi);
            }
            catch(Exception ex)
            {

            }
        }
    }
}
