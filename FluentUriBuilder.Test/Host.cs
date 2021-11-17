using System;
using FluentAssertions;
using Xunit;

namespace FluentUri.Test
{
    public class Host
    {
        [Fact]
        public void HostCannotBeNullOrWhiteSpace()
        {
            FluentUriBuilder.Create()
                .Invoking(b => b.Host(null))
                .Should().Throw<ArgumentException>();
            FluentUriBuilder.Create()
                .Invoking(b => b.Host(string.Empty))
                .Should().Throw<ArgumentException>();
            FluentUriBuilder.Create()
                .Invoking(b => b.Host(" "))
                .Should().Throw<ArgumentException>();
        }

        [Fact]
        public void HostIsUsedIfSpecified()
        {
            var host = "test.example.com";

            FluentUriBuilder.Create()
                .Host(host)
                .ToUri()
                .Host
                .Should().Be(host);
        }

        [Fact]
        public void ExistingHostIsUpdated()
        {
            var host = "subdomain.domain.hu";

            FluentUriBuilder.From(TestData.Uri)
                .Host(host)
                .ToUri()
                .Host
                .Should().Be(host);
        }
    }
}
