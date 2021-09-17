using DGSCBD;
using DGSCEL;
using DGSCUTILIDADES;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using static DGSCEL.clsModelos;

namespace DGSCBL
{
    public class clsConcursoBL
    {
        public List<clsConcursoEL> GetListaConcurso()
        {

            try
            {
                using (BD_SCI gDatos = new BD_SCI())
                {
                    clsConcursoEL oclsConcursoEL = null;
                    List<clsConcursoEL> listaConcursos = new List<clsConcursoEL>();

                    SqlCommand command = new SqlCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "sci_spGetListaConcurso";
                    
                    SqlDataReader dataReader = gDatos.ExecuteReader(command);

                    while (dataReader.Read())
                    {
                        oclsConcursoEL = new clsConcursoEL
                        {
                            Codigo = dataReader.GetString(0),
                            IdFuncionario = dataReader.GetString(1),
                            Titulo = dataReader.GetString(2),
                            Descripcion = dataReader.GetString(3),
                            TipoProceso = dataReader.GetString(4),
                            CorreoPrincipal = dataReader.GetString(5),
                            CorreoSecundario = dataReader.GetString(6),
                            TelefonoPrincipal = dataReader.GetString(7),
                            TelefonoSecundario = dataReader.GetString(8),
                            Fecha = dataReader.GetDateTime(9),
                            Hora = dataReader.GetDateTime(10),
                            Estado = dataReader.GetString(11),

                        };
                        listaConcursos.Add(oclsConcursoEL);
                    }
                    dataReader.Close();
                    return listaConcursos;

                }
            }
            catch (Exception ex)
            {
                (new clsUtilidades()).WriteErrorLog(ex.Message, ex.StackTrace, $"sci_spGetListaConcurso");
            }
            return new List<clsConcursoEL>();

        }



        public void CreateConcurso(clsConcursoEL pConcurso)
        {
            var clsResultado = new clsResultadoGenerico() { Estado = -1, Mensaje = "Error Generico" };

            try
            {
                using (BD_SCI gDatos = new BD_SCI())
                {
                    SqlCommand command = new SqlCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "sci_spInsertConcurso";
                    command.Parameters.AddWithValue("@Id", pConcurso.Codigo);
                    command.Parameters.AddWithValue("@idFuncionario", pConcurso.IdFuncionario);
                    command.Parameters.AddWithValue("@titulo", pConcurso.Titulo);
                    command.Parameters.AddWithValue("@descripcion", pConcurso.Descripcion);
                    command.Parameters.AddWithValue("@tipoProceso", pConcurso.TipoProceso);
                    command.Parameters.AddWithValue("@correoPrincipal", pConcurso.CorreoPrincipal);
                    command.Parameters.AddWithValue("@correoSecundario", pConcurso.CorreoSecundario);
                    command.Parameters.AddWithValue("@telefonoPrincipal", pConcurso.TelefonoPrincipal);
                    command.Parameters.AddWithValue("@telefonoSecundario", pConcurso.TelefonoSecundario);
                    command.Parameters.AddWithValue("@fecha", pConcurso.Fecha);
                    command.Parameters.AddWithValue("@hora", pConcurso.Hora);
                    command.Parameters.AddWithValue("@estado", pConcurso.Estado);
                    SqlParameter prmOutput = new SqlParameter("@prmResult", SqlDbType.Int);
                    prmOutput.Direction = ParameterDirection.Output;
                    command.Parameters.Add(prmOutput);

                    gDatos.ExecuteNonQuery(ref command);

                    int returnValue = (int)command.Parameters["@prmResult"].Value;

                    if (returnValue == 0)
                    {
                        clsResultado.Estado = 1;
                        clsResultado.Mensaje = "Satisfactorio";
                    }

                }

            }
            catch (Exception ex)
            {
                clsResultado.Estado = -2;
                clsResultado.Mensaje = ex.Message;

                (new clsUtilidades()).WriteErrorLog(ex.Message, ex.StackTrace, (new JavaScriptSerializer()).Serialize(pConcurso));
            }
           // return clsResultado;

        }
        public void UpdateConcurso(clsConcursoEL pConcurso)
        {
            var clsResultado = new clsResultadoGenerico() { Estado = -1, Mensaje = "Error Generico" };

            try
            {
                using (BD_SCI gDatos = new BD_SCI())
                {
                    SqlCommand command = new SqlCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "sci_spUpdateConcurso";
                    command.Parameters.AddWithValue("@Id", pConcurso.Codigo);
                    command.Parameters.AddWithValue("@idFuncionario", pConcurso.IdFuncionario);
                    command.Parameters.AddWithValue("@titulo", pConcurso.Titulo);
                    command.Parameters.AddWithValue("@descripcion", pConcurso.Descripcion);
                    command.Parameters.AddWithValue("@tipoProceso", pConcurso.TipoProceso);
                    command.Parameters.AddWithValue("@correoPrincipal", pConcurso.CorreoPrincipal);
                    command.Parameters.AddWithValue("@correoSecundario", pConcurso.CorreoSecundario);
                    command.Parameters.AddWithValue("@telefonoPrincipal", pConcurso.TelefonoPrincipal);
                    command.Parameters.AddWithValue("@telefonoSecundario", pConcurso.TelefonoSecundario);
                    command.Parameters.AddWithValue("@fecha", pConcurso.Fecha);
                    command.Parameters.AddWithValue("@hora", pConcurso.Hora);
                    command.Parameters.AddWithValue("@estado", pConcurso.Estado);
                    SqlParameter prmOutput = new SqlParameter("@prmResult", SqlDbType.Int);
                    prmOutput.Direction = ParameterDirection.Output;
                    command.Parameters.Add(prmOutput);

                    gDatos.ExecuteNonQuery(ref command);

                    int returnValue = (int)command.Parameters["@prmResult"].Value;

                    if (returnValue == 0)
                    {
                        clsResultado.Estado = 1;
                        clsResultado.Mensaje = "Satisfactorio";
                    }

                }

            }
            catch (Exception ex)
            {
                //clsResultado.Estado = -2;
                //clsResultado.Mensaje = ex.Message;

                (new clsUtilidades()).WriteErrorLog(ex.Message, ex.StackTrace, (new JavaScriptSerializer()).Serialize(pConcurso));
            }
            // return clsResultado;

        }
        public clsConcursoEL GetConcursoPorId(string id)
        {

            try
            {
                using (BD_SCI gDatos = new BD_SCI())
                {
                    clsConcursoEL oclsConcursoEL = null;

                    SqlCommand command = new SqlCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "sci_spGetConcursoPorId";
                    command.Parameters.AddWithValue("@Id", id);



                    SqlDataReader dataReader = gDatos.ExecuteReader(command);

                    while (dataReader.Read())
                    {
                        oclsConcursoEL = new clsConcursoEL
                        {
                            Codigo = dataReader.GetString(0),
                            IdFuncionario = dataReader.GetString(1),
                            Titulo = dataReader.GetString(2),
                            Descripcion = dataReader.GetString(3),
                            TipoProceso = dataReader.GetString(4),
                            CorreoPrincipal = dataReader.GetString(5),
                            CorreoSecundario = dataReader.GetString(6),
                            TelefonoPrincipal = dataReader.GetString(7),
                            TelefonoSecundario = dataReader.GetString(8),
                            Fecha = dataReader.GetDateTime(9),
                            Hora = dataReader.GetDateTime(10),
                            Estado = dataReader.GetString(11)
                        };

                    }
                    dataReader.Close();
                    return oclsConcursoEL;

                }
            }
            catch (Exception ex)
            {
                (new clsUtilidades()).WriteErrorLog(ex.Message, ex.StackTrace, $"pId: {id}");
            }
            return null;

        }
        //public clsConcursoEL GetConcursoPorId(string id)
        //{
        //    clsConcursoEL concurso = null;
        //    try
        //    {
        //        using (BD_SCI gDatos = new BD_SCI())
        //        {
        //            SqlCommand command = new SqlCommand();

