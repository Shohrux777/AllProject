<template>
  <div>
    <div class="d-flex justify-content-between main_header_bg_shadow">
      <router-link to="/user">
         <h5 class="m-0 ml-4 d-flex" style="padding: 15px 0px; font-size: 20px;">
            {{$t('user')}}</h5>
      </router-link>
      <div>
         <router-link to="/user_add/0">
            <mdb-btn class="main_button_add mt-3" m="r3" size="sm" p="r4 l4 t2 b2" style="font-size: 8px;">
              <mdb-icon icon="plus" class="mr-2"/>{{$t('add')}}</mdb-btn>
          </router-link>
      </div>
    </div>
    <loaderTable v-if="loading"/>
    <anyTable v-else
      :datasource="allUser"
      @for_delete="for_delete"
      @for_edit="for_edit"
      @for_access="for_access"
      :userLock='lock'
    />
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
import anyTable from "../../components/anyTable"
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
  },
  async mounted(){
    this.loading = true;
    await this.fetchUser();
    this.loading = false;
    if(this.$store.state.alert){
      this.$refs.message.success('Added_successfully')
      this.$store.state.alert = false
    }
  }
}
</script>

<style>
.main_header_bg_shadow{
  box-shadow: rgba(0, 0, 0, 0.15) 1.95px 1.95px 2.6px;
}
</style>