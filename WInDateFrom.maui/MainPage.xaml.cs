namespace WinDateFrom.maui;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        data.Date = DateTime.Parse(Preferences.Get("Data", DateTime.Now.ToString()));
        nome.Text = Preferences.Get("Nome", "");
#if ANDROID
        tbnome.Text = App.GetResource(Resource.String.insert_the_name);
        tbdata.Text = App.GetResource(Resource.String.insert_the_date);
        calcola.Text = App.GetResource(Resource.String.calculate);
        Title = App.GetResource(Resource.String.application);
#endif
    }

    private void calcola_Click(object sender, EventArgs e)
    {
        risultato.Text = "";
        anniversario.Text = "";
        DateTime d = DateTime.Now;
        TimeSpan differenza = d - data.Date;
        if (differenza.Milliseconds < 0)
        {
#if ANDROID
            risultato.Text = App.GetResource(Resource.String.invalid_rvalue);
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
#endif
                }
                else
                {
#if ANDROID
                    anniversario.Text = App.GetResource(Resource.String.is_your_mesiversary);
#endif
                }
            }
        }
#if ANDROID
        risultato.Text = $"{App.GetResource(Resource.String.you_meet)} {nome.Text} {App.GetResource(Resource.String.about)} {differenza.Days} {App.GetResource(Resource.String.days_ago)}.";
#endif

        Preferences.Set("Data", data.Date.ToString());
        Preferences.Set("Nome", nome.Text);
    }
}

