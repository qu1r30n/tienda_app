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
    public partial class qr_inventario : ContentPage
    {
        string directorio = "storage/emulated/0/Android/data/com.companyname.tienda_app2/";
        Tex_base bas = new Tex_base();
        modelos mod = new modelos();
        public qr_inventario()
        {
            InitializeComponent();
        }

        private void scanView_OnScanResult(ZXing.Result result)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {



                string direccion_inv = directorio + "inf/inventario/invent.txt";
                //id_0|producto_1|precio_de_venta_2|0_3|cantidad_4|costo_compra_5|provedor_6|grupo_7|multiusos_8|cantidad_productos_por_paquete_9|
                string nombre_produc = bas.Seleccionar(direccion_inv, 3, result.Text, "3");
                if (nombre_produc == "")
                {
                    nombre_produc = await DisplayPromptAsync("nombre del producto", "nombre del producto: ");
                }

                string cantidad_texto = await DisplayPromptAsync("cantidad", "cantidad: ", keyboard: Keyboard.Numeric);
                if (cantidad_texto != null)
                {
                    mod.inventario(result.Text, cantidad_texto, nombre_produc);
                }


            });
        }

    }
}