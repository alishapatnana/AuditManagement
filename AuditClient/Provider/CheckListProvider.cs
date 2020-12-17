using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AuditClient.Provider
{
    public class CheckListProvider : ICheckListProvider
    {
        private static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(CheckListProvider));
        public async Task<HttpResponseMessage> GetQuestions(string AuditType)
        {
            HttpResponseMessage response=new HttpResponseMessage();
            try
            {
                using (var client = new HttpClient())
                {
                    var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);
                    response = await client.GetAsync("http://20.75.131.48/api/auditchecklist/" + AuditType);
                    _log4net.Info("Checklist provider has successfully got questions of type " + AuditType);

                }
                //using (var client = new HttpClient())
                //{
                //    var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                //    client.DefaultRequestHeaders.Accept.Add(contentType);
                //    response = await client.GetAsync("https://localhost:44320/api/auditchecklist/" + AuditType);
                //    _log4net.Info("Checklist provider has successfully got questions of type " + AuditType);

                //}
            }
            catch(Exception e)
            {
                _log4net.Error("Unexpected error has occured with message -" + e.Message+" of type "+AuditType);
            }
            return response;
        }
    }
}
