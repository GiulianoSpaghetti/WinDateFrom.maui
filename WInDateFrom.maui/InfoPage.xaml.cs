namespace WinDateFrom.maui;

public partial class InfoPage : ContentPage
{
	public InfoPage()
	{
		InitializeComponent();
#if ANDROID
        lblinfo.Text =App.GetResource(Resource.String.info);
        btnDeletePreferences.Text =App.GetResource(Resource.String.delete_settings);
#else
        lblinfo.Text = "A simple app for know how long passed from a determinate date";
        btnDeletePreferences.Text = "Delete Settings";
#endif
    }
    private async void OnSito_Click(object sender, EventArgs e)
    {
        await Launcher.Default.OpenAsync(new Uri("https://github.com/numerunix/WinDateFrom.maui"));
    }
    private void DeleteOpzioni_Click(object sender, EventArgs e)
    {
        Preferences.Clear();
    }

}