using System.IO;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Issuer.Infrastructure.Results
{
    public class JsonNetResult : ActionResult
    {
        private readonly object _model;
        private readonly JsonSerializer _serializer;

        public JsonNetResult(object model)
        {
            _model = model;

            var settings = new JsonSerializerSettings
                               {
                                   ContractResolver = new CamelCasePropertyNamesContractResolver()
                               };

            _serializer = JsonSerializer.Create(settings);
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var stream = context.HttpContext.Response.OutputStream;

            using (var writer = new JsonTextWriter(new StreamWriter(stream)))
            {
                _serializer.Serialize(writer, _model);
                writer.Flush();
            }
        }
    }
}