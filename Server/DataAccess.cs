#region Code Info
/*
 * Coder : M. Adel C. Kodmani
 * Creation Date : 8/6/2011 - 00:07 AM
 * Last Modification : 8/16/2011 03:58 AM By M.Ahmad Mhd. Al-Samman
 * Build Status : Complete Build
 * Description : This class is responsible for retrieving, modifying, creating, and deleting data in the file database, on requests from the Net/Sync layer
 * Testing :  The code here has passed functional testing, we're currently trying to find the fastest and least expensive(memory wise) way
 *               to access the files.
 * Notes : 1- %%% ATTENTION %%% this code here doesn't, and for design reasons, won't prevent threads concrete access, any such attempt might cause a fatal crash.
 *         2- %%% ATTENTION %%% : Encoding used here is strictly ASCII unless we are dealing with the accounts file, in which case
 *                                and in that case alone, we use UTF8, DO NOT confuse this with standard Unicode (also known as UTF16), that might cause us trouble.
 * -------------------------------------------------------------------------------------------------------------------------------------------
 * Files Stucture:
 * Tellers Account(Each record looks like this)
 * TellerID, Name, UserName, Password
 * 
 * StatiCounters(strictly speaking, no additional lines) :
 *                 TellerID,StaticCounter
 *                 AccountsID,StaticCounter
 *                 TransactionsID,StaticCounter
 *                 
 * Accounts:(each record looks like this, with total max size of 449 bytes)
 * AccountID,Name(English),Name(Arabic),Balance,Adress,Phone,Mobile ###CAREFUL!!!! balance is in the 3rd position even though
 *                                                                     * it will appear like it is in the 2nd position in the file, due to the
 *                                                                     *  arrangment of unicode strings###
 * 
 * AccountsIndex(each record looks like this; StartByte stands for the very byte before the record of the id AccountI)
 * AccountID,StartByte
 * 
 * 
*/
#endregion

#region Included Namespaces
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security;
#endregion

namespace Server
{

    public static class DataAccess
    {
        public static int AddTeller(String netString)
        {//netString holds the STRING that consists of TellerName,UserName,Password
            String tempLine;
            String[] netStringSplitArray = netString.Split(',');
            String[] tempSplitArray;
            /* We first check if the same username exsists, at which case a fail happens */
            try
            {
                StreamReader streamRe = new StreamReader(".\\Files\\Tellers.bnk", ASCIIEncoding.ASCII);
                while (!streamRe.EndOfStream)
                {
                    tempLine = streamRe.ReadLine();
                    tempSplitArray = tempLine.Split(',');
                    if (tempSplitArray[2] == netStringSplitArray[1])//If the ID exsists 
                        return 1;
                }
                streamRe.Close();
                /* ^^^^^^ Checking ends, the username is not already in the file ^^^^^ */
                streamRe = new StreamReader(".\\Files\\StaticCounters.bnk", ASCIIEncoding.ASCII);
                tempLine = streamRe.ReadLine();
                String[] rebuiltLines = new String[2];
                rebuiltLines[0] = streamRe.ReadLine();//Contains the AccountsID counter  >>
                rebuiltLines[1] = streamRe.ReadLine();//Contains the TransactionID counter  >>
                tempSplitArray = tempLine.Split(',');/*Reading the StaticID value from the staticCounters, 
                //increment it by one, so that we create the new teller with the proper
                //ID, and saving the new ID static counter into the file for later use*/
                int newID = Convert.ToInt32(tempSplitArray[1]) + 1;
                streamRe.Close();
                StreamWriter streamWr = new StreamWriter(".\\Files\\StaticCounters.bnk", false, ASCIIEncoding.ASCII);
                streamWr.WriteLine("TellerID," + Convert.ToString(newID));
                streamWr.WriteLine(rebuiltLines[0]);
                streamWr.WriteLine(rebuiltLines[1]);
                streamWr.Close();
                /* Now we are ready to add the new teller */
                streamWr = new StreamWriter(".\\Files\\Tellers.bnk", true, ASCIIEncoding.ASCII);
                streamWr.WriteLine(Convert.ToString(newID) + "," + netString);
                streamWr.Close();
            }
            catch
            {
                return 2;//If an IO problem happens
            }
            GC.Collect();//Trying to force GC, an attempt to minimize memoy usage >>> To be reviewed if this call adds overhead <<<
            return 0;//At success
        }

