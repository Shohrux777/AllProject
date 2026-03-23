<template>
  <div class="change-without-reg-page">
    <!-- Header -->
    <div class="d-flex justify-content-between align-items-center change-header-card">
      <h5 class="m-0 change-header-title">
        Registratsiya qilmasdan almashtirilganlar
      </h5>
    </div>

    <!-- Filter Section -->
    <div class="container-fluid mt-2">
      <div class="card change-filter-card">
        <div class="card-body py-0 px-3">
          <form @submit.prevent="apply">
            <div class="d-flex align-items-end flex-wrap change-filter-row">
              <div class="change-date-wrapper mr-3  mb-md-0">
                <div class="change-search-wrapper">
                  <label class="change-search-label">
                    {{$t('start_time')}}
                  </label>
                  <mdb-input
                    type="date"
                    size="sm"
                    v-model="Start_time"
                    outline
                    group
                  />
                </div>
              </div>
              <div class="change-date-wrapper mr-3  mb-md-0">
                <div class="change-search-wrapper">
                  <label class="change-search-label">
                    {{$t('end_time')}}
                  </label>
                  <mdb-input
                    type="date"
                    size="sm"
                    v-model="End_time"
                    outline
                    group
                  />
                </div>
              </div>
              <div class="change-button-wrapper">
                <mdb-btn
                  @click="apply"
                  class="change-apply-btn mb-4"
                  m="r3"
                  size="sm"
                  p="r4 l4 t2 b2"
                >
                  <mdb-icon icon="check" class="mr-1" />
                  {{$t('apply')}}
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
          <div class="change-table-wrapper">
            <table class="change-table">
              <thead>
                <tr class="change-table-header">
                  <th width="50" class="text-left">№</th>
                  <th>{{$t('product')}}</th>
                  <th>{{$t('measure')}}</th>
                  <th>{{$t('summ')}}</th>
                  <th>{{$t('user')}}</th>
                  <th>{{$t('date')}}</th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="(row,rowIndex) in get_payment_list" :key="rowIndex" class="change-table-row">
                  <td><span class="change-table-cell">{{rowIndex+1}}</span></td>
                  <td><span class="change-table-cell">{{row.product.name}}</span></td>
                  <td><span class="change-table-cell">{{formatQty(row.qty_real)}}</span></td>
                  <td><span class="change-table-cell">{{row.summ}}</span></td>
                  <td><span class="change-table-cell">{{row.user_name}}</span></td>
                  <td>
                    <span class="change-table-cell">{{row.updated_date_time.slice(0,10)}}</span>
                    <span class="ml-2 change-table-cell">{{row.updated_date_time.slice(11,16)}}</span>
                  </td>
                </tr>
                <tr class="change-table-footer">
                  <td><span class="change-table-cell font-weight-bold">{{get_payment_list.length}}</span></td>
                  <td><span class="change-table-cell font-weight-bold">{{$t('all')}}</span></td>
                  <td><span class="change-table-cell font-weight-bold">{{allqty.toFixed()}} кг</span></td>
                  <td><span class="change-table-cell font-weight-bold">{{summa}} сум</span></td>
                  <td><span class="change-table-cell"></span></td>
                  <td><span class="change-table-cell"></span></td>
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
    <modal-train  :show="invoice_show" headerbackColor="white"  titlecolor="black" :title="$t('show_davernis')" 
        @close="invoice_show = false" width="85%">
          <template v-slot:body>
            <info-invoice @close="invoice_show = false" v-if="invoice_show"  :option="invoice_list" >
            </info-invoice>
          </template>
      </modal-train>
    <block ref="blocked"></block>
    <Toast ref="message"></Toast>
     <massage_box :hide="modal_status" :detail_info="modal_info"
      :m_text="$t('Failed_to_delete')" @to_hide_modal = "modal_status= false"/>
  </div>

</template>

