const localRefreshtoken = "refreshtoken";
const localAcccesstoken = "accesstoken";
const isSignedIn = "isSignedIn";

const getAccesstoken = () => {
  return localStorage.getItem(localAcccesstoken);
};

const getRefreshtoken = () => {
  return localStorage.getItem(localRefreshtoken);
};

const setAccesstoken = (accesstoken: string) => {
  localStorage.setItem(localAcccesstoken, accesstoken);
};
const setRefreshtoken = (refreshtoken: string) => {
  localStorage.setItem(localRefreshtoken, refreshtoken);
};
const setSignIn = (signed:boolean) => {
  const value = String(signed)
  localStorage.setItem(isSignedIn, value);
};
const getSignIn = () => {
  const value = localStorage.getItem(isSignedIn);
  const result = value == "true"
  console.log(result)
  return result
};
const clearTokens = () => {
  localStorage.removeItem(localAcccesstoken);
  localStorage.removeItem(localRefreshtoken);
};
export default {
  getAccesstoken,
  getRefreshtoken,
  setAccesstoken,
  setRefreshtoken,
  clearTokens,
  setSignIn,
  getSignIn
};
