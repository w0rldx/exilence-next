import { AxiosResponse } from 'axios';
import axios from 'axios-observable';
import { Observable, of } from 'rxjs';
import { concatMap, mergeMap } from 'rxjs/operators';
import { rootStore } from '..';
import { ICharacterListResponse, ICharacterResponse } from '../interfaces/character.interface';
import { IGithubRelease } from '../interfaces/github/github-release.interface';
import { ILeague } from '../interfaces/league.interface';
import { IPoeProfile } from '../interfaces/poe-profile.interface';
import { IStash, IStashTab, IStashTabResponse } from '../interfaces/stash.interface';
import AppConfig from './../config/app.config';

const apiUrl = AppConfig.pathOfExileApiUrl;

export const externalService = {
  getLatestRelease,
  getStashTab,
  getStashTabs,
  getStashTabWithChildren,
  getLeagues,
  getCharacters,
  getCharacter,
  getProfile,
  loginWithOAuth,
};

/* #region github.com */
function getLatestRelease() {
  return axios.get<IGithubRelease>(
    'https://api.github.com/repos/viktorgullmark/exilence-next/releases/latest'
  );
}

function loginWithOAuth(code: string): Observable<AxiosResponse<any>> {
  return axios.get(`${AppConfig.baseUrl}/api/authentication/oauth2?code=${code}`);
}
/* #endregion */

/* #region pathofexile.com */
function getStashTab(league: string, id: string): Observable<AxiosResponse<IStashTabResponse>> {
  return axios.get<IStashTabResponse>(`${apiUrl}/stash/${league}/${id}`);
}

function getStashTabs(league: string): Observable<AxiosResponse<IStash>> {
  return axios.get<IStash>(`${apiUrl}/stash/${league}`);
}

function getStashTabWithChildren(stashTab: IStashTab, league: string, children?: boolean) {
  const makeRequest = (tab: IStashTab) => {
    const prefix = tab.parent && children ? `${tab.parent}/` : '';
    return getStashTab(league, `${prefix}${tab.id}`).pipe(
      mergeMap((stashTab: AxiosResponse<IStashTabResponse>) => {
        if (!children) {
          rootStore.uiStateStore.incrementStatusMessageCount();
        }
        return of(stashTab.data.stash);
      })
    );
  };

  const source = of(stashTab).pipe(
    rootStore.rateLimitStore.rateLimiter1,
    rootStore.rateLimitStore.rateLimiter2,
    concatMap((tab: IStashTab) => makeRequest(tab))
  );

  return source;
}

function getLeagues(
  type: string = 'main',
  compact: number = 1,
  realm: string = 'pc'
): Observable<AxiosResponse<ILeague[]>> {
  const parameters = `?type=${type}&compact=${compact}${getRealmParam(realm)}`;
  return axios.get<ILeague[]>(apiUrl + '/leagues' + parameters, { headers: null });
}

function getCharacters(): Observable<AxiosResponse<ICharacterListResponse>> {
  return axios.get<ICharacterListResponse>(`${apiUrl}/character`);
}

function getCharacter(character: string): Observable<AxiosResponse<ICharacterResponse>> {
  return axios.get<ICharacterResponse>(`${apiUrl}/character/${character}`);
}

function getProfile(realm: string = 'pc'): Observable<AxiosResponse<IPoeProfile>> {
  const parameters = `?realm=${realm}`;
  return axios.get<IPoeProfile>(apiUrl + '/profile' + parameters);
}

function getRealmParam(realm?: string) {
  return realm !== undefined ? `&realm=${realm}` : '';
}

/* #endregion */
