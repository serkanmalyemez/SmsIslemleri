using NetGSM.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NetGSM.Concrete
{
    public class SmsSend : ISmsSend
    {
        string username = "NetGsmTarafındanVerilenUserName";
        string password = "NetGsmTarafındanVerilenPassword";
        string header = "NetGsmTarafındanVerilenGondericiAdi";

        public string HttpGet(string message, string number)
        {
            string html = string.Empty;
            string url = @"https://api.netgsm.com.tr/sms/send/get/?";
            url += "usercode=" + username;
            url += "&password=" + password;
            url += "&gsmno=" + number;
            url += "&message=" + message;
            url += "&msgheader=" + header;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }

            return html;
        }
        public async Task<netgsm.smsGonder1NV2Response> Soap(string message, string[] number)
        {
            number[0].IndexOf('9');
            netgsm.smsnnClient smsgonder1n = new netgsm.smsnnClient();
            var request = await smsgonder1n.smsGonder1NV2Async(username, password, header, message, number, "TR", "", "", "", 1);
            return request;
        }
        public string XmlSend(string message, string number)
        {
            string ss = "";
            ss += "<?xml version='1.0' encoding='UTF-8'?>";
            ss += "<mainbody>";
            ss += "<header>";
            ss += "<company dil='TR'>Netgsm</company>";
            ss += "<usercode>" + username + "</usercode>";
            ss += "<password>" + password + "</password>";
            ss += "<type>1:n</type>";
            ss += "<msgheader>" + header + "</msgheader>";
            ss += "</header>";
            ss += "<body>";
            ss += "<msg>";
            ss += "<![CDATA[" + message + "]]>";
            ss += "</msg>";
            ss += "<no>" + number + "</no>";
            ss += "</body>  ";
            ss += "</mainbody>";
            return XMLPOST("https://api.netgsm.com.tr/sms/send/xml", ss);
        }
        private string XMLPOST(string PostAddress, string xmlData)
        {
            try
            {
                WebClient wUpload = new WebClient();
                HttpWebRequest request = WebRequest.Create(PostAddress) as HttpWebRequest;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                Byte[] bPostArray = Encoding.UTF8.GetBytes(xmlData);
                Byte[] bResponse = wUpload.UploadData(PostAddress, "POST", bPostArray);
                Char[] sReturnChars = Encoding.UTF8.GetChars(bResponse);
                string sWebPage = new string(sReturnChars);
                return sWebPage;
            }
            catch
            {
                return "-1";
            }
        }
    }
}
