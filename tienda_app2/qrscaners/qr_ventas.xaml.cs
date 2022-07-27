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
    public partial class qr_ventas : ContentPage
    {

        string directorio = "storage/emulated/0/Android/data/com.companyname.tienda_app2/";

        string[] productos;

        Tex_base bas = new Tex_base();
        public qr_ventas()
        {
            string direccion1 = directorio + "inf/inventario/invent.txt";
            InitializeComponent();
            productos = bas.Leer(direccion1);
        }

        private void scanView_OnScanResult(ZXing.Result result)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                string direccion2 = directorio + "vendidos_temporal/ventas_tem.txt";
                await DisplayAlert("scaner_result", result.Text, "ok");
                string resultado = result.Text;
                //resultado = "75000011";
                bool bandera = false;

                for (int i = 0; i < productos.Length; i++)
                {
                    //id_0|producto_1|precio_de_venta_2|0_3|cantidad_4|costo_compra_5|provedor_6|grupo_7|multiusos_8|cantidad_productos_por_paquete_9|
                    string[] product_esplit = productos[i].Split('|');
                    if (product_esplit[3] == resultado)
                    {
                        bandera = true;

                        bool decicion = await DisplayAlert("producto", product_esplit[1] + ": " + product_esplit[2], "aceptar", "cancelar");
                        if (decicion)
                        {
                            bas.Agregar(direccion2, product_esplit[1] + ": " + product_esplit[2]);
                        }
                        break;

                    }
                }

                if (bandera == false)
                {

                }
            });
        }


    }
}