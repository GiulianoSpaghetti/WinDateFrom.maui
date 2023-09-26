namespace WinDateFrom.maui;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        data.Date = new DateTime(Preferences.Get("anno", DateTime.Now.Year), Preferences.Get("mese", DateTime.Now.Month), Preferences.Get("giorno", DateTime.Now.Day));
        nome.Text = Preferences.Get("nome", "");
#if ANDROID
        
        tbnome.Text = App.GetResource(Resource.String.insert_the_name);
        tbdata.Text = App.GetResource(Resource.String.insert_the_date);
        calcola.Text = App.GetResource(Resource.String.calculate);
        Title = App.GetResource(Resource.String.application);
        btnCalendario.Text=App.GetResource(Resource.String.btncalendar);
        btnCalendario.IsEnabled = false;
#else
        tbnome.Text = $"{App.d["insert_the_name"]}: ";
        tbdata.Text = $"{App.d["insert_the_date"]}: ";
        calcola.Text = App.d["calculate"] as string;
        Title = App.d["application"] as string;
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
            risultato.Text = App.d["invalid_rvalue"] as string;
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
                    anniversario.Text = App.d["is_your_anniversary"] as string;
#endif
                }
                else
                {
#if ANDROID
                    anniversario.Text = App.GetResource(Resource.String.is_your_mesiversary);
#else
                    anniversario.Text = App.d["is_your_mesiversary"] as string;
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
            risultato.Text = $"{differenza.Days} {App.d["days_are_passed"]}";
        else
            risultato.Text = $"{App.d["you_meet"]} {nome.Text} {App.d["about"]} {differenza.Days} {App.d["days_ago"]}.";
#endif
        Preferences.Set("giorno", data.Date.Day);
        Preferences.Set("mese", data.Date.Month);
        Preferences.Set("anno", data.Date.Year);
        Preferences.Set("nome", nome.Text);
    }

    private void OnCalendar_Click(object sender, EventArgs e)
    {
#if ANDROID
        long calendar=Preferences.Get("calendar", 0);
        if (calendar == 0)
        {
            calendar=WinDateFrom.maui.Platforms.Android.CalendarHelperService.CreateCalendar();
            if (calendar==0) {
                risultato.Text="Non hai nessun account google sul telefono";
                return;
            } else
                Preferences.Set("calendar", calendar);
        }
       WinDateFrom.maui.Platforms.Android.CalendarHelperService.SaveDate(calendar, nome.Text, data.Date);
#endif
        ;
    }
}