        //            command.CommandType = CommandType.StoredProcedure;
        //            command.CommandText = "sci_GetConcirsoPorId";
        //            command.Parameters.AddWithValue("@id", id);

        //            SqlDataReader dataReader = gDatos.ExecuteReader(command);

        //            while (dataReader.Read())
        //            {
        //                concurso = new clsConcursoEL() { 
        //                concurso.Codigo = dataReader.GetString(0);
        //                concurso.IdFuncionario = dataReader.GetString(1);
        //                concurso.Titulo = dataReader.GetString(2);
        //                concurso.Descripcion = dataReader.GetString(3);
        //                concurso.TipoProceso = dataReader.GetString(4);
        //                concurso.CorreoPrincipal = dataReader.GetString(5);
        //                concurso.CorreoSecundario = dataReader.GetString(6);
        //                concurso.TelefonoPrincipal = dataReader.GetString(7);
        //                concurso.TelefonoSecundario = dataReader.GetString(8);
        //                concurso.Fecha = dataReader.GetDateTime(9);
        //                concurso.Hora = dataReader.GetDateTime(10);
        //                concurso.Estado = dataReader.GetString(11);
        //            }

        //            dataReader.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        (new clsUtilidades()).WriteErrorLog(ex.ToString(), ex.StackTrace, $"codUser: {id}");
        //        concurso = null;
        //    }

        //    return concurso;

        //}
        public void Guardar(clsConcursoEL pConcurso)
        {
            if (GetConcursoPorId(pConcurso.Codigo) != null)
            {
                UpdateConcurso(pConcurso);
            }
            else
            {
                CreateConcurso(pConcurso);
            }
        }

        public int DeleteConcursoPorId(string id)
        {
            try
            {
                using (BD_SCI gDatos = new BD_SCI())
                {

                    SqlCommand command = new SqlCommand();

                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "sci_spDeleteConcursoPorId";
                    command.Parameters.AddWithValue("@Id", id);
          
                    SqlParameter prmOutput = new SqlParameter("@prmResult", SqlDbType.Int);
                    prmOutput.Direction = ParameterDirection.Output;
                    command.Parameters.Add(prmOutput);

                    gDatos.ExecuteNonQuery(ref command);

                    int returnValue = (int)command.Parameters["@prmResult"].Value;
                    return returnValue;
                }
            }
            catch (Exception ex)
            {
                (new clsUtilidades()).WriteErrorLog(ex.Message, ex.StackTrace, "");
            }
            return 1;

        }
    }
}
