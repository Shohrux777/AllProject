<template>
  <div class="rasxod_list_add p-4">
    <loader v-if="loading"/>
    <div v-else class="d-flex w-100">
      <div style="width: 50%;" v-show="false">
        <div class="row px-3">
          <div class="col-7 pr-1" >
            <div class="" style="position:relative;">
              <input class="m-0 px-2 form-control" v-model="search" @input="SearchClientNamePass()" 
                 group type="input" validate error="wrong" success="right"
                 style="height:30px; font-size: 12.5px;"/>
              <small
                style="position: absolute; top: -10px; left: 5px; font-size: 11px"
                class="bg-white px-2 py-0"
              >Поиск сотрудник</small>
            </div>
          </div>
          <div class="col-5 pl-1">
            <div class="" style="position:relative;">
              <input
                class="m-0 p-0 form-control px-2"
                style="height: 30px; font-size: 12.5px;"
                v-model="phone_number"
                @input="SearchClientBornDate()"
                outline
                group
                type="text"
                validate
                error="wrong"
                success="right"
              />
              <small
                style="position: absolute; top: -10px; left: 5px; font-size: 11px"
                class="bg-white px-2 py-0"
                >{{ $t("born_date") }}</small
              >
            </div>
          </div>
        </div>
        
        <div class="user_rasxod_worker_list py-1 border-bottom  px-3" >
          <div  v-for="(item,i) in client_list.rows" :key="i" class="item px-3" 
            @click="getBemorId(i,item)" :class="{'activeUser' : active_bemor == i }">
            <div>
              <div class="d-flex">
                <div class=" user_photo">
                  <img src="../../assets/client.png" style="height: 45px; overflow: none;" class="img-fluid" alt="">
                </div>
                <div class="px-3">
                  <p class="m-0 p-0 font-weight-bold" style="font-size: 11px;">{{item.ism}}</p>
                  <p class="m-0 mt-1 rangRasxod" style="font-size:10px;">Год: <span v-if="item.born_date" class="px-2">
                    {{ item.born_date.slice(8,10) + '-' + item.born_date.slice(5,7) + '-' + item.born_date.slice(0,4)}}</span> 
                    Тел: <span class="px-2">{{item.phone_number}}</span></p>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div style="width: 100%;">
        <div  class="row pl-3">
          <div class="col-12 mt-0">
            <input class="m-0 px-2 form-control" :value="client_info.ism" @keypress="change_klient_name()"
            @blur="blur_client_name()"
             group type="input" validate error="wrong" success="right"
             style="height:32px; font-size: 13.5px;"/>
            
          </div>
          <div class="col-12 mt-3">
            <input class="m-0 px-2 form-control applied" :value="all_summ.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')" 
             group type="input" validate error="wrong" success="right" disabled
             style="height:32px; font-size: 13.5px;"/>
            <small
              style="position: absolute; top: -12px; left: 20px; font-size: 11px"
              class="bg-white px-2 py-0"
              >Общий сумма</small>
          </div>
          <div class="col-12 mt-3">
            <input class="m-0 px-2 form-control" v-model="rasxod" @keyup="funcRasxod($event.target.value)"
              @click="select_qty_Input()" @blur="blur_qty_Input()"
             group type="input" validate error="wrong" success="right"
             style="height:32px; font-size: 13.5px;"/>
            <small
              style="position: absolute; top: -7px; left: 20px; font-size: 11px"
              class="bg-white px-2 py-0"
              >{{ $t("price") + ' сўм'}}</small>
               <small class="invalid-text pt-4" style="margin-left:5px; "  v-if="$v.rasxod.$dirty && !$v.rasxod.required" >
                  {{$t('name_invalid_text')}}
                </small>
          </div>
          

          <div class="col-12 mt-3" v-show="false">
            <div class="d-flex w-100" >
              <div style="width:50%; position:relative;" class="pr-1">
                <input class="m-0 px-2 form-control" v-model="dollor_string" @keyup="funcDollor($event.target.value)"
                  @click="select_qty_dl()" @blur="blur_qty_dl()"
                group type="input" validate error="wrong" success="right"
                style="height:32px; font-size: 13.5px;"/>
                <small
                  style="position: absolute; top: -7px; left: 5px; font-size: 11px"
                  class="bg-white px-2 py-0"
                  >{{ $t("price") + ' $' }}</small>
              </div>
              <div  style="width:40%; position:relative;" class="pr-1 pl-2">
                <input class="m-0 px-2 form-control" v-model="dollor_kurs" @keyup="funcDollorKurs($event.target.value)"
                  @click="select_qty_dl()" @blur="blur_qty_dl()"
                group type="input" validate error="wrong" success="right"
                style="height:32px; font-size: 13.5px;"/>
                <small
                  style="position: absolute; top: -7px; left: 10px; font-size: 11px"
                  class="bg-white px-2 py-0"
                  >Курс</small>
              </div>
              <div style="width:10%; position: relative;" v-if="dollor_diff<0">
                <svg xmlns="http://www.w3.org/2000/svg" style="position:absolute; top:-2px;"
                  class="icon icon-tabler icon-tabler-trending-down" width="23" height="23" viewBox="0 0 24 24" stroke-width="1.5" stroke="#ff2825" fill="none" stroke-linecap="round" stroke-linejoin="round">
                  <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                  <path d="M3 7l6 6l4 -4l8 8" />
                  <path d="M21 10l0 7l-7 0" />
                </svg>
                <small style="position:absolute; top:17px; left:3px; font-size: 10px;">{{dollor_diff}}</small>
              </div>
              <div style="width:10%; position: relative;" v-else>
                <svg xmlns="http://www.w3.org/2000/svg" style="position:absolute; top:-2px;"
                  class="icon icon-tabler icon-tabler-trending-up" width="23" height="23" viewBox="0 0 24 24" stroke-width="1.5" stroke="#00b341" fill="none" stroke-linecap="round" stroke-linejoin="round">
                  <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                  <path d="M3 17l6 -6l4 4l8 -8" />
                  <path d="M14 7l7 0l0 7" />
                </svg>
                <small style="position:absolute; top:17px; left:3px; font-size: 10px;">{{dollor_diff}}</small>
              </div>
            </div>
          </div>
          <div class="col-12 mt-3">
            <mdb-input class="m-0 p-0" v-model="note" style="font-size: 14px;" size="sm" outline group type="textarea" validate error="wrong" success="right"/>
            <small
              style="position: absolute; top: -10px; left: 20px; font-size: 11px"
              class="bg-white px-2 py-0"
              >{{ $t("reason") }}</small>
          </div>
          <div class="col-12">
            <!-- Default radio -->
            <div class="form-check" @click="note = 'Ish vaqti hisoblanmagan pul qushildi.'">
              <input class="form-check-input" type="radio" name="flexRadioDefault" id="flexRadioDefault1"/>
              <label style="font-size: 13px;" class="form-check-label" for="flexRadioDefault1"> Ish vaqti hisoblanmagan pul qo'shildi. </label>
            </div>
          </div>
          
          <div class="col-12 mt-2 text-right border-top mx-3 pt-1 d-flex justify-content-between">
            <div class="photoUrlShow">
              <img :src="hostname + photo_url" v-if="photo_url" style="height: 110px;" class="img-fluid img-thumbnail img-responsive shadow" alt="">
            </div>
            <div>
              <mdb-btn   @click="photoRasxod()" color="info" m="r2" style="font-size: 9.5px"
                p="r3 l3 t2 b2"> <mdb-icon fas class="mr-1"  icon="camera"></mdb-icon>  {{$t('photo')}}
              </mdb-btn>
              <mdb-btn   @click="saveRasxod()" v-if="photo_url" color="success" m="r2" style="font-size: 9.5px"
                p="r3 l3 t2 b2"> <mdb-icon fas class="mr-1"  icon="plus"></mdb-icon>  {{$t('save')}}
              </mdb-btn>
            </div>
          </div>
        </div>
      </div>
    </div>
    <webcam  v-if="showPhoto" @getPhotosub="takePhoto"/>
    <massage_box :hide="modal_status" :detail_info="modal_info"
      :m_text="$t('Failed_to_add')" @to_hide_modal="modal_status= false"/>
      <Toast ref="message"></Toast>
      <Alert ref="alert"></Alert>
  </div>
