<template>
  <div class="worker-page">
    <!-- Header -->
    <div class="d-flex justify-content-between align-items-center worker-header-card">
      <router-link to="/worker" class="d-flex align-items-center worker-header-link">
        <h5 class="m-0 worker-header-title">
          {{$t('worker')}}
        </h5>
      </router-link>
      <div>
        <router-link to="/worker_Add/0">
          <mdb-btn
            class="worker-add-btn"
            m="r3"
            size="sm"
            p="r4 l4 t2 b2"
          >
            <mdb-icon icon="plus" class="mr-2" />{{$t('add')}}
          </mdb-btn>
        </router-link>
      </div>
    </div>

    <!-- Search filters -->
    <div class="container-fluid mt-2">
      <div class="card worker-filter-card">
        <div class="card-body py-2 px-3">
          <div class="row">
            <div class="col-12 col-md-3 mb-2 mb-md-0">
              <div class="worker-search-wrapper">
                <label class="worker-search-label">
                  {{$t('client')}}
                </label>
                <mdb-input
                  class="m-0"
                  v-model="client_name"
                  @input="SearchClientNamePass($event)"
                  size="md"
                  outline
                  group
                  type="text"
                  validate
                  error="wrong"
                  success="right"
                />
              </div>
            </div>
            <div class="col-12 col-md-3 mb-2 mb-md-0">
              <div class="worker-search-wrapper">
                <label class="worker-search-label">
                  {{$t('passport_number')}}
                </label>
                <mdb-input
                  class="m-0"
                  v-model="passport_number"
                  @input="SearchClientNamePass($event)"
                  size="md"
                  outline
                  group
                  type="text"
                  validate
                  error="wrong"
                  success="right"
                />
              </div>
            </div>
            <div class="col-12 col-md-3 mb-2 mb-md-0">
              <div class="worker-search-wrapper">
                <label class="worker-search-label">
                  {{$t('born_date')}}
                </label>
                <input
                  class="m-0 form-control px-2 worker-born-input"
                  v-model="phone_number"
                  @input="SearchClientBornDate()"
                  type="text"
                />
              </div>
            </div>
            <div class="col-12 col-md-3">
              <div class="worker-search-wrapper">
                <label class="worker-search-label">
                  {{$t('note')}}
                </label>
                <mdb-input
                  class="m-0"
                  v-model="note"
                  @input="SearchClientNote($event)"
                  size="md"
                  outline
                  group
                  type="text"
                  validate
                  error="wrong"
                  success="right"
                />
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>

    <!-- Table card -->
    <div class="worker-card-wrapper mt-2">
      <div class="card worker-card-shadow">
        <div class="card-body p-2 p-md-3">
          <loaderTable v-if="loading"/>
          <anyTable v-else
            :datasource="client_list"
            @for_delete="for_delete"
            @for_edit="for_edit"
          />
        </div>
      </div>
    </div>

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
        columns: ['fio','phone_number','passport_number','addiotionala_information','note'],
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
  computed: mapGetters(['allWorker']),
  methods: {
    async SearchClientNamePass(search){
      console.log('search')
      console.log(search)
      this.passport_number = '';
      this.phone_number = '';
      this.note="";
      if(search == ''){
      await this.fetchWorker();
      this.client_list = this.allWorker;
        return
      }
      // /TegirmonClient/getPaginationSearchByFioOrPassportSerailNumberOrHomeOrMobilePhoneNumber?page=0&size=100&fio_or_serial_number=
      try{
        // this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/TegirmonWorker/getPaginationSearchByFioOrPassportSerailNumberOrHomeOrMobilePhoneNumber?page=0&size=100&fio_or_serial_number=" + search);
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
        this.client_list = this.allWorker;
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
      await this.fetchWorker();
      this.client_list = this.allWorker;
        return
      }
      // /TegirmonClient/getPaginationSearchByFioOrPassportSerailNumberOrHomeOrMobilePhoneNumber?page=0&size=100&fio_or_serial_number=
      try{
        // this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/TegirmonWorker/getPaginationByNote?page=0&size=100&note_str=" + search);
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
        this.client_list = this.allWorker;
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
      await this.fetchWorker();
      this.client_list = this.allWorker;
        return
      }
      // /TegirmonClient/getPaginationSearchByFioOrPassportSerailNumberOrHomeOrMobilePhoneNumber?page=0&size=100&fio_or_serial_number=
      try{
        // this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/TegirmonWorker/getPaginationByBornDate?page=0&size=100&born_date_str=" + search_birthday);
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
        this.client_list = this.allWorker;
        this.modal_info = this.$i18n.t('network_ne_connect');
        this.modal_status = true;
      }
      
    },
    ...mapActions(['fetchWorker', ]),
    ...mapMutations(['worker_delete_row']),
  
      for_edit(edit_data)
      {
        this.$router.push("/worker_add/" + edit_data.id);
      },
      async for_delete(del_data,index)
      {
         const requestOptions = {
            method : "delete",
          };
          const response = await fetch(this.$store.state.hostname + "/TegirmonWorker/" + del_data.id, requestOptions);
          const data = await response.text();

          if(response.status == 201 || response.status == 200)
          {
            this.$refs.message.success('Successfully_removed')
            this.alert_success = true;
            this.worker_delete_row(index);
          }
          else{
            this.modal_info = data;
            this.modal_status = true;
          }
      },
  },
  async mounted(){
    this.loading = true;
    await this.fetchWorker();
    this.client_list = this.allWorker;
    this.loading = false;
    console.log(this.allWorker)
    if(this.$store.state.alert){
      this.$refs.message.success('Added_successfully')
      this.$store.state.alert = false
    }
  }
}
</script>

