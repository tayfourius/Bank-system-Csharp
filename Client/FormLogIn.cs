using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net.Sockets;

namespace Client
{
    public partial class FormLogIn : Form
    {
        //Creating a variable of type FormLogIn so it will be used in showing and hiding FormLogIn.
        public static FormLogIn FormLog = new FormLogIn();

        public FormLogIn()
        {
            InitializeComponent();
        }


        #region Log In Button
        private void btnLogIn_Click(object sender, EventArgs e)
        {
            try
            {
                string uName, Pass;
                uName = txtUserName.Text;
                Pass = txtPassword.Text;
                if (uName.IndexOf(',') == -1 && uName != "" && uName != " " && Pass.IndexOf(',') == -1 && Pass != "" && Pass != " ")
                {
                    TcpClient Client = new TcpClient("127.0.0.1", 9000);
                    NetworkStream Stream = Client.GetStream();
                    StreamReader reader = new StreamReader(Stream);
                    StreamWriter writer = new StreamWriter(Stream);
                    string Operation = "LogIn";
                    string netString = uName + "," + Pass;
                    string send = Operation + ";" + netString;
                    writer.WriteLine(send);
                    writer.Flush();
                    string result = reader.ReadLine();
                    if (result == "0") //invalid user name and password....opens the Client's Window.
                    {
                        FormClient.formC.Show();
                    }
                    else if (result == "1")
                        MessageBox.Show("Invalid User Name...please try again");
                    else if (result == "2")
                        MessageBox.Show("Invalid Password...please try again");
                    else MessageBox.Show("An I/O error has accured");
                    Stream.Close();
                    Client.Close();
                }
                else MessageBox.Show("Please make sure that no text field is Empty or has any commas ',' ....then try again");
            }
            catch
            {
                MessageBox.Show("Log In:An error accured...");
            }
        }
        #endregion

        private void FormLogIn_Load(object sender, EventArgs e)
        {

        }
    }
}
