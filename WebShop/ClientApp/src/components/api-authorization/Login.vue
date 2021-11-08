
<template>
  <div :v-if="!!message">{{ message }}</div>
  <div :v-else-if="action === loginActions.Login">Processing login</div>
  <div :v-else-if="action === loginActions.LoginCallback">
    Processing login callback
  </div>
  <div
    :v-else-if="
      action === loginActions.Profile || action === loginActions.Register
    "
  ></div>
</template>

<script lang="ts">
import { Options, Vue } from "vue-class-component";
import authService from "./AuthorizeService";
import { AuthenticationResultStatus } from "./AuthorizeService";
import {
  LoginActions,
  QueryParameterNames,
  ApplicationPaths,
} from "./ApiAuthorizationConstants";

// The main responsibility of this component is to handle the user's login process.
// This is the starting point for the login process. Any component that needs to authenticate
// a user can simply perform a redirect to this component with a returnUrl query parameter and
// let the component perform the login and return back to the return url.

@Options({
  props: {
    action: {
      type: String,
      default: LoginActions.Login,
    },
  },
})
export default class Login extends Vue {
  action = "";
  message: any = null;
  private loginActions = LoginActions;

  mounted() {
    const action = this.action;
    switch (action) {
      case this.loginActions.Login:
        this.login(this.getReturnUrl());
        break;
      case this.loginActions.LoginCallback:
        this.processLoginCallback();
        break;
      case this.loginActions.LoginFailed:
        {
          const params = new URLSearchParams(window.location.search);
          const error = params.get(QueryParameterNames.Message);
          this.message = error;
        }
        break;
      case this.loginActions.Profile:
        this.redirectToProfile();
        break;
      case this.loginActions.Register:
        this.redirectToRegister();
        break;
      default:
        throw new Error(`Invalid action '${action}'`);
    }
  }

  async login(returnUrl: string) {
    const result = (await authService.signIn(returnUrl)) as any;
    switch (result.status) {
      case AuthenticationResultStatus.Redirect:
        break;
      case AuthenticationResultStatus.Success:
        this.navigateToReturnUrl(returnUrl);
        break;
      case AuthenticationResultStatus.Fail:
        this.message = result.message;
        break;
      default:
        throw new Error(`Invalid status result ${result.status}.`);
    }
  }

  async processLoginCallback() {
    const url = window.location.href;
    const result = (await authService.completeSignIn(url)) as any;
    switch (result.status) {
      case AuthenticationResultStatus.Redirect:
        // There should not be any redirects as the only time completeSignIn finishes
        // is when we are doing a redirect sign in flow.
        throw new Error("Should not redirect.");
      case AuthenticationResultStatus.Success:
        await this.navigateToReturnUrl(this.getReturnUrl(result.state));
        break;
      case AuthenticationResultStatus.Fail:
        this.message = result.message;
        break;
      default:
        throw new Error(
          `Invalid authentication result status '${result.status}'.`
        );
    }
  }

  getReturnUrl(returnUrl?: string) {
    const params = new URLSearchParams(window.location.search);
    const fromQuery = params.get(QueryParameterNames.ReturnUrl);
    if (fromQuery && !fromQuery.startsWith(`${window.location.origin}/`)) {
      // This is an extra check to prevent open redirects.
      throw new Error(
        "Invalid return url. The return url needs to have the same origin as the current page."
      );
    }
    return returnUrl || fromQuery || `${window.location.origin}/`;
  }

  redirectToRegister() {
    this.redirectToApiAuthorizationPath(
      `${ApplicationPaths.IdentityRegisterPath}?${
        QueryParameterNames.ReturnUrl
      }=${encodeURI(ApplicationPaths.Login)}`
    );
  }

  redirectToProfile() {
    this.redirectToApiAuthorizationPath(ApplicationPaths.IdentityManagePath);
  }

  redirectToApiAuthorizationPath(apiAuthorizationPath: string) {
    const redirectUrl = `${window.location.origin}/${apiAuthorizationPath}`;
    // It's important that we do a replace here so that when the user hits the back arrow on the
    // browser they get sent back to where it was on the app instead of to an endpoint on this
    // component.
    window.location.replace(redirectUrl);
  }

  navigateToReturnUrl(returnUrl: string) {
    // It's important that we do a replace here so that we remove the callback uri with the
    // fragment containing the tokens from the browser history.
    window.location.replace(returnUrl);
  }
}
</script>
