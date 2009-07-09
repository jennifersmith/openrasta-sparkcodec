//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Collections.Specialized;
//using System.Globalization;
//using System.Linq;
//using System.Security.Principal;
//using System.Text;
//using Castle.MicroKernel;
//using Castle.MicroKernel.Resolvers;
//using OpenRasta.Data;
//using OpenRasta.DI;
//using OpenRasta.Reflection;
//using OpenRasta.TypeSystem;
//using OpenRasta.TypeSystem.ReflectionBased;
//using OpenRasta.Web;
//using OpenRasta.Web.Pipeline;

//namespace OpenRasta.TestingUtilities
//{
//    public static class TestUtilities
//    {
//        public static void SetupForTesting()
//        {
//            var resolver = new InternalDependencyResolver();
//            DependencyManager.SetResolver(resolver);
//            resolver.AddDependency(typeof(ICommunicationContext), typeof(StubbedCommunicationContext), DependencyLifetime.Singleton);
//            resolver.AddDependency(typeof(IUriResolver), typeof(StubbedUriResolver), DependencyLifetime.Singleton);
//        }

//    }
//    public static class ChangeSetStubs
//    {
//        public static ChangeSetBuilder<T> CreateChangeSet<T>(T instance) where T : class
//        {
//            TypeInstance typeInstance = new TypeInstance(new ReflectionBasedType(typeof (T)));
//            typeInstance.Changes = new Dictionary<string, IPropertyInstance>();
//            return new ChangeSet<T>(typeInstance);
//        }
//    }
//    public class ChangeSetBuilder
//    {
//        private readonly TypeInstance typeInstance;
//    }

//    public class StubbedCommunicationContext : ICommunicationContext
//    {
//        public Uri ApplicationBaseUri
//        {
//            get { return new Uri("http://localhost/");}
//        }

//        public IRequest Request
//        {
//            get { throw new NotImplementedException(); }
//        }

//        public IResponse Response
//        {
//            get { throw new NotImplementedException(); }
//        }

//        public OperationResult OperationResult
//        {
//            get { throw new NotImplementedException(); }
//            set { throw new NotImplementedException(); }
//        }

//        public PipelineData PipelineData
//        {
//            get { throw new NotImplementedException(); }
//        }

//        public IList<Error> ServerErrors
//        {
//            get { throw new NotImplementedException(); }
//        }

//        public IPrincipal User
//        {
//            get { throw new NotImplementedException(); }
//            set { throw new NotImplementedException(); }
//        }
//    }
//    public class StubbedUriResolver : IUriResolver
//    {
//        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
//        {
//            throw new NotImplementedException();
//        }

//        IEnumerator IEnumerable.GetEnumerator()
//        {
//            return GetEnumerator();
//        }

//        public void AddUriMapping(string uri, object resourceKey, CultureInfo uriCulture, string uriName)
//        {
//            throw new NotImplementedException();
//        }

//        public ResourceMatch Match(Uri uriToMatch)
//        {
//            throw new NotImplementedException();
//        }

//        public void Clear()
//        {
//            throw new NotImplementedException();
//        }

//        public Uri CreateUriFor(Uri baseAddress, object resourceKey, string uriName, NameValueCollection keyValues)
//        {
//            return new UriForSomething(resourceKey);
//        }
//    }

//    public static class HttpResultExtensionMethods
//    {
//        public static void ShouldBeRedirectTo(this OperationResult operationResult, object redirectResource)
//        {
//            operationResult.As<OperationResult.SeeOther>().RedirectLocation.ShouldEqual(redirectResource.CreateUri());	
//        }
//    }

//    public class UriForSomething: Uri, IEquatable<UriForSomething>
//    {
//        public object ResourceKey { get; set; }

//        public UriForSomething(object resourceKey) : base("http://localhost/thisisatest/" + resourceKey.GetHashCode())
//        {
//            ResourceKey = resourceKey;
//        }

//        public bool Equals(UriForSomething other)
//        {
//            if (ReferenceEquals(null, other)) return false;
//            if (ReferenceEquals(this, other)) return true;
//            return base.Equals(other) && Equals(other.ResourceKey, ResourceKey);
//        }

//        public override bool Equals(object obj)
//        {
//            if (ReferenceEquals(null, obj)) return false;
//            if (ReferenceEquals(this, obj)) return true;
//            return Equals(obj as UriForSomething);
//        }

//        public override int GetHashCode()
//        {
//            unchecked
//            {
//                {
//                    return (base.GetHashCode()*397) ^ (ResourceKey != null ? ResourceKey.GetHashCode() : 0);
//                }
//            }
//        }
//    }
//}
