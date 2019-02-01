#region Code Info
/*
 * Coder : Eng. Ahmad Mhd. AlSamman //Honestly, I'm not an engineer yet...but I'm planning to be :D
 * Creation Date : 8/13/2011 - 10:00 PM
 * Last Modification : 8/14/2011 02:35 AM
 * Build Status : Complete Build
 * Description : This class is responsible for doign the various tasks an Operator may wish to do such as Adding Tellers,Deleting Tellers, Viewing Tellers' Information,
 *               and Modifeing Tellers.
 * Testing :  The code here has passed functional testing.
 * -------------------------------------------------------------------------------------------------------------------------------------------
 * -Files Stucture:
 * the Class contains functions that are responsible for Adding new Teller,Deleting an exsisting one, Modify Teller information, and viewing the details 
 * of all of the current tellers.
 * 
 * -It sends the data to the Blocking Layer which deals with the requests and returns back the result as a string[].
 * 
 * -Common Variables in those functions:
 * a.Operation: pre-initialized string variable contains the Operation's kind which will be sent to the blocking layer.
 * b.Result: a string[] contains the result recieved from the Blocking Layer at which determines the success of the operation or the kind of Error.
 * c.netString: is the variable to be sent to the Blcoking Layer as a second Parameter...Holding the Information required from the Blocking layer.
 * 
 * -All of the following functions return the varibale "Result"...so the upper layer determine the result of the Operation.
*/
#endregion

#region Included Namespaces
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Net;
using System.Net.Sockets;
#endregion

namespace Server
{
    public partial class FormOperator : Form
    {
        public FormOperator()
        {
            InitializeComponent();
        }

        static public void sms(string x)
        {
            MessageBox.Show(x);
        }

        #region Client Dealer function
        public void ClientDealer()//this function creates the TCPListener,TCPClient and start threading Clients Requests.
        {
            TcpListener Server = new TcpListener(IPAddress.Parse("127.0.0.1"), 9000);
            Server.Start();
            TcpClient Client;
            NetworkLayer net;
            while (true)
            {
                Client = Server.AcceptTcpClient();
                net = new NetworkLayer(Client);
                Thread main = new Thread(net.ClientHandler);
                main.Start();
                //MessageBox.Show("***A new thread has been created***");/////////////////
            }
            
        }
        #endregion

        #region At load of the Operator Form procedures
        private void FormOperator_Load(object sender, EventArgs e)
        {
            FormOperator o = new FormOperator();
            Thread start = new Thread(o.ClientDealer);
            start.Start();
        }
        #endregion

        #region Add Teller Button
        private void button1_Click(object sender, EventArgs e) //this button is to ADD Teller.
        {//error typing and user errors are checked here.
            try
            {
                string name = txtTellerName.Text;
                string uName = txtTellerUserName.Text;
                string pass = txtTellerPass.Text;
                if (name.IndexOf(',') == -1 && uName.IndexOf(',') == -1 && pass.IndexOf(',') == -1 && name != "" && uName != "" && pass != "" && name != " " && uName != " " && pass != " ")
                {//checked if one of the textBox is empty,or contains a comma.
                    string netString = txtTellerName.Text + "," + txtTellerUserName.Text + "," + txtTellerPass.Text;//building netString variable which is to be passed for the function of the
                                                                                                                    //operator class "AddTeller()"
                    string[] result = new string[1];
                    result = Operator.AddTeller(netString);
                    if (result[0] == "0")
                        MessageBox.Show("Add Teller Operation Done");
                    else if (result[0] == "1")
                        MessageBox.Show("Add Teller Operation:Error....The same User Name already Exsists....\n \t Please choose another one ");
                    else if (result[0] == "2" || result[0]==null)
                        MessageBox.Show("Add Teller Operation:Error....An I/O problem accured");
                    txtTellerName.Clear();
                    txtTellerPass.Clear();
                    txtTellerUserName.Clear();
                }
                else
                {
                    MessageBox.Show("Add Teller Operation:Error....Name, User Name, and Password must not contain a comma ',' and not Empty ");
                    txtTellerName.Clear();
                    txtTellerPass.Clear();
                    txtTellerUserName.Clear();
                }
            }
            catch
            {
                MessageBox.Show("An Error accured with Add Teller Operation");
            }
        }
        #endregion

        #region Delete Button
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string id = txtID.Text;
                if (id != "" && id != " " && id.IndexOf(',') == -1)
                {//After checking whatever errors may accur because of the user...
                    string[] result = new string[1];
                    result = Operator.DeleteTeller(id);
                    if (result[0] == "0")
                        MessageBox.Show("Delete Teller Operation Done");
                    else if (result[0] == "1")
                        MessageBox.Show("Delete Teller Operation:The Teller's ID doesn't Exsists");
                    else if (result[0] == "2" || result[0] == null)
                        MessageBox.Show("Delete Teller Operation: An I/O problem accured");
                    txtID.Clear();
                }
            }
            catch
            {
                MessageBox.Show("An Error accured");
                txtID.Clear();
            }

        }
        #endregion


        #region Modify Button
        private void btnModify_Click(object sender, EventArgs e)
        {
            try
            {
                string id = txtModID.Text;
                string name = txtModName.Text;
                string uName = txtModUName.Text;
                string pass = txtModPass.Text;
                if (id.IndexOf(',') == -1 && name.IndexOf(',') == -1 && uName.IndexOf(',') == -1 && pass.IndexOf(',') == -1 &&
                    id != "" && id != " " && name != "" && name != " " && uName != "" && uName != " " && pass != "" && pass != " ")
                {//after checking type errors and user errors:
                    string netString = id + "," + name + "," + uName + "," + pass;//building netString variable which is to be passed for the function of the
                                                                                  //Operator class "ModifyTeller()".
                    string[] result = new string[1];
                    result = Operator.ModifyTeller(netString);
                    if (result[0] == "0")
                        MessageBox.Show("Modify Teller Operation Done");
                    else if (result[0] == "1")
                        MessageBox.Show("Modify Teller Operation:Error...ID doesn't Exsist ");
                    else if (result[0] == "2" || result[0] == null)
                        MessageBox.Show("Modify Teller Operation:Error...An I/O problem accured");
                    txtModID.Clear();//starting Clearing the content of the text boxes after showing the result for the user.
                    txtModName.Clear();
                    txtModPass.Clear();
                    txtModUName.Clear();

                }
                else
                {
                    MessageBox.Show("Modify Teller Operation: Error...please note that no textbox could contain a comma ',' and couldn't be Empty");
                }
            }
            catch
            {
                MessageBox.Show("Modify Teller:an error accured...");
            }
        }
        #endregion

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        #region View Tellers Button
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView.Rows.Clear();
                string[] result = Operator.ViewTellers();
                if (result == null)
                    MessageBox.Show("View Tellers: an I/O error accured");
                else
                {
                    string teller,id,Name,uName,Pass;
                    string[] splittedTeller;
                    for (int i = 0; i < (result.Length)-1; i++)
                    {
                        teller = result[i];
                        splittedTeller = teller.Split(',');
                        id = splittedTeller[0];
                        Name = splittedTeller[1];
                        uName = splittedTeller[2];
                        Pass = splittedTeller[3];
                        dataGridView.Rows.Add(id, Name, uName, Pass);
                    }
                }
                
            }
            catch
            {
                MessageBox.Show("View Tellers: An error accured...please chack the Tellers data file");
            }
        }
        #endregion
    }
}
