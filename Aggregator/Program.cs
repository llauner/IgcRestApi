using IgcRestApi.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace Aggregator
{
    class Program
    {
        private readonly IFirestoreService _firestoreService;
        private readonly IFtpService _ftpService;
        private readonly IStorageService _storageService;
        private readonly IIgcReaderService _igcReaderService;
        protected readonly IConfigurationService ConfigurationService = new ConfigurationService(InitConfiguration());
        protected static readonly ILoggerFactory UnitTestLoggerFactory = LoggerFactory.Create(builder => builder.AddConsole().AddDebug());

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var program = new Program();
            program.main();
        }

        public Program()
        {
            System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "D:\\llauner\\GoogleCloud_Credentials\\Service_Account_Key-igcheatmap-f012be117f9c.json");

            _firestoreService = new FirestoreService(ConfigurationService);
            _ftpService = new FtpService(ConfigurationService);
            _storageService = new StorageService(ConfigurationService);
            _igcReaderService = new IgcReaderService();
        }

        public void main()
        {



            // Arrange
            var aggregatorService = new AggregatorService(UnitTestLoggerFactory, ConfigurationService, _ftpService, _firestoreService, _storageService, _igcReaderService);

            // Act
            aggregatorService.RunAsync();

            // Assert


        }



        /// <summary>
        /// IConfiguration
        /// Get configuration from json file
        /// </summary>
        /// <returns></returns>
        protected static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();
            return config;
        }


    }
}
