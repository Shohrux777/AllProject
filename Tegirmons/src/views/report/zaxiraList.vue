<template>
  <div class="bg-white zaxira_list" style="position:absolute;">
    <div class="p-4" >
      <div class="bg-white p-4 mb-5 pt-4 shadow" style="border-radius:5px; position:relative;">
        <form @submit.prevent="submit">
          <div  class="d-flex justify-content-between align-items-center ">
            <div class="title w-100 row align-items-center">
               <div class="col-3 px-2 w-100 mb-2 mt-1" v-for="(item, index) in all_qty_product" :key="index" v-show="item.qty>0" @click="selectProductZaxiraList(item)">
                <div class="card p-2 all_zaxira_item">
                  <div class=" border_all_qty " v-show="item.qty>1">
                    <p style="font-size: 14px;" class="mb-0 p-0">{{item.name}}</p>
                    <p class="mb-1 p-0 text-right">{{(item.qty.toFixed(1) || 0).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</p>
                  </div>
                </div>
                  
               </div>
            </div>
          </div>
          <div class="row mt-2 border-bottom">
              <div class="col-5">
                <erpSelect
                  :options="allClient.rows"
                  @select="selectOptionUser"
                  :selected="user_name"
                  size="sm"
                  url="/TegirmonClient/getPaginationSearchByFioOrPassportSerailNumberOrHomeOrMobilePhoneNumber?page=0&size=100&fio_or_serial_number="
                  :label="$t('client')"
                  />
                  <small style="position:absolute; top:-7px; left:20px; font-size: 11px;" class="bg-white px-2 py-0">{{$t('client')}}</small>
              </div>
              <div class="col-7">
                <mdb-btn @click="apply" color="primary py-2 px-4" style="font-size:10px; margin-top: 0px;">
                  <mdb-icon style="font-size:10.5px;" icon="redo" class="m-0 p-0 mr-1" />
                  {{$t('refresh')}}
                </mdb-btn>
                <mdb-btn @click="clicked_excel('Table', 'LoremTable')" color="primary py-2 px-4" style="font-size:10px; margin-top: 0px;">
                  <mdb-icon style="font-size:10.5px;" icon="file-excel" far class="m-0 p-0 mr-1" />
                  Ехсел
                </mdb-btn>
              </div>
            </div>
        </form>
        <loaderTable v-if="loading"/>
        <div v-else class="TablePatientDocId p-2">
          <table ref="table" id="table" class="myTableZaxiralist">
            <thead>
              <tr class="header">
                <th  width="40" class="text-left">№</th>
                <th >{{$t('client')}}</th>
                <th >{{$t('product')}}</th>
                <th >{{$t('qty')}}</th>
                <th >{{$t('real_qty')}}</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="(row,rowIndex) in get_payment_list" :key="rowIndex" :class="{'bg-danger': row.qty<-2}">
                <td> <small style="font-size: 12.5px;">{{rowIndex+1}}</small> </td>
                <td> <small style="font-size: 12.5px;">{{row.client.fio}}</small> </td>
                <td> <small style="font-size: 12.5px;">{{row.product.name}}</small> </td>
                <td> <small style="font-size: 12.5px;">{{row.qty.toFixed(1)}}</small> </td> 
                <td> <small style="font-size: 12.5px;">{{row.real_qty.toFixed(1)}}</small> </td> 
              </tr>
              
            </tbody>
          </table>
        </div>
      </div>
    </div>

     
   
      <mdb-modal :show="show" @close="show = false" size="sm" class="text-center" danger>
        <mdb-modal-header center :close="false">
          <p class="heading">{{$t('Are_you_sure')}}</p>
        </mdb-modal-header>
        <mdb-modal-body>
          <mdb-icon icon="times" size="4x" class="animated rotateIn"/>
        </mdb-modal-body>
        <mdb-modal-footer center>
          <mdb-btn outline="danger" >{{$t('Yes')}}</mdb-btn>
          <mdb-btn color="danger" @click="show = false">{{$t('No')}}</mdb-btn>
        </mdb-modal-footer>
    </mdb-modal>
    <block ref="blocked"></block>
    <Toast ref="message"></Toast>
     <massage_box :hide="modal_status" :detail_info="modal_info"
      :m_text="$t('Failed_to_delete')" @to_hide_modal = "modal_status= false"/>
  </div>

</template>

<script>
  import erpSelect from "../../components/erpSelectFioSearch.vue";
  import { mdbBtn, mdbBadge, mdbInput, mdbIcon,  mdbModal, mdbModalHeader,   mdbModalBody,mdbModalFooter   } from 'mdbvue';
  import {mapActions, mapGetters, mapMutations} from 'vuex'
  // import 'vue2-datepicker/index.css';
  export default {
    components: {
      mdbBtn,
      mdbModal, mdbModalHeader, mdbIcon, mdbModalBody, mdbModalFooter,
       mdbInput, mdbBadge, erpSelect
      
    },
    data(){
      return{
        show_otchert: false,

        show: false,
        admin:false,
        snipper: true,
        loading:false,
        editData: {},
        modal_info : '',
        modal_status: false,
        contragent_name: '',
        contragent_id: 0,
        Start_time: null,
        End_time: null,
        group_name: '',
        group_id: null,
        get_payment_list: [],
        all_qty_product: [],
        index: null,
        service_id: null,

        rasxod: 0,

        user_name: '',
        user_id: null,

      uri: 'data:application/vnd.ms-excel;base64,',
      template: '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheets><x:Name>{worksheets}</x:Name></x:WorksheetsOptions><x:DisplayGridlines/></x:WorksheetsOptions></x:ExcelWorksheets></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>',
      base64:function(s){return window.btoa(unescape(encodeURIComponent(s)))},
      format:function(s, c){return s.replace(/{(\w+)}/g, function(m,p){return c[p]; }) }
      }
    },
    async mounted(){
      if(localStorage.AccessType == 1){
        this.$refs.blocked.show_block();
        return;
      }
      await this.fetchClient();
      let time1 = new Date();
      this.Start_time = time1.toISOString().slice(0,10); 
      this.End_time = time1.toISOString().slice(0,10);
      await this.fetchAllQty();
      await this.apply();
    },
   
    computed: mapGetters(['get_contragent_list', 'allClient']),
    methods: {
      ...mapActions(['fetch_contragent', 'fetchClient']),
      ...mapMutations(['district_row_delete',]),

      clicked_excel(table, name){
        if(!table.nodeType) table = this.$refs.table
        var ctx = {worksheets: name || 'Worksheets', table: table.innerHTML}
        window.location.href = this.uri + this.base64(this.format(this.template, ctx))
      },

      add(){
        this.show =! this.show
        this.editData = {};
      },
      print(){
        this.$refs.listlar.generatePdf()
      },
      async selectOptionUser(option){
        console.log(option)
        this.user_name = option.fio;
        this.user_id = option.id;
        this.fetchOstatka(option.id);
      },
      async fetchOstatka(id){
        try{
          this.loading = true;
          const response = await fetch(this.$store.state.hostname + "/TegirmonClientOstatkas/getPaginationGetByClientClientIdList?page=0&size=100&client_id=" + id);
          const data = await response.json();
          this.loading = false;
          if(response.status == 201 || response.status == 200)
          {
            this.get_payment_list = data.items_list;
            this.$refs.message.success('Added_successfully')
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
          this.loading = false;
          this.modal_info = this.$i18n.t('network_ne_connect');
          this.modal_status = true;
        }
      },
      async apply(){
        try{
          this.loading = true;
          const response = await fetch(this.$store.state.hostname + "/TegirmonClientOstatkas/getPagination?page=0&size=1000000");
          this.loading = false;
          if(response.status == 201 || response.status == 200)
          {
            const data = await response.json();
            console.log(data ,  'sadasd data open')

            this.get_payment_list = data.items_list;
             
            // this.$refs.message.success('Added_successfully')
            return true;
          }
          else{
            const data = await response.text();
            this.modal_info = data;
            this.modal_status = true;
            return false;
          }
        }
        catch{
          this.loading = false;
          this.modal_info = this.$i18n.t('network_ne_connect'); 
          this.modal_status = true;
        }
      },
      async selectProductZaxiraList(product_zaxira_item){
        console.log(product_zaxira_item.tegirmonProductid);
        try{
          this.loading = true;
          const response = await fetch(this.$store.state.hostname + "/TegirmonClientOstatkas/getPaginationProductId?page=0&size=100000000&product_id=" + product_zaxira_item.tegirmonProductid);
          this.loading = false;
          if(response.status == 201 || response.status == 200)
          {
            const data = await response.json();
            console.log(data ,  'sadasd data open')
            this.get_payment_list = data.items_list;
            // this.$refs.message.success('Added_successfully')
            return true;
          }
          else{
            const data = await response.text();
            this.modal_info = data;
            this.modal_status = true;
            return false;
          }
        }
        catch{
          this.loading = false;
          this.modal_info = this.$i18n.t('network_ne_connect'); 
          this.modal_status = true;
        }
      },
      async fetchAllQty(){
        try{
          this.loading = true;
          const response = await fetch(this.$store.state.hostname + "/TegirmonClientOstatkas/getTegirmonClientOstatkaInfo");
          this.loading = false;
          if(response.status == 201 || response.status == 200)
          {
            const data = await response.json();
            console.log(data ,  'sadasd data open')
            this.all_qty_product = data;
            return true;
          }
          else{
            const data = await response.text();
            this.modal_info = data;
            this.modal_status = true;
            return false;
          }
        }
        catch{
          this.loading = false;
          this.modal_info = this.$i18n.t('network_ne_connect'); 
          this.modal_status = true;
        }
      }
      
      
      
    },
  };
</script>

<style lang="scss" scoped>
.zaxira_list{
  overflow: hidden;
  overflow-y: scroll;
  height: 99vh;
}

.add{
  position: fixed;
  background: rgba(0, 0, 0, 0.4);
  height: 100vh;
  top:0;
  width:85%;
}

.addxizmat{
  width: 470px;
  // height: 120px;
  background: #fff;
  position: relative;
  z-index: 5000;
}
.showing{
  display: none;
}
.timePicer{
  position: relative;
  margin-top: -10px;
  .timeLabel{
    position: absolute;
    font-size: 12px;
    background-color: #fff;
    padding: 1px 3px;
    z-index: 1;
    left: 6px;
    top: -1px;
  }
  .dayLabel{
    position: absolute;
    font-size: 12px;
    background-color: #fff;
    padding: 0px 3px;
    z-index: 1;
    left: 6px;
    top: -8px;
  }
}
.TablePatientDocId{
    // height: 400px;
    // overflow: hidden;
    // overflow-y: auto;
    // border: 1px solid #ddd;
  }
  .myTableZaxiralist {
  /* border-collapse: collapse; */
  table-layout:fixed;
  width: 100%;
  overflow: hidden;
  // border: 1px solid #ddd;
  font-size: 18px;
  max-height:80px; overflow-x:auto
}
.myTableZaxiralist th{
  font-weight: 600;
  font-size:12px;
}
.myTableZaxiralist td{
  font-size:13px;
}
.myTableZaxiralist th, .myTableZaxiralist td {
  text-align: left;
  padding: 7px;
}

.myTableZaxiralist tr {
  border-bottom: 1px solid rgb(240, 240, 240);
}

.myTableZaxiralist tr.header, .myTableZaxiralist tr:hover {
  // background-color: #f1f1f1;
}
.delIcon{
  color: rgb(251, 70, 70);
  font-size: 13px;
}
.bg-header-all_qty{
  background: rgb(222, 255, 221);
  padding: 0px 10px;
}
.border_all_qty{
  border-bottom: 1px solid orange;
}
.all_zaxira_item{
  background-image: radial-gradient( circle farthest-corner at 10% 20%,  rgb(255, 224, 194) 0%, rgb(253, 205, 187) 90.1% ) ;
}
.all_zaxira_item:hover{
  cursor: pointer;
  background-image: radial-gradient( circle farthest-corner at 10% 20%,  rgb(200, 220, 230) 0%, rgb(196, 223, 228) 90.1% ) !important;

}
</style>