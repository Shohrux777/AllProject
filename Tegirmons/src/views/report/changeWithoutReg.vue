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

    <!-- Bug'doy statistikasi -->
    <div class="container-fluid mt-2">
      <div class="card change-filter-card">
        <div class="card-body py-3">
          <!-- <h6 class="mb-3">Bug'doy statistikasi ({{ Start_time }} - {{ End_time }})</h6> -->
          <div class="d-flex flex-wrap justify-content-between">
            <div style="width: 20%;" class="px-1" >
              <div class="stat-card" :style="{ background: 'linear-gradient(90deg, #64b0fb 0%, #7fd4ff 50%, #b5e3ff 100%)' }">
                <div class="stat-title">Klientlar soni</div>
                <div class="stat-value">{{ get_payment_list.length }}  </div>
                
              </div>
            </div>
            <div style="width: 20%;" class="px-1" v-for="(card, idx) in bugdoyCards" :key="idx">
              <div class="stat-card" :style="{ background: card.bg }">
                <div class="stat-title">{{ card.title }}</div>
                <div class="stat-value">{{ card.value }}  <span class="stat-sub ml-1">{{ card.subtitle }}</span></div>
                
              </div>
            </div>
          </div>
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
                  <th>{{$t('shafyor_name')}}</th>
                  <th>{{$t('car_number')}}</th>
                  <th>Общий масса</th>
                  <th>{{$t('note')}}</th>
                  <th>{{$t('date')}}</th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="(row,rowIndex) in get_payment_list" :key="rowIndex" class="change-table-row">
                  <td><span class="change-table-cell">{{rowIndex+1}}</span></td>
                  <td> <span class="change-table-cell" >{{row.shafyor_name}}</span> </td>
                  <td> <span class="change-table-cell" >{{row.car_number}}</span> </td>
                  <td> <span class="change-table-cell font-weight-bold" >{{row.real_qty}} кг</span> </td>
                  <td> <span class="change-table-cell" >{{row.note}}</span> </td>
                  <td> <span class="change-table-cell" >{{row.updated_date_time.slice(8,10) + '-' + row.updated_date_time.slice(5,7) + '-' + row.updated_date_time.slice(0,4)}}</span> <span class="ml-2 change-table-cell">{{row.updated_date_time.slice(11,16)}}</span> </td>
            
                </tr>
                <!-- <tr class="change-table-footer">
                  <td><span class="change-table-cell font-weight-bold">{{get_payment_list.length}}</span></td>
                  <td><span class="change-table-cell font-weight-bold">{{$t('all')}}</span></td>
                  <td><span class="change-table-cell font-weight-bold">{{allqty.toFixed()}} кг</span></td>
                  <td><span class="change-table-cell font-weight-bold">{{summa}} сум</span></td>
                  <td><span class="change-table-cell"></span></td>
                  <td><span class="change-table-cell"></span></td>
                </tr> -->
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
        bugdoyStats: {
          total_bugdoy_received: 0,
          total_changed_to_product: 0,
          total_sold_for_money: 0,
          total_sold_for_money_summ: 0,
          total_to_zaxira: 0,
        },
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
   
    computed: {
      ...mapGetters(['get_contragent_list']),
      bugdoyCards() {
        return [
          {
            title: "Olib kelingan bug'doy",
            value: this.formatQty(this.bugdoyStats.total_bugdoy_received),
            subtitle: "кг",
            bg: "linear-gradient(90deg, #64b0fb 0%, #7fd4ff 50%, #b5e3ff 100%)",
          },
          {
            title: "Mahsulotga almashtirildi",
            value: this.formatQty(this.bugdoyStats.total_changed_to_product),
            subtitle: "кг",
            bg: "linear-gradient(90deg, #42e695 0%, #3bb2b8 100%)",
          },
          {
            title: "Pulga sotib olindi",
            value: this.formatQty(this.bugdoyStats.total_sold_for_money),
            subtitle: "кг / " + this.formatNumber(this.bugdoyStats.total_sold_for_money_summ) + " so'm",
            bg: "linear-gradient(90deg, #f89b29 0%, #ff0f7b 100%)",
          },
          {
            title: "Zaxiraga quyildi",
            value: this.formatQty(this.bugdoyStats.total_to_zaxira),
            subtitle: "кг",
            bg: "linear-gradient(90deg, #89a7fe 0%, #66a6ff 100%)",
          },
        ];
      },
    },
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
      formatNumber(value) {
        if (value === null || value === undefined) return '0';
        const num = Number(value) || 0;
        return num.toLocaleString('ru-RU');
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
          
          // Bug'doy statistikasini olish
          const bugdoyParams = `?begin_date=${this.Start_time}&end_date=${this.End_time}`;
          const bugdoyUrl = this.$store.state.hostname + `/TegirmonTortilganBugdoyRoyxatiGroupDetails/getTodayBugdoyStatistics${bugdoyParams}`;
          console.log('Bugdoy API URL:', bugdoyUrl);
          console.log('Hostname:', this.$store.state.hostname);
          
          const bugdoyRes = await fetch(bugdoyUrl, {
            method: 'GET',
            headers: {
              'Accept': 'application/json',
              'Content-Type': 'application/json'
            }
          });
          console.log('bugdoyRes status:', bugdoyRes.status);
          console.log('bugdoyRes ok:', bugdoyRes.ok);
          console.log('bugdoyRes Content-Type:', bugdoyRes.headers.get('content-type'));
          console.log('bugdoyRes URL:', bugdoyRes.url);

          // Response ni clone qilamiz, chunki bir marta o'qiladi
          const clonedResponse = bugdoyRes.clone();

          if (bugdoyRes.status === 200 || bugdoyRes.status === 201) {
            const contentType = bugdoyRes.headers.get('content-type');
            if (contentType && contentType.includes('application/json')) {
              try {
                const bugdoyData = await bugdoyRes.json();
                console.log('bugdoyData:', bugdoyData);
                this.bugdoyStats = {
                  total_bugdoy_received: bugdoyData.total_bugdoy_received || 0,
                  total_changed_to_product: bugdoyData.total_changed_to_product || 0,
                  total_sold_for_money: bugdoyData.total_sold_for_money || 0,
                  total_sold_for_money_summ: bugdoyData.total_sold_for_money_summ || 0,
                  total_to_zaxira: bugdoyData.total_to_zaxira || 0,
                };
              } catch (jsonError) {
                console.error('JSON parse error:', jsonError);
                const textResponse = await clonedResponse.text();
                console.error('Response text (first 500 chars):', textResponse.substring(0, 500));
                this.$refs.message?.error('Statistikani olishda xatolik yuz berdi');
              }
            } else {
              // HTML yoki boshqa format qaytarsa
              const textResponse = await clonedResponse.text();
              console.error('Response is not JSON. Content-Type:', contentType);
              console.error('Response URL:', bugdoyRes.url);
              console.error('Response status:', bugdoyRes.status);
              console.error('Response text (first 1000 chars):', textResponse.substring(0, 1000));
              
              // Agar HTML bo'lsa, bu endpoint topilmagan yoki server noto'g'ri konfiguratsiya qilingan
              if (textResponse.includes('<!DOCTYPE') || textResponse.includes('<html')) {
                console.error('⚠️ Server HTML qaytardi. Bu shuni anglatadiki:');
                console.error('1. API endpoint topilmadi (404)');
                console.error('2. Yoki server SPA routing ishlatmoqda va barcha route larni index.html ga yo\'naltirayapti');
                console.error('3. Yoki backend server ishlamayapti');
                this.$refs.message?.error('API endpoint topilmadi. Server konfiguratsiyasini tekshiring.');
              } else {
                this.$refs.message?.error('Server noto\'g\'ri format qaytardi.');
              }
            }
          } else {
            const errorText = await clonedResponse.text();
            console.error('API error response:', bugdoyRes.status, errorText.substring(0, 500));
            this.$refs.message?.error(`API xatolik: ${bugdoyRes.status}`);
          }

          const response = await fetch(this.$store.state.hostname + "/TegirmonTortilganBugdoyRoyxatiGroups/getPaginationOnlyAcceptedByDate?page=0&size=5000&begin_date="  + b_date + '&end_date='+ e_date);
          this.loading = false;
          if(response.status == 201 || response.status == 200)
          {
            const data = await response.json();
            console.log(data ,  'sadasd data open')
            this.get_payment_list = data.items_list;
            // for(let i=0; i<data.items_list.length; i++){
            //   this.summa += parseFloat(data.items_list[i].summ);
            //   this.allqty += parseFloat(data.items_list[i].qty_real);
            // }
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
          this.modal_info = this.$i18n.t('network_ne_connectw'); 
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

.stat-card {
  border-radius: 12px;
  color: #fff;
  padding: 7px 16px;
  box-shadow: rgba(15, 35, 52, 0.25) 0px 10px 24px -12px;
  height: 100%;
}

.stat-title {
  font-size: 12.5px;
  font-weight: 600;
  opacity: 0.9;
}

.stat-value {
  font-size: 18px;
  font-weight: 700;
  margin-top: 4px;
}

.stat-sub {
  font-size: 11px;
  margin-top: 2px;
  opacity: 0.9;
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