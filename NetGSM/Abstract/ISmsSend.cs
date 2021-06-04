using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetGSM.Abstract
{
    public interface ISmsSend
    {
        string XmlSend(string message, string number);//number = 05xxxxxxxxxx
        string HttpGet(string message, string number);//number = 05xxxxxxxxxx
        Task<netgsm.smsGonder1NV2Response> Soap(string message, string[] number);
    }
}
