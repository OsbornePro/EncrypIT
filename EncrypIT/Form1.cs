using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics;

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
                textBox1.ResetText();
                foreach (string filePath in files)
                {
                    if (File.Exists(filePath) && filePath != files.Last()) 
                        textBox1.AppendText(filePath + Environment.NewLine);
                    else
                        textBox1.AppendText(filePath);
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
            string[] arrValue = textBox1.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            textBox4.Text = "";
            foreach (string strValue in arrValue)
            {
                if (File.Exists(strValue))
                {
                    textBox4.AppendText(Environment.NewLine);
                    textBox4.Text += "File location verified.";
                    textBox4.AppendText(Environment.NewLine);
                    textBox4.Text += "Executing file encryption";
                    textBox4.AppendText(Environment.NewLine);
                    File.Encrypt(strValue);
                    textBox4.Text += $"Completed encryption of file {strValue}";
                    textBox4.AppendText(Environment.NewLine);
                }  // End If
                else if (!File.Exists(strValue))
                {
                    textBox4.Text += $"{strValue} file does NOT exist";
                    textBox4.AppendText(Environment.NewLine);

                    if (Directory.Exists(strValue))
                    {
                        textBox4.AppendText(Environment.NewLine);
                        textBox4.Text += "Directory location verified.";
                        textBox4.AppendText(Environment.NewLine); 
                        textBox4.Text += "Executing folder encryption";
                        textBox4.AppendText(Environment.NewLine);
                        File.Encrypt(textBox1.Text);
                        textBox4.Text += $"Completed encryption of directory {strValue}";
                        textBox4.AppendText(Environment.NewLine);
                    }  // End If
                    else if (!Directory.Exists(strValue))
                    {
                        textBox4.Text += $"{strValue} directory does NOT exist.";
                        textBox4.AppendText(Environment.NewLine);
                    }  // End Else

                }  // End Else If
            }  // End ForEach
        }

        // Decrypt File or Folder
        private void Button2_Click(object sender, EventArgs e)
        {
            string[] arrValue = textBox1.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            textBox4.Text = "";
            foreach (string strValue in arrValue)
            {
                if (File.Exists(strValue))
                {
                    textBox4.AppendText(Environment.NewLine);
                    textBox4.Text += "File location verified.";
                    textBox4.AppendText(Environment.NewLine); 
                    textBox4.Text += "Executing file decryption";
                    textBox4.AppendText(Environment.NewLine);
                    File.Decrypt(strValue);
                    textBox4.Text += $"Completed decryption of file {strValue}";
                    textBox4.AppendText(Environment.NewLine);
                }  // End if
                else if (!File.Exists(strValue))
                {
                    textBox4.AppendText(Environment.NewLine);
                    textBox4.Text += $"{strValue} file does NOT exist";
                    textBox4.AppendText(Environment.NewLine);
                    if (Directory.Exists(strValue))
                    {
                        textBox4.AppendText(Environment.NewLine);
                        textBox4.Text += "Directory location verified.";
                        textBox4.AppendText(Environment.NewLine);
                        textBox4.Text += "Executing folder decryption";
                        textBox4.AppendText(Environment.NewLine);
                        File.Decrypt(strValue);
                        textBox4.Text += $"Completed decryption of directory {strValue}";
                        textBox4.AppendText(Environment.NewLine);
                    }  // End if
                    else if (!Directory.Exists(strValue))
                    {
                        textBox4.AppendText(Environment.NewLine);
                        textBox4.Text += $"{strValue} directory does NOT exist.";
                        textBox4.AppendText(Environment.NewLine);
                    }  // End else if
                }  // End if
            }  // End foreach
        }

        // Backup Certificate Key
        private void Button3_Click(object sender, EventArgs e)
        {
            string certTemplateName = "Basic EFS"; // First certificate template search looks for the Basic EFS template before failing over to key usage
            int keyUsage = 32; //Key Encipherment value 32 means the key can be used for encryption

            // Defines the file name of the pfx file that will be saved/created
            string saveCertificate = "efs-backup.pfx";

            // Defines the directory to save the backup EFS certificate. Default is the user profile directory C:\Users\username
            string savePath = Environment.GetEnvironmentVariable("USERPROFILE");

            // Generating a password to protect the private key
            int passLength = 20;
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 <= passLength--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            string myPassword = res.ToString();


            // Define certificate and store to look in
            X509Store storeLocation = new X509Store("My", StoreLocation.CurrentUser);

            // Get EFS Certificate
            storeLocation.Open(OpenFlags.ReadWrite);
            IEnumerable certs = storeLocation.Certificates.Find(X509FindType.FindByTemplateName, certTemplateName, true);
            X509Certificate certTemplate = certs.OfType<X509Certificate>().FirstOrDefault();
            

            // If no certificate has been asssigned for the defined template or the wrong template is specified you will receive this message
            if (certTemplate is null)
            {
                textBox4.Text += $"No certificates could be found be found with a certiticate template name of {certTemplateName}. Attempting to obtain a certificate with a keyUsage value of 32.";
                textBox4.AppendText(Environment.NewLine);
                IEnumerable cer = storeLocation.Certificates.Find(X509FindType.FindByKeyUsage, keyUsage, true);
                X509Certificate cerTemplate = cer.OfType<X509Certificate>().FirstOrDefault();

                // Export certificate to object
                byte[] certData = cerTemplate.Export(X509ContentType.Pfx, myPassword);

                // Save certificate to file
                File.WriteAllBytes($"{savePath}\\{saveCertificate}", certData);

                if (File.Exists($"{savePath}\\{saveCertificate}"))
                {
                    // Inform user of save location
                    textBox4.Text = $"Backup of your key is saved too {savePath}\\{saveCertificate}.";
                    textBox4.AppendText(Environment.NewLine);
                    textBox4.Text += $"The password for your backup key is {myPassword}. The password is copied to your clipboard and can be pasted.";
                    textBox4.AppendText(Environment.NewLine);
                    textBox4.Text += "DO NOT LOSE THIS PASSWORD! Keep it stored somewhere safe like a password manager.";
                    textBox4.AppendText(Environment.NewLine);
                    Clipboard.SetText(myPassword);
                }
                else if (!File.Exists($"{savePath}\\{saveCertificate}"))
                {
                    // Display the value to the console.
                    textBox4.Text = $"Unable to save certificate to {savePath}\\{saveCertificate}. Ensure you have an EFS certificate";
                    textBox4.AppendText(Environment.NewLine);
                }
            }
            else
            {
                // Export certificate to object
                byte[] certData = certTemplate.Export(X509ContentType.Pfx, myPassword);

                // Save certificate to file
                File.WriteAllBytes($"{savePath}\\{saveCertificate}", certData);

                if (File.Exists($"{savePath}\\{saveCertificate}"))
                {
                    // Inform user of save location
                    textBox4.Text = $"Backup of your key is saved too {savePath}\\{saveCertificate}.";
                    textBox4.AppendText(Environment.NewLine);
                    textBox4.Text += $"The password for your backup key is {myPassword}. The password is copied to your clipboard and can be pasted.";
                    textBox4.AppendText(Environment.NewLine); 
                    textBox4.Text += "DO NOT LOSE THIS PASSWORD! Keep it stored somewhere safe like a password manager.";
                    textBox4.AppendText(Environment.NewLine);
                    Clipboard.SetText(myPassword);
                }
                else if (!File.Exists($"{savePath}\\{saveCertificate}"))
                {
                    // Display the value to the console.
                    textBox4.Text += $"Unable to save certificate to {savePath}\\{saveCertificate}. Ensure you have the appropriate permissions to save your backup here.";
                    textBox4.AppendText(Environment.NewLine);
                }
            }
        }

        // Add Access
        private void Button4_Click(object sender, EventArgs e)
        {
            string[] arrValue = textBox1.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            string[] userArrValue = textBox3.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            foreach (string strValue in arrValue)
            {
                if (File.Exists(strValue))
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
                else if (!File.Exists(strValue))
                {
                    textBox4.AppendText(Environment.NewLine);
                    textBox4.Text += $"\n{strValue} file does NOT exist";
                    textBox4.AppendText(Environment.NewLine);
                    if (Directory.Exists(strValue))
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
            string[] arrValue = textBox1.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            string[] userArrValue = textBox3.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            foreach (string strValue in arrValue)
            {
                if (File.Exists(strValue))
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
                else if (!File.Exists(strValue))
                {
                    textBox4.AppendText(Environment.NewLine);
                    textBox4.Text = $"{strValue} file does NOT exist";
                    textBox4.AppendText(Environment.NewLine);

                    if (Directory.Exists(strValue))
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
            string[] arrValue = textBox1.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            foreach (string strValue in arrValue)
            {
                if (File.Exists(strValue))
                {
                    textBox4.AppendText(Environment.NewLine);
                    textBox4.Text += "============================================================";
                    textBox4.Text += $"\nFile existence verified";
                    textBox4.AppendText(Environment.NewLine);
                    ProcessStartInfo proces1sstartinfo = new ProcessStartInfo
                    {
                        Arguments = $"/c {strValue}",
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
                else if (!File.Exists(strValue))
                {
                    textBox4.AppendText(Environment.NewLine);
                    textBox4.Text += $"{strValue} file does NOT exist.";
                    textBox4.AppendText(Environment.NewLine);

                    if (Directory.Exists(strValue))
                    {
                        textBox4.AppendText(Environment.NewLine);
                        textBox4.Text += "============================================================";
                        textBox4.Text = "Directory location verified";
                        textBox4.AppendText(Environment.NewLine);
                        ProcessStartInfo proces1sstartinfo = new ProcessStartInfo
                        {
                            Arguments = $"/c {strValue}",
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
                    else if (!Directory.Exists(strValue))
                    {
                        textBox4.AppendText(Environment.NewLine);
                        textBox4.Text += "============================================================";
                        textBox4.Text += $"{strValue} directory does NOT exist.";
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
    }
}
