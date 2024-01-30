using PracticaCore.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace PracticaCore.Repositories
{
    #region stored procedure
//    create procedure SP_CLIENTES
//AS

//    SELECT* FROM clientes;
//GO

//create procedure SP_PEDIDOS_CLIENTE
//(@IDCLIENTE NVARCHAR(MAX))
//AS

//    SELECT* FROM PEDIDOS WHERE CODIGOCLIENTE=@idcliente
//GO
    #endregion
    public class PedidosClienteRepository
    {
        private SqlConnection cn;
        private SqlCommand com;
        private SqlDataReader reader;

        public PedidosClienteRepository()
        {
            string connectionString = @"Data Source=LOCALHOST\SQLEXPRESS;Initial Catalog=NETCORE;Persist Security Info=True;User ID=sa;Password=MCSD2023";
            this.cn = new SqlConnection(connectionString);
            this.com=new SqlCommand();
            this.com.Connection = this.cn;
        }


        public List<Cliente> GetClientes()
        {
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = "SP_CLIENTES";
            this.cn.Open();
            this.reader = this.com.ExecuteReader();
            List<Cliente> clientes= new List<Cliente>();
            while (this.reader.Read())
            {
                Cliente cli = new Cliente();
                cli.CodigoCliente = this.reader["codigoCliente"].ToString();
                cli.Empresa = this.reader["empresa"].ToString();
               
               
                clientes.Add(cli);
            }
            this.reader.Close();
            this.cn.Close();
            return clientes;
        }

        public Cliente FindClienteById(string codigoCliente)
        {
            string sql = "SELECT * FROM CLIENTES WHERE CODIGOCLIENTE=@codCliente";
            SqlParameter pamCod=new SqlParameter("@codCliente",codigoCliente);
            this.com.Parameters.Add(pamCod);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            this.reader = this.com.ExecuteReader();
            Cliente cli = new Cliente();
            this.reader.Read();
            cli.CodigoCliente = this.reader["codigoCliente"].ToString();
            cli.Empresa = this.reader["empresa"].ToString();
            cli.Telefono = int.Parse(this.reader["telefono"].ToString());
            cli.Ciudad = this.reader["ciudad"].ToString();
            cli.Cargo = this.reader["cargo"].ToString();
            cli.Contacto= this.reader["contacto"].ToString();
            this.reader.Close();
            this.cn.Close();
            this.com.Parameters.Clear();
            return cli;
        }

        public List<Pedido> getPedidosCliente(string codigoCliente)
        {
            string sql = "SELECT * FROM pedidos WHERE CODIGOCLIENTE=@codCliente";
            SqlParameter pamCod = new SqlParameter("@codCliente", codigoCliente);
            this.com.Parameters.Add(pamCod);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            List<Pedido> pedidos = new List<Pedido>();
            this.reader = this.com.ExecuteReader();
            while (this.reader.Read())
            {
                Pedido pedido = new Pedido();
                pedido.CodigoCliente = this.reader["codigoCliente"].ToString();
                pedido.CodigoPedido = this.reader["CodigoPedido"].ToString();
                pedido.Importe = int.Parse(this.reader["importe"].ToString());
                pedido.FormaEnvio = this.reader["FormaEnvio"].ToString();
                pedido.FechaEntrega = this.reader["FechaEntrega"].ToString();
                pedidos.Add(pedido);
            }
            this.reader.Close();
            this.cn.Close();
            this.com.Parameters.Clear();
            return pedidos;
        }

        public Pedido FindPedidoById(string codigoPedido)
        {
            string sql = "SELECT * FROM Pedidos WHERE CODIGOPedido=@codPedido";
            SqlParameter pamCod = new SqlParameter("@codPedido", codigoPedido);
            this.com.Parameters.Add(pamCod);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            this.reader = this.com.ExecuteReader();
            Pedido pedido = new Pedido();
            this.reader.Read();
            pedido.CodigoCliente = this.reader["codigoCliente"].ToString();
            pedido.CodigoPedido = this.reader["CodigoPedido"].ToString();
            pedido.Importe = int.Parse(this.reader["importe"].ToString());
            pedido.FormaEnvio = this.reader["FormaEnvio"].ToString();
            pedido.FechaEntrega = this.reader["FechaEntrega"].ToString();
            this.reader.Close();
            this.cn.Close();
            this.com.Parameters.Clear();
            return pedido;
        }

        public int InsertPedido(string codigoPedido,
            string codigoCliente,
            string fechaEntrega,
            string formaEnvio,
            int importe)
        {
            string sql = "insert into pedidos values(@codpedido,@codcliente,@fechaEntrega,@formaEnvio,@importe)";
            SqlParameter pamCodPedido = new SqlParameter("@codpedido", codigoPedido);
            SqlParameter pamCodCliente = new SqlParameter("@codcliente", codigoCliente);
            SqlParameter pamfecha = new SqlParameter("@fechaEntrega", fechaEntrega);
            SqlParameter pamformaenvio = new SqlParameter("@formaEnvio", formaEnvio);
            SqlParameter pamImporte = new SqlParameter("@importe", importe);
            this.com.Parameters.Add(pamCodPedido);
            this.com.Parameters.Add(pamCodCliente);
            this.com.Parameters.Add(pamfecha);
            this.com.Parameters.Add(pamformaenvio);
            this.com.Parameters.Add(pamImporte);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            int insertados=this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
            return insertados;
        }
        public int EliminarPedido(string codigoPedido)
        {
            string sql = "delete from pedidos where codigopedido=@codpedido";
            SqlParameter pamCodPedido = new SqlParameter("@codpedido", codigoPedido);
            this.com.Parameters.Add(pamCodPedido);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            int eliminados = this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
            return eliminados;
        }
    }
}
