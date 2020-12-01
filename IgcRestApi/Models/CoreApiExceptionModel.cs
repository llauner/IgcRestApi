using IgcRestApi.Common.Helper;
using System;
using System.Net;

namespace IgcRestApi.Models
{
    public class CoreApiExceptionModel
    {
        public DateTime DateTime { get; set; }

        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }


        public override string ToString()
        {
            return JsonHelper.Serialize(this);
        }
    }
}
