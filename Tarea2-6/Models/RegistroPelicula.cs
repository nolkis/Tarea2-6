using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Tarea2_6.Models
{
    public class RegistroPelicula
    {
        private SqlConnection con;
        /*Conectar a la base de datos*/
        private void Conecta()
        {
            string Constr = ConfigurationManager.ConnectionStrings["MiConexion"].ToString();
            con = new SqlConnection(Constr);
        }
        /*Grabar registro en la base de datos*/
        public int Grabar(Pelicula pelicula)
        {
            Conecta();
            SqlCommand comando = new SqlCommand("INSERT INTO Pelicula (Titulo, Director, Autorp, No_Actores, Duracion, Estreno) VALUES (@Titulo, @Director, @Actorp, @No_Actores, @Duracion, @Estreno)", con);
            //VALUES(+jugador.Nombres+, jugador.Apellidos", cnn); tambien se puede poner asi;
            comando.Parameters.Add("@Titulo", SqlDbType.VarChar);
            comando.Parameters.Add("@Director", SqlDbType.VarChar);
            comando.Parameters.Add("@Autorp", SqlDbType.VarChar);
            comando.Parameters.Add("@No_Actores", SqlDbType.Int);
            comando.Parameters.Add("@Duracion", SqlDbType.Float);
            comando.Parameters.Add("@Estreno", SqlDbType.Int);
            
            //if (jugador.Nombres==null) validar
            comando.Parameters["@Titulo"].Value = pelicula.Titulo;
            comando.Parameters["@Director"].Value = pelicula.Director;
            comando.Parameters["@Autorp"].Value = pelicula.Actorp;
            comando.Parameters["@No_Actores"].Value = pelicula.No_Actores;
            comando.Parameters["@Duracion"].Value = pelicula.Duracion;
            comando.Parameters["@Estreno"].Value = pelicula.Estreno;
            con.Open();
            int i = comando.ExecuteNonQuery();
            con.Close();
            return i;
        }
        public List<Pelicula> RecuperarTodos()
        {
            Conecta();
            List<Pelicula> peliculas = new List<Pelicula>();

            SqlCommand com = new SqlCommand("Select Codigo, Titulo, Director, Actorp, No_Actores, Duracion, Estreno From Pelicula", con);
            con.Open();
            SqlDataReader registros = com.ExecuteReader();
            while (registros.Read())
            {
                Pelicula pelicula = new Pelicula
                {
                    Titulo = registros["Titulo"].ToString(),
                    Director = registros["Director"].ToString(),
                    Actorp = registros["Actorp"].ToString(),
                    No_Actores = int.Parse(registros["No_Actores"].ToString()),
                    Duracion = double.Parse(registros["Duracion"].ToString()),
                    Estreno = int.Parse(registros["Estreno"].ToString()),
                };
                peliculas.Add(pelicula);

            }
            con.Close();
            return peliculas;
        }
        public Pelicula Recuperar(int codigo)
        {
            Conecta();
            SqlCommand com = new SqlCommand("Select Codigo, Titulo, Director, Autorp, No_Actores, Duracion, Estreno From Pelicula where Codigo=@Codigo", con);
            com.Parameters.Add("@Codigo", SqlDbType.Int);
            com.Parameters["@Codigo"].Value = codigo;
            con.Open();
            SqlDataReader registros = com.ExecuteReader();
            Pelicula pelicula = new Pelicula();
            if (registros.Read())
            {

                pelicula.Titulo = registros["Titulo"].ToString();
                pelicula.Director = registros["Director"].ToString();
                pelicula.Actorp = registros["Actorp"].ToString();
                pelicula.No_Actores = int.Parse(registros["No_Actores"].ToString());
                pelicula.Duracion = double.Parse(registros["Duracion"].ToString());
                pelicula.Estreno = int.Parse(registros["Estreno"].ToString());
            }
            con.Close();
            return pelicula; 

        }
        public int Modificar(Pelicula pelicula)
        {
            Conecta();
            SqlCommand comando = new SqlCommand("Update Pelicula set Titulo=@Titulo, Director=@Director, Autorp=@Actorp, No_Actores=@No_Actores, Duracion=@Duracion, Estreno=@Estreno) where Codigo=@Codigo)", con);
            //VALUES(+jugador.Nombres+, jugador.Apellidos", cnn); tambien se puede poner asi;
            comando.Parameters.Add("@Titulo", SqlDbType.VarChar); comando.Parameters["@Titulo"].Value = pelicula.Titulo;
            comando.Parameters.Add("@Director", SqlDbType.VarChar); comando.Parameters["@Director"].Value = pelicula.Director;
            comando.Parameters.Add("@Autorp", SqlDbType.VarChar); comando.Parameters["@Autorp"].Value = pelicula.Actorp;
            comando.Parameters.Add("@No_Actores", SqlDbType.Int); comando.Parameters["@No_Actores"].Value = pelicula.No_Actores;
            comando.Parameters.Add("@Duracion", SqlDbType.Float); comando.Parameters["@Duracion"].Value = pelicula.Duracion;
            comando.Parameters.Add("@Estreno", SqlDbType.Int); comando.Parameters["@Estreno"].Value = pelicula.Estreno;
            con.Open();
            int i = comando.ExecuteNonQuery();
            con.Close();
            return i;
        }
        public int Borrar(int codigo)
        {
            Conecta();
            SqlCommand comando = new SqlCommand("delete from Pelicula where Codigo=@Codigo)", con);
            //VALUES(+jugador.Nombres+, jugador.Apellidos", cnn); tambien se puede poner asi;
            comando.Parameters.Add("@Codigo", SqlDbType.Int);
            comando.Parameters["@Codigo"].Value = codigo;
            con.Open();
            con.Open();
            int i = comando.ExecuteNonQuery();
            con.Close();
            return i;
        }
    }
    
}