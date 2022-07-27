using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using tienda_app2.clases;

namespace tienda_app2.pantallas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class compras : ContentPage
    {
        Tex_base bas = new Tex_base();
        List<string> g_datos = new List<string>();
        public compras()
        {
            InitializeComponent();
            leer_informacion();
        }

        private void leer_informacion()
        {
            string directorio = "storage/emulated/0/Android/data/com.companyname.tienda_app2/";
            string direccion1 = directorio + "inf/inventario/invent.txt";
            //id_0 | producto_1 | precio_de_venta_2 | 0_3 | cantidad_4 | costo_compra_5 | provedor_6 | grupo_7 | multiusos_8 | cantidad_productos_por_paquete_9 | _10 | _11
            string[] nom_prov = bas.Leer(direccion1, "3|1|5|6");
            
            for (int i = 0; i < nom_prov.Length; i++)
            {
                g_datos.Add(nom_prov[i]);
            }
        }

        private void btn_buscar_con_cam_clicked(object sender, EventArgs e)
        {
            globales_intercambios.G_string_info_comp = null;
            globales_intercambios.G_string_info_comp = txt_provedor_suggest.Text;
            //Navigation.PushAsync(new qrscaners.qr_solo_agrega());
            Navigation.PushAsync(new qrscaners.qr_inv_mer());
        }

        private void btn_agregar_clicked(object sender, EventArgs e)
        {

        }

        private void btn_procesar_clicked(object sender, EventArgs e)
        {

        }

        private void btn_eliminar_ultimo_clicked(object sender, EventArgs e)
        {

        }

        private void btn_eliminar_todo_clicked(object sender, EventArgs e)
        {

        }

        private void btn_eliminar_seleccionado_clicked(object sender, EventArgs e)
        {

        }


        private void txt_provedo_TextChanged(object sender, dotMorten.Xamarin.Forms.AutoSuggestBoxTextChangedEventArgs e)
        {
            dotMorten.Xamarin.Forms.AutoSuggestBox input = (dotMorten.Xamarin.Forms.AutoSuggestBox)sender;

            //input.ItemsSource = GetSuggestions(input.Text);
            input.ItemsSource = filtro(input.Text);


        }

        private List<string> filtro(string letras_escritas)
        {
            //g_datos es donde esta la informacion a mostrar
            return string.IsNullOrWhiteSpace(letras_escritas) ? null : g_datos.Where(c => c.StartsWith(letras_escritas, StringComparison.InvariantCultureIgnoreCase)).ToList();
        }

        private void txt_provedo_suggest_QuerySubmitted(object sender, dotMorten.Xamarin.Forms.AutoSuggestBoxQuerySubmittedEventArgs e)
        {
            
        }

        private async void txt_codbar_c_Completed(object sender, EventArgs e)
        {
            string precio_text = await DisplayPromptAsync("precio", "precio: ", keyboard: Keyboard.Numeric);
            string cantidad_texto = await DisplayPromptAsync("cantidad", "cantidad: ", keyboard: Keyboard.Numeric);
        }
    }
}