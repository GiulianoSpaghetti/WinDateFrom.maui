using Android;
using Android.Accounts;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using AndroidX.Core.App;
using AndroidX.Core.Content;
using Java.Util;
using Microsoft.Maui.Controls.PlatformConfiguration;
using static Android.Provider.CalendarContract;
using TimeZone = Java.Util.TimeZone;

namespace WinDateFrom.maui;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    public static MainActivity Instance { get; private set; }
    private ContentResolver cr;
    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        Instance = this;
        cr = ContentResolver;
    }
    public bool CalendarHelper(long cal, string nome, DateTime d)
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

        ContentValues values = new ContentValues();
        values.Put(Reminders.InterfaceConsts.Dtstart, startMillis);
        values.Put(Reminders.InterfaceConsts.Dtend, endMillis);
        values.Put(Reminders.InterfaceConsts.Title, $"Incontro di {nome}");
        values.Put(Reminders.InterfaceConsts.Description, $"Hai incontrato {nome} in data {d}");
        values.Put(Reminders.InterfaceConsts.CalendarId, cal);
        values.Put(Reminders.InterfaceConsts.AllDay, 1);
        values.Put(Reminders.InterfaceConsts.HasAlarm, true);
        values.Put(Reminders.InterfaceConsts.Rrule, $"FREQ=MONTHLY;BYMONTHDAY={d.Day};UNTIL={d.Year+2}{d.Month}{d.Day}");
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

    public long createLocalCalendar()
    {
        ContentValues values = new ContentValues();
        Account[] accounts = AccountManager.Get(this).GetAccountsByType("com.gmail");
        if (accounts.Length == 0)
            return 0;
        values.Put(
                Calendars.InterfaceConsts.AccountName,
        "WinDateFrom");
        values.Put(
                Calendars.InterfaceConsts.AccountType,
                CalendarContract.AccountTypeLocal);
        values.Put(
                Calendars.InterfaceConsts.AccountName,
        "WinDateFrom");
        values.Put(
                Calendars.InterfaceConsts.CalendarDisplayName,
        "WinDateFrom");
        values.Put(
                Calendars.InterfaceConsts.CalendarColor,
                0xffff0000);
        values.Put(
        Calendars.InterfaceConsts.CalendarAccessLevel,
        (int)Android.Provider.CalendarAccess.AccessOwner);
        values.Put(
                Calendars.InterfaceConsts.OwnerAccount,
        accounts[0].Name);
        values.Put(
                Calendars.InterfaceConsts.CalendarTimeZone,
        "Europe/Rome");
        Android.Net.Uri.Builder builder =
            CalendarContract.Calendars.ContentUri.BuildUpon();
        builder.AppendQueryParameter(
                Calendars.InterfaceConsts.AccountName,
        "org.altervista.numerone.windatefrom");
        builder.AppendQueryParameter(
                Calendars.InterfaceConsts.AccountType,
                CalendarContract.AccountTypeLocal);
        builder.AppendQueryParameter(
                CalendarContract.CallerIsSyncadapter,
        "true");
        checkPermissions(new[] { Manifest.Permission.WriteCalendar });
        Android.Net.Uri uri = cr.Insert(builder.Build(), values);

        // Now get the CalendarID :
        return long.Parse(uri.LastPathSegment);
    }

    public void ResetCalendar(long calendar)
    {
        long eventid;
        Android.Net.Uri events = Android.Net.Uri.Parse($"{Events.ContentUri}");
        checkPermissions(new[] { Manifest.Permission.ReadCalendar });
        var cursor = cr.Query(events, new String[] { "_id" }, "calendar_id=" + calendar, null, null);
        while (cursor.MoveToNext())
        {
                eventid = cursor.GetLong(cursor.GetColumnIndex("_id"));
                cr.Delete(ContentUris.WithAppendedId(events, eventid), null, null);
        }
        cursor.Close();
    }
 }
