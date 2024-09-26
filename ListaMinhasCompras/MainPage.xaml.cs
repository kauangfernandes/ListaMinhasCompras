using System.Collections.ObjectModel;
using ListaMinhasCompras.Models;

namespace ListaMinhasCompras
{
    public partial class MainPage : ContentPage
    {
        //Cria um objeto do tipo produto e busca os set na variavel.
        ObservableCollection<Produto> lista_produtos = new ObservableCollection<Produto>();

        //Metodo a ser chamado na renderização da pagina.
        public MainPage()
        {
            InitializeComponent();
            lst_produtos.ItemsSource = lista_produtos;
        }

        private void ToolbarItem_Clicked_somar(object sender, EventArgs e)
        {
            /*
             * Metodo a ser chamado quando clicado, pega a lista de objetos Produto.
             * A função pecorrer a lista, aecessadno os campos e realizando operações de multiplicação.
             * Ao final exibi a menssagen e exibe.
            */
            double soma = lista_produtos.Sum(i => (i.Preco * i.Quantidade));
            string msg = $"O total é {soma:C}";
            DisplayAlert("Somatória", msg, "Fechar");
        }

        private async void ToolbarItem_Clicked_add(object sender, EventArgs e)
        {
            //Ao clicar no botão o usuário e direcionando para a view.
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

        //Metodo para bucar produtos
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

        //Listar produto selecionado e direcionado para editar o perfil
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

                bool comfirm = await DisplayAlert("Tem certeza?", "Remover Produto?", "Sim", "Não");

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
