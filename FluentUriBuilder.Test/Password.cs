﻿using System;
using FluentAssertions;
using Xunit;

namespace FluentUri.Test
{
    public class Password
    {
        [Fact]
        public void UserNameCannotBeNullOrWhiteSpace()
        {
            FluentUriBuilder.Create()
                .Invoking(b => b.Credentials(null, "password"))
                .Should().Throw<ArgumentException>();
            FluentUriBuilder.Create()
                .Invoking(b => b.Credentials(string.Empty, "password"))
                .Should().Throw<ArgumentException>();
            FluentUriBuilder.Create()
                .Invoking(b => b.Credentials(" ", "password"))
                .Should().Throw<ArgumentException>();
        }

        [Fact]
        public void PasswordCannotBeNullOrWhiteSpace()
        {
            FluentUriBuilder.Create()
                .Invoking(b => b.Credentials("user", null))
                .Should().Throw<ArgumentException>();
            FluentUriBuilder.Create()
                .Invoking(b => b.Credentials(string.Empty, "user"))
                .Should().Throw<ArgumentException>();
            FluentUriBuilder.Create()
                .Invoking(b => b.Credentials(" ", "user"))
                .Should().Throw<ArgumentException>();
        }

        [Fact]
        public void CredentialsAreUsedIfSpecified()
        {
            var user = "user";
            var password = "password";
            var expectedUserInfo = "user:password";
            var exampleUriWithoutCredentials = 
                "http://example.com:888/path/to?somekey=some%2bvalue&otherkey=some%2bvalue#fragment";

            FluentUriBuilder.From(exampleUriWithoutCredentials)
                .Credentials(user, password)
                .ToUri()
                .UserInfo
                .Should().Be(expectedUserInfo);
        }

        [Fact]
        public void ExistingCredentialsAreUpdated()
        {
            var user = "new-user";
            var password = "new-password";
            var expectedUserInfo = "new-user:new-password";

            FluentUriBuilder.From(TestData.Uri)
                .Credentials(user, password)
                .ToUri()
                .UserInfo
                .Should().Be(expectedUserInfo);
        }

        [Fact]
        public void ExistingCredentialsAreDeletedIfRemoveCredentialsIsCalled()
        {
            FluentUriBuilder.From(TestData.Uri)
                .RemoveCredentials()
                .ToUri()
                .UserInfo
                .Should().Be(string.Empty);
        }
    }
}
