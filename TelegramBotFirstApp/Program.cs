﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Threading;
using System.Collections.Specialized;

namespace TelegramBotFirstApp
{
    public delegate void Response(ParameterResponse e); // Delegate response
    class Program
    {
        static void Main(string[] args)
        {
            Telegram Tr = new Telegram();
            Tr.ResponseReceived += Tr_ResponseReceived;
            Thread th = new Thread(Tr.GetUpdates);
            Tr.GetUpdates();
            Console.ReadKey();
        }
        private static void Tr_ResponseReceived(ParameterResponse e)
        {
            Console.WriteLine("{0} {1}: {2}  send: {3}  chatID: {4}", e.name, e.FirstName, e.message,e.RefMes, e.chatID);
        }
    }
    
}
