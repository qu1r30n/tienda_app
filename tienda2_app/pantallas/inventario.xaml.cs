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
        string directorio = "storage/emulated/0/Android/data/com.companyname.tienda2_app/";
        Tex_base bas = new Tex_base();
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
            string direccion_inv = directorio + "inf/inventario/invent.txt";
            //id_0|producto_1|precio_de_venta_2|0_3|cantidad_4|costo_compra_5|provedor_6|grupo_7|multiusos_8|cantidad_productos_por_paquete_9|
            string nombre_produc = bas.Seleccionar(direccion_inv, 3, txt_codigo.Text, "3");
            if (nombre_produc == "") 
            {
                nombre_produc=await DisplayPromptAsync("nombre del producto", "nombre del producto: ");
            }


            string cantidad_texto = await DisplayPromptAsync("cantidad", "cantidad: ", keyboard: Keyboard.Numeric);
            if (cantidad_texto != "") 
            {
                mod.inventario(txt_codigo.Text, cantidad_texto,nombre_produc);
                txt_codigo.Text = "";
                txt_codigo.Focus();
            }
            
        }
    }
}