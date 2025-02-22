<template>
  <div>
    <div style="margin-top:10px;" class=" pt-3">
      <div class="row">
        <div class="col-12 px-4 mb-2 mt-2">
          <div style="position:relative;">
            <input type="text" v-model="next_day_cash_string"  v-on:keyup.13 = "payed" @keyup="funcCash($event.target.value)" v-on:click.capture="naqd_Nol" @blur="funcAllBlue" ref="cashIn"  
            class="form-control  border mt-0 text-right pr-2" style="border:none; outline:none;font-weight:bold; height:30px;" >
            <small style="position:absolute; top:-16px; left:3px; font-size:11.5px; font-weight:bold; " class="testing">
              Наличные
            </small>
          </div>
        </div>
        <div class="col-12 px-4 mt-3 mb-2">
          <div style="position:relative;">
            <input type="text" v-model="next_day_dollor_string" v-on:keyup.13 = "payed" @keyup="funcDollor($event.target.value)" v-on:click.capture="dollor_Nol" @blur="funcAllBlue"  ref="uzcardIn" 
            class="form-control  border mt-0 text-right pr-2" style="border:none; outline:none;font-weight:bold; height:30px;">
            <small style="position:absolute; top:-16px; left:3px; font-size:11.5px; font-weight:bold; " class="testing">
              Доллор
            </small> 
          </div>
        </div>
        <div class="col-12 px-4 mt-3 mb-2">
          <div style="position:relative;">
            <textarea v-model="note" class="form-control" id="exampleFormControlTextarea1" rows="2"></textarea>
            <small for="exampleFormControlTextarea1" 
              style="position:absolute; top:-16px; left:3px; font-size:11.5px; font-weight:bold;" 
              class="testing">
              {{$t('note')}}
            </small>
          </div>
        </div>
        <div class="col-12 px-4 mt-3 mb-2">
          <div class="photoUrlShow">
            <img :src="hostname + photo_url" v-if="photo_url" style="height: 110px;" class="img-fluid img-thumbnail img-responsive shadow" alt="">
          </div>
        </div>
        <div class="col-12 border-top mt-1 py-2 text-right px-4">
          
          <mdb-btn color="danger" style="font-size:10px; padding: 6px 0;"  @click="close" class="m-0 px-3 ml-1 mb-0" > {{$t('cancel')}}</mdb-btn>
          <mdb-btn   @click="photoRasxod()" color="info"  style="font-size:10px; padding: 6px 0;" class="m-0 px-3 ml-2 mb-0"
            > <mdb-icon fas class="mr-1"  icon="camera"></mdb-icon>  {{$t('photo')}}
          </mdb-btn>
          <mdb-btn color="primary" style="font-size:10px; padding: 6px 0;" v-if="photo_url" @click="submitSavdoKassa" class="m-0 px-3 ml-1 mb-0" > {{$t('apply')}}</mdb-btn>
        </div>
      </div>
      </div>
    <webcam  v-if="showPhoto" @getPhotosub="takePhoto"/>
    <Alert ref="alert"></Alert> 

  </div>
</template>

<script>
import {mdbInput, mdbBtn, mdbIcon } from 'mdbvue'
import webcam from '../webcam/webcam_Add.vue'
import {mapActions, mapGetters} from 'vuex'

export default {
  components: {
    mdbInput, mdbBtn,mdbIcon, webcam
  }, 
data(){
    return{
        showPhoto:false,
        photo_url: '',
        hostname: this.$store.state.server_ip,

        next_day_cash_string: '0',
        next_day_cash: 0,
        next_day_dollor_string: '0',
        next_day_dollor: 0,
        next_day_id: 0,
        note: ''
    }
},
computed: mapGetters(['user_kassa_info']),

methods:{
    ...mapActions(['fetchKassa_info' ]),
    async refresh(kassa_data){
      this.note = kassa_data.name + "dan o'tkazildi";
      this.clw_cl();
    },

    takePhoto(img){
      // console.log(img)
      this.photo_url = img;
      this.showPhoto = false;
    },
    photoRasxod(){
      this.showPhoto = true;
    },

    close(){
        this.$emit('close')
    },
    clw_cl(){
      this.next_day_cash = 0;
      this.next_day_dollor = 0;
      this.next_day_cash_string = '0';
      this.next_day_dollor_string = '0';
      this.photo_url = '';
    },
    async submitSavdoKassa(){
      if(this.next_day_cash == 0 && this.next_day_dollor == 0){
        this.$refs.alert.error('Summa kiritilmadi!');
        return; 
      }
      await this.fetchKassa_info(localStorage.kassa_id);
      if(this.next_day_dollor>this.user_kassa_info.dollor){
        this.$refs.alert.error('Kassada Dollor yetarli emas !');
        return;

      }
      else if(this.next_day_cash>this.user_kassa_info.cash){
        this.$refs.alert.error('Kassada Naqd pul yetarli emas !');
        return;
      }
      const requestOptions = {
        method : "POST",
        headers: { "Content-Type" : "application/json" },
        body: JSON.stringify({
          "worker_name": localStorage.user_name,
          "auth_user_creator_id": localStorage.AuthId,
          "dollor": this.next_day_dollor,
          "dollor_string": this.next_day_dollor_string,
          "all_summ": 0,
          "all_summ_string": '0',
          "kurs": 0,
          "note": this.note + ' ( ' + localStorage.user_name + ' )',
          "addiotionala_information": localStorage.user_name,
          "rasxod" : 0,
          "prixod": this.next_day_cash,
          "reserve": '0',
          "image_url": this.next_day_cash_string,
          "status_rasxod": 1,
          "main_kassa_status": true,
          "auth_user_updator_id": localStorage.kassa_id,
        })
      };
      try{
        this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/TegirmonMainKassaRasxod", requestOptions);
        // const data = await response.json();
        console.log(response)
        if(response.status == 201 || response.status == 200)
        {
          this.clw_cl();
          this.$emit('close');

          return true;
        }
        else{
          console.log('else error')
          return false;
        }
      }
      catch{
        console.log('catch error')
        return false;
      }
    },
    funcCash(n){
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
      this.next_day_cash_string = tols.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      var temp = ''
      for(let i=0; i<this.next_day_cash_string.length; i++){
        if(this.next_day_cash_string[i] != ' '){
          temp += this.next_day_cash_string[i];
        }
      }
      this.next_day_cash = parseFloat(temp);
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
      this.next_day_dollor_string = tols.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      var temp = ''
      for(let i=0; i<this.next_day_dollor_string.length; i++){
        if(this.next_day_dollor_string[i] != ' '){
          temp += this.next_day_dollor_string[i];
        }
      }
      this.next_day_dollor = parseFloat(temp);
    },

    naqd_Nol(){
      if(this.next_day_cash_string == '0'){
        this.next_day_cash_string = '';
        this.next_day_cash = 0;
      }
    },
    dollor_Nol(){
      if(this.next_day_dollor_string == '0'){
        this.next_day_dollor_string = '';
        this.next_day_dollor = 0;
      }
    },

    funcAllBlue(){
      if(this.next_day_cash_string == '' || this.next_day_cash_string == null){
        this.next_day_cash_string = '0';
        this.next_day_cash = 0;
      }
      else if(this.next_day_dollor_string == '' || this.next_day_dollor_string == null){
        this.next_day_dollor_string = '0';
        this.next_day_dollor = 0;
      }
    }
}
}
</script>

<style>

</style>