using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using SimpleJSON;
using System.Threading;

namespace TelegramBotFirstApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Telegram Tr = new Telegram();
            Tr.ResponseReceived += Tr_ResponseReceived;
            Thread th = new Thread(Tr.GetUpdates);
            Tr.GetUpdates();
        }

        private static void Tr_ResponseReceived(object sendrer, ParameterResponse e)
        {
            Console.WriteLine("{0} {1}: {2}   chatID:{3}",e.name, e.FirstName,e.message, e.chatID);
        }

        public delegate void Response(object sendrer, ParameterResponse e); // Delegate response
        public class ParameterResponse:EventArgs
        {
            public string name;
            public string FirstName;
            public string message;
            public string chatID;
        }

        class Telegram
        {
            int LastUpdateID = 0;
            private string _token = "275547698:AAFqRX-HIAy-z62gSEQMZYHRvE28TM_vBEw";
            private string LINK = "https://api.telegram.org/bot";
            public event Response ResponseReceived; // событие для ответа
            ParameterResponse e = new ParameterResponse();
            public void GetUpdates()
            {
                while (true)
                {
                    using (WebClient webClient = new WebClient())
                    {
                        string response = webClient.DownloadString(LINK+ _token + "/getupdates?offset=" + (LastUpdateID+1));
                        if (response.Length <= 23)
                            continue;
                        var N = JSON.Parse(response);
                        foreach (JSONNode r in N["result"].AsArray)
                        {
                            LastUpdateID = r["update_id"].AsInt;
                            e.FirstName = r["message"]["from"]["last_name"];
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
