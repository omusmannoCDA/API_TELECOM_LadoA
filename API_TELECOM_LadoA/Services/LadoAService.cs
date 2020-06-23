using Domain.Constants;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Data;

namespace API_TELECOM_LadoA.Services
{
    public class LadoAService
    {

        public static string getDB()
        {
            using (OracleConnection con = new OracleConnection(Constantes.connectionString()))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;


                        //Use the command to display employee names from 
                        // the EMPLOYEES table
                        cmd.CommandText = "select * from CATEGORIAS";
                        string texto = "";
                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            texto += "---------------Comienzo-------------------------- \n";
                            texto += "Contenido de columna 1 | " + reader.GetString(0) + "\n";
                            texto += "Contenido de columna 2 | " + reader.GetString(1) + "\n";
                            texto += "---------------FIN-------------------------- \n \n ";
                        }

                        return texto;
                    }
                    catch (Exception ex)
                    {
                        return ex.Message;
                    }
                }
            }
        }

        public static string GrabarLog_SaldoDiagnostico(string id_interfaz, string saldo_request, string saldo_response, string legajo_usuario, string ip_usuario)
        {
            using (OracleConnection con = new OracleConnection(Constantes.connectionString()))
            {
                con.Open();
                try
                {
                    // Create a Command object to call Get_Employee_Info procedure.
                    OracleCommand cmd = con.CreateCommand();
                    cmd.CommandText = "pkg_logs_diagnostico.prc_graba_saldos_diagnostico";
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameter ParameterDirection
                    cmd.Parameters.Add("@PI_ID_SOLICITUD_INTERFAZ", OracleDbType.Varchar2).Value = id_interfaz;
                    cmd.Parameters.Add("@PI_SALDO_REQUEST", OracleDbType.Clob).Value = saldo_request;
                    cmd.Parameters.Add("@PI_SALDO_RESPONSE", OracleDbType.Clob).Value = saldo_response;
                    cmd.Parameters.Add("@PI_LEGAJO_USUARIO", OracleDbType.Varchar2).Value = legajo_usuario;
                    cmd.Parameters.Add("@PI_IP_USUARIO", OracleDbType.Varchar2).Value = ip_usuario;
                    cmd.Parameters.Add("@PO_COD_ERROR", OracleDbType.Varchar2, 200);
                    cmd.Parameters.Add("@PO_DESC_TECNICO", OracleDbType.Varchar2, 2000);
                    cmd.Parameters.Add("@PO_DESC_USU", OracleDbType.Varchar2, 200);

                    cmd.Parameters["@PO_COD_ERROR"].Direction = ParameterDirection.Output;
                    cmd.Parameters["@PO_DESC_TECNICO"].Direction = ParameterDirection.Output;
                    cmd.Parameters["@PO_DESC_USU"].Direction = ParameterDirection.Output;

                    // Execute procedure.
                    cmd.ExecuteNonQuery();

                    //Get results
                    string resul_PO_COD_ERROR = cmd.Parameters["@PO_COD_ERROR"].Value.ToString();
                    string resul_PO_DESC_TECNICO = cmd.Parameters["@PO_DESC_TECNICO"].Value.ToString();
                    object resul_PO_DESC_USU = cmd.Parameters["@PO_DESC_USU"].Value.ToString();

                    string texto = "";
                    texto = resul_PO_COD_ERROR + " | " + resul_PO_DESC_TECNICO + " | " + resul_PO_DESC_USU + "\n";

                    // Get output values.
                    string resul_Interfaz = cmd.Parameters["@PI_ID_SOLICITUD_INTERFAZ"].Value.ToString();
                    string resul_SaldoRequest = cmd.Parameters["@PI_SALDO_REQUEST"].Value.ToString();
                    string resul_SaldoResponse = cmd.Parameters["@PI_SALDO_RESPONSE"].Value.ToString();
                    string resul_Legajo = cmd.Parameters["@PI_LEGAJO_USUARIO"].Value.ToString();
                    object resul_IP = cmd.Parameters["@PI_IP_USUARIO"].Value;
                    //return
                    texto += (resul_Interfaz + " | " + resul_SaldoRequest + " | " + resul_SaldoResponse + " | " + resul_Legajo + " | " + resul_IP);
                    return texto;

                }
                catch (Exception e)
                {
                    string result = ("Error: " + e + "\n" + "\n" + "\n");
                    return result += e.StackTrace;
                }
            }
        }

        public static string PostOracleProcedure1(string param)
        {
            using (OracleConnection con = new OracleConnection(Constantes.connectionString()))
            {
                con.Open();
                try
                {
                    // Create a Command object to call Get_Employee_Info procedure.
                    OracleCommand cmd = con.CreateCommand();
                    cmd.CommandText = "prueba.prc_graba_string";
                    cmd.CommandType = CommandType.StoredProcedure;


                    // Add parameter
                    OracleParameter param1 = new OracleParameter();
                    param1.OracleDbType = OracleDbType.Varchar2;
                    param1.ParameterName = "pi_string";
                    param1.Value = param;
                    cmd.Parameters.Add(param1);

                    // Execute procedure.
                    cmd.ExecuteNonQuery();

                }
                catch (Exception e)
                {
                    return e.ToString();
                }
                return param;
            }

        }

        public static string PostOracleProcedure2(string param)
        {
            using (OracleConnection con = new OracleConnection(Constantes.connectionString()))
            {
                con.Open();
                try
                {
                    // Create a Command object to call Get_Employee_Info procedure.
                    OracleCommand cmd = con.CreateCommand();
                    cmd.CommandText = "prueba.prc_graba_clob";
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameter             

                    cmd.Parameters.Add("pi_string", OracleDbType.Varchar2).Value = "-" + param;
                    cmd.Parameters.Add("pi_clob", OracleDbType.Clob).Value = "-" + param;

                    // Execute procedure.
                    cmd.ExecuteNonQuery();

                }
                catch (Exception e)
                {
                    return e.ToString();
                }
                return param;
            }

        }


        /*PKG_CONSULTAS_DIAGNOSTICO
        PROCEDURE PRC_RECUPERA_SALDO_DIAGNOSTICO (
        PI_ID_SOLICITUD_INTERFAZ   IN     VARCHAR2,
        PO_CURSOR                     OUT REF_CURSOR,
        PO_COD_ERROR                  OUT VARCHAR2,
        PO_DESC_TECNICO               OUT VARCHAR2,
        PO_DESC_USU                   OUT VARCHAR2)*/
        //EN PROCESO
        public static string CONSULTAS_DIAGNOSTICO(string id_interfaz)
        {
            using (OracleConnection con = new OracleConnection(Constantes.connectionString()))
            {
                con.Open();
                try
                {
                    // Create a Command object to call Get_Employee_Info procedure.
                    OracleCommand cmd = con.CreateCommand();
                    cmd.CommandText = "PKG_CONSULTAS_DIAGNOSTICO.PRC_RECUPERA_SALDO_DIAGNOSTICO";
                    cmd.CommandType = CommandType.StoredProcedure;

                    //TEST
                    DataSet dataS = new DataSet();
                    DataTable dt = new DataTable();
                    dataS.Tables.Add(dt);

                    // Add parameter ParameterDirection
                    cmd.Parameters.Add("@PI_ID_SOLICITUD_INTERFAZ", OracleDbType.Varchar2).Value = id_interfaz;
                    cmd.Parameters.Add("@PO_CURSOR", OracleDbType.RefCursor);
                    cmd.Parameters.Add("@PO_COD_ERROR", OracleDbType.Varchar2 );
                    cmd.Parameters.Add("@PO_DESC_TECNICO", OracleDbType.Varchar2 );
                    cmd.Parameters.Add("@PO_DESC_USU", OracleDbType.Varchar2);

                    cmd.Parameters["@PO_CURSOR"].Direction = ParameterDirection.Output;
                    cmd.Parameters["@PO_COD_ERROR"].Direction = ParameterDirection.Output;
                    cmd.Parameters["@PO_DESC_TECNICO"].Direction = ParameterDirection.Output;
                    cmd.Parameters["@PO_DESC_USU"].Direction = ParameterDirection.Output;

                    // Execute procedure.
                    cmd.ExecuteNonQuery();

                    //Get results
                    var resul_PO_CURSOR = ((OracleRefCursor)cmd.Parameters["@PO_CURSOR"].Value).GetDataReader();
                    string resul_PO_COD_ERROR = cmd.Parameters["@PO_COD_ERROR"].Value.ToString();
                    string resul_PO_DESC_TECNICO = cmd.Parameters["@PO_DESC_TECNICO"].Value.ToString();
                    object resul_PO_DESC_USU = cmd.Parameters["@PO_DESC_USU"].Value.ToString();

                    string texto = " ";
                    texto = resul_PO_COD_ERROR + " | " + resul_PO_DESC_TECNICO + " | " + resul_PO_DESC_USU + "\n";


                    for (int i = 0; i < resul_PO_CURSOR.FieldCount; i++)
                    {
                        DataColumn column = new DataColumn(resul_PO_CURSOR.GetName(i));
                        dt.Columns.Add(column);
                    }

                    while (resul_PO_CURSOR.Read())
                    {
                        DataRow dtRow = dt.NewRow();

                        for (int i = 0; i < resul_PO_CURSOR.FieldCount; i++)
                            dtRow[i] = resul_PO_CURSOR.GetValue(i);
                        dt.Rows.Add(dtRow);
                    }
                    var DaTATA= dataS;

                    // Get output values.
                    string resul_Interfaz = cmd.Parameters["@PI_ID_SOLICITUD_INTERFAZ"].Value.ToString();
                    //return
                    texto += (resul_Interfaz) + "\n";
                    texto += "------------------------------------------------\n";
                    texto += resul_PO_CURSOR.GetName(0) + "\n";
                    texto += resul_PO_CURSOR.GetName(1) + "\n";
                    texto += "------------------------------------------------\n";
                    resul_PO_CURSOR.Close();
                    resul_PO_CURSOR.Dispose();
                    return texto;

                }
                catch (Exception e)
                {
                    string result = ("Error: " + e + "\n" + "\n" + "\n");
                    return result += e.StackTrace;
                }
            }
        }
    }

}
