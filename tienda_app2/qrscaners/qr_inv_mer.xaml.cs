using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using tienda_app2.clases;

namespace tienda_app2.qrscaners
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class qr_inv_mer : ContentPage
    {

        string nom_provedor;

        Tex_base bas = new Tex_base();
        string directorio = "storage/emulated/0/Android/data/com.companyname.tienda_app2/";
        string[] info_inv;


        public qr_inv_mer()
        {
            InitializeComponent();

            mensage();
            string direccion1 = directorio + "inf/inventario/invent.txt";
            info_inv = bas.Leer(direccion1);

        }

        private void scanView_OnScanResult(ZXing.Result result)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {

                string precio_texto = await DisplayPromptAsync("precio", "precio: ", keyboard: Keyboard.Numeric);
                string cantidad_texto = await DisplayPromptAsync("cantidad", "cantidad: ", keyboard: Keyboard.Numeric);
                if (precio_texto != null)
                {
                    modelos mod = new modelos();
                    string[] mejores_prov = mod.ordenar_mejores_propuestas_compras(result.Text, nom_provedor, precio_texto, cantidad_texto);

                }

            });
        }


        public async void mensage()
        {
            nom_provedor = await DisplayPromptAsync("provedor", "provedor: ", placeholder: "provedor");
        }

    }
}