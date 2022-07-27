﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;

namespace tienda_app2.clases
{
    internal class Tex_base
    {

        string G_palabra = "", G_entrando = "", G_temp = "";

        char[] G_parametros = { '|', '°', '¬', '^' };
        string[] G_linea, G_buscar, G_remplasar;

        public void Crear_archivo_y_directorio(string direccion_archivo, string valor_inicial = null, string[] columnas = null)//columnas: es para crearlas y se separan la columnas por un '|' valor_inicial: no se utilisa en este programa era para poner un tipo eslogan o un titulo  pero en este programa no lo nesesite
        {
            char[] parametro2 = { '/', '\\' };//estos seran los parametros de separacion de el split
            G_entrando = "";
            string[] direccion_espliteada = direccion_archivo.Split(parametro2);//spliteamos la direccion

            for (int i = 0; i < direccion_espliteada.Length; i++)//pasamos por todas las los directorios y archivo
            {
                if (i < direccion_espliteada.Length - 1)//el path muestra 6 palabras que fueron espliteadas se le resta uno por que los arreglos empiesan desde 0 y solo se le pone el menor que por que la ultima palabra es el archivo
                {
                    G_entrando = G_entrando + direccion_espliteada[i] + "/"; // va acumulando los directorios a los que va a entrar ejemplo: ventas/   ventas/2016    ventas/2016/        ventas/2016/11      ventas/2016/11/dias/  y no muestra el ultimo por que es el archivo y en el if  le dijimos que lo dejara en el penultimo
                    if (!Directory.Exists(G_entrando))//si el directorio no existe entrara y lo creara
                    {

                        Directory.CreateDirectory(G_entrando);//crea el directorio

                    }
                }
            }

            if (direccion_espliteada[direccion_espliteada.Length - 1] != "")//checa si escribio tambien el archivo o solo carpetas
            {
                if (!File.Exists(direccion_archivo))//si el archivo no existe entra y lo crea
                {
                    FileStream fs0 = new FileStream(direccion_archivo, FileMode.CreateNew);//crea una variable tipo filestream "fs0"  y crea el archivo
                    fs0.Close();//cierra fs0 para que se pueda usar despues



                    if (valor_inicial != null)// si al llamar a la funcion  le pusiste valor_inicial las escribe //se utilisa para que sea como un titulo o un eslogan pero lo utilisaremos en este prog
                    {
                        Agregar(direccion_archivo, valor_inicial);//escribe aqui el valor inicial si es que lo pusiste
                    }

                    if (columnas != null)//si al llamar a la funcion le pusistes columnas a agregar//recuerda que se separan por comas
                    {

                        string columnas_unidas = string.Join("" + G_parametros[0], columnas);
                        Agregar(direccion_archivo, columnas_unidas);//agrega las columnas

                    }

                }
            }

        }


        public string Seleccionar(string direccion_archivo, int num_column_comp, string comparar, string numero_columnas_extraer = null, char caracter_separacion = '|')
        {
            StreamReader sr = new StreamReader(direccion_archivo);
            string columna = "";
            while (sr.Peek() >= 0)//verificamos si hay mas lineas a leer
            {
                string linea = sr.ReadLine();//leemos linea y lo guardamos en palabra
                if (linea != null)
                {
                    string[] palabra = linea.Split(caracter_separacion);

                    if (palabra[num_column_comp] == comparar)
                    {
                        if (numero_columnas_extraer != null)
                        {
                            string[] columnas_extraer = numero_columnas_extraer.Split(caracter_separacion);
                            for (int i = 0; i < columnas_extraer.Length; i++)
                            {
                                int columna_extraer_numerico = Convert.ToInt32(columnas_extraer[i]);
                                if (i < columnas_extraer.Length - 1)
                                {
                                    columna = columna + palabra[columna_extraer_numerico] + caracter_separacion;
                                }
                                else
                                {
                                    columna = columna + palabra[columna_extraer_numerico];
                                }

                            }
                        }
                        else
                        {
                            columna = columna + linea;
                        }


                        columna = columna + "°";

                    }
                }
            }
            columna = Trimend_paresido(columna, '°');
            columna = Trimend_paresido(columna, '|');

            sr.Close();
            return columna;
        }

        public string Seleccionar_invertida_extraccion_columnas(string direccion_archivo, int num_column_comp, string comparar, string numero_columnas_saltar = null, char caracter_separacion = '|')
        {
            StreamReader sr = new StreamReader(direccion_archivo);
            string columna = "";
            while (sr.Peek() >= 0)//verificamos si hay mas lineas a leer
            {
                string linea = sr.ReadLine();//leemos linea y lo guardamos en palabra
                if (linea != null)
                {
                    string[] palabra = linea.Split(caracter_separacion);

                    if (palabra[num_column_comp] == comparar)
                    {
                        if (numero_columnas_saltar != null)
                        {
                            string[] columnas_saltar = numero_columnas_saltar.Split(caracter_separacion);
                            for (int j = 0; j < palabra.Length; j++)
                            {
                                for (int i = 0; i < columnas_saltar.Length; i++)
                                {
                                    int columna_saltar_entero = Convert.ToInt32(columnas_saltar[i]);
                                    if (j != columna_saltar_entero)
                                    {
                                        columna = columna + palabra[j] + caracter_separacion;

                                    }
                                }
                            }



                        }
                        else
                        {
                            columna = columna + linea;
                        }


                        columna = columna + "°";

                    }
                }
            }
            columna = Trimend_paresido(columna, '°');
            columna = Trimend_paresido(columna, '|');

            sr.Close();
            return columna;
        }

