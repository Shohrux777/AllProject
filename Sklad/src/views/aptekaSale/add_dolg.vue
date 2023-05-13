<template>
    <div class="add_dolg container-fluid">
        <div class="row">
            <div class="col-6 m-0 pl-0">
              <div class="client_list_pay_dolg" >
                <div class=" d-flex">
                  <mdb-input label="Поиск клиента" class="p-0 m-0 mt-3 w-100" size="sm" v-model="search_client" />
                  <mdb-btn style="width:170px; height: 32px; font-size: 9px;" 
                  class="px-2 py-0 mt-3" color="primary" @click="client_add_activ = true">
                  {{$t('client_add')}}</mdb-btn>
                </div>
                <div class="client_add" v-show="client_add_activ">
                  <client_add @close="closeClient"/>
                </div>
                <div v-show="!client_add_activ" class="client_list ">
                  <div class="d-flex justify-content-between border-bottom py-2 px-3 client_item" 
                    v-for="(item,i) in filteredList" :key="i"
                    @click="activ_client(i, item)" :class="{'client_activ_index': activ_index == i}">
                    <span style="font-size: 13px;">{{ item.fio }}</span>
                    <span style="font-size: 11px;">{{ item.phone_number}}</span>
                  </div>
                </div>
              </div>
            </div>
            <div class="col-6">
              <mdb-input label="Клиент" disabled class="p-0 m-0 mt-3 w-100" size="sm" v-model="client_name" />
              <mdb-input :label="$t('online_sum')"  class="p-0 m-0 mt-3 w-100" size="sm" v-model="dolg_sum" />
              <mdb-input type="textarea" :label="$t('note')" v-model="note" :rows="5" />
              <div class="border-top text-right">
                <mdb-btn style="width:170px; height: 32px; font-size: 9px;" 
                  class="px-2 py-0 mt-3" color="success" @click="fetchDolgClient">
                  {{$t('dolg_add')}}</mdb-btn>
              </div>  
            </div>
        </div>
        <massage_box :hide="modal_status" :detail_info="modal_info"
        :m_text="$t('Failed_to_delete')" @to_hide_modal = "modal_status= false"/>

      <Toast ref="message"></Toast>
    </div>
</template>

<script>
import {mdbBtn,mdbInput} from 'mdbvue'
import client_add from '@/components/client_add.vue';
export default {
  components: {
    mdbBtn,
    mdbInput,
    client_add,
  },
  data(){
    return{
      modal_status: false,
      modal_info: '',

      client_list: [],
      search_client: '',
      client_add_activ: false,
      client_id: null,
      client_name: '',
      activ_index: -1,
      dolg_sum: 0,
      note: '',
    }
  },
  async mounted(){
    await this.fetchClient();
  },
  computed: {
      filteredList: function(){
        if(this.search_client)
        {
          return this.client_list.filter((item)=>{
            return this.search_client.toLowerCase().split(' ').every(v => item.fio.toLowerCase().includes(v))
          })
        }else
        {
          return this.client_list;
        }
      }
    },
  methods:{
    async fetchClient(){
      try{
        const res = await fetch(this.$store.state.hostname + '/PosClients/getClientList?page=0&size=500');
        const res_data = await res.json();
        console.log('res_data');
        console.log(res_data);
        this.client_list = res_data.items_list;
      }
      catch{
        this.modal_info = this.$i18n.t('network_ne_connect'); 
        this.modal_status = true;
      }
    },
    async closeClient(){
      this.client_add_activ = false;
      await this.fetchClient();
    },
    async activ_client(i, data){
      this.activ_index = i;
      this.client_id = data.id;
      this.client_name = data.fio;
    },

    async fetchDolgClient(){
      const requestOptions = {
        method : "POST",
        headers: { "Content-Type" : "application/json" },
        body: JSON.stringify({
          "reserved_1": this.client_name,
          "reserved_2": localStorage.fio,
          "reserved_3": this.note,
          "note1": localStorage.AuthId.toString(),
          "real_company_id": localStorage.CompId,
          "dolg_summ": parseFloat(this.dolg_sum),
          "payed_summ" : 0,
          "id": 0,
          "posClientid": this.client_id,
          "posCheckid": 1,
          // "posClientid": 0
        })
      };
      console.log(requestOptions.body)
      try{
        this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/PosClientDolgs", requestOptions);
        const data = await response.json();
        console.log('dolg')
        console.log(data)
        this.loading = false;
        if(response.status == 201 || response.status == 200)
        {
          this.client_id = 0;
          this.client_name = '';
          this.$emit('close')
        }
      }
      catch{
        this.modal_info = this.$i18n.t('network_ne_connect'); 
        this.modal_status = true;
        return false;
      }
    },
  }
}
</script>

<style lang="scss">
.client_list_pay_dolg{
    position: relative;
    overflow-y: hidden;
    width:100%;
    height: 460px;
    border-radius: 5px;
    .client_add{
      background: #e7ffe4;
    }
    .client_list{
      height: 400px;
      overflow-y: hidden;
      overflow-y:scroll;
      .client_item{
        &:hover{
          background: #cee2ff;
          cursor:pointer;
        }
      }
      .client_activ_index{
        background: #93c5ff;
        
      }
    }
  }
</style>