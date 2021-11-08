<template></template>

<script lang="ts">
import { ApplicationPaths } from "./ApiAuthorizationConstants";
import { QueryParameterNames } from "./ApiAuthorizationConstants";
import { LogoutActions } from "./ApiAuthorizationConstants";
import authService from "./AuthorizeService";
import { AuthenticationResultStatus } from "./AuthorizeService";
export default {
  props: {
    action: {
      type: String,
      required: true,
    },
  },
  data() {
    return {
      message: "",
      isReady: false,
      authenticated: false,
    };
  },
  mounted() {
    const action = this.action;
    switch (action) {
      case LogoutActions.Logout:
        if (!!window.history.state.state.local) {
          this.logout(this.getReturnUrl());
        } else {
          // This prevents regular links to <app>/authentication/logout from triggering a logout
          this.isReady = true;
          this.message = "The logout was not initiated from within the page.";
        }
        break;
      case LogoutActions.LogoutCallback:
        this.processLogoutCallback();
        break;
      case LogoutActions.LoggedOut:
        this.isReady = true;
        this.message = "You successfully logged out!";
        break;
      default:
        throw new Error(`Invalid action '${action}'`);
    }
  },
  methods: {
    async logout(returnUrl: string) {
      const state = { returnUrl };
      const isauthenticated = await authService.isAuthenticated();
      if (isauthenticated) {
        const result = await authService.signOut(state);
        switch (result.status) {
          case AuthenticationResultStatus.Redirect:
            break;
          case AuthenticationResultStatus.Success:
            this.navigateToReturnUrl(returnUrl);
            break;
          case AuthenticationResultStatus.Fail:
            this.message = (result as any).message;
            break;
          default:
            throw new Error("Invalid authentication result status.");
        }
      } else {
        this.message = "You successfully logged out!";
      }
    },
    async processLogoutCallback() {
      const url = window.location.href;
      const result = await authService.completeSignOut(url) as any;
      switch (result.status) {
        case AuthenticationResultStatus.Redirect:
          // There should not be any redirects as the only time completeAuthentication finishes
          // is when we are doing a redirect sign in flow.
          throw new Error("Should not redirect.");
        case AuthenticationResultStatus.Success:
          await this.navigateToReturnUrl(this.getReturnUrl(result?.state?.returnUrl));
          break;
        case AuthenticationResultStatus.Fail:
          this.message = (result as any).message;
          break;
        default:
          throw new Error("Invalid authentication result status.");
      }
    },
    async populateAuthenticationState() {
      const authenticated = await authService.isAuthenticated();
      this.isReady = true;
      this.authenticated = authenticated;
    },
    getReturnUrl(returnUrl?: string) {
      const params = new URLSearchParams(window.location.search);
      const fromQuery = params.get(QueryParameterNames.ReturnUrl);
      if (fromQuery && !fromQuery.startsWith(`${window.location.origin}/`)) {
        // This is an extra check to prevent open redirects.
        throw new Error(
          "Invalid return url. The return url needs to have the same origin as the current page."
        );
      }
      return (
        returnUrl ||
        fromQuery ||
        `${window.location.origin}${ApplicationPaths.LoggedOut}`
      );
    },
    navigateToReturnUrl(returnUrl: string) {
      return window.location.replace(returnUrl);
    },
  },
};
</script>