using PracticaCore.Models;
using PracticaCore.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PracticaCore
{
    public partial class FormPractica : Form
    {
        private PedidosClienteRepository prepo;
        private List<string> idClientes;
        private List<string> idPedidos;

        public FormPractica()
        {
            InitializeComponent();
            this.idClientes = new List<string>();
            this.idPedidos = new List<string>();
            this.prepo = new PedidosClienteRepository();
            this.LoadClientes();
        }

        private void LoadClientes()
        {
            foreach (Cliente cli in this.prepo.GetClientes())
            {
                this.cmbclientes.Items.Add(cli.Empresa);
                this.idClientes.Add(cli.CodigoCliente);
            }
        }

        private void LoadPedidos(string codigoCliente)
        {
            this.lstpedidos.Items.Clear();
            List<Pedido> pedidos = this.prepo.getPedidosCliente(codigoCliente);
            foreach (Pedido pedido in pedidos)
            {
                this.idPedidos.Add(pedido.CodigoPedido);
                this.lstpedidos.Items.Add(pedido.CodigoPedido);
            }
        }

        private void cmbclientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indexSelected = this.cmbclientes.SelectedIndex;
            if (indexSelected != -1)
            {
                string id = this.idClientes[indexSelected];
                Cliente cli = this.prepo.FindClienteById(id);
                this.txtcargo.Text = cli.Cargo;
                this.txtciudad.Text = cli.Ciudad;
                this.txtcontacto.Text = cli.Contacto;
                this.txtempresa.Text = cli.Empresa;
                this.txttelefono.Text = cli.Telefono.ToString();
                this.LoadPedidos(id);
            }
        }

        private void lstpedidos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indexSelected = this.lstpedidos.SelectedIndex;
            if (indexSelected != -1)
            {
                string id = this.idPedidos[indexSelected];
                Pedido pedido = this.prepo.FindPedidoById(id);
                this.txtcodigopedido.Text = pedido.CodigoPedido;
                this.txtfechaentrega.Text = pedido.FechaEntrega;
                this.txtformaenvio.Text = pedido.FormaEnvio;
                this.txtimporte.Text = pedido.Importe.ToString();
            }
        }

        private void btnnuevopedido_Click(object sender, EventArgs e)
        {
            string codigoPedido = this.txtcodigopedido.Text;
            string codigoCliente = "";
            string fechaEntrega = this.txtfechaentrega.Text;
            string formaEnvio = this.txtformaenvio.Text;
            int importe = int.Parse(this.txtimporte.Text);
            int insertados=this.prepo.insertPedido(codigoPedido,
                codigoCliente,
                fechaEntrega,
                formaEnvio,
                importe);
            MessageBox.Show("Insertados: " + insertados);

        }
    }
}
