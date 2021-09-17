using DGSCEL;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DGSCUTILIDADES
{
    public class clsUtilidades
    {

        private static clsUtilidades instancia = null;

        public clsUtilidades()
        {

        }

        public static clsUtilidades obtenerInstancia()
        {
            if (instancia == null)
            {
                instancia = new clsUtilidades();
            }
            return instancia;
        }

        public string Encriptar(string texto)
        {
            try
            {

                string key = "XXXXXX"; //llave para encriptar datos

                byte[] keyArray;

                byte[] Arreglo_a_Cifrar = UTF8Encoding.UTF8.GetBytes(texto);

                //Se utilizan las clases de encriptación MD5

                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();

                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));

                hashmd5.Clear();

                //Algoritmo TripleDES
                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateEncryptor();

                byte[] ArrayResultado = cTransform.TransformFinalBlock(Arreglo_a_Cifrar, 0, Arreglo_a_Cifrar.Length);

                tdes.Clear();

                //se regresa el resultado en forma de una cadena
                texto = Convert.ToBase64String(ArrayResultado, 0, ArrayResultado.Length);

            }
            catch (Exception)
            {

            }
            return texto;
        }

        public string Desencriptar(string textoEncriptado)
        {
            try
            {
                string key = "XXXXXX";
                byte[] keyArray;
                byte[] Array_a_Descifrar = Convert.FromBase64String(textoEncriptado);

                //algoritmo MD5
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();

                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));

                hashmd5.Clear();

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateDecryptor();

                byte[] resultArray = cTransform.TransformFinalBlock(Array_a_Descifrar, 0, Array_a_Descifrar.Length);

                tdes.Clear();
                textoEncriptado = UTF8Encoding.UTF8.GetString(resultArray);

            }
            catch (Exception)
            {

            }
            return textoEncriptado;
        }

        public void WriteErrorLog(string exMessage, string exStackTrace, string notas)
        {
            try
            {
                var sFrame = (new StackFrame(1, true)).GetMethod();
                string _path = LogPath.PathLogs + @"Logs\" + DateTime.Now.Year + @"\Mes_" + DateTime.Now.Month + @"\Dia_" + DateTime.Now.Day;

                if (!Directory.Exists(_path))
                    Directory.CreateDirectory(_path);

                StreamWriter writer = File.AppendText(_path + @"\ErrorLog_" + sFrame.ReflectedType.Name + ".txt");
                writer.WriteLine("********************************** " + DateTime.Now + " ****************************************");
                writer.WriteLine("Clase : " + sFrame.ReflectedType.Name);
                writer.WriteLine("Método: " + sFrame.Name);
                writer.WriteLine("EXMessage: " + exMessage);
                writer.WriteLine("EXStackTrace: " + exStackTrace);
                writer.WriteLine("Notas: " + notas);
                writer.WriteLine("**************************************************************************************************");
                writer.Close();
            }
            catch { }

        }

        /// <summary>
        /// Convertir archivo de excel en un datatable
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        public DataTable ToDataTable(ExcelPackage package)
        {
            try
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets.First();
                DataTable table = new DataTable();
                foreach (var firstRowCell in workSheet.Cells[1, 1, 1, workSheet.Dimension.End.Column])
                {
                    table.Columns.Add(firstRowCell.Text);
                }
                for (var rowNumber = 2; rowNumber <= workSheet.Dimension.End.Row; rowNumber++)
                {
                    var row = workSheet.Cells[rowNumber, 1, rowNumber, workSheet.Dimension.End.Column];
                    var newRow = table.NewRow();

                    // Si la primera linea está vacia se ignoran las siguientes rows y se rompe el ciclo
                    if (row.First().Text.Replace(" ", "").Equals("") || row.First().Text.Length == 0)
                        break;

                    foreach (var cell in row)
                    {
                        // Si viene una celda con espacios en blanco setear el valor de la celda a nulo para evitar conversiones e insertar correctamente el excel
                        if (cell.Text.Replace(" ", "").Equals(""))
                            newRow[cell.Start.Column - 1] = null;
                        else
                            newRow[cell.Start.Column - 1] = cell.Text;
                    }
                    table.Rows.Add(newRow);

                }
                return table;
            }
            catch (Exception ex)
            {
                (new clsUtilidades()).WriteErrorLog(ex.Message, ex.StackTrace, "");
            }

            return null;
        }

    }
}

