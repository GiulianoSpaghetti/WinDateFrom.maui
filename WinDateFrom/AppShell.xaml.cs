namespace WinDateFrom;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		scMain.Title = App.d["application"] as string;
		scInfo.Title = App.d["informations"] as string;
	}
}
