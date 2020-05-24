import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import { FiArrowLeft } from 'react-icons/fi';

import api from '../../../services/api';
import './styles.css';

export default function EsqueciSenha() {
  const [erros, setErros] = useState([]);
  const [email, setEmail] = useState('');

  async function handleEsqueciSenha(e) {
    e.preventDefault();
    try {
      await api.post('/usuarios/recuperarsenha', {
        email,
      });
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
        <form onSubmit={handleEsqueciSenha}>
          <div>
            {erros.map((error, i) => (
              <li key={i}>{error}</li>
            ))}
          </div>
          <h1>Recuperar senha</h1>
          <input
            placeholder="E-mail"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
          />
          <button className="button" type="submit">
            Esqueci a senha
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
