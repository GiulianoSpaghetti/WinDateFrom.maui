using System.Globalization;

namespace WinDateFrom.maui;

public partial class App : Application
{
    public static ResourceDictionary d;

    public App()
	{
		InitializeComponent();
        try
        {
            d = Resources[CultureInfo.CurrentCulture.TwoLetterISOLanguageName] as ResourceDictionary;

        }
        catch (Exception ex)
        {
            d = Resources["it"] as ResourceDictionary;
        }
        MainPage = new AppShell();
	}
#if ANDROID
    public static System.String GetResource(int id)
    {
        return Android.App.Application.Context.Resources.GetString(id);

    }
#endif

}
