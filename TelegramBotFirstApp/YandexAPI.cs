using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using SimpleJSON;

namespace TelegramBotFirstApp
{
    class YandexAPI
    {
        static string key = "trnsl.1.1.20170214T095217Z.90fd12223e702d7b.21d756e8c7aa95ef36d2fb4d9b5555e3c9720fe0";
        static string LINK = "https://translate.yandex.net/api/v1.5/tr.json/translate?lang=ru-en&key=" + key;
        public string GetResult(string text)
        {
            using (WebClient web = new WebClient()) {
                string URIresponse = LINK + "&text=" + text;
                string response = web.DownloadString(URIresponse);
                var n = JSON.Parse(response);
                int val = Int32.Parse(n["code"].Value);
                if(val == 200)
                {
                    Console.WriteLine("Error");
                }
                Console.WriteLine(val);
            }
            return "good";
        }
    }
}
