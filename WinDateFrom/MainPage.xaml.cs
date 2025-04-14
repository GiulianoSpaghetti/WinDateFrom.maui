using System.Runtime.CompilerServices;

namespace WinDateFrom;

public partial class MainPage : ContentPage
{

    private static String ricorrenza = ""; 
    public MainPage()
    {
        InitializeComponent();
        data.Date = new DateTime(Preferences.Get("anno", DateTime.Now.Year), Preferences.Get("mese", DateTime.Now.Month), Preferences.Get("giorno", DateTime.Now.Day));
        nome.Text = Preferences.Get("nome", "");
        tbnome.Text = $"{App.d["insert_the_name"]}: ";
        tbdata.Text = $"{App.d["insert_the_date"]}: ";
        calcola.Text = App.d["calculate"] as string;
        Title = App.d["application"] as string;
        augura.Text = App.d["augura"] as string;
    }

    private void calcola_Click(object sender, EventArgs e)
    {
        risultato.Text = "";
        anniversario.Text = "";
        nome.Text = nome.Text.Trim();
        DateTime d = DateTime.Now;
        TimeSpan differenza = d - data.Date;
        if (differenza.Milliseconds < 0)
        {
            risultato.Text = App.d["invalid_rvalue"] as string;
            return;
        }
        if (differenza.Days > 1)
        {
            if (d.Day == data.Date.Day)
            {
                if (d.Month == data.Date.Month)
                {
                    anniversario.Text = App.d["is_your_anniversary"] as string;
                    ricorrenza = "anniversary";
                }
                else
                {
                    anniversario.Text = App.d["is_your_mesiversary"] as string;
                    ricorrenza = "mesiversary";
                }
            }
        }
        if (nome.Text == "")
            risultato.Text = $"{differenza.Days} {App.d["days_are_passed"]}";
        else
            risultato.Text = $"{App.d["you_meet"]} {nome.Text} {App.d["about"]} {differenza.Days} {App.d["days_ago"]}.";
        if (ricorrenza!="" && nome.Text!="")
            augura.IsVisible = true;
        Preferences.Set("giorno", data.Date.Day);
        Preferences.Set("mese", data.Date.Month);
        Preferences.Set("anno", data.Date.Year);
        Preferences.Set("nome", nome.Text);
    }

    private void augura_Click(object sender, EventArgs e)
    {
        augura.IsVisible=false;
        Browser.Default.OpenAsync($"https://twitter.com/intent/tweet?text=Happy%20{ricorrenza}%20my%20love.");
        ricorrenza = "";
    }
}

