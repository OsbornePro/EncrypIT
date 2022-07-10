Welcome to EncrypIT's documentation!
====================================

* `OsbornePro Site <https://osbornepro.com>`_
* `GitHub Page <https://github.com/OsbornePro/EncrypIT>`_
* `PayPal Donations <https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=AGKU5LWZA67XC&currency_code=USD&source=url>`_
* `LiberPay Donations <https://liberapay.com/tobor/donate>`_
* `Report Issues <https://osbornepro.com/schedule-or-contact>`_

**Contributions**

This is open source so if you wish to help contribute to the contents of this project feel free to reach out to me at info@osbornepro.com with your thoughts and ideas.
For more general information on this feel free to refer to the `CONTRIBUTING <https://github.com/OsbornePro/EncrypIT/blob/main/CONTRIBUTING.md>`_ documentation.

**General Summary for this project can be read at** https://github.com/OsbornePro/EncrypIT/blob/main/README.md
This repo contains the source code used to build the EncrypIT application.
EncrypIT is an attempt to simplify the use of Encryption File System (EFS) for the everyday user.
The application will allow a user to quickly and easily backup their EFS certificate to a PFX file, modify what domain users have permissions to decrypt their file, and of course encrypt and decrypt files and directories.
The instructions are simple which I have included in the topics below.
You can verify whether or not EFS is enabled by issuing the below command

.. code-block:: powershell

    If ((Get-ItemProperty -Path 'HKLM:\SOFTWARE\Microsoft\Windows NT\CurrentVersion\EFS' -Name "EfsConfiguration" -ErrorAction SilentlyContinue) -eq 1) { Write-Output "EFS is Disabled" } Else { Write-Output "EFS is Enabled" }



Ideas For Future Releases
=========================

1. Change the backend for granting and removing access to a file. The current method has to utilize a PowerShell window which seems unavoidable.
2. Attempt to allow a group to be defined in order to add all members of that group to a files permissions If you have any suggestions feel free to reach out to me at info@osbornepro.com.


Disclaimer
==========
**DISCLAIMER:** This tool was created to make the use of Encrypting File System (EFS) something the every day user can take advantage of. This tool does require the use of certificates to encrypt and decrypt data. I highly recommend following my `YouTube instructional video <https://www.youtube.com/watch?v=pSxSByxT25k>`_ for configuration EFS on your file server. This is to ensure you have an administrative way of decrypting files in the event someone loses their key.


How To Instructions
===================
EncrypIT is a stand alone application and does not require any installation.
Once you download the file you can simply open/run the program and it will start right up.
You can view the simplicity of the application in the images and instructions below.


How To Encrypt a File or Directory
----------------------------------

1. Open the application
2. Drag and Drop a file(s) or folder(s) from File Explorer into the top white area of the application.
3. Tick the "Recursive" button to encrypt the contents of any selected folders
4. Click the "Encrypt" button The file(s) are now encrypted using EFS and can only be accessed by the user who performed the encryption.

.. image:: https://raw.githubusercontent.com/OsbornePro/EncrypIT/main/EncrypIT/Encrypt.png
   :scale: 100
   :alt: Encrypt File(s)


How To Decrypt a File or Directory
----------------------------------

1. Open the application
2. Drag and Drop a file(s) or folder(s) from File Explorer into the top white area of the application.
3. Tick the "Recursive" button to decrypt the contents of any selected folders
4. Click the "Decrypt" button

The file(s) you defined are now decrypted and can be accessed by anyone with appropriate NTFS permissions.

.. image:: https://raw.githubusercontent.com/OsbornePro/EncrypIT/main/EncrypIT/Decrypt.png
   :scale: 100
   :alt: Decrypt File(s)


How to Grant a Domain User Access
---------------------------------

1. Open the application
2. Drag and Drop a file(s) or folder(s) from File Explorer into the top white area of the application.
3. Enter a persons email into the smaller text box. Put each new email address on a new line.
4. Click the "Grant Access" button

.. image:: https://raw.githubusercontent.com/OsbornePro/EncrypIT/main/EncrypIT/GrantAccess.png
   :scale: 100
   :alt: Grant Access to User(s)


How to Revoke a Domain Users Access
-----------------------------------

1. Open the application
2. Drag and Drop a file(s) or folder(s) from File Explorer into the top white area of the application.
3. Enter a persons username into the smaller text box. Put each new email address on a new line.
4. Click the "Revoke Access" button

.. image:: https://raw.githubusercontent.com/OsbornePro/EncrypIT/main/EncrypIT/RevokeAccess.png
   :scale: 100
   :alt: Remove Access From User(s)


How to view encryption info of a file(s)
----------------------------------------

1. Open the application
2. Drag and Drop a file(s) or folder(s) from File Explorer into the top white area of the application.
3. Click the "Get Encrypted File Info" button

.. image:: https://raw.githubusercontent.com/OsbornePro/EncrypIT/main/EncrypIT/GetInfo.png
   :scale: 100
   :alt: Get File Information


How To Backup an EFS Certificate
--------------------------------
1. Open the application
2. Click the "Backup Key" button
3. Enter a custom password or click "Auto Generate" to have one created for you
4. Select a location to save your backup EFS key in PFX file format
5. The password you created is now copied to your clipboard. Save it somewhere safe like a password manager


.. image:: https://raw.githubusercontent.com/OsbornePro/EncrypIT/main/EncrypIT/BackupKey.png
   :scale: 100
   :alt: Backup EFS Key


**SAVE LOCATION**

The **"Status Information"** section of the application will display the password you set as well as the location your backup was saved too.

NOTE: This application discovers the EFS certificate used based on first searching for a template with the name "Basic EFS" in the user's certificate store.
If no certificate is found, it next looks for a "Key Usage" value of 32 in the users local certificate store.
If you wish to define a different certificate template name to search for, modify line 152's "certTemplateName variable" in the file "Form1.cs" to match your custom EFS template name.


.. toctree::
   :maxdepth: 2
   :caption: Contents:



Indices and tables
==================

* :ref:`genindex`
* :ref:`modindex`
* :ref:`search`
