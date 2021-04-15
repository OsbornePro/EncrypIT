using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace EncrypIT
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void TextBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else

                e.Effect = DragDropEffects.None;
        }

        private void TextBox1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = e.Data.GetData(DataFormats.FileDrop) as string[]; // get all files droppeds  
            if (files != null && files.Any())
                textBox1.Text = files.First(); //select the first one  
        }

        private void TextBox1_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link;
            else
                e.Effect = DragDropEffects.None;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string strValue = textBox1.Text;

            if (File.Exists(strValue))
            {
                label1.Text = $"{strValue} file location verified. Encrypting file...";
                File.Encrypt(textBox1.Text);
            }  // End If
            else if (!File.Exists(strValue))
            {
                label1.Text = $"{strValue} file does NOT exist";

                if (Directory.Exists(strValue))
                {
                    label1.Text = $"{strValue} directory location verified. Encrypting folder...";
                    File.Encrypt(textBox1.Text);
                }  // End If
                else if (!Directory.Exists(strValue))
                {
                    label1.Text = $"{strValue} directory does NOT exist.";
                }  // End Else

            }  // End Else If
            label1.Text = $"Completed encryption of {strValue}";
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            string strValue = textBox1.Text;

            if (File.Exists(strValue))
            {
                label1.Text = $"{strValue} file location verified. Decrypting file...";
                File.Decrypt(strValue);
            }  // End If
            else if (!File.Exists(strValue))
            {
                label1.Text = $"{strValue} file does NOT exist";

                if (Directory.Exists(strValue))
                {
                    label1.Text = $"{strValue} directory location verified. Decrypting folder...";
                    File.Decrypt(strValue);
                }  // End If
                else if (!Directory.Exists(strValue))
                {
                    label1.Text = $"{strValue} directory does NOT exist.";
                }  // End Else

            }  // End Else If
            label1.Text = $"Completed decryption of {strValue}";
        }
        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