        public static int DeleteTeller(String tellerId)
        {//We first check if the ID is there, then we delete it
            try
            {
                StreamReader streamRe = new StreamReader(".\\Files\\Tellers.bnk", ASCIIEncoding.ASCII);
                String tempLine;
                String ourNewFile = "";
                String[] tempSplitArray = new String[4];
                bool found = false;
                while (!streamRe.EndOfStream)
                {//Checking, if the ID we need is found
                    tempLine = streamRe.ReadLine();
                    tempSplitArray = tempLine.Split(',');
                    if (tempSplitArray[0] == tellerId)
                        found = true;
                }
                if (found == false)
                {
                    streamRe.Close();
                    return 1;//Return 1 if the the ID to be deleted is not in the file
                }
                streamRe.Close();
                streamRe = new StreamReader(".\\Files\\Tellers.bnk", ASCIIEncoding.ASCII);
                while (!streamRe.EndOfStream)
                {//Here, we will save the whole file in the string ourNewFile, but we will not include the record which we want to delete
                    //Then, we will replace the old file with the string ourNewFile
                    tempLine = streamRe.ReadLine();
                    tempSplitArray = tempLine.Split(',');
                    if (tempSplitArray[0] != tellerId)
                    {
                        ourNewFile = ourNewFile + tempLine + "\n";//Adding \n to put each record on a seperate line
                    }
                }
                streamRe.Close();
                StreamWriter streamWr = new StreamWriter(".\\Files\\Tellers.bnk", false, ASCIIEncoding.ASCII);
                streamWr.Write(ourNewFile);
                streamWr.Close();
            }
            catch
            {
                return 2;//Return 2 if a problem in the I/O happened
            }
            GC.Collect();
            return 0;
        }

        public static int ModifyTeller(String netString)
        {//We first check if the ID is there, then we delete it
            try
            {
                StreamReader streamRe = new StreamReader(".\\Files\\Tellers.bnk", ASCIIEncoding.ASCII);
                String tempLine;
                String ourModifiedFile = "";
                String[] tempSplitArray = new String[4];
                bool found = false;
                String[] splitNetString = netString.Split(',');
                while (!streamRe.EndOfStream)
                {//Checking, if the ID we need is found
                    tempLine = streamRe.ReadLine();
                    tempSplitArray = tempLine.Split(',');
                    if (tempSplitArray[0] == splitNetString[0])
                        found = true;
                }
                if (found == false)
                {
                    streamRe.Close();
                    return 1;//This means that the ID we are trying to modify doesn't exist
                }
                streamRe.Close();
                streamRe = new StreamReader(".\\Files\\Tellers.bnk", ASCIIEncoding.ASCII);
                while (!streamRe.EndOfStream)
                {//Now, we will read each record and store them all in the string ourModifiedFile
                    tempLine = streamRe.ReadLine();
                    tempSplitArray = tempLine.Split(',');
                    if (tempSplitArray[0] == splitNetString[0])
                    {
                        tempLine = netString;//
                    }
                    ourModifiedFile = ourModifiedFile + tempLine + "\n";
                }
                streamRe.Close();
                StreamWriter streamWr = new StreamWriter(".\\Files\\Tellers.bnk", false, ASCIIEncoding.ASCII);
                streamWr.Write(ourModifiedFile);
                streamWr.Close();
                if (found == false)
                    return 1;//Return 1 if the the ID to be deleted is not in the filereturn 0;
            }
            catch
            {
                return 2;//If a failure in I/O happens
            }
            GC.Collect();
            return 0;
        }

