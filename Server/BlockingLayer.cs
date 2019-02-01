#region Code Info
/*
 * Coder : M. Adel G. Kodmani
 * Creation Date : 8/12/2011 - 01:22 AM
 * Last Modification : 8/16/2011 03:02 AM by M.Ahmad Mhd. Al-Samman
 * Build Status : Complete Build
 * Description : The class just calls the appropriate for each operation, it's created because we need a layer that would include the lock facility
 *               in order to prvent concrete access to the data access layer.
 * Testing :  Needs testing
 * Notes : 1- The code here returns the error that was returned from the DataAccess layer, so check what the that layer returns in order to 
 *             recognize the error type and display it properly to the user
 *         2- %%%ATTENTION%%% : DO NOT create more than one instance of this class, or you'll ruin the purpose of its existence
 *         3- This class returns null  if the operation that was supposed to be done is not an available operation
 *         4- %%%ATTENTION%%% : Careful that this code returns error level, but not as an integer, it actually returns it as a string array, 
 *         so remember to cast if you are expecting an int and use the return value if you are expecting a string(like in ViewAccount example)
 * -------------------------------------------------------------------------------------------------------------------------------------------
 * */
#endregion

#region Namepsaces
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion

namespace Server
{
    class BlockingLayer
    {
        public String[] FunctionCaller(String operation, String netString)
        {
            lock (this)
            {
                #region Add Teller
                if (operation == "AddTeller")
                {
                    int err = DataAccess.AddTeller(netString);
                    String[] ar = new String[1];
                    ar[0] = Convert.ToString(err);
                    return ar;
                }
                #endregion
                #region Delete Teller
                if (operation == "DeleteTeller")
                {
                    int err = DataAccess.DeleteTeller(netString);
                    String[] ar = new String[1];
                    ar[0] = Convert.ToString(err);
                    return ar;
                }
                #endregion
                #region Modify Teller
                if (operation == "ModifyTeller")
                {
                    int err = DataAccess.ModifyTeller(netString);
                    String[] ar = new String[1];
                    ar[0] = Convert.ToString(err);
                    return ar;
                }
                #endregion
                #region View Tellers
                if (operation == "ViewTellers")
                {
                    String[] ar = DataAccess.ViewTellers();
                    return ar;
                }
                #endregion

                #region Add Account
                if (operation == "AddAccount")
                {
                    int err = DataAccess.AddAccount(netString);
                    String[] ar = new String[1];
                    ar[0] = Convert.ToString(err);
                    return ar;
                }
                #endregion
                #region Delete Account
                if (operation == "DeleteAccount")
                {
                    int err = DataAccess.DeleteAccount(netString);
                    String[] ar = new String[1];
                    ar[0] = Convert.ToString(err);
                    return ar;
                }
                #endregion
                #region Withdraw Operation
                if (operation == "Withdarw")
                {
                    String[] splitArray = new String[3];
                    splitArray = netString.Split(',');
                    int err = DataAccess.Withdraw(splitArray[0], splitArray[1], splitArray[2]);
                    String[] ar = new String[1];
                    ar[0] = Convert.ToString(err);
                    return ar;
                }
                #endregion
                #region Deposit Operation
                if (operation == "Deposit")
                {
                    String[] splitArray = new String[3];
                    splitArray = netString.Split(',');
                    int err = DataAccess.Deposit(splitArray[0], splitArray[1], splitArray[2]);
                    String[] ar = new String[1];
                    ar[0] = Convert.ToString(err);
                    return ar;
                }
                #endregion
                #region Transfer Operation
                if (operation == "Transfer")
                {
                    String[] splitArray = new String[4];
                    splitArray = netString.Split(',');
                    int err = DataAccess.Transfer(splitArray[0], splitArray[1], splitArray[2], splitArray[3]);
                    String[] ar = new String[1];
                    ar[0] = Convert.ToString(err);
                    return ar;
                }
                #endregion

                #region View Account
                if (operation == "ViewAccount")
                {
                    String err = DataAccess.ViewAccount(netString);
                    String[] ar = new String[1];
                    ar[0] = (err);
                    return ar;
                }
                #endregion 
                #region View Last 10 Transactions 
                if (operation == "ViewLastTenTransactions")//this is Modified as the function in Data Access layer has been modified
                                                           //to return string not array of strings.
                {
                    String err = DataAccess.ViewLastTenTransactions(netString);
                    string[] send = new string[1];
                    send[0] = err;
                    return send;
                }
                #endregion

                #region Teller Log In Check
                if (operation == "LogIn")
                {
                    String[] splitArray = new String[2];
                    splitArray = netString.Split(',');
                    int err = DataAccess.TellerCheckLogIn(splitArray[0], splitArray[1]);
                    String[] ar = new String[1];
                    ar[0] = Convert.ToString(err);
                    return ar;
                }
                #endregion
            }
            return null;//returning null if something went terribly wrong
        }
    }
}
