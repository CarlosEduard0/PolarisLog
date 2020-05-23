import React, { useEffect, useState } from 'react';
import { useHistory } from 'react-router-dom';

import api from '../../services/api';

export default function Dashboard() {
  const [logs, setLogs] = useState([]);

  const history = useHistory();

  useEffect(() => {
    async function getLogs() {
      try {
        const response = await api.get('logs', {
          headers: {
            Authorization: `Bearer ${localStorage.getItem('token')}`,
          },
        });
        setLogs(response.data);
      } catch (err) {
        if (err.response.status === 401) {
          history.push('/');
        }
      }
    }

    getLogs();
  }, []);

  return (
    <table>
      <thead>
        <tr>
          <th>Nível</th>
          <th>Descrição</th>
        </tr>
      </thead>
      <tbody>
        {logs.map((log) => (
          <tr key={log.id}>
            <td>{log.nivel.nome}</td>
            <td>{log.descricao}</td>
          </tr>
        ))}
      </tbody>
    </table>
  );
}
