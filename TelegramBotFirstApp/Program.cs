using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using SimpleJSON;

namespace TelegramBotFirstApp
{
    class Program
    {
        static void Main(string[] args)
        {
        }
        public delegate void Response(object sendrer, ParameterResponse e); // Delegate response
        public class ParameterResponse:EventArgs
        {
            public string name;
            public string message;
            public string chat;
        }

        class Telegram
        {
            public string token;
            int LastUpdateID = 0;
            public event Response ResponseReceived; // событие для ответа
            ParameterResponse e = new ParameterResponse();

            public void GetUpdates()
            {
                while (true)
                {

                }
            }
        }
        }
}
