<template>
  <div>
    <loader v-if="loading"/>
    <div class="border-bottom ">
      <router-link to="/contragent">
         <h5 class="m-0 ml-3 d-flex" style="padding: 16px 0px;">
           <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-chevron-left mr-2" width="24" height="24" viewBox="0 0 24 24" stroke-width="2.5" stroke="#007BFF" fill="none" stroke-linecap="round" stroke-linejoin="round">
              <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
              <polyline points="15 6 9 12 15 18" />
            </svg>
            {{$t('Add_contragent')}}</h5>
      </router-link>
     
    </div>
    <div class="contragent_add p-5">
      <form @submit.prevent="submit">
        <mdb-row>
          <mdb-col col="3">
            <p class="p-0 m-0 mt-2" style="font-size: 14px;">{{$t('name')}}</p>
          </mdb-col>
          <mdb-col col="5">
            <mdb-input class="m-0 p-0" v-model="name" size="md"  outline  group type="text" validate error="wrong" success="right"/>
            <small class="invalid-text pt-4" style="margin-left:5px; "  v-if="$v.name.$dirty && !$v.name.required" >
              {{$t('name_invalid_text')}}
            </small>
            <!-- <small class="p-0" style="margin-left:5px; font-size: 12px; color: gray;"  v-else >
              {{$t('write_district_name_full')}}
            </small> -->
          </mdb-col>
        </mdb-row> 
         <mdb-row class="mt-3">
          <mdb-col col="3">
            <p class="p-0 m-0 mt-2" style="font-size: 14px;">{{$t('phoneNumber')}}</p>
          </mdb-col>
          <mdb-col col="5">
            <!-- <mdb-input class="m-0 p-0" v-model="phoneNumber" placeholder="+998" size="md" outline  group type="text" validate error="wrong" success="right"/> -->
            <input type="text"  class="w-100 input_style" v-mask="'+998 (##) ###-##-##'" placeholder="+998 (__) ___-__-__" 
            v-model="phoneNumber" />
          </mdb-col>
        </mdb-row>
        <mdb-row class="mt-3">
          <mdb-col col="3">
            <p class="p-0 m-0 mt-2" style="font-size: 14px;">{{$t('passport_number')}}</p>
          </mdb-col>
          <mdb-col col="5">
            <input
              id="passport"
              v-mask="'AA#######'"
              v-model="passport_number"
              placeholder="AA1234567"
              class="w-100 input_style"
            />
            <!-- <mdb-input class="m-0 p-0" v-model="passport_number" size="md" outline  group type="text" validate error="wrong" success="right"/> -->
          </mdb-col>
        </mdb-row>

        <mdb-row class="mt-3">
          <mdb-col col="3">
            <p class="p-0 m-0 mt-2" style="font-size: 14px;">{{$t('car_number')}}</p>
          </mdb-col>
          <mdb-col col="5">
            <mdb-input class="m-0 p-0" v-model="car_number" size="md" outline  group type="text" validate error="wrong" success="right"/>
          </mdb-col>
        </mdb-row>

        <mdb-row class="mt-4" v-show="false">
          <mdb-col col="3">
            <p class="p-0 m-0 mt-2" style="font-size: 14px;">{{$t('contragent_company_name')}}</p>
          </mdb-col>
          <mdb-col col="5">
            <mdb-input class="m-0 p-0" v-model="contragent_company_name" size="md" outline  group type="text" validate error="wrong" success="right"/>
          </mdb-col>
        </mdb-row>

        <mdb-row class="mt-3">
          <mdb-col col="3">
            <p class="p-0 m-0 mt-2" style="font-size: 14px;">{{$t('address')}}</p>
          </mdb-col>
          <mdb-col col="5">
            <mdb-input class="m-0 p-0" v-model="address" size="md" outline  group type="text" validate error="wrong" success="right"/>

          </mdb-col>
        </mdb-row>

        <mdb-row class="mt-3">
          <mdb-col col="3">
            <p class="p-0 m-0 mt-2" style="font-size: 14px;">{{$t('note')}}</p>
          </mdb-col>
          <mdb-col col="5">
            <mdb-input class="m-0 p-0" v-model="note" size="sm" outline group type="textarea" validate error="wrong" success="right"/>
          </mdb-col>
        </mdb-row>
        

        
        <mdb-row class="mt-3" v-show="false">
          <mdb-col col="3">
            <p class="p-0 m-0 mt-2" style="font-size: 14px;">{{$t('district')}}</p>
          </mdb-col>
          <mdb-col col="5" style="position: relative;">
              <erpSelect
                :options="all_district_t.rows"  
                @select="selectOption"
                :selected="district_name"
                :label="$t('district')"
              />
              
          </mdb-col>
        </mdb-row>

        
        <mdb-row class="mt-3">
          <mdb-col col="3">
            <p class="p-0 m-0 mt-2" style="font-size: 14px;">{{$t('picture')}}</p>
          </mdb-col>
          <mdb-col col="5" style="position: relative;">
            <div class="d-flex justify-content-start align-items-center">
              <!-- v-show="!PicShow" -->
              <div class="contragent_img" v-show="!PicShow" style="position: relative;">
                <img  :src="hostname + image"  id="prewImage" style="height: 150px;" class="img-fluid img-thumbnail img-responsive shadow" alt="">
                
                <div @click="delImage">
                  <svg xmlns="http://www.w3.org/2000/svg"  class="icon icon-tabler icon-tabler-x border p-1" style="border-radius:50%;
                    position: absolute; top:8px; right: 8px; cursor:pointer;" width="25" height="25" viewBox="0 0 24 24" stroke-width="2.5" stroke="#ff2825" fill="none" stroke-linecap="round" stroke-linejoin="round">
                    <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                    <line x1="18" y1="6" x2="6" y2="18" />
                    <line x1="6" y1="6" x2="18" y2="18" />
                  </svg>
                </div>
              </div>
            </div>
            <input type="file" style="display: none;" id="file_download" @change="onFileChange" accept="image/*" />
                <label for="file_download" class="download">
                  <span>📁 Rasm yuklash</span>
                </label>
          </mdb-col>
        </mdb-row>

                           

        <div class="blue-gradient">
          <hr class="mt-3 mb-2"/>
        </div>
        <mdb-row class="mt-1">
          <mdb-col col="8">
            <div class="mt-1 text-right">
          <mdb-btn  @click="submit_continue" color="primary" m="r3" style="font-size: 9px"
            p="r4 l4 t2 b2">  {{$t('Add_and_continue')}}
          </mdb-btn>
          <mdb-btn color="success"  type="submit" style="font-size: 9px"
            p="r4 l4 t2 b2">
            <mdb-icon  />{{$t('add')}}</mdb-btn>
        </div>
          </mdb-col>
        </mdb-row>
        
      </form>
      <massage_box :hide="modal_status" :detail_info="modal_info"
      :m_text="$t('Failed_to_add')" @to_hide_modal="modal_status= false"/>

      <Toast ref="message"></Toast>
    </div>
  </div>
