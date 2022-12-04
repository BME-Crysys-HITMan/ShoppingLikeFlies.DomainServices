using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShoppingLikeFiles.DataAccessLogic.Context;
using ShoppingLikeFiles.DataAccessLogic.Entities;
using ShoppingLikeFiles.DomainServices.Service;
using System.Runtime.InteropServices;

namespace DomainServices.IntegrationTest.Scenario;

internal class DomainTestScenario
{
    private readonly IServiceProvider provider;

    public DomainTestScenario(
        ILogger logger,
        string generatorPath = "generator",
        string uploadPath = "upload",
        string dbName = "testDb")
    {
        var dic = new Dictionary<string, string>()
        {
            { "ConnectionStrings:DefaultConnection", "value" },
        };
        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(dic).Build();

        string v = "CAFF_Processor";
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            v += ".exe";
        }

        var validator = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "NativeFiles" + Path.DirectorySeparatorChar + v;
        Directory.CreateDirectory(Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + generatorPath);
        Directory.CreateDirectory(Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + uploadPath);
        generatorPath = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + generatorPath;
        uploadPath = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + uploadPath;
        IServiceCollection services = new ServiceCollection();
        services.AddScoped<ILogger>((x) => { return logger; });
        services.AddCaffProcessor
        (
            x => { x.Validator = validator; },
            y => { y.ShouldUploadToAzure = false; y.DirectoryPath = uploadPath; },
            z => { z.GeneratorDir = generatorPath; },
            config
        );

        //strip original dbcontext out of it's existence

        var c = services.FirstOrDefault(d => d.ServiceType == typeof(ShoppingLikeFliesDbContext));
        if (c is not null)
        {
            services.Remove(c);
            var options = services.Where(r => r.ServiceType == typeof(DbContextOptions)
                || (r.ServiceType.IsGenericType && r.ServiceType.GetGenericTypeDefinition() == typeof(DbContextOptions<>))).ToArray();

            foreach (var item in options)
            {
                services.Remove(item);
            }
        }

        services.AddDbContext<ShoppingLikeFliesDbContext>(c => c.UseInMemoryDatabase(dbName));

        provider = services.BuildServiceProvider();
        var context = provider.GetRequiredService<ShoppingLikeFliesDbContext>();
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
    }

    public async Task SeedDb(List<Caff> caffs)
    {
        var context = provider.GetRequiredService<ShoppingLikeFliesDbContext>();

        await context.Caff!.AddRangeAsync(caffs);

        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Gets the <see cref="IServiceProvider"/> from the scenario.
    /// </summary>
    public IServiceProvider Provider => provider;

    /// <summary>
    /// Creates a fresh <see cref="IDataService"/> instance from DI.
    /// </summary>
    public IDataService DataService => provider.GetRequiredService<IDataService>();

    /// <summary>
    /// Creates a new <see cref="ICaffService"/> instance from DI.
    /// </summary>
    public ICaffService CaffService => provider.GetRequiredService<ICaffService>();

    /// <summary>
    /// Creates a new <see cref="IPaymentService"/> instance from DI.
    /// </summary>
    public IPaymentService PaymentService => provider.GetRequiredService<IPaymentService>();
}
