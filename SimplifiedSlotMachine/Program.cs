using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SimplifiedSlotMachine;
using SimplifiedSlotMachine.Services;
using SimplifiedSlotMachine.Services.Validation;

var configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .Build();

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
        services.AddHostedService<SlotMachineWorker>()
                .AddTransient<IBalanceService, BalanceService>()
                .AddTransient<ISlotService, SlotService>()
                .AddTransient<IValidationService, ValidationService>()
                .AddSingleton(configuration))
        .ConfigureLogging((hostContext, logging) => 
        logging.AddConsole()
               .SetMinimumLevel(LogLevel.Error))
        .Build();

host.Run();