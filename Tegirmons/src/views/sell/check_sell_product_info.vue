<template>
  <div class="check_info p-2">
    <div class="header">
        <div class="border-bottom text-center pb-1">
            <span style="color:#8697BA; font-style: italic; font-size: 14px;" class="bg-primary text-white px-3 py-1 rounded"> {{ choosen_day }}  kundagi ketgan tovar miqdori 🚚💨
            </span>
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

                    <th class=" text-left">Тароз список</th>
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

                  <td class="p-2"><span>{{ formatNumber(item.sellQty).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} {{ item.unit }}</span>
                    <span>
                      ➡️ {{ formatNumber(item.sellQty * item.product.real_qty).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} кг
                    </span>
                  </td>

                  <td class="p-2"><span>{{ formatNumber(item.invoiceAcceptedQty).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} {{ item.unit }}</span>
                    <span>
                      ➡️ {{ formatNumber(item.invoiceAcceptedQty * item.product.real_qty).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} кг
                    </span>
                  </td>

                  <td class="p-2"><span>{{ formatNumber(item.invoiceOnlyChangeQty).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} {{ item.unit }}</span>
                    <span>
                      ➡️ {{ formatNumber(item.invoiceOnlyChangeQty * item.product.real_qty).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} кг
                    </span>
                  </td>

                  
                  
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
      
      order_load_list: [],
      order_category_list: [],
      all_sht: 0,
      all_kg: 0,
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
    //   this.clw_cw()
      await this.fetch_get_order_items_summary();
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
    formatNumber(value) {
      if (value % 1 === 0) {
        // Butun son bo'lsa, o'zgartirmaymiz
        return value;
      } else {
        // 2 ta onlik raqamgacha kesamiz
        return Math.floor(value * 10) / 10;
      }
    },
    
    async fetch_get_order_items_summary(){
      let b_data = this.choosen_day + 'T00:00:35.000Z';
      let e_data = this.choosen_day + 'T23:59:59.000Z';
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
</style>