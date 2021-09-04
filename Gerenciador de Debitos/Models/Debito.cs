﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Gerenciador_de_Debitos.Model
{
    public class Debito
    {
        private int idDebito;
        private string descricao;
        private DateTime data;
        private double valor;
        private Usuario usuario;
        private Connection conn;

        public Debito(int idDebito, string descricao, DateTime data, double valor, Usuario usuario,Connection conn)
        {
            this.IdDebito = idDebito;
            this.Descricao = descricao;
            this.Data = data;
            this.Valor = valor;
            this.Usuario = usuario;
            this.conn = conn;
        }

        public Debito(Connection conn)
        {
            this.conn = conn;
        }
        public int IdDebito { get => idDebito; set => idDebito = value; }
        public string Descricao { get => descricao; set => descricao = value; }
        public DateTime Data { get => data; set => data = value; }
        public double Valor { get => valor; set => valor = value; }
        public Usuario Usuario { get => usuario; set => usuario = value; }

        public List<Debito> obterDebitosPorNome(Usuario usu, string descricao)
        {
            List<Debito> debitos = new List<Debito>();
            try
            {
                DataTable dt = conn.ExecutarSelect($"SELECT * FROM debitos.debito where idUsuario = {usu.IdUsuario} and descricao = '{descricao}'");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Debito debito = new Debito(this.conn);
                        debito.idDebito = Convert.ToInt32(row["idDebito"]);
                        debito.Descricao = row["descricao"].ToString();
                        debito.Data = Convert.ToDateTime(row["data"]);
                        debito.Valor = Convert.ToDouble(row["valor"]);
                        debito.Usuario = usuario;
                        debitos.Add(debito);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return debitos;
        }

        public List<Debito> obterDebitosBanco(Usuario usuario)
        {
            List<Debito> debitos = new List<Debito>();
            try
            {
                conn.LimparParametros();
                conn.AdicionarParametro("@idUsuario", usuario.IdUsuario.ToString());
                DataTable dt = conn.ExecutarSelect("SELECT * FROM debitos.debito where idUsuario = @idUsuario");
                if(dt.Rows.Count>0)
                {           
                    foreach (DataRow row in dt.Rows)
                    {
                        Debito debito = new Debito(this.conn);
                        debito.idDebito = Convert.ToInt32(row["idDebito"]);
                        debito.Descricao = row["descricao"].ToString();
                        debito.Data = Convert.ToDateTime(row["data"]);
                        debito.Valor = Convert.ToDouble(row["valor"]);
                        debito.Usuario = usuario;
                        debitos.Add(debito);
                    }
                }
            }catch(Exception e)
            {
                Console.WriteLine(e);
            }
            return debitos;
        }

        public bool Cadastrar() // Feito por Pedro
        {
            int linhasAfetadas = 0;
            try
            {
                string sql = "insert into debitos.debito (descricao, data, valor, idUsuario)" +
                                $"values ('{descricao}', '{data.ToString("yyyy-MM-dd")}', {valor}, {usuario.IdUsuario})";
                linhasAfetadas = conn.ExecutarNonQuery(sql);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return linhasAfetadas > 0;
        }

        public List<Debito> filtroDebitos(Usuario usu)
        {
            List<Debito> debitos = new List<Debito>();
            try
            {
                DataTable dt = conn.ExecutarSelect($"SELECT * FROM debitos.debito where idUsuario = {usuario.IdUsuario}" +
                    $" and (descricao = '{descricao}' or data = '{data.ToString("yyyy,MM,dd")}' or valor = {valor})");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Debito debito = new Debito(this.conn);
                        debito.idDebito = Convert.ToInt32(row["idDebito"]);
                        debito.Descricao = row["descricao"].ToString();
                        debito.Data = Convert.ToDateTime(row["data"]);
                        debito.Valor = Convert.ToDouble(row["valor"]);
                        debito.Usuario = usuario;
                        debitos.Add(debito);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return debitos;
        }
    }
}
