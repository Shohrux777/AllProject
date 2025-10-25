<template>
  <div>
    <loader v-if="loading"/>
    <div class="border-bottom ">
      <router-link to="/product">
         <h5 class="m-0 ml-3 d-flex" style="padding: 16px 0px">
           <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-chevron-left mr-2" width="24" height="24" viewBox="0 0 24 24" stroke-width="2.5" stroke="#007BFF" fill="none" stroke-linecap="round" stroke-linejoin="round">
              <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
              <polyline points="15 6 9 12 15 18" />
            </svg>
            {{$t('Add_product')}}</h5>
      </router-link>
     
    </div>
    <div class="product_add p-5">
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
          </mdb-col>
        </mdb-row> 
         <mdb-row class="mt-4">
          <mdb-col col="3">
            <p class="p-0 m-0 mt-2" style="font-size: 14px;">{{$t('price')}}</p>
          </mdb-col>
          <mdb-col col="5">
            <input class="m-0 p-0 form-control px-3" v-model="price_str" type="text" @keyup="funcPrice($event.target.value)" />
            <!-- <small class="invalid-text pt-4" style="margin-left:5px; "  v-if="$v.price.$dirty && !$v.price.required" >
              {{$t('name_invalid_text')}}
            </small>
            <small class="p-0" style="margin-left:5px; font-size: 12px; color: gray;"  v-else >
              {{$t('write_company_name_to_chek')}}
            </small> -->
          </mdb-col>
        </mdb-row>
        <mdb-row class="mt-4">
          <mdb-col col="3">
            <p class="p-0 m-0 mt-2" style="font-size: 14px;">Оптом цена</p>
          </mdb-col>
          <mdb-col col="5">
            <div class="optom_price mt-2" v-for="(item,i) in productPrices" :key="i">
              <div class="add_icon" @click="addProductPrice">
                <i class="fas fa-plus-square text-success" style="font-size: 18px;"></i>
              </div>
              <div class="delete_icon" v-if="productPrices.length>1" @click="productPrices.splice(i,1)">
                <i class="fas fa-trash-alt text-danger" style="font-size: 14px;"></i>
              </div>
              <div class="row mt-2">
                <div class="col-12 mb-2">
                  <mdb-input
                    class="m-0 p-0"
                    v-model="item.name"
                    size="sm"
                    outline
                    type="text"
                  />
                  <small
                    style="position: absolute; top: -10px; left: 20px; font-size: 11px"
                    class="bg-white px-2 py-0"
                    >{{ $t("name") }}</small
                  >
                </div>
                <div class="col-4">
                  <mdb-input
                    class="m-0 p-0"
                    v-model="item.qty"
                    size="sm"
                    outline
                    type="number"
                  />
                  <small
                    style="position: absolute; top: -10px; left: 20px; font-size: 11px"
                    class="bg-white px-2 py-0"
                    >{{ $t("qty") }}</small
                  >
                </div>
                <div class="col-4 px-1">
                  <input
                    class="m-0 p-0 form-control px-2"
                    v-model="item.price_str"
                    @keyup="funcItemPrice($event.target.value, i)"
                    style="height: 26px; font-size: 12px;"
                    type="text"
                  />
                  <small
                    style="position: absolute; top: -10px; left: 10px; font-size: 11px"
                    class="bg-white px-2 py-0"
                    >{{ $t("price") }}</small
                  >
                </div>
                <div class="col-2">
                  <div class="custom-control custom-switch  pl-4">
                    <input v-model="item.status_uzs" type="checkbox" class="custom-control-input " :id="i"  @change="updateStatusNum(i)">
                    <label class="custom-control-label status-style " :for="i" style="font-size: 14.5px; padding-top:2px;">UZS</label>
                  </div>
                </div>
                <div class="col-2">
                  <div class="custom-control custom-switch  pl-4">
                    <input v-model="item.status_usd" type="checkbox" class="custom-control-input " :id="i"  @change="updateStatusNum1(i)">
                    <label class="custom-control-label status-style " :for="i" style="font-size: 14.5px; padding-top:2px;">USD</label>
                  </div>
                </div>
              </div>
            </div>
          </mdb-col>
        </mdb-row>

        <mdb-row class="mt-4">
          <mdb-col col="3">
            <p class="p-0 m-0 mt-2" style="font-size: 14px;">{{$t('code')}}</p>
          </mdb-col>
          <mdb-col col="5">
            <mdb-input class="m-0 p-0" v-model="code" size="md" outline  group type="text" validate error="wrong" success="right"/>
          </mdb-col>
        </mdb-row>
        <mdb-row class="mt-4">
          <mdb-col col="3">
            <p class="p-0 m-0 mt-2" style="font-size: 14px;">{{$t('color')}}</p>
          </mdb-col>
          <mdb-col col="5">
            <input class="m-0 p-0 form-control" style="height: 25px;" v-model="color" size="md" type="color" />
          </mdb-col>
        </mdb-row>
        <mdb-row class="mt-4">
          <mdb-col col="3">
            <p class="p-0 m-0 mt-2" style="font-size: 14px;">{{$t('auth_user_updator_id')}}</p>
          </mdb-col>
          <mdb-col col="5">
            <mdb-input class="m-0 p-0" v-model="number" size="md" outline  group type="number" validate error="wrong" success="right"/>
          </mdb-col>
        </mdb-row>

        <!-- <mdb-row class="mt-4">
          <mdb-col col="3">
            <p class="p-0 m-0 mt-2" style="font-size: 14px;">{{$t('address')}}</p>
          </mdb-col>
          <mdb-col col="5">
            <mdb-input class="m-0 p-0" v-model="address" size="md" outline  group type="text" validate error="wrong" success="right"/>
          </mdb-col>
        </mdb-row> -->
        
        <mdb-row class="mt-4">
          <mdb-col col="3">
            <p class="p-0 m-0 mt-2" style="font-size: 14px;">{{$t('measurment')}}</p>
          </mdb-col>
          <mdb-col col="5" style="position: relative;">
              <erpSelect
                :options="allMeasurment.rows"  
                @select="selectOption"
                :selected="measurment_name"
                :label="$t('measurment')"
              />
              <small v-if="$v.measurment_name.$dirty && measurment_id == null" class="invalid-text mt-0 ml-2" >
                  {{$t('select_item')}}
                </small>
          </mdb-col>
        </mdb-row>

        <mdb-row class="mt-4">
          <mdb-col col="3">
            <p class="p-0 m-0 mt-2" style="font-size: 14px;">KG</p>
          </mdb-col>
          <mdb-col col="5">
            <mdb-input class="m-0 p-0" v-model="numkg" size="md" outline  group type="text" validate error="wrong" success="right"/>
          </mdb-col>
        </mdb-row>

        <mdb-row class="mt-4">
          <mdb-col col="3">
            <p class="p-0 m-0 mt-2" style="font-size: 14px;">{{$t('status')}}</p>
          </mdb-col>
          <mdb-col col="5" style="position: relative;">
              <div class="custom-control custom-switch  pl-5">
                <input v-model="status" type="checkbox" class="custom-control-input " id="customSwitch1" checked>
                <label class="custom-control-label status-style" for="customSwitch1"></label>
            </div>
          </mdb-col>
        </mdb-row>
        <mdb-row class="mt-4">
          <mdb-col col="3">
            <p class="p-0 m-0 mt-2" style="font-size: 14px;">UZS статус</p>
          </mdb-col>
          <mdb-col col="5" style="position: relative;">
              <div class="custom-control custom-switch  pl-5">
                <input v-model="uzs_status" type="checkbox" class="custom-control-input " id="customSwitch2" checked>
                <label class="custom-control-label status-style" for="customSwitch2"></label>
              </div>
          </mdb-col>
        </mdb-row>



        
        <mdb-row class="mt-4">
          <mdb-col col="3">
            <p class="p-0 m-0 mt-2" style="font-size: 14px;">{{$t('picture')}}</p>
          </mdb-col>
          <mdb-col col="5" style="position: relative;">
            <div class="d-flex justify-content-start align-items-center">
              <!-- v-show="!PicShow" -->
              <div class="product_img" v-show="!PicShow" style="position: relative;">
                <img  :src="image" id="prewImage" style="height: 150px;" class="img-fluid img-thumbnail img-responsive shadow" alt="">
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
            <input hidden  id="inputFileToLoad" @change="previewFile()" accept="image/png, image/gif, image/jpeg" type="file" ref="file_Img" class="shadow text-right ml-2"  />
              <label for="inputFileToLoad" class="download">
                <span>{{$t('download...')}}</span>
              </label>
          </mdb-col>
        </mdb-row>

                           

        <div class="blue-gradient">
          <hr class="mt-5 "/>
        </div>
        <mdb-row class="mt-4">
          <mdb-col col="8">
            <div class="mt-2 text-right">
          <mdb-btn  @click="submit_continue" color="primary" m="r3" style="font-size: 10.5px"
            p="r4 l4 t2 b2">  {{$t('Add_and_continue')}}
          </mdb-btn>
          <mdb-btn color="success"  type="submit" style="font-size: 10.5px"
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
      modal_info: '',
      modal_status: false,
      loading: false,
      id: this.$route.params.id,
      name: '',
      price: '',
      price_str: '',
      alert_danger: false,
      PicShow: true,
      base64: '',
      image: '',
      measurment_name: '',
      measurment_id: null,
      code: '',
      status: false,
      color: '#ffffff',
      uzs_status: false,
      number: 0,
      numkg:0,
      productPrices: [
        {
          name: '',
          qty: null,
          price: null,
          price_str: '',
          type: 1,
          measure_str: 'UZS',
          status_uzs: true,
          status_usd: false,

        }
      ],
    //address: '',

    }
  },
  components: {
    mdbInput, mdbRow, mdbCol, mdbIcon, mdbBtn, erpSelect
  },
  validations: {
      name: {required},
      price: {required},
      measurment_name : {required},
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
        const res = await fetch(this.$store.state.hostname + '/TegirmonProduct/' + this.id);
        const data = await res.json();
        console.log('this is by id')
        console.log(data)
        this.name = data.name,
        this.price = data.price,
        this.price_str = data.price.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 '),
        this.measurment_id = data.tegirmonUnitMeasurmentid,
        this.measurment_name = data.measurment_name,
        this.image = this.$store.state.server_ip +  data.image_base_64,
        this.base64 = data.image_base_64,
        this.code = data.code,
        this.status = data.inv_accepted_status;
        this.color = data.shitrix_code;
        this.number = data.auth_user_updator_id;
        if(data.productPrices.length>0){
          this.productPrices = [];
          for(let i=0; i<data.productPrices.length; i++){
            let tmp = {
              name: data.productPrices[i].name,
              qty: data.productPrices[i].qty,
              price: data.productPrices[i].price,
              price_str: data.productPrices[i].price.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 '),
              type: data.productPrices[i].type,
              measure_str: data.productPrices[i].measure_str,
              id: 0,
              status_uzs: true,
              status_usd: false,
            }
            if(data.productPrices[i].type == 1){
              tmp.status_uzs = true;
              tmp.status_usd = false;
            }
            else{
              tmp.status_uzs = false;
              tmp.status_usd = true;
            }
            this.productPrices.push(tmp);
          }
        }
        if(data.auth_user_creator_id == 0){
          this.uzs_status = false;
        }
        else{
          this.uzs_status = true;
        }
        this.numkg = data.real_qty

        // this.address = data.adddress,
        this.PicShow = false;
        await this.MeasureName(this.measurment_id);
      }

    },
    mounted(){
      this.fetchMeasurment();
    },
    computed: mapGetters(['allMeasurment']),

  methods:{
    ...mapActions(['fetchMeasurment']),
    async addProductPrice(){
      let temp = {
        name: '',
        qty: null,
        price: null,
        type: 1,
        measure_str: 'UZS',
        status_uzs: true,
        status_usd: false,
      };
      this.productPrices.push(temp)
    },
    async updateStatusNum(i){
      if(this.productPrices[i].status_uzs == true){
        this.productPrices[i].status_usd = false;
        this.productPrices[i].type = 1;
      }
      else{
        this.productPrices[i].status_usd = true;
        this.productPrices[i].type = 2;
      }
    },
    async updateStatusNum1(i){
      if(this.productPrices[i].status_usd == true){
        this.productPrices[i].status_uzs = false;
        this.productPrices[i].type = 2;
      }
      else{
        this.productPrices[i].status_uzs = true;
        this.productPrices[i].type = 1;
      }
    },
    async MeasureName(m_id){
      try{
        const res = await fetch(this.$store.state.hostname + '/TegirmonUnitMeasurment/' + m_id);
        if(res.status == 200 || res.status == 201){
          const data = await res.json();
          this.measurment_name = data.name;
        }
      }
      catch(error){
        console.log(error);
      }
    },
    selectOption(option){
      this.measurment_name = option.name
      this.measurment_id = option.id
    },
    delImage(){
      this.base64 = ''
      this.PicShow = true
      this.image = ''
    },
    async previewFile(){
      const preview = document.getElementById('prewImage');
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
    cls_wnd()
      {
        this.name = '';
        this.price = '';
        this.measurment_name = '';
        this.measurment_id = null;
        this.base64 = '';
        this.image_base_64 = '';
        this.PicShow = true;
        this.code ='';
        // this.address = '';
      },
    save_data :  async function(){
      if(this.productPrices[0].price == null || this.productPrices[0].qty == null 
      || this.productPrices[0].price == 0 || this.productPrices[0].qty == 0){
        this.productPrices = [
          {
            name: '1 ' + this.measurment_name,
            qty: 1,
            price: this.price,
            type: 1,
            measure_str: 'UZS',
            status_uzs: true,
            status_usd: false,
          }
        ];
      }
      var img = document.getElementById('prewImage');
        if(img.src.slice(0,4) != 'http'){
          this.base64 = img.src;
        }
        // console.log(this.born_date)
        if(this.$v.$invalid)
          {
            this.$v.$touch();
            this.$refs.message.warning('please_fill')
            return false;
          }
        else{ 
          let uzs_num = 0;
          if(this.uzs_status == true){
            uzs_num = 1;
          }
          this.alert_danger = false;
          const requestOptions = {
              method : "POST",
              headers: { "Content-Type" : "application/json" },
              body: JSON.stringify({
                "name" : this.name,
                "price": this.price,
                "tegirmonUnitMeasurmentid": this.measurment_id,
                "image_base_64": this.base64,
                "code": this.code,
                "inv_accepted_status": this.status,
                "shitrix_code": this.color,
                "auth_user_creator_id": uzs_num,
                "auth_user_updator_id": this.number,
                "productPrices": this.productPrices,
                "real_qty": this.numkg,
                // "adddress": this.address,
                // "image_url": this.image_url,
                "id" : this.id,
              })
            };
            try{
              this.loading = true;
              const response = await fetch(this.$store.state.hostname + "/TegirmonProduct", requestOptions);
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
          this.$router.push("/product");
        }

      },
    async submit_continue()
      {
        if(await this.save_data())
        {
          if(this.id != 0){
            this.$router.push('/product_add/0')
            this.cls_wnd();
          }
          else{
            this.cls_wnd();
          }
        }
      },
    funcPrice(n){
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
       this.price_str = tols.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
       var temp = ''
       for(let i=0; i<this.price_str.length; i++){
        if(this.price_str[i] != ' '){
          temp += this.price_str[i];
        }
       }
      this.price = parseFloat(temp);
    },
    funcItemPrice(n,k){
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
       this.productPrices[k].price_str = tols.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
       var temp = ''
       for(let i=0; i<this.productPrices[k].price_str.length; i++){
        if(this.productPrices[k].price_str[i] != ' '){
          temp += this.productPrices[k].price_str[i];
        }
       }
      this.productPrices[k].price = parseFloat(temp);
    }
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
.optom_price{
  position: relative;
  width: 100%;
  border: 1px solid #4285F4;
  padding: 5px;
  border-radius: 5px;
  .add_icon{
    position: absolute;
    right: -30px;
    padding: 5px;
    cursor: pointer;
  }
  .delete_icon{
    position: absolute;
    top: 30px;
    right: -28px;
    padding: 5px;
    cursor: pointer;
  }
}
</style>