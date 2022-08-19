using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tienda_app2.clases;
using tienda_app2.herramientas_para_contentpages;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace tienda_app2.pantallas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class textbox_a_archivo : ContentPage
    {
        herramientas_pagina_contenido hr_page = new herramientas_pagina_contenido();
        Tex_base bas = new Tex_base();
        string nom_provedor = "";
        string directorio = "storage/emulated/0/Android/data/com.companyname.tienda_app2/";
        public textbox_a_archivo()
        {
            InitializeComponent();
            mensage();
        }

        private void txt_codbar_c_Completed(object sender, EventArgs e)
        {
            
            hr_page.listview_agregar(lstv_productos, txt_codbar_c.Text);
            txt_codbar_c.Text = "";
            txt_codbar_c.Focus();
        }
        public async void mensage()
        {
            nom_provedor = await DisplayPromptAsync("provedor", "provedor: ", placeholder: "provedor");
        }

        private void btn_proceder_Clicked(object sender, EventArgs e)
        {
            DateTime fecha_hora = DateTime.Now;
            string año_mes_dia = fecha_hora.ToString("yyyyMMdd");
            string dir_codigos_rapidos = directorio + "codigo_rapidos/" + año_mes_dia + nom_provedor + ".txt";
            bas.Crear_archivo_y_directorio(dir_codigos_rapidos);
            foreach (var elementos in lstv_productos.ItemsSource)
            {
                string[] temp =elementos.ToString().Split('°');
                bas.Agregar(dir_codigos_rapidos, temp[1]);
            }
            lstv_productos.ItemsSource = "";
            txt_codbar_c.Text = "";
            txt_codbar_c.Focus();
        }

        private void lstv_productos_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
    }
}