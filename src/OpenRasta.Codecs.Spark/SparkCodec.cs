using System.Collections.Generic;
using System.IO;
using System.Text;
using OpenRasta.Codecs.Spark;
using OpenRasta.Codecs.Spark;
using OpenRasta.Codecs.Spark;
using OpenRasta.Collections.Specialized;
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
		static readonly string[] DEFAULT_VIEW_NAMES = new[] { "index", "default", "view", "get" };
		public ILogger Log { get; set; }
		public IDictionary<string, string> Configuration { get; private set; }
		readonly IRequest request;
		private readonly ISparkConfiguration sparkConfiguration;

		public SparkCodec(IRequest request, ISparkConfiguration sparkConfiguration)
		{
			this.request = request;
			this.sparkConfiguration = sparkConfiguration;
		}

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
			string templateAddress = GetViewVPath(Configuration, codecParameterList.ToArray());
			RenderTemplate(response, templateAddress, entity);
		}

		private void RenderTemplate(IHttpEntity response, string templateAddress, object entity)
		{
			var descriptor = new SparkViewDescriptor().AddTemplate(templateAddress);
			var engine = sparkConfiguration.Container.GetService<ISparkViewEngine>();
			var view = (SparkResourceView)engine.CreateInstance(descriptor);
			try
			{
				view.ViewData = new ViewData(entity);
				RenderToResponse(response, view);
			}
			finally
			{
				engine.ReleaseInstance(view);
			}
		}

		static void RenderToResponse(IHttpEntity response, ISparkView templateBase)
		{
			var targetEncoding = Encoding.UTF8;
			response.ContentType.CharSet = targetEncoding.HeaderName;
			TextWriter writer = null;
			bool ownsWriter = false;
			try
			{
				if (response is ISupportsTextWriter)
				{
					writer = ((ISupportsTextWriter)response).TextWriter;
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

		public static string GetViewVPath(IDictionary<string, string> codecConfiguration, string[] codecUriParameters)
		{
			// if no pages were defined, return 501 not implemented
			if (codecConfiguration == null || codecConfiguration.Count == 0)
			{
				return null;
			}

			// if no request parameters, take the default or return null
			if (codecUriParameters == null || codecUriParameters.Length == 0)
			{
				foreach (string defaultViewName in DEFAULT_VIEW_NAMES)
					if (codecConfiguration.Keys.Contains(defaultViewName))
						return codecConfiguration[defaultViewName];
			}
			else
			{
				string requestParameter = codecUriParameters[codecUriParameters.Length - 1];
				if (codecConfiguration.Keys.Contains(requestParameter))
					return codecConfiguration[requestParameter];
			}
			return null;
		}
	}
}