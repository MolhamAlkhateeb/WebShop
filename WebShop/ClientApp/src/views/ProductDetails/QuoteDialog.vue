<template>
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
      <div class="p-field p-grid p-float-label p-mb-4">
        <InputText id="quote-firstname" v-model="quote.firstname" />
        <label for="quote-firstname">Firstname</label>
      </div>
      <div class="p-field p-grid p-float-label p-mb-4">
        <InputText id="quote-lastname" v-model="quote.lastname" />
        <label for="quote-lastname">Lastname</label>
      </div>

      <div class="p-field p-grid p-float-label p-mb-4">
        <InputText id="quote-phone" v-model="quote.phone" />
        <label for="quote-phone">Phone</label>
      </div>

      <div class="p-field p-grid p-float-label p-mb-4">
        <InputText id="quote-email" v-model="quote.email" />
        <label for="quote-email">Email</label>
      </div>

      <div class="p-field p-grid p-float-label p-mb-4">
        <InputNumber
          id="quote-quantity"
          v-model="quote.quantity"
          showButtons
          :min="1"
        />
        <label for="quote-quantity">Quantity</label>
      </div>

      <div class="p-field p-grid p-float-label p-mb-4">
        <Textarea
          id="quote-message"
          :autoResize="true"
          v-model="quote.message"
        />
        <label for="quote-message">Optional Message</label>
      </div>
    </div>

    <template #footer>
      <div class="p-grid p-justify-between">
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
  },
  emits: ["update:show"],
})
export default class QuoteDialog extends Vue {
  quote = new Quote();
  show!: boolean;
  productId?: number;
  itemNumber?: string;

  get visible() {
    return this.show;
  }
  set visible(value: boolean) {
    this.$emit("update:show", value);
  }

  async sendClick() {
    this.quote.productId = this.productId;
    this.quote.itemNumber = this.itemNumber;
    await this.send()
  }
  async send() {
    //TODO: send a quote to the backend
    const response = await axios.post(`/api/orderquote`, this.quote)
    console.log(this.quote);
  }

  restForm() {
    this.quote = new Quote();
  }
}
</script>
