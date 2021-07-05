import axios, { AxiosRequestConfig } from "axios";
import localStorageService from "./LocalStorageService";

const getRefreshtokenUrl = "/api/authentication/accesstoken";
const client = axios.create();
// client.interceptors.request.use(
//   (config: AxiosRequestConfig) => {
//     const accesstoken = localStorageService.getAccesstoken();
//     if (!accesstoken) {
//       return config;
//     }
//     config.headers["Authorization"] = `Bearer ${accesstoken}`;
//     return config;
//   },
//   error => {
//     Promise.reject(error);
//   }
// );

// client.interceptors.response.use(
//   response => response,
//   async error => {
//     const originalRequest = error.config;
//     const isAccesstokenExpired: boolean = error.response.headers[
//       "www-authenticate"
//     ].includes("expired");
//     if (
//       error.response.status === 401 &&
//       isAccesstokenExpired &&
//       !originalRequest._retry
//     ) {
//       originalRequest._retry = true;
//       const RefreshToken = localStorageService.getRefreshtoken();
//       const AccessToken = localStorageService.getAccesstoken();
//       const newAccesstokenResponse = await axios.post(getRefreshtokenUrl, {
//         RefreshToken,
//         AccessToken
//       });
      
//       if (
//         newAccesstokenResponse.status !== 200 &&
//         newAccesstokenResponse.status !== 201
//       ) {
//         return Promise.reject(error);
//       }
//       const newAccesstoken = newAccesstokenResponse.data;
//       localStorageService.setAccesstoken(newAccesstoken);
//       originalRequest.headers["Authorization"] = `Bearer ${newAccesstoken}`;
//       return axios(originalRequest);
//     }
//     window.location.href = "/login";
//     return Promise.reject(error);
//   }
// );

export default client;
