<template>
  <div>
    <div class="container">
      <div class="p-fluid p-grid">
        <div class="p-field p-col-12 p-md-4"></div>
        <div class="p-field p-col-12 p-md-4">
          <div class="p-field p-col-12 p-md-12">
            <Field
              name="firstname"
              :rules="required"
              v-slot="{ field, errors }"
              v-model="firstname"
            >
              <span class="p-float-label">
                <InputText id="firstname" type="text" v-bind="field" />
                <label v-if="!firstname" for="firstname">Firstname</label>
              </span>
              <small class="p-error">{{ errors[0] }}</small>
            </Field>
          </div>
          <div class="p-field p-col-12 p-md-12">
            <Field
              name="lastname"
              :rules="required"
              v-slot="{ field, errors }"
              v-model="lastname"
            >
              <span class="p-float-label">
                <InputText id="lastname" type="text" v-bind="field" />
                <label v-if="!lastname" for="lastname">Lastname</label>
              </span>
              <small class="p-error">{{ errors[0] }}</small>
            </Field>
          </div>

          <div class="p-field p-col-12 p-md-12">
            <Field
              name="email"
              :rules="validateEmail"
              v-slot="{ field, errors }"
              v-model="email"
            >
              <span class="p-float-label">
                <InputText id="email" type="text" v-bind="field" />
                <label for="email" v-if="!email">Email</label>
              </span>
              <small class="p-error">{{ errors[0] }}</small>
            </Field>
          </div>

          <div class="p-field p-col-12 p-md-12">
            <Field
              name="password"
              :rules="required"
              v-slot="{ field, errors }"
              v-model="password"
            >
              <span class="p-float-label">
                <Password
                  id="password"
                  toggleMask
                  v-bind="field"
                  :feedback="true"
                />
                <label for="password" v-if="!password">Password</label>
              </span>
              <small class="p-error">{{ errors[0] }}</small>
            </Field>
          </div>

          <div class="p-field p-col-12 p-md-12">
            <Field
              name="confirmPassword"
              :rules="validateConfirmPassword"
              v-slot="{ field, errors }"
              v-model="confirmPassword"
            >
              <span class="p-float-label">
                <Password id="confirmPassword" toggleMask v-bind="field" />
                <label for="confirmPassword" v-if="!confirmPassword"
                  >Confirm Password</label
                >
              </span>
              <small class="p-error">{{ errors[0] }}</small>
            </Field>
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

          <div class="p-field p-col-12 p-md-12 flex">
            <div class="p-col-12 p-md-4"></div>
            <div class="p-col-12 p-md-4">
              <div class="btn" @click="register()">Register</div>
            </div>
            <div class="p-col12 p-md-4"></div>
          </div>
        </div>

        <div class="p-field p-col-12 p-md-4"></div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
interface FieldData {
  field: string;
  value: string;
}
import { Vue, Options } from "vue-class-component";
import InputText from "primevue/inputtext";
import Password from "primevue/password";
import Checkbox from "primevue/checkbox";
import { Field } from "vee-validate/dist/vee-validate";
import AuthService from "@/services/AuthenticationService";
import Register from "@/models/Register";

@Options({
  components: {
    InputText,
    Password,
    Checkbox,
    Field,
  },
})
export default class Registration extends Vue {
  //   v$ = useVuelidate();

  firstname = "";
  lastname = "";
  email = "";
  password = "";
  confirmPassword = "";
  accept = null;
  submitted = false;
  unvalidFields = new Array<FieldData>();
  errorMsg = "";

  async register() {
    if (this.unvalidFields.length < 1) {
      const registerModel: Register = new Register();
      registerModel.Firstname = this.firstname;
      registerModel.Lastname = this.lastname;
      registerModel.Password = this.password;
      registerModel.Email = this.email;
      registerModel.ConfirmPassword = this.confirmPassword;
      try {
        await AuthService.Register(registerModel);
      } catch (error) {
        if (error.response.data.status == 409) {
          this.errorMsg = "User laready exists";
        }
      }
      //   if(isSuccess){
      //       await AuthService.Login()
      //   }
    }
  }

  addUnvalidField(data: FieldData) {
    const fieldExists = this.unvalidFields.find((d) => d.field == data.field);
    if (!fieldExists) {
      this.unvalidFields.push(data);
    }
  }

  removeUnvalidField(data: FieldData) {
    this.unvalidFields = this.unvalidFields.filter(
      (v) => v.field != data.field
    );
  }

  required(value: any, field: any) {
    if (value && value.trim()) {
      this.removeUnvalidField(field);
      return true;
    }

    this.addUnvalidField(field);
    return "This field is required";
  }

  validateEmail(value: string, data: FieldData) {
    // if the field is empty
    if (!value) {
      this.addUnvalidField(data);
      return "This field is required";
    }

    // if the field is not a valid email
    if (!/^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$/i.test(value)) {
      this.addUnvalidField(data);
      return "This field must be a valid email";
    }

    this.removeUnvalidField(data);
    // All is good
    return true;
  }

  validateConfirmPassword(value: string, data: FieldData) {
    if (!value) {
      this.addUnvalidField(data);
      return "This field is required";
    }

    if (value != this.password) {
      this.addUnvalidField(data);
      return "Passwords do not match";
    }
    this.removeUnvalidField(data);
    return true;
  }
}
</script>

<style scoped>
.country-item {
  display: flex;
}
.flex {
  display: flex;
}
</style>