        public static String[] ViewTellers()
        {//Will read everything from the tellers file into one string, split that string by '\n'
            //and return the array of the splitted string, returning null at failure
            try
            {
                StreamReader streamRe = new StreamReader(".\\Files\\Tellers.bnk", ASCIIEncoding.ASCII);
                String tempText = streamRe.ReadToEnd();
                String[] tempSplitArray = tempText.Split('\n');
                streamRe.Close();
                return tempSplitArray;
            }
            catch
            {
                return null;
            }
        }

        public static int AddAccount(String netString)
        {//We, to make indexing easier, will add (n) extra spaces after each netString where n is 449-netString.Length
            //Then, we by checking the StaticCounters file determine a new ID for the new Account, eventually we edit the StaticCounter
            //and work using the agreed-on indexing algorithm to arrange the indexing file accordingly.
            try
            {
                #region Getting, setting and saving the account ID
                String[] staticCounterFile = new String[3];
                StreamReader streamRe = new StreamReader(".\\Files\\StaticCounters.bnk", ASCIIEncoding.ASCII);
                staticCounterFile[0] = streamRe.ReadLine();//The first line of the StaticCounters file
                staticCounterFile[1] = streamRe.ReadLine();//The second line
                staticCounterFile[2] = streamRe.ReadLine();//The third
                String[] splitArray = staticCounterFile[1].Split(',');//Splitting the first line in two parts, the second part is the actual counter
                int accountsStaticCount = Convert.ToInt32(splitArray[1]);
                accountsStaticCount++;//Incrementing the counter
                splitArray[1] = Convert.ToString(accountsStaticCount);
                staticCounterFile[1] = "AccountsID" + "," + splitArray[1];//Saving the new, incremented, counter back to the first line
                streamRe.Close();
                StreamWriter streamWr = new StreamWriter(".\\Files\\StaticCounters.bnk", false, ASCIIEncoding.ASCII);
                streamWr.WriteLine(staticCounterFile[0]);
                streamWr.WriteLine(staticCounterFile[1]);
                streamWr.WriteLine(staticCounterFile[2]);
                streamWr.Close();
                #endregion

                netString = Convert.ToString(accountsStaticCount) + "," + netString;//netString now has a valid ID
                FileStream fStream = new FileStream(".\\Files\\Accounts.bnk", FileMode.Open, FileAccess.Read);
                long fileSize = fStream.Length;//The size of the file in Bytes before we add our new field, this will be used later to index it by jumping to the specified byte
                fStream.Close();
                streamWr = new StreamWriter(".\\Files\\AccountsIndex.bnk", true, ASCIIEncoding.ASCII);
                streamWr.WriteLine(Convert.ToString(accountsStaticCount) + "," + (fileSize));//Saving the index, which is the ID of the record, and how many bytes from the begining
                //of the file till we reach the record that we want
                streamWr.Close();
                splitArray = netString.Split(',');

                #region Counting how many bytes we have in netString
                int byteCount = 0;
                byteCount += splitArray[0].Length;
                byteCount += splitArray[1].Length;
                byteCount += (splitArray[2].Length) * 2;//Multiplied by two because it's Arabic, it takes two bytes for each charecter
                byteCount += splitArray[3].Length;
                byteCount += splitArray[4].Length;
                byteCount += splitArray[5].Length;
                byteCount += splitArray[6].Length;
                byteCount += 6;
                if (byteCount > 449)
                    return 3;//We do not allow storing records larger than 449 bytes in size
                string spacer = "";
                for (int i = 0; i < (449 - byteCount); i++)
                {
                    spacer = spacer + " ";
                }
                #endregion
                netString = netString + spacer;
                streamWr = new StreamWriter(".\\Files\\Accounts.bnk", true, UnicodeEncoding.UTF8);
                streamWr.WriteLine(netString);
                streamWr.Close();
            }
            catch
            {
                return 2;//If I/O exception happens, we return 2
            }
            return 0;
        }

