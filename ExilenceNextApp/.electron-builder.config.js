/**
 * TODO: Rewrite this config to ESM
 * But currently electron-builder doesn't support ESM configs
 * @see https://github.com/develar/read-config-file/issues/10
 */

/**
 * @type {() => import('electron-builder').Configuration}
 * @see https://www.electron.build/configuration/configuration
 */
module.exports = async function () {
  const { getVersion } = await import('./version/getVersion.mjs');

  return {
    extends: null,
    generateUpdatesFilesForAllChannels: true,
    productName: 'Exilence Next',
    appId: 'com.exilence.exilence-next-app',
    directories: {
      output: 'dist',
      buildResources: 'buildResources',
    },
    files: ['packages/**/dist/**', 'packages/!(locales)'],
    extraFiles: ['packages/localization/locales/**/*'],
    extraMetadata: {
      version: getVersion(),
    },
    nsis: {
      oneClick: false,
      allowToChangeInstallationDirectory: true,
    },
    win: {
      target: ['nsis', 'msi'],
      icon: 'build/icon.ico',
    },
  };
};
