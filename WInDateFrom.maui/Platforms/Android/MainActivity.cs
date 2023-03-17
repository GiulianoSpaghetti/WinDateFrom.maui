using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using AndroidX.Core.App;
using AndroidX.Core.Content;
using Java.Util;
using static Android.Provider.CalendarContract;
using TimeZone = Java.Util.TimeZone;

namespace WinDateFrom.maui;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    public static MainActivity Instance { get; private set; }
    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        Instance = this;
    }
    public bool CalendarHelper(string nome, DateTime d)
    {
        long startMillis = 0;
        long endMillis = 0;
        Android.Net.Uri uri;
        Calendar beginTime = Calendar.GetInstance(LocaleList.Default.Get(0));
        beginTime.Set(d.Year, d.Month, d.Day, 0, 0);
        startMillis = beginTime.TimeInMillis;
        Calendar endTime = Calendar.GetInstance(LocaleList.Default.Get(0));
        beginTime.Set(d.Year, d.Month, d.Day, 0, 30);
        startMillis = beginTime.TimeInMillis;

        ContentResolver cr = ContentResolver;
        ContentValues values = new ContentValues();
        values.Put(Reminders.InterfaceConsts.Dtstart, startMillis);
        values.Put(Reminders.InterfaceConsts.Dtend, endMillis);
        values.Put(Reminders.InterfaceConsts.Title, $"Incontro di {nome}");
        values.Put(Reminders.InterfaceConsts.Description, $"Hai incontrato {nome} in data {d}");
        values.Put(Reminders.InterfaceConsts.CalendarId, 0);
        values.Put(Reminders.InterfaceConsts.AllDay, 1);
        values.Put(Reminders.InterfaceConsts.HasAlarm, true);
        values.Put(Reminders.InterfaceConsts.Rrule, $"FREQ=MONTHLY;BYMONTHDAY={d.Day};UNTIL={d.Year+50}{d.Month}{d.Day}");
        values.Put(Reminders.InterfaceConsts.EventTimezone, TimeZone.Default.ID);
        checkPermissions(new[] { Manifest.Permission.WriteCalendar });
        try
        {
            uri = cr.Insert(Events.ContentUri, values);
        }
        catch (Java.Lang.SecurityException)
        {
            return false;
        }
        return true;
    }

    private void checkPermissions(String[] permissionsId)
    {
        bool permissions = true;
        foreach (String p in permissionsId)
        {
            permissions = permissions && ContextCompat.CheckSelfPermission(this, p) == Permission.Granted;
        }

        if (!permissions)
            ActivityCompat.RequestPermissions(this, permissionsId, 0);
    }
}
