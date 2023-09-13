import { useTranslation } from 'react-i18next';

import { Version } from './components/Version';

function App() {
  const { t } = useTranslation();

  return (
    <div>
      <h1>Exilence Next Reboot</h1>

      <div className="my-10">
        <Version />
        <div>Language Toggle Check: {t('label.sign_out')}</div>
      </div>
    </div>
  );
}

export default App;
