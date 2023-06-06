using AutoMapper;
using MassTransit;
using EventBus.Messages.Events;
using UserInfo.Common.Repository;
using System.Security.Claims;
using UserInfo.Common.DTOs;
using Microsoft.Extensions.Logging;

namespace UserInfo.Common.EventBusConsumers
{
    public class UpdateAllReadMangaConsumer : IConsumer<UpdateAllReadMangaEvent>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateAllReadMangaConsumer> _logger;
        private readonly IUserInformationRepository _repository;

        public UpdateAllReadMangaConsumer(IMapper mapper, ILogger<UpdateAllReadMangaConsumer> logger, IUserInformationRepository repository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task Consume(ConsumeContext<UpdateAllReadMangaEvent> context)
        {
            var updateInfo = _mapper.Map<UpdateUserInfoDTO>(context.Message);
            await _repository.AddMangaInAllReadMangaIds(updateInfo);

            _logger.LogInformation("Updated AllMangaRead successfully");
        }
    }
}
