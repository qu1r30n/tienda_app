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
    public partial class archivo_add_codigo_precio : ContentPage
    {
        string directorio = "storage/emulated/0/Android/data/com.companyname.tienda2_app/";
        string nom_provedor = "";
        Tex_base bas = new Tex_base();
        
        public archivo_add_codigo_precio()
        {
            InitializeComponent();
            mensage();
            
        }

        private async void txt_codbar_c_Completed(object sender, EventArgs e)
        {
            string direccion_inv = directorio + "inf/inventario/invent.txt";
            //id_0|producto_1|precio_de_venta_2|0_3|cantidad_4|costo_compra_5|provedor_6|grupo_7|multiusos_8|cantidad_productos_por_paquete_9|
            string nombre_produc = bas.Seleccionar(direccion_inv, 3, txt_codbar_c.Text, "3");
            if (nombre_produc == "")
            {
                nombre_produc = await DisplayPromptAsync("nombre del producto", "nombre del producto: ");
            }

            DateTime fecha_hora = DateTime.Now; //se usara la variable fecha y hora para sacar el dia de hoy y la hora
            string año_mes_dia = fecha_hora.ToString("yyyyMMdd");
            string direcion_info_guardar = directorio + "investigacion_a_enviar/" + año_mes_dia + "_" + nom_provedor + ".txt";
            string precio_text = await DisplayPromptAsync("precio", "precio: ", keyboard: Keyboard.Numeric);
            bas.Crear_archivo_y_directorio(direcion_info_guardar);
            bas.Agregar(direcion_info_guardar, txt_codbar_c.Text + "|" + precio_text + "|" + nombre_produc);
        }

        public async void mensage()
        {
            nom_provedor = await DisplayPromptAsync("provedor", "provedor: ", placeholder: "provedor");
        }
    }
}