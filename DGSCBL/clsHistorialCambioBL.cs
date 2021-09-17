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
    public class clsHistorialCambioBL
    {
        public List<clsHistorialCambioEL> GetListaHistorialCambio()
        {

            try
            {
                using (BD_SCI gDatos = new BD_SCI())
                {
                    clsHistorialCambioEL oclsHistorialCambioEL = null;
                    List<clsHistorialCambioEL> listaConcursos = new List<clsHistorialCambioEL>();

                    SqlCommand command = new SqlCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "sci_spGetListaHistorialCambio";

                    SqlDataReader dataReader = gDatos.ExecuteReader(command);

                    while (dataReader.Read())
                    {
                        oclsHistorialCambioEL = new clsHistorialCambioEL
                        {
                            Id = dataReader.GetString(0),
                            Concurso = new clsConcursoBL().GetConcursoPorId(dataReader.GetString(1)),
                            IdFuncionario = dataReader.GetString(2),
                            Fecha  = dataReader.GetDateTime(3),
                            Hora = dataReader.GetDateTime(4),
                        };
                        listaConcursos.Add(oclsHistorialCambioEL);
                    }
                    dataReader.Close();
                    return listaConcursos;

                }
            }
            catch (Exception ex)
            {
                (new clsUtilidades()).WriteErrorLog(ex.Message, ex.StackTrace, $"sci_spGetListaHistorialCambio");
            }
            return new List<clsHistorialCambioEL>();

        }



        public void CreateHistorialCambio(clsHistorialCambioEL pHistorialCambio)
        {
            var clsResultado = new clsResultadoGenerico() { Estado = -1, Mensaje = "Error Generico" };

            try
            {
                using (BD_SCI gDatos = new BD_SCI())
                {
                    SqlCommand command = new SqlCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "sci_spInsertHistorialCambio";
                    command.Parameters.AddWithValue("@Id", pHistorialCambio.Id);
                    command.Parameters.AddWithValue("@idConcurso", pHistorialCambio.Concurso.Codigo);
                    command.Parameters.AddWithValue("@idFuncionario", pHistorialCambio.IdFuncionario);
                    command.Parameters.AddWithValue("@fecha", pHistorialCambio.Fecha);
                    command.Parameters.AddWithValue("@hora", pHistorialCambio.Hora);

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

                (new clsUtilidades()).WriteErrorLog(ex.Message, ex.StackTrace, (new JavaScriptSerializer()).Serialize(pHistorialCambio));
            }
            // return clsResultado;

        }
        public void UpdateHistorialCambio(clsHistorialCambioEL pHistorialCambio)
        {
            var clsResultado = new clsResultadoGenerico() { Estado = -1, Mensaje = "Error Generico" };

            try
            {
                using (BD_SCI gDatos = new BD_SCI())
                {
                    SqlCommand command = new SqlCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "sci_spUpdateHistorialCambio";
                    command.Parameters.AddWithValue("@Id", pHistorialCambio.Id);
                    command.Parameters.AddWithValue("@idConcurso", pHistorialCambio.Concurso.Codigo);
                    command.Parameters.AddWithValue("@idFuncionario", pHistorialCambio.IdFuncionario);
                    command.Parameters.AddWithValue("@fecha", pHistorialCambio.Fecha);
                    command.Parameters.AddWithValue("@hora", pHistorialCambio.Hora);
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

                (new clsUtilidades()).WriteErrorLog(ex.Message, ex.StackTrace, (new JavaScriptSerializer()).Serialize(pHistorialCambio));
            }
            // return clsResultado;

        }
        public clsHistorialCambioEL GetHistorialCambioPorId(string id)
        {

            try
            {
                using (BD_SCI gDatos = new BD_SCI())
                {
                    clsHistorialCambioEL oclsHistorialCambioEL = null;

                    SqlCommand command = new SqlCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "sci_spGetHistorialPorId";
                    command.Parameters.AddWithValue("@Id", id);



                    SqlDataReader dataReader = gDatos.ExecuteReader(command);

                    while (dataReader.Read())
                    {
                        oclsHistorialCambioEL = new clsHistorialCambioEL
                        {
                            Id = dataReader.GetString(0),
                            Concurso = new clsConcursoBL().GetConcursoPorId(dataReader.GetString(1)),
                            IdFuncionario = dataReader.GetString(2),
                            Fecha = dataReader.GetDateTime(3),
                            Hora = dataReader.GetDateTime(4),
                           
                        };

                    }
                    dataReader.Close();
                    return oclsHistorialCambioEL;

                }
            }
            catch (Exception ex)
            {
                (new clsUtilidades()).WriteErrorLog(ex.Message, ex.StackTrace, $"pId: {id}");
            }
            return null;

        }
        
        public void Guardar(clsHistorialCambioEL pHistorialCambio)
        {
            if (GetHistorialCambioPorId(pHistorialCambio.Id) != null)
            {
                UpdateHistorialCambio(pHistorialCambio);
            }
            else
            {
                CreateHistorialCambio(pHistorialCambio);
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
                    command.CommandText = "sci_spDeleteHistorialConcursoPorId";
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