        public static string ViewAccount(String AccountID) //**this function needs documentation correction.
        {//We first search for the ID, if it is there, we go and check the index, take the byte, use the seek method to jump to that
            //byte in the accounts file, read the record and return it as a string
            string tempLine;
            string[] tempSplitString;
            string seekedByte = "";
            bool found = false;
            try
            {
                StreamReader streamRe = new StreamReader(".\\Files\\AccountsIndex.bnk");
                while (!streamRe.EndOfStream)
                {//Search if the account record of the ID is there or not
                    tempLine = streamRe.ReadLine();
                    tempSplitString = tempLine.Split(',');
                    if (tempSplitString[0] == AccountID)
                    {
                        seekedByte = tempSplitString[1];//If we find it, we save the index from the index file
                        found = true;
                    }
                }
                if (found == false)
                    return null;
                FileStream fStream = new FileStream(".\\Files\\Accounts.bnk", FileMode.Open, FileAccess.Read);
                fStream.Seek(Convert.ToInt64(seekedByte), SeekOrigin.Begin);//Making the stream jump to the index we took from the index file
                streamRe.Close();
                streamRe = new StreamReader(fStream);
                tempLine = streamRe.ReadLine();//Reading the line we want
                tempSplitString = tempLine.Split(',');
                tempLine = tempSplitString[0] + "," + tempSplitString[1] + "," + tempSplitString[2] + "," + tempSplitString[3] + "," + tempSplitString[4] + ",";
                tempLine += tempSplitString[5] + "," + tempSplitString[6]; //Splitting and removing because only the first six segments are
                //valid data, the rest could be old or corrupted data
                streamRe.Close();
                fStream.Close();
                return tempLine.Trim();
            }
            catch
            {
                return null;
            }
        }

