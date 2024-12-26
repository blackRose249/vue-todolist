using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Contexts;
using WebApplication1.Repository;
using WebApplication1.Services;
using WebApplication1.Interceptor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", policy =>
    {
        policy.WithOrigins("http://localhost:8080")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// dbConnect
builder.Services.AddDbContext<TodoLIstDbContext>(options =>
{
    options.UseMySql("server=localhost;port=3306;database=TodoList_Root;uid=root;pwd=011007;CharSet=utf8",
    new MySqlServerVersion("5.7.44"));
});
// when requesing,new Instance;
//builder.Services.AddScoped<ITodoRepository, TodoRepository>();
//builder.Services.AddScoped<ITodoListService, TodoListService>();

// 1,use autofac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
// 2,reg autofac's service containers 
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    // 1.InstancePerDependency() 默?是瞬?生命周期：?次?求都会?建一个新的?例
    containerBuilder.RegisterType<TodoListService>().As<ITodoListService>().InstancePerDependency().EnableInterfaceInterceptors().InterceptedBy(typeof(MyInterceptor));
    containerBuilder.RegisterType<TodoRepository>().As<ITodoRepository>().InstancePerDependency().EnableInterfaceInterceptors().InterceptedBy(typeof(MyInterceptor));
    // 2.SingleInstance() ?例生命周期:容器会?建一个唯一的?例，并在整个?用程序生命周期内共享??例
    //containerBuilder.RegisterType<TodoListService>().As<ITodoListService>().SingleInstance().EnableInterfaceInterceptors().InterceptedBy(typeof(MyInterceptor));
    //containerBuilder.RegisterType<TodoRepository>().As<ITodoRepository>().SingleInstance().EnableInterfaceInterceptors().InterceptedBy(typeof(MyInterceptor));
    // 3.InstancePerDependency() 作用域生命周期:?个?求或?个操作?元的生命周期中
    //containerBuilder.RegisterType<TodoListService>().As<ITodoListService>().InstancePerLifetimeScope().EnableInterfaceInterceptors().InterceptedBy(typeof(MyInterceptor));
    //containerBuilder.RegisterType<TodoRepository>().As<ITodoRepository>().InstancePerLifetimeScope().EnableInterfaceInterceptors().InterceptedBy(typeof(MyInterceptor));
    // reg interceptor
    containerBuilder.RegisterType<MyInterceptor>();
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Cors
app.UseCors("AllowLocalhost");

app.MapControllers();

app.Run();