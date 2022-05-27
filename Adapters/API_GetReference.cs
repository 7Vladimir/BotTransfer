using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using BotTransfer.WorkMessage;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;

namespace BotTransfer.Adapters
{
    internal class API_GetReference
    {
        public static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        public static string  pull(int value)
        {
            DateTime dateTime = DateTime.Now;
            string time = dateTime.ToString()
                .Replace(".", "")
                .Replace(":", "")
                .Replace(" " , "");
            WebResponse response;
            string ContentResponse = "";
            // Pass the handler to httpclient(from you are calling api)
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, errors) =>
            {
                // local dev, just approve all certs
                return true;                
            };
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://kassa.amra-bank.com/api/v1/payments");
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, errors) =>
            {
                // local dev, just approve all certs
                return true;
            };

            NetworkCredential myCred = new NetworkCredential("c63054dc-8edc-401e-a648-b372ccc20057", "7793a157-eacb-46a9-8dbb-6085062a5a96");
            request.Method = "POST";
            request.Credentials = myCred;
            request.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            // Certificate with private key
                        request.PreAuthenticate = true;
            var body = @"{" + "\n" +
            @"  ""amount"": {" + "\n" +
            @"    ""currency"": ""RUB""," + "\n" +
            @$"    ""value"": ""{value}""" + "\n" +
            @"  }," + "\n" +
            @$"  ""description"": ""Заказ {time}""," + "\n" +
            @"  ""returnUrl"": ""https://merchant.website/return_url""," + "\n" +
            @"  ""metadata"": {" + "\n" +
            @"    ""orderId"": 2123" + "\n" +
            @"  }" + "\n" +
            @"}";

            string postData = body.ToString();//"ЭтоТелоЗапроса
            request.ContentType = "application/json";
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            byte[] postByteArray = encoding.GetBytes(postData);
            request.ContentLength = postByteArray.Length;
            //((System.Net.HttpWebRequest)request).ProtocolVersion=HttpVersion.Version10;
            System.IO.Stream postStream = request.GetRequestStream();
            postStream.Write(postByteArray, 0, postByteArray.Length);
            postStream.Close();
            try
            {
                using (response = request.GetResponse())//
                {
                    Stream receiveStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(receiveStream, Encoding.UTF8);
                    ContentResponse = reader.ReadToEnd();
                }
                response.Close();
                Newtonsoft.Json.JsonConvert.SerializeObject(response);

            }
            catch (Exception ex)
            {
                int r = 0;//
            }
            
            return "";
        }
    }
}
