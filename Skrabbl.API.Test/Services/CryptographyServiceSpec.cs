using System.Security.Policy;
using NUnit.Framework;
using Skrabbl.API.Services;

namespace Skrabbl.API.Test.Services
{
    public class CryptographyServiceSpec
    {
        private CryptographyService _service;
        
        [SetUp]
        public void Setup()
        {
            _service = new CryptographyService();
        }

        [Test]
        public void SaltLengthIsCorrect()
        {
            var salt = _service.CreateSalt();

            Assert.AreEqual(salt.Length, 16);
        }

        [TestCase("foo")]
        [TestCase("bar")]
        [TestCase("foobar")]
        public void HashesHaveCorrectLength(string input)
        {
            var salt = _service.CreateSalt();
            var hash = _service.GenerateHash(input, salt);
            
            Assert.AreEqual(hash.Length, 44);
        }

        [TestCase("foo")]
        [TestCase("bar")]
        [TestCase("foobar")]
        public void InputWithSameSaltsAreEqual(string input)
        {
            var salt = _service.CreateSalt();
            var hash = _service.GenerateHash(input, salt);
            var areEqual = _service.AreEqual(input, hash, salt);
            
            Assert.IsTrue(areEqual);
        }
        
        [TestCase("foo")]
        [TestCase("bar")]
        [TestCase("foobar")]
        public void InputWithDifferentHashesArentEqual(string input)
        {
            var salt = _service.CreateSalt();
            var secondSalt = _service.CreateSalt();
            var hash = _service.GenerateHash(input, salt);
            var areEqual = _service.AreEqual(input, hash, secondSalt);
            
            Assert.IsFalse(areEqual);
        }
    }
}