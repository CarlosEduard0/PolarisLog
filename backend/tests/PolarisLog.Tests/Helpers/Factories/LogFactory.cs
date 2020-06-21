using System;
using Bogus;
using PolarisLog.Domain.Entities;

namespace PolarisLog.Tests.Helpers.Factories
{
    public class LogFactory
    {
        public static Log GerarLog()
        {
            return new Faker<Log>("pt_BR").CustomInstantiator(faker => new Log(
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                faker.Lorem.Word(),
                faker.Lorem.Sentence(),
                faker.Internet.Ip()));
        }
    }
}