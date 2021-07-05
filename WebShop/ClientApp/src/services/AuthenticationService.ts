import LoginModel from "@/models/LoginModel";
import LoginResponse from "@/models/LoginResponse";
import RegisterModel from "@/models/Register";
import TokenWithRefresh from "@/models/TokensWithRefresh";
import axios from "axios";
import LocalStorageService from "./LocalStorageService";

const Login = async (loginModel: LoginModel) => {
  try {
    const loginResponse = await axios.post<LoginResponse>(
      "/api/authenticate/login",
      loginModel
    );
    const token: TokenWithRefresh = loginResponse.data.tokens;
    LocalStorageService.setAccesstoken(token.accessToken);
    LocalStorageService.setRefreshtoken(token.refreshToken);
    LocalStorageService.setSignIn(true);
    return loginResponse.data;
  } catch (error) {
    return false;
  }
};

const Register = async (registerModel: RegisterModel) => {
  const isSuccessfull = await axios.post<boolean>(`/api/register`, registerModel);
  return isSuccessfull;
};

const logout = () => {
  LocalStorageService.clearTokens();
  LocalStorageService.setSignIn(false);
};

const isSignedIn = () => {
  return LocalStorageService.getSignIn();
};

export default { Login, logout, isSignedIn, Register };
