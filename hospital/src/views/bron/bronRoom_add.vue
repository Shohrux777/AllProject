<template>
  <div>
    <loader v-if="loading" />
    <form v-on:submit.prevent="submit">
      <div class="addxizmat px-2 pb-2">
        <div class="row">
          <div class="col-12">
            <mdb-input :label="$t('name')" v-model="name" outline />
            <small
              class="invalid-text"
              v-if="$v.name.$dirty && !$v.name.required"
            >
              {{ $t("name_invalid_text") }}
            </small>
            <mdb-input
              :label="$t('room_beds_count')"
              v-model="room_beds_count"
              type="number"
              outline
            />
            <small
              class="invalid-text"
              v-if="$v.room_beds_count.$dirty && !$v.room_beds_count.required"
            >
              {{ $t("name_invalid_text") }}
            </small>
            <!-- <mdb-input :label="$t('room_type')" v-model="room_type" outline /> -->

            <mdb-input :label="$t('note')" v-model="note" outline />
            <lineSelect
              :options="price_type_room.rows"
              :searchshow="true"
              @select="selectRoomType"
              :selected="roomType_name"
              :label="$t('room_type')"
              />
              <small
                class="invalid-text mt-0"
                v-if="$v.roomType_name.$dirty && !$v.roomType_name.required"
              >
                {{ $t("name_invalid_text") }}
              </small>

          </div>

          <div class="text-right container">
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
import lineSelect from "../../components/lineSelect.vue";

import { mapActions, mapGetters } from "vuex";

export default {
  components: {
    mdbBtn,
    mdbInput,
    lineSelect
  },
  data() {
    return {
      modal_info: "",
      modal_status: false,
      loading: false,
      room_type: "",
      room_beds_count: "",
      note: "",
      name: "",
      id: 0,
      roomType_name: '',
      roomType_id: null,
      room_beds_list: [],
    };
  },
  validations: {
    name: { required },
    room_beds_count: { required },
    roomType_name: {required}
  },
  props: {
    options: {
      type: Object,
      default() {
        return {};
      },
    },
  },
  computed: mapGetters(["get_province_list", 'price_type_room']),
  async mounted() {
    // await this.fetch_province();
    await this.fetch_price_type_room();
    await this.options;
    if (Object.keys(this.options).length != 0) {
      this.update();
    } else {
      this.cls_wnd();
    }
  },
  methods: {
    ...mapActions(["fetch_province", "fetch_bron_room", 'fetch_price_type_room']),
    update() {
      console.log("da salom");
      console.log(this.options);
      this.id = this.options.id;
      this.name = this.options.room_name;
      this.room_type = this.options.room_type;
      this.room_beds_count = this.options.room_beds_count;
      this.note = this.options.note;
    },
    selectRoomType(option){
      this.roomType_id = option.data.id;
      this.roomType_name = option.data.name;
    },

    cls_wnd() {
      this.name = "";
      this.room_type = "";
      this.room_beds_count = "";
      this.note = "";
      this.id = 0;
      this.roomType_id= null;
      this.roomType_name='';
    },
    async submit() {
      if (this.$v.$invalid) {
        this.$v.$touch();
        this.$refs.message.warning("please_fill");
        return false;
      }
      if(this.room_beds_count){
        this.room_beds_list = [];
        for(let i=0; i<this.room_beds_count; i++){
          let a = {
            name: 'Кровать',
            id: 0
          }
          this.room_beds_list.push(a);
        }
      }
      const requestOptions = {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          name: this.name + ' (' + this.roomType_name + ' )',
          beds_count: this.room_beds_count,
          bedsList: this.room_beds_list,
          reserved_name_1: this.roomType_name,
          reserved_name_2: this.note,
          hospitalRoomTypeid: this.roomType_id,
          id: this.id,
          // "code" : 0
        }),
      };
      this.loading = true;
      const response = await fetch(
        this.$store.state.hostname + "/HospitalBronRoomNs",
        requestOptions
      );
      const data = await response.json();
      this.loading = false;
      if (data.id) {
        this.cls_wnd();
        this.fetch_bron_room();
        this.$refs.message.success("Added_successfully");
      } else {
        this.$refs.msg.error("Error_successfully");

        // this.modal_info = data.detail + "    (" + data.routine + ")";
        // this.modal_status = true;
        return false;
      }
    },
  },
};
</script>

<style scoped>
</style>