import { contextBridge, ipcRenderer } from 'electron';
import { preloadBindings } from 'i18next-electron-fs-backend';

export default function configureBridgedServices() {
  contextBridge.exposeInMainWorld('api', {
    i18nextElectronBackend: preloadBindings(ipcRenderer, process),
  });
}
