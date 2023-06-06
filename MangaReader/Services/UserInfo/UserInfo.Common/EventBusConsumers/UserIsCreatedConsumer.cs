using System;
using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using MassTransit.Mediator;
using Microsoft.Extensions.Logging;
using UserInfo.Common.DTOs;
using UserInfo.Common.Repository;

namespace UserInfo.Common.EventBusConsumers
{
	public class UserIsCreatedConsumer : IConsumer<UserIsCreatedEvent>
	{
        private readonly IMapper _mapper;
        private readonly ILogger<UserIsCreatedConsumer> _logger;
        private readonly IUserInformationRepository _repository;

        public UserIsCreatedConsumer(IMapper mapper, ILogger<UserIsCreatedConsumer> logger, IUserInformationRepository repository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task Consume(ConsumeContext<UserIsCreatedEvent> context)
        {
            var userId = _mapper.Map<CreateUserInfoDTO>(context.Message);
            await _repository.CreateUserInfo(userId);

            _logger.LogInformation("{} consumed successfully. Created user with id: {}", nameof(UserIsCreatedEvent), userId.userId);
        }

    }
}
