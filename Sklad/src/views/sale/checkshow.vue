<template>
  <div class="checkShow " style="z-index: 1000;" v-on:click.self="close"> 
    <div style="position: absolute; top:5px; right:10px;" >
      <mdb-btn color="info" style="font-size:11px; padding: 8px 25px;" @click="checkPrint" class="m-0  mt-1 w-100" > {{'print'}}</mdb-btn>
    </div>
    <div  class="ckeck_main bg-white my-2  border">
      <div  style="text-align:center; margin: 10px 0;">
        <h5>{{comp_name}}</h5>
      </div>
      <div class="infoHeader">
        <table>
          <tbody>
            <tr>
              <th  style="font-weight: bold;">Чек №</th>
              <td></td>
            </tr>
            <tr style="padding-top:10px;">
              <th  style="font-weight: bold;">Кассир</th>
              <td>{{kassir}}</td>
            </tr>
            <tr style="padding-top:10px;">
              <th style="font-weight: bold; padding-right: 35px;">Напечатан</th>
              <td>{{dateTime}}</td>
            </tr>
          </tbody>
        </table>
      </div>
      <div class="infoList">
        <table>
          <thead>
              <tr style="border-bottom: 0.5px solid #CED4DA;">
                <th  style="font-weight: bold; width: 135px;">{{$t('name')}}</th>
                <th  style="font-weight: bold;  width: 55px; text-align: center">{{$t('soni')}}</th>
                <th  style="font-weight: bold; width: 80px; text-align: right">{{$t('price')}}</th>
                <th  style="font-weight: bold; width: 100px; text-align: right">{{$t('total')}}</th>
              </tr>
          </thead>
          <tbody>
  
            <tr v-for="(item,i) in get_zakaz_product_all_list[get_page_savat]" :key="i" style="border-bottom: 0.5px solid #CED4DA;">
              <td>{{item.name}}</td>
              <td style="text-align: center">{{item.qty + ' / ' + item.qty_in_pack}}</td>
              <td style="text-align: right">{{item.real_sum}}</td>
              <td style="text-align: right">{{item.summ}}</td>
            </tr>
          </tbody>
        </table>
      </div>

      <div class="totalInfo">
        <h6 style="margin:0; padding:0 5px; font-weight:bold">{{$t('total')}}</h6>
        <div style="border: 1px dashed black; width:30%;"></div>
        <h5 style="margin:0; padding:0 5px; font-weight:bold">{{get_all_summa[get_page_savat]}} <span style="font-size:15px;">Сум</span></h5>
      </div>

      <div class="totalInfo">
        <h6 style="margin:0; padding:0 10px 0 5px; font-weight:bold">К оплате</h6>
        <div style="border: 1px dashed black; width:20%;"></div>
        <h4 style="margin:0; padding:0 5px 0 10px; font-weight:bold; " >{{get_all_summa[get_page_savat]}} <span style="font-size:16.5px;">Сум</span></h4>
      </div>

      <div style="margin-top:15px; border:1px solid black;"></div>
     
    </div>
  </div>
</template>

<script>
import {
  
  mdbBtn
} from "mdbvue";
import {mapGetters, mapMutations} from 'vuex'
export default {
  components: {
    mdbBtn
  },
  data(){
    return{
      printBtn: true,
      comp_name: localStorage.CompName,
      kassir : localStorage.fio,
      dateTime: new Date().toLocaleDateString(),
      time: new Date().toDateString,
    }
  },
  mounted(){
  },

  computed: mapGetters(['get_zakaz_product_all_list','get_page_savat', 'get_all_summa','get_page_savat']),
  methods: {
   ...mapMutations(['clear_order']),
   close(){
    this.$emit('close')
   },
   checkPrint(){
    window.print();
    this.close();
   }
  }
}
</script>

<style lang="scss">
.checkShow {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(47, 47, 47, 0.9);
  display: flex;
  justify-content: center;

  .ckeck_main{
    width: 403px;
    max-height: 100vh;
    overflow-y: scroll;

    .infoHeader{
      border-bottom: 1px dashed black;
      padding: 0 10px 10px 10px; 
    }
    .infoList{
      padding: 10px 10px 15px 10px; 
    }
    .totalInfo{
      display: flex;
      justify-content: space-between;
      align-items: center;
      padding: 10px 10px 20px 10px; 

    }
  }
}
</style>