using Serilog;

namespace DomainServices.UnitTest.Scenarios;

internal class CaffValidationScenario
{
    private readonly ICaffValidator validator;
    private readonly Mock<INativeCommunicator> mockCommunicator;

    public CaffValidationScenario(string returnString, string fileName, ILogger logger)
    {
        mockCommunicator = new Mock<INativeCommunicator>();
        mockCommunicator.Setup(x => x.Communicate(It.Is<string>(s => s.Contains(fileName)))).Returns(returnString);
        mockCommunicator.Setup(x => x.CommunicateAsync(It.Is<string>(s => s.Contains(fileName)))).Returns(Task.FromResult<string?>(returnString));
        mockCommunicator.Setup(x => x.Communicate(It.Is<string>(s => !s.Contains(fileName)))).Returns(string.Empty);
        mockCommunicator.Setup(x => x.CommunicateAsync(It.Is<string>(s => !s.Contains(fileName)))).Returns(Task.FromResult<string?>(null));

        validator = new DefaultCaffValidator(mockCommunicator.Object, logger);
    }

    public ICaffValidator Validator => validator;
    public Mock<INativeCommunicator> Mock => mockCommunicator;
}
