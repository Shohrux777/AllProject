<template>
  <div class="change-without-reg-page">
    <!-- Header -->
    <div class="d-flex justify-content-between align-items-center change-header-card">
      <h5 class="m-0 change-header-title">
        Zaxira ro'yxati
      </h5>
    </div>

    <!-- Products Cards Section -->
    <div class="container-fluid mt-2" v-if="all_qty_product.length > 0">
      <div class="card change-filter-card">
        <div class="card-body py-3 px-3">
          <div class="row">
            <div class="col-12 col-md-3 col-lg-3 mb-2" v-for="(item, index) in all_qty_product" :key="index" v-show="item.qty>0" @click="selectProductZaxiraList(item)">
              <div class="card p-2 all_zaxira_item change-product-card">
                <div class="border_all_qty" v-show="item.qty>1">
                  <p style="font-size: 13px; font-weight: 600;" class="mb-1 p-0">{{item.name}}</p>
                  <p class="mb-0 p-0 text-right" style="font-size: 14px; font-weight: 700; color: #1976d2;">{{(item.qty.toFixed(1) || 0).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</p>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>

    <!-- Filter Section -->
    <div class="container-fluid mt-2">
      <div class="card change-filter-card">
        <div class="card-body py-0 px-3">
          <form @submit.prevent="apply">
            <div class="d-flex align-items-end flex-wrap change-filter-row">
              <div class="change-client-wrapper mr-3 mb-md-0">
                <div class="change-search-wrapper">
                  <label class="change-search-label">
                    {{$t('client')}}
                  </label>
                  <erpSelect
                    :options="allClient.rows"
                    @select="selectOptionUser"
                    :selected="user_name"
                    size="sm"
                    url="/TegirmonClient/getPaginationSearchByFioOrPassportSerailNumberOrHomeOrMobilePhoneNumber?page=0&size=100&fio_or_serial_number="
                    :label="$t('client')"
                  />
                </div>
              </div>
              <div class="change-button-wrapper">
                <mdb-btn @click="apply" class="change-apply-btn  mr-2"  size="sm" style="margin-bottom: 12px;" p="r4 l4 t2 b2">
                  <mdb-icon icon="redo" class="mr-1" />
                  {{$t('refresh')}}
                </mdb-btn>
                <mdb-btn @click="clicked_excel('Table', 'LoremTable')" class="change-excel-btn "  size="sm" style="margin-bottom: 12px;" p="r4 l4 t2 b2">
                  <mdb-icon icon="file-excel" far class="mr-1" />
                  Ехсел
                </mdb-btn>
              </div>
            </div>
          </form>
        </div>
      </div>
    </div>

    <!-- Table Section -->
    <div class="container-fluid mt-2">
      <div class="card change-table-card">
        <div class="card-body p-0">
          <loaderTable v-if="loading"/>
          <div v-else class="change-table-wrapper">
            <table ref="table" id="table" class="change-table">
              <thead>
                <tr class="change-table-header">
                  <th width="50" class="text-left">№</th>
                  <th>{{$t('client')}}</th>
                  <th>{{$t('product')}}</th>
                  <th>{{$t('qty')}}</th>
                  <th>{{$t('real_qty')}}</th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="(row,rowIndex) in get_payment_list" :key="rowIndex" class="change-table-row" :class="{'change-table-row-danger': row.qty<-2}">
                  <td><span class="change-table-cell">{{rowIndex+1}}</span></td>
                  <td><span class="change-table-cell">{{row.client.fio}}</span></td>
                  <td><span class="change-table-cell">{{row.product.name}}</span></td>
                  <td><span class="change-table-cell">{{row.qty.toFixed(1)}}</span></td>
                  <td><span class="change-table-cell">{{row.real_qty.toFixed(1)}}</span></td>
                </tr>
              </tbody>
            </table>
          </div>
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
.change-without-reg-page {
  padding: 8px 10px 14px 10px;
  background: #f5f7fb;
  min-height: 100%;
}

.change-header-card {
  padding: 14px 16px;
  border-radius: 10px;
  background-image: linear-gradient(90deg, #64b0fb 0%, #7fd4ff 50%, #b5e3ff 100%);
  box-shadow: rgba(15, 35, 52, 0.25) 0px 8px 20px -12px;
  color: #fff;
}

.change-header-title {
  font-size: 18px;
  font-weight: 600;
  letter-spacing: 0.3px;
}

.change-filter-card {
  border-radius: 10px;
  border: none;
  box-shadow: rgba(60, 64, 67, 0.15) 0px 1px 2px 0px,
              rgba(60, 64, 67, 0.1) 0px 1px 3px 1px;
  background: #ffffff;
}

.change-filter-row {
  gap: 12px;
}

.change-client-wrapper {
  min-width: 300px;
  flex: 1;
}

.change-button-wrapper {
  display: flex;
  gap: 8px;
}

.change-search-wrapper {
  position: relative;
  margin-top: 6px;
}

.change-search-label {
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

.change-apply-btn {
  font-size: 10px !important;
  padding: 6px 16px !important;
  border-radius: 20px;
  background-image: linear-gradient(90deg, #4caf50 0%, #66bb6a 100%) !important;
  color: #ffffff !important;
  border: none;
  box-shadow: 0 2px 6px rgba(76, 175, 80, 0.4);
  min-width: 120px;
}

.change-apply-btn:hover {
  filter: brightness(1.05);
  box-shadow: 0 3px 8px rgba(76, 175, 80, 0.55);
}

.change-excel-btn {
  font-size: 10px !important;
  padding: 6px 16px !important;
  border-radius: 20px;
  background-image: linear-gradient(90deg, #1976d2 0%, #42a5f5 100%) !important;
  color: #ffffff !important;
  border: none;
  box-shadow: 0 2px 6px rgba(25, 118, 210, 0.4);
  min-width: 120px;
}

.change-excel-btn:hover {
  filter: brightness(1.05);
  box-shadow: 0 3px 8px rgba(25, 118, 210, 0.55);
}

.change-product-card {
  border-radius: 8px;
  border: none;
  box-shadow: rgba(60, 64, 67, 0.1) 0px 1px 2px 0px;
  transition: all 0.3s;
  cursor: pointer;
}

.change-product-card:hover {
  transform: translateY(-2px);
  box-shadow: rgba(60, 64, 67, 0.2) 0px 2px 4px 0px;
}

.change-table-card {
  border-radius: 10px;
  border: none;
  box-shadow: rgba(60, 64, 67, 0.15) 0px 1px 2px 0px,
              rgba(60, 64, 67, 0.1) 0px 1px 3px 1px;
  background: #ffffff;
}

.change-table-wrapper {
  overflow-x: auto;
  max-height: calc(100vh - 170px);
  overflow-y: auto;
}

.change-table {
  width: 100%;
  border-collapse: collapse;
  font-size: 13px;
}

.change-table-header {
  background-color: #C4DEE4;
  position: sticky;
  top: 0;
  z-index: 10;
}

.change-table-header th {
  font-weight: 600;
  font-size: 12px;
  padding: 7px 12px;
  text-align: left;
  color: #333;
  border-bottom: 2px solid #b0bec5;
}

.change-table-row {
  border-bottom: 1px solid #e0e0e0;
  transition: background-color 0.2s;
}

.change-table-row:hover {
  background-color: #f5f5f5;
}

.change-table-row-danger {
  background-color: #ffebee !important;
}

.change-table-row-danger:hover {
  background-color: #ffcdd2 !important;
}

.change-table-row td {
  padding: 5px 12px;
  cursor: pointer;
}

.change-table-cell {
  font-size: 12px;
  color: #333;
}

.border_all_qty {
  border-bottom: 2px solid #ff9800;
}

.all_zaxira_item {
  background-image: radial-gradient(circle farthest-corner at 10% 20%, rgb(255, 224, 194) 0%, rgb(253, 205, 187) 90.1%);
  transition: all 0.3s;
}

.all_zaxira_item:hover {
  cursor: pointer;
  background-image: radial-gradient(circle farthest-corner at 10% 20%, rgb(200, 220, 230) 0%, rgb(196, 223, 228) 90.1%) !important;
  transform: translateY(-2px);
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
  background: #fff;
  position: relative;
  z-index: 5000;
}
.showing{
  display: none;
}
.delIcon{
  color: rgb(251, 70, 70);
  font-size: 13px;
}
</style>