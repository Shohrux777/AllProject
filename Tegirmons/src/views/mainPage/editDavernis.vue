<template>
  <div class="add_davernis p-4" v-if="status_active">
    <mdb-row>
      <mdb-col col="4">
        <p class="p-0 m-0 mt-2" style="font-size: 14px;">{{$t('fio')}}</p>
      </mdb-col>
      <mdb-col col="8">
        <mdb-input class="m-0 p-0" v-model="name" size="md"  outline  group type="text" validate error="wrong" success="right"/>
        <small class="invalid-text pt-4" style="margin-left:5px; "  v-if="$v.name.$dirty && !$v.name.required" >
          {{$t('name_invalid_text')}}
        </small>
      </mdb-col>
    </mdb-row>

    <mdb-row class="mt-3">
      <mdb-col col="4">
        <p class="p-0 m-0 mt-2" style="font-size: 14px;">{{$t('phone_number')}}</p>
      </mdb-col>
      <mdb-col col="8">
        <mdb-input class="m-0 p-0" v-model="phone_number" size="md" outline  group type="text" validate error="wrong" success="right"/>
        <!-- <small class="invalid-text pt-4" style="margin-left:5px; "  v-if="$v.phone_number.$dirty && !$v.phone_number.required" >
          {{$t('name_invalid_text')}}
        </small> -->
        <!-- <small class="p-0" style="margin-left:5px; font-size: 12px; color: gray;"  v-else >
          {{$t('write_company_name_to_chek')}}
        </small> -->
      </mdb-col>
    </mdb-row>
    <mdb-row class="mt-3">
      <mdb-col col="4">
        <p class="p-0 m-0 mt-2" style="font-size: 14px;">{{$t('passport_number')}}</p>
      </mdb-col>
      <mdb-col col="8">
        <mdb-input class="m-0 p-0" v-model="passport_number" size="md" outline  group type="text" validate error="wrong" success="right"/>
        <small class="invalid-text pt-4" style="margin-left:5px; "  v-if="$v.passport_number.$dirty && !$v.passport_number.required" >
          {{$t('name_invalid_text')}}
        </small>
      </mdb-col>
    </mdb-row>
    <mdb-row class="mt-3">
      <mdb-col col="4">
        <p class="p-0 m-0 mt-2" style="font-size: 14px;">{{$t('born_date')}}</p>
      </mdb-col>
      <mdb-col col="8">
        <mdb-input class="m-0 p-0" v-model="born_date" size="md" outline  group type="date" validate error="wrong" success="right"/>
      </mdb-col>
    </mdb-row>
    <mdb-row class="mt-3">
      <mdb-col col="4">
        <p class="p-0 m-0 mt-2" style="font-size: 14px;">{{$t('address')}}</p>
      </mdb-col>
      <mdb-col col="8">
        <mdb-input class="m-0 p-0" v-model="address" size="md" outline  group type="text" validate error="wrong" success="right"/>
      </mdb-col>
    </mdb-row>
    <mdb-row class="mt-3">
      <mdb-col col="4">
        <p class="p-0 m-0 mt-2" style="font-size: 14px;">{{$t('picture')}}</p>
      </mdb-col>
      <mdb-col col="8" style="position: relative;">
        <div class="d-flex justify-content-start align-items-center">
          <!-- v-show="!PicShow" -->
          <div class="client_img"  style="position: relative;">
            <img  :src="hostname + image" id="prewImage" style="height: 150px;" class="img-fluid img-thumbnail img-responsive shadow" alt="">
            <div @click="delImage"> 
              <svg xmlns="http://www.w3.org/2000/svg"  class="icon icon-tabler icon-tabler-x border p-1" style="border-radius:50%;
                position: absolute; top:8px; right: 8px; cursor:pointer;" width="25" height="25" viewBox="0 0 24 24" stroke-width="2.5" stroke="#ff2825" fill="none" stroke-linecap="round" stroke-linejoin="round">
                <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                <line x1="18" y1="6" x2="6" y2="18" />
                <line x1="6" y1="6" x2="18" y2="18" />
              </svg>
            </div>
          </div>
            <img :src="hostname + image2" style="height: 150px;" v-show="!PicShow" class="img-fluid img-thumbnail img-responsive shadow" alt="">
            <img :src="hostname + photo_url" v-if="photo_url" v-show="!PicShow" style="height: 150px;" class="img-fluid img-thumbnail img-responsive shadow" alt="">
        </div>
        <input hidden  id="inputFileToLoadPhoto" @change="previewFiled()" accept="image/png, image/gif, image/jpeg" type="file" class="shadow text-right ml-2"  />
          <div class="d-flex">
            <label @click="scanerFile" class="download">
              <span>Изображение паспорта</span>
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

      <div class="blue-gradient">
        <hr class="mt-5 "/>
      </div>
      <mdb-row class="mt-4">
        <mdb-col col="12">
          <div class="mt-2 text-right">
        <mdb-btn color="success"  @click="save_data" style="font-size: 10.5px"
          p="r4 l4 t2 b2">
          <mdb-icon  />{{$t('add')}}</mdb-btn>
      </div>
        </mdb-col>
      </mdb-row>
      <webcam  v-if="showPhoto" @getPhotosub="takePhoto"/>
    <Toast ref="message"></Toast>
  </div>
