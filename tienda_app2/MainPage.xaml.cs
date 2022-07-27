using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using tienda_app2.clases;

namespace tienda_app2
{
    public partial class MainPage : ContentPage
    {
        Tex_base bas = new Tex_base();

        public MainPage()
        {
            InitializeComponent();

            //crea los archivos de compras por si se hace una busqueda y no estan
            DateTime fecha_hora = DateTime.Now; //se usara la variable fecha y hora para sacar el dia de hoy y la hora
            string año_mes_dia = fecha_hora.ToString("yyyyMMdd");


            string directorio = "storage/emulated/0/Android/data/com.companyname.tienda_app2/";


            string direccion1, direccion2, direccion3, direccion4, direccion5; //variables de direcciones

            direccion1 = directorio + "inf/inventario/invent.txt";
            direccion2 = directorio + "inf/inventario/provedores.txt";
            bas.Crear_archivo_y_directorio(direccion1, "id|producto|precio_de_venta|0|cantidad|costo_compra|marca|grupo|multiusos|cantidad_productos_por_paquete");
            bas.Crear_archivo_y_directorio(direccion2, "provedor|id|");

            /*
            bas.Agregar(direccion1, "1|pomada ceresa|5|1|9|1|pomada labiual|2°producto||1|");
            bas.Agregar(direccion1, "2|ariel jabon 250g|11.5|11.5|0|10.35||1°producto||1|");
            bas.Agregar(direccion1, "3|chicle orbit 5.6g|2.5|2201302|0|1|orbit|1°producto||1|");
            bas.Agregar(direccion1, "4|orbit hierbabuena 5.6g|2.5|2261003|99|2.5|orbit|1°producto||1|");
            bas.Agregar(direccion1, "5|bubulubu  35g|9|7432354|23|8.1|ricolino|1°producto||1|");
            bas.Agregar(direccion1, "6|leche nesquik chocolate 240ml|9|75000011|3|7.2|nesquik|1°producto||7|");
            */
            direccion2 = directorio + "inf/inventario/pru/provedores_pru_cmb.txt";
            bas.Crear_archivo_y_directorio(direccion2);


            direccion1 = directorio + "ventas/" + fecha_hora.ToString("yyyy") + "/" + fecha_hora.ToString("MM") + "/dias/" + fecha_hora.ToString("yyyyMMdd") + ".txt"; // direccion1= ventas/2016/11/dias/28-11-2016.Txt
            direccion2 = directorio + "ventas/" + fecha_hora.ToString("yyyy") + "/" + fecha_hora.ToString("MM") + "/" + fecha_hora.ToString("MM") + ".txt"; // direccion2= ventas/2016/11/11.Txt
            direccion3 = directorio + "ventas/" + fecha_hora.ToString("yyyy") + "/" + fecha_hora.ToString("yyyy") + ".txt"; // direccion3 = ventas/2016/2016.Txt
            direccion4 = directorio + "ventas/total_años.txt"; // no hace falta explicacion
            direccion5 = directorio + "ventas/total_en_juego.txt"; // no hace falta explicacion
            bas.Crear_archivo_y_directorio(direccion1);//aqui si no existe los directorios  los crea y si existen entra  lo mismo con el archivo
            bas.Crear_archivo_y_directorio(direccion2);//aqui si no existe los directorios  los crea y si existen entra  lo mismo con el archivo
            bas.Crear_archivo_y_directorio(direccion3);//aqui si no existe los directorios  los crea y si existen entra  lo mismo con el archivo
            bas.Crear_archivo_y_directorio(direccion4);//aqui si no existe los directorios  los crea y si existen entra  lo mismo con el archivo
            bas.Crear_archivo_y_directorio(direccion5);//aqui si no existe los directorios  los crea y si existen entra  lo mismo con el archivo

            direccion1 = directorio + "ventas/" + fecha_hora.ToString("yyyy") + "/" + fecha_hora.ToString("MM") + "/dias/g_" + fecha_hora.ToString("yyyyMMdd") + ".txt"; //aqui lo que cambia es la g_ antes del archivo direccion1= ventas/2016/11/dias/g_28-11-2016.Txt
            direccion2 = directorio + "ventas/" + fecha_hora.ToString("yyyy") + "/" + fecha_hora.ToString("MM") + "/g_" + fecha_hora.ToString("MM") + ".txt";//aqui lo que cambia es la g_ antes del archivo direccion1= ventas/2016/11/g_11.Txt
            direccion3 = directorio + "ventas/" + fecha_hora.ToString("yyyy") + "/g_" + fecha_hora.ToString("yyyy") + ".txt";//aqui lo que cambia es la g_ antes del archivo direccion1= ventas/2016/g_2016.Txt
            direccion4 = directorio + "ventas/g_total_años.txt";//no hace falta explicacion o si?
            bas.Crear_archivo_y_directorio(direccion1);//aqui si no existe los directorios  los crea y si existen entra  lo mismo con el archivo
            bas.Crear_archivo_y_directorio(direccion2);//aqui si no existe los directorios  los crea y si existen entra  lo mismo con el archivo
            bas.Crear_archivo_y_directorio(direccion3);//aqui si no existe los directorios  los crea y si existen entra  lo mismo con el archivo
            bas.Crear_archivo_y_directorio(direccion4);//aqui si no existe los directorios  los crea y si existen entra  lo mismo con el archivo

            direccion1 = directorio + "ventas/" + fecha_hora.ToString("yyyy") + "/" + fecha_hora.ToString("MM") + "/dias/p_" + fecha_hora.ToString("yyyyMMdd") + ".txt";//aqui lo que cambia es la p_ antes del archivo direccion1= ventas/2016/11/dias/p_28-11-2016.Txt
            direccion2 = directorio + "ventas/" + fecha_hora.ToString("yyyy") + "/" + fecha_hora.ToString("MM") + "/p_" + fecha_hora.ToString("MM") + ".txt";//aqui lo que cambia es la p_ antes del archivo direccion1= ventas/2016/11/p_11.Txt
            direccion3 = directorio + "ventas/" + fecha_hora.ToString("yyyy") + "/p_" + fecha_hora.ToString("yyyy") + ".txt";//aqui lo que cambia es la p_ antes del archivo direccion1= ventas/2016/p_2016.Txt
            direccion4 = directorio + "ventas/p_total_años.txt";
            bas.Crear_archivo_y_directorio(direccion1);//aqui si no existe los directorios  los crea y si existen entra  lo mismo con el archivo
            bas.Crear_archivo_y_directorio(direccion2);//aqui si no existe los directorios  los crea y si existen entra  lo mismo con el archivo
            bas.Crear_archivo_y_directorio(direccion3);//aqui si no existe los directorios  los crea y si existen entra  lo mismo con el archivo
            bas.Crear_archivo_y_directorio(direccion4);//aqui si no existe los directorios  los crea y si existen entra  lo mismo con el archivo

            direccion1 = directorio + "inf/inventario/img/";
            bas.Crear_archivo_y_directorio(direccion1);//crear la carpeta que contendra las imagenes de codigos de barras

            direccion1 = directorio + "inf/us/ad.txt";
            bas.Crear_archivo_y_directorio(direccion1);

            direccion1 = directorio + "inf/us/encargado.txt";
            bas.Crear_archivo_y_directorio(direccion1);

            direccion1 = directorio + "inf/us/usuario.txt";
            bas.Crear_archivo_y_directorio(direccion1);

            direccion1 = directorio + "inf/us/invitado.txt";
            bas.Crear_archivo_y_directorio(direccion1);

            direccion1 = directorio + "inf/us/simul.txt";
            bas.Crear_archivo_y_directorio(direccion1);


        }

        async private void btn_inventario(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new pantallas.inventario());

        }

        async private void btn_ventas(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new pantallas.ventas());
        }

        async private void btn_investigacion_merc(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new pantallas.compras());
        }

        async private void btn_cambiar_info(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new pantallas.cambio_info());
        }

        async private void btn_pedidos_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new pantallas.pedidos());
        }
    }
}
