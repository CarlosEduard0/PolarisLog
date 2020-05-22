import React from 'react';
import { BrowserRouter, Switch, Route } from 'react-router-dom';

import Login from './pages/Login';
import CadastrarUsuario from './pages/Usuario/CadastrarUsuario';
import Dashboard from './pages/Dashboard';
import CadastrarLog from './pages/Log/CadastrarLog';

export default function Routes() {
  return (
    <BrowserRouter>
      <Switch>
        <Route path="/" exact component={Login} />
        <Route path="/cadastrar" component={CadastrarUsuario} />
        <Route path="/dashboard" component={Dashboard} />
        <Route path="/cadastrarlog" component={CadastrarLog} />
      </Switch>
    </BrowserRouter>
  );
}
