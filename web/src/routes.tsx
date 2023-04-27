import React from 'react';
import { BrowserRouter, Switch, Route, Redirect } from 'react-router-dom';
import Auth from './pages/Auth';
import CreateOrphanage from './pages/CreateOrphanage';
import Landing from './pages/Landing';
import Orphanage from './pages/Orphanage';
import OrphanagesMap from './pages/OrphanagesMap';
import ToggleOrphanage from './pages/ToggleOrphanage';

function Routes() {
  return (
    <BrowserRouter>
      <Switch>
        <Route path="/" exact component={Landing} />
        <Route path="/app" component={OrphanagesMap} />

        <Route path="/orphanages/create" component={CreateOrphanage} />
        <Route path="/orphanages/:orfanatoId" component={Orphanage} />

        {/* <Redirect from="/admin/toggle-orphanages" to="/admin/auth" exact/> */}
        <Route from="/admin/auth" component={Auth} />
        <Route path="/admin/toggle-orphanages" component={ToggleOrphanage} />
      </Switch>
    </BrowserRouter>
  );
}

export default Routes;