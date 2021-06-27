import LoginModel from "@/models/Login";
import LoginResponse from "@/models/LoginResponse";
import axios from "axios";
import LocalStorageService from "./LocalStorageService";

const Login = async (loginModel: LoginModel) => {
  try {
    const loginResponse = await axios.post<LoginResponse>(
      "/api/authenticate/login",
      loginModel
    );
    const tokens: LoginResponse = loginResponse.data;
    LocalStorageService.setAccesstoken(tokens.accessToken);
    LocalStorageService.setRefreshtoken(tokens.refreshToken);
    LocalStorageService.setSignIn(true);
  } catch (error) {
      console.log(error)
  }
 
};
const logout = ()=>{
  LocalStorageService.clearTokens();
  LocalStorageService.setSignIn(false);
}
const isSignedIn = ()=>{
  return LocalStorageService.getSignIn();
}
export default { Login,logout,isSignedIn };