        public string Editar(string direccion_archivo, string buscar0, string remplasar0, string posiciones = null)
        {

            int cont;

            if (posiciones != null)
            {
                posiciones = Convierte_nom_col_a_numeros(posiciones, direccion_archivo);
            }
            string[] pos_str = posiciones.Split(G_parametros[0]);
            int[] pos_in = new int[pos_str.Length];

            for (int kl = 0; kl < pos_in.Length; kl++)
            {
                pos_in[kl] = Convert.ToInt32(pos_str[kl]);
            }
            G_buscar = buscar0.Split(G_parametros[0]);//espliteamos la busqueda
            G_remplasar = remplasar0.Split(G_parametros[0]);//esplitemos remplasar
            G_linea = direccion_archivo.Split('/');//esplitea la direccion
            G_temp = G_linea[0];//temp es igual al primer directorio
            for (int i = 1; i < G_linea.Length; i++)//checa si es el ultimo directorio 
            {
                if (i == G_linea.Length - 1)//si llego al archivo le va a colocar un temp_ y el nombre del archivo
                {
                    G_linea[i] = "temp_" + G_linea[i];
                }
                G_temp = G_temp + "/" + G_linea[i];//le pone la barrita para pasarselo a la funcion de crear achivos
            }
            Crear_archivo_y_directorio(G_temp);//creamos el archivo temporal

            StreamReader sr = new StreamReader(direccion_archivo);//abrimos el archivo a leer
            StreamWriter sw = new StreamWriter(G_temp);//abrimos el archivo a escribir
            try
            {
                string[] temp = new string[G_buscar.Length];
                while (sr.Peek() >= 0)//verificamos si hay mas lineas a leer
                {

                    G_palabra = sr.ReadLine();//leemos linea y lo guardamos en palabra
                    G_linea = G_palabra.Split(G_parametros[0]);//palabra la spliteamos y la guardamos en linea
                    if (G_palabra != "")//si palabra es "" se salta todo el proceso por que no se por que aparecen varios con ""
                    {
                        if (posiciones != null)
                        {
                            cont = 0;
                            for (int i = 0; i < G_buscar.Length; i++)//igual recorremos busqueda
                            {
                                for (int j = 0; j < G_buscar.Length; j++)//recorremos linea
                                {
                                    if (G_linea[pos_in[j]] == G_buscar[i])//si linea y busqueda son iguales
                                    {
                                        cont = cont + 1;
                                        if (cont == G_buscar.Length)
                                        {
                                            for (int pl = 0; pl < G_buscar.Length; pl++)
                                            {
                                                G_linea[pos_in[pl]] = G_remplasar[pl];//remplasamos la palabra de linea de su posision
                                            }




                                            G_palabra = "";
                                            for (int z = 0; z < G_linea.Length; z++)
                                            {

                                                if (z < G_linea.Length - 1)
                                                {
                                                    G_palabra = G_palabra + G_linea[z] + G_parametros[0];
                                                }
                                                else
                                                {
                                                    G_palabra = G_palabra + G_linea[z];
                                                }

                                            }
                                        }
                                    }
                                }
                            }
                        }

                        else
                        {
                            for (int i = 0; i < G_buscar.Length; i++)//igual recorremos linea
                            {
                                for (int j = 0; j < G_linea.Length; j++)//recorremos busqueda
                                {
                                    if (G_linea[j] == G_buscar[i])//si linea y busqueda son iguales
                                    {
                                        G_linea[j] = G_remplasar[i];//remplasamos la palabra de linea de su posision
                                        G_palabra = "";
                                        for (int kl = 0; kl < G_linea.Length; kl++)
                                        {
                                            if (kl < G_linea.Length - 1)
                                            {
                                                G_palabra = G_palabra + G_linea[kl] + G_parametros[0];
                                            }
                                            else
                                            {
                                                G_palabra = G_palabra + G_linea[kl];
                                            }
                                        }



                                    }
                                }
                            }
                        }
                        sw.WriteLine(G_palabra);//escribimos en el ARCHIVO TEMPORAL
                    }
                }

                sw.Close();//cerramos el archivo de escritura que es EL TEMPORAL
                sr.Close();//cerramos el archivo de lectura
                File.Delete(direccion_archivo);//borramos el archivo original
                File.Move(G_temp, direccion_archivo);//renombramos el archivo temporal por el que tenia el original
            }

            catch (Exception e)
            {
                sw.Close();//cerramos el archivo de escritura que es EL TEMPORAL
                sr.Close();//cerramos el archivo de lectura
                return "ERROR: " + e;
            }
            sw.Close();//cerramos el archivo de escritura que es EL TEMPORAL
            sr.Close();//cerramos el archivo de lectura
            return "se remplaso " + buscar0 + " por " + remplasar0;//retornamos que todo fue un exito
        }

