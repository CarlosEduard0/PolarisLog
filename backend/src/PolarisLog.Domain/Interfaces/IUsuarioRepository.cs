﻿using System;
using System.Threading.Tasks;
using PolarisLog.Domain.Entities;

namespace PolarisLog.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario> ObterPorId(Guid id);
        Task<Usuario> ObterPorEmail(string email);
        Task<Usuario> Adicionar(Usuario usuario);
    }
}