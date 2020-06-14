import React, { useState } from 'react';
import { Link, useHistory } from 'react-router-dom';

import api from '../../services/api';
import './styles.css';
import logoImg from '../../assets/logo.png';

export default function Login() {
  const [erro, setErro] = useState('');
  const [email, setEmail] = useState('');
  const [senha, setSenha] = useState('');
  const [formEnviado, setFormEnviado] = useState(false);
  const history = useHistory();

  async function handleLogin(e) {
    e.preventDefault();
    setFormEnviado(true);
    try {
      const { data } = await api.post('/usuarios/logar', { email, senha });

      localStorage.setItem('accessToken', data.accessToken);
      localStorage.setItem('username', data.usuario.nome);

      history.push('/dashboard');
    } catch ({ response }) {
      if (
        response.status === 400 &&
        response.data[0].includes('Email ou senha')
      ) {
        setErro(response.data[0]);
      }
    }
  }

  return (
    <div className="logon-container">
      <section className="form">
        <form onSubmit={handleLogin}>
          <img src={logoImg} alt="PolarisLog" width={350} />
          {erro && <span>E-mail ou senha inválidos *</span>}
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
          <button className="button" type="submit">
            Entrar
          </button>
          <Link className="back-link" to="/esquecisenha">
            Esqueceu sua senha?
          </Link>
          <Link className="back-link cadastrar-link" to="/cadastrar">
            Ainda não é cadastrado? Cadastrar
          </Link>
        </form>
      </section>
    </div>
  );
}
