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
using System.Net;
using System.Net.Sockets;
#endregion

namespace Client
{
    public partial class FormClient : Form
    {
        
        //Defining a variable of type FormClient to use it in showing and hiding.
        public static FormClient formC = new FormClient();

        public FormClient()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


        #region Add Account Button
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string Operation = "AddAccount";
                string netString, send;
                string eName, aName, balance, address, phone, mobile;                
                eName = txtEName.Text;
                aName = txtAName.Text;
                balance = txtBalance.Text;
                address = txtAddress.Text;
                phone = txtPhone.Text;
                mobile = txtMobile.Text;
                if (eName.IndexOf(',') == -1 && aName.IndexOf(',') == -1 && balance.IndexOf(',') == -1 && address.IndexOf(',') == -1 && phone.IndexOf(',') == -1 && mobile.IndexOf(',') == -1
                    && eName != "" && eName != " " && aName != "" && aName != " " && balance != "" && balance != " " && address != "" && address != " " && phone != "" && phone != " " && mobile != "" && mobile != " ")
                {//After checking that no input is empty,spaced or has a comma ','...
                    TcpClient Client = new TcpClient("127.0.0.1", 9000);
                    NetworkStream Stream = Client.GetStream();
                    StreamReader reader = new StreamReader(Stream);
                    StreamWriter writer = new StreamWriter(Stream);
                    netString = eName + "," + aName + "," + balance + "," + address + "," + phone + "," + mobile;
                    send = Operation + ";" + netString;
                    writer.WriteLine(send);
                    writer.Flush();
                    string result = reader.ReadLine();
                    if (result == "0")
                        MessageBox.Show("Add Account Done");
                    else if (result == "2")
                        MessageBox.Show("Add Account: Error...an I/O problem has accured");
                    else if (result == "3")
                        MessageBox.Show("Add Account: Error...Too large Information");
                    txtAccountID.Clear();
                    txtAddress.Clear();
                    txtAName.Clear();
                    txtBalance.Clear();
                    txtEName.Clear();
                    txtMobile.Clear();
                    txtPhone.Clear();
                    txtViewID.Clear();
                    Stream.Close();
                    Client.Close();
                }
                else MessageBox.Show("Add Account...Error,please make sure that all of the feilds are filled and without commas ','");
                txtAccountID.Clear();
                txtAddress.Clear();
                txtAName.Clear();
                txtBalance.Clear();
                txtEName.Clear();
                txtMobile.Clear();
                txtPhone.Clear();
                txtViewID.Clear();
            }
            catch
            {
                MessageBox.Show("Add Account...Error !!!");
            }
        #endregion

        }

        #region View Account Button
        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {   
                string id = txtViewID.Text;
                if (id != "" && id != " " && id.IndexOf(',') == -1)
                {
                    string Operation = "ViewAccount";
                    string send = Operation + ";" + id;//this variable contains the string that'll get sent to the server.
                    string result;//contains the result recieved from the server.
                    TcpClient Client = new TcpClient("127.0.0.1", 9000);
                    NetworkStream Stream = Client.GetStream();
                    StreamReader strr = new StreamReader(Stream);
                    StreamWriter strw = new StreamWriter(Stream);
                    strw.WriteLine(send);
                    strw.Flush();
                    result = strr.ReadLine();
                    if (result == null)
                    {
                        MessageBox.Show("View Account: Error...ID doesn't exsist, please check again");
                        Stream.Close();
                        Client.Close();
                    }
                    else
                    {
                        string[] splittedResult = result.Split(',');
                        txtAccountID.Text = splittedResult[0];
                        txtEName.Text = splittedResult[1];
                        txtAName.Text = splittedResult[2];
                        txtBalance.Text = splittedResult[3];
                        txtAddress.Text = splittedResult[4];
                        txtPhone.Text = splittedResult[5];
                        txtMobile.Text = splittedResult[6];
                        Stream.Close();
                        Client.Close();
                    }
                }
                else MessageBox.Show("View Account: Error...please make sure that text box is filled and without commas ','");
            }
            catch
            {
 
            }
        }
        #endregion

        private void FormClient_Load(object sender, EventArgs e)
        {
            FormLogIn.FormLog.Hide();
            FormLogIn.FormLog.Close();
        }

        #region Delete Account Button
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string accountID = txtDeleteAccount.Text;
                if (accountID.IndexOf(',') == -1 && accountID != "" && accountID != " ")
                {
                    string Operation = "DeleteAccount";
                    string send = Operation + ";" + accountID;
                    TcpClient Client = new TcpClient("127.0.0.1", 9000);
                    NetworkStream Stream = Client.GetStream();
                    StreamReader reader = new StreamReader(Stream);
                    StreamWriter writer = new StreamWriter(Stream);
                    writer.WriteLine(send);
                    writer.Flush();
                    string result = reader.ReadLine();
                    if (result == "0")
                        MessageBox.Show("Account Deleted");
                    else if (result == "1")
                        MessageBox.Show("Delete Account: Error....Account Doesn't exsist");
                    else MessageBox.Show("Delete Account: Error in I/O");
                    Stream.Close();
                    Client.Close();
                }
                else MessageBox.Show("Delete Account: Error...please check that text feild is filled and with no commas ','");
            }
            catch
            {
                MessageBox.Show("Error...please contact IT technician");
            }
        }
        #endregion

        #region Withdraw Button
        private void btnWith_Click(object sender, EventArgs e)
        {
            try
            {
                string AccountID = txtAccountWith.Text;
                string tellerID = txtTellerWith.Text;
                string Amount = txtAmountWith.Text;
                if (AccountID.IndexOf(',') == -1 && tellerID.IndexOf(',') == -1 && Amount.IndexOf(',') == -1 && AccountID != "" && AccountID != " "
                    && tellerID != "" && tellerID != " " && Amount != "" && Amount != " ")
                {//after checking varous typing errors...
                    TcpClient Client = new TcpClient("127.0.0.1", 9000);
                    NetworkStream Stream = Client.GetStream();
                    StreamReader reader = new StreamReader(Stream);
                    StreamWriter writer = new StreamWriter(Stream);
                    string Operation = "Withdarw";
                    string netString = AccountID + "," + tellerID + "," + Amount;
                    string send = Operation + ";" + netString;
                    writer.WriteLine(send);
                    writer.Flush();
                    string result = reader.ReadLine();
                    if (result == "0")
                        MessageBox.Show("Withdraw from account done");
                    else if (result == "1")
                        MessageBox.Show("Withdraw:Error...Account's ID Doesn't exist");
                    else if (result == "2")
                        MessageBox.Show("Withdraw:Error...Teller's ID Doesn't exist");
                    else if (result == "3")
                        MessageBox.Show("Withdraw: Error...Amount is not available...Balance<Amount");
                    else if (result == "4")
                        MessageBox.Show("Withdraw: Error...I/O error has accured");
                    Stream.Close();
                    Client.Close();
                    txtAccountWith.Clear();
                    txtAmountWith.Clear();
                    txtTellerWith.Clear();
                }
                else MessageBox.Show("Deposit: Error...Please check that all feilds are filled with no commas ','");
                txtAccountWith.Clear();
                txtAmountWith.Clear();
                txtTellerWith.Clear();
            }
            catch
            {
                MessageBox.Show("withdraw Operation Faild....call the IT technician");
            }
        }
        #endregion

        #region Deposit Button
        private void btnDepo_Click(object sender, EventArgs e)
        {
            try
            {
                string AccountID = txtAccountWith.Text;
                string tellerID = txtTellerWith.Text;
                string Amount = txtAmountWith.Text;
                if (AccountID.IndexOf(',') == -1 && tellerID.IndexOf(',') == -1 && Amount.IndexOf(',') == -1 && AccountID != "" && AccountID != " "
                    && tellerID != "" && tellerID != " " && Amount != "" && Amount != " ")
                {//after checking varous typing errors...
                    TcpClient Client = new TcpClient("127.0.0.1", 9000);
                    NetworkStream Stream = Client.GetStream();
                    StreamReader reader = new StreamReader(Stream);
                    StreamWriter writer = new StreamWriter(Stream);
                    writer.Flush();
                    string Operation = "Deposit";
                    string netString = AccountID + "," + tellerID + "," + Amount;
                    string send = Operation + ";" + netString;
                    writer.WriteLine(send);
                    string result = reader.ReadLine();
                    if (result == "0")
                        MessageBox.Show("Deposit done");
                    else if (result == "1")
                        MessageBox.Show("Deposit:Error...Account's ID Doesn't exist");
                    else if (result == "2")
                        MessageBox.Show("Deposit:Error...Teller's ID Doesn't exist");
                    else if (result == "3")
                        MessageBox.Show("Deposit: Error...I/O error has accured");
                    Stream.Close();
                    Client.Close();
                    txtAccountWith.Clear();
                    txtAmountWith.Clear();
                    txtTellerWith.Clear();
                }
                else MessageBox.Show("Deposit: Error...Please check that all feilds are filled with no commas ','");
                txtAccountWith.Clear();
                txtAmountWith.Clear();
                txtTellerWith.Clear();
            }
            catch
            {
                MessageBox.Show("Deposit Operation Faild....Please call the IT technician");
            }
        }
        #endregion

        #region Transfer Button
        private void btnTransfer_Click(object sender, EventArgs e)
        {
            try
            {
                string source = txtS.Text;
                string destination = txtD.Text;
                string amount = txtA.Text;
                string teller = txtT.Text;
                if (source.IndexOf(',') == -1 && destination.IndexOf(',') == -1 && amount.IndexOf(',') == -1 && teller.IndexOf(',') == -1
                    && source != "" && source != " " && destination != "" && destination != " " && amount != "" && amount != " "
                    && teller != "" && teller != " ")
                {//after checking various typing errors...
                    TcpClient Client = new TcpClient("127.0.0.1", 9000);
                    NetworkStream Stream = Client.GetStream();
                    StreamReader reader = new StreamReader(Stream);
                    StreamWriter writer = new StreamWriter(Stream);
                    string Operation = "Transfer";
                    string netString = source + "," + destination + "," + amount + "," + teller;
                    string send = Operation + ";" + netString;
                    writer.WriteLine(send);
                    writer.Flush();
                    string result = reader.ReadLine();
                    if (result == "0")
                        MessageBox.Show("Transfer Done");
                    else if (result == "1")
                        MessageBox.Show("Transfer: Error...check destination ID \n check Source ID");
                    else if (result == "3")
                        MessageBox.Show("Transfer: Error...Amount is not available in Source's Account");
                    Stream.Close();
                    Client.Close();
                    txtA.Clear();
                    txtS.Clear();
                    txtD.Clear();
                    txtT.Clear();
                }
                else MessageBox.Show("Transfer: Error...please make sure that all text feilds are filled and with no commas ','");
                txtA.Clear();
                txtS.Clear();
                txtD.Clear();
                txtT.Clear();
            }
            catch
            {
                MessageBox.Show("Transfer can't be done...please call IT technician");
            }
        }
        #endregion

        private void btnViewAccount_Click(object sender, EventArgs e)
        {
            try
            {
                string AccountID = txtViewTransactions.Text;
                if (AccountID.IndexOf(',') == -1 && AccountID != "" && AccountID != " ")
                {
                    TcpClient Client = new TcpClient("127.0.0.1", 9000);
                    NetworkStream Stream = Client.GetStream();
                    StreamReader reader = new StreamReader(Stream);
                    StreamWriter writer = new StreamWriter(Stream);
                    string Operation = "ViewLastTenTransactions";
                    string send = Operation + ";" + AccountID;
                    writer.WriteLine(send);
                    writer.Flush();
                    string result = reader.ReadLine();
                    if (result == null)
                        MessageBox.Show("Operation failed...Or this account has no Transactions yet");
                    else
                    {
                        string[] splitresult = result.Split(';');
                        string show="";
                        for (int i = 0; i < splitresult.Length; i++)                        
                            show = show + splitresult[i] + "\n";
                        MessageBox.Show("The last ten Transactions:\n {0]",show);
                    }
                    Stream.Close();
                    Client.Close();
                }
                else MessageBox.Show("Please make sure that text feild is filled with no commas ','");
            }
            catch
            {
                MessageBox.Show("Operation Failed...please contact the IT technician");
            }
        }







        #region FAILED Buttons
        /* private void btnWithdraw_Click(object sender, EventArgs e)
        {
            try
            {
                string AccountID = txtAccountWithdraw.Text;
                string tellerID = txtTellerwithdraw.Text;
                string Amount = txtAmountWithdraw.Text;
                if (AccountID.IndexOf(',') == -1 && tellerID.IndexOf(',') == -1 && Amount.IndexOf(',') == -1 && AccountID != "" && AccountID != " "
                    && tellerID != "" && tellerID != " " && Amount != "" && Amount != " ")
                {//after checking varous typing errors...
                    TcpClient Client = new TcpClient("127.0.0.1", 9000);
                    NetworkStream Stream = Client.GetStream();
                    StreamReader reader = new StreamReader(Stream);
                    StreamWriter writer = new StreamWriter(Stream);
                    string Operation = "Withdarw";
                    string netString = AccountID + "," + tellerID + "," + Amount;
                    string send = Operation + ";" + netString;
                    writer.WriteLine(send);
                    string result = reader.ReadLine();
                    if (result == "0")
                        MessageBox.Show("Withdraw from account done");
                    else if (result == "1")
                        MessageBox.Show("Withdraw:Error...Account's ID Doesn't exist");
                    else if (result == "2")
                        MessageBox.Show("Withdraw:Error...Teller's ID Doesn't exist");
                    else if (result == "3")
                        MessageBox.Show("Withdraw: Error...Amount is not available...Balance<Amount");
                    else if (result == "4")
                        MessageBox.Show("Withdraw: Error...I/O error has accured");
                    Stream.Close();
                    Client.Close();
                }
            }
            catch
            {
                MessageBox.Show("withdraw Operation Faild....call the IT technician");
            }
        }
        #endregion

        #region Deposit Button
        private void btnDeposit_Click(object sender, EventArgs e)
        {
            try
            {
                string AccountID = txtAccountWithdraw.Text;
                string tellerID = txtTellerwithdraw.Text;
                string Amount = txtAmountWithdraw.Text;
                if (AccountID.IndexOf(',') == -1 && tellerID.IndexOf(',') == -1 && Amount.IndexOf(',') == -1 && AccountID != "" && AccountID != " "
                    && tellerID != "" && tellerID != " " && Amount != "" && Amount != " ")
                {//after checking varous typing errors...
                    TcpClient Client = new TcpClient("127.0.0.1", 9000);
                    NetworkStream Stream = Client.GetStream();
                    StreamReader reader = new StreamReader(Stream);
                    StreamWriter writer = new StreamWriter(Stream);
                    string Operation = "Deposit";
                    string netString = AccountID + "," + tellerID + "," + Amount;
                    string send = Operation + ";" + netString;
                    writer.WriteLine(send);
                    string result = reader.ReadLine();
                    if (result == "0")
                        MessageBox.Show("Deposit done");
                    else if (result == "1")
                        MessageBox.Show("Deposit:Error...Account's ID Doesn't exist");
                    else if (result == "2")
                        MessageBox.Show("Deposit:Error...Teller's ID Doesn't exist");                    
                    else if (result == "3")
                        MessageBox.Show("Deposit: Error...I/O error has accured");
                    Stream.Close();
                    Client.Close();
                }
            }
            catch
            {
                MessageBox.Show("Deposit Operation Faild....Please call the IT technician");
            }
        }
        #endregion

        #region Transfer Button
        private void btnTrans_Click(object sender, EventArgs e)
        {
            try
            {
                string source = txtSource.Text;
                string destination = txtDestination.Text;
                string amount = txtAmountTrans.Text;
                string teller = txtTellerTrans.Text;
                if (source.IndexOf(',') == -1 && destination.IndexOf(',') == -1 && amount.IndexOf(',') == -1 && teller.IndexOf(',') == -1
                    && source != "" && source != " " && destination != "" && destination != " " && amount != "" && amount != " "
                    && teller != "" && teller != " ")
                {//after checking various typing errors...
                    TcpClient Client = new TcpClient("127.0.0.1", 9000);
                    NetworkStream Stream = Client.GetStream();
                    StreamReader reader = new StreamReader(Stream);
                    StreamWriter writer = new StreamWriter(Stream);
                    string Operation = "Transfer";
                    string netString = source + "," + destination + "," + amount + "," + teller;
                    string send = Operation + ";" + netString;
                    writer.WriteLine(send);
                    writer.Flush();
                    string result = reader.ReadLine();
                    if (result == "0")
                        MessageBox.Show("Transfer Done");
                    else if (result == "1")
                        MessageBox.Show("Transfer: Error...check destination ID \n check Source ID");
                    else if (result == "3")
                        MessageBox.Show("Transfer: Error...Amount is not available in Source's Account");
                    Stream.Close();
                    Client.Close();
                }
                else MessageBox.Show("Transfer: Error...please make sure that all text feilds are filled and with no commas ','");
            }
            catch
            {
                MessageBox.Show("Transfer can't be done...please call IT technician");
            }
        }*/
        #endregion
        


    }
}
