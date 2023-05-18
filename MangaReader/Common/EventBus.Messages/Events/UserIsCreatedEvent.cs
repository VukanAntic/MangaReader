using System;

namespace EventBus.Messages.Events
{
	public class UserIsCreatedEvent : IntegrationBaseEvent
	{
		public Guid UserId { get; set; }

		public UserIsCreatedEvent(Guid userId)
		{
			UserId = userId;
		}

        public UserIsCreatedEvent()
        {
            UserId = new Guid();
        }
    }
}

