import TokenWithRefresh from "@/models/TokensWithRefresh";

interface LoginResponse {
  firstname: string;
  lastname: string;
  email: string;
  tokens: TokenWithRefresh;
}
export default LoginResponse;
