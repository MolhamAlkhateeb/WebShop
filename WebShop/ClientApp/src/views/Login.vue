<template>
  <div v-if="!isSignedIn" class="login-form">
    <span class="login-form__header">Login</span>
    <div class="login-form__content">
      <span class="p-float-label login-form__input">
        <InputText class="full-width" id="email" type="text" v-model="email" />
        <label for="email" class="login-form__email-input-label">Email</label>
      </span>
      <span class="p-float-label full-width login-form__input">
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

    <div class="btn" @click="Login">Login</div>
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
  email = "";
  password = "";
  isSignedIn = false;
  firstname = "";
  lastname = "";

  async mounted() {
    this.isSignedIn = AuthService.isSignedIn();
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
      this.$router.push({ name: "Home" }); //TODO check in home page if user is already logged in
    }
  }

  async onLogOutClick() {
    AuthService.logout();
    this.isSignedIn = false;
  }
}
</script>

<style scoped lang="scss">
.login-form {
  margin: auto;
  display: flex;
  flex-direction: column;
  margin-bottom: 50px;
  width: 500px;
  .btn {
    font-size: 1.3em;
    width: 7em;
    align-self: center;
  }
  &__header {
    color: var(--var-main-color);
    font-size: 1.5em;
    font-weight: 700;
    padding-top: 20px;
  }
  &__input {
    padding: 0 20px;
    margin: 35px 0;
    width: 100%;
    label {
      text-align: left;
      margin-left: 30px;
    }
    & * {
      width: 100%;
    }
  }
}
.username-tag {
  color: var(--var-secondary-color);
  display: inline-block;
}
.username-tag span {
  display: inline-block;
  vertical-align: middle;
}

@media screen and (max-width: 600px) {
  .login-form {
    width: 100%;
  }
}
</style>
