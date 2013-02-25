using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MKS.PostcodeLookupService;
using MKS.PostcodeLookupService.Interfaces;
using Moq;
using Xunit;

namespace PostcodeLookup.Tests
{
    public class PostcodeServiceTests
    {
        [Fact]
        public void EmptyPostCodeThrowsArgumentException()
        {
            var mockService = new Mock<IPostcodeLookup>();

            new PostcodeService(mockService.Object).LookupPostcode("").ContinueWith((result =>
                {
                    Assert.True(result.IsFaulted);
                    Assert.IsType<ArgumentException>(result.Exception.Flatten().InnerException);
                })).Wait();

        }

        [Fact]
        public void NullPostCodeThrowsArgumentException()
        {
            var mockService = new Mock<IPostcodeLookup>();

            new PostcodeService(mockService.Object).LookupPostcode(null).ContinueWith((result =>
            {
                Assert.True(result.IsFaulted);
                Assert.IsType<ArgumentException>(result.Exception.Flatten().InnerException);
                Assert.Equal("invalid postCode provided", result.Exception.Flatten().InnerException.Message);
            })).Wait();


        }

        [Fact]
        public void PostCodeTooShortThrowsArgumentException()
        {
            var mockService = new Mock<IPostcodeLookup>();

            new PostcodeService(mockService.Object).LookupPostcode("12").ContinueWith((result =>
        {
            Assert.True(result.IsFaulted);
            Assert.IsType<ArgumentException>(result.Exception.Flatten().InnerException);
            Assert.Equal("invalid postCode provided", result.Exception.Flatten().InnerException.Message);
        })).Wait();

        }

        [Fact]
        public void PostCodeTooLongThrowsArgumentException()
        {
            var mockService = new Mock<IPostcodeLookup>();

            new PostcodeService(mockService.Object).LookupPostcode("12345678")
                .ContinueWith((result =>
                {
                    Assert.True(result.IsFaulted);
                    Assert.IsType<ArgumentException>(result.Exception.Flatten().InnerException);
                    Assert.Equal("invalid postCode provided", result.Exception.Flatten().InnerException.Message);
                })).Wait();
        }

        [Fact]
        public void PostCodeWithWhiteSpaceMakesStringToLongDoesntThrowArgumentException()
        {
            var mockService = new Mock<IPostcodeLookup>();
            new PostcodeService(mockService.Object).LookupPostcode("AA9A 9AA");
        }

        [Fact]
        public void PostCodeAlphaAlphaDigitAlphaDigitAlphaAlphaFormatWithWhitespaceDoesntThrow()
        {
            var mockService = new Mock<IPostcodeLookup>();
            new PostcodeService(mockService.Object).LookupPostcode("AA9A 9AA");
        }

        [Fact]
        public void PostCodeAlphaDigitAlphaDigitAlphaAlphaFormatWithWhitespaceDoesntThrow()
        {
            var mockService = new Mock<IPostcodeLookup>();
            new PostcodeService(mockService.Object).LookupPostcode("A9A 9AA");
        }

        [Fact]
        public void PostCodeAlphaDigitDigitAlphaAlphaFormatWithWhitespaceDoesntThrow()
        {
            var mockService = new Mock<IPostcodeLookup>();
            new PostcodeService(mockService.Object).LookupPostcode("A9 9AA");
        }

        [Fact]
        public void PostCodeAlphaDigitDigitDigitAlphaAlphaFormatWithWhitespaceDoesntThrow()
        {
            var mockService = new Mock<IPostcodeLookup>();
            new PostcodeService(mockService.Object).LookupPostcode("A99 9AA");
        }
        [Fact]
        public void PostCodeAlphaAlphaDigitDigitAlphaAlphaFormatWithWhitespaceDoesntThrow()
        {
            var mockService = new Mock<IPostcodeLookup>();
            new PostcodeService(mockService.Object).LookupPostcode("AA9 9AA");
        }

        [Fact]
        public void PostCodeAlphaAlphaDigitDigitDigitAlphaAlphaFormatWithWhitespaceDoesntThrow()
        {
            var mockService = new Mock<IPostcodeLookup>();
            new PostcodeService(mockService.Object).LookupPostcode("AA99 9AA");
        }

        [Fact]
        public void PostCodeWithWhiteSpaceMakesStringToLongDoesntThrowWithWhitespaceArgumentException()
        {
            var mockService = new Mock<IPostcodeLookup>();
            new PostcodeService(mockService.Object).LookupPostcode("AA9A 9AA");
        }

        [Fact]
        public void PostCodeAlphaAlphaDigitAlphaDigitAlphaAlphaFormatWithoutWhitespaceDoesntThrow()
        {
            var mockService = new Mock<IPostcodeLookup>();
            new PostcodeService(mockService.Object).LookupPostcode("AA9A9AA");
        }

        [Fact]
        public void PostCodeAlphaDigitAlphaDigitAlphaAlphaFormatWithoutWhitespaceDoesntThrow()
        {
            var mockService = new Mock<IPostcodeLookup>();
            new PostcodeService(mockService.Object).LookupPostcode("A9A9AA");
        }

        [Fact]
        public void PostCodeAlphaDigitDigitAlphaAlphaFormatWithoutWhitespaceDoesntThrow()
        {
            var mockService = new Mock<IPostcodeLookup>();
            new PostcodeService(mockService.Object).LookupPostcode("A99AA");
        }

        [Fact]
        public void PostCodeAlphaDigitDigitDigitAlphaAlphaFormatWithoutWhitespaceDoesntThrow()
        {
            var mockService = new Mock<IPostcodeLookup>();
            new PostcodeService(mockService.Object).LookupPostcode("A999AA");
        }
        [Fact]
        public void PostCodeAlphaAlphaDigitDigitAlphaAlphaFormatWithoutWhitespaceDoesntThrow()
        {
            var mockService = new Mock<IPostcodeLookup>();
            new PostcodeService(mockService.Object).LookupPostcode("AA99AA");
        }

        [Fact]
        public void PostCodeAlphaAlphaDigitDigitDigitAlphaAlphaFormatWithoutWhitespaceDoesntThrow()
        {
            var mockService = new Mock<IPostcodeLookup>();
            new PostcodeService(mockService.Object).LookupPostcode("AA999AA");
        }

        [Fact]
        public void PostCodeNonValidFormatThrowsArgumentException()
        {
            var mockService = new Mock<IPostcodeLookup>();
             new PostcodeService(mockService.Object).LookupPostcode("1234567")
                 .ContinueWith((result =>
                 {
                     Assert.True(result.IsFaulted);
                     Assert.IsType<ArgumentException>(result.Exception.Flatten().InnerException);
                     Assert.Equal("postCode has invalid format", result.Exception.Flatten().InnerException.Message);
                 })).Wait();
        }
    }
}