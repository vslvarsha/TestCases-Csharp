using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Object;
using System.CodeDom.Compiler;
using System.Web;
using System.Net;
using System.IO;


namespace RelationshipExtraction
{
    class Program
    {

        private String serviceName = "relationship_extraction";

        // If running locally complete the variables below with the information in VCAP_SERVICES
        private static String baseURL = "https://gateway.watsonplatform.net/relationship-extraction-beta/api";
        private static String username = "63d168c5-0a47-4d7a-93d3-f19ff12c291f";
        private static String password = "ZZ5rAWsi7CD0";
        static void Main(string[] args)
        {
            doStuff();
        }

        static void doStuff(){


		Console.WriteLine("starting");

        String text = "Pakistan captain Misbah-ul-Haq and flamboyant all-rounder Shahid Afridi's ODI careers came to an end on Friday following their World Cup quarterfinal defeat against Australia but the two players will continue to play Test and Twenty cricket respectively.";
		String sid = "ie-en-news";

            var qparams = new List<KeyValuePair<string, string>>() { 
    new KeyValuePair<string, string>("txt", text),
    new KeyValuePair<string, string>("sid", sid),
    new KeyValuePair<string, string>("rt", "xml"),
};

            try{

            StringBuilder postString = new StringBuilder();
            bool first = true;
            foreach (KeyValuePair<string,string> pair in qparams)
            {
                if (first)
                    first = false;
                else
                    postString.Append("&");
                postString.AppendFormat("{0}={1}", pair.Key, System.Web.HttpUtility.UrlEncode(pair.Value));
            }

            UTF8Encoding utf = new UTF8Encoding();
            byte[] postBytes = utf.GetBytes(postString.ToString());
            

            Uri serviceURI = new Uri(baseURL);
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(serviceURI);
            String encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(username + ":" + password));
            request.Headers.Add("Authorization", "Basic " + encoded);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postBytes.Length;

            // add post data to request
            Stream postStream = request.GetRequestStream();
            postStream.Write(postBytes, 0, postBytes.Length);
            postStream.Close();

            String response = "";
            using (HttpWebResponse webResponse = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader sr = new StreamReader(webResponse.GetResponseStream()))
                {
                    response = sr.ReadToEnd().Trim();
                    sr.Close();
                }
                webResponse.Close();
            }
          
            Console.Write(response);

            Console.Read();

		} catch (Exception e) {
			Console.WriteLine(e.Message);
		}



	}
    }
}
