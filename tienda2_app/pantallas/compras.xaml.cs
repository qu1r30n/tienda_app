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
    public partial class compras : ContentPage
    {
        Tex_base bas = new Tex_base();
        List<string> datos = new List<string>();
        public compras()
        {
            InitializeComponent();
            leer_informacion();

            

        }

        private void leer_informacion()
        {
            string directorio = "storage/emulated/0/Android/data/com.companyname.tienda2_app/";
            string direccion1 = directorio + "inf/inventario/invent.txt";
            string[] nom_prov = bas.Leer(direccion1,"0");
            for (int i = 0; i < nom_prov.Length; i++)
            {
                datos.Add(nom_prov[i]);
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

            input.ItemsSource = GetSuggestions(input.Text);
        }

        private List<string> GetSuggestions(string input)
        {
            return string.IsNullOrWhiteSpace(input) ? null : datos.Where(c => c.StartsWith(input, StringComparison.InvariantCultureIgnoreCase)).ToList();
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