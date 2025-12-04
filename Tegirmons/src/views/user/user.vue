<template>
  <div class="user-page">
    <!-- Header -->
    <div class="d-flex justify-content-between align-items-center user-header-card">
      <div class="d-flex align-items-center">
        <router-link to="/user">
          <h5 class="m-0 d-flex align-items-center user-header-title text-white">
            {{$t('user')}}
          </h5>
        </router-link>
      </div>
      <div class="d-flex align-items-center">
        <div class="custom-control custom-switch mr-3" v-show="!admin">
          <input
            v-model="access_status"
            type="checkbox"
            class="custom-control-input"
            @change="fetch_close_page"
            id="number11"
            checked
          >
          <label style="cursor:pointer;" class="custom-control-label" for="number11"></label>
        </div>
        <router-link to="/user_add/0">
          <mdb-btn
            class="main_button_add user-add-btn"
            m="r3"
            size="sm"
            p="r4 l4 t2 b2"
          >
            <mdb-icon icon="plus" class="mr-2" />{{$t('add')}}
          </mdb-btn>
        </router-link>
      </div>
    </div>

    <!-- Content card -->
    <div class="user-card-wrapper mt-3">
      <div class="card user-card-shadow">
        <div class="card-body p-2 p-md-3">
          <loaderTable v-if="loading" />
          <anyTable
            v-else
            :datasource="allUser"
            @for_delete="for_delete"
            @for_edit="for_edit"
            @for_access="for_access"
            :userLock="lock"
          />
        </div>
      </div>
    </div>
    <massage_box :hide="modal_status" :detail_info="modal_info"
      :m_text="$t('Failed_to_add')" @to_hide_modal="modal_status= false"/>
    <Toast ref="message"></Toast>

    <mdb-modal :show="confirm" @close="confirm = false" size="md" class="text-center" info>
      <mdb-modal-header center class="py-1" :close="false">
        <p class="heading">{{$t('access_user')}}</p>
      </mdb-modal-header>
      <mdb-modal-body>
        <user_access :user_id="user_id" :user_name="user_name" @close="confirm = false"/>
      </mdb-modal-body>
    </mdb-modal>
  </div>
</template>

<script>
import {
  mdbIcon,
  mdbBtn,
  mdbModal,mdbModalHeader, mdbModalBody
} from "mdbvue";
import {mapActions, mapGetters, mapMutations} from 'vuex'
import anyTable from "../../components/anyTable.vue"
import user_access from './user_access.vue';
export default {
  data(){
    return {
      confirm: false,
      modal_info: '',
      modal_status: false,
      loading: false,
      auth_id: localStorage.AuthId,
      lock: true,
      user_id: 0,
      user_name: '',
      access_status: false,
      admin:true,
    }
  },
  components: {
    anyTable, mdbIcon,
    mdbBtn,
    mdbModal,mdbModalHeader, mdbModalBody,
    user_access
  },
  computed: mapGetters(['allUser']),
  methods: {
    ...mapActions(['fetchUser']),
    ...mapMutations(['user_delete_row']),
      for_edit(edit_data)
      {
        this.$router.push("/user_add/" + edit_data.id);
      },
      for_access(data){
        this.confirm = true;
        this.user_id = data.id;
        this.user_name = data.fio;
      },
      async for_delete(del_data,index)
      {
         const requestOptions = {
            method : "delete",
          };
          const response = await fetch(this.$store.state.hostname + "/TegirmonUser/" + del_data.id, requestOptions);
          const data = await response.text();

          if(response.status == 201 || response.status == 200)
          {
            this.$refs.message.success('Successfully_removed')
            this.alert_success = true;
            this.user_delete_row(index);
          }
          else{
            this.modal_info = data;
            this.modal_status = true;
          }
      },
      async fetch_close_page(){
        const requestOptions = {
          method : "POST",
          headers: { "Content-Type" : "application/json" },
          body: JSON.stringify({
              "status" : this.access_status,
              "id": 1
          })
        };
        try{
        this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/TegirmonClose", requestOptions);
        const data = await response.text();
        this.loading = false;
        if(response.status == 201 || response.status == 200)
        {
          console.log(data)
          return true;
        }
        }
        catch{
            this.loading = false;
            this.modal_info = this.$i18n.t('network_ne_connect'); 
            this.modal_status = true;
        }
      },
      async update_close_page(){
        try{
        this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/TegirmonClose/1");
        const data = await response.json();
        console.log('test uchun edi', response)

        this.loading = false;
        if(response.status == 201 || response.status == 200)
        {
          console.log('test uchun', data)
          this.access_status = data.status;
          return true;
        }
        }
        catch{
            this.loading = false;
            this.modal_info = this.$i18n.t('network_ne_connect'); 
            this.modal_status = true;
        }
      },
  },
  
  async mounted(){
    this.loading = true;
    await this.update_close_page();
    await this.fetchUser();
    this.loading = false;
    if(this.$store.state.alert){
      this.$refs.message.success('Added_successfully')
      this.$store.state.alert = false
    }
    if(localStorage.AccessType == 0){
      this.admin = false;
    }
  }
}
</script>

<style scoped>
.user-page {
  padding: 8px 10px 14px 10px;
  background: #f5f7fb;
  min-height: 100%;
}

.user-header-card {
  padding: 10px 16px;
  border-radius: 10px;
  background-image: linear-gradient(90deg, #64b0fb 0%, #7fd4ff 50%, #b5e3ff 100%);
  box-shadow: rgba(15, 35, 52, 0.25) 0px 8px 20px -12px;
  color: #fff;
}

.user-header-title {
  font-size: 18px;
  font-weight: 600;
  letter-spacing: 0.3px;
}

.user-add-btn {
  font-size: 11px !important;
  padding: 4px 12px !important;
  border-radius: 16px;
  background-image: linear-gradient(90deg, #1e88e5 0%, #42a5f5 100%) !important;
  color: #ffffff !important;
  border: none;
  box-shadow: 0 2px 6px rgba(25, 118, 210, 0.4);
}

.user-add-btn:hover {
  filter: brightness(1.05);
  box-shadow: 0 3px 8px rgba(25, 118, 210, 0.55);
}

.user-card-wrapper {
  padding: 0 2px;
}

.user-card-shadow {
  border-radius: 10px;
  border: none;
  box-shadow: rgba(60, 64, 67, 0.3) 0px 1px 2px 0px,
              rgba(60, 64, 67, 0.15) 0px 1px 3px 1px;
  background: #ffffff;
}
</style>