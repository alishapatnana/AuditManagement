using AuditClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AuditClient.Provider
{
    public interface ISeverityProvider
    {
        Task<HttpResponseMessage> CalculateSeverity(AuditRequest request);
        Task<bool> CreateResponse(AuditResponseDbo auditResponse);
    }
}
