import React from 'react';
import { BrowserRouter, Switch, Route } from 'react-router-dom';

import Login from './pages/Login';
import CadastrarUsuario from './pages/Usuario/CadastrarUsuario';
import Dashboard from './pages/Dashboard';
import CadastrarLog from './pages/Log/CadastrarLog';
import VisualizarLog from './pages/Log/VisualizarLog';
import EsqueciSenha from './pages/Usuario/EsqueciSenha';
import RecuperarSenha from './pages/Usuario/RecuperarSenha';

export default function Routes() {
  return (
    <BrowserRouter>
      <Switch>
        <Route path="/" exact component={Login} />
        <Route path="/cadastrar" component={CadastrarUsuario} />
        <Route path="/esquecisenha" component={EsqueciSenha} />
        <Route path="/recuperarsenha" component={RecuperarSenha} />
        <Route path="/dashboard" component={Dashboard} />
        <Route path="/cadastrarlog" component={CadastrarLog} />
        <Route path="/visualizarlog" component={VisualizarLog} />
      </Switch>
    </BrowserRouter>
  );
}
