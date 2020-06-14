import React, { useEffect, useState } from 'react';
import { useHistory, Link } from 'react-router-dom';
import { FiLogOut } from 'react-icons/fi';
import { FaTrashAlt } from 'react-icons/fa';
import { GoFileDirectory } from 'react-icons/go';

import './styles.css';
import logoImg from '../../assets/logo.png';
import api from '../../services/api';

export default function Dashboard() {
  const [logs, setLogs] = useState([]);
  const [environments, setEnvironments] = useState([]);
  const [selectedLogs, setSelectedLogs] = useState([]);

  const history = useHistory();

  async function getLogs(environmentId) {
    const response = await api.get('logs', {
      params: {
        ambienteId: environmentId,
        arquivado: false,
      },
    });
    setLogs(response.data);
  }

  useEffect(() => {
    async function loadData() {
      try {
        const { data: ambientes } = await api.get('ambientes');
        setEnvironments(ambientes);

        await getLogs(ambientes[0].id);
      } catch (err) {
        if (err.response.status === 401) {
          history.push('/');
        }
      }
    }
    loadData();
  }, []);

  function navigateToViewLog(log) {
    history.push({
      pathname: '/visualizarlog',
      state: { log },
    });
  }

  async function handleSelectEnvironment(environmentId) {
    await getLogs(environmentId);
  }

  function handleSelectLog(logId) {
    const alreadySelected = selectedLogs.includes(logId);
    if (alreadySelected) {
      const filteredItems = selectedLogs.filter((id) => id !== logId);
      setSelectedLogs(filteredItems);
      return;
    }

    setSelectedLogs([...selectedLogs, logId]);
  }

  function handleSelectAllLog() {
    if (logs.length === selectedLogs.length) {
      setSelectedLogs([]);
      return;
    }

    setSelectedLogs([]);
    const logsId = logs.map((log) => log.id);
    setSelectedLogs(logsId);
  }

  async function handleArchive() {
    if (selectedLogs.length === 0) return;

    await api.put('logs', {
      ids: selectedLogs,
    });

    const logsFiltered = logs.filter((log) => !selectedLogs.includes(log.id));
    setLogs(logsFiltered);
  }

  async function handleDelete() {
    if (selectedLogs.length === 0) return;

    await api.delete(`logs?ids=${selectedLogs.join('&ids=')}`);

    const logsFiltered = logs.filter((log) => !selectedLogs.includes(log.id));
    setLogs(logsFiltered);
  }

  return (
    <div className="dashboard-container">
      <header>
        <img src={logoImg} alt="PolarisLog" width={250} />
        <Link to="/">
          <FiLogOut size={20} color="#3F7657" />
          Sair
        </Link>
      </header>
      <section className="filter">
        <p>
          Bem vindo {localStorage.getItem('username')}, seu token é:{' '}
          {localStorage.getItem('accessToken')}
        </p>
        <div className="fields">
          <select
            name="ambiente"
            id="ambiente-select"
            onChange={(event) => handleSelectEnvironment(event.target.value)}
          >
            {environments.map((environment) => (
              <option key={environment.id} value={environment.id}>{environment.nome}</option>
            ))}
          </select>
          <select name="ordernar" id="ordernar-select" defaultValue="">
            <option value="" disabled>
              Ordenar por
            </option>
            <option value="a2973498294">Nível</option>
          </select>
          <select name="buscar" id="buscar-select" defaultValue="">
            <option value="" disabled>
              Buscar por
            </option>
            <option value="a2973498294">Nível</option>
          </select>
          <input name="term" />
        </div>
        <div className="buttons">
          <button type="button" onClick={handleArchive}>
            <GoFileDirectory />
            Arquivar
          </button>
          <button type="button" onClick={handleDelete}>
            <FaTrashAlt />
            Apagar
          </button>
        </div>
      </section>
      <table>
        <thead>
          <tr>
            <th className="checkbox">
              <input
                type="checkbox"
                onChange={handleSelectAllLog}
                checked={logs.length === selectedLogs.length}
              />
            </th>
            <th>Nível</th>
            <th>Data/Hora</th>
            <th>Origem</th>
            <th>Descrição</th>
          </tr>
        </thead>
        <tbody>
          {logs.map((log) => (
            <tr key={log.id} onClick={() => navigateToViewLog(log)}>
              <td>
                <input
                  type="checkbox"
                  onClick={(e) => e.stopPropagation()}
                  onChange={() => handleSelectLog(log.id)}
                  checked={selectedLogs.includes(log.id)}
                />
              </td>
              <td>
                <span className={log.nivel.nome}>{log.nivel.nome}</span>
              </td>
              <td>
                {Intl.DateTimeFormat('pt-BR', {
                  year: 'numeric',
                  month: 'numeric',
                  day: 'numeric',
                  hour: 'numeric',
                  minute: 'numeric',
                  second: 'numeric',
                }).format(new Date(`${log.cadastradoEm}Z`))}
              </td>
              <td>{log.origem}</td>
              <td className="descricao">{log.descricao}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
