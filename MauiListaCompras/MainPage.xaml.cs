using MauiListaCompras.Models;
using System.Collections.ObjectModel;

namespace MauiListaCompras
{
    public partial class MainPage : ContentPage
    {
        ObservableCollection<Produto> lista_produtos =
            new ObservableCollection<Produto>();

        public MainPage()
        {
            InitializeComponent();
            lst_produtos.ItemsSource = lista_produtos;
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            double soma = lista_produtos.Sum(i => (i.Preco * i.Quantidade));
            string msg = $"O total é {soma:C}";
            DisplayAlert("somatória" , msg , "Fechar");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (lista_produtos.Count == 0)
            {
                Task.Run(async () =>
                {
                    List<Produto> tmp = await App.Db.GetAll();
                    foreach (var p in tmp)
                    {
                        lista_produtos.Add(p);
                    }
                });
            }
        }

        private async void ToolbarItem_Clicked_Add(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//NovoProduto");
        }

        private void ToolbarItem_Clicked_Somar(object sender, EventArgs e)
        {

        }
    }

}
