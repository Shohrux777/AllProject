<template>
  <div>
    <loader v-if="loading" />
    <div class="border-bottom">
      <router-link to="/hisob">
        <h5 class="m-0 ml-3 d-flex" style="padding: 16px 0px">
          <svg
            xmlns="http://www.w3.org/2000/svg"
            class="icon icon-tabler icon-tabler-chevron-left mr-2"
            width="24"
            height="24"
            viewBox="0 0 24 24"
            stroke-width="2.5"
            stroke="#007BFF"
            fill="none"
            stroke-linecap="round"
            stroke-linejoin="round"
          >
            <path stroke="none" d="M0 0h24v24H0z" fill="none" />
            <polyline points="15 6 9 12 15 18" />
          </svg>
          {{ $t("hisob_add") }}
        </h5>
      </router-link>
    </div>
    <div class="user_add p-5">
      <form @submit.prevent="submit">
        <mdb-row>
          <mdb-col col="3">
            <p class="p-0 m-0 mt-2" style="font-size: 14px">{{ $t("name") }}</p>
          </mdb-col>
          <mdb-col col="5">
            <mdb-input
              class="m-0 p-0"
              v-model="name"
              size="md"
              outline
              group
              type="text"
              validate
              error="wrong"
              success="right"
            />
            <small
              class="invalid-text pt-4"
              style="margin-left: 5px"
              v-if="$v.name.$dirty && !$v.name.required"
            >
              {{ $t("name_invalid_text") }}
            </small>
            <small
              class="p-0"
              style="margin-left: 5px; font-size: 12px; color: gray"
              v-else
            >
              Hisobni nomini kiriting. 
            </small>
          </mdb-col>
        </mdb-row>
        <mdb-row class="mt-4" v-show="false">
          <mdb-col col="3">
            <p class="p-0 m-0 mt-2" style="font-size: 14px">{{ $t("hisob_num") }}</p>
          </mdb-col>
          <mdb-col col="5">
            <mdb-input
              class="m-0 p-0"
              v-model="hisob_num"
              size="md"
              outline
              group
              type="text"
              validate
              error="wrong"
              success="right"
            />
          </mdb-col>
        </mdb-row>
        <!-- <mdb-row class="mt-4">
          <mdb-col col="3">
            <p class="p-0 m-0 mt-2" style="font-size: 14px;">{{$t('phoneNumber')}}</p>
          </mdb-col>
          <mdb-col col="5">
            <mdb-input class="m-0 p-0" v-model="phoneNumber" size="md" outline  group type="text" validate error="wrong" success="right"/>
          </mdb-col>
        </mdb-row>  -->
        <!-- <mdb-row class="mt-4">
          <mdb-col col="3">
            <p class="p-0 m-0 mt-2" style="font-size: 14px;">{{$t('address')}}</p>
          </mdb-col>
          <mdb-col col="5">
            <mdb-input class="m-0 p-0" v-model="address" size="md" outline  group type="text" validate error="wrong" success="right"/>
          </mdb-col>
        </mdb-row> -->
        <div class="blue-gradient">
          <hr class="mt-5" />
        </div>
        <mdb-row class="mt-4">
          <mdb-col col="8">
            <div class="mt-2 text-right">
              <mdb-btn
                @click="submit_continue"
                color="primary"
                m="r3"
                style="font-size: 10.5px"
                p="r4 l4 t2 b2"
              >
                {{ $t("Add_and_continue") }}
              </mdb-btn>
              <mdb-btn
                color="success"
                type="submit"
                style="font-size: 10.5px"
                p="r4 l4 t2 b2"
              >
                <mdb-icon />{{ $t("add") }}</mdb-btn
              >
            </div>
          </mdb-col>
        </mdb-row>
      </form>
      <massage_box
        :hide="modal_status"
        :detail_info="modal_info"
        :m_text="$t('Failed_to_add')"
        @to_hide_modal="modal_status = false"
      />

      <Toast ref="message"></Toast>
    </div>
  </div>
</template>

<script>
import { mdbInput, mdbRow, mdbCol, mdbIcon, mdbBtn } from "mdbvue";
import { required } from "vuelidate/lib/validators";
export default {
  naem: "companyAdd",

  data() {
    return {
      modal_info: "",
      modal_status: false,
      loading: false,
      id: this.$route.params.id,

      name: "",
      address: "",
      phoneNumber: "",
      hisob_num: 0,
      user_id: null,
      user_name: '',
    };
  },
  components: {
    mdbInput,
    mdbRow,
    mdbCol,
    mdbIcon,
    mdbBtn,
  },
  validations: {
    name: {
      required,
    },
  },
  async created() {
    if (this.id > 0) {
      const res = await fetch(
        this.$store.state.hostname + "/TegirmonHisoblar/" + this.id
      );
      const data = await res.json();
      this.name = data.name;
      this.hisob_num = data.num_1;
      this.user_id = data.tegirmonUserid;
      this.user_name = data.text_1;
    }
  },
  methods: {
    cls_wnd() {
      this.name = "";
    },
    save_data: async function () {
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
          auth_user_creator_id: localStorage.AuthId,
          num_1: this.hisob_num,
          text_1: this.user_name,
          tegirmonUserid: this.user_id,
          id: this.id,
        }),
      };
      try {
        this.loading = true;
        const response = await fetch(
          this.$store.state.hostname + "/TegirmonHisoblar",
          requestOptions
        );
        const data = await response.text();
        this.loading = false;
        if (response.status == 201 || response.status == 200) {
          this.$refs.message.success("Added_successfully");
          return true;
        } else {
          this.modal_info = data;
          this.modal_status = true;
          return false;
        }
      } catch {
        this.loading = false;
        this.modal_info = this.$i18n.t("network_ne_connect");
        this.modal_status = true;
      }
    },
    async submit() {
      if (await this.save_data()) {
        this.$store.state.alert = true;
        this.$router.push("/hisob");
      }
    },
    async submit_continue() {
      if (await this.save_data()) {
        if (this.id != 0) {
          this.$router.push("/hisob_add/0");
          this.cls_wnd();
        } else {
          this.cls_wnd();
        }
      }
    },
  },
};
</script>

<style>
</style>