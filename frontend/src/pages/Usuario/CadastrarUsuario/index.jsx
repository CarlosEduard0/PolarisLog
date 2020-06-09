import React, { useState } from 'react';
import { Link, useHistory } from 'react-router-dom';
import { TiChevronLeft } from 'react-icons/ti';

import api from '../../../services/api';
import './styles.css';
import logoImg from '../../../assets/logo.png';

export default function CadastrarUsuario() {
  const [nome, setNome] = useState('');
  const [email, setEmail] = useState('');
  const [senha, setSenha] = useState('');
  const [senhaConfirmacao, setSenhaConfirmacao] = useState('');
  const [formEnviado, setFormEnviado] = useState(false);
  const [erro, setErro] = useState('');

  const history = useHistory();

  async function handleCadastrar(e) {
    e.preventDefault();
    setFormEnviado(true);

    const data = { nome, email, senha, senhaConfirmacao };

    try {
      await api.post('/usuarios', data);
      history.push('/');
    } catch ({ response }) {
      if (response.status === 400 && response.data[0].includes(email)) {
        setErro(response.data[0]);
      }
    }
  }

  return (
    <div className="register-container">
      <div className="content">
        <section>
          <img src={logoImg} alt="PolarisLog" />

          <p>Faça seu cadastro para ter acesso a plataforma</p>

          <Link className="back-link" to="/">
            <TiChevronLeft size={20} color="#413E3E" />
            Já tenho cadastro
          </Link>
        </section>

        <form onSubmit={handleCadastrar}>
        {erro && <span>{erro}</span>}
          <input
            placeholder="Nome"
            value={nome}
            onChange={(e) => setNome(e.target.value)}
          />
          {formEnviado && !nome && <span>Campo obrigatório *</span>}
          <input
            type="email"
            placeholder="E-mail"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
          />
          {formEnviado && !email && <span>Campo obrigatório *</span>}
          <input
            type="password"
            placeholder="Senha"
            value={senha}
            onChange={(e) => setSenha(e.target.value)}
          />
          {formEnviado && !senha && <span>Campo obrigatório *</span>}
          <input
            type="password"
            placeholder="Confirmação de senha"
            value={senhaConfirmacao}
            onChange={(e) => setSenhaConfirmacao(e.target.value)}
          />
          {formEnviado && !senhaConfirmacao && <span>Campo obrigatório *</span>}
          {formEnviado && senha !== senhaConfirmacao && (
            <span>Senhas não coincidem *</span>
          )}
          <button className="button" type="submit">
            Cadastrar
          </button>
        </form>
      </div>
    </div>
  );
}
