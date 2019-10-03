using System;

namespace AccServerAdmin.Domain
{
    public interface IKeyedEntity
    {
        Guid Id { get; set; }
    }
}
