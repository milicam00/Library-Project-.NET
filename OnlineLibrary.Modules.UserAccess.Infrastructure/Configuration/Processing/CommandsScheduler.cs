using Dapper;
using Newtonsoft.Json;
using OnlineLibrary.BuildingBlocks.Application.Data;
using OnlineLibrary.BuildingBlocks.Infrastructure.IInternalCommandsMapper;
using OnlineLibrary.BuildingBlocks.Infrastructure.Serialization;
using OnlineLibrary.Modules.UserAccess.Application.Configuration.Commands;
using OnlineLibrary.Modules.UserAccess.Application.Contracts;

namespace OnlineLibrary.Modules.UserAccess.Infrastructure.Configuration.Processing
{
    public class CommandsScheduler : ICommandsScheduler
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        private readonly IInternalCommandsMapper _internalCommandsMapper;

        public CommandsScheduler(
            ISqlConnectionFactory sqlConnectionFactory,
            IInternalCommandsMapper internalCommandsMapper)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
            _internalCommandsMapper = internalCommandsMapper;
        }

        public async Task EnqueueAsync(ICommand command)
        {
            var connection = this._sqlConnectionFactory.GetOpenConnection();

            const string sqlInsert = "INSERT INTO [administration].[InternalCommands] ([Id], [EnqueueDate] , [Type], [Data]) VALUES " +
                                     "(@Id, @EnqueueDate, @Type, @Data)";

            await connection.ExecuteAsync(sqlInsert, new
            {
                command.Id,
                EnqueueDate = DateTime.UtcNow,
                Type = _internalCommandsMapper.GetName(command.GetType()),
                Data = JsonConvert.SerializeObject(command, new JsonSerializerSettings
                {
                    ContractResolver = new AllPropertiesContactResolver()
                })
            });
        }

        public async Task EnqueueAsync<T>(ICommand<T> command)
        {
            var connection = this._sqlConnectionFactory.GetOpenConnection();

            const string sqlInsert = "INSERT INTO [administration].[InternalCommands] ([Id], [EnqueueDate] , [Type], [Data]) VALUES " +
                                     "(@Id, @EnqueueDate, @Type, @Data)";

            await connection.ExecuteAsync(sqlInsert, new
            {
                command.Id,
                EnqueueDate = DateTime.UtcNow,
                Type = _internalCommandsMapper.GetName(command.GetType()),
                Data = JsonConvert.SerializeObject(command, new JsonSerializerSettings
                {
                    ContractResolver = new AllPropertiesContactResolver()
                })
            });
        }
    }
}
