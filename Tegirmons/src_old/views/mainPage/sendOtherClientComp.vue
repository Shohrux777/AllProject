<template>
  <div class="send_other_client_product container-fluid p-1">
    <div class="send_header ">
      <div class="container-fluid">
        <div class="row px-3 py-2 d-flex align-items-center main_header_bg_new rounded">
          <div class="col-3">
              <div class="">
              <erpSelect
              :options="allClient.rows"
              @select="selectOptionUser"
              :selected="user_name"
              size="sm"
              class="bg-white"
              style="margin-top:8px;"
              url="/TegirmonClient/getPaginationSearchByFioOrPassportSerailNumberOrHomeOrMobilePhoneNumber?page=0&size=100&fio_or_serial_number="
              :label="$t('select_item')"
              />
              <small style="position:absolute; top:-8px; left:10px; font-size: 11px; color: rgb(62, 62, 139);" class="font-weight-bold px-2 py-0">Поиск клиент</small>
              <small v-if="$v.user_name.$dirty && user_id == null" class="invalid-text mt-0 ml-2" >
                  {{$t('select_item')}}
              </small>
              </div>
          </div>

          <div class="col-3">
              <input-search  @select="selectClientPassport" :label="$t('passport_number')" :selected="passport_number"
              url="/TegirmonClient/getPaginationSearchByFioOrPassportSerailNumberOrHomeOrMobilePhoneNumber?page=0&size=100&fio_or_serial_number="
              ref="search_client" placeholder="AA12345678"  style="height:32px;">
              </input-search>
          </div>
  
          <div class="col-3">
              <input-search-year  @select="selectClientBorn" type="text" :label="$t('born_date')" :selected="born_date" placeholder="дд-мм-гггг"
              url="/TegirmonClient/getPaginationByBornDate?page=0&size=100&born_date_str="
              ref="search_client_born_date" style="height:32px;">
              </input-search-year>
          </div>
          <div class="col-3">
              <input-search  @select="selectClientPhone" :label="$t('note')" :selected="phone_number"
              url="/TegirmonClient/getPaginationByNote?page=0&size=100&note_str="
              ref="search_client_phone" style="height:32px;" placeholder="1234">
              </input-search>
          </div>
  
        </div>
      </div>
    </div>
    <div class="main_ostatka container-fluid mt-3">
      <div class="row align-items-center">
        <!-- Chap card -->
        <div class="col-4">
          <div class="card pt-2 pr-3">
            <h6 class="ml-3 pro_name_color" style="font-size: 15.5px;">{{ option.client.fio }}</h6>
            <div class="d-flex justify-content-between align-items-center">
              <h6 class="ml-3 mt-2 pro_name_color">{{ option.product.name }}</h6>
              <h4 class="mt-2">
                {{ option.real_qty.toFixed(1) }}
                <small>{{ option.product.unitMeasurment.name }}</small>
              </h4>
            </div>
          </div>
        </div>

        <!-- Transfer oqim -->
        <div class="col-4" v-if="ostatkaProduct_status">
          <div class="card">
            <p class="m-0 p-0 px-2 py-1" style="font-size: 14px;">Отправить товар <i class="fas fa-paper-plane"></i></p>
            <div class="p-2 py-1 w-100" style="position: relative;">
                <input class="m-0 form-control mr-2" style="height:30px; font-size: 14px;" v-model="qty_str" @keyup="funcSendSumm($event.target.value)"  type="text" validate error="wrong" success="right"/>
                <small class="bg-white" style="position: absolute; z-index:1; left:15px; top: -5px; color: #757575; font-size: 12px;">
                  Масса продукта
                </small>   
            </div>
            <div class="transfer-line"></div>
          </div>
          
        </div>

        <!-- O‘ng card -->
        <div class="col-4">
          <div class="card pt-2 pr-3" v-if="ostatkaProduct_status">
            <h6 class="ml-3 pro_name_color" style="font-size: 15.5px;">{{ ostatkaProduct.client.fio }}</h6>
            <div class="d-flex justify-content-between align-items-center">
              <h6 class="ml-3 mt-2 pro_name_color">{{ ostatkaProduct.product.name }}</h6>
              <h4 class="mt-2">
                {{ take_massa.toFixed(1) }}
                <small>{{ ostatkaProduct.product.unitMeasurment.name }}</small>
              </h4>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="container-fluid">
      <div class="row">
        <div class="col-4">
          <div class="photo w-100 mt-4 d-flex justify-content-center px-3" >
            <img :src="hostname + image_url_str" width="240" alt="" v-if="image_url_str"
              class="shadow border rounded " 
              @click="$imageModal.open(hostname + image_url_str)">
          </div>
        </div>
        <div class="col-4">
          <div class="card py-3 px-2 mt-4" style="position: relative;" v-if="ostatkaProduct_status">
            <input class="m-0 form-control" style="height:30px; font-size: 14px;" v-model="note" type="note" validate error="wrong" success="right"/>
            <small class="bg-white" style="position: absolute; z-index:1; left:15px; top: 3px; color: #757575; font-size: 12px;">
              {{$t('note')}}
            </small>
          </div>
        </div>
      </div>
    </div>
    
    <div style="position: absolute; bottom: 5px; right: 0px;" class=" px-3">
      <div class="">
        <mdb-btn
          class="mr-1 px-3 mt-0 py-2 text-white"
          style="font-size: 10px"
          color="success"
          darkWaves
          tag="a"
          @click="getPhoto"
          size="sm"
          >{{ $t("photo") }}</mdb-btn
        >
        <!-- v-if="image_url_str" -->
        <mdb-btn 
          v-if="image_url_str"
          class="mr-1 mt-0 px-3 py-2 text-white"
          style="font-size: 10px"
          color="success"
          darkWaves
          tag="a"
          :disabled="option.real_qty.toFixed(1)<qty || qty<=0"
          @click="sendClientOtherClient"
          size="sm"
          >{{ $t("save") }}</mdb-btn
        >
      </div>
    </div>
    <webcam  v-show="showPhoto" @getPhotosub="takePhoto"/>
  </div>
