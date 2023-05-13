<template>
    <div class="checked_show" >
      <div  class="ckeck_main_show ">
        <div  style="text-align:center; margin: 10px 0;">
          <h4 class="font-weight-bold">Накладной № {{ options.id }}</h4>
        </div>
        <div class="infoHeader">
          <table style="width:100% !important">
            <tbody >
              <tr>
                <th  style="font-weight: bold; font-size: 17px;">Накладной №</th>
                <td style="font-size: 17px; font-weight: bold;" class="text-right">{{options.id}}</td>
              </tr>
              <!-- <tr style="padding-top:10px;">
                <th  style="font-weight: bold; font-size: 17px;">Пользователь</th>
                <td style="font-size: 17px;" class="text-right">{{options.posAuthorizationid}}</td>
              </tr> -->
              <tr style="padding-top:10px;" v-if="client_name != ''">
                <th class="paymentCheckPrintCheck" style="font-weight: bold; font-size: 17px;">Клиент</th>
                <td class="paymentCheckPrintCheck text-right" style="font-weight: bold; font-size: 17px;">{{client_name}}</td>
              </tr>
              <tr style="padding-top:10px;">
                <th style="font-weight: bold; font-size: 17px; padding-right: 35px;">Напечатан</th>
                <td style="font-weight: bold; font-size: 17px;" class="text-right">{{options.created_date_time_str}}</td>
              </tr>
            </tbody>
          </table>
        </div>
        <div class="infoList">
          <table style="width:100% !important">
            <thead>
                <tr style="border-bottom: 0.5px solid #CED4DA;">
                  <th  style="font-weight: bold; font-size: 17px; width: 135px;">{{$t('name')}}</th>
                  <th  style="font-weight: bold; font-size: 17px;  width: 55px; text-align: center">{{$t('soni')}}</th>
                  <th  style="font-weight: bold; font-size: 17px; width: 80px; text-align: right">{{$t('price')}}</th>
                  <th  style="font-weight: bold; font-size: 17px; width: 100px; text-align: right">{{$t('total')}}</th>
                </tr>
            </thead>
            <tbody>
    
              <tr v-for="(item,i) in options.paymentsList" :key="i" style="border-bottom: 0.5px solid #CED4DA;">
                <td style="font-size: 17px; width:400px;">{{item.product_name}}</td>
                <td v-if="item.product.contains_number_in_pack>1" style="font-size: 17px; text-align: center"><span>{{parseInt(parseInt(item.qty_in_pack)/parseInt(item.product.contains_number_in_pack)) }}</span> <span> / </span> <span>{{ parseInt(item.qty_in_pack) - parseInt(parseInt(item.qty_in_pack)/parseInt(item.product.contains_number_in_pack))*item.product.contains_number_in_pack}} </span></td>
                <td v-else style="font-size: 17px; text-align: center">{{item.qty}}</td>
                <td style="font-size: 17px; text-align: right">{{item.real_sum}}</td>
                <td style="font-size: 17px; text-align: right">{{item.summ}}</td>
              </tr>
            </tbody>
          </table>
        </div>
  
        <div class="totalInfoShowCheck d-flex justify-content-between" style="border-top: 1px dashed black;">
          <h6 style="margin:0; padding:0 2px; font-weight:bold; font-size:17px;">{{$t('total')}}</h6>
          <h5 style="margin:0; padding:0 2px; font-weight:bold; font-size:17px;">{{parseFloat(parseFloat(options.summ) + parseFloat(options.online_sum))}} <span style="font-size:13px;">Сум</span></h5>
        </div>
        <!-- <div class="totalInfoShowCheck d-flex justify-content-between" style="border-top: 1px dashed black;" v-if="options.discount_summ">
          <h6 style="margin:0; padding:0 2px; font-weight:bold; font-size:17px;">{{$t('skidka')}}</h6>
          <h5 style="margin:0; padding:0 2px; font-weight:bold; font-size:17px;">{{options.discount_summ}} <span style="font-size:13px;">Сум</span></h5>
        </div> -->
        <div class="totalInfoShowCheck d-flex justify-content-between" style="border-top: 1px dashed black;" v-if="options.online_sum">
          <h6 style="margin:0; padding:0 2px; font-weight:bold; font-size:17px;">{{$t('creditd_summ')}}</h6>
          <h5 style="margin:0; padding:0 2px; font-weight:bold; font-size:17px;">{{options.online_sum}} <span style="font-size:13px;">Сум</span></h5>
        </div>
  
        <div class="totalInfoShowCheck d-flex justify-content-between" style="border-top: 1px dashed black;">
          <h6 style="margin:0; padding:0 2px; font-weight:bold; font-size:17px;">К оплате</h6>
          <h4 style="margin:0; padding:0 2px; font-weight:bold; font-size:17px;" >{{parseFloat(options.summ)}} <span style="font-size:13.5px;">Сум</span></h4>
        </div>
  
        <div style="margin-top:15px; border:1px solid black;"></div>
       
      </div>
    </div>
  </template>
  
  <script>
  import {mapGetters, mapMutations} from 'vuex'
  export default {
    data(){
      return{
        comp_name: localStorage.CompName,
        kassir : localStorage.fio,
        dateTime: new Date().toLocaleDateString(),
        time: new Date().toDateString,
      }
    },
    props:{
        options:
      {
        type: Object,
        default(){
          return {}
        }
      },
      client_name:
      {
        type: String,
        default: '',
      },
    },
    mounted(){
      window.print();
      this.$emit('close')
    //   this.clear_order();
    },
    created() {
      this.$root.$refs.check = this;
    },
    computed: mapGetters(['get_zakaz_product_all_list','get_page_savat', 'get_all_summa','get_page_savat', 'CheckId','get_skidka_dolg']),
    methods: {
     ...mapMutations(['clear_order']),
    }
  }
  </script>
  
  <style lang="scss">
  .checked_show {
    
    width: 100%;
    height: 100%;
    background-color: #fff;
    display: flex;
    justify-content: center;
    .ckeck_main_show{
      width: 100%;
      
  
      .infoHeader{
        border-bottom: 1px dashed black;
        padding: 0 10px 10px 10px; 
      }
      .infoList{
        padding: 10px 10px 15px 10px; 
      }
      .totalInfoShowCheck{
        display: flex;
        width:100% !important;
        justify-content: space-between;
        align-items: center;
        padding: 5px 10px 5px 10px; 
  
      }
    }
  }
  </style>