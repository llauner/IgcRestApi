using AutoMapper;
using IgcRestApi.DataConversion;
using NUnit.Framework;

namespace IgcRestApi.UnitTests.Dto
{
    [TestFixture]
    public class IgcRestApiDataConverterConfigurationTests
    {
        private IDataConverter _dataConverter;

        [OneTimeSetUp]
        public void InitOneTime()
        {
            var mapperConfiguration = new MapperConfiguration(IgcRestApiDataConverterConfiguration.ConfigureMapping);
            var mapper = mapperConfiguration.CreateMapper();
            _dataConverter = new AutoMapperDataConverter(mapper);
        }

        [Test]
        public void ConfigureMapping_IsValid()
        {
            _dataConverter.AssertConfigurationIsValid();
        }


    }
}
