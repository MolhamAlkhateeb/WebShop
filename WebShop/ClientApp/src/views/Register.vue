<template>
  <div>
    <div class="p-fluid p-grid p-justify-center">
      <div class="p-grid p-col-12 p-md-4 p-mt-6">
        <div class="p-field p-col-12 p-md-12" :class="shakeClass('Firstname')">
          <span class="p-float-label">
            <InputText
              id="firstname"
              v-model="registerDto.Firstname"
              :class="invalidClass('Firstname')"
            />
            <label for="firstname">Firstname</label>
          </span>
          <small class="p-error">{{ errorFor("Firstname") }}</small>
        </div>
        <div class="p-field p-col-12 p-md-12" :class="shakeClass('Lastname')">
          <span class="p-float-label">
            <InputText
              id="lastname"
              v-model="registerDto.Lastname"
              :class="invalidClass('Lastname')"
            />
            <label for="lastname">Lastname</label>
          </span>
          <small class="p-error">{{ errorFor("Lastname") }}</small>
        </div>

        <div class="p-field p-col-12 p-md-12" :class="shakeClass('Email')">
          <span class="p-float-label">
            <InputText
              id="email"
              v-model="registerDto.Email"
              :class="invalidClass('Email')"
            />
            <label for="email">Email</label>
          </span>
          <small class="p-error">{{ errorFor("Email") }}</small>
        </div>

        <div class="p-field p-col-12 p-md-12" :class="shakeClass('Password')">
          <span class="p-float-label">
            <Password
              id="password"
              toggleMask
              v-model="registerDto.Password"
              :feedback="true"
              :class="invalidClass('Password')"
            />
            <label for="password">Password</label>
          </span>
          <small class="p-error">{{ errorFor("Password") }}</small>
        </div>

        <div
          class="p-field p-col-12 p-md-12"
          :class="shakeClass('ConfirmPassword')"
        >
          <span class="p-float-label">
            <Password
              id="confirmPassword"
              toggleMask
              v-model="registerDto.ConfirmPassword"
              :feedback="false"
              :class="invalidClass('ConfirmPassword')"
            />
            <label for="confirmPassword">Confirm Password</label>
          </span>
          <small class="p-error">{{ errorFor("ConfirmPassword") }}</small>
        </div>
        <!-- <div class="p-field-checkbox p-col-12 p-md-12">
            <Checkbox
              id="accept"
              name="accept"
              value="Accept"
              v-model="v$.accept.$model"
              :class="{ 'p-invalid': v$.accept.$invalid && submitted }"
            />
            <label
              for="accept"
              :class="{ 'p-error': v$.accept.$invalid && submitted }"
              >I agree to the
              <router-link to="/terms-and-conditions"
                >terms and conditions</router-link
              >
            </label>
          </div> -->

        <div class="p-field p-grid p-col-12 p-md-12 p-justify-center">
          <div class="p-col-12 p-md-4">
            <div class="btn" @click="register()">Register</div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { Vue, Options } from "vue-class-component";
import InputText from "primevue/inputtext";
import Password from "primevue/password";
import Checkbox from "primevue/checkbox";
import AuthService from "@/services/AuthenticationService";
import Register from "@/models/Register";
import ValidationProvider, {
  email,
  minLength,
  required,
  sameAs,
} from "@/validation/validators";

@Options({
  components: {
    InputText,
    Password,
    Checkbox,
  },
})
export default class Registration extends Vue {
  validationProvider?: ValidationProvider<Registration>;
  registerDto = new Register();
  accept = null;
  submitted = false;
  errorMsg?: string;
  shake = false;

  created() {
    const schema = {
      Firstname: [required],
      Lastname: [required],
      Email: [required, email],
      Password: [required, minLength(6)],
      ConfirmPassword: [required, minLength(6), sameAs("registerDto.Password")],
    };
    this.validationProvider = new ValidationProvider(
      this,
      schema,
      "registerDto"
    );
    this.validationProvider.refreshValidation(this.registerDto);
  }
  async register() {
    this.submitted = true;
    if (this.validationProvider?.isValid()) {
      try {
        await AuthService.Register(this.registerDto);
      } catch (error) {
        if (error.response.data.status == 409) {
          this.errorMsg = "User laready exists";
        }
      }
      //   if(isSuccess){
      //       await AuthService.Login()
      //   }
    } else {
      this.shake = true;
      setTimeout(() => {
        this.shake = false;
      }, 500);
    }
  }

  errorFor(field: string) {
    return this.submitted ? this.validationProvider?.errorsFor(field)[0] : "";
  }
  invalidClass(field: string) {
    return this.submitted && !this.validationProvider?.isFieldValid(field)
      ? "p-invalid"
      : "";
  }

  shakeClass(field: string) {
    return this.submitted &&
      !this.validationProvider?.isFieldValid(field) &&
      this.shake
      ? "shake"
      : "";
  }
}
</script>

<style scoped>

</style>
