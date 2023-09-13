import './styles/global.css';

import { createRoot } from 'react-dom/client';
import { I18nextProvider } from 'react-i18next';

import i18n from '../../localization/i18n.config';
import App from './App';

const container = document.getElementById('root') as HTMLDivElement;
const root = createRoot(container);

root.render(
  <I18nextProvider i18n={i18n}>
    <App />
  </I18nextProvider>,
);
