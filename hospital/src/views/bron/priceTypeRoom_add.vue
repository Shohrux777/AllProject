<template>
  <div>
    <loader v-if="loading" />
    <form v-on:submit.prevent="submit">
      <div class="addxizmat px-2 pb-2">
        <div class="row">
          <div class="col-12">
            <mdb-input :label="$t('name')" v-model="name" outline />
            <small
              class="invalid-text ml-1"
              v-if="$v.name.$dirty && !$v.name.required"
            >
              {{ $t("name_invalid_text") }}
            </small>



            <mdb-input :label="$t('room_price')" v-model="price" outline type="number"/>
            <small
              class="invalid-text ml-1"
              v-if="$v.price.$dirty && !$v.price.required"
            >
              {{ $t("price_invalid") }}
            </small>


            <mdb-input :label="$t('patient_price')" v-model="patient_price" type="number" outline/>
            <small class="invalid-text ml-1" v-if="$v.patient_price.$dirty && !$v.patient_price.required">
              {{ $t("price_invalid") }}
            </small>


            <mdb-input :label="$t('no_patient_price')" type="number" v-model="no_patient_price" outline />
              <small
                class="invalid-text ml-1"
                v-if="$v.no_patient_price.$dirty && !$v.no_patient_price.required"
              >
                {{ $t("price_invalid") }}
              </small>

            <!-- <mdb-input :label="$t('note')" v-model="note" outline /> -->
            
          </div>

          <div class="text-right container" style="margin-top: -20px;">
            <mdb-btn
              color="success px-4 py-2"
              type="submit"
              style="margin-top: 28px; font-size: 10px"
              >Добавить</mdb-btn
            >
          </div>
        </div>
      </div>
    </form>
    <Toast ref="message"></Toast>
    <AlertError ref="msg"></AlertError>

    <massage_box
      :hide="modal_status"
      :detail_info="modal_info"
      :m_text="$t('Failed_to_add')"
      @to_hide_modal="modal_status = false"
    />
  </div>
</template>
<script>
import { mdbBtn, mdbInput } from "mdbvue";
import { required } from "vuelidate/lib/validators";

import { mapActions, mapGetters } from "vuex";

export default {
  components: {
    mdbBtn,
    mdbInput,
  },
  data() {
    return {
      modal_info: "",
      modal_status: false,
      loading: false,

      name: '',
      note: "",
      price: null,
      patient_price: null,
      no_patient_price: null,
      id: 0,
    };
  },
  validations: {
    name: { required },
    price: { required },
    patient_price: { required },
    no_patient_price: { required },

  },
  props: {
    options: {
      type: Object,
      default() {
        return {};
      },
    },
  },
  computed: mapGetters(["get_province_list"]),
  async mounted() {
    await this.options;
    if (Object.keys(this.options).length != 0) {
      this.update();
    } else {
      this.cls_wnd();
    }
  },
  methods: {
    ...mapActions([ "fetch_price_type_room"]),
    update() {
      console.log("da salom");
      console.log(this.options);
      this.id = this.options.id;
      this.name = this.options.name;
      this.price = this.options.room_price;
      this.patient_price = this.options.room_bed_price;
      this.no_patient_price = this.options.room_bed_price_not_patient;
      
    },

    cls_wnd() {
      this.price = null;
      this.patient_price = null;
      this.no_patient_price = null;
      this.name = "";
      this.note = "";
      this.id = 0;
    },
    async submit() {
      if (this.$v.$invalid) {
        this.$v.$touch();
        this.$refs.message.warning("please_fill");
        return false;
      }
      const requestOptions = {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          name: this.name,
          room_price: this.price,
          room_bed_price: this.patient_price,
          room_bed_price_not_patient: this.no_patient_price,
          // note: this.note,
          id: this.id,
          // "code" : 0
        }),
      };
      this.loading = true;
      const response = await fetch(
        this.$store.state.hostname + "/HospitalRoomTypes", requestOptions
      );
      const data = await response.json();
      this.loading = false;
      if (data.id) {
        this.cls_wnd();
        this.fetch_price_type_room();
        this.$refs.message.success("Added_successfully");
      } else {
        this.$refs.msg.error("Error_successfully");
        return false;
      }
    },
  },
};
</script>

<style scoped>
</style>