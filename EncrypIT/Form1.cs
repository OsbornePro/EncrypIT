using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Collections;
using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;


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

        // Drag Enter
        private void TextBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }

        // Drag Drop
        private void TextBox1_DragDrop(object sender, DragEventArgs e)
        {
            // get all files droppeds
            if (e.Data.GetData(DataFormats.FileDrop) is string[] files && files.Any())
            {
                Encrypt.ResetText();
                foreach (string filePath in files)
                {
                    if ((File.Exists(filePath) || Directory.Exists(filePath)) && filePath != files.Last())
                        Encrypt.AppendText(filePath + Environment.NewLine);
                    else
                        Encrypt.AppendText(filePath);
                }  // End foreach
            }  // End if
        }

        // DragOver
        private void TextBox1_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link;
            else
                e.Effect = DragDropEffects.None;
        }

        // Encrypt File or Folder
        private void Button1_Click(object sender, EventArgs e)
        {
            string[] arrValue = Encrypt.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            textBox4.Text = "";
            foreach (string strValue in arrValue)
            {
                if (File.Exists(strValue) && (!strValue.Contains('"')) && (!strValue.Contains("'")))
                {
                    textBox4.AppendText(Environment.NewLine);
                    textBox4.Text += "File location verified.";
                    textBox4.AppendText(Environment.NewLine);
                    textBox4.Text += "Executing file encryption";
                    textBox4.AppendText(Environment.NewLine);
                    try
                    {
                        File.Encrypt(strValue);
                    }
                    catch
                    {
                        textBox4.Text += $"{strValue} is already encrypted";
                        textBox4.AppendText(Environment.NewLine);
                    }
                    textBox4.Text += $"Completed encryption of {strValue}";
                    textBox4.AppendText(Environment.NewLine);
                }  // End If
                else if (!File.Exists(strValue) && (!strValue.Contains('"')) && (!strValue.Contains("'")))
                {
                    if (Directory.Exists(strValue) && (!strValue.Contains('"')) && (!strValue.Contains("'")))
                    {
                        textBox4.AppendText(Environment.NewLine);
                        textBox4.Text += "Directory location verified.";
                        textBox4.AppendText(Environment.NewLine);
                        try
                        {
                            if (checkBox1.Checked == true)
                            {
                                textBox4.Text += $"Telling {strValue} to encrypt newly saved files";
                                textBox4.AppendText(Environment.NewLine);
                                File.Encrypt(strValue);
                                foreach (string dir in Directory.GetDirectories(strValue))
                                {
                                    foreach (string file in Directory.GetFiles(dir))
                                    {
                                        textBox4.Text += $"Encrypting {file}";
                                        textBox4.AppendText(Environment.NewLine);
                                        File.Encrypt(file);
                                    }
                                }
                            }
                            else
                            {
                                textBox4.Text += "Telling directory to encrypt newly saved files";
                                textBox4.AppendText(Environment.NewLine);
                                File.Encrypt(strValue);
                            }
                        }
                        catch
                        {
                            textBox4.Text += $"{strValue} is already encrypted or cannot be encrypted by current user";
                            textBox4.AppendText(Environment.NewLine);
                        }
                        textBox4.Text += $"Any New Files added too {strValue} will be encrypted.";
                        textBox4.AppendText(Environment.NewLine);
                        textBox4.Text += $"Current folder contents will not be encrypted to help prevent this application from being used as ransomware";
                        textBox4.AppendText(Environment.NewLine);
                    }  // End If
                    else if (!Directory.Exists(strValue))
                    {
                        textBox4.Text += $"{strValue} does NOT exist.";
                        textBox4.AppendText(Environment.NewLine);
                    }  // End Else

                }  // End Else If
            }  // End ForEach
        }

        // Decrypt File or Folder
        private void Button2_Click(object sender, EventArgs e)
        {
            string[] arrValue = Encrypt.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            textBox4.Text = "";
            foreach (string strValue in arrValue)
            {
                if (File.Exists(strValue) && (!strValue.Contains('"')) && (!strValue.Contains("'")))
                {
                    textBox4.AppendText(Environment.NewLine);
                    textBox4.Text += "File location verified.";
                    textBox4.AppendText(Environment.NewLine);
                    textBox4.Text += "Executing file decryption";
                    textBox4.AppendText(Environment.NewLine);
                    try
                    {
                        File.Decrypt(strValue);
                    }
                    catch
                    {
                        textBox4.Text += $"{strValue} is not encrypted or can not be decrypted";
                        textBox4.AppendText(Environment.NewLine);
                    }
                    textBox4.Text += $"Completed decryption of file {strValue}";
                    textBox4.AppendText(Environment.NewLine);
                }  // End if
                else if (!File.Exists(strValue) && (!strValue.Contains('"')) && (!strValue.Contains("'")))
                {
                    if (Directory.Exists(strValue) && (!strValue.Contains('"')) && (!strValue.Contains("'")))
                    {
                        textBox4.AppendText(Environment.NewLine);
                        textBox4.Text += "Directory location verified.";
                        textBox4.AppendText(Environment.NewLine);
                        try
                        {
                            if (checkBox1.Checked == true)
                            {
                                textBox4.Text += $"Telling {strValue} NOT to encrypt newly saved files";
                                textBox4.AppendText(Environment.NewLine);
                                File.Encrypt(strValue);
                                foreach (string dir in Directory.GetDirectories(strValue))
                                {
                                    foreach (string file in Directory.GetFiles(dir))
                                    {
                                        textBox4.Text += $"Decrypting {file}";
                                        textBox4.AppendText(Environment.NewLine);
                                        File.Decrypt(file);
                                    }
                                }
                            }
                            else
                            {
                                textBox4.Text += "Telling directory to encrypt newly saved files";
                                textBox4.AppendText(Environment.NewLine);
                                File.Decrypt(strValue);
                            }
                        }
                        catch
                        {
                            textBox4.Text += $"{strValue} is not encrypted or can not be decrypted";
                            textBox4.AppendText(Environment.NewLine);
                        }
                        textBox4.Text += $"New Files added to {strValue} will NO longer be encrypted.";
                        textBox4.AppendText(Environment.NewLine);
                        textBox4.Text += $"Current folder contents will NOT be decrypted to prevent unintedned decryptions";
                        textBox4.AppendText(Environment.NewLine);
                    }  // End if
                    else if (!Directory.Exists(strValue))
                    {
                        textBox4.AppendText(Environment.NewLine);
                        textBox4.Text += $"{strValue} does NOT exist.";
                        textBox4.AppendText(Environment.NewLine);
                    }  // End else if
                }  // End if
            }  // End foreach
        }

        // Backup Certificate Key
        private void Button3_Click(object sender, EventArgs e)
        {
            textBox4.Text = "";
            string certTemplateName = "Basic EFS"; // First certificate template search looks for the Basic EFS template before failing over to key usage
            int keyUsage = 32; //Key Encipherment value 32 means the key can be used for encryption

            // Prompt user for password
            PasswordBox getPassword = new PasswordBox();
            string myPassword = getPassword.Show("Enter a password to protect the private key on your certificate", "Password Prompt");

            SaveFileDialog saveFileDialog1 = new SaveFileDialog
            {
                Filter = "pfx files (*.pfx)|*.pfx|All files (*.*)|*.*",
                DefaultExt = "*.pfx",
                FileName = "efs-backup.pfx",
                InitialDirectory = Environment.GetEnvironmentVariable("USERPROFILE"),
                FilterIndex = 2,
                RestoreDirectory = true
            };

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Saves the Image via a FileStream created by the OpenFile method.
                FileStream fs = (FileStream)saveFileDialog1.OpenFile();
                if (fs != null)
                {
                    fs.Close();
                    // Define certificate and store to look in
                    X509Store storeLocation = new X509Store("My", StoreLocation.CurrentUser);

                    // Get EFS Certificate by template name
                    storeLocation.Open(OpenFlags.ReadWrite);
                    IEnumerable certs = storeLocation.Certificates.Find(X509FindType.FindByTemplateName, certTemplateName, true);
                    X509Certificate certTemplate = certs.OfType<X509Certificate>().FirstOrDefault();

                    // If no certificate has been asssigned for the defined template or the wrong template is specified you will receive this message
                    if (certTemplate is null)
                    {
                        textBox4.Text = $"No certificates could be found be found with a certiticate template name of {certTemplateName}. Attempting to obtain a certificate with a keyUsage value of 32.";
                        textBox4.AppendText(Environment.NewLine);
                        textBox4.AppendText(Environment.NewLine);
                        IEnumerable cer = storeLocation.Certificates.Find(X509FindType.FindByKeyUsage, keyUsage, true);
                        X509Certificate cerTemplate = cer.OfType<X509Certificate>().FirstOrDefault();

                        // Export certificate to object
                        byte[] certData = cerTemplate.Export(X509ContentType.Pfx, myPassword.ToString());

                        // Save certificate to file
                        File.WriteAllBytes($"{saveFileDialog1.FileName}", certData);
                        if (File.Exists($"{saveFileDialog1.FileName}"))
                        {
                            // Inform user of save location
                            textBox4.Text += $"Backup of your key is saved too {saveFileDialog1.FileName}.";
                            textBox4.AppendText(Environment.NewLine);
                            textBox4.AppendText(Environment.NewLine);
                            textBox4.Text += $"The password for your backup key is {myPassword}";
                            textBox4.AppendText(Environment.NewLine);
                            textBox4.AppendText(Environment.NewLine);
                            Clipboard.SetText(myPassword.ToString());
                            textBox4.Text += "The password is now copied to your clipboard and can be pasted.";
                            textBox4.AppendText(Environment.NewLine);
                            textBox4.AppendText(Environment.NewLine);
                            textBox4.Text += "DO NOT LOSE THIS PASSWORD! Keep it stored somewhere safe like a password manager.";
                            textBox4.AppendText(Environment.NewLine);
                            textBox4.AppendText(Environment.NewLine);
                        }
                        else if (!File.Exists($"{saveFileDialog1.FileName}"))
                        {
                            // Display the value to the console.
                            textBox4.Text = $"Unable to save certificate to {saveFileDialog1.FileName}. Ensure you have an EFS certificate";
                            textBox4.AppendText(Environment.NewLine);
                        }
                    }
                    else
                    {
                        // Export certificate to object
                        byte[] certData = certTemplate.Export(X509ContentType.Pfx, myPassword.ToString());

                        // Save certificate to file
                        File.WriteAllBytes($"{saveFileDialog1.FileName}", certData);
                        if (File.Exists($"{saveFileDialog1.FileName}"))
                        {
                            // Inform user of save location
                            textBox4.Text = $"Backup of your key is saved too {saveFileDialog1.FileName}.";
                            textBox4.AppendText(Environment.NewLine);
                            textBox4.Text += $"The password for your backup key is {myPassword}. The password is copied to your clipboard and can be pasted.";
                            textBox4.AppendText(Environment.NewLine);
                            textBox4.Text += "DO NOT LOSE THIS PASSWORD! Keep it stored somewhere safe like a password manager.";
                            textBox4.AppendText(Environment.NewLine);
                            Clipboard.SetText(myPassword.ToString());
                        }
                        else if (!File.Exists($"{saveFileDialog1.FileName}"))
                        {
                            // Display the value to the console.
                            textBox4.Text = $"Unable to save certificate to {saveFileDialog1.FileName}. Ensure you have the appropriate permissions to save your backup here.";
                            textBox4.AppendText(Environment.NewLine);
                        }
                    }
                }
            }
        }

        // Add Access
        private void Button4_Click(object sender, EventArgs e)
        {
            textBox4.Text = "";
            string[] arrValue = Encrypt.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            string[] userArrValue = textBox3.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            foreach (string strValue in arrValue)
            {
                if (File.Exists(strValue) && (!strValue.Contains('"')) && (!strValue.Contains("'")))
                {
                    textBox4.AppendText(Environment.NewLine);
                    textBox4.Text += "File location verified";
                    textBox4.AppendText(Environment.NewLine);
                    foreach (string userValue in userArrValue)
                    {
                        ProcessStartInfo processtartinfo = new ProcessStartInfo
                        {
                            Arguments = $"/adduser /user:\"{userValue}\" /B /H \"{strValue}\"",
                            WindowStyle = ProcessWindowStyle.Hidden,
                            FileName = "C:\\Windows\\System32\\cipher.exe",
                            RedirectStandardOutput = true,
                            UseShellExecute = false,
                        };

                        textBox4.Text += $"Please wait...";
                        textBox4.AppendText(Environment.NewLine);
                        textBox4.Text += $"Verifying {userValue} can be granted access";
                        using (var process = Process.Start(processtartinfo))
                        {
                            var standardOutput = new StringBuilder();
                            while (!process.HasExited)
                            {
                                textBox4.Text += standardOutput.Append(process.StandardOutput.ReadToEnd()).ToString();
                            }  // End while
                        }  // End using
                    } // End foreach
                }  // End If
                else if (!File.Exists(strValue) && (!strValue.Contains('"')) && (!strValue.Contains("'")))
                {
                    textBox4.AppendText(Environment.NewLine);
                    textBox4.Text += $"\n{strValue} file does NOT exist";
                    textBox4.AppendText(Environment.NewLine);
                    if (Directory.Exists(strValue) && (!strValue.Contains('"')) && (!strValue.Contains("'")))
                    {
                        textBox4.AppendText(Environment.NewLine);
                        textBox4.Text += "Directory location verified";
                        textBox4.AppendText(Environment.NewLine);
                        foreach (string userValue in userArrValue)
                        {
                            ProcessStartInfo proces1sstartinfo = new ProcessStartInfo
                            {
                                Arguments = $"/adduser /user:\"{userValue}\" /S:\"{strValue}\" /B /H",
                                WindowStyle = ProcessWindowStyle.Hidden,
                                FileName = "C:\\Windows\\System32\\cipher.exe",
                                RedirectStandardOutput = true,
                                UseShellExecute = false,
                            };

                            textBox4.Text += $"Please wait...";
                            textBox4.AppendText(Environment.NewLine);
                            textBox4.Text += $"Verifying {userValue} can be granted access";
                            textBox4.AppendText(Environment.NewLine);
                            using (var process = Process.Start(proces1sstartinfo))
                            {
                                var standardOutput = new StringBuilder();

                                while (!process.HasExited)
                                {
                                    textBox4.Text = standardOutput.Append(process.StandardOutput.ReadToEnd()).ToString();
                                }  // End while
                            }  // End using
                        }  // End foreach
                    }  // End If
                    else if (!Directory.Exists(strValue))
                    {
                        textBox4.AppendText(Environment.NewLine);
                        textBox4.Text += $"{strValue} directory does NOT exist.";
                        textBox4.AppendText(Environment.NewLine);
                    }  // End Else

                }  // End Else If
            }  // End foreach
        }

        // Remove Access
        private void Button5_Click(object sender, EventArgs e)
        {
            textBox4.Text = "";
            string[] arrValue = Encrypt.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            string[] userArrValue = textBox3.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            foreach (string strValue in arrValue)
            {
                if (File.Exists(strValue) && (!strValue.Contains('"')) && (!strValue.Contains("'")))
                {
                    textBox4.AppendText(Environment.NewLine);
                    textBox4.Text = $"{strValue} file location verified";
                    textBox4.AppendText(Environment.NewLine);
                    foreach (string userValue in userArrValue)
                    {
                        ProcessStartInfo processtartinfo = new ProcessStartInfo
                        {
                            Arguments = $"-Command \"$Thumbprint = ((cipher /C '{strValue}' | Select-String -Pattern '{userValue}' -Context 1,2 | findstr Certificate` thumbprint).Trim()).Replace(\'Certificate thumbprint: \',\'\'); cipher /removeuser '{userValue}' /certhash:$Thumbprint /B /H '{strValue}'\"",
                            WindowStyle = ProcessWindowStyle.Hidden,
                            FileName = "C:\\Windows\\System32\\WindowsPowerShell\\v1.0\\powershell.exe",
                            RedirectStandardOutput = true,
                            UseShellExecute = false,
                        };

                        textBox4.Text += $"Please wait...";
                        textBox4.AppendText(Environment.NewLine);
                        textBox4.Text += $"Verifying {strValue} can have access removed. Thumbprint is";
                        textBox4.AppendText(Environment.NewLine);
                        using (var process = Process.Start(processtartinfo))
                        {
                            var standardOutput = new StringBuilder();
                            while (!process.HasExited)
                            {
                                textBox4.Text += standardOutput.Append(process.StandardOutput.ReadToEnd()).ToString();
                            }  // End while
                        }  // End using
                    }  // End foreach
                }  // End if
                else if (!File.Exists(strValue) && (!strValue.Contains('"')) && (!strValue.Contains("'")))
                {
                    textBox4.AppendText(Environment.NewLine);
                    textBox4.Text = $"{strValue} file does NOT exist";
                    textBox4.AppendText(Environment.NewLine);

                    if (Directory.Exists(strValue) && (!strValue.Contains('"')) && (!strValue.Contains("'")))
                    {
                        textBox4.AppendText(Environment.NewLine);
                        textBox4.Text = "Directory location verified";
                        textBox4.AppendText(Environment.NewLine);
                        foreach (string userValue in userArrValue)
                        {
                            ProcessStartInfo processtartinfo = new ProcessStartInfo
                            {
                                Arguments = $"-Command \"$Thumbprint = ((cipher /C '{strValue}' | Select-String -Pattern '{userValue}' -Context 1,2 | findstr Certificate` thumbprint).Trim()).Replace(\'Certificate thumbprint: \',\'\'); cipher /removeuser '{userValue}' /certhash:$Thumbprint /S:'{strValue}' /B /H\"",
                                WindowStyle = ProcessWindowStyle.Hidden,
                                FileName = "C:\\Windows\\System32\\WindowsPowerShell\\v1.0\\powershell.exe",
                                RedirectStandardOutput = true,
                                UseShellExecute = false,
                            };

                            textBox4.Text += $"Please wait...";
                            textBox4.AppendText(Environment.NewLine);
                            textBox4.Text += $"Verifying {strValue} can be granted access";
                            textBox4.AppendText(Environment.NewLine);
                            using (var process = Process.Start(processtartinfo))
                            {
                                var standardOutput = new StringBuilder();

                                while (!process.HasExited)
                                {
                                    textBox4.Text += standardOutput.Append(process.StandardOutput.ReadToEnd()).ToString();
                                }  // End while
                            }  // End using
                        } // End foreach
                    }  // End if
                    else if (!Directory.Exists(strValue))
                    {
                        textBox4.AppendText(Environment.NewLine);
                        textBox4.Text += $"{strValue} directory does NOT exist.";
                        textBox4.AppendText(Environment.NewLine);
                    }  // End else

                }  // End else if
            }  // End foreach
        }

        // Get File Info Button
        private void Button6_Click(object sender, EventArgs e)
        {
            textBox4.Text = "";
            string[] arrValue = Encrypt.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            foreach (string strValue in arrValue)
            {
                textBox4.AppendText(strValue.ToString());
                if (File.Exists(strValue) && (!strValue.Contains('"')) && (!strValue.Contains("'")))
                {
                    textBox4.AppendText(Environment.NewLine);
                    textBox4.Text += "============================================================";
                    textBox4.AppendText(Environment.NewLine);
                    textBox4.Text += $"\nFile existence verified";
                    textBox4.AppendText(Environment.NewLine);
                    ProcessStartInfo proces1sstartinfo = new ProcessStartInfo
                    {
                        Arguments = $"/c \"{strValue}\"",
                        WindowStyle = ProcessWindowStyle.Hidden,
                        FileName = "C:\\Windows\\System32\\cipher.exe",
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                    };
                    using (var process = Process.Start(proces1sstartinfo))
                    {
                        var standardOutput = new StringBuilder();
                        textBox4.Text += "============================================================";
                        while (!process.HasExited)
                        {
                            textBox4.Text += standardOutput.Append(process.StandardOutput.ReadToEnd()).ToString();
                        }  // End while
                    }  // End using
                }  // End if
                else if (!File.Exists(strValue) && (!strValue.Contains('"')) && (!strValue.Contains("'")))
                {
                    textBox4.AppendText(Environment.NewLine);
                    textBox4.Text += $"{strValue} file does NOT exist.";
                    textBox4.AppendText(Environment.NewLine);

                    if (Directory.Exists(strValue) && (!strValue.Contains('"')) && (!strValue.Contains("'")))
                    {
                        textBox4.AppendText(Environment.NewLine);
                        textBox4.Text += "============================================================";
                        textBox4.AppendText(Environment.NewLine);
                        textBox4.Text += "Directory location verified";
                        textBox4.AppendText(Environment.NewLine);
                        ProcessStartInfo proces1sstartinfo = new ProcessStartInfo
                        {
                            Arguments = $"/c \"{strValue}\"",
                            WindowStyle = ProcessWindowStyle.Hidden,
                            FileName = "C:\\Windows\\System32\\cipher.exe",
                            RedirectStandardOutput = true,
                            UseShellExecute = false,
                        };
                        using (var process = Process.Start(proces1sstartinfo))
                        {
                            var standardOutput = new StringBuilder();
                            textBox4.Text += "============================================================";
                            textBox4.AppendText(Environment.NewLine);
                            while (!process.HasExited)
                            {
                                textBox4.Text += standardOutput.Append(process.StandardOutput.ReadToEnd()).ToString();
                            }  // End while
                        }  // End using
                    }  // End if
                    else if (!Directory.Exists(strValue) && (!strValue.Contains('"')) && (!strValue.Contains("'")))
                    {
                        textBox4.AppendText(Environment.NewLine);
                        textBox4.Text += "============================================================";
                        textBox4.AppendText(Environment.NewLine);
                        textBox4.Text += $"{strValue} does NOT exist.";
                        textBox4.AppendText(Environment.NewLine);
                    }  // End else
                }  // End else if
            }  // End foreach
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
    class PasswordBox
    { 
        // Below is code for the password input box
        private Form passwordForm;
        public string Show(string prompt, string title)
        {
            passwordForm = new Form();
            FlowLayoutPanel FL = new FlowLayoutPanel();
            Label lbl = new Label();
            TextBox txt = new TextBox();
            Button ok = new Button();
            Button autogen = new Button();

            passwordForm.Font = new Font("Arial", 9, FontStyle.Bold);
            passwordForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            passwordForm.StartPosition = FormStartPosition.CenterScreen;
            passwordForm.Width = 400;
            passwordForm.Height = 175;

            passwordForm.Text = title;
            lbl.Text = prompt;
            ok.Text = "OK";
            autogen.Text = "Auto Generate";
            txt.PasswordChar = '*';

            ok.FlatStyle = FlatStyle.Flat;
            ok.BackColor = SystemColors.ButtonShadow;
            ok.ForeColor = SystemColors.ButtonHighlight;
            ok.Cursor = Cursors.Hand;

            autogen.FlatStyle = FlatStyle.Flat;
            autogen.BackColor = SystemColors.ButtonShadow;
            autogen.ForeColor = SystemColors.ButtonHighlight;
            autogen.Cursor = Cursors.Hand;

            FL.Left = 0;
            FL.Top = 0;
            FL.Width = passwordForm.Width;
            FL.Height = passwordForm.Height;
            FL.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            FL.Padding = new Padding(10);
            FL.FlowDirection = FlowDirection.TopDown;

            ok.Width = FL.Width - 35;
            txt.Width = ok.Width;
            autogen.Width = ok.Width;
            lbl.Width = ok.Width;

            ok.Click += new EventHandler(OkClick);
            autogen.Click += new EventHandler(AutoGenClick);
            txt.KeyPress += new KeyPressEventHandler(TxtEnter);

            FL.Controls.Add(lbl);
            FL.Controls.Add(txt);
            FL.Controls.Add(ok);
            FL.Controls.Add(autogen);
            passwordForm.Controls.Add(FL);

            passwordForm.ShowDialog();
            DialogResult DR = passwordForm.DialogResult;
            passwordForm.Dispose();
            passwordForm = null;
            if (DR == DialogResult.OK)
            {
                return txt.Text;
            }
            else
            {
                // Generating a password to protect the private key
                int passLength = 20;
                const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!";
                StringBuilder res = new StringBuilder();
                Random rnd = new Random();
                while (0 <= passLength--)
                {
                    res.Append(valid[rnd.Next(valid.Length)]);
                }
                string myPassword = res.ToString();
                return myPassword;
            }
        }
        private void OkClick(object sender, EventArgs e)
        {
            passwordForm.DialogResult = DialogResult.OK;
            passwordForm.Close();
        }
        private void AutoGenClick(object sender, EventArgs e)
        {
            passwordForm.DialogResult = DialogResult.Cancel;
            passwordForm.Close();
        }
        private void TxtEnter(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) { OkClick(null, null); }
        }
    }
}
