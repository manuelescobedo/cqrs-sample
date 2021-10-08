using Microsoft.Extensions.Logging;
using Moq;
using System;

namespace Test.Doubles
{
     
    public class MockLogger<TState> : Mock<ILogger<TState>> where TState : class
    {
        public MockLogger()
        {
            Setup(x =>
                x.Log(
                    It.IsAny<LogLevel>(),
                    It.IsAny<EventId>(),
                    It.IsAny<object>(),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<object, Exception, string>>()
                )
            );
        }
    }
}
