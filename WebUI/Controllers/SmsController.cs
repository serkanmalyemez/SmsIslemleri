using Microsoft.AspNetCore.Mvc;
using NetGSM.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Models;

namespace WebUI.Controllers
{
    /// <summary>
    /// https://www.netgsm.com.tr/dokuman/?language=C%23#xml-post-sms-g%C3%B6nderme
    /// </summary>
    public class SmsController : Controller
    {
        private ISmsSend _smsSend;
        public SmsController(ISmsSend smsSend)
        {
            _smsSend = smsSend;
        }

        public IActionResult Index()
        {
            var request = new SmsSendViewModel { Bulkid = "" };
            return View(request);
        }
        public IActionResult SendXmlPost(string tel, string msg)
        {
            string jData = "";
            if (!String.IsNullOrEmpty(tel) && !String.IsNullOrEmpty(msg))
            {
                var bulkid = _smsSend.XmlSend(msg, tel);
                jData = bulkid.ToString();
            }
            else
            {
                jData = "Mesaj metni veya telefon alanı boş olamaz!..";
            }
            SmsSendViewModel smsSendViewModel = new SmsSendViewModel { Bulkid = jData };

            return Json(smsSendViewModel);
        }
        public IActionResult SendHtmlGet(string tel, string msg)
        {
            string jData = "";
            if (!String.IsNullOrEmpty(tel) && !String.IsNullOrEmpty(msg))
            {
                var bulkid = _smsSend.HttpGet(msg, tel);
                jData = bulkid.ToString();
            }
            else
            {
                jData = "Mesaj metni veya telefon alanı boş olamaz!..";
            }
            SmsSendViewModel smsSendViewModel = new SmsSendViewModel { Bulkid = jData };

            return Json(smsSendViewModel);
        }
        public async Task<IActionResult> SendSOAP(string tel, string msg)
        {
            string jData = "";
            if (!String.IsNullOrEmpty(tel) && !String.IsNullOrEmpty(msg))
            {
                string[] number = new string[] { tel };
                var bulkid = await _smsSend.Soap(msg, number);
                jData = bulkid.ToString();
            }
            else
            {
                jData = "Mesaj metni veya telefon alanı boş olamaz!..";
            }
            SmsSendViewModel smsSendViewModel = new SmsSendViewModel { Bulkid = jData };

            return Json(smsSendViewModel);
        }
    }
}
