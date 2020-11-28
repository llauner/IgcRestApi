using IgcRestApi.Services;
using NFluent;
using NUnit.Framework;

namespace IgcRestApi.UnitTests
{
    class AggregatorServiceTests : BaseUnitTest
    {
        private IFirestoreService _firestoreService;
        private IFtpService _ftpService;
        private IStorageService _storageService;
        private IIgcReaderService _igcReaderService;


        public AggregatorServiceTests()
        {
        }

        [OneTimeSetUp]
        public void Setup()
        {
            _firestoreService = new FirestoreService(ConfigurationService);
            _ftpService = new FtpService(ConfigurationService);
            _storageService = new StorageService(ConfigurationService);
            _igcReaderService = new IgcReaderService();

        }


        [Test]
        //[Ignore("Used for dev only")]
        public void Aggregate_Ok()

        {
            // Arrange
            var aggregatorService = new AggregatorService(UnitTestLoggerFactory, ConfigurationService, _ftpService, _firestoreService, _storageService, _igcReaderService);

            // Act
            aggregatorService.Run();

            // Assert
            Check.That(true);

        }

    }
}
