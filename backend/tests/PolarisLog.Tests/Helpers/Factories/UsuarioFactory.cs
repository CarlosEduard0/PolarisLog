﻿using PolarisLog.Domain.Entities;

namespace PolarisLog.Tests.Helpers.Factories
{
    public class UsuarioFactory
    {
        public static Usuario Create()
        {
            return new Usuario("nome", "email@email.com");
        }
    }
}