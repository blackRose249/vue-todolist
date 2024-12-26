namespace WebApplication1.Test
{
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;
    using System;
    using Autofac.Core;

    public interface ITodoListService { }
    public class TodoListService : ITodoListService { }

    public interface ITodoRepository { }
    public class TodoRepository : ITodoRepository { }

    public class DependencyInjectionTests
    {
        //  验证 Transient 生命周期
        [Fact]
        public void TestTransientLifetime()
        {
            // Arrange
            var serviceProvider = new ServiceCollection()
                .AddTransient<ITodoListService, TodoListService>()
                .AddTransient<ITodoRepository, TodoRepository>()
                .BuildServiceProvider();

            // Act
            var service1 = serviceProvider.GetRequiredService<ITodoListService>();
            var service2 = serviceProvider.GetRequiredService<ITodoListService>();

            // Assert
            Assert.NotSame(service1, service2); // 期望是不同的实例
        }

        // 验证 Singleton 生命周期
        [Fact]
        public void TestSingletonLifetime()
        {
            // Arrange
            var serviceProvider = new ServiceCollection()
                .AddSingleton<ITodoListService, TodoListService>()
                .AddSingleton<ITodoRepository, TodoRepository>()
                .BuildServiceProvider();

            // Act
            var service1 = serviceProvider.GetRequiredService<ITodoListService>();
            var service2 = serviceProvider.GetRequiredService<ITodoListService>();

            // Assert
            Assert.Same(service1, service2); // 期望是相同的实例
        }

        // 验证 Scoped 生命周期
        [Fact]
        public void TestScopedLifetime()
        {
            //ServiceCollection 是 .NET Core 和 .NET 5+ 中依赖注入的核心容器，它用于注册服务并配置它们的生命周期
            // Arrange
            var serviceProvider = new ServiceCollection()
                .AddScoped<ITodoListService, TodoListService>()
                .AddScoped<ITodoRepository, TodoRepository>()
                .BuildServiceProvider();

            // Act & Assert
            using (var scope1 = serviceProvider.CreateScope())
            {
                var service1 = scope1.ServiceProvider.GetRequiredService<ITodoListService>();
                var service2 = scope1.ServiceProvider.GetRequiredService<ITodoListService>();

                // 期望同一作用域内的实例是相同的
                Assert.Same(service1, service2);
            }

            using (var scope2 = serviceProvider.CreateScope())
            {
                var service3 = scope2.ServiceProvider.GetRequiredService<ITodoListService>();
                var service4 = scope2.ServiceProvider.GetRequiredService<ITodoListService>();

                // 期望同作用域内的实例是相同的,service3和service1则不相同
                Assert.Same(service3, service4);
            }
        }
    }

}