</template>

<script>
import {mdbRow, mdbCol, mdbInput, mdbBtn, mdbIcon} from 'mdbvue'
import { required } from 'vuelidate/lib/validators'
import webcam from '../webcam/webcam_Add.vue'
export default {
  components:{
    mdbRow, mdbCol, mdbInput, mdbBtn, mdbIcon, webcam
  },
  validations: {
      name: {required},
      // phone_number: {required},
      passport_number: {required},
      // district_name : {required},
      // clientgroup_name : {required},
    },
  data() {
    return {
      hostname: this.$store.state.server_ip,
      name: '',
      phone_number: '',
      passport_number: '',
      address: '',
      note: '',
      showPhoto: false,
      photo_url: '',
      loading: false,

      PicShow: true,
      base64: '',
      image: '',
      image2: '',
      born_date: '',
      id: 0,
      status_active: false,
    }
  },
  props:{
    client_id: {
      type: Number,
      default: null,
    }
  },
  methods: {
    delImage(){
      this.base64 = ''
      this.PicShow = true
      this.image = ''
    },
    async mountedFunc(data){
      this.status_active = false;
     
      this.name = data.fio;
      this.phone_number = data.phone_number;
      this.passport_number = data.passport_number;
      this.address = data.adddress;
      this.status_active = true;
      this.born_date = data.addiotionala_information;
      this.note = data.note;
      this.id = data.id;
      this.photo_url = data.image_url;
      this.image = data.passport_image_base_64;
      this.image2 = data.passport_image_url;
      this.client_id = data.client.id
      this.PicShow = false;

    },
    async previewFiled(){
      // console.log('preview.src')
      const preview = document.getElementById('prewImage');
      const file = document.querySelector('input[type=file]').files[0];
      var reader = new FileReader();
        // console.log(reader)
      reader.addEventListener("load", function () {
        // console.log('preview.src')
        this.image =  reader.result;
        preview.src = reader.result;
        // console.log(preview.src)
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
    clv_ws(){
      this.name = '';
      this.phone_number = '';
      this.passport_number = '';
      this.address = '';
      this.note = '';
      this.base64 = '';
      this.image = '';
      this.image2 = '';
      this.PicShow = true;
      this.photo_url = '';
      this.born_date = '';
      this.showPhoto = false;
      let preview = document.getElementById('prewImage');
      preview.src = '';
    },

    save_data :  async function(){
      // console.log('this.base64')
      // console.log(this.base64)
      if(this.$v.$invalid)
        {
          this.$v.$touch();
          this.$refs.message.warning('please_fill')
          return false;
        }
      
      else{ 
        this.alert_danger = false;
        const requestOptions = {
          method : "POST",
          headers: { "Content-Type" : "application/json" },
          body: JSON.stringify({
            "fio": this.name,
            "adddress": this.address,
            "passport_number": this.passport_number,
            "phone_number": this.phone_number,
            "note": this.note,
            "image_url": this.photo_url,
            "passport_image_base_64": this.image,
            "passport_image_url": this.image2,
            "addiotionala_information": this.born_date,
            "tegirmonClientid": this.client_id,
            "id": this.id,
          })
        };
        // console.log(requestOptions.body)
        try{
          this.loading = true;
          const response = await fetch(this.$store.state.hostname + "/TegirmonClientDavennosts", requestOptions);
          const data = await response.text();
          // console.log(data)
          this.loading = false;
          if(response.status == 201 || response.status == 200)
          {
            this.$refs.message.success('Added_successfully')
            this.clv_ws();
            this.$emit('close')
            return true;
          }
          else{
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
          console.error('Failed: ' + mesg);
          return;
      }

      if(successful && mesg != null && mesg.toLowerCase().indexOf('user cancel') >= 0) { // User canceled.
          console.info('User canceled');
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
          this.image = data.image_url_str;
        }
        else{
          this.image = '';
        }
      }
      catch{
        this.image = '';
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
    }
  },
}
</script>

<style>
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
</style>