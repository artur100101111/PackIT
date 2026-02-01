using Microsoft.Extensions.Logging;
using PackIt.Shared.Abstractions.Commands;

namespace PackIT.Infrastructure.Logging
{
    internal class LoggingCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand> where TCommand : class, ICommand
    {
        public LoggingCommandHandlerDecorator(ICommandHandler<TCommand> commandHandler, ILogger<LoggingCommandHandlerDecorator<TCommand>> logger)
        {
            _commandHandler = commandHandler;
            _logger = logger;
        }

        private ICommandHandler<TCommand> _commandHandler;

        private ILogger<LoggingCommandHandlerDecorator<TCommand>> _logger { get; }

        public async Task HandleAsync(TCommand command, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation( "Started processing {CommandType}", command.GetType().Name);
                await _commandHandler.HandleAsync(command, cancellationToken);
                _logger.LogInformation("Finish processing {CommandType}", command.GetType().Name);
            }
            catch(Exception ex) 
            {
                _logger.LogError(ex, "Failed to process {CommandType}", command.GetType().Name);
                throw;
            }
        }
    }
}
