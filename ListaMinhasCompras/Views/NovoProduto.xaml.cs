using ListaMinhasCompras.Models;

namespace ListaMinhasCompras.Views;

public partial class NovoProduto : ContentPage
{
	public NovoProduto()
	{
		InitializeComponent();
	}

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            /*
             * Intacia um novo objeto do tipo produto
             * Passa os paramentros que e recebido pela view
            */
            Produto p = new Produto
            {
                Descricao = txt_descricao.Text,
                Quantidade = Convert.ToDouble(txt_quantidade.Text),
                Preco = Convert.ToDouble(txt_preco.Text),
            };

            /*
             * Acessa a conexao e acessa o metodo insert passa o produto
             * Exibi um alert
             * Rerirecionar para MainPage.
            */
            await App.Database.Insert(p);
            await DisplayAlert("Sucesso!", "Produto inserido", "OK");
            await Navigation.PushAsync(new MainPage());


        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}