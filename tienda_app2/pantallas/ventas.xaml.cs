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
    public partial class ventas : ContentPage
    {
        Tex_base bas = new Tex_base();
        List<string> datos = new List<string>();
        public ventas()
        {
            InitializeComponent();
            leer_informacion();
        }

        private void leer_informacion()
        {
            string directorio = "storage/emulated/0/Android/data/com.companyname.tienda_app2/";
            string direccion1 = directorio + "inf/inventario/invent.txt";
            string[] registros = bas.Leer(direccion1);
            for (int i = 0; i < registros.Length; i++)
            {
                datos.Add(registros[i]);
            }
        }

        private void btn_buscar_con_cam_clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new qrscaners.qr_ventas());

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


        async private void checar_permisos()
        {
            var estatus_escr = await Xamarin.Essentials.Permissions.CheckStatusAsync<Xamarin.Essentials.Permissions.StorageWrite>();
            var estatus_lect = await Xamarin.Essentials.Permissions.CheckStatusAsync<Xamarin.Essentials.Permissions.StorageRead>();

            if (estatus_escr != Xamarin.Essentials.PermissionStatus.Granted)
            {
                var permiso_escr = await Xamarin.Essentials.Permissions.RequestAsync<Xamarin.Essentials.Permissions.StorageWrite>();
            }
            if (estatus_escr != Xamarin.Essentials.PermissionStatus.Granted)
            {
                return;
            }
            if (estatus_lect == Xamarin.Essentials.PermissionStatus.Granted)
            {
                var permiso_lect = await Xamarin.Essentials.Permissions.RequestAsync<Xamarin.Essentials.Permissions.StorageRead>();
            }
            if (estatus_lect == Xamarin.Essentials.PermissionStatus.Granted)
            {
                return;
            }
        }

        private void txt_codbar_TextChanged(object sender, dotMorten.Xamarin.Forms.AutoSuggestBoxTextChangedEventArgs e)
        {
            dotMorten.Xamarin.Forms.AutoSuggestBox input = (dotMorten.Xamarin.Forms.AutoSuggestBox)sender;

            input.ItemsSource = GetSuggestions(input.Text);

        }

        private List<string> GetSuggestions(string input)
        {
            return string.IsNullOrWhiteSpace(input) ? null : datos.Where(c => c.StartsWith(input, StringComparison.InvariantCultureIgnoreCase)).ToList();
        }

        private void txt_codbar_suggest_QuerySubmitted(object sender, dotMorten.Xamarin.Forms.AutoSuggestBoxQuerySubmittedEventArgs e)
        {

        }
    }
}