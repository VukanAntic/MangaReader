import { JwtPayloadKeys } from "./jwt-payload-keys";

export interface IJwtPayload {
  [JwtPayloadKeys.ID]: string;
  [JwtPayloadKeys.Username]: string;
  [JwtPayloadKeys.Email]: string;
  exp: number;
  iss: string;
  aud: string;
}
