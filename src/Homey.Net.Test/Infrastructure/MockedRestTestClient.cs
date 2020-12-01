using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Moq;

namespace Homey.Net.Test.Infrastructure
{
    public class MockedRestTestClient
    {
        private readonly Mock<IHomeyRestClient> _mock;
        private readonly Dictionary<string, string> _responseFiles;

        public MockedRestTestClient()
        {
            _responseFiles = new Dictionary<string, string>();
            _mock = new Mock<IHomeyRestClient>();

            _mock.Setup(m => m.RequestAsyncGet(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string endpoint, string token) => RequestAsyncGet(endpoint, token));

            _mock.Setup(m => m.RequestAsyncPut(It.IsAny<string>(), It.IsAny<object>(),It.IsAny<string>()))
                .Returns((string endpoint, object body, string token) => RequestAsyncGet(endpoint, token));
        }

        public IHomeyRestClient Object
        {
            get
            {
                return _mock.Object;
            }
        }

        public void AddResponseFile(string partialEndPoint, string fullResponseFilePath)
        {
            _responseFiles.Add(partialEndPoint, fullResponseFilePath);
        }
        
        private Task<RestResponseResult> RequestAsyncGet(string endpoint, string token)
        {
            string fileName = GetResponseFileName(endpoint);

            return Task.FromResult(new RestResponseResult
            {
                Code = HttpStatusCode.OK,
                Content = File.ReadAllText(fileName)
            });
        }

        private string GetResponseFileName(string endpoint)
        {
            foreach (KeyValuePair<string, string> file in _responseFiles)
            {
                if (endpoint.Contains(file.Key))
                {
                    return file.Value;
                }
            }

            throw new NotImplementedException($"No response file for request={endpoint} registered");
        }
    }
}
