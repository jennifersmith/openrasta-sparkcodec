using System;
using System.Collections.Generic;
using System.Web;
using OpenRasta.DI;
using OpenRasta.Web;
using OpenRasta.Web.Markup;
using OpenRasta.Web.Markup.Modules;
using Spark;

namespace OpenRasta.Codecs.Spark
{
	public abstract class SparkResourceView : AbstractSparkView, IXhtmlAnchorSite
	{
		public ViewData ViewData { get; set; }

		XhtmlAnchor _xhtmlAnchor;
		public IDependencyResolver Resolver { get; set; }
		public IXhtmlAnchor Xhtml
		{
			get
			{
				// todo: where has my user gone?
				if (_xhtmlAnchor == null)
					_xhtmlAnchor = new XhtmlAnchor(Resolver, new XhtmlTextWriter(Output), () => HttpContext.Current.User);
				return _xhtmlAnchor;
			}
		}
		public IList<Error> Errors { get; set; }
		public IDisposable scope(IContentModel element)
		{
			return IXhtmlAnchorSiteExtensions.scope(this, element);
		}
	}
}