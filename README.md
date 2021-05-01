# EncrypIT
This program was created in an attempt to simplify the use of Encryption File System (EFS) for the everyday user. This application will allow a user to quickly and easily backup their EFS certificate to a PFX file. The application can also quickly and easily encrypt a file or directory using Encrypting File System (EFS). The instructions are simple.

__COMING SOON:__ In the future I will attempt to add a simple way to modify EFS users who have permissions to the soon to be encrypted file.

### How To Encrypt a File or Directory
1. Open the application
2. Drag and Drop a file or folder from File Explorer into the white area of the application.
3. Click the "_Encrypt_" button
The file is now encrypted using EFS and can only be accessed by the user who performed the encryption.

### How To Decrypt a File or Directory
1. Open the application
2. Drag and Drop a file or folder from File Explorer into the white area of the application.
3. Click the "_Decrypt_" button
The file you defined is now decrypted and can be accessed by anyone with appropriate NTFS permissions.

### How To Backup an EFS Certificate
1. Open the application
2. Click the "_Backup Key_" button

The "__Status Information__" section of the application will display the following information to you as well.
Your EFS certificate was saved to C:\\Users\\your username\\efs-backup.pfx. This was done to prevent accidental upload to a OneDrive cloud ensuring the EFS certificate stays on the local device.
A random 20 character password was set containing numbers, uppercase letters, and lowercase letters.
The password value is automatically copied directly to your clipboard. Be sure to save the password to a Password Manager to ensure it does not become lost. <br>
<br>
__NOTE:__ This application discovers the EFS certificate used based on first searching for a template with the name "Basic EFS" in the user's certificate store. If no certificate is found, it next looks for a "Key Usage" value of 32 in the users local certificate store. If you wish to define a different certificate template name to search for, modify __line 97__'s "__certTemplateName variable__" in the file "__Form1.cs__".

![EncrypIT](https://github.com/OsbornePro/EncrypIT/raw/main/EncrypIT/EncrypIT.png)
