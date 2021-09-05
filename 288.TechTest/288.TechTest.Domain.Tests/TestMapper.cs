using AutoMapper;

namespace _288.TechTest.Domain.Tests
{
    public class TestMapper
    {
        public static MapperConfiguration Configure()
        {
            return new MapperConfiguration(config => {
                config.AddProfile<DomainDataMappingProfile>();
            });
        }
    }
}
