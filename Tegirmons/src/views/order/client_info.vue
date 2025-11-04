<template>
  <div class="client_info_statistic py-2">
    <loader v-if="loading" />
    <div v-else class="container-fluid">
      
      <div class="row mt-1">
        <div class="col-9 border-right" style="position: relative;">
          <div class="activStatus">
            <span style="font-size: 13px; color:green">
              🟢 Активный клиент
            </span>
            
          </div>
          <!-- Client informatsiyasi page. -->
          <div class="client_info border-bottom">
            <div class="d-flex align-items-center">
              <div class=" user_photo">
                <img src="../../assets/client.png" style="height: 50px; overflow: none; " class="img-fluid" alt="">
              </div>
              <div class="client_details ml-3">
                <p>{{ client_info.fio }}</p>
                <span>📞 Тел: {{ client_info.phone_number }}</span>
                <span v-if="client_info.created_date">📅 Дата рег.: {{ client_info.created_date.slice(8,10) + '-' + client_info.created_date.slice(5,7) + '-' + client_info.created_date.slice(0,4)}} {{ client_info.created_date.slice(11,16) }}</span>
              </div>
            </div>
          </div>
          <!-- Client mahsuloti va hisobidagi pulini informatsiyasi.  -->
          <div class=" px-2 border-bottom" v-if="client_info.fio">
            <div class="w-100 p-2">
              <div class="row equal-height px-3" >
                <div class="col-3 p-1 " >
                  <div class="card py-0 pt-2 px-2 main_kassa_poluchit balance" >
                    <span style="font-size: 13.5px;">Balance UZS</span>
                    <span class="text-right" style="font-size: 19px;">{{client_info.sum.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}</span>
                  </div>
                  
                </div>
                
                <div class="col-3 p-1 ">
                  <div class="card py-0 pt-2 px-2 main_kassa_bg balance">
                    <span style="font-size: 13.5px;">Balance USD</span>
                    <span class="text-right" style="font-size: 19px;">{{client_info.dollor.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}💲</span>
                  </div>
                  
                </div>

                <div class="col-3 p-1 " v-for="(item, index) in clientProducts" :key="index" v-show="item.qty>0">
                  <div class="card py-0 pt-2 px-2 main_kassa_bg balance" :style="{'background': item.product.shitrix_code}">
                    <span style="font-size: 13.5px;">{{item.product.name}}</span>
                    <span class="text-right" style="font-size: 19px;">{{item.qty.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}} {{ item.product.unitMeasurment.name }}</span>
                  </div>
                  
                </div>
              </div>
            </div>
          </div>
          

          <!-- Client oxirgi 30 kundagi to'lov diagrammasi -->
          <Client30DaysChart :clientId="clientId" />

        </div>
        <div class="col-3">
          <div  class="p-2 card">
            <div>
              <span class="header_name ml-2" style="font-size: 14px;">Последние 30 дней</span>
            </div>
            <TopProductsPie :products="topProducts"/>
          </div>

          <div class="mt-3">
            <div class="check_item card_n bg-white mb-3 p-2" >
              <div class="d-flex justify-content-center">
                <small class="summ_title" style="font-size: 13px;">
                  Статистика активности клиента
                </small>
              </div>
              <!-- <div class=" p-1 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  Oбщий тулов суммаси
                </small>
                <small class="summ_title" style="color: #5EB0A0; font-weight: bold;">
                  + {{item.summ.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                </small>
              </div> -->
              <div v-if="order_all_info.totalOrders" class=" p-1 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  Кол. заказов
                </small>
                <small class="summ_title">
                  {{order_all_info.totalOrders.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                </small>
              </div>
              <div v-if="order_all_info.totalProducts" class=" p-1 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  Объем
                </small>
                <small class="summ_title">
                  {{order_all_info.totalProducts.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}} кг
                </small>
              </div>

              <div v-if="order_all_info.totalAmountPaid" class=" p-1 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  Всего оплачено
                </small>
                <small class="summ_title">
                  {{order_all_info.totalAmountPaid.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}} UZS
                </small>
              </div>
              <div  class=" p-1 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  Последний заказ
                </small>
                <small class="summ_title"  style="color: #5EB0A0; font-weight: bold;" v-if="order_all_info.daysSinceLastOrder >0">
                  {{order_all_info.daysSinceLastOrder.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}} кун олдин
                </small>
                <small class="summ_title"  style="color: green; font-weight: bold;" v-else>
                  Бугун
                </small>
              </div>

            </div>
          </div>
        </div>
      </div>
    </div>
    <Toast ref="message"></Toast>

    <massage_box :hide="modal_status" :detail_info="modal_info"
    :m_text="$t('Failed_to_add')" @to_hide_modal="modal_status= false"/>
    <Alert ref="alert"></Alert>
  </div>
</template>

<script>
import Client30DaysChart from "./Client30DaysChart.vue"
import TopProductsPie from "../sklad/topProductPia.vue"
export default {
  components:{Client30DaysChart, TopProductsPie},
  data(){
    return{
      loading: false,
      modal_info: '',
      modal_status: false,
      order_data: {},
      car_order_list: [],
      clientProducts: [],
      check_list: [],
      client_info: {},
      topProducts: [],
      clientId: null,
      order_all_info: {},
    }
  },
  methods:{
    async fetchMounted(client_id){
      this.clientId = client_id;
      this.clientProducts = [];
      this.topProducts = [];
      this.client_info = {};
      this.order_all_info = {};
      console.log(client_id)
      this.loading = true;

      await this.fetchUserInfo(client_id);
      await this.getClientProducts(client_id);
      await this.getLast30DaysProducts(client_id);
      await this.getAllOrderInfo(client_id);
      this.loading = false;

    },
    async fetchUserInfo(client_id){
      try{
          const response = await fetch(this.$store.state.hostname + "/TegirmonOrderClient/" + client_id);
          if(response.status == 201 || response.status == 200)
          {
            const data = await response.json();
            this.client_info = data;
            console.log(data)
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
          this.client_list = [];
          this.modal_info = this.$i18n.t('network_ne_connect');
          this.modal_status = true;
        }
    },
    
    async getClientProducts(client_id) {
      try {
        // 🔹 Backend API manzili
        const response = await fetch(
          this.$store.state.hostname + '/TegirmonOrderClientProductProduct/getClientProducts?client_id=' + client_id,
          {
            method: 'GET',
            headers: {
              'Content-Type': 'application/json',
            },
          }
        );

        if (!response.ok) {
          const errText = await response.text();
          this.clientProducts = [];
          throw new Error(errText || 'Server xatosi');
        }

        // 🔹 Backenddan kelgan JSON javobni o‘qish
        const data = await response.json();

        // 🔹 Ma’lumotni tekshirish
        if (Array.isArray(data) && data.length > 0) {
          this.clientProducts = data; // masalan, jadvalda ko‘rsatish uchun
          // this.$refs.message.success('Mahsulotlar yuklandi');
        } else {
          this.clientProducts = [];
          // this.$refs.message.warning('Bu client uchun mahsulot topilmadi');
        }
      } catch (err) {
        console.error('Xatolik:', err);
        this.clientProducts = [];
        // this.$refs.message.error('Server bilan bog‘lanishda muammo: ' + err.message);
      }
    },
    async getLast30DaysProducts(id){
      try{
        this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/TegirmonOrder/last30days-products/" + id);
        const data = await response.json();
        this.loading = false;
        if(response.status == 201 || response.status == 200)
        {
          console.log(data)
          this.topProducts = data;
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
        this.loading = false;
        this.modal_info = this.$i18n.t('network_ne_connect');
        this.modal_status = true;
      }
    },

    async getAllOrderInfo(id){
      try{
        this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/TegirmonOrder/client-stats/" + id);
        const data = await response.json();
        this.loading = false;
        if(response.status == 201 || response.status == 200)
        {
          console.log(data)
          this.order_all_info = data;
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
        this.loading = false;
        this.modal_info = this.$i18n.t('network_ne_connect');
        this.modal_status = true;
      }
    },
    
  }
}
</script>

<style lang="scss" scoped>
.client_info_statistic{
  background: #fafafa;
}
.client_info{
  border-radius: 5px;
  background: white;
  padding: 7px;
  // box-shadow: rgba(60, 64, 67, 0.3) 0px 1px 2px 0px, rgba(60, 64, 67, 0.15) 0px 1px 3px 1px;
}
.user_photo{
  background-color: #fff;
  border-radius: 50%;
  width: 50px;
  height: 50px;
  overflow: hidden;
}
.client_details{
  p{
    font-size: 14px;
    color: #585555;
    font-weight: bold;
    margin: 0 0 3px 0px;
  }
  span{
    font-size: 13px;
    color: #636262;
    margin: 0;
    display: block;
    margin: 0 0 3px 0px;

  }
}
.activStatus{
  position: absolute;
  top: 2px;
  right: 10px;
}

.client_balance{
  width: 12.5%;
  margin: 5px 0px;
}

.equal-height {
    display: flex;
    align-items: stretch;
}
.balance_btn2{
  position: absolute;
  bottom: -50px;
  left: 0;
  background: rgba(32, 32, 32, 0.7);
  height: 30px;
  padding-top: 10px;
  border-top-left-radius: 5px;
  border-bottom-left-radius: 5px;
  border-bottom-right-radius: 5px;
  z-index: 1111;
  box-shadow: rgba(0, 0, 0, 0.25) 0px 54px 55px,s
              rgba(0, 0, 0, 0.12) 0px -12px 30px,
              rgba(0, 0, 0, 0.12) 0px 4px 6px,
              rgba(0, 0, 0, 0.17) 0px 12px 13px,
              rgba(0, 0, 0, 0.09) 0px -3px 5px;

  opacity: 0;
  max-height: 0;
  transform: translateY(15px);
  pointer-events: none;
  transition: all 0.3s ease;
}


.balance_btn2 > * {
  position: relative;
  z-index: 2;
}

.balance:hover {
  cursor: pointer;
}

.balance:hover .balance_btn2{
  opacity: 1;
  max-height: 100px; /* yetarli bir qiymat */
  transform: translateY(0);
  pointer-events: auto;
}
.bg-active_link{
  background-image: radial-gradient( circle 502px at 2.7% 23.7%,  rgb(172, 73, 253) 0%, rgba(33,250,214,1) 99.6% ) !important;
  color: white !important;
}
.main_kassa_bg{
  background-image: linear-gradient( 90deg,  rgba(255,157,129,1) 24.3%, rgb(255, 110, 78) 78.3% );
}
.main_kassa_poluchit{
  background-image: linear-gradient( 90deg,  rgb(117, 207, 165) 24.3%, rgb(93, 236, 177) 78.3% );
}
.card_n{
  box-shadow: rgba(9, 30, 66, 0.25) 0px 4px 8px -2px, rgba(9, 30, 66, 0.08) 0px 0px 0px 1px;
  border-radius: 5px;
}
</style>