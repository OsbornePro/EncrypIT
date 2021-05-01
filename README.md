# EncrypIT
This program was created in an attempt to simplify the use of Encryption File System (EFS) for the everyday user. This application will allow a user to quickly and easily backup their EFS certificate to a PFX file. The application can also quickly and easily encrypt a file or directory using Encrypting File System (EFS). The instructions are simple which I have included in the topics below. <br>
<br>
There are a couple limitations to know when using EFS. 
1. You are only able to grant EFS permissions to a user, NOT groups. This is because public-private key pairs are used for encryption.
2. When encryption is set for a folder, EFS automatically encrypts all new files created in the folder and all files copied or moved into the folder. This means EFS file sharing can be applied only to individual EFS-encrypted files, and not to EFS-encrypted NTFS folders.
3. Using file shares for remote EFS operations requires a Windows 2000 or later domain environment because EFS must impersonate the user by using Kerberos delegation to encrypt or decrypt files for the user. This requires that the user must be logged on with a domain account that can be delegated and the computer must be a domain member in a domain that uses Kerberos authentication and the computer must be trusted for delegation.
4. Microsoft suggests using "Web Distributed Authoring and Versioning (WebDAV) Web folders" whenever possible for remote storage of encrypted files. Web folders require less administrative effort and are more secure than file shares. Web folders can also securely store and deliver encrypted files over the Internet by using standard HTTP file transfers. EFS encrypted files stored on Network Shares require the user to be able to log into the server. This is because network share files get decrypted on the server before being streamed to the device accessing it. The Web Folders decrypt the file on the local computer instead of the server.
5. A user must have either a local profile on the computer where EFS operations will occur or a roaming profile. If the user does not have a local profile on the remote computer or a roaming profile, EFS creates a local profile for the user on the remote computer.
__RESOURCE:__ [https://docs.microsoft.com/en-us/previous-versions/windows/it-pro/windows-xp/bb457116(v=technet.10)?redirectedfrom=MSDN](https://docs.microsoft.com/en-us/previous-versions/windows/it-pro/windows-xp/bb457116(v=technet.10)?redirectedfrom=MSDN)

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
