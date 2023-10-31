using Bogus;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using FastEndpoints.Testing;
using Microsoft.AspNetCore.Http;

namespace Artema.Platform.Api.Integration.Tests.Endpoints;

// ================================================================ //
/**
 * This class is copied form FastEndpoints.Testing.TestFixture code,
 * and adapted for our purpose by removing the fixture cache
 * See: https://github.com/FastEndpoints/FastEndpoints/blob/main/Src/Testing/TestFixture.cs
 */
// ================================================================ //

public abstract class TestFixture<TProgram> : IAsyncLifetime, IFixture where TProgram : class
{
    private static readonly Faker _faker = new();
    
    public Faker Fake => _faker;

    public IServiceProvider Services => _app.Services;

    public TestServer Server => _app.Server;

    public HttpClient Client { get; set; }

    private readonly WebApplicationFactory<TProgram> _app;

    protected TestFixture(IMessageSink s)
    {
        _app = new WebApplicationFactory<TProgram>().WithWebHostBuilder(b =>
        {
            b.UseEnvironment("Testing");
            b.ConfigureLogging(l => l.ClearProviders().AddXUnit(s));
            b.ConfigureTestServices(ConfigureServices);
            ConfigureApp(b);
        });
        Client = _app.CreateClient();
    }

    protected virtual Task SetupAsync() => Task.CompletedTask;

    protected virtual Task TearDownAsync() => Task.CompletedTask;

    protected virtual void ConfigureApp(IWebHostBuilder a) { }

    protected virtual void ConfigureServices(IServiceCollection s) { }

    public HttpClient CreateClient(WebApplicationFactoryClientOptions? o = null) => CreateClient(_ => { }, o);

    public HttpClient CreateClient(Action<HttpClient> c, WebApplicationFactoryClientOptions? o = null)
    {
        var client = o is null ? _app.CreateClient() : _app.CreateClient(o);
        c(client);
        return client;
    }

    public HttpMessageHandler CreateHandler(Action<HttpContext>? c = null)
        => c is null ? _app.Server.CreateHandler() : _app.Server.CreateHandler(c);

    public Task InitializeAsync() => SetupAsync();

    public virtual Task DisposeAsync()
    {
        Client.Dispose();
        return TearDownAsync();
    }
}