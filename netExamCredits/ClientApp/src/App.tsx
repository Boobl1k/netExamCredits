import React from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';

import './custom.css'
import Questionnaire from "./Pages/questionnaire";

const App = () => {
    return (
      <Layout>
        <Route exact path='/' component={Questionnaire} />
      </Layout>
    );
}
export default App; 