<template>
  <Toast position="top-center" />
  <Dialog
    v-model:visible="visible"
    :modal="true"
    :showHeader="true"
    :breakpoints="{ '960px': '50vw', '640px': '100vw' }"
    :style="{ width: '30vw' }"
  >
    <template #header>
      <div>
        <h3>Get a Quote</h3>
      </div>
    </template>
    <div class="p-grid p-fluid p-justify-center p-col-12">
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
          <Dropdown
            v-model="selectedCountry"
            :options="countries"
            optionLabel="name"
            :filter="true"
            placeholder="Code"
            class="dropdown"
          >
            <template #value="slotProps">
              <div class="dropdown-item" v-if="slotProps.value">
                <img
                  :alt="slotProps.value.nativeName"
                  :src="slotProps.value.flag"
                />
                <span>{{ slotProps.value.callingCodes[0] }}</span>
              </div>
              <span v-else>
                {{ slotProps.placeholder }}
              </span>
            </template>
            <template #option="slotProps">
              <div class="dropdown-item dropdown-options">
                <img
                  :alt="slotProps.option.nativeName"
                  :src="slotProps.option.flag"
                />
                <span>{{ slotProps.option.callingCodes[0] }}</span>
              </div>
            </template>
          </Dropdown>
          <span class="p-float-label">
            <InputText
              id="phone"
              v-model="quote.phone"
              :class="invalidClass('phone')"
            />
            <label for="phone">Phone</label>
          </span>
        </div>
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
    </div>

    <template #footer>
      <div class="p-grid p-justify-between p-mt-2">
        <div class="btn" @click="visible = false">Cancel</div>
        <div class="btn" @click="sendClick()">Send</div>
      </div>
    </template>
  </Dialog>
</template>
<script lang="ts">
import axios from "axios";
import { Options, Vue } from "vue-class-component";
import Dialog from "primevue/dialog";
import InputText from "primevue/inputtext";
import InputNumber from "primevue/inputnumber";
import Quote from "@/models/Quote";
import Textarea from "primevue/textarea";
import Toast from "primevue/toast";
import Button from "primevue/button";
import Dropdown from "primevue/dropdown";
import ValidationProvider, { email, required } from "@/validation/validators";
import CountriesService from "@/services/CountriesService";
import Country from "@/models/Country";

@Options({
  props: {
    show: Boolean,
    productId: Number,
    itemNumber: String,
  },
  components: {
    Dialog,
    InputText,
    InputNumber,
    Quote,
    Textarea,
    Toast,
    Button,
    Dropdown,
  },
  emits: ["update:show"],
})
export default class QuoteDialog extends Vue {
  quote = new Quote();
  show!: boolean;
  productId?: number;
  itemNumber?: string;
  submitted = false;
  shake = false;
  countries?: Array<Country>;
  selectedCountry: Country | null = null;

  validationProvider?: ValidationProvider<QuoteDialog>;

  created() {
    const validationSchema = {
      firstname: [required],
      lastname: [required],
      phone: [required],
      email: [required, email],
      quantity: [required],
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

  get visible() {
    return this.show;
  }
  set visible(value: boolean) {
    this.$emit("update:show", value);
  }

  async sendClick() {
    this.quote.productId = this.productId;
    this.quote.itemNumber = this.itemNumber;
    this.submitted = true;
    if (this.validationProvider?.isValid()) {
      await this.send();
    } else {
      this.refreshShake();
    }
  }
  async send() {
    try {
      const response = await axios.post<boolean>(`/api/orderquote`, this.quote);
      if (response) {
        this.showEmailSentSuccess();
      } else {
        this.showEmailFailed();
      }
    } catch (error) {
      this.showEmailFailed();
    } finally {
      this.visible = false;
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
.dropdown {
  min-width: 30%;
}
.dropdown-item {
  img {
    width: 17px;
    margin-right: 0.5rem;
  }
}

// When i wrote this code, only me and God knows how it works.
// Now only God knows how it works.
.dropdown-options {
  max-width: 0;
}
</style>
