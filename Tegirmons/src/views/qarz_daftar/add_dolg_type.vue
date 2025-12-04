<template>
<div class="addClient_Modal">
  <div class="row">
    <div class="col-12">
      <div class="add_davernis p-4">
        <mdb-row>
          <mdb-col col="4">
            <p class="p-0 m-0 mt-2" style="font-size: 14px;">{{$t('name')}}</p>
          </mdb-col>
          <mdb-col col="8">
            <input class="m-0 form-control" style="height:34px;" v-model="name"  size="md"  outline  group type="text" validate error="wrong" success="right"/>
            <small class="invalid-text pt-4" style="margin-left:5px; "  v-if="$v.name.$dirty && !$v.name.required" >
              {{$t('name_invalid_text')}}
            </small>
          </mdb-col>
        </mdb-row>

      
      

        <!-- <mdb-row class="mt-3">
          <mdb-col col="4">
            <p class="p-0 m-0 mt-2" style="font-size: 14px;">Долг (UZS)</p>
          </mdb-col>
          <mdb-col col="8">
            <input class="m-0 form-control" style="height:34px;" v-model="sum_str" @keyup="funcchangeSum($event.target.value)"
            size="md" outline  group type="text" validate error="wrong" success="right"/>
          </mdb-col>
        </mdb-row>


        <mdb-row class="mt-3">
          <mdb-col col="4">
            <p class="p-0 m-0 mt-2" style="font-size: 14px;">Долг $ (USD)</p>
          </mdb-col>
          <mdb-col col="8">
            <input class="m-0 form-control" style="height:34px;" v-model="dollor_str" @keyup="funcchangeDollor($event.target.value)"
            size="md" outline  group type="text" validate error="wrong" success="right"/>
          </mdb-col>
        </mdb-row> -->
        
        <mdb-row class="mt-3">
          <mdb-col col="4">
            <p class="p-0 m-0 mt-2" style="font-size: 14px;">{{$t('picture')}}</p>
          </mdb-col>
          <mdb-col col="8" style="position: relative;">
            <div class="d-flex justify-content-start align-items-center">
              <div class="client_img" v-show="!PicShow" style="position: relative;">

              </div>
                <!-- <img :src="hostname + image2" style="height: 150px;" v-show="!PicShow" class="img-fluid img-thumbnail img-responsive shadow" alt=""> -->
                <img :src="hostname + photo_url" v-if="photo_url" v-show="photo_url" style="height: 150px;" class="img-fluid img-thumbnail img-responsive shadow" alt="">
            </div>
            <input hidden  id="inputFileToLoad" @change="previewFile()" accept="image/png, image/gif, image/jpeg" type="file" ref="file_Img" class="shadow text-right ml-2"  />
              <div class="d-flex">

                <input type="file" style="display: none;" id="file_download" @change="onFileChange" accept="image/*" />
                <label for="file_download" class="download">
                  <span>📁 Rasm yuklash</span>
                </label>
                  
                  <label @click="showPhoto = true" class="download ml-2">
                    <span>{{$t('photo')}}</span>  
                  </label>
                  
              </div>



          </mdb-col>
        </mdb-row>
          <mdb-row class="mt-3">
            <mdb-col col="4">
              <p class="p-0 m-0 mt-2" style="font-size: 14px;">{{$t('note')}}</p>
            </mdb-col>
            <mdb-col col="8">
              <mdb-input class="m-0 p-0" v-model="note" size="md" outline group type="textarea" validate error="wrong" success="right"/>
            </mdb-col>
          </mdb-row>
          <!-- <mdb-row class="mt-3">
            <mdb-col col="4">
              <p class="p-0 m-0 mt-2 text-danger" style="font-size: 14px;">{{$t('oshibka')}}</p>
            </mdb-col>
            <mdb-col col="8">
              <input
                v-model="oshibka"
                type="checkbox"
                class="mt-1"
                style="width: 22px; height: 22px;"
              />  
            </mdb-col>
          </mdb-row> -->
          <div class="m-0 p-0 bg-white" style="position:sticky; bottom:0px;">
            <div class="blue-gradient">
              <hr class="mt-2 mb-0"/>
            </div>
            <mdb-row class="mt-0 ">
              <mdb-col col="12 m-0">
                <div class="mt-2 text-right">
                  <mdb-btn color="primary" v-if="client_id" @click="next_data" style="font-size: 10.5px"
                    p="r4 l4 t2 b2">
                  <mdb-icon  />Продолжить</mdb-btn>
                  <mdb-btn color="success"  @click="save_data" style="font-size: 10.5px"
                    p="r4 l4 t2 b2">
                  <mdb-icon  />{{$t('add')}}</mdb-btn>
                </div>
              </mdb-col>
            </mdb-row>
          </div>
          
          <webcam  v-if="showPhoto" @getPhotosub="takePhoto"/>
        <Toast ref="message"></Toast>
      </div>
    </div>
    
  </div>
