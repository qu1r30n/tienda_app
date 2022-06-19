using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using tienda2_app.clases;

namespace tienda2_app.qrscaners
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class qr_invent : ContentPage
    {
        Tex_base bas = new Tex_base();
        public qr_invent()
        {
            InitializeComponent();
        }

        private void scanView_OnScanResult(ZXing.Result result)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                
                modelos mod = new modelos();
                
                string cantidad_texto = await DisplayPromptAsync("cantidad", "cantidad: ",keyboard: Keyboard.Numeric);
                if (cantidad_texto!=null)
                {
                    mod.inventario(result.Text, cantidad_texto);
                }
                

            });
        }

    }
}