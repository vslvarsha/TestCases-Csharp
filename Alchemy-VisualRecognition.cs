using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Web;
using System.Security.Policy;
using System.IO;
using System.Net;
namespace AlchemyAPI
{
    public class AlchemyAPI_Params
    {
        public static String OUTPUT_JSON = "xml";
        public static String OUTPUT_RDF = "rdf";
        private String url;
        private String html;
        private String text;
        private String outputMode = OUTPUT_JSON;
        private String customParameters;
        public String getUrl()
        {
            return url;
        }
        public void setUrl(String url)
        {
            this.url = url;
        }
        public String getHtml()
        {
            return html;
        }
        public void setHtml(String html)
        {
            this.html = html;
        }
        public String getText()
        {
            return text;
        }
        public void setText(String text)
        {
            this.text = text;
        }
        public String getOutputMode()
        {
            return outputMode;
        }
        public void setOutputMode(String outputMode)
        {
            if (!outputMode.Equals(AlchemyAPI_Params.OUTPUT_JSON) && !outputMode.Equals(OUTPUT_RDF))
            {
                throw new SystemException("Invalid setting " + outputMode + " for parameter outputMode");
            }
            this.outputMode = outputMode;
        }
        public String getCustomParameters()
        {
            return customParameters;
        }

        public void setCustomParameters(params String[] customParameters)
        {
            StringBuilder data = new StringBuilder();

            for (int i = 0; i < customParameters.Length; ++i)
            {
                data.Append('&').Append(customParameters[i]);
                if (++i < customParameters.Length)
                    data.Append('=').Append(HttpUtility.UrlEncode(customParameters[i]));
            }

            this.customParameters = data.ToString();
        }

        public String getParameterString()
        {
            String retString = "";
            if (url != null) retString += "&url=" + HttpUtility.UrlEncode(url);
            if (html != null) retString += "&html=" + HttpUtility.UrlEncode(html);
            if (text != null) retString += "&text=" + HttpUtility.UrlEncode(text);
            if (customParameters != null) retString += customParameters;
            if (outputMode != null) retString += "&outputMode=" + outputMode;
            return retString;
        }

    }

    class AlAPI
    {
        private String serviceName = "Alchemy API";
        private String requestUri = "http://access.alchemyapi.com/calls/";
        private String apiKey = "dbba4d2b66d284bf32f222c7f852ca79eda2f2a5";


        static void Main(string[] args)
        {
            Console.WriteLine("starting");
            AlAPI a = new AlAPI();
            XmlDocument d = a.URLGetAuthor("http://carolinedonahue.blog.com/files/2013/04/Hollywood-collage.jpg");
            // Console.Read();
        }
        public XmlDocument URLGetAuthor(string url)
        {
            return URLGetAuthor(url, new AlchemyAPI_Params());
        }
        public XmlDocument URLGetAuthor(string url, AlchemyAPI_Params param)
        {
            CheckURL(url);

            param.setUrl(url);
            param.setOutputMode("xml");
            return GET("URLGetRankedImageFaceTags", "url", param);
        }
        private void CheckURL(String url)
        {
            return;
        }
        private XmlDocument GET(String callName, String callPrefix, AlchemyAPI_Params param)
        {
            StringBuilder uri = new StringBuilder();
            uri.Append(requestUri).Append(callPrefix).Append('/').Append(callName)
               .Append('?').Append("apikey=").Append(this.apiKey);
            uri.Append(param.getParameterString());

            string url = uri.ToString();
            //   Console.Write("\n{0}",url);
            HttpWebRequest handle = (HttpWebRequest)WebRequest.Create(url);
            return doRequest(handle, param.getOutputMode());
        }


        private XmlDocument doRequest(HttpWebRequest handle, String outputMode)
        {
            HttpWebResponse response = (HttpWebResponse)handle.GetResponse();
            System.IO.StreamWriter file = new System.IO.StreamWriter("Outputfilepath");
            StreamReader istream = new StreamReader(response.GetResponseStream());
            string str = istream.ReadToEnd();
            Console.Write(str);
            Console.Read();
            file.Write(str);

            XmlDocument doc = new XmlDocument();


            istream.Close();

            return doc;
        }


    }
}