<style scoped>
.worker-page {
  padding: 8px 10px 14px 10px;
  background: #ffffff;
  min-height: 100%;
}

.worker-header-card {
  padding: 10px 16px;
  border-radius: 10px;
  background-image: linear-gradient(90deg, #64b0fb 0%, #7fd4ff 50%, #b5e3ff 100%);
  box-shadow: rgba(15, 35, 52, 0.25) 0px 8px 20px -12px;
  color: #fff;
}

.worker-header-link {
  color: #ffffff;
  text-decoration: none;
}

.worker-header-link:hover {
  text-decoration: none;
}

.worker-header-title {
  font-size: 18px;
  font-weight: 600;
  letter-spacing: 0.3px;
}

.worker-add-btn {
  font-size: 11px !important;
  padding: 4px 12px !important;
  border-radius: 16px;
  background-image: linear-gradient(90deg, #1e88e5 0%, #42a5f5 100%) !important;
  color: #ffffff !important;
  border: none;
  box-shadow: 0 2px 6px rgba(25, 118, 210, 0.4);
}

.worker-add-btn:hover {
  filter: brightness(1.05);
  box-shadow: 0 3px 8px rgba(25, 118, 210, 0.55);
}

.worker-filter-card {
  border-radius: 10px;
  border: none;
  box-shadow: rgba(60, 64, 67, 0.15) 0px 1px 2px 0px,
              rgba(60, 64, 67, 0.1) 0px 1px 3px 1px;
}

.worker-search-wrapper {
  position: relative;
  margin-top: 6px;
}

.worker-search-label {
  position: absolute;
  top: -9px;
  left: 6px;
  font-size: 11px;
  font-weight: 600;
  color: #607d8b;
  background: #ffffff;
  padding: 0 4px;
  z-index: 1;
}

.worker-born-input {
  height: 32px;
  font-size: 12px;
}

.worker-card-wrapper {
  padding: 0 2px;
}

.worker-card-shadow {
  border-radius: 10px;
  border: none;
  box-shadow: rgba(60, 64, 67, 0.3) 0px 1px 2px 0px,
              rgba(60, 64, 67, 0.15) 0px 1px 3px 1px;
  background: #ffffff;
}
</style>