import React, { useState } from 'react';
import { Link, useHistory } from 'react-router-dom';
import { FiArrowLeft } from 'react-icons/fi';

import api from '../../../services/api';
import './styles.css';
// import logoImg from '../../../assets/logo.svg';

export default function CadastrarUsuario() {
  const [erros, setErros] = useState([]);
  const [nome, setNome] = useState('');
  const [email, setEmail] = useState('');
  const [senha, setSenha] = useState('');
  const [senhaConfirmacao, setSenhaConfirmacao] = useState('');

  const history = useHistory();

  async function handleCadastrar(e) {
    e.preventDefault();

    const data = { nome, email, senha, senhaConfirmacao };

    try {
      await api.post('/usuarios', data);
      history.push('/');
    } catch ({ response }) {
      if (response.status === 400) {
        setErros(response.data);
      }
    }
  }

  return (
    <div className="register-container">
      <div className="content">
        <section>
          {/* <img src={logoImg} alt="PolarisLog" /> */}
          <div />

          <h1>Cadastro</h1>
          <p>
            Faça seu cadastro, entre na plataforma e ajude pessoas a encontrarem
            os casos da sua ONG.
          </p>

          <Link className="back-link" to="/">
            <FiArrowLeft size={16} color="#E02041" />
            Já tenho cadastro
          </Link>
        </section>

        <form onSubmit={handleCadastrar}>
          <div>
            {erros.map((error, i) => (
              <li key={i}>{error}</li>
            ))}
          </div>
          <input
            placeholder="Nome"
            value={nome}
            onChange={(e) => setNome(e.target.value)}
          />
          <input
            type="email"
            placeholder="E-mail"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
          />
          <input
            type="password"
            placeholder="Senha"
            value={senha}
            onChange={(e) => setSenha(e.target.value)}
          />
          <input
            type="password"
            placeholder="Senha confirmação"
            value={senhaConfirmacao}
            onChange={(e) => setSenhaConfirmacao(e.target.value)}
          />
          <button className="button" type="submit">
            Cadastrar
          </button>
        </form>
      </div>
    </div>
  );
}
