import React, { useState } from 'react';
import { Link, useHistory } from 'react-router-dom';
import { TiChevronLeft } from 'react-icons/ti';
import { FiCheckSquare } from 'react-icons/fi';

import api from '../../../services/api';
import './styles.css';
import logoImg from '../../../assets/logo.png';

export default function RecuperarSenha(props) {
  const [senha, setSenha] = useState('');
  const [senhaConfirmacao, setSenhaConfirmacao] = useState('');
  const [formEnviado, setFormEnviado] = useState(false);
  const [senhaAlterada, setSenhaAlterada] = useState(false);
  const [erro, setErro] = useState('');

  const history = useHistory();

  const query = new URLSearchParams(props.location.search);
  if (query.get('email') === null || query.get('token') === null) {
    history.push('/');
  }

  const token = query.get('token').replace(/\s/g, '+');
  const email = query.get('email');

  async function handleRecuperarSenha(e) {
    e.preventDefault();
    setFormEnviado(true);

    try {
      await api.post('/usuarios/resetarsenha', {
        token,
        email,
        senha,
      });

      setSenhaAlterada(true);
      setTimeout(() => history.push('/'), 3000);
    } catch ({ response }) {
      if (response.status === 400 && response.data[0].includes('Token')) {
        setErro(response.data[0]);
      }
    }
  }

  return (
    <div className="recover-password">
      <section className="form">
        <form onSubmit={handleRecuperarSenha}>
          <img src={logoImg} alt="PolarisLog" width={350} />
          {senhaAlterada ? (
            <p>
              <br />
              Senha redefinida com sucesso! <br />
              Redirecionando para o login <br />
              <br />
              <FiCheckSquare size={40} color="#61B887" />
            </p>
          ) : (
            <>
              {formEnviado && erro && (
                <span>Token para redifinição de senha inválido</span>
              )}
              <input
                type="password"
                placeholder="Nova senha"
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
              {formEnviado && !senhaConfirmacao && (
                <span>Campo obrigatório *</span>
              )}
              {formEnviado && senha !== senhaConfirmacao && (
                <span>Senhas não coincidem *</span>
              )}
              <button className="button" type="submit">
                Alterar senha
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
