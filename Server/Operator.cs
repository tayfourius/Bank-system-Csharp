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
using System.Linq;
using System.Text;
using System.Threading;
#endregion

namespace Server
{
    class Operator
    {
        #region AddTeller
        public static string[] AddTeller(string netString)
        {
            string operation = "AddTeller";
            BlockingLayer b = new BlockingLayer();
            string[] result = new string[1];//this array take the returning value of the function FunctionCaller which is a Function of BlockingLayer class. 
            result = b.FunctionCaller(operation, netString);
            return result;
        }
        #endregion

        #region DeleteTeller
        public static string[] DeleteTeller(string netString)
        {
            string operation = "DeleteTeller";
            BlockingLayer b = new BlockingLayer();
            string[] result = new string[1];
            result = b.FunctionCaller(operation, netString);
            return result;
        }
        #endregion

        #region ModifyTeller
        public static string[] ModifyTeller(string netString)
        {
            string operation = "ModifyTeller";
            BlockingLayer b = new BlockingLayer();
            string[] result = new string[1];
            result = b.FunctionCaller(operation, netString);
            return result;
        }
        #endregion

        #region ViewTellers
        public static string[] ViewTellers()//this method doesn't need any parameters.
        {
            string operation = "ViewTellers";
            string netString="ViewTellers";
            BlockingLayer b = new BlockingLayer();
            string[] result = new string[1];
            result = b.FunctionCaller(operation,netString);
            return result;
        }
        #endregion
    }
}
