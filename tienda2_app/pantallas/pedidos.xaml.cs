using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using tienda2_app.clases;
using tienda2_app.herramientas_para_contentpages;

namespace tienda2_app.pantallas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class pedidos : ContentPage
    {
        herramientas_pagina_contenido herramientas_page = new herramientas_pagina_contenido();
        
        Tex_base bas = new Tex_base();
        string g_index_sel;
        string g_index_sel_2;
        string directorio = "storage/emulated/0/Android/data/com.companyname.tienda2_app/";

        public pedidos()
        {
            InitializeComponent();
            //lstv_productos.ItemsSource = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2" };
            
            herramientas_page.listview_agregar(lstv_productos, "1|2|3");
            //herramientas_page.listview_agregar(lstv_productos, "4|5|6");
            //herramientas_page.listview_eliminar(lstv_productos, 2);
        }

        
        private void btn_agregar_Clicked(object sender, EventArgs e)
        {
            int cont = 0;
            if (g_index_sel != null)
            {
                foreach (var item in lstv_productos.ItemsSource)
                {

                    if (cont == Convert.ToInt32(g_index_sel))
                    {

                        var seleccion = lstv_productos.SelectedItem;
                        string[] seleccion_esp_sacar_indice = seleccion.ToString().Split('°');
                        herramientas_page.listview_agregar(lstv_productos2, seleccion_esp_sacar_indice[1]);
                        herramientas_page.listview_eliminar(lstv_productos, Convert.ToInt32(seleccion_esp_sacar_indice[0]));
                        g_index_sel = null;
                        DisplayAlert("bien", "" + seleccion, "ok");
                    }
                    cont++;



                }
            }
            else
            {
                DisplayAlert("selecciona una opcion de arriba", "selecciona una opcion de arriba si no hay quiere decir que el archivo de pedido no se encuentra", "ok");
            }
            
        }
        private void btn_remover_Clicked(object sender, EventArgs e)
        {

            int cont = 0;
            if (g_index_sel_2 != null)
            {
                foreach (var item in lstv_productos2.ItemsSource)
                {

                    if (cont == Convert.ToInt32(g_index_sel_2))
                    {

                        var seleccion = lstv_productos2.SelectedItem;
                        string[] seleccion_esp_sacar_indice = seleccion.ToString().Split('°');
                        herramientas_page.listview_agregar(lstv_productos, seleccion_esp_sacar_indice[1]);
                        herramientas_page.listview_eliminar(lstv_productos2, Convert.ToInt32(seleccion_esp_sacar_indice[0]));
                        g_index_sel = null;
                        DisplayAlert("bien", "" + seleccion, "ok");
                    }
                    cont++;

                }
            }
            else
            {
                DisplayAlert("no se a seleccionado", "no se a seleccionado", "ok");
            }


        }
        private void btn_proceder_Clicked(object sender, EventArgs e)
        {
            
        }

        private void lstv_productos_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            // en este metodo hago un cast sobre el elemento seleccionado. tenes que cambiar el (tuClase) ya que no pusiste con que clase rellenar el ListView
            var seleccion = lstv_productos.SelectedItem;
            string[] seleccion_espliteada = seleccion.ToString().Split('°');
            g_index_sel = seleccion_espliteada[0];


        }

        private void lstv_productos2_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            // en este metodo hago un cast sobre el elemento seleccionado. tenes que cambiar el (tuClase) ya que no pusiste con que clase rellenar el ListView
            var seleccion = lstv_productos2.SelectedItem;
            string[] seleccion_espliteada = seleccion.ToString().Split('°');
            g_index_sel_2 = seleccion_espliteada[0];

        }
    }
}