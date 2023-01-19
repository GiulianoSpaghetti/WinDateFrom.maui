namespace WinDateFrom.maui;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
        data.Date = DateTime.Parse(Preferences.Get("Data", DateTime.Now.ToString()));
    }

    private void calcola_Click(object sender, EventArgs e)
    {
        DateTime d = DateTime.Now;
        TimeSpan differenza = d - data.Date.Date;
        risultato.Text = $"Hai incontrotato {nome.Text} circa {differenza.Days} giorni fa.";
        Preferences.Set("Data", data.Date.ToString());
        Preferences.Set("Nome", nome.Text);
    }

    private async void Informazioni_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new InfoPage());
    }
}

