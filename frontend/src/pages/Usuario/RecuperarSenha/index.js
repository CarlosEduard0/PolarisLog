import React, { useState } from 'react';
import { Link, useHistory } from 'react-router-dom';
import { FiArrowLeft } from 'react-icons/fi';

import api from '../../../services/api';
import './styles.css';

export default function RecuperarSenha(props) {
  const [erros, setErros] = useState([]);
  const [senha, setSenha] = useState('');

  const history = useHistory();

  const query = new URLSearchParams(props.location.search);
  const token = query.get('token');
  const email = query.get('email');

  async function handleRecuperarSenha(e) {
    e.preventDefault();
    try {
      const response = await api.post('/usuarios/recuperarsenha', {
        token,
        email,
        senha,
      });

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
        <form onSubmit={handleRecuperarSenha}>
          <div>
            {erros.map((error, i) => (
              <li key={i}>{error}</li>
            ))}
          </div>
          <h1>Trocar senha</h1>
          <input type="email" value={email} disabled />
          <input
            type="password"
            placeholder="Nova senha"
            value={senha}
            onChange={(e) => setSenha(e.target.value)}
          />
          <button className="button" type="submit">
            Trocar senha
          </button>
          <Link className="back-link" to="/">
            <FiArrowLeft size={16} color="#E02041" />
            Voltar para o login
          </Link>
        </form>
      </section>

      {/* <img src={heroesImg} alt="Heroes" /> */}
    </div>
  );
}
