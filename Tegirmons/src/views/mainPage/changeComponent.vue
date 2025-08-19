<template>
  <div class="changeComponent p-2 pt-2">
    <div class="row py-2 px-2 border-bottom bg-white shadow border-bottom" :class="{'bg-danger': product_qty_change<-1}" style="position: sticky; top:0; z-index: 99999;">
      <div class="col-4">
        <mdb-input class="m-0 p-0 bg_warning" v-model="product_name" disabled size="md"  outline  group type="text" validate error="wrong" success="right"/>
        <small style="position:absolute; top:-10px; left:22px; font-size: 11px;" class="bg-white px-2 py-0">{{$t('product')}}</small>
      </div>
      <div class="col-3">
        <mdb-input class="m-0 p-0 bg_warning" v-model="product_qty_change" disabled size="md"  outline  group type="text" validate error="wrong" success="right"/>
        <small style="position:absolute; top:-10px; left:22px; font-size: 11px;" class="bg-white px-2 py-0">{{$t('kg_ves')}}</small>
      </div>
      <div class="col-3" v-show="!skidka_status">
        <mdb-input class="m-0 p-0 " v-model="skidka" @input="skidkaChange" @blur="skidkaOut"  size="md"  outline  group type="text" validate error="wrong" success="right"/>
        <small style="position:absolute; top:-10px; left:22px; font-size: 11px;" class="bg-white px-2 py-0">{{$t('skidka')}}</small>
      </div>
    </div>
    <div class="row px-2">
      <div class="col-3 mt-4 px-3" v-for="(item,index) in changeProduct" :key="index">
        <div class=" card pt-2 pr-3" style="position:relative;" :class="{'bg_red_color': item.auth_user_updator_id == 1}">
          <div class="product_name_price text-right">
            <div class="d-flex justify-content-between">
              <div style="height:30px; overflow: hidden;">
                <h6 class="pro_name_color text-left ml-3">{{item.product_name}}</h6>
              </div>
              <h6 class="pro_name_color text-left ml-3">{{item.ostatka_qty.toFixed(1)}}</h6>
            </div>
            <h4 class="mt-2">{{item.qty}} <span>{{item.measure}}</span></h4>
          </div>
          <div class="clickItem border-top mt-2 mx-2">
            <div class="mt-2">
              <div style="position: relative;">
                <mdb-input class="m-0 p-0" v-model="item.real_qty" size="md" @input="changeRealQty(index)" @blur="blurchangeRealQty(index)" @click="enterchangeRealQty(index)"  outline  group type="text" validate error="wrong" success="right"/>
                <small style="position:absolute; top:-7px; left:10px; font-size: 11px;" class="bg-white px-2 py-0">{{$t('kg_ves')}}</small>
              </div>
            </div>
          </div>
        </div>
      </div> 
      <div class="col-3  mt-4 px-3">
        <div class=" card pt-2 pr-3" style="position:relative;">
          <div class="product_name_price text-right mb-2">
            <h6 class="pro_name_color text-left ml-3">{{product_name_buy}}</h6>
            <div class="mx-2" style="position: relative;">
              <small style="position:absolute; top:-15px; right:5px; font-size: 12px;" class="bg-white px-2 py-0">сум</small>
              <input class="m-0 p-0 px-3 form-control" v-model="summa_buy_string" size="md" @keyup="changeMoney($event.target.value)" @click="enterMoney"  @blur="blurMoney"   outline  group type="text" validate error="wrong" success="right"/>
              <!-- <h4 class="mt-2">{{summ_buy.toFixed()}} <small>сум</small></h4> -->
            </div>
          </div>
          <div class="clickItem border-top mx-2">
            <div class="mt-2">
              <div style="position: relative;">
                <mdb-input class="m-0 p-0" v-model="product_buy" size="md" @input="changeSumma()" @blur="blurchangeSumma" @click="enterchangeSumma"  outline  group type="text" validate error="wrong" success="right"/>
                <small style="position:absolute; top:-7px; left:10px; font-size: 11px;" class="bg-white px-2 py-0">{{$t('kg_ves')}}</small>
              </div>
              <div style="position: relative;">
                <mdb-input class="m-0 p-0" v-model="product_price_buy" size="md" @input="changeSumma()"  outline  group type="text" validate error="wrong" success="right"/>
                <small style="position:absolute; top:-7px; left:10px; font-size: 11px;" class="bg-white px-2 py-0">{{$t('price')}}</small>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="text-right border-top mt-3">
      <mdb-btn   @click="commitOut()" :disabled="product_qty_change<-1" color="success" m="r2 t2" style="font-size: 10.5px"
        p="r4 l4 t2 b2"> <mdb-icon fas class="mr-2"  icon="plus"></mdb-icon>  {{$t('changing')}}
      </mdb-btn>
    </div>
  </div>
