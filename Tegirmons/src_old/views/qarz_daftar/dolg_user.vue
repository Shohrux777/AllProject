<template>
  <div>
    <div class="border-bottom d-flex justify-content-between">
      <router-link to="/dolg_user">
         <h5 class="m-0 ml-4 d-flex" style="padding: 16px 0px">
            {{$t('dolg_user')}}</h5>
      </router-link>
      <div>
        <router-link to="/dolg_user_add/0">
          <mdb-btn color="success mt-3" m="r3" size="sm" p="r4 l4 t2 b2"><mdb-icon icon="plus" class="mr-2"/>{{$t('add')}}</mdb-btn>
        </router-link>
      </div>
    </div>
    <div class="container-fluid mb-0">
      <div class="row mb-0">
        <div class="col-3 mt-2 mb-0">
          <mdb-input
            class="m-0 p-0"
            v-model="search"
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
    <block ref="blocked"></block>
  </div>
</template>

<script>
import {
  mdbIcon,
  mdbBtn,mdbInput
} from "mdbvue";
import {mapActions, mapGetters, mapMutations} from 'vuex'
import anyTable from "../../components/anyTable.vue"
export default {
  data(){
    return {
      modal_info: '',
      modal_status: false,
      loading: false,
      auth_id: localStorage.AuthId,
      client_list: {
        rows: [],
        columns: ['fio', 'phone_number', 'image_url'],
        col: []
      },
      search: '',
    }
  },
  components: {
    anyTable, mdbIcon,
    mdbBtn, mdbInput
  },
  computed: mapGetters(['allcategory']),
  methods: {
    ...mapActions(['fetchcategory', ]),
    ...mapMutations(['category_delete_row']),
    async SearchClientNamePass(search){
      this.search = '';
      if(search == ''){
        await this.fetchClient();
        return
      }
      // /TegirmonClient/getPaginationSearchByFioOrPassportSerailNumberOrHomeOrMobilePhoneNumber?page=0&size=100&fio_or_serial_number=
      try{
        // this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/TegirmonQarzdorlikUser/getPaginationSearchByFioOrPassportSerailNumberOrHomeOrMobilePhoneNumber?page=0&size=100&fio_or_serial_number=" + search);
        const data = await response.json();
        // this.loading = false;
        if(response.status == 201 || response.status == 200)
        {
          this.client_list.rows = data.items_list;
          if(data.items_list == 0){
            this.$refs.message.error('not_client')
          }
          return true;
        }
        else{
          this.modal_info = data;
          this.modal_status = true;
          return false;
        }
      }
      catch{
        // this.loading = false;
        this.client_list = this.allClient;
        this.modal_info = this.$i18n.t('network_ne_connect');
        this.modal_status = true;
      }
      
    },
      async fetchClient(){
      try{
        const response = await fetch(this.$store.state.hostname + "/TegirmonQarzdorlikUser/getPagination?page=0&size=100");
        const data = await response.json();
        if(response.status == 201 || response.status == 200)
        {
          console.log(data)
          this.client_list.rows = data.items_list;
          return true;
        }
        else{
          this.modal_info = data;
          this.modal_status = true;
          return false;
        }
      }
      catch{
        this.client_list.rows = [];
        this.modal_info = this.$i18n.t('network_ne_connect');
        this.modal_status = true;
      }
    },
      for_edit(edit_data)
      {
        this.$router.push("/dolg_user_add/" + edit_data.id);
      },
      async for_delete(del_data,index)
      {
        return;
         const requestOptions = {
            method : "delete",
          };
          const response = await fetch(this.$store.state.hostname + "/TegirmonCategoryControler/" + del_data.id, requestOptions);
          const data = await response.text();

          if(response.status == 201 || response.status == 200)
          {
            this.$refs.message.success('Successfully_removed')
            this.alert_success = true;
            this.category_delete_row(index);
          }
          else{
            this.modal_info = data;
            this.modal_status = true;
          }
      },
  },
  async mounted(){
    if(localStorage.AccessType != 0){
      this.$refs.blocked.show_block();
    }
    this.loading = true;
    await this.fetchClient();
    this.loading = false;
    if(this.$store.state.alert){
      this.$refs.message.success('Added_successfully')
      this.$store.state.alert = false
    }
  }
}
</script>

<style>

</style>