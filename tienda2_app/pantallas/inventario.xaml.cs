using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using tienda2_app.clases;

namespace tienda2_app.pantallas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class inventario : ContentPage
    {
        modelos mod = new modelos();
        public inventario()
        {
            InitializeComponent();
        }

        private void btn_empezar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new qrscaners.qr_invent());
        }

        private async void txt_codigo_Completed(object sender, EventArgs e)
        {
            string cantidad_texto = await DisplayPromptAsync("cantidad", "cantidad: ", keyboard: Keyboard.Numeric);
            if (cantidad_texto != "") 
            {
                mod.inventario(txt_codigo.Text, cantidad_texto);
            }
            
            
        }
    }
}