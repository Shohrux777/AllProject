<template>
  <div class="check_info p-2">
    <div class="header">
        <div class="d-flex justify-content-start align-items-center mb-2 mt-1">
            <div class="d-flex align-items-center" style="gap: 15px;">
                <div class="date-input-wrapper">
                    <!-- <label class="date-label mb-0">Boshlang'ich sana</label> -->
                    <input
                        type="date"
                        v-model="start_date"
                        @change="onDateChange"
                        class="form-control date-input mt-0"
                    />
                </div>
                <div class="date-input-wrapper">
                    <!-- <label class="date-label mb-0">Tugash sana</label> -->
                    <input
                        type="date" 
                        v-model="end_date"
                        @change="onDateChange"
                        class="form-control date-input"
                    />
                </div>
            </div>
            <div class="border-bottom  pb-1 ml-5">
              <span style="color:#8697BA; font-style: italic; font-size: 14px;" class="bg-primary text-white px-3 py-1 rounded"> {{ start_date }} ➡️ {{ end_date }} kundagi tushgan zakazlar miqdori 🚚💨
              </span>
          </div>
        </div>
        <div class="d-flex justify-content-start mt-2 mb-2">
            <div class="main_kassa_hisoblar main_kassa_bg">
                <small>
                  Общий заказ {{$t('qty')}}
                </small>
                <p class="summ_title">
                    {{total.totalQty.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} шт
                      ⮕ {{total.totalKg.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} кг
                </p>
            </div>
            <div class="main_kassa_hisoblar ml-3 main_kassa_poluchit" >
                <small>
                  ✅ Погружено
                </small>
                <p class="summ_title">
                    {{total.loadedQty.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} шт
                      ⮕ {{total.loadedKg.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} кг
                </p>
            </div>
            <div class="main_kassa_hisoblar ml-3 main_kassa_warning">
                <small>
                  ⏳  Идёт погрузка...
                </small>
                <p class="summ_title">
                    {{total.loadingQty.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} шт
                      ⮕ {{total.loadingKg.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} кг
                </p>
            </div>
            <div class="main_kassa_hisoblar ml-3 main_kassa_balance">
                <small>
                   Осталось погрузить
                </small>
                <p class="summ_title">
                    {{total.remainingQty.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} шт
                      ⮕ {{total.remainingKg.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} кг
                </p>
            </div>
        </div>
        <div class="d-flex justify-content-start mt-2 mb-2">
            <div class="main_kassa_hisoblar main_kassa_bg " :class="{'ml-3': index != 0}" 
              v-for="(item , index) in order_category_list" :key="index" :style="`background: ${item.color} !important`"
              v-show="item.auth_user_creator_id>0 || index == 0">
                <small>
                  {{item.name}}
                </small>
                <p class="summ_titled">
                    {{item.auth_user_creator_id.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} шт
                      ⮕ {{item.auth_user_updator_id.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} кг
                </p>
            </div>
        </div>
    </div>
    <div class="check_list mt-1">
        <table class="w-100  myTableuserSalaryList rounded my-2 px-2">
            <thead class="bg-gray-200 header py-3 info_client_header ">
                <tr class="bg-gray-100 info_client_header1">
                    <th width="300" class=" text-left">{{ $t('product') }}</th>
                    <th class=" text-left">Общий {{$t('qty')}}</th>

                    <th class=" text-left">Погружено ✅</th>

                    <th class=" text-left">Идёт погрузка... ⏳</th>

                    <th class=" text-left">Осталось погрузить</th>
                    <th width="120"  class=" text-left">{{$t('category')}}</th>

                </tr>
            </thead>
            <tbody>
              <tr v-for="item in order_load_list" :key="item.id" :style="`background: ${item.product.shitrix_code}`">
                  <td class="p-2" ><span v-if="item.productId">{{ item.productName }}</span></td>
                  <td class="p-2"><span>{{ item.totalQty.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} {{ item.unit }}</span>
                    <span>
                      ➡️ {{ (item.totalQty * item.product.real_qty).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} кг
                    </span>
                  </td>
                  
                  <td class="p-2"><span>{{ item.loadedQty.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} {{ item.unit }}</span>
                    ➡️ {{ (item.loadedQty * item.product.real_qty).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} кг</td>
                  
                  <td class="p-2"><span>{{ item.loadingQty.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} {{ item.unit }}</span>
                    ➡️ {{ (item.loadingQty * item.product.real_qty).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} кг</td>

                  <td class="p-2"><span>{{ item.remainingQty.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} {{ item.unit }}</span>
                    ➡️ {{ (item.remainingQty * item.product.real_qty).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} кг</td>
                  <td class="p-2" ><span style="font-size: 10px; ">{{ item.product.print_name }}</span></td>
              </tr>
            </tbody>
        </table>
    </div>
  </div>
</template>

<script>
import {mapActions, mapGetters} from 'vuex'

export default {
  data(){
    return{
      start_date: '',
      end_date: '',
      total: {
        "totalQty": 0,
        "totalKg": 0,
        "loadedQty": 0,
        "loadedKg": 0,
        "loadingQty": 0,
        "loadingKg": 0,
        "remainingQty": 0,
        "remainingKg": 0
      },
      order_load_list: [],
      order_category_list: [],
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
      
      this.clw_cw()
      await this.fetch_get_order_items_summary();
      await this.fetch_get_order_items_summary_total();
      await this.fetchcategory();
      console.log(this.allcategory);
      this.order_category_list = this.allcategory.rows;
      for(let i=0; i<this.order_category_list.length; i++){
        let qty = 0;
        let qty_kg = 0;
        for(let j=0; j< this.order_load_list.length; j++){
          if(this.order_category_list[i].id == this.order_load_list[j].product.bot_id){
            qty += this.order_load_list[j].loadedQty;
            qty_kg += (this.order_load_list[j].loadedQty * this.order_load_list[j].product.real_qty);
          }
        }
        this.order_category_list[i].auth_user_creator_id = qty;
        this.order_category_list[i].auth_user_updator_id = qty_kg;
      }
      console.log(this.order_category_list)

    },
    async onDateChange(){
      await this.fetch_get_order_items_summary();
      await this.fetch_get_order_items_summary_total();
      // Category listni ham yangilash
      for(let i=0; i<this.order_category_list.length; i++){
        let qty = 0;
        let qty_kg = 0;
        for(let j=0; j< this.order_load_list.length; j++){
          if(this.order_category_list[i].id == this.order_load_list[j].product.bot_id){
            qty += this.order_load_list[j].loadedQty;
            qty_kg += (this.order_load_list[j].loadedQty * this.order_load_list[j].product.real_qty);
          }
        }
        this.order_category_list[i].auth_user_creator_id = qty;
        this.order_category_list[i].auth_user_updator_id = qty_kg;
      }
    },
    
    async fetch_get_order_items_summary(){
      // Agar date inputlar bo'sh bo'lsa, choosen_day dan foydalanamiz
      let startDate = this.start_date || this.choosen_day;
      let endDate = this.end_date || this.choosen_day;
      
      let b_data = startDate + 'T00:00:00.000Z';
      let e_data = endDate + 'T00:00:00.000Z';
      this.order_load_list = []

      try{
        const res = await fetch(this.$store.state.hostname + '/TegirmonOrder/order-items-summary?begin_date=' +  b_data + '&end_date=' + e_data);
        const res_data = await res.json();
        console.log('res_data order check hammasi online uchun data')
        console.log(res_data)
          if(res.status == 200 || res.status == 201){
            this.order_load_list = res_data;
          }
      }
      catch{
        console.log('2')
        this.$refs.alert.error("Serverda uzilish bor. Qayta urinib ko'ring !");
        return false;
      }

    },
    async fetch_get_order_items_summary_total(){
      // Agar date inputlar bo'sh bo'lsa, choosen_day dan foydalanamiz
      let startDate = this.start_date || this.choosen_day;
      let endDate = this.end_date || this.choosen_day;
      
      let b_data = startDate + 'T00:00:00.000Z';
      let e_data = endDate + 'T00:00:00.000Z';

      try{
        const res = await fetch(this.$store.state.hostname + '/TegirmonOrder/order-items-summary-total?begin_date=' +  b_data + '&end_date=' + e_data);
        const res_data = await res.json();
        console.log('res_data order check hammasi online uchun data')
        console.log(res_data)
          if(res.status == 200 || res.status == 201){
            this.total = res_data;
          }
      }
      catch{
        console.log('2')
        this.$refs.alert.error("Serverda uzilish bor. Qayta urinib ko'ring !");
        return false;
      }

    },

    clw_cw(){
      this.total = 
        {
        "totalQty": 0,
        "totalKg": 0,
        "loadedQty": 0,
        "loadedKg": 0,
        "loadingQty": 0,
        "loadingKg": 0,
        "remainingQty": 0,
        "remainingKg": 0
      };
    }
  }
}
</script>

<style lang="scss" scoped>
.main_kassa_hisoblar{
  width:25%; 
  padding: 8px 20px;
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
    background: #afd1fd;
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
</style>