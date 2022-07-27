using System;
using System.Collections.Generic;
using System.Text;

namespace tienda_app2.clases
{
    internal class modelos
    {
        Tex_base bas = new Tex_base();
        string directorio = "storage/emulated/0/Android/data/com.companyname.tienda_app2/";
        string[] info_inv;

        public string[] ordenar_mejores_propuestas_compras(string codigo_barras, string provedor_actual, string precio_texto, string cantidad_texto)
        {

            double precio_numero_actual = Convert.ToDouble(precio_texto);

            double cantidad_numero_actual = Convert.ToDouble(cantidad_texto);

            double precio_por_cantidad = precio_numero_actual * cantidad_numero_actual;

            DateTime fecha_hora = DateTime.Now; //se usara la variable fecha y hora para sacar el dia de hoy y la hora
            string año_mes_dia = fecha_hora.ToString("yyyyMMdd");



            //resultado = "75000011";//PRUEBA



            string direccion2 = directorio + "inf/produc_cod_bar/historial/" + codigo_barras + ".txt";
            bas.Crear_archivo_y_directorio(direccion2, "provedor|costo_unitario¬fecha°costo_unitario¬fecha|");

            string investigador_eliminar = "";


            string[] historial_provedores = bas.Leer(direccion2);
            bool bandera = false;
            List<string> precios_ordenardos = new List<string>();
            for (int i = 1; i < historial_provedores.Length; i++)
            {

                string[] provedores = historial_provedores[i].Split('|');

                if (provedores[0] == provedor_actual)
                {
                    provedores[1] = investigador_eliminar = precio_numero_actual + "¬" + año_mes_dia + "°" + provedores[1];
                    historial_provedores[i] = string.Join("|", provedores);
                    bandera = true;

                    bas.eliminar_archivo(direccion2);
                    bas.Crear_archivo_y_directorio(direccion2);
                    for (int k = 0; k < historial_provedores.Length; k++)
                    {
                        bas.Agregar(direccion2, historial_provedores[k]);
                    }
                }
            }
            if (bandera == false)
            {
                bas.Agregar(direccion2, provedor_actual + "|" + precio_texto + "¬" + año_mes_dia + "|");
            }


            historial_provedores = bas.Leer(direccion2);

            for (int i = 1; i < historial_provedores.Length; i++)
            {

                for (int j = i + 1; j < historial_provedores.Length; j++)
                {

                    string[] provedores_arriba = historial_provedores[i].Split('|');
                    string[] historiales_precios_arriba = provedores_arriba[1].Split('°');
                    string[] cantidad_fecha_arriba = historiales_precios_arriba[0].Split('¬');


                    string[] provedores_abajo = historial_provedores[j].Split('|');
                    string[] historiales_precios_abajo = provedores_abajo[1].Split('°');
                    string[] cantidad_fecha_abajo = historiales_precios_abajo[0].Split('¬');

                    if (Convert.ToDouble(cantidad_fecha_arriba[0]) > Convert.ToDouble(cantidad_fecha_abajo[0]))
                    {
                        string temp = historial_provedores[i];
                        historial_provedores[i] = historial_provedores[j];
                        historial_provedores[j] = temp;

                    }

                }
            }

            bas.eliminar_archivo(direccion2);
            bas.Crear_archivo_y_directorio(direccion2);
            for (int i = 0; i < historial_provedores.Length; i++)
            {
                bas.Agregar(direccion2, historial_provedores[i]);
            }

            return historial_provedores;

        }

        public void inventario(string codigo, string cantidad, string nombre_produc)
        {


            DateTime fecha_hora = DateTime.Now;
            string año_mes_dia = fecha_hora.ToString("yyyyMMdd");
            string hora_min_seg = fecha_hora.ToString("HHmmss");
            string inventario_de_hoy = directorio + "hacer_inventarios/" + año_mes_dia + "_inventario.txt";

            bas.Crear_archivo_y_directorio(inventario_de_hoy);
            bas.Agregar(inventario_de_hoy, codigo + "|" + nombre_produc + "|" + cantidad + "|" + hora_min_seg + "|");
        }

        private string[] ordenar_arreglo(string[] arreglo, int columna, string orden = "menor_mayor", char caracter_separacion = '|')
        {
            if (orden == "menor_mayor")
            {
                for (int i = 0; i < arreglo.Length; i++)
                {
                    for (int j = i + 1; j < arreglo.Length; j++)
                    {

                        string[] arreglo_espliteado = arreglo[i].Split(caracter_separacion);
                        double arriba = Convert.ToDouble(arreglo_espliteado[columna]);
                        string[] arreglo_espliteado2 = arreglo[j].Split(caracter_separacion);
                        double abajo = Convert.ToDouble(arreglo_espliteado2[columna]);
                        string temp;
                        if (arriba > abajo)
                        {
                            temp = arreglo[i];
                            arreglo[i] = arreglo[j];
                            arreglo[j] = temp;
                        }
                    }
                }
            }


            else if (orden == "mayor_menor")
            {
                for (int i = 0; i < arreglo.Length; i++)
                {
                    for (int j = i + 1; j < arreglo.Length; j++)
                    {

                        string[] arreglo_espliteado = arreglo[i].Split(caracter_separacion);
                        double arriba = Convert.ToDouble(arreglo_espliteado[columna]);
                        string[] arreglo_espliteado2 = arreglo[j].Split(caracter_separacion);
                        double abajo = Convert.ToDouble(arreglo_espliteado2[columna]);
                        string temp;
                        if (arriba < abajo)
                        {
                            temp = arreglo[i];
                            arreglo[i] = arreglo[j];
                            arreglo[j] = temp;
                        }
                    }
                }
            }



            return arreglo;
        }




    }

}