</template>

<script>
import { required } from 'vuelidate/lib/validators'
import {mdbInput, mdbBtn, mdbIcon } from 'mdbvue'
import {mapActions, mapGetters} from 'vuex'
import webcam from '../webcam/webcam_Add.vue'

export default {
  components: {
    mdbInput, mdbBtn,mdbIcon, webcam
  }, 
  data() {
    return {
      hostname: this.$store.state.server_ip,
      modal_info: '',
      modal_status: false,
      loading: false,
      active_bemor: -1,
      client_data: {},
      client_name: '',
      client_id: null,
      rasxod: '0',
      rasxod_qty: 0,
      dollor_string: 0,
      dollor: 0,
      note: '',
      search: '',
      phone_number: '',
      client_list: {
        rows:[]
      },
      all_summ: 0,
      dollor_kurs: '0',
      dollor_kurs_qty: 0,
      dollor_diff: 0,

      photo_url: '',
      showPhoto: false,
    }
  },
  validations: {
    rasxod: {
      required
    },
  },
  props:{
    client_info:{
      type: Object,
      default() {
        return {}; // yoki istalgan default object
      }
    },
    
  },
  computed: mapGetters(['get_user_list']),
  async mounted(){
    await this.fetch_user();
    this.client_list = this.get_user_list;
    console.log('this.get_user_list', this.get_user_list)
    // await this.nbuKurs();
  },
  methods: {
    ...mapActions(['fetch_user', ]),
    clw_rw(){
      this.rasxod = '0';
      this.rasxod_qty = 0;
      this.client_data = {};
      this.client_name = '';
      this.client_id = null;
      this.active_bemor = -1;
      this.note = '';
      this.all_summ = 0;
      this.dollor_string = '0'
      this.dollor = 0;
      this.photo_url = '';
      this.showPhoto = false;
    },
    async saveRasxod(){
      if(!this.rasxod_qty){
        this.$refs.alert.error('Summa kiritilmadi!');
        return;
      }
       if(this.$v.$invalid )
        {
          this.$v.$touch();
          this.$refs.message.warning('please_fill')
          return false;
        }
      console.log('dadas')
      const requestOptions = {
        method : "POST",
        headers: { "Content-Type" : "application/json" },
        body: JSON.stringify({
          "userid": this.client_info.userid,
          "salary_id": this.client_info.res_badgenumber,
          "work_time": "00:00:00",
          "sum": this.all_summ,
          "num": 3,
          "note": this.note,
          "image_url": this.photo_url,
        })
      };
      console.log('requestOptions.body')
      console.log(requestOptions.body)
      try{
        this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/TegirmonUserIshlaganPuli", requestOptions);
        // const data = await response.json();
        console.log(response)
        if(response.status == 201 || response.status == 200)
        {
          this.clw_rw();
          this.$emit('close');
          this.loading = false;
          return true;
        }
        else{
          this.modal_info = this.$i18n.t('network_ne_connect');
          this.modal_status = true;
          this.loading = false;
          return false;
        }
      }
      catch{
        this.loading = false;
        this.modal_info = this.$i18n.t('network_ne_connect');
        this.modal_status = true;
        return false;
      }
      
    },
    getBemorId(i, data){
      console.log(data)
      this.client_data = data;
      this.active_bemor = i;
      this.client_name = data.ism;
      this.client_id = data.userid;
      this.search = data.ism;
      if(data.born_date){
        this.phone_number = data.born_date.slice(8,10) + '-' + data.born_date.slice(5,7) + '-' + data.born_date.slice(0,4)
      }
    },
    async change_klient_name(){
      this.client_data = {};
      this.active_bemor = -1;
      this.client_id = null;
      this.phone_number = '';
      this.search = '';
    },
    async blur_client_name(){
      if(this.client_id){
        this.client_name = this.search;
      }
    },
    async SearchClientNamePass(){
      this.phone_number = '';
      if(this.search == ''){
        await this.fetch_user();
        this.client_list = this.get_user_list;
        return
      }
      // /TegirmonClient/getPaginationSearchByFioOrPassportSerailNumberOrHomeOrMobilePhoneNumber?page=0&size=100&fio_or_serial_number=
      try{
        // this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/SkudMyUserinfoes/getPaginationSearchByIsmOrPassportSerailNumberOrMobilePhoneNumberFaceUser?page=0&size=100&fio_or_serial_number=" + this.search);
        const data = await response.json();
        if(response.status == 201 || response.status == 200)
        {
          this.client_list.rows = data.items_list;
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
        // this.loading = false;
        this.client_list = this.get_user_list;
        this.modal_info = this.$i18n.t('network_ne_connect');
        this.modal_status = true;
      }
      
    },
    async SearchClientBornDate(){
      this.search = '';
      if(this.phone_number == ''){
        await this.fetch_user();
        this.client_list = this.get_user_list;
        return
      }
      var x = this.phone_number.replace(/\D/g, '').match(/(\d{0,2})(\d{0,2})(\d{0,4})/);
      // this.phone_number = !x[2] ? x[1] : + x[1] + '-' + x[2] + (x[3] ? '-' + x[3] : '');
      if(this.phone_number.length == 2){
        this.phone_number = x[1];
      }
      else if(x[1].length >= 2){
        this.phone_number = x[1] + '-' + x[2] + (x[3] ? '-' + x[3] : '');
      }
      // --> regex inputni tugilgan yil formatiga utkazish dd-mm-yyyy

      // --> regexdan qidirish formatiga utkazish yyyy-mm-dd
      var search_birthday = '';
      if(this.phone_number.length <= 2){
        search_birthday = this.phone_number
      }
      else if(this.phone_number.length >= 5 && this.phone_number.length < 10){
        search_birthday = this.phone_number.slice(3,5) + '-' + this.phone_number.slice(0,2)
      }
      else{
        search_birthday = this.phone_number.slice(6,11) + '-' + this.phone_number.slice(3,5) + '-' + this.phone_number.slice(0,2);
      }
      // --> regexdan qidirish formatiga utkazish yyyy-mm-dd
    

      // /TegirmonClient/getPaginationSearchByFioOrPassportSerailNumberOrHomeOrMobilePhoneNumber?page=0&size=100&fio_or_serial_number=
      try{
        // this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/SkudMyUserinfoes/getPaginationSearchByIsmOrPassportSerailNumberOrMobilePhoneNumberFaceUser?page=0&size=100&fio_or_serial_number=" + search_birthday);
        const data = await response.json();
        // this.loading = false;
        if(response.status == 201 || response.status == 200)
        {
          this.client_list.rows = data.items_list;
          if(data.items_list == 0){
            this.$refs.message.error('not_client')
          }
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
        // this.loading = false;
        this.client_list = this.get_user_list;
        this.modal_info = this.$i18n.t('network_ne_connect');
        this.modal_status = true;
      }
      
    },

    select_qty_Input(){
      if(this.rasxod == '0'){
        this.rasxod = null;
      }
    },
    blur_qty_Input(){
      if(this.rasxod == null || this.rasxod == ''){
        this.rasxod = '0';
      }
    },
    select_qty_dl(){
      if(this.dollor_string == '0'){
        this.dollor_string = null;
      }
    },
    blur_qty_dl(){
      if(this.dollor_string == null || this.dollor_string == ''){
        this.dollor_string = '0';
      }
    },
    funcRasxod(n){
      var tols = ''
      for(let i=0; i<n.length; i++){
        if(n[i] != ' '){
          tols += n[i];
        }
       }
       if(tols[tols.length-1] != '0' && tols[tols.length-1] != '1' && tols[tols.length-1] != '2' && tols[tols.length-1] != '3' && tols[tols.length-1] != '4' && 
        tols[tols.length-1] != '5' && tols[tols.length-1] != '6' && tols[tols.length-1] != '7' && tols[tols.length-1] != '8' && tols[tols.length-1] != '9'){
        tols = tols.slice(0,tols.length-1)
       }
       this.rasxod = tols.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
       var temp = ''
       for(let i=0; i<this.rasxod.length; i++){
        if(this.rasxod[i] != ' '){
          temp += this.rasxod[i];
        }
       }
      this.rasxod_qty = parseFloat(temp);
      console.log(this.rasxod_qty)
      console.log('sum ' + this.rasxod_qty)
      console.log('dollor ' + this.dollor)
      console.log('kurs ' + this.dollor_kurs_qty)
      this.all_summ = this.rasxod_qty + (this.dollor * parseFloat(this.dollor_kurs_qty));
    },
    funcDollor(n){
      var tols = ''
      for(let i=0; i<n.length; i++){
        if(n[i] != ' '){
          tols += n[i];
        }
       }
       if(tols[tols.length-1] != '0' && tols[tols.length-1] != '1' && tols[tols.length-1] != '2' && tols[tols.length-1] != '3' && tols[tols.length-1] != '4' && 
        tols[tols.length-1] != '5' && tols[tols.length-1] != '6' && tols[tols.length-1] != '7' && tols[tols.length-1] != '8' && tols[tols.length-1] != '9'){
        tols = tols.slice(0,tols.length-1)
       }
       this.dollor_string = tols.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
       var temp = ''
       for(let i=0; i<this.dollor_string.length; i++){
        if(this.dollor_string[i] != ' '){
          temp += this.dollor_string[i];
        }
       }
      this.dollor = parseFloat(temp);
      console.log(this.dollor)
      console.log('sum ' + this.rasxod_qty)
      console.log('dollor ' + this.dollor)
      console.log('kurs ' + this.dollor_kurs_qty)
      this.all_summ = this.rasxod_qty + (this.dollor * parseFloat(this.dollor_kurs_qty));
    },
    funcDollorKurs(n){
      var tols = ''
      for(let i=0; i<n.length; i++){
        if(n[i] != ' '){
          tols += n[i];
        }
       }
       if(tols[tols.length-1] != '0' && tols[tols.length-1] != '1' && tols[tols.length-1] != '2' && tols[tols.length-1] != '3' && tols[tols.length-1] != '4' && 
        tols[tols.length-1] != '5' && tols[tols.length-1] != '6' && tols[tols.length-1] != '7' && tols[tols.length-1] != '8' && tols[tols.length-1] != '9'){
        tols = tols.slice(0,tols.length-1)
       }
       this.dollor_kurs = tols.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
       var temp = ''
       for(let i=0; i<this.dollor_kurs.length; i++){
        if(this.dollor_kurs[i] != ' '){
          temp += this.dollor_kurs[i];
        }
       }
      this.dollor_kurs_qty = parseFloat(temp);
      console.log(this.dollor_kurs_qty);
      console.log('sum ' + this.rasxod_qty)
      console.log('dollor ' + this.dollor)
      console.log('kurs ' + this.dollor_kurs_qty)
      this.all_summ = this.rasxod_qty + (this.dollor * parseFloat(this.dollor_kurs_qty));
    },
    async nbuKurs(){
      try{
        const response = await fetch("https://cbu.uz/uz/arkhiv-kursov-valyut/json/");
        const data = await response.json();
        console.log('json valyuta')
        console.log(data)
        if(data.length>0){
          this.dollor_kurs = parseInt(data[0].Rate).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')
          this.dollor_kurs_qty = parseInt(data[0].Rate);
          this.dollor_diff = data[0].Diff;
        }
      }
      catch(error){
        console.log(error)
      }
    },
    takePhoto(img){
      // console.log(img)
      this.photo_url = img;
      this.showPhoto = false;
    },
    photoRasxod(){
      this.showPhoto = true;
    }
  },
}
</script>

<style lang="scss" scoped>
::-webkit-scrollbar {
  width: 5px;
}

/* Track */
::-webkit-scrollbar-track {
  background: #dfdfdf;
}

/* Handle */
::-webkit-scrollbar-thumb {
  background: #abbdd7;
}

/* Handle on hover */
::-webkit-scrollbar-thumb:hover {
  background: #212d3d;
}
.user_rasxod_worker_list{
  overflow: hidden;
 overflow-y: scroll;
 height: 50vh;
 width: 100%;
 // background-color: rgba(32, 32, 32,0.75);
 .item{
     .user_photo{
       background-color: #fff;
         border-radius: 50%;
         width: 45px;
         height: 45px;
         overflow: hidden;
         

       // img{
       //   border-radius: 50%;
       //   overflow: hidden;          
       // }
     }
     width:100%;
     box-shadow: 2px 2px 8px rgb(224, 224, 224), -1px -1px 2px rgb(224, 224, 224);
     height: 62px;
     margin: 5px auto;
     display: flex;
     align-items: center;
     justify-content: space-between;
     // background-color: #fff;
     transition: all 0.5s ease-in-out;
     .rangRasxod{
        color: #ea605b;
        font-weight: bold;
     }
     &:hover{
       cursor: pointer;
       box-shadow: 2px 2px 5px rgb(224, 224, 224);
       background-color: #f68680;
       transform: translate(6px, 0px);
       transition: all 0.1s ease-in-out;
       color:white;
       .rangRasxod{
          color: white;
          font-weight: bold;
        }
     }
   }
 
}
.activeUser{
 cursor: pointer;
 box-shadow: 2px 2px 5px rgb(224, 224, 224);
 background-color: #f76e67;
 transform: translate(6px, 0px);
 transition: all 0.1s ease-in-out;
 color:white;
 .rangRasxod{
  color: white !important;
  font-weight: bold;
}
}
</style>