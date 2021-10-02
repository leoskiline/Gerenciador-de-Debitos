﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Gerenciador_de_Debitos.Models;

namespace Gerenciador_de_Debitos.Model
{
    interface Istate
    {
        public StatusPagamento setState(StateContext ctx);
    }

    class StateContext
    {
        private Istate currentState;

        public StateContext()
        {
            currentState = new Pendente();
        }

        public void setState(Istate state)
        {
            currentState = state;
        }

        public Istate getState()
        {
            return currentState;
        }

    }

    class Pago : Istate
    {
        public StatusPagamento setState(StateContext ctx)
        {
            return new StatusPagamento(2, "PAGO");
        }
    }

    class Pendente : Istate
    {
        public StatusPagamento setState(StateContext ctx)
        {
            return new StatusPagamento(1, "PENDENTE");
        }
    }

    class EmAtraso : Istate
    {
        public StatusPagamento setState(StateContext ctx)
        {
            return new StatusPagamento(3, "EM ATRASO");
        }
    }


    public class Debito
    {
        private int idDebito;
        private string descricao;
        private DateTime data;
        private double valor;
        private Usuario usuario;
        private StatusPagamento statusPagamento;
        private Connection conn;

        public Debito(int idDebito, string descricao, DateTime data, double valor, Usuario usuario,StatusPagamento statusPagamento,Connection conn)
        {
            this.IdDebito = idDebito;
            this.Descricao = descricao;
            this.Data = data;
            this.Valor = valor;
            this.Usuario = usuario;
            this.statusPagamento = statusPagamento;
            this.conn = conn;
        }

        public Debito(Connection conn)
        {
            this.conn = conn;
        }

        public Debito(int idDebito,Connection conn)
        {
            this.IdDebito = idDebito;
            this.conn = conn;
        }

        public int IdDebito { get => idDebito; set => idDebito = value; }
        public string Descricao { get => descricao; set => descricao = value; }
        public DateTime Data { get => data; set => data = value; }
        public double Valor { get => valor; set => valor = value; }
        public Usuario Usuario { get => usuario; set => usuario = value; }
        public StatusPagamento StatusPagamento { get => statusPagamento; set => statusPagamento = value; }

        public bool DeletarPorID()
        {
            bool sucesso = false;
            try
            {
                this.conn.LimparParametros();
                this.conn.AdicionarParametro("@idDebito", this.IdDebito.ToString());
                int rows = this.conn.ExecutarNonQuery("DELETE FROM debitos.debito WHERE idDebito = @idDebito");
                if(rows > 0)
                {
                    sucesso = true;
                }
            }catch(Exception e)
            {
                Console.WriteLine(e);
            }
            return sucesso;
        }

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
                        debito.statusPagamento = new StatusPagamento().obterStatusPagamentoPorID(Convert.ToInt32(row["idStatusPagamento"]));
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
                this.conn.LimparParametros();
                StateContext ctx = new StateContext(); // Inicia no estado padrao Pendente.
                ctx.setState(new Pendente()); // Para deixar claro que mudou de estado para Pendente.
                this.statusPagamento = ctx.getState().setState(ctx);
                this.conn.AdicionarParametro("@descricao", this.Descricao);
                this.conn.AdicionarParametro("@data", this.Data.ToString("yyyy-MM-dd"));
                this.conn.AdicionarParametro("@valor", this.Valor.ToString().Replace(",","."));
                this.conn.AdicionarParametro("@idUsuario", this.Usuario.IdUsuario.ToString());
                this.conn.AdicionarParametro("@idStatusPagamento", this.statusPagamento.IdStatusPagamento.ToString());
                string sql = "INSERT INTO debitos.debito (descricao, data, valor,idStatusPagamento, idUsuario) " +
                                "VALUES (@descricao,@data,@valor,@idStatusPagamento,@idUsuario)";
                linhasAfetadas = conn.ExecutarNonQuery(sql);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return linhasAfetadas > 0;
        }
        public bool AlterarConta() // Feito por Pedro
        {
            int linhasAfetadas = 0;
            try
            {
                string valor = this.valor.ToString().Replace(",",".");
                string data = this.data.ToString("yyyy-MM-dd");
                conn.LimparParametros();
                //conn.AdicionarParametro("@idUsuario", this.usuario.IdUsuario.ToString());
                conn.AdicionarParametro("@descricao", this.descricao);
                conn.AdicionarParametro("@valor", valor);
                conn.AdicionarParametro("@idDebito", this.idDebito.ToString());
                conn.AdicionarParametro("@data", data);

                string sql = "UPDATE debitos.debito SET descricao = @descricao, valor = @valor, data = @data " +
                    "WHERE idDebito = @idDebito";
                
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
