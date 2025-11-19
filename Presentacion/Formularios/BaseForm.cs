using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CentroAcopioApp.Presentacion.Formularios
{
    public class BaseForm : Form
    {
        public BaseForm()
        {
            // Abrir el formulario en el centro de la pantalla
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        // ---------------------------------------------------------
        // EJECUTAR CON CONTROL DE ERRORES
        // ---------------------------------------------------------
        protected void Ejecutar(Action accion)
        {
            try
            {
                accion();
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }

        // ---------------------------------------------------------
        // MENSAJES
        // ---------------------------------------------------------
        protected void MostrarInfo(string mensaje)
        {
            MessageBox.Show(mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        protected void MostrarError(string mensaje)
        {
            MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        protected bool Confirmar(string mensaje)
        {
            return MessageBox.Show(
                mensaje,
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            ) == DialogResult.Yes;
        }

        // ---------------------------------------------------------
        // COMBOBOX DropDownList
        // ---------------------------------------------------------
        protected void CargarCombo<T>(ComboBox combo, IEnumerable<T> datos, string display, string value,
            bool seleccionarPrimero = false)
        {
            combo.DataSource = null;
            combo.DropDownStyle = ComboBoxStyle.DropDownList;

            combo.DisplayMember = display;
            combo.ValueMember = value;

            combo.DataSource = datos.ToList();

            combo.SelectedIndex = seleccionarPrimero && combo.Items.Count > 0 ? 0 : -1;
        }

        // ---------------------------------------------------------
        // CONFIGURACIÓN GENERAL DGV
        // ---------------------------------------------------------
        protected void ConfigurarDataGrid(DataGridView dgv)
        {
            dgv.AutoGenerateColumns = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;

            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeRows = false;

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dgv.RowHeadersVisible = false;
            dgv.BackgroundColor = SystemColors.Window;
            dgv.BorderStyle = BorderStyle.FixedSingle;

            dgv.Font = new Font(dgv.Font.FontFamily, 12f, dgv.Font.Style); // Filas
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font(dgv.Font.FontFamily, 12f, FontStyle.Bold); // Encabezados
        }

        // ---------------------------------------------------------
        // CONFIGURAR QUÉ COLUMNAS MOSTRAR Y NOMBRE DE ENCABEZADOS
        // ---------------------------------------------------------
        protected void ConfigurarColumnas(
            DataGridView dgv,
            Dictionary<string, string> columnasVisiblesYHeaders,
            IEnumerable<string> columnasOcultas = null)
        {
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                // Columnas ocultas
                if (columnasOcultas != null && columnasOcultas.Contains(col.Name))
                {
                    col.Visible = false;
                    continue;
                }

                // Cambiar nombre de encabezado
                if (columnasVisiblesYHeaders.ContainsKey(col.Name))
                {
                    col.HeaderText = columnasVisiblesYHeaders[col.Name];
                    col.Visible = true;
                }
                else
                {
                    // Toda columna NO incluida en la lista, se oculta
                    col.Visible = false;
                }
            }
        }

        // ---------------------------------------------------------
        // LIMPIAR CONTROLES
        // ---------------------------------------------------------
        protected void LimpiarControles(Control parent = null)
        {
            if (parent == null)
                parent = this;

            foreach (Control c in parent.Controls)
            {
                if (c is TextBox)
                    ((TextBox)c).Clear();

                else if (c is ComboBox)
                    ((ComboBox)c).SelectedIndex = -1;

                else if (c is CheckBox)
                    ((CheckBox)c).Checked = false;

                else if (c is DateTimePicker)
                    ((DateTimePicker)c).Value = DateTime.Now;

                if (c.HasChildren)
                    LimpiarControles(c);
            }
        }

        // ---------------------------------------------------------
        // CONFIGURAR DGV EN TODOS LOS FORMULARIOS
        // ---------------------------------------------------------

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            AplicarConfiguracionATodosLosDataGrid(this);
        }

        private void AplicarConfiguracionATodosLosDataGrid(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is DataGridView dgv)
                    ConfigurarDataGrid(dgv);

                if (c.HasChildren)
                    AplicarConfiguracionATodosLosDataGrid(c);
            }
        }
    }
}