        public static int Withdraw(String accountID, String tellerID, String amount)
        {/*We first check if the accountID exsists(return 1 at failure), then we check if the tellerID exsists(return 2 at failure) then we check if the amount is available in 
          * the account(return 3 at failure) then we perform the Withdraw operation and save the resulting balance, we also save the transaction */
            String tempLine = "";
            String[] tempIndexSplitArray = new String[2];
            String[] tempTellersSplitArray = new String[4];
            String[] tempAccountSplitArray = new String[7];
            Boolean found = false;
            Double balance = 0.00;
            try
            {
                StreamReader streamRe = new StreamReader(".\\Files\\AccountsIndex.bnk", ASCIIEncoding.ASCII);
                #region Checking if the accountID exists
                while (!streamRe.EndOfStream)
                {
                    tempLine = streamRe.ReadLine();
                    tempIndexSplitArray = tempLine.Split(',');
                    if (tempIndexSplitArray[0] == accountID)
                    {
                        found = true;
                        break;
                    }
                }
                if (found == false)
                {
                    streamRe.Close();
                    return 1;
                }
                found = false;
                #endregion
                #region Checking if the tellerId exists
                streamRe.Close();
                streamRe = new StreamReader(".\\Files\\Tellers.bnk", ASCIIEncoding.ASCII);
                while (!streamRe.EndOfStream)
                {
                    tempLine = streamRe.ReadLine();
                    tempTellersSplitArray = tempLine.Split(',');
                    if (tempTellersSplitArray[0] == tellerID)
                    {
                        found = true;
                        break;
                    }
                }
                if (found == false)
                {
                    streamRe.Close();
                    return 2;
                }
                found = false;
                #endregion
                #region Checking if the account has enough money
                FileStream fStream = new FileStream(".\\Files\\Accounts.bnk", FileMode.Open, FileAccess.ReadWrite);
                streamRe.Close();
                streamRe = new StreamReader(fStream);
                fStream.Seek(Convert.ToInt64(tempIndexSplitArray[1]), SeekOrigin.Begin);//We know the account's position because it was given in by the index file
                tempLine = streamRe.ReadLine();
                tempAccountSplitArray = (tempLine.Trim()).Split(',');
                balance = Convert.ToDouble(tempAccountSplitArray[3]);
                if (balance < Convert.ToDouble(amount))
                {
                    streamRe.Close();
                    fStream.Close();
                    return 3;
                }
                #endregion
                #region Editing the account's balance
                fStream.Close();
                fStream = new FileStream(".\\Files\\Accounts.bnk", FileMode.Open, FileAccess.ReadWrite);
                fStream.Seek(Convert.ToInt64(tempIndexSplitArray[1]), SeekOrigin.Begin);//We know the account's position because it was given in by the index file
                streamRe.Close();
                streamRe = new StreamReader(fStream);
                tempLine = streamRe.ReadLine();
                tempAccountSplitArray = tempLine.Split(',');
                balance = Convert.ToDouble(tempAccountSplitArray[3]) - Convert.ToDouble(amount);
                StreamWriter streamWr = new StreamWriter(fStream, UnicodeEncoding.UTF8);
                fStream.Seek(Convert.ToInt64(tempIndexSplitArray[1]), SeekOrigin.Begin);//Getting the stream back to the begining of the record
                //We will rebuilt the line of the account
                tempLine = "";
                tempLine = tempAccountSplitArray[0] + "," + tempAccountSplitArray[1] + "," + tempAccountSplitArray[2] + "," + Convert.ToString(balance) + "," + tempAccountSplitArray[4] + ",";
                tempLine += tempAccountSplitArray[5] + "," + tempAccountSplitArray[6];
                tempLine = tempLine.Trim();
                String[] splitArray1 = new String[7];
                splitArray1 = tempLine.Split(',');
                int byteCount = 0;
                byteCount += splitArray1[0].Length;
                byteCount += splitArray1[1].Length;
                byteCount += (splitArray1[2].Length) * 2;//Multiplied by two because it's Arabic, it takes two bytes for each charecter
                byteCount += splitArray1[3].Length;
                byteCount += splitArray1[4].Length;
                byteCount += splitArray1[5].Length;
                byteCount += splitArray1[6].Length;
                byteCount += 6;
                string spacer = "";
                for (int i = 0; i < (449 - byteCount); i++)
                {
                    spacer = spacer + " ";
                }
                streamWr.WriteLine(tempLine + spacer);
                streamWr.Close();
                #endregion
                #region Saving the transaction in its file

                //We will get the id from StaticCounters file, then we will use it to save th new transaction
                String[] staticCounterFile = new String[3];
                streamRe.Close();
                streamRe = new StreamReader(".\\Files\\StaticCounters.bnk", ASCIIEncoding.ASCII);
                staticCounterFile[0] = streamRe.ReadLine();//The first line of the StaticCounters file
                staticCounterFile[1] = streamRe.ReadLine();//The second line
                staticCounterFile[2] = streamRe.ReadLine();//The third
                String[] splitArray = staticCounterFile[2].Split(',');//Splitting the first line in two parts, the second part is the actual counter
                int transactionsStaticCount = Convert.ToInt32(splitArray[1]);
                transactionsStaticCount++;//Incrementing the counter
                splitArray[1] = Convert.ToString(transactionsStaticCount);
                staticCounterFile[2] = "TransactionsID" + "," + splitArray[1];//Saving the new, incremented, counter back to the first line
                streamRe.Close();
                streamWr = new StreamWriter(".\\Files\\StaticCounters.bnk", false, ASCIIEncoding.ASCII);
                streamWr.WriteLine(staticCounterFile[0]);
                streamWr.WriteLine(staticCounterFile[1]);
                streamWr.WriteLine(staticCounterFile[2]);
                streamWr.Close();
                //Now we will actually build the transaction record
                tempLine = Convert.ToString(transactionsStaticCount) + "," + accountID + "," + tellerID + "," + amount + ",W,";
                tempLine += DateTime.Now.Month + "/" + DateTime.Now.Day + "/" + DateTime.Now.Year + ":";
                tempLine += DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
                streamWr = new StreamWriter(".\\Files\\Transactions.bnk", true, ASCIIEncoding.ASCII);
                streamWr.WriteLine(tempLine);
                #endregion
                streamWr.Close();
                streamRe.Close();
                fStream.Close();
                GC.Collect();
            }
            catch
            {
                return 4;//If an error in streaming happened
            }
            return 0;
        }

