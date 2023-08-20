using CL3.Entidades;
using Microsoft.Data.SqlClient;

namespace CL3.DAO
{
    public class TrabajadorDAO
    {
        private string cadena;

        public TrabajadorDAO()
        {
            cadena = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build()
                .GetConnectionString("cn");
        }

        public IEnumerable<Trabajador> GetTrabajador()
        {
            List<Trabajador> trabajadorList = new List<Trabajador>();

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand sqlCommand = new SqlCommand("exec sp_GetTrabajador", cn);
                cn.Open();
                SqlDataReader dr = sqlCommand.ExecuteReader();
                while (dr.Read())
                {
                    trabajadorList.Add(new Trabajador()
                    {
                        dni = dr.GetString(0),
                        nombre = dr.GetString(1),
                        apellido = dr.GetString(2),
                        empresa = dr.GetString(3),
                        ciudad = dr.GetString(4),
                        telefono = dr.GetString(5),
                        denuncia = dr.GetString(6),
                    });
                }
            }

            return trabajadorList;
        }


        public string Agregar(Trabajador trabajador)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_InsertTrabajador",
                        cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@prmDni", trabajador.dni);
                    cmd.Parameters.AddWithValue("@prmNombre", trabajador.nombre);
                    cmd.Parameters.AddWithValue("@prmApellido", trabajador.apellido);
                    cmd.Parameters.AddWithValue("@prmEmpresa", trabajador.empresa);
                    cmd.Parameters.AddWithValue("@prmaCiudad", trabajador.ciudad);
                    cmd.Parameters.AddWithValue("@prmTelefono", trabajador.telefono);
                    cmd.Parameters.AddWithValue("@prmDenuncia", trabajador.denuncia);
                    cn.Open();
                    int c = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha agregado {c} trabajador";
                }
                catch (Exception ex) { mensaje = ex.Message; }
            }
            return mensaje;
        }

        public string Actualizar(Trabajador trabajador)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarTrabajador", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@prmDni", trabajador.dni);
                    cmd.Parameters.AddWithValue("@prmNombre", trabajador.nombre);
                    cmd.Parameters.AddWithValue("@prmApellido", trabajador.apellido);
                    cmd.Parameters.AddWithValue("@prmEmpresa", trabajador.empresa);
                    cmd.Parameters.AddWithValue("@prmaCiudad", trabajador.ciudad);
                    cmd.Parameters.AddWithValue("@prmTelefono", trabajador.telefono);
                    cmd.Parameters.AddWithValue("@prmDenuncia", trabajador.denuncia);
                    cn.Open();
                    int c = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha actualizado {c} trabajador";
                }
                catch (Exception ex) { mensaje = ex.Message; }
            }
            return mensaje;
        }

        public string Eliminar(string dni)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_EliminarTrabajador", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@prmDni", dni);
                    cn.Open();
                    int c = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha eliminado {c} trabajador";
                }
                catch (Exception ex) { mensaje = ex.Message; }
            }
            return mensaje;
        }

    }
}
