﻿using System;

namespace PolarisLog.Domain.Entities
{
    public class Entity
    {
        public Guid Id { get; }

        public Entity()
        {
            Id = Guid.NewGuid();
        }
    }
}