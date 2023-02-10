namespace WinDateFrom.maui;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
        data.Date = DateTime.Parse(Preferences.Get("Data", DateTime.Now.ToString()));
        nome.Text = Preferences.Get("Nome", "");
#if ANDROID
        tbnome.Text = GetResource(Resource.String.insert_the_name);
        tbdata.Text = GetResource(Resource.String.insert_the_date);
        calcola.Text = GetResource(Resource.String.calculate);
        Informazioni.Text = GetResource(Resource.String.informations);
#elif NET7_0_OR_GREATER
        tbnome.Text = GetResource("insert_the_name");
        tbdata.Text = GetResource("insert_the_date");
        calcola.Text = GetResource("calculate");
        Informazioni.Text = GetResource("informations");
#endif
    }

    private void calcola_Click(object sender, EventArgs e)
    {
        DateTime d = DateTime.Now;
        TimeSpan differenza = d - data.Date;
        if (differenza.Milliseconds<0)
        {
#if ANDROID
            risultato.Text= GetResource(Resource.String.invalid_rvalue);
#elif NET7_0_OR_GREATER
            risultato.Text = GetResource("invalid_rvalue");
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
                    anniversario.Text = GetResource(Resource.String.is_your_anniversary);
#elif NET7_0_OR_GREATER
                    anniversario.Text = GetResource("is_your_anniversary");
#endif
                }
                else
                {
#if ANDROID
                    anniversario.Text = GetResource(Resource.String.is_your_mesiversary);
#elif NET7_0_OR_GREATER
                    anniversario.Text = GetResource("is_your_mesiversary");
#endif
                }
            }
        }
#if ANDROID
        risultato.Text = $"{GetResource(Resource.String.you_meet)} {nome.Text} {GetResource(Resource.String.about)} {differenza.Days} {GetResource(Resource.String.days_ago)}.";
#elif NET7_0_OR_GREATER
        risultato.Text = $"{GetResource("you_meet")} {nome.Text} {GetResource("about")} {differenza.Days} {GetResource("days_ago")}.";
#endif

        Preferences.Set("Data", data.Date.ToString());
        Preferences.Set("Nome", nome.Text);
    }


    private async void Informazioni_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new InfoPage());
    }
#if ANDROID
    private System.String GetResource(int id)
    {
        return Android.App.Application.Context.Resources.GetString(id);

    }
#elif NET7_0_OR_GREATER
    private System.String GetResource(string id)
    {
        return Resources[id].ToString();
    }
#endif
}

