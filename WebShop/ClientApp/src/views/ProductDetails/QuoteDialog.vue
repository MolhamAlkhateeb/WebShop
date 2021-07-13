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
      <h3>Get a Quote</h3>
    </template>
    <h5></h5>
    <div class="p-fluid">
      <div class="p-field p-mb-4">
        <span class="p-float-label">
          <InputText id="quote-firstname" v-model="quote.firstname" />
          <label for="quote-firstname">Firstname</label>
        </span>
        <small v-if="submitted" class="p-error">{{
          validationProvider.errorsFor("firstname")[0]
        }}</small>
      </div>

      <div class="p-field p-mb-4">
        <span class="p-float-label">
          <InputText id="quote-lastname" v-model="quote.lastname" />
          <label for="quote-lastname">Lastname</label>
        </span>
        <small v-if="submitted" class="p-error">{{
          validationProvider.errorsFor("lastname")[0]
        }}</small>
      </div>

      <div class="p-field p-mb-4">
        <span class="p-float-label">
          <InputText id="quote-phone" v-model="quote.phone" />
          <label for="quote-phone">Phone</label>
        </span>
        <small v-if="submitted" class="p-error">{{
          validationProvider.errorsFor("phone")[0]
        }}</small>
      </div>

      <div class="p-field">
        <span class="p-float-label">
          <InputText id="quote-email" v-model="quote.email" />
          <label for="quote-email">Email</label>
        </span>
        <small v-if="submitted" class="p-error">{{
          validationProvider.errorsFor("email")[0]
        }}</small>
      </div>

      <div class="p-field p-mb-4">
        <span class="p-float-label">
          <InputNumber
            id="quote-quantity"
            v-model="quote.quantity"
            showButtons
            :min="1"
          />
          <label for="quote-quantity">Quantity</label>
        </span>
        <small v-if="submitted" class="p-error">{{
          validationProvider.errorsFor("quantity")[0]
        }}</small>
      </div>

      <div class="p-field p-mb-4">
        <span class="p-float-label">
          <Textarea
            id="quote-message"
            :autoResize="true"
            v-model="quote.message"
          />
          <label for="quote-message">Optional Message</label>
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
import ValidationProvider, { email, required } from "@/validation/validators";

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
  },
  emits: ["update:show"],
})
export default class QuoteDialog extends Vue {
  quote = new Quote();
  show!: boolean;
  productId?: number;
  itemNumber?: string;
  submitted = false;

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
      summary: "Success Message error",
      details: "MEsage detail error",
      life: 3000,
    });
  }

  restForm() {
    this.quote = new Quote();
  }
}
</script>
