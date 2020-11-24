using NUnit.Framework;
using IgcRestApi.Services;
using NFluent;
using System;

namespace IgcRestApi.UnitTests
{
    public class IgcReaderServiceTests
    {
        [SetUp]
        public void Setup()
        {
            // Method intentionally left empty.
        }

        [Test]
        public void ReadDateFromFile_Ok()
        {
            // Arrange
            var igcReader = new IgcReaderService();

            // Act 
            var header = igcReader.GetHeader("data/06OV89C1.igc");

            // Assert
            Check.That(header.Date).IsEqualTo(new DateTimeOffset(new DateTime(20, 06, 24)));
        }
    }
}