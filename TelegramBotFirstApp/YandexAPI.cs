using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using SimpleJSON;

namespace TelegramBotFirstApp
{
    class YandexResponse
    {
        public int code;
        public string text;
    }
    class YandexAPI
    {
        private static string key = "trnsl.1.1.20170214T095217Z.90fd12223e702d7b.21d756e8c7aa95ef36d2fb4d9b5555e3c9720fe0";
        private static string LINK = "https://translate.yandex.net/api/v1.5/tr.json/translate?lang=ru-en&key=" + key;
        YandexResponse obj = new YandexResponse();
        public object GetResult(string text)
        {
            try
            {
                using (WebClient web = new WebClient())
                {
                    string URIresponse = LINK + "&text=" + text;
                    string response = web.DownloadString(URIresponse);
                    var n = JSON.Parse(response);
                    obj.code = n["code"].AsInt;
                    obj.text = n["text"][0].Value;
                    if (obj.code != 200)
                    {
                        throw new Exception("Что то случилось я YandexAPI");
                    }
                    return obj;
                } 
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка: " + e.Message);
                return "";
            }
        }
    }
}
