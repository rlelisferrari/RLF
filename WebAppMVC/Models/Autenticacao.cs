﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppMVC.Models
{
    public class Autenticacao : IAutenticacao
    {
        public IConfiguration Configuration { get; set; }

        //Le a string de conexão do arquivo de configuração
        public string GetConnectionString()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            return connectionString;
        }

        public string RegistrarUsuario(Usuario usuario)
        {
            return "Sucesso";
            //using (SqlConnection con = new SqlConnection(GetConnectionString()))
            //{
            //    SqlCommand cmd = new SqlCommand("RegistrarUsuario", con);
            //    cmd.CommandType = CommandType.StoredProcedure;

            //    cmd.Parameters.AddWithValue("@Login", usuario.Login);
            //    cmd.Parameters.AddWithValue("@Senha", usuario.Senha);
            //    cmd.Parameters.AddWithValue("@Nome", usuario.Nome);
            //    cmd.Parameters.AddWithValue("@Email", usuario.Email);

            //    con.Open();
            //    string resultado = cmd.ExecuteScalar().ToString();
            //    con.Close();

            //    return resultado;
            //}
        }

        public string ValidarLogin(Usuario usuario)
        {
            return "Sucesso";
            //using (SqlConnection con = new SqlConnection(GetConnectionString()))
            //{
            //    SqlCommand cmd = new SqlCommand("ValidarLogin", con);
            //    cmd.CommandType = CommandType.StoredProcedure;

            //    cmd.Parameters.AddWithValue("@Login", usuario.Login);
            //    cmd.Parameters.AddWithValue("@Senha", usuario.Senha);

            //    con.Open();
            //    string resultado = cmd.ExecuteScalar().ToString();
            //    con.Close();

            //    return resultado;
            //}
        }
    }
}
