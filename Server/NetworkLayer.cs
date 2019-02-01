#region Code Info
/*
 * Coders : M. Adel G. Kodmani - M.Ahmad Mhd. Al-Samman
 * Creation Date : 8/12/2011 - 02:01 AM
 * Last Modification : 8/16/2011 06:41 AM by Ahmad Mhd. Al-Samman
 * Build Status : Complete build
 * Description : This class basically listens to clients requests, and whenever a request happens
 *               it creates a thread for it.
 * Testing :  Passed function testing
 * Notes : 1- The code here returns the error that was returned from the DataAccess layer, so check what the that layer returns in order to 
 *             recognize the error type and display it properly to the user
 *         2- %%%ATTENTION%%% : DO NOT create more than one instance of this class, or you'll ruin the purpose of its existence
 *         3- This class returns null  if the operation that was supposed to be done is not an available operation
 *         4- %%%ATTENTION%%% : Careful that this code returns error level, but not as an integer, it actually returns it as a string array, 
 *         so remember to cast if you are expecting an int and use the return value if you are expecting a string(like in ViewAccount example)
 * -------------------------------------------------------------------------------------------------------------------------------------------
 * */
#endregion

#region Namespaces
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Net.Sockets;
using System.IO;
#endregion

namespace Server
{
    public class NetworkLayer
    {
        public TcpClient Client;
        BlockingLayer call;
        #region Constructor
        public NetworkLayer(TcpClient c)//Class Constructor, created to pass parameters to Threads.
        {
            Client = c;            
        }
        #endregion

        #region Client Handler Function
        public void ClientHandler()//this function will be passed to the thread and handle the client request.
        {
            NetworkStream Stream;
            //FormOperator.sms("Test");
            Stream = Client.GetStream();
            //FormOperator.sms("Test2");
            StreamReader reader = new StreamReader(Stream);
            StreamWriter writer = new StreamWriter(Stream);
            string recieved = reader.ReadLine();
            FormOperator.sms(recieved);///////////////////////////
            string[] splitted = new string[2];
            splitted = recieved.Split(';');
            string Operation = splitted[0];
            string netString = splitted[1];
            call = new BlockingLayer();
            string[] result = call.FunctionCaller(Operation, netString);
            //FormOperator.sms(result[0]);//////////////////////////////
            writer.WriteLine(result[0]);//sending the result to the client as a string, because streamReader and writer
                                        //only sends and recieves Strings.
            writer.Flush();
            Stream.Close();
            Client.Close();
            Thread.CurrentThread.Abort();
        }
        #endregion
    }
}
