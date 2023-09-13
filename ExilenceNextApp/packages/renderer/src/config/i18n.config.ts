import i18n from 'i18next';
import backend from 'i18next-electron-fs-backend';
import { initReactI18next } from 'react-i18next';

function configureI18n() {
  i18n
    .use(backend)
    .use(initReactI18next)
    .init({
      lng: 'en',
      backend: {
        loadPath: './packages/renderer/locales/{{lng}}/{{ns}}.json',
        addPath: '/packages/renderer/locales/{{lng}}/{{ns}}.missing.json',
      },

      // other options you might configure
      debug: true,
      saveMissing: true,
      saveMissingTo: 'current',
      fallbackLng: 'en',
      ns: ['common', 'notification', 'error', 'tables', 'stepper', 'status'],
      defaultNS: 'common',
      interpolation: {
        escapeValue: false,
        formatSeparator: ',',
      },
    });
}

export default configureI18n;
