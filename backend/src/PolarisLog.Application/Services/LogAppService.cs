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

        public async Task<PagedList<Log>> ObterTodos(QueryViewModel queryViewModel)
        {
            var query = new ObterTodosOsLogsQuery(queryViewModel.PageNumber, queryViewModel.PageSize);
            return await _mediator.Send(query);
        }

        public async Task<Guid> Adicionar(LogViewModel logViewModel)
        {
            var command = new AdicionarNovoLogCommand(logViewModel.UsuarioId, logViewModel.Level, logViewModel.Descricao, logViewModel.Origem);
            return await _mediator.Send(command);
        }

        public async Task Arquivar(Guid id)
        {
            var command = new ArquivarLogCommand(id);
            await _mediator.Send(command);
        }

        public async Task Deletar(Guid id)
        {
            var command = new DeletarLogCommand(id);
            await _mediator.Send(command);
        }
    }
}