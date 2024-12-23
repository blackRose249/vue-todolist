using Castle.DynamicProxy;

namespace WebApplication1.Interceptor
{
    public class MyInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            Console.WriteLine($"Method {invocation.Method.Name} is called;Staring!");
            invocation.Proceed();
            Console.WriteLine($"Method {invocation.Method.Name} is called;Ending");
        }
    }
}
