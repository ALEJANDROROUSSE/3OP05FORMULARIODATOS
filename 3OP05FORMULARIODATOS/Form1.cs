using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _3OP05FORMULARIODATOS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //Agregar controladores  de eventos TextChanged a los campos
            tbEdad.TextChanged += ValidarEdad;
            tbEstatura.TextChanged += ValidarEstatura;
            tbTelefono.TextChanged += ValidarTelefono;
            tbNombres.TextChanged += ValidarNombre;
            tbApellidos.TextChanged += ValidarApellidos;



        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string nombres = tbNombres.Text;
            string apellidos = tbApellidos.Text;
            string edad = tbEdad.Text;
            string telefono = tbTelefono.Text;
            string estatura = tbEstatura.Text;

            //Obtener el género seleccionado

            string genero = "";
            
            if (rbHombre.Checked)
            {
                genero = "Hombre";
            }
            else if (rbMujer.Checked)
            {
                genero = "Mujer";
            }
            //validar que los campos tengan el formato correcto
            if (EsEnterovalido(edad) && EsDecimalvalido(estatura) && EsEnterovalidoDe100Digitos(telefono) && EsTextovalido(nombres) && EsTextovalido(apellidos))
            {
                string Datos = $"Nombres:{nombres}\r\nApellidos:{apellidos}\r\nTeléfono:{telefono}kg\r\nEstatura:{estatura}cm\r\nEdad:{edad}años\r\nGénero:{genero}\r\n";
                //Guardar los Datos en un archivo de texto
                string rutaArchivo = "c:/Users/ybal/Documents/3o12.txt";
                bool archivoExiste = File.Exists(rutaArchivo);
                if (archivoExiste == false)
                {
                    File.WriteAllText(rutaArchivo, Datos);
                }
                else
                {
                    using (StreamWriter writer = new StreamWriter(rutaArchivo, true))
                    {
                        if (archivoExiste)
                        {
                            writer.WriteLine();
                        }
                        writer.WriteLine(Datos);
                    }
                }
                MessageBox.Show("Datos guardados con éxito:\n\n" + Datos, "información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Por favor,ingrese datos validos en los campos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private bool EsEnterovalido(string valor)
        {
            int resultado;
            return int.TryParse(valor, out resultado);
        }
        private bool EsDecimalvalido(string valor)
        {
            decimal resultado;
            return decimal.TryParse(valor,out resultado);
        }
        private bool EsEnterovalidoDe100Digitos(string valor)
        {
            long resultado;
            return long.TryParse(valor, out resultado) && valor.Length == 10;
        }
        private bool EsTextovalido(string valor)
        {
            return Regex.IsMatch(valor, @"^[a-zA-z\s]+$");
        }
        private void ValidarEdad(object sender, EventArgs e)
        {
            TextBox textBox=(TextBox)sender;
            if(!EsEnterovalido(textBox.Text))
            {
                MessageBox.Show("Por favor,ingrese una estatura válida.", "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox.Clear();
            }

        }
        private void ValidarEstatura(object sender, EventArgs e)
        {
            TextBox textBox=(TextBox)sender;
            if (!EsDecimalvalido(textBox.Text))
            {
                MessageBox.Show("Por favor,ingrese una Estatura válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox.Clear();
            }
        }
        private void ValidarTelefono(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string input = textBox.Text;

            if (input.Length > 10)
            {
                if (!EsEnterovalidoDe100Digitos(input))
                {
                    MessageBox.Show("Por favor, ingrese un número de teléfono válido de 10 digitos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox.Clear();

                }
                else if (!EsEnterovalidoDe100Digitos(input))
                {
                    MessageBox.Show("Por favor,ingrese un número de télefono válido de 10 digitos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }  
      private void ValidarNombre(object sender, EventArgs e)
      {
            TextBox textBox = (TextBox)sender;
            if (!EsTextovalido(textBox.Text))
            {
                MessageBox.Show("Por favor,ingrese un Nombre válido(solo letras y espacios).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox.Clear();
            }
      }
      
        private void ValidarApellidos(object sender, EventArgs e)
        {
            TextBox textBox1 = (TextBox)sender;
            if(!EsTextovalido(textBox1.Text)) 
            {
                MessageBox.Show("Por favor,ingrese apellidos válidos(solo letras y espacios).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Clear();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            tbNombres.Clear();
            tbApellidos.Clear();
            tbEstatura.Clear();
            tbTelefono.Clear();
            tbEdad.Clear();
            rbHombre.Checked = false;
            rbMujer.Checked = false;
        }
   
        
    }
}