        public static int Deposit(String accountID, String tellerID, String amount)
        {/*We first check if the accountID exsists(return 1 at failure), then we check if the tellerID exsists(return 2 at failure) 
          * then we perform the deposit operation and save the resulting balance, we also save the transaction */
            String tempLine = "";
            String[] tempIndexSplitArray = new String[2];
            String[] tempTellersSplitArray = new String[4];
            String[] tempAccountSplitArray = new String[7];
            Boolean found = false;
            Double balance = 0.00;
            try
            {
                StreamReader streamRe = new StreamReader(".\\Files\\AccountsIndex.bnk", ASCIIEncoding.ASCII);
                #region Checking if the accountID exists
                while (!streamRe.EndOfStream)
                {
                    tempLine = streamRe.ReadLine();
                    tempIndexSplitArray = tempLine.Split(',');
                    if (tempIndexSplitArray[0] == accountID)
                    {
                        found = true;
                        break;
                    }
                }
                if (found == false)
                    return 1;
                found = false;
                #endregion
                #region Checkinf if the tellerId exists
                streamRe = new StreamReader(".\\Files\\Tellers.bnk", ASCIIEncoding.ASCII);
                while (!streamRe.EndOfStream)
                {
                    tempLine = streamRe.ReadLine();
                    tempTellersSplitArray = tempLine.Split(',');
                    if (tempTellersSplitArray[0] == tellerID)
                    {
                        found = true;
                        break;
                    }
                }
                if (found == false)
                    return 2;
                found = false;
                #endregion
                #region Editing the account's balance
                FileStream fStream = new FileStream(".\\Files\\Accounts.bnk", FileMode.Open, FileAccess.ReadWrite);
                fStream.Seek(Convert.ToInt64(tempIndexSplitArray[1]), SeekOrigin.Begin);//We know the account's position because it was given in by the index file
                streamRe = new StreamReader(fStream);
                tempLine = streamRe.ReadLine();
                tempAccountSplitArray = tempLine.Split(',');
                balance = Convert.ToDouble(tempAccountSplitArray[3]) + Convert.ToDouble(amount);
                StreamWriter streamWr = new StreamWriter(fStream, UnicodeEncoding.UTF8);
                fStream.Seek(Convert.ToInt64(tempIndexSplitArray[1]), SeekOrigin.Begin);//Getting the stream back to the begining of the record
                //We will rebuilt the line of the account
                tempLine = "";
                tempLine = tempAccountSplitArray[0] + "," + tempAccountSplitArray[1] + "," + tempAccountSplitArray[2] + "," + Convert.ToString(balance) + "," + tempAccountSplitArray[4] + ",";
                tempLine += tempAccountSplitArray[5] + "," + tempAccountSplitArray[6];
                tempLine = tempLine.Trim();
                String[] splitArray1 = new String[7];
                splitArray1 = tempLine.Split(',');
                int byteCount = 0;
                byteCount += splitArray1[0].Length;
                byteCount += splitArray1[1].Length;
                byteCount += (splitArray1[2].Length) * 2;//Multiplied by two because it's Arabic, it takes two bytes for each charecter
                byteCount += splitArray1[3].Length;
                byteCount += splitArray1[4].Length;
                byteCount += splitArray1[5].Length;
                byteCount += splitArray1[6].Length;
                byteCount += 6;
                string spacer = "";
                for (int i = 0; i < (449 - byteCount); i++)
                {
                    spacer = spacer + " ";
                }
                streamWr.WriteLine(tempLine + spacer);
                streamWr.Close();
                #endregion
                #region Saving the transaction in its file

                //We will get the id from StaticCounters file, then we will use it to save th new transaction
                String[] staticCounterFile = new String[3];
                streamRe.Close();
                streamRe = new StreamReader(".\\Files\\StaticCounters.bnk", ASCIIEncoding.ASCII);
                staticCounterFile[0] = streamRe.ReadLine();//The first line of the StaticCounters file
                staticCounterFile[1] = streamRe.ReadLine();//The second line
                staticCounterFile[2] = streamRe.ReadLine();//The third
                streamRe.Close();
                String[] splitArray = staticCounterFile[2].Split(',');//Splitting the first line in two parts, the second part is the actual counter
                int transactionsStaticCount = Convert.ToInt32(splitArray[1]);
                transactionsStaticCount++;//Incrementing the counter
                splitArray[1] = Convert.ToString(transactionsStaticCount);
                staticCounterFile[2] = "TransactionsID" + "," + splitArray[1];//Saving the new, incremented, counter back to the first line
                streamWr = new StreamWriter(".\\Files\\StaticCounters.bnk", false, ASCIIEncoding.ASCII);
                streamWr.WriteLine(staticCounterFile[0]);
                streamWr.WriteLine(staticCounterFile[1]);
                streamWr.WriteLine(staticCounterFile[2]);
                streamWr.Close();
                //Now we will actually build the transaction record
                tempLine = Convert.ToString(transactionsStaticCount) + "," + accountID + "," + tellerID + "," + amount + ",D,";
                tempLine += DateTime.Now.Month + "/" + DateTime.Now.Day + "/" + DateTime.Now.Year + ":";
                tempLine += DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
                streamWr = new StreamWriter(".\\Files\\Transactions.bnk", true, ASCIIEncoding.ASCII);
                streamWr.WriteLine(tempLine);
                #endregion
                streamWr.Close();
                streamRe.Close();
                fStream.Close();
                GC.Collect();
            }
            catch
            {
                return 3;//If an error in streaming happened
            }
            return 0;
        }