</div>
  
</template>

<script>
// import erpSelect from "../../components/erpSelect.vue";
import {mdbRow, mdbCol, mdbInput, mdbBtn, mdbIcon} from 'mdbvue'
import { required } from 'vuelidate/lib/validators'
import webcam from '../webcam/webcam_Add.vue'
import {mapActions,mapGetters} from 'vuex'
export default {
  components:{
    mdbRow, mdbCol, mdbInput, mdbBtn, mdbIcon, webcam
  },
  validations: {
      name: {required},
      // phone_number: {required},
      // passport_number: {required},
      // district_name : {required},
      // clientgroup_name : {required},
    },
  data() {
    return {
      hostname: this.$store.state.server_ip,
      name: '',
      id: 0,
      phone_number: '',
      passport_number: '',
      client_group_name: '',
      client_group_id: null,
      born_date: '',
      address: '',
      note: '',
      active_bemor: -1,
      showPhoto: false,
      photo_url: '',
      car_number: '',
      loading: false,
      client_id : null,
      client_data : {},

      PicShow: true,
      base64: '',
      image: '',
      image2: '',
      client_list: {
        rows:[]
      },
      oshibka: false,
      auth_user_updator_id: 0,

      sum: 0,
      sum_str: '',
      dollor_str: '',
      dollor: 0,
      user_face_id: 0,
    }
  },

  props:{
    client_info: {
      type: Object,
      default(){
        return {}
      }
    },
  },
  // props:{
  //   client_id: {
  //     type: Number,
  //     default: null,
  //   }
  // },
   computed: mapGetters(['all_district_t', 'all_client_controler', 'allClient']),
  methods: {
    ...mapActions(['fetch_district_t',]),
    delImage(){
      this.base64 = ''
      this.PicShow = true
      this.image = ''
    },
    async onFileChange(event) {
      const file = event.target.files[0];
      if (!file) return;

      const reader = new FileReader();
      reader.onload = e => {
       this.fetchBase64ToHttpImg(e.target.result);
        this.imageBase64 = e.target.result;
      };
      reader.readAsDataURL(file);
    },
    async previewFile(){
      const preview = document.getElementById('prewImageClientAdd');
        const file = document.querySelector('input[type=file]').files[0];
        this.PicShow = true;
      var reader = new FileReader();
      reader.addEventListener("load", function () {
        this.image =  reader.result;
        preview.src = reader.result;
      }, true);
      if (file) {
          this.PicShow = false
        reader.readAsDataURL(file);
      }
    },
    takePhoto(img){
      // console.log(img)
      this.photo_url = img;
      this.showPhoto = false;
    },
    async MountedFunc(){
      this.id = 0;
      this.clv_ws();
      this.active_bemor = -1;
      await this.fetchClient();
      // this.oshibka = false;
      // this.auth_user_updator_id = 0;
    },
    fetchPassword_client(length) {
      var result = '';
      var characters  = 'abcdefghijklmnopqrstuvwxyz123456789';
      var charactersLength = characters.length;
      for ( var i = 0; i < length; i++ ) {
          result += characters.charAt(Math.floor(Math.random() * charactersLength));
      }
      return result;
    },
    async getBemorId(i, data){
      this.client_data = data;
      this.active_bemor = i;
      this.name = data.name;
      this.id = data.id;
      this.sum = data.sum;
      this.dollor = data.dollor;
      this.sum_str = data.sum.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      this.dollor_str = data.dollor.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')
      this.phone_number = data.phone_number;
      this.passport_number = data.passport_number;
      this.address = data.adddress;
      this.user_face_id = data.user_face_id;
      this.note = data.note;
      this.client_id = data.id;
      this.born_date = data.addiotionala_information;
      if(data.passport_image_url == ''){
        this.base64 = ''
        this.PicShow = true;
        this.image = ''
      }else{
        this.PicShow = false;
      }
      this.image = data.passport_image_base_64;
      this.photo_url = data.image_url;
      this.image2 = data.passport_image_url;

      // if(data.auth_user_updator_id == 1){
      //   this.oshibka = true;
      // }
      // else{
      //   this.oshibka = false;
      // }
    },
    clv_ws(){
      this.name = '';
      this.id = 0;
      this.phone_number = '';
      this.passport_number = '';
      this.address = '';
      this.note = '';
      this.base64 = '';
      this.born_date = '';
      this.image = '';
      this.image2 = '';
      this.photo_url = '';
      this.showPhoto = false;
      this.PicShow = true;
      this.car_number = '';
      this.sum = 0;
      this.sum_str = '';
      this.dollor = 0;
      this.dollor_str = '';
    },

    async next_data(){
      await this.save_data();
      this.id = 0;
      // this.$emit('close_data', this.client_data)
    },

    async fetchClient(){
      try{
        const response = await fetch(this.$store.state.hostname + "/TegirmonQarzdorlikUser/getPagination?page=0&size=100");
        const data = await response.json();
        if(response.status == 201 || response.status == 200)
        {
          this.client_list.rows = data.items_list;
          return true;
        }
        else{
          this.modal_info = data;
          this.modal_status = true;
          return false;
        }
      }
      catch{
        this.client_list = this.allClient;
        this.modal_info = this.$i18n.t('network_ne_connect');
        this.modal_status = true;
      }
    },

    save_data :  async function(){
      // if(this.oshibka == false){
      //   this.auth_user_updator_id = 0;
      // }
      // else{
      //   this.auth_user_updator_id = 1;
      // }
      if(this.$v.$invalid)
      {
        this.$v.$touch();
        this.$refs.message.warning('please_fill')
        return false;
      }
      
      const requestOptions = {
        method : "POST",
        headers: { "Content-Type" : "application/json" },
        body: JSON.stringify({
          "name" : this.name,
          "note" : this.note,
          "image_url": this.photo_url,
          "passport_image_url": this.image2,
          "sum": this.sum,
          "dollor": this.dollor,
          "reserve": localStorage.user_name,
          "TegirmonQarzUserId": this.client_info.id,
          "id" : this.id,
          "auth_user_updator_id": localStorage.AuthId
        })
      };
      try{
        this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/TegirmonQarzUserType", requestOptions);
        this.loading = false;
        if(response.status == 201 || response.status == 200)
        {
        // console.log('dasdas')

          const tryjson = await response.json();
          // console.log(tryjson)
          this.clv_ws();
          this.$emit('close_data', tryjson)
          this.$refs.message.success('Added_successfully')
        }
        else{
          const data = await response.text();
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

    async scanerFile()
    {
        scanner.scan(this.displayImagesOnPage,
        {
            "output_settings" :
            [
              {
                  "type" : "return-base64",
                  "format" : "jpg"
              }
            ]
        }
        );
      },
      displayImagesOnPage(successful, mesg, response) {
        if(!successful) { // On error
            // console.error('Failed: ' + mesg);
            return;
        }

        if(successful && mesg != null && mesg.toLowerCase().indexOf('user cancel') >= 0) { // User canceled.
            // console.info('User canceled');
            return;
        }

        var scannedImages = scanner.getScannedImages(response, true, false); // returns an array of ScannedImage
        this.processScannedImage(scannedImages);
        //for(var i = 0; (scannedImages instanceof Array) && i < scannedImages.length; i++) {
        //    var scannedImage = scannedImages[i];   
        //}
      },

      processScannedImage(scannedImage) {
        var imagesScanned = [];
        // console.log(scannedImage.length)
        imagesScanned.push(scannedImage[0]);
        this.fetchBase64ToHttpImg(scannedImage[0].src);
        if(scannedImage.length>1){
          this.fetchBase64ToHttpImg2(scannedImage[1].src);
        }
        // console.log(this.image)
        this.PicShow = false;
      },

      async fetchBase64ToHttpImg(img){
        try{
          const requestOptions = {
            method : "POST",
            headers: { "Content-Type" : "application/json" },
            body: JSON.stringify({
              "image_base_64" : img,
            })
          };
          const response = await fetch(this.$store.state.hostname + "/TegirmonClient/getSaveBase64ImageToFolderAndGetImageUrl", requestOptions);
          // console.log(response)
          const data = await response.json();
          // console.log(data)
          if(response.status == 201 || response.status == 200)
          {
            this.photo_url = data.image_url_str;
          }
          else{
            this.photo_url = '';
          }
        }
        catch{
          this.photo_url = '';
        }
      },
      async fetchBase64ToHttpImg2(img){
        try{
          const requestOptions = {
            method : "POST",
            headers: { "Content-Type" : "application/json" },
            body: JSON.stringify({
              "image_base_64" : img,
            })
          };
          const response = await fetch(this.$store.state.hostname + "/TegirmonClient/getSaveBase64ImageToFolderAndGetImageUrl", requestOptions);
          // console.log(response)
          const data = await response.json();
          // console.log(data)
          if(response.status == 201 || response.status == 200)
          {
            this.image2 = data.image_url_str;
          }
          else{
            this.image2 = '';
          }
        }
        catch{
          this.image2 = '';
        }
      },

      funcchangeDollor(n){
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
        this.dollor_str = tols.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
        var temp = ''
        for(let i=0; i<this.dollor_str.length; i++){
          if(this.dollor_str[i] != ' '){
            temp += this.dollor_str[i];
          }
        }
        this.dollor = parseFloat(temp);
      },
      funcchangeSum(n){
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
        this.sum_str = tols.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
        var temp = ''
        for(let i=0; i<this.sum_str.length; i++){
          if(this.sum_str[i] != ' '){
            temp += this.sum_str[i];
          }
        }
        this.sum = parseFloat(temp);
      },
  },
}
</script>

<style lang="scss" scoped>
.addClient_Modal{
  overflow: hidden;
  overflow-y: scroll;
  height: 100%;
}

.download{
  width: 35%;
  height: 33px;
  border: 0.5px solid #4285F4;
  border-radius: 4px;
  margin-top: 7px;
  display: flex;
  align-items: center;
  color: gray; 
  font-size: 13px;
  cursor:pointer;
}
.download span{
    margin-left: 10px;
}
.user_illSendPatient{
  overflow: hidden;
  overflow-y: scroll;
  height: 67vh;
  width: 100%;
  // background-color: rgba(32, 32, 32,0.75);
  .item{
      .user_photo{
        background-color: #fff;
        border-radius: 50%;
        width: 45px;
        height: 45px;
        overflow: hidden;
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
      .rang{
        color: rgb(66, 167, 255);
        font-weight: bold;
      }
      &:hover{
        cursor: pointer;
        box-shadow: 2px 2px 5px rgb(224, 224, 224);
        background-color: rgb(215, 242, 255);
        transform: translate(6px, 0px);
        transition: all 0.1s ease-in-out;
      }
    }
  
}
.activeUser{
  cursor: pointer;
  box-shadow: 2px 2px 5px rgb(224, 224, 224);
  background-color: rgb(179, 230, 255);
  transform: translate(6px, 0px);
  transition: all 0.1s ease-in-out;
}
</style>