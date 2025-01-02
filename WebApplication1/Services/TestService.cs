namespace WebApplication1.Services
{
    public class TestService : ITransientTestService, IScopedTestService, ISingletonTestService
    {
        private readonly string _id;
        public TestService()
        {
            _id = Guid.NewGuid().ToString();
        }

        public string GetId() => _id;
    }
}
