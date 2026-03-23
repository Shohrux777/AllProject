<template>
  <div class="check_info p-2">
    <div class="header">
        <div class="d-flex justify-content-start align-items-center mb-2 mt-1">
            <div class="d-flex align-items-center" style="gap: 15px;">
                <div class="date-input-wrapper">
                    <input
                        type="date"
                        v-model="start_date"
                        @change="onDateChange"
                        class="form-control date-input mt-0"
                    />
                </div>
                <div class="date-input-wrapper">
                    <input
                        type="date" 
                        v-model="end_date"
                        @change="onDateChange"
                        class="form-control date-input"
                    />
                </div>
            </div>
            <div class="border-bottom  pb-1 ml-5">
              <span style="color:#8697BA; font-style: italic; font-size: 14px;" class="bg-primary text-white px-3 py-1 rounded"> {{ start_date }} ➡️ {{ end_date }} kundagi ketgan tovar miqdori 🚚💨
              </span>
          </div>
        </div>
        <div class="row my-2 container-fluid">
            <div class="col-4 px-1 mt-1">
              <div class="main_kassa_hisoblar main_kassa_sell_clients">
                  <small>
                    Sotuv bo'limidan checklar
                  </small>
                  <p class="summ_titled">
                    {{ client_counts.sellChecksCount }} ta
                    <span v-if="client_counts.prevSellChecksCount > 0" 
                          :class="getChangeClass(calculatePercentageChange(client_counts.sellChecksCount, client_counts.prevSellChecksCount))"
                          class="change-badge">
                      {{ formatPercentageChange(calculatePercentageChange(client_counts.sellChecksCount, client_counts.prevSellChecksCount)) }}
                    </span>
                  </p>
              </div>
            </div>
            <div class="col-4 px-1 mt-1">
              <div class="main_kassa_hisoblar main_kassa_invoice_clients">
                  <small>
                    Zaxiradan olgan mijozlar
                  </small>
                  <p class="summ_titled">
                    {{ client_counts.invoiceAcceptedClientsCount }} ta
                    <span v-if="client_counts.prevInvoiceAcceptedClientsCount > 0" 
                          :class="getChangeClass(calculatePercentageChange(client_counts.invoiceAcceptedClientsCount, client_counts.prevInvoiceAcceptedClientsCount))"
                          class="change-badge">
                      {{ formatPercentageChange(calculatePercentageChange(client_counts.invoiceAcceptedClientsCount, client_counts.prevInvoiceAcceptedClientsCount)) }}
                    </span>
                  </p>
              </div>
            </div>
            <div class="col-4 px-1 mt-1">
              <div class="main_kassa_hisoblar main_kassa_change_clients">
                  <small>
                    Almashtirib ketganlar
                  </small>
                  <p class="summ_titled">
                    {{ client_counts.invoiceOnlyChangeCount }} ta
                    <span v-if="client_counts.prevInvoiceOnlyChangeCount > 0" 
                          :class="getChangeClass(calculatePercentageChange(client_counts.invoiceOnlyChangeCount, client_counts.prevInvoiceOnlyChangeCount))"
                          class="change-badge">
                      {{ formatPercentageChange(calculatePercentageChange(client_counts.invoiceOnlyChangeCount, client_counts.prevInvoiceOnlyChangeCount)) }}
                    </span>
                  </p>
              </div>
            </div>
        </div>
        <div class="row my-1 container-fluid">
            <div class="col-3 px-1 mt-1">
              <div class="main_kassa_hisoblar main_kassa_bg">
                  <small>
                    {{$t('all_measure')}}
                  </small>
                  <p class="summ_titled">
                    {{formatNumber(all_sht).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} шт
                      ⮕ {{formatNumber(all_kg).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} кг
                  </p>
              </div>
            </div>
            <div class="col-3 px-1 mt-1"
              v-for="(item , index) in order_category_list" :key="index" 
              v-show="item.auth_user_creator_id>0 || index == 0">
              <div class="main_kassa_hisoblar main_kassa_bg" :style="`background: ${item.color} !important`">
                  <small>
                    {{item.name}}
                  </small>
                  <p class="summ_titled">
                    {{formatNumber(item.auth_user_creator_id).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} шт
                      ⮕ {{formatNumber(item.auth_user_updator_id).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} кг
                  </p>
              </div>
            </div>
        </div>
        
    </div>
    <div class="check_list mt-1">
        <table class="w-100  myTableuserSalaryList rounded my-2 px-2">
            <thead class="bg-gray-200 header py-3 info_client_header ">
                <tr class="bg-gray-100 info_client_header1">
                    <th width="300" class=" text-left">{{ $t('product') }}</th>
                    <th class=" text-left">Общий {{$t('qty')}}</th>

                    <th class=" text-left">Продажа ✅</th>

                    <th class=" text-left">Захира ✅ </th>

                    <th class=" text-left">Тароз список ✅ </th>
                    <th width="120"  class=" text-left">{{$t('category')}}</th>
                </tr>
            </thead>
            <tbody>
              <tr v-for="item in order_load_list" :key="item.id" :style="`background: ${item.product.shitrix_code}`">
                  <td class="p-2" ><span v-if="item.productId">{{ item.productName }}</span></td>
                  
                  <td class="p-2"><span>{{ formatNumber(item.totalQty).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} {{ item.unit }}</span>
                    <span>
                      ➡️ {{ formatNumber(item.totalQty * item.product.real_qty).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} кг
                    </span>
                  </td>

                  <td class="p-2">
                    <span 
                      @click="openDetailsModal(item, 'sell')" 
                      style="cursor: pointer; text-decoration: underline; "
                      title="Batafsil ko'rish"
                    >
                      {{ formatNumber(item.sellQty).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} {{ item.unit }}
                    </span>
                    <span>
                      ➡️ {{ formatNumber(item.sellQty * item.product.real_qty).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} кг
                    </span>
                  </td>

                  <td class="p-2">
                    <span 
                      @click="openDetailsModal(item, 'invoiceAccepted')" 
                      style="cursor: pointer; text-decoration: underline; "
                      title="Batafsil ko'rish"
                    >
                      {{ formatNumber(item.invoiceAcceptedQty).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} {{ item.unit }}
                    </span>
                    <span>
                      ➡️ {{ formatNumber(item.invoiceAcceptedQty * item.product.real_qty).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} кг
                    </span>
                  </td>

                  <td class="p-2">
                    <span 
                      @click="openDetailsModal(item, 'invoiceOnlyChange')" 
                      style="cursor: pointer; text-decoration: underline; "
                      title="Batafsil ko'rish"
                    >
                      {{ formatNumber(item.invoiceOnlyChangeQty).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} {{ item.unit }}
                    </span>
                    <span>
                      ➡️ {{ formatNumber(item.invoiceOnlyChangeQty * item.product.real_qty).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} кг
                    </span>
                  </td>

                  
                  
                  <td class="p-2" ><span style="font-size: 10px; ">{{ item.product.print_name }}</span></td>
              </tr>
            </tbody>
        </table>
    </div>

    <!-- Details Modal -->
    <modal-train 
      :show="detailsModalShow" 
      headerbackColor="white" 
      titlecolor="black" 
      :title="detailsModalTitle" 
      @close="detailsModalShow = false" 
      width="70%"
    >
      <template v-slot:body>
        <div v-if="detailsLoading" class="text-center p-4">
          <div class="spinner-border" role="status">
            <span class="sr-only">Yuklanmoqda...</span>
          </div>
        </div>
        <div v-else class="details-table-wrapper">
          <table class="details-table">
            <thead>
              <tr class="details-table-header">
                <th width="50">№</th>
                <th v-if="detailsType === 'sell'">Check ID</th>
                <th v-if="detailsType === 'sell'">Kassir</th>
                <th v-if="detailsType === 'invoiceAccepted'">Mijoz</th>
                <th v-if="detailsType === 'invoiceOnlyChange'">Invoice ID</th>
                <th>Sana</th>
                <th>Miqdor ({{ detailsUnit }})</th>
                <th v-if="detailsType === 'sell'">Summa</th>
                <th v-if="detailsType === 'sell'">Foyda</th>
                <th v-if="detailsType !== 'sell'">Summa</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="(detail, index) in detailsList" :key="detail.id" class="details-table-row">
                <td>{{ index + 1 }}</td>
                <td v-if="detailsType === 'sell'">{{ detail.checkId }}</td>
                <td v-if="detailsType === 'sell'">{{ detail.kassirName }}</td>
                <td v-if="detailsType === 'invoiceAccepted'">{{ detail.clientName || 'Noma\'lum' }}</td>
                <td v-if="detailsType === 'invoiceOnlyChange'">{{ detail.invoiceId }}</td>
                <td>
                  {{ formatDate(detail.checkDate || detail.invoiceDate) }}
                </td>
                <td>
                  {{ formatNumber(detail.qty || detail.real_qty).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}
                </td>
                <td v-if="detailsType === 'sell'">
                  {{ formatNumber(detail.summa).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} сум
                </td>
                <td v-if="detailsType === 'sell'">
                  {{ formatNumber(detail.profit_summ).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} сум
                </td>
                <td v-if="detailsType !== 'sell'">
                  {{ formatNumber(detail.sum || detail.real_sum).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} сум
                </td>
              </tr>
              <tr v-if="detailsList.length === 0" class="details-table-row">
                <td :colspan="detailsType === 'sell' ? 7 : 6" class="text-center p-3">
                  Ma'lumot topilmadi
                </td>
              </tr>
            </tbody>
          </table>
        </div>
      </template>
    </modal-train>
    <Toast ref="message"></Toast>
  </div>
</template>

<script>
import {mapActions, mapGetters} from 'vuex'

export default {
  data(){
    return{
      start_date: '',
      end_date: '',
      order_load_list: [],
      order_category_list: [],
      all_sht: 0,
      all_kg: 0,
      client_counts: {
        sellChecksCount: 0,
        prevSellChecksCount: 0,
        invoiceAcceptedClientsCount: 0,
        prevInvoiceAcceptedClientsCount: 0,
        invoiceOnlyChangeCount: 0,
        prevInvoiceOnlyChangeCount: 0
      },
      detailsModalShow: false,
      detailsModalTitle: '',
      detailsType: '', // 'sell', 'invoiceAccepted', 'invoiceOnlyChange'
      detailsList: [],
      detailsLoading: false,
      detailsUnit: ''
    }
  },
  props: {
    // client_info:
    // {
    //   type: Object,
    //   default(){
    //     return {
    //       fio: '',
    //       id:null,
    //     }
    //   }
    // },
    choosen_day: {
      type: String,
      default: ''
    },
  },
  // async mounted(){
  //   await this.fetch_order_all_naqd();
  //   await this.fetch_order_all_summa_online();
  // },
   computed: mapGetters(['allcategory']),
  methods:{
    ...mapActions(['fetchcategory', ]),
    async func_mounted(){
      // Date inputlarni choosen_day ga tenglashtirish
      if(this.choosen_day){
        this.start_date = this.choosen_day;
        this.end_date = this.choosen_day;
      }
      
      await this.fetch_get_order_items_summary();
      await this.fetch_client_counts();
      await this.fetchcategory();
      console.log(this.allcategory);

      //  bu catolog uchu kerak buladi
      this.order_category_list = this.allcategory.rows;
      for(let i=0; i<this.order_category_list.length; i++){
        let qty = 0;
        let qty_kg = 0;
        for(let j=0; j< this.order_load_list.length; j++){
          if(this.order_category_list[i].id == this.order_load_list[j].product.bot_id){
            qty += this.order_load_list[j].totalQty;
            qty_kg += (this.order_load_list[j].totalQty * this.order_load_list[j].product.real_qty);
          }
        }
        this.order_category_list[i].auth_user_creator_id = qty;
        this.order_category_list[i].auth_user_updator_id = qty_kg;
      }
      console.log(this.order_category_list)

    },
    async onDateChange(){
      await this.fetch_get_order_items_summary();
      await this.fetch_client_counts();
      // Category listni ham yangilash
      for(let i=0; i<this.order_category_list.length; i++){
        let qty = 0;
        let qty_kg = 0;
        for(let j=0; j< this.order_load_list.length; j++){
          if(this.order_category_list[i].id == this.order_load_list[j].product.bot_id){
            qty += this.order_load_list[j].totalQty;
            qty_kg += (this.order_load_list[j].totalQty * this.order_load_list[j].product.real_qty);
          }
        }
        this.order_category_list[i].auth_user_creator_id = qty;
        this.order_category_list[i].auth_user_updator_id = qty_kg;
      }
    },
    formatNumber(value) {
      if (value % 1 === 0) {
        // Butun son bo'lsa, o'zgartirmaymiz
        return value;
      } else {
        // 2 ta onlik raqamgacha kesamiz
        return Math.floor(value * 10) / 10;
      }
    },
    calculatePercentageChange(current, previous) {
      if (previous === 0) return 0;
      return ((current - previous) / previous) * 100;
    },
    formatPercentageChange(percentage) {
      if (percentage === 0) return '0%';
      const sign = percentage > 0 ? '+' : '';
      return `${sign}${percentage.toFixed(1)}%`;
    },
    getChangeClass(percentage) {
      if (percentage > 0) return 'change-positive';
      if (percentage < 0) return 'change-negative';
      return 'change-neutral';
    },
    
    async fetch_get_order_items_summary(){
      // Agar date inputlar bo'sh bo'lsa, choosen_day dan foydalanamiz
      let startDate = this.start_date || this.choosen_day;
      let endDate = this.end_date || this.choosen_day;
      
      let b_data = startDate + 'T00:00:00.000Z';
      let e_data = endDate + 'T00:00:00.000Z';
      this.order_load_list = []
      this.all_sht = 0;
      this.all_kg = 0;
      try{
        const res = await fetch(this.$store.state.hostname + '/TegirmonPayments/order-items-summary?begin_date=' +  b_data + '&end_date=' + e_data);
        const res_data = await res.json();
        console.log('res_data order check hammasi online uchun data')
        console.log(res_data)
          if(res.status == 200 || res.status == 201){
            this.order_load_list = res_data;
            this.all_sht = this.order_load_list.reduce((sum, item) => {
              return sum + (item.totalQty || 0)
            }, 0)
            this.all_kg = this.order_load_list.reduce((sum, item) => {
              return sum + ((item.totalQty * item.product.real_qty) || 0)
            }, 0)
            this.all_sht = this.all_sht.toFixed(1);
            this.all_kg = this.all_kg.toFixed(1);
          }
      }
      catch{
        console.log('2')
        this.$refs.alert.error("Serverda uzilish bor. Qayta urinib ko'ring !");
        return false;
      }

    },
    async fetch_client_counts(){
      // Agar date inputlar bo'sh bo'lsa, choosen_day dan foydalanamiz
      let startDate = this.start_date || this.choosen_day;
      let endDate = this.end_date || this.choosen_day;
      
      let b_data = startDate + 'T00:00:00.000Z';
      let e_data = endDate + 'T00:00:00.000Z';

      try{
        const res = await fetch(this.$store.state.hostname + '/TegirmonPayments/client-count-summary?begin_date=' +  b_data + '&end_date=' + e_data);
        const res_data = await res.json();
        console.log('res_data client counts')
        console.log(res_data)
          if(res.status == 200 || res.status == 201){
            this.client_counts = res_data;
          }
      }
      catch{
        console.log('2')
        this.$refs.alert.error("Serverda uzilish bor. Qayta urinib ko'ring !");
        return false;
      }

    },
    async openDetailsModal(item, type) {
      this.detailsType = type;
      this.detailsUnit = item.unit;
      this.detailsLoading = true;
      this.detailsModalShow = true;
      
      let startDate = this.start_date || this.choosen_day;
      let endDate = this.end_date || this.choosen_day;
      let b_data = startDate + 'T00:00:00.000Z';
      let e_data = endDate + 'T00:00:00.000Z';
      
      let apiUrl = '';
      if (type === 'sell') {
        this.detailsModalTitle = `Продажа ✅ - ${item.productName}`;
        apiUrl = `/TegirmonPayments/getProductSellDetails?product_id=${item.productId}&begin_date=${b_data}&end_date=${e_data}`;
      } else if (type === 'invoiceAccepted') {
        this.detailsModalTitle = `Захира ✅ - ${item.productName}`;
        apiUrl = `/TegirmonPayments/getProductInvoiceAcceptedDetails?product_id=${item.productId}&begin_date=${b_data}&end_date=${e_data}`;
      } else if (type === 'invoiceOnlyChange') {
        this.detailsModalTitle = `Тароз список - ${item.productName}`;
        apiUrl = `/TegirmonPayments/getProductInvoiceOnlyChangeDetails?product_id=${item.productId}&begin_date=${b_data}&end_date=${e_data}`;
      }

      try {
        const res = await fetch(this.$store.state.hostname + apiUrl);
        if (res.status === 200 || res.status === 201) {
          const data = await res.json();
          this.detailsList = data;
        } else {
          this.$refs.message.error("Ma'lumotlarni yuklashda xatolik yuz berdi");
          this.detailsList = [];
        }
      } catch (error) {
        console.error('Details fetch error:', error);
        this.$refs.message.error("Serverda uzilish bor. Qayta urinib ko'ring !");
        this.detailsList = [];
      } finally {
        this.detailsLoading = false;
      }
    },
    formatDate(dateString) {
      if (!dateString) return '';
      const date = new Date(dateString);
      const day = String(date.getDate()).padStart(2, '0');
      const month = String(date.getMonth() + 1).padStart(2, '0');
      const year = date.getFullYear();
      const hours = String(date.getHours()).padStart(2, '0');
      const minutes = String(date.getMinutes()).padStart(2, '0');
      return `${day}.${month}.${year} ${hours}:${minutes}`;
    },
  }
}
</script>

<style lang="scss" scoped>
.main_kassa_hisoblar{
  padding: 3px 7px;
  border-radius: 7px;
  background-image: radial-gradient( circle 502px at 2.7% 23.7%,  rgba(142,5,254,1) 0%, rgba(33,250,214,1) 99.6% );

  small{
    color:#f8f8ff;
    font-size: 14px;

  }
  .summ_title{
    color:#ffffff;
    font-size: 18px;
    text-align: right;
    padding-bottom: 0px;
    margin-bottom: 0px;
    margin-top: 5px;
  }
  .summ_titled{
    color:#ffffff;
    font-size: 16px;
    text-align: right;
    padding-bottom: 0px;
    margin-bottom: 0px;
    margin-top: 5px;
  }
  .summ_title_diff{
    color:#ffffff;
    font-size: 14px;
    text-align: right;
    padding-bottom: 0px;
    margin-bottom: 0px;
    margin-top: 10px;
  }
}
.main_kassa_bg{
  //background-image: radial-gradient( circle farthest-corner at 10% 20%,  rgba(239,87,87,0.74) 0%, rgba(245,123,13,0.74) 90.2% );
  background-image: linear-gradient( 90deg,  rgba(255,157,129,1) 24.3%, rgb(255, 110, 78) 78.3% );
}
.main_kassa_warning{
  background-image: linear-gradient( 90deg,  rgb(219, 193, 119) 24.3%, rgb(255, 177, 88) 78.3% );
}
.main_kassa_balance{
  background-image: linear-gradient( 90deg,  rgb(255, 110, 110) 24.3%, rgb(236, 93, 129) 78.3% );

}
.main_kassa_poluchit{
  background-image: linear-gradient( 90deg,  rgb(117, 207, 165) 24.3%, rgb(93, 236, 177) 78.3% );

}
.main_kassa_sell_clients{
  background-image: linear-gradient( 90deg,  rgb(74, 144, 226) 24.3%, rgb(100, 181, 246) 78.3% );
}
.main_kassa_invoice_clients{
  background-image: linear-gradient( 90deg,  rgb(156, 39, 176) 24.3%, rgb(186, 104, 200) 78.3% );
}
.main_kassa_change_clients{
  background-image: linear-gradient( 90deg,  rgb(255, 152, 0) 24.3%, rgb(255, 183, 77) 78.3% );
}
.hisob_bg{
  background-image: radial-gradient( circle 502px at 2.7% 23.7%,  rgba(142,5,254,1) 0%, rgba(33,250,214,1) 99.6% );

}
.myTableuserSalaryList {
  /* border-collapse: collapse; */
  table-layout:fixed;
  width: 100%;
  overflow: hidden;
  overflow-y: scroll;
  // border: 1px solid #ddd;
  font-size: 16px;
  max-height:80px; overflow-x:auto
}
.myTableuserSalaryList th{
  font-weight: 600;
  font-size:11px;
}
.myTableuserSalaryList td{
  font-size:11.5px;
}
.myTableuserSalaryList tbody td small{
  font-size:11px;
}
.myTableuserSalaryList td {
  text-align: left;
  padding: 3px 10px;
  // border-right: 1px solid rgb(219, 219, 219);
}
.myTableuserSalaryList th{
  text-align: left;
  padding: 5px 10px;

}

.myTableuserSalaryList tr {
  border-bottom: 1px solid rgb(219, 219, 219);
  &:hover{
    background: #b1d3ff !important;
    cursor: pointer;
  }
}


.info_client_header{
  background-image: radial-gradient( circle farthest-corner at 10% 20%,  rgb(202, 226, 238) 0%, rgb(195, 236, 243) 90.1% );
}
.check_list{
    min-height: 400px;
    max-height: 650px;
    overflow: hidden;
    overflow-y: scroll;
}
.change-badge{
    margin-left: 8px;
    font-size: 12px;
    font-weight: 600;
    padding: 2px 6px;
    border-radius: 4px;
    display: inline-block;
    vertical-align: middle;
}
.change-positive{
    background-color: rgba(76, 175, 80, 0.2);
    color: #4caf50;
}
.change-negative{
    background-color: rgba(244, 67, 54, 0.2);
    color: #f44336;
}
.change-neutral{
    background-color: rgba(158, 158, 158, 0.2);
    color: #9e9e9e;
}
.date-input-wrapper{
    display: flex;
    flex-direction: column;
    gap: 5px;
}
.date-label{
    font-size: 12px;
    color: #506286;
    font-weight: 500;
    margin-left: 5px;

}
.date-input{
    padding: 8px 12px;
    border: 1px solid #ddd;
    border-radius: 5px;
    font-size: 14px;
    min-width: 180px;
    &:focus{
        outline: none;
        border-color: #4a90e2;
        box-shadow: 0 0 0 2px rgba(74, 144, 226, 0.2);
    }
}
.details-table-wrapper {
  max-height: 500px;
  overflow-y: auto;
  padding: 10px;
}
.details-table {
  width: 100%;
  border-collapse: collapse;
  font-size: 13px;
}
.details-table-header {
  background-color: #C4DEE4;
  position: sticky;
  top: 0;
  z-index: 10;
}
.details-table-header th {
  font-weight: 600;
  font-size: 12px;
  padding: 10px 12px;
  text-align: left;
  color: #333;
  border-bottom: 2px solid #b0bec5;
}
.details-table-row {
  border-bottom: 1px solid #e0e0e0;
  transition: background-color 0.2s;
}
.details-table-row:hover {
  background-color: #f5f5f5;
}
.details-table-row td {
  padding: 8px 12px;
  font-size: 12px;
  color: #333;
}
</style>