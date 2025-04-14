using System.Globalization;

namespace WinDateFrom;

public partial class App : Application
{
    private static ResourceDictionary dic;
    public static ResourceDictionary d { get => dic; }
    public App()
	{
		InitializeComponent();
        try
        {
            dic = Resources[CultureInfo.CurrentCulture.TwoLetterISOLanguageName] as ResourceDictionary;

        }
        catch (Exception ex)
        {
            dic = Resources["it"] as ResourceDictionary;
        }
	}
    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShell());
    }
}