</template>

<script>
// import md5 from 'js-md5'
import erpSelect from "../../components/erpSelect.vue";
import { mdbInput, mdbRow, mdbCol, mdbIcon, mdbBtn, } from "mdbvue"
import { required } from 'vuelidate/lib/validators'
import {mapActions,mapGetters} from 'vuex'

export default {
  naem: "companyAdd",
  
  data(){
    return{
      hostname: this.$store.state.server_ip,
      modal_info: '',
      modal_status: false,
      loading: false,
      id: this.$route.params.id,
      name: '',
      phoneNumber: '',
      address: '',
      alert_danger: false,
      PicShow: true,
      base64: '',
      image: 'ddd',
      district_name: '',
      district_id: null,
      note: '',
      passport_number: '',
      car_number: '',
      contragent_company_name: '',

    }
  },
  components: {
    mdbInput, mdbRow, mdbCol, mdbIcon, mdbBtn, erpSelect
  },
  validations: {
      name: {required},
      phoneNumber: {required},
    },
     props :
    {
      props_name : {
        type : String,
        default : ''
      },
      
    },
    async created()
    {
      if(this.props_name.length > 0)
      {
          this.name = this.props_name; 
          this.id = 0;
          this.PicShow = true;
         return 
      }
      if(this.id > 0)
      {
        const res = await fetch(this.$store.state.hostname + '/TegirmonContragent/' + this.id);
        const data = await res.json();
        this.name = data.name;
        this.phoneNumber = data.phone_number;
        this.address = data.adddress;
        this.note = data.note;
        this.car_number = data.car_number;
        this.district_id = data.tegirmonDepartmentid;
        this.district_name = data.district_name;
        this.passport_number = data.passport_number;
        this.contragent_company_name = data.contragent_company_name;
        if(data.image_url){
          this.PicShow = false;
          this.image = data.image_url;
        }
      }

    },
    mounted(){
      this.fetch_district_t();
    },
    computed: mapGetters(['all_district_t']),

  methods:{
    ...mapActions(['fetch_district_t']),
    selectOption(option){
      this.district_name = option.name
      this.district_id = option.id
    },
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
        // this.imageBase64 = e.target.result;
      };
      reader.readAsDataURL(file);
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
        console.log(response)
        const data = await response.json();
        console.log(data)
        if(response.status == 201 || response.status == 200)
        {
          console.log('data.image_url_str', data.image_url_str)
          this.image = data.image_url_str;
          this.PicShow = false;

        }
        else{
          this.image = '';
        }
      }
      catch{
        this.image = '';
      }
    },
    
    cls_wnd()
      {
        this.name = '';
        this.phoneNumber = '';
        this.address = '';
        this.note ='';
        this.district_name = '';
        this.district_id = null;
        this.base64 = '';
        this.image_base_64 = '';
        this.PicShow = true;
        this.passport_number = '';
        this.car_number = '';
        this.contragent_company_name ='';
      },
    save_data :  async function(){
      // var img = document.getElementById('prewImage');
      //   if(img.src.slice(0,4) != 'http'){
      //     this.base64 = img.src;
      //   }
        // console.log(this.born_date)
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
                "name" : this.name,
                "phone_number": this.phoneNumber,
                "adddress": this.address,
                "tegirmonDistrictid": this.district_id,
                "image_url": this.image,
                "note": this.note,
                "passport_number": this.passport_number,
                "car_number": this.car_number,
                "contragent_company_name": this.contragent_company_name,
                // "image_url": this.image_url,
                // "passport_image_base_64": passport_image_base_64,
                // "passport_image_url": passport_image_url,
                "id" : this.id,
              })
            };
            try{
              this.loading = true;
              const response = await fetch(this.$store.state.hostname + "/TegirmonContragent", requestOptions);
              const data = await response.text();
              this.loading = false;
              if(response.status == 201 || response.status == 200)
              {
                this.$refs.message.success('Added_successfully')
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
    async submit()
      {
        if(await this.save_data())
        {
          this.$store.state.alert = true;
          this.$router.push("/contragent");
        }

      },
    async submit_continue()
      {
        if(await this.save_data())
        {
          if(this.id != 0){
            this.$router.push('/contragent_add/0')
            this.cls_wnd();
          }
          else{
            this.cls_wnd();
          }
        }
      },
  }
}
</script>

<style lang="scss">
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
.input_style{
  height: 37px;
  border: 1px solid #DADCE0;
  border-radius: 3px;
  padding: 5px 10px;
  color: #4A5057;
}
.input_style:focus{
   border: 2px solid #5585F0;
  // border-color: #5585F0;
  outline: none;
}
</style>