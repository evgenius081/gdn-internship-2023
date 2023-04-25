using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Tests
{
    /// <summary>
    /// Mock DelegatingHandler for testing purposes.
    /// </summary>
    public class DelegatingHandlerStub : DelegatingHandler
    {
        private readonly Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> handlerFunc;

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegatingHandlerStub"/> class.
        /// </summary>
        public DelegatingHandlerStub()
        {
            this.handlerFunc = (request, cancellationToken) => Task.FromResult(request.CreateResponse(HttpStatusCode.OK));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegatingHandlerStub"/> class.
        /// </summary>
        /// <param name="handlerFunc">Function that will handle requests.</param>
        public DelegatingHandlerStub(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> handlerFunc)
        {
            this.handlerFunc = handlerFunc;
        }

        /// <inheritdoc />
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return this.handlerFunc(request, cancellationToken);
        }
    }
}
