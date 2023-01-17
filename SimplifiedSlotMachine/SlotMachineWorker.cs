using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SimplifiedSlotMachine.Models;
using SimplifiedSlotMachine.Services;
using SimplifiedSlotMachine.Services.Validation;

namespace SimplifiedSlotMachine
{
    public class SlotMachineWorker : BackgroundService
    {
        private readonly ILogger<SlotMachineWorker> _logger;
        private readonly IHost _host;
        private readonly ISlotService _slotService;
        private readonly IBalanceService _balanceService;
        private readonly IValidationService _validationService;
        private readonly IConfiguration _config;

        public SlotMachineWorker(ILogger<SlotMachineWorker> logger, IHost host, ISlotService slotService, IBalanceService balanceService, IConfiguration config, IValidationService validationService)
        {
            _logger = logger;
            _host = host;
            _slotService = slotService;
            _balanceService = balanceService;
            _config = config;
            _validationService = validationService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            User user = new User() { UserID = Guid.NewGuid() };

            _slotService.Play(user);

            //could add storage for user in the future such as a database to store balance and other information

            await _host.StopAsync();
        }
    }
}
