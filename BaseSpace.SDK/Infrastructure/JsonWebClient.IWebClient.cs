﻿using System;
using System.IO;
using Common.Logging;
using Illumina.BaseSpace.SDK.ServiceModels;
using ServiceStack.ServiceClient.Web;

namespace Illumina.BaseSpace.SDK
{
    public partial class JsonWebClient : IWebClient
    {
		public void SetDefaultRequestOptions(IRequestOptions options)
        {
            DefaultRequestOptions = options;
        }

		public TReturn Send<TReturn>(AbstractRequest<TReturn> request, IRequestOptions options = null)
			where TReturn : class
		{
			return WrapResult<TReturn>(request, options ?? DefaultRequestOptions, logger, DefaultRequestOptions.RetryAttempts);
		}

		private TReturn WrapResult<TReturn>(AbstractRequest<TReturn> request, IRequestOptions options, ILog logger, uint maxRetry)
			where TReturn : class
		{
			try
			{
				TReturn result = null;
				RetryLogic.DoWithRetry(maxRetry, request.GetName(), () => 
				{
					result = request.GetFunc(client, options)();
				}
			, logger);
				return result;
			}
			catch (WebServiceException wex)
			{
				throw new BaseSpaceException<TReturn>(request.GetName() + " failed", wex);
			}
		}

        public TReturn Send<TReturn>(HttpMethods httpMethod, string relativeOrAbsoluteUrl, object request,
                                         IRequestOptions options = null)
            where TReturn : class
        {
			//var rr = new RestRequest<TReturn>
			//{
			//	Method = httpMethod,
			//	RelativeOrAbsoluteUrl = relativeOrAbsoluteUrl,
			//	Request = request,
			//	Options = options ?? DefaultRequestOptions,
			//	Name = string.Format("{0} request to {1} ", httpMethod, relativeOrAbsoluteUrl)
			//};

			//return rr.Send(client, logger);

	        throw new NotImplementedException();
        }


        public TReturn PostFileWithRequest<TReturn>(string relativeOrAbsoluteUrl, FileInfo fileToUpload,
                                                        object request,
                                                        IRequestOptions options = null) 
            where TReturn : class
        {
			//var rr = new FileRestRequest<TReturn>
			//{
			//	Method = HttpMethods.PUT,
			//	RelativeOrAbsoluteUrl = relativeOrAbsoluteUrl,
			//	Request = request,
			//	FileInfo = fileToUpload,
			//	Options = options ?? DefaultRequestOptions,
			//	Name = string.Format("File Put request to {0} for file {1}", relativeOrAbsoluteUrl, fileToUpload.FullName)
			//};

			//return rr.Send(client, logger);

			throw new NotImplementedException();
        }

        public TReturn PostFileWithRequest<TReturn>(string relativeOrAbsoluteUrl, Stream fileToUpload,
                                                        object request, string fileName,
                                                        IRequestOptions options = null)
            where TReturn : class
        {
			//var rr = new StreamingRestRequest<TReturn>
			//{
			//	Method = HttpMethods.PUT,
			//	RelativeOrAbsoluteUrl = relativeOrAbsoluteUrl,
			//	Request = request,
			//	Stream = fileToUpload,
			//	FileName = fileName,
			//	Options = options ?? DefaultRequestOptions,
			//	Name = string.Format("File put request to {0} from stream with file name {1} ", relativeOrAbsoluteUrl, fileName)
			//};

			//return rr.Send(client, logger);

			throw new NotImplementedException();
        }
    }
}