        public static int DeleteAccount(String accountID)
        {/*We will first check if the account exists in the first place(return 1 at failure)
          * and then perform the delete operation on the file and also delete it from the index
          * deletion will be by replacing the account with '*' marks
          */
            String tempLine = "";
            String[] tempIndexSplit = new String[2];
            Boolean found = false;
            Char[] deleted = new Char[449];
            try
            {
                #region Checking if the account exists and retriving its position
                FileStream fStream = new FileStream(".\\Files\\AccountsIndex.bnk", FileMode.Open, FileAccess.ReadWrite);
                StreamReader streamRe = new StreamReader(fStream, ASCIIEncoding.ASCII);
                while (!streamRe.EndOfStream)
                {
                    tempLine = streamRe.ReadLine();
                    tempIndexSplit = tempLine.Split(',');
                    if (tempIndexSplit[0] == accountID)
                    {
                        found = true;
                        break;
                    }
                }
                if (found == false)
                {
                    streamRe.Close();
                    fStream.Close();
                    return 1;
                }
                fStream.Close();
                streamRe.Close();
                #endregion
                #region Finding the account, and deleting it
                fStream = new FileStream(".\\Files\\Accounts.bnk", FileMode.Open, FileAccess.ReadWrite);
                fStream.Seek(Convert.ToInt64(tempIndexSplit[1]), SeekOrigin.Begin);
                for (int i = 0; i < 449; i++)
                {
                    deleted[i] = '*';
                }
                tempLine = new string(deleted);
                StreamWriter streamWr = new StreamWriter(fStream);
                streamWr.WriteLine(tempLine);
                streamWr.Close();
                fStream.Close();
                #endregion
                #region Deleting the account from the index file
                /*We plan to delete it from the index by copying the whole file into another TEMP file, line by line 
                 without copying */
                streamRe = new StreamReader(".\\Files\\AccountsIndex.bnk", ASCIIEncoding.ASCII);
                streamWr = new StreamWriter(".\\Files\\temp.bnk", false, ASCIIEncoding.ASCII);
                while (!streamRe.EndOfStream)
                {
                    tempLine = streamRe.ReadLine();
                    tempIndexSplit = tempLine.Split(',');
                    if (tempIndexSplit[0] != accountID)
                    {
                        streamWr.WriteLine(tempLine);
                    }
                    else
                    {
                        streamWr.WriteLine("*********************************************************");
                    }
                }
                streamWr.Close();
                streamRe.Close();
                /*Now we copy the re-write the contents of the AccountsIndex by copying the temp file to it */
                streamRe = new StreamReader(".\\Files\\temp.bnk", ASCIIEncoding.ASCII);
                streamWr = new StreamWriter(".\\Files\\AccountsIndex.bnk", false, ASCIIEncoding.ASCII);
                while (!streamRe.EndOfStream)
                {
                    tempLine = streamRe.ReadLine();
                    streamWr.WriteLine(tempLine);
                }
                streamWr.Close();
                streamRe.Close();
                File.Delete(".\\Files\\temp.bnk");
                File.Create(".\\Files\\temp.bnk");
                #endregion
            }
            catch
            {
                return 2;//Return 2 at I/O failure
            }
            return 0;
        }

