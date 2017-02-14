using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleJSON;
using System.Net;
using System.Collections.Specialized;

namespace TelegramBotFirstApp
{
    class Telegram
    {
        int LastUpdateID = 0;
        private static string _token = "275547698:AAFqRX-HIAy-z62gSEQMZYHRvE28TM_vBEw";
        private static string LINK = "https://api.telegram.org/bot";
        public event Response ResponseReceived; // событие для ответа
        ParameterResponse e = new ParameterResponse();
        public void GetUpdates()
        {
            while (true)
            {
                using (WebClient webClient = new WebClient())
                {
                    string response = webClient.DownloadString(LINK + _token + "/getupdates?offset=" + (LastUpdateID + 1));
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
                        if (e.message == "/time")
                        {
                            GetTime();
                        }
                    }
                }
                ResponseReceived(e);
            }
        }
        //test class
        public void GetTime()
        {
            using (WebClient web = new WebClient())
            {
                NameValueCollection coll = new NameValueCollection();
                string date = "Time: " +  DateTime.Now.ToLongTimeString();
                coll.Add("chat_id", e.chatID.ToString());
                coll.Add("text", date);
                web.UploadValues(LINK + _token + "/sendMessage", coll);
            }
        }
        public void SendMessage(string text) {
            using (WebClient web = new WebClient())
            {
                NameValueCollection collection = new NameValueCollection();
                collection.Add("chat_id", e.chatID.ToString());
                collection.Add("text", text);
                web.UploadValues(LINK + _token + "/sendMessage", collection);
            }
        }
    }
    public class ParameterResponse
    {
        public string name;
        public string FirstName;
        public string message;
        public string chatID;
    }
}
