using System.Collections.Generic;
using System.IO;
using System.Text;
using OpenRasta.Collections.Specialized;
using OpenRasta.DI;
using OpenRasta.Diagnostics;
using OpenRasta.IO;
using OpenRasta.Web;
using Spark;

namespace OpenRasta.Codecs.Spark
{
	[MediaType("application/xhtml+xml;q=0.9", "xhtml"), MediaType("text/html", "html"),
	 MediaType("application/vnd.openrasta.htmlfragment+xml;q=0.5")]
	public class SparkCodec : IMediaTypeWriter
	{
		private static readonly string[] DEFAULT_VIEW_NAMES = new[] {"index", "default", "view", "get"};
		private readonly IRequest request;
		private readonly IDependencyResolver resolver;
		private readonly ISparkConfiguration sparkConfiguration;

		public SparkCodec(IRequest request, ISparkConfiguration sparkConfiguration, IDependencyResolver resolver)
		{
			this.request = request;
			this.resolver = resolver;
			this.sparkConfiguration = sparkConfiguration;
		}

		public ILogger Log { get; set; }
		public IDictionary<string, string> Configuration { get; private set; }

		#region IMediaTypeWriter Members

		object ICodec.Configuration
		{
			get { return Configuration; }
			set
			{
				if (value != null)
					Configuration = value.ToCaseInvariantDictionary();
			}
		}

		public void WriteTo(object entity, IHttpEntity response, string[] codecParameters)
		{
			var codecParameterList = new List<string>(codecParameters);
			if (!string.IsNullOrEmpty(request.UriName))
				codecParameterList.Add(request.UriName);
			string templateAddress = GetViewVPath(Configuration);
			RenderTemplate(response, templateAddress, entity);
		}

		#endregion

		private void RenderTemplate(IHttpEntity response, string templateAddress, object entity)
		{
			SparkViewDescriptor descriptor = new SparkViewDescriptor().AddTemplate(templateAddress);
			var engine = sparkConfiguration.Container.GetService<ISparkViewEngine>();
			var view = (SparkResourceView) engine.CreateInstance(descriptor);
			view.ViewData = new ViewData(entity);
			view.Resolver = resolver;
			try
			{
				RenderToResponse(response, view);
			}
			finally
			{
				engine.ReleaseInstance(view);
			}
		}

		private static void RenderToResponse(IHttpEntity response, ISparkView templateBase)
		{
			Encoding targetEncoding = Encoding.UTF8;
			response.ContentType.CharSet = targetEncoding.HeaderName;
			TextWriter writer = null;
			bool ownsWriter = false;
			try
			{
				if (response is ISupportsTextWriter)
				{
					writer = ((ISupportsTextWriter) response).TextWriter;
				}
				else
				{
					writer = new DeterministicStreamWriter(response.Stream, targetEncoding, StreamActionOnDispose.None);
					ownsWriter = true;
				}
				templateBase.RenderView(writer);
			}
			finally
			{
				if (ownsWriter)
					writer.Dispose();
			}
		}

		public static string GetViewVPath(IDictionary<string, string> codecConfiguration)
		{
			// if no pages were defined, return 501 not implemented
			if (codecConfiguration == null || codecConfiguration.Count == 0)
			{
				return null;
			}
			string result = null;
			if (codecConfiguration.TryGetValue("TemplateName", out result))
			{
				return result;
			}
			return null;
		}
	}
}