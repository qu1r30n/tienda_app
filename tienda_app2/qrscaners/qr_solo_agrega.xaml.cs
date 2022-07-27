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
    public partial class qr_solo_agrega : ContentPage
    {
        string nom_provedor;
        Tex_base bas = new Tex_base();
        string directorio = "storage/emulated/0/Android/data/com.companyname.tienda_app2/";
        string direccion1 = "";

        public qr_solo_agrega()
        {
            InitializeComponent();
            DateTime fecha_hora = DateTime.Now;
            direccion1 = directorio + nom_provedor + "_" + fecha_hora.ToString("yyyyMMdd") + ".txt";
        }

        private async void scanView_OnScanResult(ZXing.Result result)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                string costo_string = await DisplayPromptAsync("costo", "costo: ", keyboard: Keyboard.Numeric);

                bas.Crear_archivo_y_directorio(direccion1);
                if (costo_string != null)
                {
                    bas.Agregar(direccion1, result.Text + "|" + nom_provedor + "|" + costo_string);
                }

            });
        }

        public async void mensage()
        {
            nom_provedor = await DisplayPromptAsync("provedor", "provedor: ", placeholder: "provedor");
        }
    }
}