<template>
  <div class="check_info p-2">
    <div class="header">
        <div class="border-bottom text-center pb-1">
            <span style="color:#8697BA; font-style: italic; font-size: 14px;" class="bg-primary text-white px-3 py-1 rounded">{{ choosen_day }}  kundagi optom savdo uchun to'lovlar
            </span>
        </div>
        <div class="d-flex justify-content-start mt-2 mb-2">
            <div class="main_kassa_hisoblar main_kassa_bg">
                <small>
                    Oбщий савдо суммаси 
                </small>
                <p class="summ_title">
                    {{ order_summ.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}
                </p>
            </div>
            <div class="main_kassa_hisoblar ml-3 main_kassa_bg">
                <small>
                    Наличные 
                </small>
                <p class="summ_title">
                    {{ order_cash_str.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}
                </p>
            </div>
            <div class="main_kassa_hisoblar ml-3 main_kassa_bg">
                <small>
                  Доллор
                </small>
                <p class="summ_title">
                    {{ order_dollor_str.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} 💲
                </p>
            </div>
            <div class="main_kassa_hisoblar ml-3 main_kassa_bg">
                <small>
                    Другой сумма
                </small>
                <p class="summ_title">
                    {{ another_summa_order.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}
                </p>
            </div>
        </div>
        <div class="d-flex justify-content-start mt-2 mb-2">
            <div class="main_kassa_hisoblar ">
                <small>
                  🏦  Счета Наличные 
                </small>
                <p class="summ_title">
                    {{ order_hisob_cash.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}
                </p>
            </div>
            <div class="main_kassa_hisoblar ml-3 ">
                <small>
                  🏦  Счета Доллор 
                </small>
                <p class="summ_title">
                    {{ order_hisob_dollor.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} 💲
                </p>
            </div>
            <div class="main_kassa_hisoblar ml-3 main_kassa_balance">
                <small>
                   🔄 Баланс наличные 
                </small>
                <p class="summ_title">
                    {{ order_balance_cash.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}
                </p>
            </div>
            <div class="main_kassa_hisoblar ml-3 main_kassa_balance">
                <small>
                   🔄 Баланс доллор 
                </small>
                <p class="summ_title">
                    {{ order_balance_dollor.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} 💲
                </p>
            </div>
        </div>
        <div class="d-flex justify-content-start mt-2 mb-2 border-top pt-2">
            
            <div class="main_kassa_hisoblar  main_kassa_warning" >
                <small>
                  ⏳  Ожидаемые наличные 
                </small>
                <p class="summ_title">
                    {{ order_waiting_cash.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}
                </p>
            </div>
            <div class="main_kassa_hisoblar ml-3 main_kassa_warning">
                <small>
                  ⏳  Ожидаемые доллор 
                </small>
                <p class="summ_title">
                    {{ order_waiting_dollor.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} 💲
                </p>
            </div>
            <div class="main_kassa_hisoblar ml-3 main_kassa_poluchit" >
                <small>
                    ⬇️ Получить наличный баланс 
                </small>
                <p class="summ_title">
                    {{ prixod_cash.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}
                </p>
            </div>
            <div class="main_kassa_hisoblar ml-3 main_kassa_poluchit" >
                <small>
                  ⬇️  Получить доллор баланс
                </small>
                <p class="summ_title">
                    {{ prixod_dollor.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} 💲
                </p>
            </div>
        </div>
    </div>
    <div class="check_list mt-1">
        <table class="w-100  myTableuserSalaryList rounded my-2 px-2">
            <thead class="bg-gray-200 header py-3 info_client_header ">
                <tr class="bg-gray-100 info_client_header1">
                    <th class=" text-left">{{ $t('client') }}</th>
                    <th class=" text-left">{{$t('sum')}}</th>
                    <th class=" text-left">{{$t('cash')}}</th>
                    <th class=" text-left">{{$t('dollor')}}</th>
                    <th class=" text-left">Баланс {{$t('cash')}}</th>
                    <th class=" text-left">Баланс {{$t('dollor')}}</th>
                    <th class=" text-left">{{$t('status_')}}</th>
                    <th class=" text-left">Кассир</th>
                </tr>
            </thead>
            <tbody>
            <tr v-for="item in order_check_list" :key="item.id" :class="{'main_kassa_warning': !item.isInCashbox}">
                    <td class="p-2" ><span v-if="item.tegirmonOrderClientid">{{ item.client.fio }}</span></td>
                    <td class="p-2"><span>{{ item.summ.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}</span></td>
                    <td class="p-2">{{ item.cash.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}</td>
                    <td class="p-2">{{ item.dollor.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}</td>
                    <td class="p-2">{{ item.sum_balance.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}</td>
                    <td class="p-2">{{ item.dollor_balance.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}</td>

                    <td class="p-2">
                      <span class="hisob_bg text-white px-2 py-1 rounded" style="font-size: 10px;" v-if="item.hisob_id " >{{item.hisob_name}}</span>
                      <span class="main_kassa_balance text-white px-2 py-1 rounded" style="font-size: 10px;" v-else-if="item.sum_balance || item.dollor_balance" >Баланс</span>
                      <span class="main_kassa_bg text-white px-2 py-1 rounded" style="font-size: 10px;" v-else>Касса</span>
                    </td>
                    <td>
                      <span>{{ item.image_url }}</span>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
  </div>
</template>

<script>
export default {
  data(){
    return{
      order_cash_str : 0,
      order_dollor_str : 0,
      order_summ : 0,
      order_humo_str : 0,
      order_uz_card_str : 0,
      order_click_str : 0,
      order_skidka_str : 0,
      order_payme_str : 0,
      order_paynet_str : 0,
      order_uzumpay_str : 0,
      order_hisob_cash : 0,
      order_hisob_dollor : 0,
      order_balance_cash:0,
      order_balance_dollor: 0,
      another_summa_order: 0,
      order_waiting_cash: 0,
      order_waiting_dollor: 0,
      order_check_list: [],
      prixod_cash: 0,
      prixod_dollor: 0,
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
  methods:{
    async func_mounted(){
      this.clw_cw()
      await this.fetch_order_all_naqd();
      await this.fetch_order_all_summa_online();
      await this.fetch_getNotInCashboxTotal();
      await this.fetch_getAll_check_list();
      await this.fetch_getClientPrixod();

    },
    async fetch_order_all_naqd(){
      this.order_summ = 0;
      this.another_summa_order = 0;
      this.order_balance_cash = 0;
      this.order_balance_dollor = 0;
      let b_data = this.choosen_day + 'T00:00:35.000Z';
      let e_data = this.choosen_day + 'T23:59:59.000Z';
      try{
        const res = await fetch(this.$store.state.hostname + '/TegirmonOrderCheck/getKassaCurrentRealTegirmonOrderNotHisob?begin_date=' +  b_data + '&end_date=' + e_data);
        const res_data = await res.json();
        console.log('res_data order check hammasi online uchun data')
        console.log(res_data)
          if(res.status == 200 || res.status == 201){
            
            this.order_cash_str = res_data[0].cash
            this.order_dollor_str = res_data[0].dollor
            this.order_summ = res_data[0].summ;

            this.order_humo_str = res_data[0].humo;
            this.order_uz_card_str = res_data[0].uz_card;
            this.order_click_str = res_data[0].click;
            this.order_skidka_str = res_data[0].skidka;
            this.order_payme_str = res_data[0].payme;
            this.order_paynet_str = res_data[0].paynet;
            this.order_uzumpay_str = res_data[0].uzumpay;
            
            this.another_summa_order += res_data[0].humo + res_data[0].uz_card + res_data[0].click + 
              res_data[0].payme + res_data[0].paynet + res_data[0].uzumpay;
            
              this.order_balance_cash += res_data[0].sum_balance;
              this.order_balance_dollor += res_data[0].dollor_balance;
            
          }
          // this.qayt_naqd = res_data[0].srogi_otganlar_uchun_rasxod.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
          // this.qayt_dollor = res_data[0].salary.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
          
          // this.sell = res_data[0].for_buy_tovar_rasxod.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')
      }
      catch{
        console.log('2')
        this.$refs.alert.error("Serverda uzilish bor. Qayta urinib ko'ring !");
        return false;
      }
    },
    async fetch_order_all_summa_online(){
      let b_data = this.choosen_day + 'T00:00:35.000Z';
      let e_data = this.choosen_day + 'T23:59:59.000Z';

      try{
        const res = await fetch(this.$store.state.hostname + '/TegirmonOrderCheck/getKassaCurrentRealTegirmonOrder?begin_date=' +  b_data + '&end_date=' + e_data);
        const res_data = await res.json();
        console.log('res_data order check hammasi online uchun data')
        console.log(res_data)
          if(res.status == 200 || res.status == 201){
            this.order_hisob_cash += res_data[0].cash;
            this.order_hisob_dollor += res_data[0].dollor;
            this.order_humo_str += res_data[0].humo;
            this.order_uz_card_str += res_data[0].uz_card;
            this.order_click_str += res_data[0].click;
            this.order_skidka_str += res_data[0].skidka;
            this.order_payme_str += res_data[0].payme;
            this.order_paynet_str += res_data[0].paynet;
            this.order_uzumpay_str += res_data[0].uzumpay;

            this.order_balance_cash += res_data[0].sum_balance;
            this.order_balance_dollor += res_data[0].dollor_balance;
            let temp = 0;
            temp += res_data[0].humo + res_data[0].uz_card + res_data[0].click +
            res_data[0].payme + res_data[0].paynet + res_data[0].uzumpay

            this.another_summa_order += temp;
            this.order_summ += res_data[0].summ;

            this.another_summa += this.another_summa_order;
          }
      }
      catch{
        console.log('2')
        this.$refs.alert.error("Serverda uzilish bor. Qayta urinib ko'ring !");
        return false;
      }
    },

    async fetch_getNotInCashboxTotal(){
      let b_data = this.choosen_day + 'T00:00:35.000Z';
      let e_data = this.choosen_day + 'T23:59:59.000Z';
      this.order_waiting_cash = 0;
      this.order_waiting_dollor = 0;

      try{
        const res = await fetch(this.$store.state.hostname + '/TegirmonOrderCheck/getNotInCashboxTotal?begin_date=' +  b_data + '&end_date=' + e_data);
        const res_data = await res.json();
        console.log('res_data order check hammasi online uchun data')
        console.log(res_data)
          if(res.status == 200 || res.status == 201){
            this.order_waiting_cash = res_data.total_cash;
            this.order_waiting_dollor = res_data.total_dollor;
          }
      }
      catch{
        console.log('2')
        this.$refs.alert.error("Serverda uzilish bor. Qayta urinib ko'ring !");
        return false;
      }

    },
    async fetch_getAll_check_list(){
      let b_data = this.choosen_day + 'T00:00:35.000Z';
      let e_data = this.choosen_day + 'T23:59:59.000Z';
      this.order_check_list = []

      try{
        const res = await fetch(this.$store.state.hostname + '/TegirmonOrderCheck/getAllChecksList?begin_date=' +  b_data + '&end_date=' + e_data);
        const res_data = await res.json();
        console.log('res_data order check hammasi online uchun data')
        console.log(res_data)
          if(res.status == 200 || res.status == 201){
            this.order_check_list = res_data;
          }
      }
      catch{
        console.log('2')
        this.$refs.alert.error("Serverda uzilish bor. Qayta urinib ko'ring !");
        return false;
      }

    },
    async fetch_getClientPrixod(){
      let b_data = this.choosen_day + 'T00:00:35.000Z';
      let e_data = this.choosen_day + 'T23:59:59.000Z';
      this.prixod_cash = 0;
      this.prixod_dollor = 0;

      try{
        const res = await fetch(this.$store.state.hostname + '/TegirmonOrderClientRasxod/getPrixodSummary?begin_date=' +  b_data + '&end_date=' + e_data);
        const res_data = await res.json();
        console.log('res_data order check hammasi online uchun data')
        console.log(res_data)
          if(res.status == 200 || res.status == 201){
            this.prixod_cash = res_data.summa_total;
            this.prixod_dollor = res_data.dollor_total;
          }
      }
      catch{
        console.log('2')
        this.$refs.alert.error("Serverda uzilish bor. Qayta urinib ko'ring !");
        return false;
      }

    },

    clw_cw(){
      this.order_cash_str  =  0;
      this.order_dollor_str  =  0;
      this.order_summ  =  0;
      this.order_humo_str  =  0;
      this.order_uz_card_str  =  0;
      this.order_click_str  =  0;
      this.order_skidka_str  =  0;
      this.order_payme_str  =  0;
      this.order_paynet_str  =  0;
      this.order_uzumpay_str  =  0;
      this.order_hisob_cash  =  0;
      this.order_hisob_dollor  =  0;
      this.order_balance_cash = 0;      
      this.order_balance_dollor =  0;
      this.another_summa_order =  0;
    }
  }
}
</script>

<style lang="scss" scoped>
.main_kassa_hisoblar{
  width:25%; 
  padding: 5px 20px;
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
  background-image: linear-gradient( 90deg,  rgb(250, 99, 237) 24.3%, rgb(193, 72, 218) 78.3% );

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
</style>