</template>

<script>
import erpSelect from "../../components/erpSelectFioSearch.vue";
import InputSearch from '../../components/inputSearch';
import inputSearchYear from '../../components/inputSearchYear';
import webcam from '../webcam/webcam_Add.vue'
import { mdbInput, mdbIcon,  mdbBtn,} from "mdbvue"
import {mapActions,mapGetters} from 'vuex';
import { required } from 'vuelidate/lib/validators';

export default {
  components: {
    webcam,
    mdbIcon,
    mdbInput,
    mdbBtn, 
    erpSelect, 
    InputSearch,
    inputSearchYear
  },
  validations: {
    user_name: {
      required
    }
  },
  props:{
    option:{
      type: Object,
      default() {
        return {}
      }
    },
    navbat:{
      type: Array,
      default() {
        return []
      }
    }
  },
  data(){
    return{
      hostname: this.$store.state.server_ip,
      modal_info: '',
      modal_status: false,  
      loading: false,
      loading_table: false,
      showPhoto: false,
      id: this.$route.params.id,

      image_url_str: '',
      passport_number: '',
      born_date: '',
      phone_number: '',
      user_name: '',
      user_id: null,
      client_info: {},
      ostatkaList: [],
      ostatkaProduct: {},
      ostatkaProduct_status: false,
      note: '',

      qty:0,
      qty_str:'',
      take_massa: 0,
      allList: [],
      check_number: 1,
    }
  },
  async mounted() {
    await this.fetchClient();
  },
  computed: mapGetters(['allClient',]),
  methods:{
    ...mapActions(['fetchClient',]),
    // mahsulatni bir kliendan boshqasiga yuborish
    async sendClientOtherClient(){
      this.check_number = 1;
      if(this.allList.length>0){
        if(this.allList[0].check_number != undefined ){
          if(parseInt(this.allList[0].check_number) == 0)
          {
            this.check_number = parseInt(this.allList[1].check_number) + 1;
          }
          else{
            this.check_number = parseInt(this.allList[0].check_number) + 1;
          }
        }
      }
      const requestOptions = {
          method : "POST",
          headers: { "Content-Type" : "application/json" },
          body: JSON.stringify({
            "note" : this.note + ' ( ' + this.option.client.fio + 'dan ' + this.user_name + 'ga utkazildi. )',
            "tegirmonClientid" : this.user_id,
            "summ" : 0,
            "tegirmonProductid" : this.option.product.id,
            "qty_real" : this.qty,
            "image_str_url" : this.image_url_str,
            "id" : 0,
            "tegirmonAuthid": localStorage.AuthId,
            "check_number": this.check_number,
            "user_name": this.user_name,
            "auth_user_name": localStorage.user_name,
            "credit_sum": this.take_massa,
          })
        };
        try{
          this.loading = true;
          const response = await fetch(this.$store.state.hostname + "/TegirmonInvoice/postInvoiceBugdoyniZaxiragaOlibQolishClientdan", requestOptions);
          const data = await response.json();
          console.log(data)
          this.loading = false;
          // console.log(response.status)
          if(response.status == 201 || response.status == 200)
          {
            await this.sendClientMinusZaxira();
            // console.log('fire' )
          }
          else{
            this.modal_info = data;
            this.modal_status = true;
          }
        }
        catch{
          this.loading = false;
          this.modal_info = this.$i18n.t('network_ne_connect'); 
          this.modal_status = true;
        }
    },
    async sendClientMinusZaxira(){
      let check_number = 1;
      if(this.navbat.length>0){
        if(this.navbat[0].check_number != undefined ){
          if(parseInt(this.navbat[0].check_number) == 0)
          {
            check_number = parseInt(this.navbat[1].check_number) + 1;
          }
          else{
            check_number = parseInt(this.navbat[0].check_number) + 1;
          }
        }
      }

      const requestOptions = {
          method : "POST",
          headers: { "Content-Type" : "application/json" },
          body: JSON.stringify({
            "note" : this.note + ' ( ' + this.option.client.fio + 'dan ' + this.user_name + 'ga utkazildi. )',
            "summ": 0,
            "tegirmonClientid" : this.option.client.id,
            "tegirmonProductid" : this.option.product.id,
            "tegirmonAuthid": localStorage.AuthId,
            "image_str_url": this.image_url_str,
            "user_name": this.option.client.fio,
            "credit_sum": parseFloat(this.option.real_qty - this.qty),     //bu usha paytgi ostatkani yozib quyabman
            "qty_real" : this.qty,
            "auth_user_updator_id": parseInt(this.option.real_qty),
            "check_number": check_number,
            "tegirmonSkladid": 1,
            "id" : 0,
          })
        };
        try{
          this.loading = true;
          const response = await fetch(this.$store.state.hostname + "/TegirmonInvoice/postInvoiceBugdoyniZaxiradanNarsalargaAlmashtrish", requestOptions);
          this.loading = false;
          if(response.status == 201 || response.status == 200)
          {
            // const data = await response.json();
            // this.fetchSendTgMessage(this.option.client.fio, this.option.product.name, this.option.client.id, this.qty)
            if(parseFloat(this.option.real_qty - this.qty)>= -1 && parseFloat(this.option.real_qty - this.qty) < 1){
              await this.fetch_Zero_done();
            }
            this.$emit('close', {qty:this.qty});
            
          }
          else{
            this.loading = false;
            const data_string = await response.text();
            this.modal_info = data_string;
            this.modal_status = true;
          }
        }
        catch{
          this.loading = false;
          const data_string = await response.text();
          this.modal_info = data_string;
          this.modal_status = true;
        }
    },

    async fetch_Zero_done(){
      console.log('nulni ichiga kirayabdi')
      const requestOptions = {
        method : "POST",
        headers: { "Content-Type" : "application/json" },
        body: JSON.stringify({
          "note" : '0 buldi',
          "summ": 0,
          "tegirmonClientid" : this.option.client.id,
          "tegirmonProductid" : this.option.product.id,
          "tegirmonAuthid": localStorage.AuthId,
          "image_str_url": this.image_url_str,
          "user_name": this.option.client.fio,
          "credit_sum": parseFloat(this.option.real_qty - this.qty),     //bu usha paytgi ostatkani yozib quyabman
          "qty_real" : 0,
          "id" : 0,
        })
      };
      try{
        const response = await fetch(this.$store.state.hostname + "/TegirmonInvoice/postInvoiceBugdoyniZaxiradanNarsalargaAlmashtrish", requestOptions);
          if(response.status == 201 || response.status == 200)
          {
            console.log('nol yacheyka qushildi');
          }
      }
      catch(error){
        console.log(error, 'nol qilishda server bilan aloqa uzildi')
      }
    },




    async fetchOldList(id){
      try{
        // this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/TegirmonInvoice/getPaginationByClientId?page=0&size=10000&client_id=" + id);
        const data = await response.json();
        // this.loading = false;
        if(response.status == 201 || response.status == 200)
        {
          this.allList = data.items_list;
          return true;
        }
        else{
          // const data = await response.text();
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


    getPhoto(){
      this.showPhoto = true;
    },
    takePhoto(img){
      this.image_url_str = img;
      this.showPhoto = false;
    },
    async selectClientPassport(option){
      this.client_info = option;
      this.user_name = option.fio;
      this.user_id = option.id;
      this.passport_number = option.passport_number;
      this.phone_number = option.note;
      if(option.addiotionala_information){
        this.born_date = option.addiotionala_information.slice(8,10) + '-' + option.addiotionala_information.slice(5,7) + '-' + option.addiotionala_information.slice(0,4);
      }
      await this.fetchOstatka(option.id);
      await this.fetchOldList(option.id);
    },
    async selectClientPhone(option){
      this.client_info = option;
      this.user_name = option.fio;
      this.user_id = option.id;
      this.passport_number = option.passport_number;
      this.phone_number = option.note;
      if(option.addiotionala_information){
        this.born_date = option.addiotionala_information.slice(8,10) + '-' + option.addiotionala_information.slice(5,7) + '-' + option.addiotionala_information.slice(0,4);
      }
      await this.fetchOstatka(option.id);
      await this.fetchOldList(option.id);

    },
    async selectClientBorn(option){
      this.client_info = option;
      this.user_name = option.fio;
      this.user_id = option.id;
      this.passport_number = option.passport_number
      this.phone_number = option.note
      if(option.addiotionala_information){
        this.born_date = option.addiotionala_information.slice(8,10) + '-' + option.addiotionala_information.slice(5,7) + '-' + option.addiotionala_information.slice(0,4);
      }
      await this.fetchOstatka(option.id);
      await this.fetchOldList(option.id);

    },

    async selectOptionUser(option){
      this.client_info = option;
      this.user_name = option.fio;
      this.user_id = option.id;
      this.passport_number = option.passport_number
      this.phone_number = option.note
      if(option.addiotionala_information){
        this.born_date = option.addiotionala_information.slice(8,10) + '-' + option.addiotionala_information.slice(5,7) + '-' + option.addiotionala_information.slice(0,4);
      }
      await this.fetchOstatka(option.id);
      await this.fetchOldList(option.id);

    },
    async fetchOstatka(id){
      try{
        this.loading_table = true;
        this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/TegirmonClientOstatkas/getPaginationGetByClientClientIdList?page=0&size=100&client_id=" + id);
        const data = await response.json();
        this.loading_table = false;
        this.loading = false;
        if(response.status == 201 || response.status == 200)
        {
          this.ostatkaList = data.items_list;
          // console.log('this.ostatkaList', this.ostatkaList)
          this.ostatkaProduct_status = false;
          this.ostatkaProduct = {};
          this.take_massa = 0;
          for(let i=0; i<this.ostatkaList.length; i++){
            if(this.option.TegirmonProductid == this.ostatkaList[i].TegirmonProductid){
              this.ostatkaProduct_status = true;
              this.ostatkaProduct = this.ostatkaList[i];
              this.take_massa  =  this.ostatkaList[i].real_qty;
            }
          }
          if(this.ostatkaProduct_status == false){
            this.ostatkaProduct = {
              real_qty: 0,
              client: {
                fio: this.client_info.fio
              },
              product:{
                name: this.option.product.name,
                unitMeasurment: {
                  name: this.option.product.unitMeasurment.name
                }

              }
            }
            this.take_massa = 0;
            this.ostatkaProduct_status = true;

          }
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
        this.loading_table = false;
        this.modal_info = this.$i18n.t('network_ne_connect');
        this.modal_status = true;
      }
    },
    
    funcSendSumm(n){
      var tols = ''
      for(let i=0; i<n.length; i++){
        if(n[i] != ' '){
          tols += n[i];
        }
       }
       if(tols[tols.length-1] != '0' && tols[tols.length-1] != '1' && tols[tols.length-1] != '2' && tols[tols.length-1] != '3' && tols[tols.length-1] != '4' && 
        tols[tols.length-1] != '5' && tols[tols.length-1] != '6' && tols[tols.length-1] != '7' && tols[tols.length-1] != '8' && tols[tols.length-1] != '9' && tols[tols.length-1] != '.'){
        tols = tols.slice(0,tols.length-1)
       }
       this.qty_str = tols.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
       var temp = ''
       for(let i=0; i<this.qty_str.length; i++){
        if(this.qty_str[i] != ' '){
          temp += this.qty_str[i];
        }
       }
       if(temp == ''){
        this.qty = 0
       }
       else{
        this.qty = parseFloat(temp);
       }
       this.take_massa = this.ostatkaProduct.real_qty + this.qty;

    },
  }
}
</script>

<style lang="scss" scoped>
.send_other_client_product{
  min-height: 400px;
  position: relative;
}
.pro_name_color{
  color: #757575;
}
.transfer-line {
  position: relative;
  height: 5px;
  border-radius: 999px;
  background: rgba(34,197,94,.15);
  overflow: hidden;
}

/* Oqim animatsiyasi */
.transfer-line::before {
  content: "";
  position: absolute;
  inset: 0;
  background: 
    repeating-linear-gradient(
      90deg,
      #22c55e 0 15px,        /* yashil segment */
      transparent 15px 30px  /* bo‘sh segment */
    );
  animation: move-flow 1s linear infinite;
  opacity: .8;
}

@keyframes move-flow {
  to { background-position: 30px 0; }
}
</style>