using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;
using AccServerAdmin.Service.Middleware;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;

namespace AccServerAdmin.Tests.Service.Middleware
{
    [ExcludeFromCodeCoverage]
    public class ExceptionMiddlewareTests
    {
        [Test]
        public async Task Invokes()
        {
            // Arrange
            var request = Substitute.For<RequestDelegate>();
            var handler = new ExceptionMiddleware(request);
            var context = new DefaultHttpContext();

            // Act
            await handler.Invoke(context);

            // Assert
            await request.Received().Invoke(context);
        }

        [Test]
        public async Task HandlesInternalServerError()
        {
            // Arrange
            var request = Substitute.For<RequestDelegate>();
            var handler = new ExceptionMiddleware(request);
            var context = new DefaultHttpContext();
            var exc = new Exception("Test exception");

            request.Invoke(context).Throws(exc);

            // Act
            await handler.Invoke(context);

            // Assert
            Assert.That(context.Response.StatusCode, Is.EqualTo((int)HttpStatusCode.InternalServerError));
        }

        [Test]
        public async Task HandlesNotFound()
        {
            // Arrange
            var request = Substitute.For<RequestDelegate>();
            var handler = new ExceptionMiddleware(request);
            var context = new DefaultHttpContext();
            var exc = new KeyNotFoundException("Something not found");

            request.Invoke(context).Throws(exc);

            // Act
            await handler.Invoke(context);

            // Assert
            Assert.That(context.Response.StatusCode, Is.EqualTo((int)HttpStatusCode.NotFound));

        }
    }
}
