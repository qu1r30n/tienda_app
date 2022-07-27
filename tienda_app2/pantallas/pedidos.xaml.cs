using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using tienda_app2.clases;
using tienda_app2.herramientas_para_contentpages;

namespace tienda_app2.pantallas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class pedidos : ContentPage
    {
        List<string> g_datos = new List<string>();

        herramientas_pagina_contenido herramientas_page = new herramientas_pagina_contenido();

        Tex_base bas = new Tex_base();
        string g_index_sel;
        string g_index_sel_2;
        string directorio = "storage/emulated/0/Android/data/com.companyname.tienda_app2/";
        int total_de_productos_list = 0;
        public pedidos()
        {
            InitializeComponent();
            leer_informacion();
            //lstv_productos.ItemsSource = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2" };
            DateTime fecha = DateTime.Now;
            string año_mes_dia = fecha.ToString("yyyyMMdd");
            string dir_archivo = directorio + "pedidos/" + año_mes_dia + "_pedido.txt";
            bas.Crear_archivo_y_directorio(dir_archivo);
            string[] pedido=bas.Leer(dir_archivo);
            total_de_productos_list=pedido.Length;
            herramientas_page.listview_agregar_arreglo(lstv_productos, pedido);
            //herramientas_page.listview_agregar(lstv_productos, "4|5|6");
            //herramientas_page.listview_eliminar(lstv_productos, 2);
        }


        async private void btn_agregar_Clicked(object sender, EventArgs e)
        {

            var seleccion = lstv_productos.SelectedItem;
            string[] seleccion_esp_sacar_indice = seleccion.ToString().Split('¬');
            string[] info_produc = seleccion_esp_sacar_indice[1].Split('|');

            string precio_text = await DisplayPromptAsync("precio", "precio: ", keyboard: Keyboard.Numeric);
            string cantidad_text = await DisplayPromptAsync("cantidad", "cantidad: ", keyboard: Keyboard.Numeric);
            info_produc[6] = precio_text;
            info_produc[7] = cantidad_text;
            seleccion_esp_sacar_indice[1] = string.Join("|", info_produc);

            string[] item_a_agregar = new string[] { seleccion_esp_sacar_indice[0] + "¬" + seleccion_esp_sacar_indice[1] };
            herramientas_page.listview_agregar_arreglo_sin_cont(lstv_productos2, item_a_agregar);
            
            
            herramientas_page.listview_eliminar_mejorado(lstv_productos, seleccion_esp_sacar_indice[0]);
            g_index_sel = null;
            //DisplayAlert("bien", "" + seleccion, "ok");

            herramientas_page.listview_ordenar(lstv_productos, 0);
            herramientas_page.listview_ordenar(lstv_productos2, 0);

        }

        private void btn_remover_Clicked(object sender, EventArgs e)
        {


            var seleccion = lstv_productos2.SelectedItem;

            string[] item_a_agregar = new string[] { seleccion.ToString() };
            herramientas_page.listview_agregar_arreglo_sin_cont(lstv_productos, item_a_agregar);

            string[] seleccion_esp_sacar_indice = seleccion.ToString().Split('¬');
            herramientas_page.listview_eliminar_mejorado(lstv_productos2, seleccion_esp_sacar_indice[0]);
            g_index_sel = null;

            DisplayAlert("bien", "" + seleccion, "ok");

            herramientas_page.listview_ordenar(lstv_productos, 0);
            herramientas_page.listview_ordenar(lstv_productos2, 0);

        }
        private void btn_proceder_Clicked(object sender, EventArgs e)
        {
            DateTime fecha = DateTime.Now;
            string año_mes_dia = fecha.ToString("yyyyMMdd");
            string dir_archivo = directorio + "pedidos/" + año_mes_dia + "_ped_comprados.txt";
            foreach (var elementos in lstv_productos2.ItemsSource)
            {
                string[] elemento_esplit = elementos.ToString().Split('¬');
                bas.Agregar(dir_archivo, elemento_esplit[1]);

            }
        }

        private void lstv_productos_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            // en este metodo hago un cast sobre el elemento seleccionado. tenes que cambiar el (tuClase) ya que no pusiste con que clase rellenar el ListView
            var seleccion = lstv_productos.SelectedItem;
            string[] seleccion_espliteada = seleccion.ToString().Split('¬');
            g_index_sel = seleccion_espliteada[0];


        }

        private void lstv_productos2_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            // en este metodo hago un cast sobre el elemento seleccionado. tenes que cambiar el (tuClase) ya que no pusiste con que clase rellenar el ListView
            var seleccion = lstv_productos2.SelectedItem;
            string[] seleccion_espliteada = seleccion.ToString().Split('¬');
            g_index_sel_2 = seleccion_espliteada[0];

        }

        async private void txt_codigo_Completed(object sender, EventArgs e)
        {

            string esta_en_el_lstview = "";
            bool encontro_producto=false;
            foreach (var elemento in lstv_productos.ItemsSource)
            {
                string[] seleccion_esp_sacar_indice = elemento.ToString().Split('¬');
                string[] info_produc = seleccion_esp_sacar_indice[1].Split('|');
                if (info_produc[2]==txt_codigo.Text)
                {
                    esta_en_el_lstview = elemento.ToString();
                    encontro_producto = true;
                }
            }
            string producto = "";
            if (encontro_producto==false)
            {
                string direccion_inv = directorio + "inf/inventario/invent.txt";
                //id_0|producto_1|precio_de_venta_2|0_3|cantidad_4|costo_compra_5|provedor_6|grupo_7|multiusos_8|cantidad_productos_por_paquete_9|
                producto = bas.Seleccionar(direccion_inv, 3, txt_codigo.Text);
                if (producto == "")
                {
                    encontro_producto = true;
                }
            }

            if (encontro_producto==false)
            {
                string nombre_produc = await DisplayPromptAsync("nombre del producto", "nombre del producto: ");
            }
            
            

            string precio_text = await DisplayPromptAsync("precio", "precio: ", keyboard: Keyboard.Numeric);
            string cantidad_texto = await DisplayPromptAsync("cantidad", "cantidad: ", keyboard: Keyboard.Numeric);
            if (cantidad_texto != "")
            {
                
                txt_codigo.Text = "";
                txt_codigo.Focus();
            }

        }

        //----------------------------------------------------------------------------------------------
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

        async private void txt_provedo_suggest_QuerySubmitted(object sender, dotMorten.Xamarin.Forms.AutoSuggestBoxQuerySubmittedEventArgs e)
        {
            string precio_text = await DisplayPromptAsync("precio", "precio: ", keyboard: Keyboard.Numeric);
            string cantidad_texto = await DisplayPromptAsync("cantidad", "cantidad: ", keyboard: Keyboard.Numeric);

            string[] =txt_provedor_suggest.Text.Split('|');
            
            string producto_suges = total_de_productos_list+"¬"+cantidad_texto + "|" + txt_provedor_suggest.Text + "|" + precio_text + "|" + cantidad_texto;
            total_de_productos_list = total_de_productos_list + 1;
            
            string esta_en_el_lstview = "";
            bool encontro_producto = false;
            foreach (var elemento in lstv_productos.ItemsSource)
            {
                string[] seleccion_esp_sacar_indice = elemento.ToString().Split('¬');
                string[] info_produc = seleccion_esp_sacar_indice[1].Split('|');
                if (info_produc[2] == txt_codigo.Text)
                {
                    esta_en_el_lstview = elemento.ToString();
                    encontro_producto = true;
                }
            }
            string producto = "";
            if (encontro_producto == false)
            {
                string direccion_inv = directorio + "inf/inventario/invent.txt";
                //id_0|producto_1|precio_de_venta_2|0_3|cantidad_4|costo_compra_5|provedor_6|grupo_7|multiusos_8|cantidad_productos_por_paquete_9|
                producto = bas.Seleccionar(direccion_inv, 3, txt_codigo.Text);
                if (producto == "")
                {
                    encontro_producto = true;
                }
            }

            if (encontro_producto == false)
            {
                string nombre_produc = await DisplayPromptAsync("nombre del producto", "nombre del producto: ");
            }



            
            if (cantidad_texto != "")
            {

                txt_codigo.Text = "";
                txt_codigo.Focus();
            }

        }

        private void leer_informacion()
        {
            //lista_final//0_codigo|1_nombre_producto|2_codigo_de_barras|3_provedor|4_uso_multi_cantidad_invent|5_costo_compra|
            //0_cantidad_a_comprar|1_nombre_producto|2_codigo_de_barras|3_provedpor|4_uso_multi_cantidad_invent|5_costo_compra|6_precio_app|7_cantidad_app|
            string directorio = "storage/emulated/0/Android/data/com.companyname.tienda_app2/";
            string direccion1 = directorio + "inf/inventario/invent.txt";
            //inventario//id_0 | producto_1 | precio_de_venta_2 | 0_3 | cantidad_4 | costo_compra_5 | provedor_6 | grupo_7 | multiusos_8 | cantidad_productos_por_paquete_9 | _10 | _11
            string[] nom_prov = bas.Leer(direccion1, "3|1|5|6");
            
            for (int i = 0; i < nom_prov.Length; i++)
            {
                g_datos.Add(nom_prov[i]);
            }
        }
        //----------------------------------------------------------------------------------------------
    }
}