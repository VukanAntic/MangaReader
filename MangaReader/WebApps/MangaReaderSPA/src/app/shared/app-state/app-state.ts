export interface IAppState {
  accessToken?: string;
  refreshToken?: string;
  username?: string;

  clone(): IAppState;
}

export class AppState implements IAppState {
  public accessToken?: string;
  public refreshToken?: string;
  public username?: string;

  public constructor();
  public constructor(
    accessToken?: string,
    refreshToken?: string,
    username?: string
  );

  public constructor(...args: any[]) {
    if (args.length === 0) {
      return;
    } else if (args.length === 3) {
      this.accessToken = args[0];
      this.refreshToken = args[1];
      this.username = args[2];
    }
  }

  public clone(): IAppState {
    const newState = new AppState();
    Object.assign(newState, this);
    return newState;
  }
}
