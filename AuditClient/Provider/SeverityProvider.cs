using AuditClient.Models;
using AuditClient.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AuditClient.Provider
{
    public class SeverityProvider : ISeverityProvider
    {
        private static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(SeverityProvider));
        private readonly IResponseRepo _response;

        public SeverityProvider(IResponseRepo response)
        {
            _response = response;
        }
        public async Task<HttpResponseMessage> CalculateSeverity(AuditRequest request)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            try
            {
                using (var client = new HttpClient())
                {
                    var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);
                    response = await client.PostAsync("http://40.88.225.44/api/auditseverity/", content);
                    _log4net.Info("Successfully got the response in severity provider for project " + request.ProjectName);
                }
                //using (var client = new HttpClient())
                //{
                //    var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                //    client.DefaultRequestHeaders.Accept.Add(contentType);
                //    response = await client.PostAsync("https://localhost:44316/api/auditseverity/", content);
                //    _log4net.Info("Successfully got the response in severity provider for project " + request.ProjectName);
                //}
            }
            catch(Exception e)
            {
                _log4net.Error("Unexpected error has occured with message " + e.Message + " for request with project name " + request.ProjectName);
            }
            return response;

        }

        public async Task<bool> CreateResponse(AuditResponseDbo auditResponse)
        {
            bool result;
            try
            {
                result=await _response.CreateResponse(auditResponse);
                _log4net.Info("Provider called repository to store data for response with id " + auditResponse.AuditId);
                return result;

            }
            catch(Exception e)
            {
                _log4net.Error("Unexpected error occured with message " + e.Message + " for response with id " + auditResponse.AuditId);
                return false;
            }
               
        }
    }
}
