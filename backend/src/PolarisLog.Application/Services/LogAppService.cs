﻿using System;
using System.Threading.Tasks;
using MediatR;
using PolarisLog.Application.Interfaces;
using PolarisLog.Application.ViewModels;
using PolarisLog.Domain.CommandSide.Commands.Log;
using PolarisLog.Domain.Entities;
using PolarisLog.Domain.QuerySide;
using PolarisLog.Domain.QuerySide.Queries.Log;

namespace PolarisLog.Application.Services
{
    public class LogAppService : ILogAppService
    {
        private readonly IMediator _mediator;

        public LogAppService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<PagedList<Log>> ObterTodos(LogQueryViewModel logQueryViewModel)
        {
            var query = new ObterTodosOsLogsQuery(
                logQueryViewModel.PageNumber,
                logQueryViewModel.PageSize,
                logQueryViewModel.AmbienteId,
                logQueryViewModel.Origem,
                logQueryViewModel.Descricao,
                logQueryViewModel.Arquivado
                );
            return await _mediator.Send(query);
        }

        public async Task<Guid> Adicionar(LogViewModel logViewModel)
        {
            var command = new AdicionarNovoLogCommand(logViewModel.UsuarioId, logViewModel.AmbienteId,
                logViewModel.NivelId, logViewModel.Titulo, logViewModel.Descricao, logViewModel.Origem);
            return await _mediator.Send(command);
        }

        public async Task Arquivar(params Guid[] ids)
        {
            var command = new ArquivarLogCommand(ids);
            await _mediator.Send(command);
        }

        public async Task Deletar(params Guid[] ids)
        {
            var command = new DeletarLogCommand(ids);
            await _mediator.Send(command);
        }
    }
}