import React from 'react';
import { useHistory, Link } from 'react-router-dom';
import { FiLogOut } from 'react-icons/fi';
import { TiChevronLeft } from 'react-icons/ti';

import './styles.css';
import logoImg from '../../../assets/logo.png';

export default function VisualizarLog({ location }) {
  const { log } = location.state;
  return (
    <div className="visualizar-container">
      <header>
        <Link to="/dashboard">
          <img src={logoImg} alt="PolarisLog" width={250} />
        </Link>
        <Link to="/">
          <FiLogOut size={20} color="#3F7657" />
          Sair
        </Link>
      </header>

      <section className="voltar">
        <Link to="/dashboard">
          <TiChevronLeft size={20} color="#413E3E" />
          Voltar
        </Link>
      </section>
      <section className="log">
        <header>
          <p>
            {log.nivel.nome} no {log.origem} em {Intl.DateTimeFormat('pt-BR', {
              year: 'numeric',
              month: 'numeric',
              day: 'numeric',
              hour: 'numeric',
              minute: 'numeric',
              second: 'numeric',
            }).format(new Date(`${log.cadastradoEm}Z`))}
          </p>
        </header>

        <div className="content">
          <div className="titulo-descricao">
            <div className="titulo">
              <h3>Título</h3>
              <p>{log.titulo}</p>
            </div>
            <div className="descricao">
              <h3>Descrição</h3>
              <p>{log.descricao}</p>
            </div>
          </div>
          <div className="usuario">
            <span className={log.nivel.nome}>{log.nivel.nome}</span>
            <h3>Coletador por</h3>
            <p>{log.usuario.nome}</p>
          </div>
        </div>
      </section>
    </div>
  );
}
