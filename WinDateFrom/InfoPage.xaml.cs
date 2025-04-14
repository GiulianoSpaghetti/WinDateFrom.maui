namespace WinDateFrom;

public partial class InfoPage : ContentPage
{
    public static readonly Uri uri=new Uri("https://github.com/GiulianoSpaghetti/WinDateFrom.maui");

    public InfoPage()
	{
		InitializeComponent();
        Title = App.d["informations"] as string;
        lblinfo.Text = App.d["info"] as string;
        btnDeletePreferences.Text = App.d["delete_settings"] as string;
    }
    private async void OnSito_Click(object sender, EventArgs e)
    {
        await Launcher.Default.OpenAsync(uri);
    }
    private void DeleteOpzioni_Click(object sender, EventArgs e)
    {
        Preferences.Clear();
    }

}
