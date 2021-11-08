<template>
  <div class="p-grid p-fluid p-nogutter p-col-12 p-xl-offset-1 p-xl-10">
    <div class="p-field p-col-12 p-md-12" :class="shakeClass('firstname')">
      <span class="p-float-label">
        <InputText
          id="firstname"
          v-model="quote.firstname"
          :class="invalidClass('firstname')"
        />
        <label for="firstname">Firstname</label>
      </span>
      <small class="p-error">{{ errorFor("firstname") }}</small>
    </div>
    <div class="p-field p-col-12 p-md-12" :class="shakeClass('lastname')">
      <span class="p-float-label">
        <InputText
          id="lastname"
          v-model="quote.lastname"
          :class="invalidClass('lastname')"
        />
        <label for="lastname">Lastname</label>
      </span>
      <small class="p-error">{{ errorFor("lastname") }}</small>
    </div>

    <div class="p-field p-col-12 p-md-12" :class="shakeClass('phone')">
      <div class="p-inputgroup">
        <select
          class="phone-code-select p-col-4 p-xl-2"
          :class="invalidClass('countryCode')"
          v-model="quote.countryCode"
        >
          <option
            v-for="(country, index) in countries"
            :key="`get-quote-form-${index}`"
            :value="country.callingCodes[0]"
          >
            {{ `${country.name} (+${country.callingCodes[0]})` }}
          </option>
        </select>
        <span class="p-float-label">
          <InputText
            id="phone"
            v-model="quote.phone"
            :class="invalidClass('phone')"
          />
          <label for="phone">Phone</label>
        </span>
      </div>
      <small class="p-error">{{ errorFor("countryCode") }}</small>
      <small class="p-error">{{ errorFor("phone") }}</small>
    </div>

    <div class="p-field p-col-12 p-md-12" :class="shakeClass('email')">
      <span class="p-float-label">
        <InputText
          id="email"
          v-model="quote.email"
          :class="invalidClass('email')"
        />
        <label for="email">Email</label>
      </span>
      <small class="p-error">{{ errorFor("email") }}</small>
    </div>
    <div class="p-field p-col-12 p-md-12" :class="shakeClass('quantity')">
      <span class="p-float-label">
        <InputNumber
          id="quantity"
          v-model="quote.quantity"
          showButtons
          :min="1"
        />
        <label for="quantity">Quantity</label>
      </span>
      <small class="p-error">{{ errorFor("quantity") }}</small>
    </div>

    <div class="p-field p-col-12 p-md-12">
      <span class="p-float-label">
        <Textarea id="message" :autoResize="true" v-model="quote.message" />
        <label for="message">Message</label>
      </span>
    </div>
    <div class="p-field p-col-12 p-md-12">
      <div class="btn" @click="sendClick()">Send</div>
    </div>
  </div>
</template>

<script lang="ts">
import { Options, Vue } from "vue-class-component";
import Quote from "@/models/Quote";
import ValidationProvider, { email, required } from "@/validation/validators";
import CountriesService from "@/services/CountriesService";
import Country from "@/models/Country";

import Dialog from "primevue/dialog";
import InputText from "primevue/inputtext";
import InputNumber from "primevue/inputnumber";
import Textarea from "primevue/textarea";
import Toast from "primevue/toast";
import Button from "primevue/button";
import Dropdown from "primevue/dropdown";
import axios from "axios";

@Options({
  components: {
    Dialog,
    InputText,
    InputNumber,
    Textarea,
    Button,
    Dropdown,
  },
})
export default class GetQuoteForm extends Vue {
  quote = new Quote();
  show!: boolean;
  productId?: number;
  itemNumber?: string;
  submitted = false;
  shake = false;
  countries = new Array<Country>();

  validationProvider?: ValidationProvider<GetQuoteForm>;

  created() {
    const validationSchema = {
      firstname: [required],
      lastname: [required],
      phone: [required],
      email: [required, email],
      quantity: [required],
      countryCode: [required],
    };
    this.validationProvider = new ValidationProvider(
      this,
      validationSchema,
      "quote"
    );
    this.validationProvider.refreshValidation(this.quote);
  }

  async mounted() {
    this.countries = await CountriesService.getAll();
  }
  async sendClick() {
    this.quote.productId = this.productId;
    this.quote.itemNumber = this.itemNumber;
    console.log(this.quote);

    this.submitted = true;
    if (this.validationProvider?.isValid()) {
      await this.send();
    } else {
      this.refreshShake();
    }
  }
  async send() {
    try {
      this.quote.phone = this.quote.countryCode! + this.quote.phone!;
      const response = await axios.post<boolean>(`/api/orderquote`, this.quote);
      if (response) {
        this.showEmailSentSuccess();
      } else {
        this.showEmailFailed();
      }
    } catch (error) {
      this.showEmailFailed();
    } finally {
      this.restForm();
    }
  }

  showEmailSentSuccess() {
    this.$toast.add({
      severity: "success",
      summary: "Success Message",
      details: "MEsage detail",
      life: 3000,
    });
  }
  showEmailFailed() {
    this.$toast.add({
      severity: "error",
      summary: "Success message error",
      details: "Mesage detail error",
      life: 3000,
    });
  }

  restForm() {
    this.quote = new Quote();
  }

  errorFor(field: string) {
    return this.submitted ? this.validationProvider?.errorsFor(field)[0] : "";
  }

  invalidClass(field: string) {
    return this.submitted && !this.validationProvider?.isFieldValid(field)
      ? "p-invalid"
      : "";
  }

  refreshShake() {
    this.shake = true;
    setTimeout(() => {
      this.shake = false;
    }, 500);
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

<style lang="scss" scoped>
.phone-code-select {
  font-family: -apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, Helvetica,
    Arial, sans-serif, "Apple Color Emoji", "Segoe UI Emoji", "Segoe UI Symbol";
  font-size: 1rem;
  color: #495057;
  background: #ffffff;
  padding: 0.5rem 0.5rem;
  border: 1px solid #ced4da;
  transition: background-color 0.2s, color 0.2s, border-color 0.2s,
    box-shadow 0.2s;
  -webkit-appearance: none;
  -moz-appearance: none;
  appearance: none;
  border-radius: 3px;
  box-sizing: border-box;
}

.phone-code-select.p-invalid {
  border-color: #f44336;
}

.phone-code-select:focus-visible {
  outline-color: #2196f3;
}
</style>