namespace WinDateFrom.maui;

public partial class MainPage : ContentPage
{
    private long cal;
    public MainPage()
    {
        InitializeComponent();
        data.Date = DateTime.Parse(Preferences.Get("Data", DateTime.Now.ToString()));
        nome.Text = Preferences.Get("Nome", "");
#if ANDROID
        cal = Preferences.Get("calendar", 0L);
        tbnome.Text = App.GetResource(Resource.String.insert_the_name);
        tbdata.Text = App.GetResource(Resource.String.insert_the_date);
        calcola.Text = App.GetResource(Resource.String.calculate);
        Title = App.GetResource(Resource.String.application);
        btnCalendario.Text=App.GetResource(Resource.String.btncalendar);
        btnCalendario.IsEnabled = false;
#else
        tbnome.Text = "Insert the name:";
        tbdata.Text = "Insert the date:";
        calcola.Text = "Calculate";
        Title = "Application";
        btnCalendario.IsVisible = false;
#endif
    }

    private void calcola_Click(object sender, EventArgs e)
    {
        btnCalendario.IsEnabled = false;
        risultato.Text = "";
        anniversario.Text = "";
        DateTime d = DateTime.Now;
        TimeSpan differenza = d - data.Date;
        if (differenza.Milliseconds < 0)
        {
#if ANDROID
            risultato.Text = App.GetResource(Resource.String.invalid_rvalue);
#else
            risultato.Text = "Invalid rvalue";
#endif
            return;
        }
        if (differenza.Days > 1)
        {
            if (d.Day == data.Date.Day)
            {
                if (d.Month == data.Date.Month)
                {
#if ANDROID
                    anniversario.Text = App.GetResource(Resource.String.is_your_anniversary);
#else
                    anniversario.Text = "Is your anniversary";
#endif
                }
                else
                {
#if ANDROID
                    anniversario.Text = App.GetResource(Resource.String.is_your_mesiversary);
#else
                    anniversario.Text = "Is your mesiversary";
#endif
                }
            }
        }
#if ANDROID
    if (nome.Text=="")
        risultato.Text= $"{differenza.Days} {App.GetResource(Resource.String.days_are_passed)}";
    else {
        risultato.Text = $"{App.GetResource(Resource.String.you_meet)} {nome.Text} {App.GetResource(Resource.String.about)} {differenza.Days} {App.GetResource(Resource.String.days_ago)}.";
        btnCalendario.IsEnabled=true;
    }
#else
        if (nome.Text == "")
            risultato.Text = $"{differenza.Days} days have passed";
        else
            risultato.Text = $"You met {nome.Text} about {differenza.Days} days ago.";
#endif

        Preferences.Set("Data", data.Date.ToString());
        Preferences.Set("Nome", nome.Text);
    }

    private void OnCalendar_Click(object sender, EventArgs e)
    {
#if ANDROID 
            risultato.Text="";
            btnCalendario.IsEnabled = false;
            if (nome.Text=="") {
                risultato.Text=App.GetResource(WinDateFrom.maui.Resource.String.invalid_name);
                return;
            }
            if (cal == 0)
            {
                cal = WinDateFrom.maui.Platforms.Android.CalendarHelperService.CreateCalendar();
                if (cal == 0)
                {
                    risultato.Text = App.GetResource(Resource.String.calendar_not_created);
                    btnCalendario.IsVisible = false;
                }
                else
                    Preferences.Set("calendar", cal);
        }
        if (!WinDateFrom.maui.Platforms.Android.CalendarHelperService.Set(cal, nome.Text,  data.Date))
                risultato.Text=App.GetResource(WinDateFrom.maui.Resource.String.calendar_error);
            else
                risultato.Text=App.GetResource(WinDateFrom.maui.Resource.String.inserted_into_calendar);
#endif
    }
}

