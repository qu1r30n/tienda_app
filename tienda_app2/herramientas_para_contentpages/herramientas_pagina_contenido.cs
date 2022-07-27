using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using tienda_app2.clases;
using Xamarin.Forms;

namespace tienda_app2.herramientas_para_contentpages
{
    public class herramientas_pagina_contenido : ContentPage
    {
        Tex_base bas = new Tex_base();
        public herramientas_pagina_contenido()
        {
        }
        public void listview_agregar(ListView lstv_a_hacer, string texto_de_nuevo_registro, char caracter_separacion = '|')
        {
            //aqui agrega los nuevos registros en un string para luego esplitearlos y  y que ese sea lo que va a mostrar-----------
            string lista_estring = "";
            if (lstv_a_hacer.ItemsSource != null)
            {
                foreach (var elemento in lstv_a_hacer.ItemsSource)
                {
                    lista_estring = lista_estring + elemento + caracter_separacion;
                }

                lista_estring = bas.Trimend_paresido(lista_estring);
                lista_estring = lista_estring + caracter_separacion + texto_de_nuevo_registro;
            }
            else
            {
                lista_estring = texto_de_nuevo_registro;
            }

            //-----------------------------------------------------------------------------------------------------------------------------------

            string[] nueva_lista = lista_estring.Split(caracter_separacion);

            for (int i = 0; i < nueva_lista.Length; i++)
            {
                string[] temp = nueva_lista[i].Split('°');
                if (temp.Length < 2)
                {
                    nueva_lista[i] = i + "°" + nueva_lista[i];
                }

            }
            lstv_a_hacer.ItemsSource = nueva_lista;
        }

        public void listview_agregar_arreglo(ListView lstv_a_hacer, string[] texto_de_nuevo_registro)
        {
            List<string> nueva_lista = new List<string>();
            int cont = 0;
            if (lstv_a_hacer.ItemsSource!=null)
            {
                foreach (var elemento in lstv_a_hacer.ItemsSource)
                {
                    nueva_lista.Add(cont + "¬" + elemento);
                    cont++;
                }
            }
            
            foreach (var elemento in texto_de_nuevo_registro)
            {
                nueva_lista.Add(cont + "¬" + elemento);
                cont++;
            }

            
            lstv_a_hacer.ItemsSource = nueva_lista;
        }

        public void listview_agregar_arreglo_sin_cont(ListView lstv_a_hacer, string[] texto_de_nuevo_registro)
        {
            List<string> nueva_lista = new List<string>();
            
            if (lstv_a_hacer.ItemsSource != null)
            {
                foreach (var elemento in lstv_a_hacer.ItemsSource)
                {
                    nueva_lista.Add("" + elemento);
                    
                }
            }

            foreach (var elemento in texto_de_nuevo_registro)
            {
                nueva_lista.Add(""+ elemento);
                
            }


            lstv_a_hacer.ItemsSource = nueva_lista;
        }

        public int listview_cantidad_elementos(ListView lstv_a_hacer, int indice_a_eliminar)
        {
            int cont = 0;
            if (lstv_a_hacer.ItemsSource != null)
            {
                foreach (var elemento in lstv_a_hacer.ItemsSource)
                {
                    cont++;
                }
            }
            return cont;
        }

        public void listview_eliminar(ListView lstv_a_hacer, int indice_a_eliminar, char caracter_separacion = '|')
        {
            int cont = 0;
            string lista_estring = "";
            foreach (var elementos in lstv_a_hacer.ItemsSource)
            {
                if (lstv_a_hacer.ItemsSource != null)
                {
                    string[] temp = elementos.ToString().Split('¬');
                    if (cont != indice_a_eliminar)
                    {
                        lista_estring = lista_estring + temp[1] + caracter_separacion;
                    }
                    cont++;
                }
            }
            lista_estring = bas.Trimend_paresido(lista_estring);



            if (lista_estring == "")
            {
                lstv_a_hacer.ItemsSource = null;
                return;
            }
            string[] nueva_lista = lista_estring.Split(caracter_separacion);

            for (int i = 0; i < nueva_lista.Length; i++)
            {
                string[] temp = nueva_lista[i].Split('¬');
                if (temp.Length < 2)
                {
                    nueva_lista[i] = i + "¬" + nueva_lista[i];
                }
            }
            lstv_a_hacer.ItemsSource = nueva_lista;
        }

        public void listview_eliminar_mejorado(ListView lstv_a_hacer, string indice_a_eliminar, char caracter_separacion = '¬')
        {
            int cont = 0;
            List<string> indices = new List<string>();
            foreach (var elementos in lstv_a_hacer.ItemsSource)
            {
                if (lstv_a_hacer.ItemsSource != null)
                {
                    string[] temp = elementos.ToString().Split(caracter_separacion);
                    if (temp[0] != indice_a_eliminar)
                    {
                        indices.Add(elementos.ToString());
                    }
                    cont++;
                }
            }



            if (indices.Count==0)
            {
                lstv_a_hacer.ItemsSource = null;
                return;
            }
            

            
            lstv_a_hacer.ItemsSource = indices.ToArray();
        }

        public void listview_ordenar(ListView lstv_ordenar, int num_columna, string orden = "menor_a_mayor", char caracter_separacion = '¬')
        {

            List<string> lista_cargada = new List<string>();
            try
            {
                foreach (var elemento in lstv_ordenar.ItemsSource)
                {
                    lista_cargada.Add(elemento.ToString());
                }

                if (orden == "menor_a_mayor")
                {
                    for (int i = 0; i < lista_cargada.Count; i++)
                    {
                        for (int j = i + 1; j < lista_cargada.Count; j++)
                        {
                            string[] info_comparar_arriba_split = lista_cargada[i].Split(caracter_separacion);
                            string[] info_comparar_abajo_split = lista_cargada[j].Split(caracter_separacion);

                            double info_comparar_arriba = Convert.ToDouble(info_comparar_arriba_split[num_columna]);
                            double info_comparar_abajo = Convert.ToDouble(info_comparar_abajo_split[num_columna]);
                            if (info_comparar_arriba > info_comparar_abajo)
                            {
                                string temporal_cambio_info = lista_cargada[i];
                                lista_cargada[i] = lista_cargada[j];
                                lista_cargada[j] = temporal_cambio_info;
                            }
                        }
                    }
                }

                else if (orden == "mayor_a_menor")
                {
                    for (int i = 0; i < lista_cargada.Count; i++)
                    {
                        for (int j = i + 1; j < lista_cargada.Count; j++)
                        {
                            string[] info_comparar_arriba_split = lista_cargada[i].Split(caracter_separacion);
                            string[] info_comparar_abajo_split = lista_cargada[j].Split(caracter_separacion);

                            double info_comparar_arriba = Convert.ToDouble(info_comparar_arriba_split[num_columna]);
                            double info_comparar_abajo = Convert.ToDouble(info_comparar_abajo_split[num_columna]);
                            if (info_comparar_arriba < info_comparar_abajo)
                            {
                                string temporal_cambio_info = lista_cargada[i];
                                lista_cargada[i] = lista_cargada[j];
                                lista_cargada[j] = temporal_cambio_info;
                            }
                        }
                    }
                }

            }
            catch (Exception)
            {

                
            }


            lstv_ordenar.ItemsSource = lista_cargada.ToArray();

        }






    }
}