using System;
using NUnit.Framework;
using Shouldly;
using Trackwane.Framework.Common.Configuration;

namespace Trackwane.Tests.Behavior.Engine.Application
{
    internal class Register_Application_Tests : Scenario
    {
        [Test]
        public void Succeeds_When_Valid_Secret_Key_Provided()
        {
            var newAppKey = GenerateKey();
            Client = SetupClient(newAppKey);
            Register_Application.With(new PlatformConfig().SecretKey, GenerateKey(), "email@nowhere.com", "my password", "Application Creator");
        }

        [Test]
        public void Fails_When_Invalid_Secret_Key_Provided()
        {
            Should.Throw<Exception>(() =>
            {
                var newAppKey = GenerateKey();
                Client = SetupClient(newAppKey);
                Register_Application.With("sdfsdfsdfsdfsdf", GenerateKey(), "email@nowhere.com", "my password", "Application Creator");
            });
        }

        [Test]
        public void Fails_When_Application_Key_Already_In_Use()
        {
            Should.Throw<Exception>(() =>
            {
                var newAppKey = GenerateKey();
                Client = SetupClient(newAppKey);
                Register_Application.With(new PlatformConfig().SecretKey, GenerateKey(), "email@nowhere.com", "my password", "Application Creator");
                Register_Application.With(new PlatformConfig().SecretKey, GenerateKey(), "email@nowhere.com", "my password", "Application Creator");
            });
        }
    }
}
