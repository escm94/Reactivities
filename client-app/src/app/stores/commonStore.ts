import { makeAutoObservable, reaction } from 'mobx';
import { ServerError } from '../models/serverError';

export default class CommonStore {
  error: ServerError | null = null;
  token: string | null = localStorage.getItem('jwt');
  appLoaded = false;

  constructor() {
    makeAutoObservable(this);

    // this type of reaction only runs when observables change, not when initially set (see above token setting)
    // autoRun another reaction type that WOULD run when initially set
    reaction(
      () => this.token,
      (token) => {
        if (token) {
          localStorage.setItem('jwt', token);
        } else {
          localStorage.removeItem('jwt');
        }
      }
    );
  }

  setServerError(error: ServerError) {
    this.error = error;
  }

  setToken(token: string | null) {
    this.token = token;
  }

  setAppLoaded() {
    this.appLoaded = true;
  }
}
