using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Net;



namespace TradeoffAnalytics
{
    class Program
    {
        private static String baseURL = "https://gateway.watsonplatform.net/tradeoff-analytics-beta/api";
        private static String username = "cfd2440b-f6a5-4fdd-a4cd-122beae071c1";
        private static String password = "i5bEoMOSVGWS";
	 
        static void Main(string[] args)
        {
            doStuff();
        }

        static void doStuff(){



            Console.WriteLine("Starting");


            
                byte[] jsonStr = File.ReadAllBytes("C://Users/Varsha/Documents/Visual Studio 2013/Projects/TradeoffAnalytics/to.json");
            
		try {
	         String queryStr=null;
	         String url = baseURL + "/v1/dilemmas";
	         if (queryStr != null) {
	            url += "?" + queryStr;
	         }
            
	         Uri uri = new Uri(url);
 
	         HttpWebRequest newReq = (HttpWebRequest)WebRequest.Create(url);
             String encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(username + ":" + password));
             newReq.Headers.Add("Authorization", "Basic " + encoded);
             newReq.Accept =  "application/json";
             newReq.ContentType = "application/json";
             newReq.ContentLength = jsonStr.Length;
            newReq.Method = "POST";


            
	         
 
	       /* newReq.bodyString(s, ContentType.APPLICATION_JSON);
 
	         Executor executor = Executor.newInstance().auth(username, password);
	         Response response = executor.execute(newReq);
	         HttpResponse httpResponse = response.returnResponse();*/
	   
	        // httpResponse.getEntity().writeTo(System.out);

            
             Stream postStream = newReq.GetRequestStream();
            postStream.Write(jsonStr, 0, jsonStr.Length);
            postStream.Close();

            String response = "";
          
            using (HttpWebResponse webResponse = (HttpWebResponse)newReq.GetResponse())
            {
                using (StreamReader sr = new StreamReader(webResponse.GetResponseStream()))
                {
                    response = sr.ReadToEnd().Trim();
                    sr.Close();
                }
                webResponse.Close();

            }
            

            System.IO.StreamWriter file = new System.IO.StreamWriter("C:/Users/Varsha/Documents/Visual Studio 2013/Projects/TradeoffAnalytics/output.json");
            dynamic formatted = JsonConvert.DeserializeObject(response);
            JsonConvert.SerializeObject(formatted, Formatting.Indented);
            file.Write(formatted);


            Console.Write(formatted);

            Console.Read();

	       
	      } catch (Exception e) {
	         Console.WriteLine("got error: " + e.Message);  
	      }
    
		
 
		Console.WriteLine("end");
		
	}
 
    }
}
