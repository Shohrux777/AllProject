<template>
  <div>
    <div style="margin-top:10px;" class=" pt-3">
      <div class="row">
        <div class="col-12 px-4 mb-2 mt-2">
          <div style="position:relative;">
            <input type="text" v-model="name"
            class="form-control  border mt-0 pr-2" style="border:none; outline:none; height:30px;">
            <small style="position:absolute; top:-16px; left:3px; font-size:11.5px; font-weight:bold; " class="testing">
              {{ $t('name') }}
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

          <mdb-btn   @click="photoRasxod()" color="info"  style="font-size: 10px; padding: 6px 0;" class="m-0 px-3 ml-2 mb-0"
            > <mdb-icon fas class="mr-1"  icon="camera"></mdb-icon>  {{$t('photo')}}
          </mdb-btn>

          <mdb-btn color="primary" style="font-size:10px; padding: 6px 0;" v-if="photo_url"  @click="submitSavdoKassa" class="m-0 px-3 ml-1 mb-0" > {{$t('apply')}}</mdb-btn>
        </div>
      </div>
      </div>
    <webcam  v-if="showPhoto" @getPhotosub="takePhoto"/>  
    <Alert ref="alert"></Alert> 

  </div>
</template>

<script>
import webcam from '../webcam/webcam_Add.vue'
import {mdbInput, mdbBtn, mdbIcon } from 'mdbvue'
import {mapActions,mapGetters} from 'vuex'
export default {
  components: {
    mdbInput, mdbBtn,mdbIcon,
    webcam
  }, 
data(){
    return{
      showPhoto:false,
      photo_url: '',
      hostname: this.$store.state.server_ip,

      name: '',
      note: '',
      kassa_name: '',
      kassa_id: null,
    }
},
props: {
  item_list: {
    type: Array,
    default() {
      return [];
    }
  }
},
computed: mapGetters(['allKassa','user_kassa_list', 'user_kassa_info']),
methods:{
  ...mapActions(['fetchKassa', 'fetchKassa_userId', 'fetchKassa_info']),
    async refresh(kassa_data){
      console.log(kassa_data)
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

    async clw_cl(){
      this.name = '';
      this.note = '';
    },
    async submitSavdoKassa(){
      let list = [];
      for(let i=0; i<this.item_list.length; i++){
        let temp = {
            Product_Name: this.item_list[i].name,
            Qty: this.item_list[i].qty,
            Product_Id: this.item_list[i].product_id,
        }
        list.push(temp)
      }
      console.log(list);

      await this.fetchKassa_userId(localStorage.user_id);
        if(this.user_kassa_list.length){
          localStorage.kassa_id = this.user_kassa_list[0].id;
          localStorage.kassa_num = this.user_kassa_list[0].num_1;
        }
        else{
          this.$refs.alert.error('Bu foydalanuvchi kassaga biriktirilmagan, unda savdo qilish huquqi yuq !');
          localStorage.kassa_id = 0;
          localStorage.kassa_num = 0;
          return;
        }

      const requestOptions = {
        method : "POST",
        headers: { "Content-Type" : "application/json" },
        body: JSON.stringify({
          "Name": this.name,
          "User_Name": localStorage.user_name,
          "Note": this.note,
          "Photo": this.photo_url,
          "Auth_Id": localStorage.AuthId,
          "Sklad_Id": 1,
          "Item_List": list
          // "uz_card": 0,     for skidka uchun ishlataman
        })
      };
      console.log('requestOptions')

      console.log(requestOptions.body)
    //   return;
      try{
        this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/TegirmonSklad/transfer_oshxona", requestOptions);
        // const data = await response.json();
        console.log(response)
        if(response.status == 201 || response.status == 200)
        {
          this.clw_cl();
          this.$emit('close_pay');
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
    
}
}
</script>

<style>

</style>