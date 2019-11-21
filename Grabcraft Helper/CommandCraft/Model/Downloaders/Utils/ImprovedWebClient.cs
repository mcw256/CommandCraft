using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Grabcraft_Helper.Model.Downloaders.Utils
{
    class ImprovedWebClient : WebClient
    {
            CookieContainer _cookies = new CookieContainer();
            //^here are automatically stored the cookies

            protected override WebRequest GetWebRequest(Uri address)
            {
                WebRequest request = base.GetWebRequest(address);

                if (request is HttpWebRequest)  //if it is a Http request
                    ((HttpWebRequest)request).CookieContainer = _cookies;
                //^we bind that cookie container to the request

                return request; // return the modified request (the one with cookies)
            }
    }
}
