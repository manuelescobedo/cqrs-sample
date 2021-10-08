using MediatR;
using Moq;
using System.Threading;
using System.Threading.Tasks;

namespace Test.Doubles
{
    public class MockMediator : Mock<IMediator>
    {
        public MockMediator()
        {
            Setup(m => m.Publish(It.IsAny<object>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
        }

        public void MockSend<TRequest, TResponse>(TResponse dummy) 
            where TRequest: IRequest<TResponse>
            where TResponse : class, new()
        {
            Setup(m => m.Send(It.IsAny<TRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(dummy);
        }

        public void MockSend<TRequest>()
            where TRequest : IRequest
        {
            Setup(m => m.Send(It.IsAny<TRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(Unit.Value);
        }

    }
}
