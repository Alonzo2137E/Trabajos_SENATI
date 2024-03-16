using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CapaLogica;

namespace CapaPresentacion
{
    public partial class FrmModuloEspecialista : Form
    {
        public FrmModuloEspecialista()
        {
            InitializeComponent();
        }

        private void MensajeOk(string mensaje)
        {
            MessageBox.Show(mensaje, "Sistema Clinico",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void MensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, "Sistema Clinico",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        private void Limpiar()
        {
            this.txtEspecialistaCMP.Text = string.Empty;
            this.txtEspecialistaNombre.Text = string.Empty;
            this.txtEspecialistaApellido.Text = string.Empty;
            this.cbxEspecialidadCodigo.SelectedIndex = -1;
        }

        private void CargarDatosEspecialista()
        {
            try
            {
                DataTable dtEspecialista = NEspecialista.ListarEspecialista();
                if (dtEspecialista != null && dtEspecialista.Rows.Count > 0)
                {
                    this.dvgEspecialista.DataSource = dtEspecialista;
                }
                else
                {
                    MessageBox.Show("No se encontraron datos de especialista.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener los datos de especialista: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BuscarDatosEspecialista()
        {
            if (int.TryParse(txtEspecialistaCMP.Text, out int especialistaCMP))
            {
                // Si la conversión es exitosa, llama al método con el valor convertido
                this.dvgEspecialista.DataSource = NEspecialista.ListarEspecialistaCMP(especialistaCMP);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void FrmModuloEspecialista_Load(object sender, EventArgs e)
        {
            CargarDatosEspecialista();
        }

        private void InsertaEspecialista()
        {
            int especialistaCMP;
            string especialistaNombre = txtEspecialistaNombre.Text.Trim();
            string especialistaApellido = txtEspecialistaApellido.Text.Trim();
            int especialidadcodigo;


            // Validación del teléfono
            if (!int.TryParse(txtEspecialistaCMP.Text.Trim(), out especialistaCMP))
            {
                MessageBox.Show("El CMP debe ser un número entero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validación básica (puedes agregar más validaciones)
            if (string.IsNullOrEmpty(especialistaNombre) ||
                string.IsNullOrEmpty(especialistaApellido))
            {
                MessageBox.Show("Los campos nombre y apellido son obligatorios.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Verifica si EECC y Genero son enteros antes de convertirlos
            if (int.TryParse(cbxEspecialidadCodigo.SelectedItem.ToString(), out especialidadcodigo))
            {
                NEspecialista.Insertar(especialistaCMP, especialistaNombre, especialistaApellido, especialidadcodigo);
                // ... (Maneja la respuesta de la función Insertar)
            }
            else
            {
                MessageBox.Show("Error en los valores de EspecialidadCodigo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ModificaEspecialista()
        {
            int especialistaCMP;
            string especialistaNombre = txtEspecialistaNombre.Text.Trim();
            string especialistaApellido = txtEspecialistaApellido.Text.Trim();
            int especialidadcodigo;


            // Validación del teléfono
            if (!int.TryParse(txtEspecialistaCMP.Text.Trim(), out especialistaCMP))
            {
                MessageBox.Show("El CMP debe ser un número entero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validación básica (puedes agregar más validaciones)
            if (string.IsNullOrEmpty(especialistaNombre) ||
                string.IsNullOrEmpty(especialistaApellido))
            {
                MessageBox.Show("Los campos nombre y apellido son obligatorios.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Verifica si EECC y Genero son enteros antes de convertirlos
            if (int.TryParse(cbxEspecialidadCodigo.SelectedItem.ToString(), out especialidadcodigo))
            {
                NEspecialista.Actualizar(especialistaCMP, especialistaNombre, especialistaApellido, especialidadcodigo);
                // ... (Maneja la respuesta de la función Insertar)
            }
            else
            {
                MessageBox.Show("Error en los valores de EspecialidadCodigo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EliminaEspecialista()
        {
            // Obtener el ID del registro a eliminar
            string especialistacmp = txtEspecialistaCMP.Text;

            // Confirmar la eliminación
            if (MessageBox.Show("¿Está seguro de eliminar el registro?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // Eliminar el registro
                string rpta = NPaciente.Eliminar(especialistacmp);

                // Mostrar mensaje de éxito o error
                if (rpta == "Ok")
                {
                    MessageBox.Show("Registro eliminado exitosamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error al eliminar registro: " + rpta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            InsertaEspecialista();
            CargarDatosEspecialista();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ModificaEspecialista();
            CargarDatosEspecialista();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            EliminaEspecialista();
            CargarDatosEspecialista();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            BuscarDatosEspecialista();
        }
    }
}