        public string Editar_espesifico(string direccion_archivo, int num_column_comp, string comparar, string numero_columnas_editar, string editar_columna, char caracter_separacion = '|')
        {

            StreamReader sr = new StreamReader(direccion_archivo);
            string dir_tem = direccion_archivo.Replace(".txt", "_tem.txt");
            StreamWriter sw = new StreamWriter(dir_tem, true);
            string exito_o_fallo;

            try
            {
                while (sr.Peek() >= 0)//verificamos si hay mas lineas a leer
                {
                    string linea = sr.ReadLine();//leemos linea y lo guardamos en palabra
                    if (linea != null)
                    {
                        string[] palabra = linea.Split(caracter_separacion);

                        if (palabra[num_column_comp] == comparar)
                        {
                            string linea_editada = "";
                            string[] columnas_editar = numero_columnas_editar.Split(caracter_separacion);
                            string[] remplaso_dato = editar_columna.Split(caracter_separacion);
                            for (int i = 0; i < columnas_editar.Length; i++)
                            {
                                palabra[Convert.ToInt32(columnas_editar[i])] = remplaso_dato[i];
                            }
                            for (int i = 0; i < palabra.Length; i++)
                            {
                                linea_editada = linea_editada + palabra[i] + caracter_separacion;
                            }
                            linea_editada = Trimend_paresido(linea_editada, caracter_separacion);

                            sw.WriteLine(linea_editada);

                        }
                        else
                        {
                            sw.WriteLine(linea);
                        }
                    }
                }
                exito_o_fallo = "1)exito";
                sr.Close();
                sw.Close();
                File.Delete(direccion_archivo);//borramos el archivo original
                File.Move(dir_tem, direccion_archivo);//renombramos el archivo temporal por el que tenia el original

            }
            catch (Exception error)
            {
                sr.Close();
                sw.Close();
                exito_o_fallo = "2)error:" + error;
                File.Delete(dir_tem);//borramos el archivo original
            }
            return exito_o_fallo;
        }

        public string Editar_una_columna(string direccion_archivo, int columna, string info_editar, char caracter_separacion = '|')
        {
            StreamReader sr = new StreamReader(direccion_archivo);
            string dir_tem = direccion_archivo.Replace(".txt", "_tem.txt");
            StreamWriter sw = new StreamWriter(dir_tem, true);
            string exito_o_fallo;

            try
            {
                while (sr.Peek() >= 0)//verificamos si hay mas lineas a leer
                {
                    string linea = sr.ReadLine();//leemos linea y lo guardamos en palabra
                    string[] palabra = linea.Split(caracter_separacion);
                    palabra[columna] = info_editar;
                    string linea_editada = "";

                    for (int i = 0; i < palabra.Length; i++)
                    {
                        linea_editada = linea_editada + palabra[i] + caracter_separacion;
                    }
                    linea_editada = Trimend_paresido(linea_editada, caracter_separacion);

                    sw.WriteLine(linea_editada);
                }
                exito_o_fallo = "1)exito";
                sr.Close();
                sw.Close();
                File.Delete(direccion_archivo);//borramos el archivo original
                File.Move(dir_tem, direccion_archivo);//renombramos el archivo temporal por el que tenia el original
            }
            catch (Exception error)
            {
                sr.Close();
                sw.Close();
                exito_o_fallo = "2)error:" + error;
                File.Delete(dir_tem);//borramos el archivo original
            }


            return exito_o_fallo;
        }

        public void Agregar(string direccion_archivos, string agregando)
        {
            StreamWriter sw = new StreamWriter(direccion_archivos, true);
            sw.WriteLine(agregando);
            sw.Close();

        }

        public string Eliminar(string direccion_archivo, string buscar)
        {
            bool bandera = true;
            G_linea = direccion_archivo.Split('/');
            G_temp = G_linea[0];
            for (int i = 1; i < G_linea.Length; i++)
            {
                if (i == G_linea.Length - 1)
                {
                    G_linea[i] = "temp_" + G_linea[i];
                }
                G_temp = G_temp + "/" + G_linea[i];
            }
            Crear_archivo_y_directorio(G_temp);

            StreamReader sr = new StreamReader(direccion_archivo);
            StreamWriter sw = new StreamWriter(G_temp, true);
            int cont = 0;
            while (sr.Peek() >= 0)
            {
                cont++;
                G_palabra = sr.ReadLine();
                if (G_palabra != "")
                {
                    G_linea = G_palabra.Split(G_parametros[0]);

                    for (int i = 0; i < G_linea.Length; i++)
                    {

                        if (G_linea[i] == buscar)
                        {
                            bandera = false;
                        }

                    }
                    if (bandera)
                    {
                        sw.WriteLine(G_palabra);

                    }
                    bandera = true;

                }

            }
            sw.Close();
            sr.Close();
            try
            {
                File.Delete(direccion_archivo);
                File.Move(G_temp, direccion_archivo);
            }
            catch { }
            return buscar;
        }

