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
        int g_total_de_productos_list = 0;
        string g_provedor = "";


        public pedidos()
        {
            InitializeComponent();
            preguntas_iniciales();
            leer_informacion();
            //lstv_productos.ItemsSource = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2" };
            DateTime fecha = DateTime.Now;
            string año_mes_dia = fecha.ToString("yyyyMMdd");
            string dir_archivo = directorio + "pedidos/" + año_mes_dia + "_pedido.txt";
            bas.Crear_archivo_y_directorio(dir_archivo);
            string[] pedido=bas.Leer(dir_archivo);
            g_total_de_productos_list=pedido.Length;
            herramientas_page.listview_agregar_arreglo(lstv_productos, pedido);
            //herramientas_page.listview_agregar(lstv_productos, "4|5|6");
            //herramientas_page.listview_eliminar(lstv_productos, 2);
        }


        async private void btn_agregar_Clicked(object sender, EventArgs e)
        {

            var seleccion = lstv_productos.SelectedItem;
            string[] seleccion_esp_sacar_indice = seleccion.ToString().Split('¬');
            string[] info_produc = seleccion_esp_sacar_indice[1].Split('|');
            string precio_text = "";
            string cantidad_text = "";
            while (precio_text==""||precio_text==null)
            {
                precio_text = await DisplayPromptAsync("precio", "precio: ", keyboard: Keyboard.Numeric);
                
                if (precio_text == "" || precio_text == null)
                {
                    bool res = await DisplayAlert("QUIERES SALIR?", "QUIERES SALIR?", "Ok", "Cancel");
                    if (res == true)
                    {
                        return;
                    }
                }
            }
            while (cantidad_text==""||cantidad_text==null)
            {
                cantidad_text = await DisplayPromptAsync("cantidad", "cantidad: ", keyboard: Keyboard.Numeric);
                
                if (cantidad_text == "" || cantidad_text == null)
                {
                    bool res = await DisplayAlert("QUIERES SALIR?", "QUIERES SALIR?", "Ok", "Cancel");
                    if (res == true)
                    {
                        return;
                    }
                }
            }
            
            
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
            string precio_text = "";
            while (precio_text==""||precio_text==null)
            {
                precio_text = await DisplayPromptAsync("precio", "precio: ", keyboard: Keyboard.Numeric);
                if (precio_text == "" || precio_text == null)
                {
                    bool res = await DisplayAlert("QUIERES SALIR?", "QUIERES SALIR?", "Ok", "Cancel");
                    if (res == true)
                    {
                        return;
                    }
                }
            }           
            string cantidad_texto="";
            while (cantidad_texto == ""||cantidad_texto==null) 
            {
                cantidad_texto = await DisplayPromptAsync("cantidad", "cantidad: ", keyboard: Keyboard.Numeric);
                
                if (cantidad_texto == "" || cantidad_texto == null)
                {
                    bool res = await DisplayAlert("QUIERES SALIR?", "QUIERES SALIR?", "Ok", "Cancel");
                    if (res == true)
                    {
                        return;
                    }
                }
                
            }
            
            
            
            string producto_suges = g_total_de_productos_list+"¬"+cantidad_texto + "|" + txt_provedor_suggest.Text + "|" + precio_text + "|" + cantidad_texto;
            g_total_de_productos_list = g_total_de_productos_list + 1;
            
            bool encontro_producto = false;
            string[] codigo_del_txt_prov_su = txt_provedor_suggest.Text.Split('|');

            foreach (var elemento in lstv_productos.ItemsSource)
            {
                string[] seleccion_esp_sacar_indice = elemento.ToString().Split('¬');
                string[] info_produc = seleccion_esp_sacar_indice[1].Split('|');
                
                if (info_produc[2] == codigo_del_txt_prov_su[0])
                {
                    
                    encontro_producto = true;
                    string[] elemento_a_agregar= { elemento.ToString() };
                    //posicion_listview¬cantidad_a_comprar|producto|codbar|provedor|cant_invent|precio_comp_inv|precio_actual|cantidad_comprada
                    herramientas_page.listview_agregar_arreglo_sin_cont(lstv_productos2, elemento_a_agregar);

                }
            }
            
            if (encontro_producto == false)
            {
                string direccion_inv = directorio + "inf/inventario/invent.txt";
                //id_0|producto_1|precio_de_venta_2|0_3|cantidad_4|costo_compra_5|provedor_6|grupo_7|multiusos_8|cantidad_productos_por_paquete_9|
                string producto = "";
                producto = bas.Seleccionar(direccion_inv, 3, codigo_del_txt_prov_su[0]);

                if (producto != "")
                {
                    
                    encontro_producto = true;
                    string[] producto_espliteado = producto.Split('|');

                    //producto_espliteado//inventario//id_0|producto_1|precio_de_venta_2|0_3|cantidad_4|costo_compra_5|provedor_6|grupo_7|multiusos_8|cantidad_productos_por_paquete_9|
                    //posicion_listview¬cantidad_a_comprar|producto|codbar|provedor|cant_invent|precio_comp_inv|precio_actual|cantidad_comprada
                    string[] item_a_agregar = new string[] { g_total_de_productos_list + "¬" + cantidad_texto + "|" + producto_espliteado[1] + "|" + producto_espliteado[3] + "|" + producto_espliteado[6] + "|" + producto_espliteado[4] + "|" + producto_espliteado[5] + "|" + precio_text + "|" + cantidad_texto };
                    herramientas_page.listview_agregar_arreglo_sin_cont(lstv_productos2, item_a_agregar);
                }
            }

            if (encontro_producto == false)
            {
                await DisplayAlert("producto nuevo", "producto nuevo no esta en el inventario","ok");
                string nombre_produc = await DisplayPromptAsync("nombre del producto", "nombre del producto: ");
                                                                                        //posicion_listview¬cantidad_a_comprar|producto|codbar|provedor|cant_invent|precio_comp_inv|precio_actual|cantidad_comprada
                string[] item_a_agregar = new string[] { g_total_de_productos_list + "¬" + cantidad_texto + "|" + nombre_produc + "|" + codigo_del_txt_prov_su[0] + "|" + g_provedor + "|" + cantidad_texto + "|" + precio_text + "|" + precio_text + "|" + cantidad_texto};
                herramientas_page.listview_agregar_arreglo_sin_cont(lstv_productos2, item_a_agregar);
            }



            
            if (cantidad_texto != "")
            {

                txt_provedor_suggest.Text = "";
                txt_provedor_suggest.Focus();
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
        async private void preguntas_iniciales()
        {
            string g_provedor = await DisplayPromptAsync("provedor", "provedor: ");
        }
        

    }
}