</template>

<script>
import {mdbInput, mdbBtn, mdbIcon } from 'mdbvue'
export default {
  data() {
    return {
      changeProduct: [],
      product_qty_change: 0,
      product_real_qty: 0,


      product_name_buy: '',
      product_buy: 0,
      summ_buy: 0,
      summa_buy_string: '0',
      product_id_buy: null,
      product_price_buy: 0,
      product_real_qty_buy: 0,
      product_real_qty_skidka: 0,
      skidka: 0,
      zaxira_qty: 0,
    }
  },
  components:{
    mdbInput, mdbBtn,mdbIcon
  },
  props: {
    product_id:{
      type: Number,
      default: null,
    },
    product_qty: {
      type: String,
      default: ''
    },
    product_name: {
      type: String,
      default: ''
    },
    row:{
      type: Number,
      default: 0
    },
    product_price:{
      type: Number,
      default: 0
    },
    skidka_status:{
      type: Boolean,
      default: false
    }
    

  },
  watch:{
    product_qty(){
      if(this.product_id != null){
        this.getProductId(this.product_id,this.product_qty-this.zaxira_qty)
      }
    },
    product_name(){
      if(this.product_id != null){
        this.getProductId(this.product_id,this.product_qty-this.zaxira_qty)
      }
    },
  },
  mounted() {
    // this.getProductId(this.product_id,this.product_qty)
  },
  methods: {
    async skidkaChange(){
      // console.log('this.skidka fsdf werwer wer wer')
      // console.log(this.skidka)
      let tempSkidka = this.product_real_qty_skidka;
      // console.log(tempSkidka)

      // this.product_qty_change = this.product_real_qty_skidka;
      if(this.skidka == null || this.skidka == ''){ 
        // this.product_qty_change = this.product_qty_change - 0;
        this.product_real_qty =  tempSkidka - 0
        this.product_real_qty_buy = tempSkidka - 0
      }
      else{
        // this.product_qty_change = this.product_qty_change - this.skidka;
        this.product_real_qty =  tempSkidka - this.skidka;
        this.product_real_qty_buy = tempSkidka - this.skidka;
      }
      await this.changeRealQty(0);
      await this.changeSumma();
    },
    skidkaOut(){
      if(this.skidka == null || this.skidka == ''){
        this.skidka = 0;
      }
    },
    async updateChanger(list){
      // console.log('list')
      // console.log(list)
      if(list.zaxira_qty == '' || list.zaxira_qty == null){
        this.zaxira_qty = 0
      }
      else{
        this.zaxira_qty = parseFloat(list.zaxira_qty);
      }
      await this.getProductId(list.product_id,list.qty-this.zaxira_qty, list)
      // console.log('list')
      // console.log(list)
      if(list.skidka == '' || list.skidka == null){
        this.skidka = 0;
      }
      else{
        // console.log('kirayabdi skidkaga nimaga ishlamayabdi')
        this.skidka = parseFloat(list.skidka)
            await this.skidkaChange();
        
      }
      
    },
    async getProductId(id, qty){
      this.product_buy = 0;
      this.summ_buy = 0;
      this.summa_buy_string = '0';
      this.product_name_buy = this.product_name;
      this.product_id_buy = id;
      this.product_price_buy = this.product_price;
      try{
        this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/TegirmonProductToProductPersentage/getPaginationByProductId?page=0&size=100&product_id=" + id + "&sklad_id=1");
        const data = await response.json();
        // console.log('data percented')
        // console.log(data)
        this.loading = false;
        if(response.status == 201 || response.status == 200)
        {
          if(data.items_list.length>0){
            this.showObyom = true;
            this.changeProduct = [];
            // console.log('data.items_list[0].item_list')
            // console.log(data.items_list[0].item_list)
            for(let i=0; i<data.items_list[0].item_list.length; i++){
              let ostatk = 0;
              if(data.items_list[0].item_list[i].skladOstatka.length >0){
                ostatk = data.items_list[0].item_list[i].skladOstatka[0].real_qty;
              }
              else{
                ostatk = 0;
              }
              let temp = {
                product_name: data.items_list[0].item_list[i].product_name,
                product_id: data.items_list[0].item_list[i].TegirmonProductid,
                qty: (data.items_list[0].item_list[i].persantage*qty).toFixed(1),
                persantage: data.items_list[0].item_list[i].persantage,
                measure: data.items_list[0].item_list[i].sub_product.unitMeasurment.name,
                color: data.items_list[0].item_list[i].sub_product.shitrix_code,
                real_qty:0,
                ostatka_qty: ostatk,
                auth_user_updator_id: data.items_list[0].item_list[i].auth_user_updator_id,
                id: 0,
              }
              if(ostatk>0){
                this.changeProduct.push(temp)
              } 
              // console.log(temp)
            }
            this.product_qty_change = qty;
            this.product_real_qty = qty;
            this.product_real_qty_buy = qty;
            this.product_real_qty_skidka = qty;

            // console.log(data);
            await this.skidkaChange();
          }
          else{
            this.showObyom = false;
            this.changeProduct = [];
            this.product_qty_change = qty;
            this.product_real_qty = qty;
            this.product_real_qty_buy = qty;
            this.product_real_qty_skidka = qty;
            // this.product_name = name;
          }
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
    changeSumma(){
      let test_buy_qty = this.product_real_qty_buy;
      if(this.product_buy == null || this.product_buy == ''){
        this.product_real_qty = test_buy_qty - 0;
        this.summ_buy = this.product_price_buy * 0;
        this.summa_buy_string = this.summ_buy.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')
      }
      else{
        this.product_real_qty = test_buy_qty - parseFloat(this.product_buy);
        this.summ_buy = (this.product_price_buy * this.product_buy).toFixed(0);
        if(this.summ_buy.toString().length <3){
          console.log('hali 1000 ga yetib bormadi')
        }
        else if(this.summ_buy.toString().length == 3){
          this.summ_buy = 1000
        }
        else{
          if(this.summ_buy.toString().slice(this.summ_buy.toString().length-3, this.summ_buy.toString().length) != '000'){
            // console.log('elseda chiqayabdi')
            // console.log(this.summ_buy.toString())
            // console.log(parseFloat((parseFloat(this.summ_buy.toString().slice(0, this.summ_buy.toString().length-3)) + 1).toString() + '000'))
            this.summ_buy = parseFloat((parseFloat(this.summ_buy.toString().slice(0, this.summ_buy.toString().length-3)) + 1).toString() + '000')
          }
          
        }
        this.summa_buy_string = this.summ_buy.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')
      }
      this.changeRealQty(0);
    },
    blurchangeSumma(){
      if(this.product_buy == null || this.product_buy == ''){
        this.product_buy = 0;
      }
    },
    enterchangeSumma(){
      if(this.product_buy == 0){
        this.product_buy = null;
      }
    },
    changeMoney(n){
      console.log(n)
      var tols = ''
      for(let i=0; i<n.length; i++){
        if(n[i] != ' '){
          tols += n[i];
        }
       }
       this.summa_buy_string = tols.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
       var temp = ''
       for(let i=0; i<this.summa_buy_string.length; i++){
        if(this.summa_buy_string[i] != ' '){
          temp += this.summa_buy_string[i];
        }
       }
      this.summ_buy = parseFloat(temp);
      if(this.summa_buy_string == '' || this.summa_buy_string == '0'){
        this.product_buy = 0;
      }
      else{
        this.product_buy = (parseInt(this.summ_buy)/this.product_price_buy).toFixed(2);
      } 
      let test_buy_qty = this.product_real_qty_buy;
      // console.log(this.product_buy)
      if(this.product_buy == null || this.product_buy == ''){
        this.product_real_qty = test_buy_qty - 0;
      }
      else{
        this.product_real_qty = test_buy_qty - parseFloat(this.product_buy);
      }
      this.changeRealQty(0);
      // console.log(test_buy_qty)

    },
    enterMoney(){
      if(this.summa_buy_string == '0'){
        this.summ_buy = null;
        this.summa_buy_string = '';
      }
    },
    blurMoney(){
      if(this.summa_buy_string == '' || this.summa_buy_string == null){
        this.summ_buy = 0;
        this.summa_buy_string = '0';
      }
    },
    blurchangeRealQty(i){
      // console.log(this.changeProduct[i])
      if(this.changeProduct[i].real_qty == '' || this.changeProduct[i].real_qty == null){
        this.changeProduct[i].real_qty = 0;
      }
    },
    enterchangeRealQty(i){
      if(this.changeProduct[i].real_qty == 0){
        this.changeProduct[i].real_qty = null;
      }
    },
    changeRealQty(index){
      console.log(index)
      // console.log(this.changeProduct[index])
      let test_qty = this.product_real_qty;
      for(let i=0; i<this.changeProduct.length; i++){
        if(this.changeProduct[i].real_qty != ''){
          // console.log('this.changeProduct[index]')
          test_qty = parseFloat(test_qty) - (parseFloat(this.changeProduct[i].real_qty) / parseFloat(this.changeProduct[i].persantage))
        }
        else{ 
          // console.log('pustoy')
          test_qty = parseFloat(test_qty) - (0 / parseFloat(this.changeProduct[i].persantage))
        }
      }
      this.product_qty_change = test_qty.toFixed(2);
      // console.log(this.product_qty_change);
      this.fetchChangeSubPro(this.product_qty_change);
    },
    fetchChangeSubPro(qty){
      // console.log('qty')
      // console.log(qty)
      let tempArray = []
      for(let i=0; i<this.changeProduct.length; i++){
        let temp = {
          product_name: this.changeProduct[i].product_name,
          product_id: this.changeProduct[i].product_id,
          qty: (this.changeProduct[i].persantage*qty).toFixed(1),
          persantage: this.changeProduct[i].persantage,
          ostatka_qty: this.changeProduct[i].ostatka_qty,
          auth_user_updator_id: this.changeProduct[i].auth_user_updator_id,
          real_qty:0,
          skidka: 0,
          summ:0,
          color: this.changeProduct[i].color,
          id:this.changeProduct[i].id,
        }
        if(this.changeProduct[i].real_qty == '' && this.changeProduct[i].real_qty != 0){
          temp.real_qty = null;
        }
        else{
          temp.real_qty = this.changeProduct[i].real_qty;
        }
        tempArray.push(temp)
      }
      

      this.changeProduct = tempArray;
      // console.log('this.changeProduct')
      // console.log(this.changeProduct)
    },
    commitOut(){
      if(this.product_buy>0){
        let tempInvoice = {
          product_name: this.product_name_buy,
          product_id: this.product_id_buy,
          ostatka_qty: 0,
          qty: 0,
          persantage: 0,
          skidka: 0,
          color: '#ffffff',
          real_qty: this.product_buy,
          summ: this.summ_buy,
        }
        this.changeProduct.push(tempInvoice)
      }
      if(this.skidka>0){
        let tempSkidkaInV = {
          product_name: this.product_name_buy,
          product_id: this.product_id_buy,
          ostatka_qty: 0,
          qty: 0,
          persantage: 0,
          skidka: this.skidka,
          real_qty: 0,
          color: '#ffffff',
          summ: 0,
        }
        this.changeProduct.push(tempSkidkaInV)
      }
      // console.log('ichidagi narsa')
      // console.log(this.changeProduct)
      this.$emit('addNewData', {index:this.row, data: this.changeProduct})
      this.skidka = 0;
    }
  },
}
</script>

<style lang="scss" scoped>
.product_name_price{
  height: 55px;
}
.pro_name_color{
  color: #757575;
}
.bg_warning{
  background: #e0e0ff;
  
}
.bg_red_color{
  background: #ff937d !important;
}
</style>