        public string[] Leer_columnas(string direccion_archivo)
        {
            string[] linea, tem = { "" };
            StreamReader sr = new StreamReader(direccion_archivo);

            G_palabra = sr.ReadLine();
            try
            {
                linea = G_palabra.Split(G_parametros[0]);
            }
            catch (Exception)
            {
                linea = tem;
            }


            sr.Close();

            return linea;
        }


        public string[] Leer(string direccion_archivo, string pos_string = null, char caracter_separacion = '|')
        {
            ArrayList linea = new ArrayList();
            ArrayList resultado = new ArrayList();
            string[] pos_split;
            int[] posiciones;

            StreamReader sr = new StreamReader(direccion_archivo);

            if (pos_string == null)
            {

                while ((G_palabra = sr.ReadLine()) != null)
                {
                    if (G_palabra != "")
                    {
                        linea.Add(G_palabra);
                    }
                }
            }

            else
            {
                pos_split = pos_string.Split(caracter_separacion);
                posiciones = new int[pos_split.Length];
                for (int i = 0; i < posiciones.Length; i++)
                {
                    posiciones[i] = Convert.ToInt32(pos_split[i]);
                }


                for (int i = 0; (G_palabra = sr.ReadLine()) != null; i++)
                {
                    string[] spl_linea = G_palabra.Split(caracter_separacion);

                    G_palabra = "";
                    for (int j = 0; j < posiciones.Length; j++)
                    {
                        if (j < posiciones.Length - 1)
                        {
                            G_palabra = G_palabra + spl_linea[posiciones[j]] + caracter_separacion;
                        }
                        else
                        {
                            G_palabra = G_palabra + spl_linea[posiciones[j]];
                        }

                    }
                    resultado.Add(G_palabra);
                }
                sr.Close();
                string[] t = new string[resultado.Count];
                for (int mnm = 0; mnm < resultado.Count; mnm++)
                {
                    t[mnm] = "" + resultado[mnm];
                }
                return t;
            }

            sr.Close();
            string[] t2 = new string[linea.Count];
            for (int mnm = 0; mnm < linea.Count; mnm++)
            {
                t2[mnm] = "" + linea[mnm];
            }
            return t2;
        }

        public string Convierte_nom_col_a_numeros(string nom_columnas, string direccion_archivo)
        {
            bool decicion = false;
            string result = "";
            string[] col_spli = nom_columnas.Split(G_parametros[0]);
            string[] cantidad = Leer_columnas(direccion_archivo);

            for (int z = 0; z < col_spli.Length; z++)
            {
                for (int y = 0; y < cantidad.Length; y++)
                {
                    if (cantidad[y] == col_spli[z])
                    {
                        if (z < col_spli.Length - 1) //sele pone coma mientras no sea el ultimo
                        {
                            result = result + y + G_parametros[0];
                            decicion = true;
                        }
                        else//si es el ultimo no se le pone coma
                        {
                            result = result + y;
                            decicion = true;
                        }

                    }
                }
                if (decicion == false)
                {
                    int temp = Convert.ToInt32(col_spli[z]);
                    for (int y = 0; y < cantidad.Length; y++)
                    {
                        if (temp == y)
                        {
                            if (z < col_spli.Length - 1) //sele pone coma mientras no sea el ultimo
                            {
                                result = result + y + G_parametros[0];
                                decicion = true;
                            }
                            else//si es el ultimo no se le pone coma
                            {
                                result = result + y;
                                decicion = true;
                            }
                        }
                    }

                }
                if (decicion == false)
                {
                    return "error_columna";
                }
                decicion = false;
            }


            return result;//editar
        }

