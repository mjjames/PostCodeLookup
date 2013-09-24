using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AssertExLib;
using MKS.PostcodeLookupService;
using Newtonsoft.Json;
using Xunit;

namespace PostcodeLookup.Tests
{
    public class PostcodeAnywhereUKAddressTests
    {
        private const string TestPostCode = "WR2 6NJ"; //Dev Test PostCode suitable for repetitive testing
        private const string ApiKey = "GZ99-TZ39-XR31-UM99"; //Dev Test API Key - expires monthly renew accordling

        [Fact]
        public async Task TestPostcodeReturnsResults()
        {
            var service = new PostcodeAnywhereUKPostcodeLookup(ApiKey);
            var result = await service.LookupAsync(TestPostCode);
            Assert.NotEmpty(result);
        }

        [Fact]
        public void TestPostcodeDoesntThrowWebException()
        {
            var service = new PostcodeAnywhereUKPostcodeLookup(ApiKey);
            AssertEx.TaskDoesNotThrow<WebException>(async () => await service.LookupAsync(TestPostCode));
        }

        [Fact]
        public void TestPostcodeDoesntThrowJsonReaderException()
        {
            var service = new PostcodeAnywhereUKPostcodeLookup(ApiKey);
            AssertEx.TaskDoesNotThrow<JsonReaderException>(async () => await service.LookupAsync(TestPostCode));
        }

        [Fact]
        public void TestPostcodeDoesntThrowJsonSerialisationException()
        {
            var service = new PostcodeAnywhereUKPostcodeLookup(ApiKey);
            AssertEx.TaskDoesNotThrow<JsonSerializationException>(async () => await service.LookupAsync(TestPostCode));
        }

        [Fact]
        public void TestPostcodeDoesntThrowAnyException()
        {
            var service = new PostcodeAnywhereUKPostcodeLookup(ApiKey);
            AssertEx.TaskDoesNotThrow<AggregateException>(async () => await service.LookupAsync(TestPostCode));
        }

        [Fact]
        public async Task TestPostcodeReturnsFirstAddressLine()
        {
            var service = new PostcodeAnywhereUKPostcodeLookup(ApiKey);
            var result = await service.LookupAsync(TestPostCode);
            var address = result.First();
            Assert.Equal("Moseley Road", address.Address);
        }

        [Fact]
        public async Task TestPostcodeReturnsCityAddressLine()
        {
            var service = new PostcodeAnywhereUKPostcodeLookup(ApiKey);
            var result = await service.LookupAsync(TestPostCode);
            var address = result.First();
            Assert.Equal("Worcester", address.City);
        }

        [Fact]
        public async Task TestPostcodeReturnsCountyAddressLine()
        {
            var service = new PostcodeAnywhereUKPostcodeLookup(ApiKey);
            var result = await service.LookupAsync(TestPostCode);
            var address = result.First();
            Assert.Equal("Worcestershire", address.County);
        }

        [Fact]
        public async Task TestPostcodeReturnsPostcodeAddressLine()
        {
            var service = new PostcodeAnywhereUKPostcodeLookup(ApiKey);
            var result = await service.LookupAsync(TestPostCode);
            var address = result.First();
            Assert.Equal("WR2 6NJ", address.PostCode);
        }

        [Fact]
        public async Task TestPostcodeReturnsTownAddressLine()
        {
            var service = new PostcodeAnywhereUKPostcodeLookup(ApiKey);
            var result = await service.LookupAsync(TestPostCode);
            var address = result.First();
            Assert.Equal("Hallow", address.Town);
        }
    }
}
