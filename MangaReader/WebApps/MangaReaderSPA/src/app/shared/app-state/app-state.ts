export interface IAppState {
  accessToken?: string;
  refreshToken?: string;
  id?: string;
  username?: string;
  email?: string;
  firstName?: string;
  lastName?: string;

  clone(): IAppState;
}

export class AppState implements IAppState {
  public accessToken?: string;
  public refreshToken?: string;
  public id?: string;
  public username?: string;
  public email?: string;
  public firstName?: string;
  public lastName?: string;

  public constructor();
  public constructor(accessToken?: string, refreshToken?: string, id?: string, username?: string, email?: string, firstName?: string, lastName?: string);

  public constructor(...args: any[]) {
    if (args.length === 0) {
      return;
    } else if (args.length === 7) {
      this.accessToken = args[0];
      this.refreshToken = args[1];
      this.id = args[2];
      this.username = args[3];
      this.email = args[4];
      this.firstName = args[5];
      this.lastName = args[6];
    }
  }

  public clone(): IAppState {
    const newState = new AppState();
    Object.assign(newState, this);
    return newState;
  }
}
