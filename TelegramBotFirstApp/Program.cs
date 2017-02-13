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
            Telegram Tr = new Telegram();
            Tr.ResponseReceived += Tr_ResponseReceived;
            Tr.GetUpdates();
        }

        private static void Tr_ResponseReceived(object sendrer, ParameterResponse e)
        {
            Console.WriteLine("{0}: {1}   chatID:{2}",e.name,e.message, e.chatID);
        }

        public delegate void Response(object sendrer, ParameterResponse e); // Delegate response
        public class ParameterResponse:EventArgs
        {
            public string name;
            public string message;
            public string chatID;
        }

        class Telegram
        {
            public string token = "token";
            int LastUpdateID = 0;
            public event Response ResponseReceived; // событие для ответа
            ParameterResponse e = new ParameterResponse();

            public void GetUpdates()
            {
                while (true)
                {
                    using (WebClient webClient = new WebClient())
                    {
                        string response = webClient.DownloadString("https://api.telegram.org/bot"+ token + "/getupdates");
                        var N = JSON.Parse(response);
                        foreach (JSONNode r in N["result"].AsArray)
                        {
                            LastUpdateID = r["update_id"].AsInt;
                            e.name = r["message"]["from"]["first_name"];
                            e.message = r["message"]["text"];
                            e.chatID = r["message"]["chat"]["id"];
                        }
                    }
                    ResponseReceived(this, e);
                }
            }
        }
        }
}
