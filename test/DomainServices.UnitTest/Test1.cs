namespace DomainServices.UnitTest;

public class Test1
{
    [Fact]
    public void TestCase1()
    {
        //Arrange
        ICaffService service = new CaffService();
        string expectedResponse = "pong";

        //Act
        var response = service.Ping();

        //Assert
        response.Should().Be(expectedResponse);
    }
}