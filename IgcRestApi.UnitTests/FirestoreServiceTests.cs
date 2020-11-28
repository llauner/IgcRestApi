using IgcRestApi.Services;
using NFluent;
using NUnit.Framework;

namespace IgcRestApi.UnitTests
{
    class FirestoreServiceTests : BaseUnitTest
    {
        [OneTimeSetUp]
        public void Setup()
        {
            // Arrange Tests
        }

        [Test]
        public void GetLastProcesseFileName_Ok()
        {
            // Arrange
            var firestoreService = new FirestoreService(ConfigurationService);

            // Act
            var lastProcessedFileName = firestoreService.GetLastProcessedFile();

            // Assert
            Check.That(lastProcessedFileName).IsNotEmpty();
            Check.That(lastProcessedFileName).IsNotNull();
        }

    }
}