        public static int Transfer(String sourceID, String destinationID, String amount, String tellerID)
        {//We basically withdraw the amount from one account and deposit it into another
            //we return 3 when the amount is not available in the source account, we return 1 at any other failure
            //we return 0 at success
            int errorLvl = DataAccess.Withdraw(sourceID, tellerID, amount);
            if (errorLvl == 3)
                return 3;

            if (errorLvl == 0)
            {
                errorLvl = DataAccess.Deposit(destinationID, tellerID, amount);
                if (errorLvl == 0)
                    return 0;
            }
            return 1;
        }

        public static String ViewLastTenTransactions(String accountID)
        {
            /*
             * We will read the transactions file, reading each line, checking if it has the accountID we are searching for
             * if it has it, then we save it in a string
             * then we split that string into an array of strings and return it
             * we return null at failure
             * NOTE : We didn't create an array of size 10 and saved the strings in it because we are not sure that an account
             * has 10 transactions, so that would waste space and return an array that might not be entirely filled
             * 
             * Modification: returns string with the transactions seperated by ';' as streamReader and streamWriter only sends strings
             *         So the received string will be splitted in the Client Side.
             */
            String tempLine = "";
            String[] tempSplitArray = new String[6];
            String tobeSplitString = "";
            StreamReader streamRe = new StreamReader(".\\Files\\Transactions.bnk", ASCIIEncoding.ASCII);
            Boolean found = false;
            int counter = 0;
            while (!streamRe.EndOfStream && counter < 10)
            {
                tempLine = streamRe.ReadLine();
                tempSplitArray = tempLine.Split(',');
                if (tempSplitArray[1] == accountID)
                {
                    found = true;
                    tobeSplitString = tobeSplitString + tempLine + ";";
                    counter++;
                }
            }
            if (found == false)
            {
                streamRe.Close();
                return null;
            }//I will Modify the next three Lines of Code because...we must return strings only not array of strings
             //and I will split it with ';' when recieved in the Client Side.
             //String[] transactions = tobeSplitString.Split(';');
            streamRe.Close();
            return tobeSplitString;
        }

        public static int TellerCheckLogIn(String userName, String Password)
        {/*
          * Reading the tellers file record by record, checking if the User Name is valid(return 1 at failure), then checking if the
          * password is valid (return 2 at failure)
          */
            String[] SplitArray = new String[4];
            String tempLine = "";
            try
            {
                StreamReader streamRe = new StreamReader(".\\Files\\Tellers.bnk", ASCIIEncoding.ASCII);
                while (!streamRe.EndOfStream)
                {
                    tempLine = streamRe.ReadLine();
                    SplitArray = tempLine.Split(',');
                    if (SplitArray[2] == userName)
                    {
                        if (SplitArray[3] == Password)
                            return 0;
                        else
                            return 2;
                    }
                }
                return 1;//If the account username was not found
            }
            catch
            {
                return 3;
            }
        }
    }
}