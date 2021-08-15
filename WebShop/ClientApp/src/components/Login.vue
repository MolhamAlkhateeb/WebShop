<template>
  <div v-if="!isSignedIn" class="login-container">
    <div class="btn-reverse" @click="onLogInClick">Log in</div>
    <router-link class="btn-reverse" to="/register">Sing up</router-link>
    <!-- <router-link to="/">Log in</router-link>
    <router-link to="/">Sign up</router-link> -->

    <Dialog
      v-model:visible="showDialog"
      :modal="true"
      :showHeader="true"
      :breakpoints="{ '960px': '50vw', '640px': '100vw' }"
      :style="{ width: '30vw' }"
    >
      <template #header>
        <h3>Login</h3>
      </template>
      <h5></h5>
      <div class="dialog-body">
        <span class="p-float-label">
          <InputText
            class="full-width"
            id="email"
            type="text"
            v-model="email"
          />
          <label for="email">Email</label>
        </span>
        <h5></h5>
        <span class="p-float-label full-width">
          <Password
            inputStyle="width:100%"
            id="password"
            v-model="password"
            toggleMask
            :feedback="false"
          ></Password>
          <label for="password">Password</label>
        </span>
      </div>

      <template #footer>
        <div class="dialog-footer">
          <div class="btn" @click="showDialog = false">Cancel</div>
          <div class="btn" @click="Login">Login</div>
        </div>
      </template>
    </Dialog>
  </div>

  <div v-if="isSignedIn" class="login-container">
    <div class="username-tag">
      <span>Hello {{ firstname }} {{ lastname }}</span>
    </div>
    <div class="btn-reverse" @click="onLogOutClick">Log Out</div>
  </div>
</template>

<script lang="ts">
import { Vue, Options } from "vue-class-component";
import Dialog from "primevue/dialog";
import Button from "primevue/button";
import InputText from "primevue/inputtext";
import Password from "primevue/password";
import AuthService from "@/services/AuthenticationService";

@Options({
  components: {
    Dialog,
    Button,
    InputText,
    Password
  }
})
export default class Login extends Vue {
  showDialog = false;
  email = "";
  password = "";
  isSignedIn = false;
  firstname = "";
  lastname = "";

  async mounted() {
    this.isSignedIn = AuthService.isSignedIn();
  }

  onLogInClick() {
    this.showDialog = true;
  }
  async Login() {
    const loginResponse = await AuthService.Login({
      Password: this.password,
      Email: this.email
    });
    if (loginResponse) {
      this.isSignedIn = true;
      this.firstname = loginResponse.firstname;
      this.lastname = loginResponse.lastname;
    }
  }

  async onLogOutClick() {
    AuthService.logout();
    this.isSignedIn = false;
  }
}
</script>

<style scoped>
.login-container {
  display: flex;
}

.login-container > *:not(:last-child) {
  margin-right: 1em;
}

.full-width,
.full-width > * {
  width: 100%;
}

.dialog-footer {
  width: 100%;
  display: flex;
  justify-content: space-between;
}

.dialog-footer .btn {
  border-color: var(--var-main-color);
  color: var(--var-main-color);
}

.username-tag {
  color: var(--var-secondary-color);
  display: inline-block;
}
.username-tag span {
  display: inline-block;
  vertical-align: middle;
}
</style>
