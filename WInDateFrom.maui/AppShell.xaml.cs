namespace WinDateFrom.maui;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
#if ANDROID
		scMain.Title = App.GetResource(Resource.String.application);
		scInfo.Title = App.GetResource(Resource.String.informations);
#else
		scMain.Title = App.d["application"] as string;
		scInfo.Title = App.d["informations"] as string;
#endif
	}
}
