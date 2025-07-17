<template>
  <div>
    <div class="border-bottom d-flex justify-content-between main_header_bg_shadow">
      <router-link to="/userFace">
         <h5 class="m-0 ml-4 d-flex" style="padding: 15px 0px; font-size: 20px;">
            {{$t('userFace')}}</h5>
      </router-link>
      <div>
         <router-link to="/userFace_Add/0">
            <mdb-btn class="main_button_add mt-3" m="r3" size="sm" p="r4 l4 t2 b2" style="font-size: 8px;">
              <mdb-icon icon="plus" class="mr-2"/>{{$t('add')}}</mdb-btn>
          </router-link>
      </div>
    </div>
    <div class="search px-3 pt-3 pb-1 border-bottom">
      <div class="row">
        <div class="col-3">
          <mdb-input
            class="m-0 p-0"
            v-model="client_name"
            @input="SearchClientNamePass($event)"
            size="sm"
            outline
            group
            type="text"
            validate
            error="wrong"
            success="right"
          />
          <small
            style="position: absolute; top: -10px; left: 20px; font-size: 11px"
            class="bg-white px-2 py-0"
            >{{ $t("client") }}</small
          >
        </div>
        <div class="col-3">
          <mdb-input
            class="m-0 p-0"
            v-model="passport_number"
            @input="SearchClientNamePass($event)"
            size="sm"
            outline
            group
            type="text"
            validate
            error="wrong"
            success="right"
          />
          <small
            style="position: absolute; top: -10px; left: 20px; font-size: 11px"
            class="bg-white px-2 py-0"
            >{{ $t("passport_number") }}</small
          >
        </div>
        <div class="col-3">
          <input
            class="m-0 p-0 form-control px-2"
            style="height: 27px; font-size: 12px;"
            v-model="phone_number"
            @input="SearchClientBornDate()"
            outline
            group
            type="text"
            validate
            error="wrong"
            success="right"
          />
          <small
            style="position: absolute; top: -10px; left: 20px; font-size: 11px"
            class="bg-white px-2 py-0"
            >{{ $t("born_date") }}</small
          >
        </div>
        <div class="col-3">
          <mdb-input
            class="m-0 p-0"
            v-model="note"
            @input="SearchClientNote($event)"
            size="sm"
            outline
            group
            type="text"
            validate
            error="wrong"
            success="right"
          />
          <small
            style="position: absolute; top: -10px; left: 20px; font-size: 11px"
            class="bg-white px-2 py-0"
            >{{ $t("note") }}</small
          >
        </div>
      </div>
    </div>
    <loaderTable v-if="loading"/>
    <anyTable v-else
      :datasource="client_list"
      @for_delete="for_delete"
      @for_edit="for_edit"
    />
    <massage_box :hide="modal_status" :detail_info="modal_info"
      :m_text="$t('Failed_to_add')" @to_hide_modal="modal_status= false"/>
      <Toast ref="message"></Toast>
  </div>
</template>

