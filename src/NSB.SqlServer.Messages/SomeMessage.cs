using System;
using NServiceBus;

namespace NSB.SqlServer.Messages
{
    public class SomeMessage : IMessage
    {
        public SomeMessage()
        { }

        public SomeMessage(long customerId, DateTime createdAt)
        {
            CreatedAt = createdAt;
            CustomerId = customerId;
        }

        public DateTime CreatedAt { get; private set; }
        public long CustomerId { get; private set; }
        public Guid Guid { get; private set; } = Guid.NewGuid();
        public bool IsTruth { get; private set; } = true;

        public void SetIsTruth(bool isTruth)
        {
            IsTruth = isTruth;
        }
    }
}
