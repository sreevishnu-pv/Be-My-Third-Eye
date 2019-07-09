using Microsoft.Azure.KeyVault;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using ThirdEye.Cognitive.Image.Tag;
using ThirdEye.Infrastructure;
using ThirdEye.Infrastructure.Helpers;

namespace ThirdEye.Console.Infrastructure
{
    class Bootstraper
    {
        public static ServiceProvider ServiceProvider { get; private set; }
        public static void Initialize()
        {
            //setup our DI
            var serviceCollection = new ServiceCollection();

            InitializeAppsettings(serviceCollection);
            InitializeDependencies(serviceCollection);

            ServiceProvider = serviceCollection
                .BuildServiceProvider();
        }

        private static void InitializeDependencies(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IHttpHelper, HttpHelper>();
            serviceCollection.AddSingleton<IKeyVaultHelper, KeyVaultHelper>();
            serviceCollection.AddScoped<IImageTagger, ImageTagger>();          

        }

        private static void InitializeAppsettings(IServiceCollection serviceCollection)
        {
            var builder = new ConfigurationBuilder()
                           .SetBasePath(Directory.GetCurrentDirectory())
                           .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            serviceCollection.Configure<AppSettings>(configuration);
        }

    }
}
