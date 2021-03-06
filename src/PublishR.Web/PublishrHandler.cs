using System.Web;
using System.Web.Routing;
using PublishR.Handlers;
using PublishR.Messaging;
using PublishR.Registry;

namespace PublishR.Web
{
    public class PublishrHandler : PublishrHandlerBase, IHttpHandler
    {
        public RequestContext RequestContext { get; private set; }

        public PublishrHandler(RequestContext requestContext, IGlobalRegistry globalRegistry)
            : base(globalRegistry)
        {
            RequestContext = requestContext;
        }

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.QueryString.Get("_") != null)
            {
                string json = context.Request.QueryString.Get("_");
                var message = ServiceStack.Text.JsonSerializer.DeserializeFromString<PublishrMessage>(json);
                message.Raw = json;

                if (Correlate())
                {
                    Handle(message);
                }
            }
        }

        public virtual bool IsReusable
        {
            get
            {
                return true;
            }
        }
    }
}