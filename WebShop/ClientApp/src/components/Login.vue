<template>
  <div class="login-container">
    <div class="btn" @click="onLogInClick">Log in</div>
    <div class="btn">Sing up</div>
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
            id="username"
            type="text"
            v-model="username"
          />
          <label for="username">Username</label>
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
          <div class="btn">Login</div>
        </div>
      </template>
    </Dialog>
  </div>
</template>

<script lang="ts">
import { Vue, Options } from "vue-class-component";
import Dialog from "primevue/dialog";
import Button from "primevue/button";
import InputText from "primevue/inputtext";
import Password from "primevue/password";

@Options({
  components: {
    Dialog,
    Button,
    InputText,
    Password,
  },
})
export default class Login extends Vue {
  showDialog = false;
  username = "";
  password = "";

  onLogInClick() {
    this.showDialog = true;
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

.btn {
  text-decoration: none;
  cursor: pointer;
  color: #fff;
  border: 0.125em solid #fff;
  padding: 0.25em 1em;
  border-radius: 0.25em;
  transition: color 0.1s linear, border-color 0.2s linear;
}

.btn:hover {
  color: #000;
  border-color: #000;
}

.full-width,
.full-width > * {
  width: 100%;
}

.dialog-body {
}
.dialog-footer {
  width: 100%;
  display: flex;
  justify-content: space-between;
}

.dialog-footer .btn {
  border-color: #025844;
  color: #025844;
}
</style>