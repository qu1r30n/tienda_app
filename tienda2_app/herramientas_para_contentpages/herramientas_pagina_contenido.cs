using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using tienda2_app.clases;
using Xamarin.Forms;

namespace tienda2_app.herramientas_para_contentpages
{
    public class herramientas_pagina_contenido : ContentPage
    {
        Tex_base bas = new Tex_base();
        public herramientas_pagina_contenido()
        {
        }
        public void listview_agregar(ListView lstv_a_hacer, string texto_de_nuevo_registro, char caracter_separacion = '|')
        {
            string lista_estring = "";
            if (lstv_a_hacer.ItemsSource!=null)
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
            
            
            
            string[] nueva_lista = lista_estring.Split(caracter_separacion);

            for (int i = 0; i < nueva_lista.Length; i++)
            {
                string[]temp=nueva_lista[i].Split('°');
                if (temp.Length<2)
                {
                    nueva_lista[i] = i + "°" + nueva_lista[i];
                }
                
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
                    string[] temp = elementos.ToString().Split('°');
                    if (cont != indice_a_eliminar)
                    {
                        lista_estring = lista_estring + temp[1] + caracter_separacion;
                    }
                    cont++;
                }
            }
            lista_estring = bas.Trimend_paresido(lista_estring);



            if (lista_estring=="")
            {
                lstv_a_hacer.ItemsSource=null;
                return;
            }
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






    }
}