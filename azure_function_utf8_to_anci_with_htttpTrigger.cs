using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text; // stream- Reader/Writer Entcoding Function UTF-8 and Latin1 = ANSI 

namespace Company.Function
{
    public static class HttpTrigger1
    {
        [FunctionName("HttpTrigger1")]
        public static string Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            [Blob("inbound/WKZ_encoding_test.csv", FileAccess.Read)] Stream inbound_blob,
            [Blob("outbound/test_entkodiert_httpTrigger.csv", FileAccess.Write)] Stream outbound_blob,
            ILogger log)
        {   
            StreamReader sr_inbound_blob_utf8 = new StreamReader(inbound_blob, Encoding.UTF8);
            StreamWriter sw_outbound_blob_anci = new StreamWriter(outbound_blob, Encoding.Latin1);
            string read_line; 
            while((read_line = sr_inbound_blob_utf8.ReadLine()) != null){
                sw_outbound_blob_anci.WriteLine(read_line);
            }
            sr_inbound_blob_utf8.Close();
            sw_outbound_blob_anci.Close();

            return "umcodiert ";
        }
    }
}
