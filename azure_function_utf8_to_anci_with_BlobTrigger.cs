//using System;
using System.IO;
using Microsoft.Azure.WebJobs;
//using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using System.Text; // Stream-Reader,Writer Entcoding Function UTF-8 and Latin1 = ANSI 

namespace Company.Function{
    public class BlobTrigger1{
        [FunctionName("BlobTrigger1")]
        public void Run([BlobTrigger("inbound/{name}", Connection = "connection")] string name, 
        [Blob("inbound/{name}", FileAccess.Read)] Stream inbound_blob, 
        [Blob("outbound/{name}", FileAccess.Write)] Stream outbound_blob,
        ILogger log ){
            StreamReader sr_inbound_blob_utf8 = new StreamReader(inbound_blob, Encoding.UTF8);
            StreamWriter sw_outbound_blob_anci = new StreamWriter(outbound_blob, Encoding.Latin1);
            string Line;
            while((Line = sr_inbound_blob_utf8.ReadLine()) != null){
               sw_outbound_blob_anci.WriteLine(Line);
            }
            sr_inbound_blob_utf8.Close();
            sw_outbound_blob_anci.Close();
        }
    }
}