        public string Si_existe_suma_sino_desde_el_inventario_agrega(string direccion_archivo, int num_column_comp, string comparar, string numero_columnas_editar, string cantidad_a_sumar, string info_extra = "", char caracter_separacion = '|')
        {
            Crear_archivo_y_directorio(direccion_archivo);
            bool bandera = false;
            StreamReader sr = new StreamReader(direccion_archivo);
            string dir_tem = direccion_archivo.Replace(".txt", "_tem.txt");
            StreamWriter sw = new StreamWriter(dir_tem, true);
            string exito_o_fallo;

            try
            {

                while (sr.Peek() >= 0)//verificamos si hay mas lineas a leer
                {
                    string linea = sr.ReadLine();//leemos linea y lo guardamos en linea
                    if (linea != null)
                    {
                        string[] palabra = linea.Split(caracter_separacion);

                        if (palabra[num_column_comp] == comparar)
                        {
                            bandera = true;
                            string linea_editada = "";


                            if (info_extra != "")
                            {

                                string[] info_extra_spliteada = info_extra.Split(caracter_separacion);

                                if (info_extra_spliteada[0] == "1")
                                {
                                    numero_columnas_editar = numero_columnas_editar + "|7";
                                    cantidad_a_sumar = cantidad_a_sumar + "|" + (Convert.ToDecimal(info_extra_spliteada[1]) * Convert.ToDecimal(cantidad_a_sumar));
                                    string[] columnas_editar = numero_columnas_editar.Split(caracter_separacion);
                                    string[] cantidades_sumara = cantidad_a_sumar.Split(caracter_separacion);

                                    for (int i = 0; i < columnas_editar.Length; i++)
                                    {
                                        palabra[Convert.ToInt32(columnas_editar[i])] = "" + (Convert.ToInt32(palabra[Convert.ToInt32(columnas_editar[i])]) + Convert.ToInt32(cantidades_sumara[i]));//esta largo lo se. pero significa que a la columna a editar le va a sumar la cantidad señalada
                                    }
                                }
                                else
                                {
                                    string[] columnas_editar = numero_columnas_editar.Split(caracter_separacion);
                                    string[] cantidades_sumara = cantidad_a_sumar.Split(caracter_separacion);

                                    for (int i = 0; i < columnas_editar.Length; i++)
                                    {
                                        palabra[Convert.ToInt32(columnas_editar[i])] = "" + (Convert.ToInt32(palabra[Convert.ToInt32(columnas_editar[i])]) + Convert.ToInt32(cantidad_a_sumar));//esta largo lo se. pero significa que a la columna a editar le va a sumar la cantidad señalada
                                    }
                                }

                            }

                            else
                            {
                                string[] columnas_editar = numero_columnas_editar.Split(caracter_separacion);
                                string[] cantidades_sumara = cantidad_a_sumar.Split(caracter_separacion);

                                for (int i = 0; i < columnas_editar.Length; i++)
                                {
                                    palabra[Convert.ToInt32(columnas_editar[i])] = "" + (Convert.ToDecimal(palabra[Convert.ToInt32(columnas_editar[i])]) + Convert.ToDecimal(cantidades_sumara[i]));//esta largo lo se. pero significa que a la columna a editar le va a sumar la cantidad señalada
                                }
                            }


                            for (int i = 0; i < palabra.Length; i++)
                            {
                                linea_editada = linea_editada + palabra[i] + caracter_separacion;
                            }
                            linea_editada = Trimend_paresido(linea_editada, caracter_separacion);
                            sw.WriteLine(linea_editada);

                        }

                        else
                        {
                            sw.WriteLine(linea);
                        }
                    }
                }
                sr.Close();
                sw.Close();
                exito_o_fallo = "1)exito";
                File.Delete(direccion_archivo);//borramos el archivo original
                File.Move(dir_tem, direccion_archivo);//renombramos el archivo temporal por el que tenia el original

                if (bandera == false)
                {

                    string temp = Seleccionar("inf/inventario/invent.txt", 3, comparar, "1|3|0|6|8|2");
                    string[] texto = temp.Split('°');

                    DateTime fecha_y_hora = DateTime.Now;


                    texto[0] = cantidad_a_sumar + caracter_separacion + texto[0] + caracter_separacion + fecha_y_hora.ToString("HH:mm");
                    if (info_extra != "")
                    {
                        string[] info_extra_spliteada = info_extra.Split(caracter_separacion);

                        if (info_extra_spliteada[0] == "1")
                        {
                            for (int i = 1; i < info_extra_spliteada.Length; i++)
                            {
                                texto[0] = texto[0] + caracter_separacion + info_extra_spliteada[i];
                            }
                        }


                        texto[0] = Trimend_paresido(texto[0], caracter_separacion);

                    }




                    Agregar(direccion_archivo, texto[0]);
                }


            }
            catch (Exception error)
            {
                sr.Close();
                sw.Close();
                exito_o_fallo = "2)error:" + error;
                File.Delete(dir_tem);//borramos el archivo temporal
            }
            return exito_o_fallo;
        }

