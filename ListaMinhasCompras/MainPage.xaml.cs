using System.Collections.ObjectModel;
using ListaMinhasCompras.Models;

namespace ListaMinhasCompras
{
    public partial class MainPage : ContentPage
    {
        ObservableCollection<Produto> lista_produtos = new ObservableCollection<Produto>();

        public MainPage()
        {
            InitializeComponent();
            lst_produtos.ItemsSource = lista_produtos;
        }

        private void ToolbarItem_Clicked_somar(object sender, EventArgs e)
        {
            double soma = lista_produtos.Sum(i => i.Total);
            string msg = $"Total dos produtos é {soma:C}";
            DisplayAlert("Resultado", msg, "Fechar");
        }

        private async void ToolbarItem_Clicked_add(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.NovoProduto());
        }

        protected async override void OnAppearing()
        {
            if (lista_produtos.Count == 0)
            {
                List<Produto> tmp = await App.Database.GetAll();
                foreach (Produto p in tmp) { 
                    lista_produtos.Add(p);
                }

            }
        }

        private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            string q = e.NewTextValue;
            lista_produtos.Clear();

            List<Produto> tmp = await App.Database.Search(q);
            foreach (Produto p in tmp)
            {
                lista_produtos.Add(p);
            }
        }

        private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Produto? p = e.SelectedItem as Produto;

            Navigation.PushAsync(new Views.EditarProduto {
                BindingContext = p
            });
        }

        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                MenuItem selecionado = (MenuItem)sender;
                Produto p =  selecionado.BindingContext as Produto;

                bool comfirm = await DisplayAlert("TEm certeza?", "Remover Produto?", "Sim", "Não");

                if (comfirm) {
                    await App.Database.Delete(p.Id);
                    await DisplayAlert("Sucesso!", "Produto removido", "Ok");
                    lista_produtos.Remove(p);
                
                }

            } catch (Exception ex)
            {
                //Code
                await DisplayAlert("Ops", ex.Message, "Fechar");
            }
        }
    }


}
