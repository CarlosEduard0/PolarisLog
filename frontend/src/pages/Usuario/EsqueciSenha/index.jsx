import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import { TiChevronLeft } from 'react-icons/ti';
import { FiCheckSquare } from 'react-icons/fi';

import api from '../../../services/api';
import './styles.css';
import logoImg from '../../../assets/logo.png';

export default function EsqueciSenha() {
  const [email, setEmail] = useState('');
  const [formEnviado, setFormEnviado] = useState(false);
  const [emailEnviado, setEmailEnviado] = useState(false);
  const [errorMessage, setErrorMessage] = useState('');

  async function handleEsqueciSenha(e) {
    e.preventDefault();
    setFormEnviado(true);
    try {
      await api.post('/usuarios/recuperarsenha', {
        email,
      });
      setEmailEnviado(true);
    } catch (err) {
      if (err.response.status === 400) {
        setErrorMessage(err.response.data);
      }
    }
  }

  return (
    <div className="forgot-password">
      <section className="form">
        <form onSubmit={handleEsqueciSenha}>
          <img src={logoImg} alt="PolarisLog" width={350} />
          {emailEnviado ? (
            <p>
              <br />
              E-mail de redefinição de senha enviado com sucesso! <br />
              <br />
              <FiCheckSquare size={40} color="#61B887" />
            </p>
          ) : (
            <>
              <input
                type="email"
                placeholder="E-mail"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
              />
              {formEnviado && !email && <span>Campo obrigatório *</span>}
              <span>{errorMessage}</span>
              <button className="button" type="submit">
                Esqueci a senha
              </button>
              <Link className="back-link" to="/">
                <TiChevronLeft size={20} color="#413E3E" />
                Voltar
              </Link>
            </>
          )}
        </form>
      </section>
    </div>
  );
}