        public string Si_existe_suma_sino_desde_el_inventario_las_columnas_agrega(string direccion_archivo, int num_column_comp, string comparar, string numero_columnas_editar, string cantidad_a_sumar, string columnas_a_extraer_inventario, string info_extra = "", char caracter_separacion = '|')
        {
            Crear_archivo_y_directorio(direccion_archivo);
            bool bandera = false;
            StreamReader sr = new StreamReader(direccion_archivo);
            string dir_tem = direccion_archivo.Replace(".txt", "_tem.txt");
            StreamWriter sw = new StreamWriter(dir_tem, true);
            string exito_o_fallo;

            try
            {

                while (sr.Peek() >= 0)//verificamos si hay mas lineas a leer
                {
                    string linea = sr.ReadLine();//leemos linea y lo guardamos en linea
                    if (linea != null)
                    {
                        string[] palabra = linea.Split(caracter_separacion);

                        if (palabra[num_column_comp] == comparar)
                        {
                            bandera = true;
                            string linea_editada = "";


                            if (info_extra != "")
                            {

                                string[] info_extra_spliteada = info_extra.Split(caracter_separacion);

                                if (info_extra_spliteada[0] == "1")
                                {
                                    numero_columnas_editar = numero_columnas_editar + "|7";
                                    cantidad_a_sumar = cantidad_a_sumar + "|" + (Convert.ToDecimal(info_extra_spliteada[1]) * Convert.ToDecimal(cantidad_a_sumar));
                                    string[] columnas_editar = numero_columnas_editar.Split(caracter_separacion);
                                    string[] cantidades_sumara = cantidad_a_sumar.Split(caracter_separacion);

                                    for (int i = 0; i < columnas_editar.Length; i++)
                                    {
                                        palabra[Convert.ToInt32(columnas_editar[i])] = "" + (Convert.ToInt32(palabra[Convert.ToInt32(columnas_editar[i])]) + Convert.ToInt32(cantidades_sumara[i]));//esta largo lo se. pero significa que a la columna a editar le va a sumar la cantidad señalada
                                    }
                                }
                                else
                                {
                                    string[] columnas_editar = numero_columnas_editar.Split(caracter_separacion);
                                    string[] cantidades_sumara = cantidad_a_sumar.Split(caracter_separacion);

                                    for (int i = 0; i < columnas_editar.Length; i++)
                                    {
                                        palabra[Convert.ToInt32(columnas_editar[i])] = "" + (Convert.ToInt32(palabra[Convert.ToInt32(columnas_editar[i])]) + Convert.ToInt32(cantidad_a_sumar));//esta largo lo se. pero significa que a la columna a editar le va a sumar la cantidad señalada
                                    }
                                }

                            }

                            else
                            {
                                string[] columnas_editar = numero_columnas_editar.Split(caracter_separacion);
                                string[] cantidades_sumara = cantidad_a_sumar.Split(caracter_separacion);

                                for (int i = 0; i < columnas_editar.Length; i++)
                                {
                                    palabra[Convert.ToInt32(columnas_editar[i])] = "" + (Convert.ToDecimal(palabra[Convert.ToInt32(columnas_editar[i])]) + Convert.ToDecimal(cantidades_sumara[i]));//esta largo lo se. pero significa que a la columna a editar le va a sumar la cantidad señalada
                                }
                            }


                            for (int i = 0; i < palabra.Length; i++)
                            {
                                linea_editada = linea_editada + palabra[i] + caracter_separacion;
                            }
                            linea_editada = Trimend_paresido(linea_editada, caracter_separacion);
                            sw.WriteLine(linea_editada);

                        }

                        else
                        {
                            sw.WriteLine(linea);
                        }
                    }
                }
                sr.Close();
                sw.Close();
                exito_o_fallo = "1)exito";
                File.Delete(direccion_archivo);//borramos el archivo original
                File.Move(dir_tem, direccion_archivo);//renombramos el archivo temporal por el que tenia el original

                if (bandera == false)
                {

                    string temp = Seleccionar("inf/inventario/invent.txt", 3, comparar, columnas_a_extraer_inventario);
                    string[] texto = temp.Split('°');

                    DateTime fecha_y_hora = DateTime.Now;


                    texto[0] = cantidad_a_sumar + caracter_separacion + texto[0] + caracter_separacion + fecha_y_hora.ToString("HH:mm");
                    if (info_extra != "")
                    {
                        string[] info_extra_spliteada = info_extra.Split(caracter_separacion);

                        if (info_extra_spliteada[0] == "1")
                        {
                            for (int i = 1; i < info_extra_spliteada.Length; i++)
                            {
                                texto[0] = texto[0] + caracter_separacion + info_extra_spliteada[i];
                            }
                        }


                        texto[0] = Trimend_paresido(texto[0], caracter_separacion);

                    }




                    Agregar(direccion_archivo, texto[0]);
                }


            }
            catch (Exception error)
            {
                sr.Close();
                sw.Close();
                exito_o_fallo = "2)error:" + error;
                File.Delete(dir_tem);//borramos el archivo temporal
            }
            return exito_o_fallo;
        }

        public string si_no_existe_agega_comparacion(string direccion_archivo, string comparar, char caracter_separacion = '|')
        {
            Crear_archivo_y_directorio(direccion_archivo);
            bool bandera = false;
            StreamReader sr = new StreamReader(direccion_archivo);
            string dir_tem = direccion_archivo.Replace(".txt", "_tem.txt");
            StreamWriter sw = new StreamWriter(dir_tem, true);
            string exito_o_fallo;
            int num_column_comp = 0;

            try
            {

                while (sr.Peek() >= 0)//verificamos si hay mas lineas a leer
                {
                    string linea = sr.ReadLine();//leemos linea y lo guardamos en linea
                    if (linea != null)
                    {

                        string[] linea_espliteada = linea.Split(G_parametros[0]);
                        if (linea_espliteada[0] == comparar)
                        {
                            bandera = true;
                            sw.WriteLine(linea);
                        }
                        else
                        {
                            sw.WriteLine(linea);
                        }
                    }
                    num_column_comp++;
                }
                num_column_comp = 0;

                sr.Close();
                sw.Close();
                exito_o_fallo = "1)exito";
                File.Delete(direccion_archivo);//borramos el archivo original
                File.Move(dir_tem, direccion_archivo);//renombramos el archivo temporal por el que tenia el original

                if (bandera == false)
                {
                    Agregar(direccion_archivo, comparar + G_parametros[0]);
                }


            }
            catch (Exception error)
            {
                sr.Close();
                sw.Close();
                exito_o_fallo = "2)error:" + error;
                File.Delete(dir_tem);//borramos el archivo temporal
            }
            return exito_o_fallo;
        }

