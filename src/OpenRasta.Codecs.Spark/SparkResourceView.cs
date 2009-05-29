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
		private XhtmlAnchor _xhtmlAnchor;
		public ViewData ViewData { get; set; }
		public IDependencyResolver Resolver { get; set; }

		public IList<Error> Errors { get; set; }

		#region IXhtmlAnchorSite Members

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

		#endregion

		public IDisposable scope(IContentModel element)
		{
			return IXhtmlAnchorSiteExtensions.scope(this, element);
		}
	}
}