<script>

  import { mdbBtn, mdbBadge, mdbInput, mdbIcon,  mdbModal, mdbModalHeader,   mdbModalBody,mdbModalFooter   } from 'mdbvue';
  import {mapActions, mapGetters, mapMutations} from 'vuex'
  import infoInvoice from '../mainPage/infoInvoice.vue'

  // import 'vue2-datepicker/index.css';
  export default {
    components: {
      mdbBtn,
      mdbModal, mdbModalHeader, mdbIcon, mdbModalBody, mdbModalFooter,
       mdbInput, mdbBadge, infoInvoice
      
    },
    data(){
      return{
        user_id: 0,
        show_otchert: false,
        invoice_show: false,
        invoice_list: [],
        select_invoice_id: null,
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
        index: null,
        service_id: null,
        summa : 0,
        allqty : 0,
       
      }
    },
    async mounted(){
      if(localStorage.AccessType == 1){
        this.$refs.blocked.show_block();
        return;
      }
      let time1 = new Date();
      this.Start_time = time1.toISOString().slice(0,10); 
      this.End_time = time1.toISOString().slice(0,10);
      await this.apply();
    },
   
    computed: mapGetters(['get_contragent_list', ]),
    methods: {
      ...mapActions(['fetch_contragent', ]),
      ...mapMutations(['district_row_delete',]),
      add(){
        this.show =! this.show
        this.editData = {};
      },
      print(){
        this.$refs.listlar.generatePdf()
      },
       async selectInvoiceItem(data){
      console.log(data)
      console.log(data.id)
      this.select_invoice_id = data.id;
      try{
        this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/TegirmonInvoice/getAnyInvoiceFullInfoById?invoice_id=" + this.select_invoice_id);
        const data = await response.json();
        console.log('weqeqw')
        console.log(data)
        this.loading = false;
        if(response.status == 201 || response.status == 200)
        {
          this.invoice_list = data;
          this.invoice_show = true;
          this.$refs.message.success('Added_successfully')
          return true;
        }
        else{
          this.modal_info = this.$i18n.t('network_ne_connect');
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
        this.summa = 0;
        this.allqty = 0;
        // console.log('dasdas')
        let newdate = new Date().toISOString().slice(0,10);
        console.log(newdate)
        let b_date  = this.Start_time + 'T00:00:35.000Z';
        let e_date  = this.End_time + 'T23:59:35.000Z';
        console.log(b_date)
        console.log(e_date)
        console.log(this.user_id)
        try{
          this.loading = true;
          const response = await fetch(this.$store.state.hostname + "/TegirmonInvoice/getPaginationBugdoyniRegistratsiyaQilmasdanNarsalargaALmashtrilganlarListi?page=0&size=1000&b_date="  + b_date + '&e_date='+ e_date);
          this.loading = false;
          if(response.status == 201 || response.status == 200)
          {
            const data = await response.json();
            console.log(data ,  'sadasd data open')

            this.get_payment_list = data.items_list;
            for(let i=0; i<data.items_list.length; i++){
              this.summa += parseFloat(data.items_list[i].summ);
              this.allqty += parseFloat(data.items_list[i].qty_real);
            }
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
      formatQty(value) {
        if (!value && value !== 0) return '';
        
        // Agar butun son bo'lsa, faqat butun sonni qaytaramiz
        if (value % 1 === 0) {
          return value.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1 ');
        }
        
        // Nuqtadan keyin 2 ta raqamgacha formatlash
        const formatted = value.toFixed(2);
        
        // Mingdan keyin space qo'shish
        // Avval butun qism va kasr qismini ajratamiz
        const parts = formatted.split('.');
        const integerPart = parts[0].replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1 ');
        const decimalPart = parts[1];
        
        // Agar kasr qismi "00" bo'lsa, faqat butun sonni qaytaramiz
        if (decimalPart === '00') {
          return integerPart;
        }
        
        // Agar kasr qismi oxirida 0 bo'lsa, uni olib tashlaymiz (masalan, 1.50 -> 1.5)
        const trimmedDecimal = decimalPart.replace(/0+$/, '');
        
        return trimmedDecimal ? `${integerPart}.${trimmedDecimal}` : integerPart;
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

.change-date-wrapper {
  min-width: 220px;
  max-width: 200px;
}

.change-button-wrapper {
  min-width: 200px;
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
  width: 150px;
}

.change-apply-btn:hover {
  filter: brightness(1.05);
  box-shadow: 0 3px 8px rgba(76, 175, 80, 0.55);
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

.change-table-row td {
  padding: 5px 12px;
  cursor: pointer;
}

.change-table-cell {
  font-size: 12px;
  color: #333;
}

.change-table-footer {
  background-color: #f8f9fa;
  border-top: 2px solid #b0bec5;
  font-weight: 600;
}

.change-table-footer td {
  padding: 7px 12px;
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