        public string si_no_existe_agega_extra(string direccion_archivo, int columna_a_comparar, string comparar, string texto_a_agregar, char caracter_separacion = '|')
        {
            Crear_archivo_y_directorio(direccion_archivo);
            bool bandera = false;
            StreamReader sr = new StreamReader(direccion_archivo);
            string dir_tem = direccion_archivo.Replace(".txt", "_tem.txt");
            StreamWriter sw = new StreamWriter(dir_tem, true);
            string exito_o_fallo;
            int num_column_comp = 0;

            try
            {

                while (sr.Peek() >= 0)//verificamos si hay mas lineas a leer
                {
                    string linea = sr.ReadLine();//leemos linea y lo guardamos en linea
                    if (linea != null)
                    {

                        string[] linea_espliteada = linea.Split(G_parametros[0]);
                        if (linea_espliteada[columna_a_comparar] == comparar)
                        {
                            bandera = true;
                            sw.WriteLine(linea);
                        }
                        else
                        {
                            sw.WriteLine(linea);
                        }
                    }
                    num_column_comp++;
                }
                num_column_comp = 0;

                sr.Close();
                sw.Close();
                exito_o_fallo = bandera + "|1)exito";
                File.Delete(direccion_archivo);//borramos el archivo original
                File.Move(dir_tem, direccion_archivo);//renombramos el archivo temporal por el que tenia el original

                if (bandera == false)
                {
                    Agregar(direccion_archivo, texto_a_agregar);
                }


            }
            catch (Exception error)
            {
                sr.Close();
                sw.Close();
                exito_o_fallo = bandera + "|2)error:" + error;
                File.Delete(dir_tem);//borramos el archivo temporal
            }
            return exito_o_fallo;
        }

        public string si_existe_suma_sino_agega_extra(string direccion_archivo, int columna_a_comparar, string comparar, string numero_columnas_editar, string cantidad_a_sumar, string texto_a_agregar, char caracter_separacion = '|')
        {
            Crear_archivo_y_directorio(direccion_archivo);
            bool bandera = false;
            StreamReader sr = new StreamReader(direccion_archivo);
            string dir_tem = direccion_archivo.Replace(".txt", "_tem.txt");
            StreamWriter sw = new StreamWriter(dir_tem, true);
            string exito_o_fallo;
            int num_column_comp = 0;

            try
            {

                while (sr.Peek() >= 0)//verificamos si hay mas lineas a leer
                {
                    string linea = sr.ReadLine();//leemos linea y lo guardamos en linea
                    if (linea != null)
                    {

                        string[] linea_espliteada = linea.Split(caracter_separacion);
                        if (linea_espliteada[columna_a_comparar] == comparar)
                        {
                            string[] num_col_spliteadas = numero_columnas_editar.Split(caracter_separacion);
                            string[] cantidad_spliteada = cantidad_a_sumar.Split(caracter_separacion);
                            for (int i = 0; i < num_col_spliteadas.Length; i++)
                            {
                                linea_espliteada[Convert.ToInt32(num_col_spliteadas[i])] = "" + (Convert.ToDecimal(linea_espliteada[Convert.ToInt32(num_col_spliteadas[i])]) + Convert.ToDecimal(cantidad_spliteada[i]));
                            }
                            linea = string.Join("|", linea_espliteada);
                            bandera = true;
                        }

                        sw.WriteLine(linea);
                    }
                    num_column_comp++;
                }
                num_column_comp = 0;

                sr.Close();
                sw.Close();
                exito_o_fallo = bandera + "|1)exito";
                File.Delete(direccion_archivo);//borramos el archivo original
                File.Move(dir_tem, direccion_archivo);//renombramos el archivo temporal por el que tenia el original

                if (bandera == false)
                {
                    bandera = false;
                    Agregar(direccion_archivo, texto_a_agregar);
                }


            }
            catch (Exception error)
            {
                sr.Close();
                sw.Close();
                exito_o_fallo = bandera + "|2)error:" + error;
                File.Delete(dir_tem);//borramos el archivo temporal
            }
            return exito_o_fallo;
        }

        public string Trimend_paresido(string texto, char caracter_separacion = '|')
        {
            string texto_editado = "";
            string[] texto_spliteado = texto.Split(caracter_separacion);

            if (texto_spliteado[texto_spliteado.Length - 1] == "")
            {
                for (int i = 0; i < texto_spliteado.Length; i++)
                {
                    if (i < texto_spliteado.Length - 2)
                    {
                        texto_editado = texto_editado + texto_spliteado[i] + caracter_separacion;
                    }
                    else
                    {
                        texto_editado = texto_editado + texto_spliteado[i];
                    }
                }
            }
            else
            {
                for (int i = 0; i < texto_spliteado.Length; i++)
                {
                    if (i < texto_spliteado.Length - 1)
                    {
                        texto_editado = texto_editado + texto_spliteado[i] + caracter_separacion;
                    }

                    else
                    {
                        texto_editado = texto_editado + texto_spliteado[i];
                    }
                }
            }


            return texto_editado;
        }

