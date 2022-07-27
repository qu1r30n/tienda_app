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
    public partial class qr_cambiar_info : ContentPage
    {
        Tex_base bas = new Tex_base();
        public qr_cambiar_info()
        {
            InitializeComponent();
        }

        private void scanView_OnScanResult(ZXing.Result result)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                string directorio = "storage/emulated/0/Android/data/com.companyname.tienda_app2/";
                string direccion1 = directorio + "inf/inventario/invent.txt";
                await DisplayAlert("scaner_result", result.Text, "ok");
                string resultado = result.Text;
                //resultado = "75000011";


                string info_produc = bas.Seleccionar(direccion1, 3, resultado);


                if (info_produc != "")
                {
                    globales_intercambios.G_string_info_comp = info_produc;
                    string[] produc_espl = info_produc.Split('|');
                    await DisplayAlert("informacion producto", produc_espl[1], "ok");
                }

                else
                {
                    string texto = await DisplayPromptAsync("no esta el producto", "nombre_producto?", placeholder: "nombre");
                }
            });
        }


    }
}