<script>
import {
  mdbIcon,
  mdbBtn,mdbInput
} from "mdbvue";
import {mapActions, mapGetters, mapMutations} from 'vuex'
import anyTable from "../../components/clientTable.vue"
export default {
  data(){
    return {
      modal_info: '',
      modal_status: false,
      loading: false,
      auth_id: localStorage.AuthId,
      client_list: {
        rows: [],
        columns: ['userid','ism', 'acc_name', 'middle_name','familiya', 'group_name', ],
        col: []
      },
      default_list: {},
      client_name: '',
      client_id: null,
      passport_number: '',
      phone_number: '',
      note: '',
    }
  },
  components: {
    anyTable, mdbIcon,
    mdbBtn, mdbInput
  },
  computed: mapGetters(['get_user_list']),
  methods: {
    async SearchClientNamePass(search){
      console.log('search')
      console.log(search)
      this.passport_number = '';
      this.phone_number = '';
      this.note="";
      if(search == ''){
      await this.fetch_user();
      this.client_list = this.get_user_list;
        return
      }
      // /TegirmonClient/getPaginationSearchByFioOrPassportSerailNumberOrHomeOrMobilePhoneNumber?page=0&size=100&fio_or_serial_number=
      try{
        // this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/SkudMyUserinfoes/getPaginationSearchByIsmOrPassportSerailNumberOrMobilePhoneNumberFaceUser?page=0&size=100&fio_or_serial_number=" + search);
        const data = await response.json();
        console.log('weqeqw')
        console.log(data)
        // this.loading = false;
        if(response.status == 201 || response.status == 200)
        {
          this.client_list.rows = data.items_list;
          if(data.items_list == 0){
            this.$refs.message.error('not_client')
          }
          // this.$refs.message.success('Added_successfully')
          return true;
        }
        else{
          // const data = await response.text();
          this.modal_info = data;
          this.modal_status = true;
          return false;
        }
      }
      catch{
        // this.loading = false;
        this.client_list = this.get_user_list;
        this.modal_info = this.$i18n.t('network_ne_connect');
        this.modal_status = true;
      }
      
    },
    async SearchClientNote(search){
      this.passport_number = '';
      this.phone_number = '';
      this.client_name= "";
      console.log('search')
      console.log(search)
      if(search == ''){
      await this.fetch_user();
      this.client_list = this.get_user_list;
        return
      }
      // /TegirmonClient/getPaginationSearchByFioOrPassportSerailNumberOrHomeOrMobilePhoneNumber?page=0&size=100&fio_or_serial_number=
      try{
        // this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/SkudMyUserinfoes/getPaginationSearchByIsmOrPassportSerailNumberOrMobilePhoneNumberFaceUser?page=0&size=100&fio_or_serial_number=" + search);
        const data = await response.json();
        console.log('weqeqw')
        console.log(data)
        // this.loading = false;
        if(response.status == 201 || response.status == 200)
        {
          this.client_list.rows = data.items_list;
          if(data.items_list == 0){
            this.$refs.message.error('not_client')
          }
          // this.$refs.message.success('Added_successfully')
          return true;
        }
        else{
          // const data = await response.text();
          this.modal_info = data;
          this.modal_status = true;
          return false;
        }
      }
      catch{
        // this.loading = false;
        this.client_list = this.get_user_list;
        this.modal_info = this.$i18n.t('network_ne_connect');
        this.modal_status = true;
      }
      
    },
    async SearchClientBornDate(){
      this.passport_number = ' ';
      this.note = '';
      this.client_name= "";
      // console.log('search')
      // console.log(search)
      // --> regex inputni tugilgan yil formatiga utkazish dd-mm-yyyy
      var x = this.phone_number.replace(/\D/g, '').match(/(\d{0,2})(\d{0,2})(\d{0,4})/);
      // this.phone_number = !x[2] ? x[1] : + x[1] + '-' + x[2] + (x[3] ? '-' + x[3] : '');
      if(this.phone_number.length == 2){
        this.phone_number = x[1];
      }
      else if(x[1].length >= 2){
        this.phone_number = x[1] + '-' + x[2] + (x[3] ? '-' + x[3] : '');
      }
      // --> regex inputni tugilgan yil formatiga utkazish dd-mm-yyyy

      // --> regexdan qidirish formatiga utkazish yyyy-mm-dd
      var search_birthday = '';
      if(this.phone_number.length <= 2){
        search_birthday = this.phone_number
      }
      else if(this.phone_number.length >= 5 && this.phone_number.length < 10){
        search_birthday = this.phone_number.slice(3,5) + '-' + this.phone_number.slice(0,2)
      }
      else{
        search_birthday = this.phone_number.slice(6,11) + '-' + this.phone_number.slice(3,5) + '-' + this.phone_number.slice(0,2);
      }
      // --> regexdan qidirish formatiga utkazish yyyy-mm-dd
      if(search_birthday == ''){
      await this.fetch_user();
      this.client_list = this.get_user_list;
        return
      }
      // /TegirmonClient/getPaginationSearchByFioOrPassportSerailNumberOrHomeOrMobilePhoneNumber?page=0&size=100&fio_or_serial_number=
      try{
        // this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/SkudMyUserinfoes/getPaginationSearchByIsmOrPassportSerailNumberOrMobilePhoneNumberFaceUser?page=0&size=100&fio_or_serial_number=" + search_birthday);
        const data = await response.json();
        // this.loading = false;
        if(response.status == 201 || response.status == 200)
        {
          this.client_list.rows = data.items_list;
          if(data.items_list == 0){
            this.$refs.message.error('not_client')
          }
          // this.$refs.message.success('Added_successfully')
          return true;
        }
        else{
          // const data = await response.text();
          this.modal_info = data;
          this.modal_status = true;
          return false;
        }
      }
      catch{
        // this.loading = false;
        this.client_list = this.get_user_list;
        this.modal_info = this.$i18n.t('network_ne_connect');
        this.modal_status = true;
      }
      
    },
    ...mapActions(['fetch_user', ]),
    ...mapMutations(['userFace_delete_row']),
  
      for_edit(edit_data)
      {
        this.$router.push("/userFace_add/" + edit_data.userid);
      },
      async for_delete(del_data,index)
      {
        console.log(del_data)
         const requestOptions = {
            method : "delete",
          };
          const response = await fetch(this.$store.state.hostname + "/SkudMyUserinfoes/" + del_data.userid, requestOptions);
          const data = await response.text();
          console.log(data)
          if(response.status == 201 || response.status == 200)
          {
            this.$refs.message.success('Successfully_removed')
            this.alert_success = true;
            this.userFace_delete_row(index);
          }
          else{
            this.modal_info = data;
            this.modal_status = true;
          }
      },
  },
  async mounted(){
    this.loading = true;
    await this.fetch_user();
    this.client_list = this.get_user_list;
    this.loading = false;
    console.log(this.get_user_list)
    if(this.$store.state.alert){
      this.$refs.message.success('Added_successfully')
      this.$store.state.alert = false
    }
  }
}
</script>

<style>

</style>