        public string Incrementa_celda(string direccion_archivo, int num_column_comp, string comparar, string numero_columnas_editar, string cantidad_a_sumar, char caracter_separacion = '|')
        {
            Crear_archivo_y_directorio(direccion_archivo);
            StreamReader sr = new StreamReader(direccion_archivo);
            string dir_tem = direccion_archivo.Replace(".txt", "_tem.txt");
            StreamWriter sw = new StreamWriter(dir_tem, true);
            string exito_o_fallo;

            try
            {

                while (sr.Peek() >= 0)//verificamos si hay mas lineas a leer
                {
                    string linea = sr.ReadLine();//leemos linea y lo guardamos en linea
                    if (linea != null)
                    {
                        string[] palabra = linea.Split(caracter_separacion);

                        if (palabra[num_column_comp] == comparar)
                        {
                            string linea_editada = "";
                            string[] columnas_editar = numero_columnas_editar.Split(caracter_separacion);
                            string[] cantidades_sumara = cantidad_a_sumar.Split(caracter_separacion);

                            for (int i = 0; i < columnas_editar.Length; i++)
                            {
                                palabra[Convert.ToInt32(columnas_editar[i])] = "" + (Convert.ToDouble(palabra[Convert.ToInt32(columnas_editar[i])]) + Convert.ToDouble(cantidades_sumara[i]));//esta largo lo se. pero significa que a la columna a editar le va a sumar la cantidad señalada
                            }
                            for (int i = 0; i < palabra.Length; i++)
                            {
                                linea_editada = linea_editada + palabra[i] + caracter_separacion;
                            }
                            linea_editada = Trimend_paresido(linea_editada, caracter_separacion);
                            sw.WriteLine(linea_editada);

                        }
                        else
                        {
                            sw.WriteLine(linea);
                        }
                    }
                }
                sr.Close();
                sw.Close();
                exito_o_fallo = "1)exito";
                File.Delete(direccion_archivo);//borramos el archivo original
                File.Move(dir_tem, direccion_archivo);//renombramos el archivo temporal por el que tenia el original
            }
            catch (Exception error)
            {
                sr.Close();
                sw.Close();
                exito_o_fallo = "2)error:" + error;
                File.Delete(dir_tem);//borramos el archivo temporal
            }
            return exito_o_fallo;
        }


        //para la variable "info_a_editar" en el caracter de separacion es '|' 
        public string si_existe_edita_sino_agega_extra(string direccion_archivo, int columna_a_comparar, string comparar, string numero_columnas_editar, string info_a_editar, string texto_a_agregar, char caracter_separacion = '|')
        {
            Crear_archivo_y_directorio(direccion_archivo);
            bool bandera = false;
            StreamReader sr = new StreamReader(direccion_archivo);
            string dir_tem = direccion_archivo.Replace(".txt", "_tem.txt");
            StreamWriter sw = new StreamWriter(dir_tem, true);
            string exito_o_fallo;
            int num_column_comp = 0;

            try
            {

                while (sr.Peek() >= 0)//verificamos si hay mas lineas a leer
                {
                    string linea = sr.ReadLine();//leemos linea y lo guardamos en linea
                    if (linea != null)
                    {

                        string[] linea_espliteada = linea.Split(caracter_separacion);
                        if (linea_espliteada[columna_a_comparar] == comparar)
                        {
                            string[] num_col_spliteadas = numero_columnas_editar.Split(caracter_separacion);
                            string[] info_espliteado = info_a_editar.Split(caracter_separacion);
                            for (int i = 0; i < num_col_spliteadas.Length; i++)
                            {
                                linea_espliteada[Convert.ToInt32(num_col_spliteadas[i])] = info_espliteado[i];
                            }
                            linea = string.Join("|", linea_espliteada);
                            bandera = true;
                        }

                        sw.WriteLine(linea);
                    }
                    num_column_comp++;
                }
                num_column_comp = 0;

                sr.Close();
                sw.Close();
                exito_o_fallo = bandera + "|1)exito";
                File.Delete(direccion_archivo);//borramos el archivo original
                File.Move(dir_tem, direccion_archivo);//renombramos el archivo temporal por el que tenia el original

                if (bandera == false)
                {
                    bandera = false;
                    Agregar(direccion_archivo, texto_a_agregar);
                }


            }
            catch (Exception error)
            {
                sr.Close();
                sw.Close();
                exito_o_fallo = bandera + "|2)error:" + error;
                File.Delete(dir_tem);//borramos el archivo temporal
            }
            return exito_o_fallo;
        }

        public void eliminar_archivo(string direccion_archivo)
        {
            File.Delete(direccion_archivo);

        }

    }
}