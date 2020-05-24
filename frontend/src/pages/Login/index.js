import React, { useState } from 'react';
import { Link, useHistory } from 'react-router-dom';
import { FiLogIn } from 'react-icons/fi';

import api from '../../services/api';
import './styles.css';

export default function Login() {
  const [erros, setErros] = useState([]);
  const [email, setEmail] = useState('');
  const [senha, setSenha] = useState('');
  const history = useHistory();

  async function handleLogin(e) {
    e.preventDefault();
    try {
      const response = await api.post('/usuarios/logar', { email, senha });

      localStorage.setItem('token', response.data.token);

      history.push('/dashboard');
    } catch ({ response }) {
      if (response.status === 400) {
        setErros(response.data);
      }
    }
  }

  return (
    <div className="logon-container">
      <section className="form">
        {/* <img src={logoImg} alt="Be The Hero" /> */}
        <form onSubmit={handleLogin}>
          <div>
            {erros.map((error, i) => (
              <li key={i}>{error}</li>
            ))}
          </div>
          <h1>Faça seu logon</h1>
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
          <button className="button" type="submit">
            Entrar
          </button>
          <Link className="back-link" to="/esquecisenha">
            Esqueci a senha
          </Link>
          <Link className="back-link" to="/cadastrar">
            <FiLogIn size={16} color="#E02041" />
            Não tenho cadastro
          </Link>
        </form>
      </section>

      {/* <img src={heroesImg} alt="Heroes" /> */}
    </div>
  );
}
