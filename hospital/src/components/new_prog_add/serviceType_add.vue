<template>
     
<div>
    <loader v-if="loading"/>
    <form v-on:submit.prevent="submit">
      <div class="addxizmat px-2 ">
        <div class="row">
          <div class="col-12">
            <mdb-input label="Наименование услуги" v-model="name" outline/>
            <small class="invalid-text"  v-if="$v.name.$dirty && !$v.name.required" >
              {{$t('name_invalid_text')}}
            </small>
            <mdb-input label="Цена" class="mt-3" v-model="price" type="number" outline/>
            <small class="invalid-text"  v-if="$v.price.$dirty && !$v.price.required" >
              {{$t('enter_price')}}
            </small>

            <erpSelect
                :options = "get_service_group_list"  @btn_add="addGroup = true"
                @select="select_service_Group"
                :selected="group_name"
                :label="$t('service_group')"
                class="mt-2"
              />
              <small v-if="$v.group_name.$dirty && group_id == null" class="invalid-text pt-2" >
                  {{$t('select_item')}}
              </small>

              <erpSelectAdd
                :options = "get_lab_group_list.rows" :btnshow="false"
                @select="onS_labGroup"
                :selected="labGroup_name"
                :label="$t('labGroup')"
                @addTag="addTagLab_group"
              />
              <erpSelectTitle
                :options = "get_lab_main_list"  :btnshow="false"
                @select="select_lab_link"
                :selected="$t(labLink_name)"
                :label="('Ссылка на лабораторию')"
                class="mt-2"
              />
            <div class="row">
              <div class="custom-control custom-switch col-4 pl-5 text-center">
                  <input v-model="status" type="checkbox" class="custom-control-input " id="customSwitch1" checked>
                  <label class="custom-control-label " for="customSwitch1">{{$t('persentage')}}</label>
              </div>
              <div class="col-8">
                <mdb-input :label="$t('contragent_percint')" class="m-0 p-0" v-show="status" v-model="persint" type="number" outline/>
              </div>
            </div>
            <mdb-input :label="$t('contragent_price')" class="mt-3" v-show="!status" v-model="contragent_price" type="number" outline/>
            <div class="d-flex justify-content-between">
              <div class="custom-control custom-switch ml-2">
                <input v-model="unpaidToContragent" type="checkbox" class="custom-control-input " id="customSwitch2" checked>
                <label class="custom-control-label " for="customSwitch2">{{$t('paidtoContragent')}}</label>
              </div>
              <div class="custom-control custom-switch ml-2">
                <input v-model="serviceAdditional" type="checkbox" class="custom-control-input " id="customSwitch5" checked>
                <label class="custom-control-label " for="customSwitch5">
                  <img src="../../assets/ser_add.png" class="ml-1" height="20px" alt="">
                </label>
              </div>
            </div>
            
          </div>
          
          <div class="text-right container" >
            <mdb-btn color="success px-4 py-2" type="submit" style="margin-top:20px; font-size:10px;">{{$t('add')}}</mdb-btn>
          </div>
        </div>
      </div> 
    </form>
  <Toast ref="message"></Toast>
  <!-- <mdb-modal  :show="addGroup"  @close="addGroup = false" size="lg" info>
      <mdb-modal-header>
        <mdb-modal-title>{{$t('service_group')}}</mdb-modal-title>
      </mdb-modal-header>
      <mdb-modal-body>
           <serviceGroup/>
      </mdb-modal-body>
    </mdb-modal>   -->

    <ModalGroup  :title="$t('service_group')" closeColor="#fff" titlecolor="white" headerbackColor="info"
        :show="addGroup" @close="addGroup = false" width="510px">
        <template v-slot:body>
          <serviceGroup  @close="addGroup = false"/>
        </template>
      </ModalGroup>



    
  
</div>
</template>

<script>
  import { mdbBtn, mdbInput  } from 'mdbvue';
import ModalGroup from '../modal'
  import serviceGroup from "../../components/hospital/serviceGroup"
import erpSelect from "../../components/erpSelect";
import erpSelectTitle from "../../components/erpSelectTitle";
import erpSelectAdd from "../erpSelectAdd";

import { required } from 'vuelidate/lib/validators'
import {mapActions, mapGetters} from 'vuex'
// import jsonLabMenu from '../../json/Iymon.json'

export default {
  components: {
      mdbBtn, erpSelect,
      mdbInput,ModalGroup,
      serviceGroup, erpSelectAdd,
      erpSelectTitle
    },
     validations: {
      name: {required},
      price: {required},
      group_name: {required}
    },
    data(){
      return{
        name: '',
        price: '',   
        adress: '',     
        id: 0,
        group_id: null,
        group_name: ' ',
        status: false,
        unpaidToContragent: true,
        serviceAdditional: false,
        addGroup: false,
        contragent_price: 0,
        persint: 0,
        link_str: '',
        loading: false,
        labGroup_name: ' ',
        labGroup_id: null,
        labLink: [],
        labLink_name: ' ',
        labLink_link: null,
      }
    },
    props:{
     options:{
        type: Object,
        default(){
          return {}
        }
      }
  },
  computed: mapGetters(['get_service_group_list', 'get_lab_group_list', 'get_lab_main_list']),
  async mounted(){
    await this.options;
    await this.fetch_service_group();
    await this.fetch_lab_group();
    await this.fetch_lab_main_list();
    console.log('this.get_lab_main_list')
    console.log(this.get_lab_main_list)
    if(Object.keys(this.options).length != 0){
      this.id = this.options.id;
      this.name = this.options.name;
      this.price = this.options.price;
      this.group_id = this.options.groupId;
      this.group_name = this.options.groupSer;
      this.status = this.options.calculateWithPersentage;
      this.paymentable = this.options.paymentable;
      this.contragent_price = this.options.contragentPrice;
      this.persint = this.options.persantage;
      if(this.options.link_str == '1'){
        this.serviceAdditional = true;
      }
      this.labGroup_id  = this.options.link_str1;
      this.labGroup_name  = this.options.link_str2;
      this.labLink_link  = this.options.link_str3;
      this.labLink_name  = this.options.link_str4;

    }
    else{
      this.cls_wnd();
    }
  },
    methods: {
      ...mapActions(['fetch_service_type','fetch_service_group', 'fetch_lab_group','fetch_lab_main_list']),
      cls_wnd(){
        this.name = '';
        this.price = '';
        this.id = 0;
        // this.group_id = null;
        // this.group_name = '';
        this.link_str = ''
      },
      onS_labGroup(option){
        this.labGroup_name = option.name_1;
        this.labGroup_id = option.id
        console.log(option)
      },
      select_lab_link(option){
        console.log(option)
        this.labLink_name = option.name;
        this.labLink_link = option.id;
      },
      select_service_Group(option){
        this.group_name = option.name;
        this.group_id = option.id;
      },
      async submit(){
        if(this.serviceAdditional == true){
          this.link_str = '1'
        }
        else{
          this.link_str = ''
        }
        if(this.labGroup_id == null){
          this.labGroup_id = '';
        }
        else{
          this.labGroup_id = this.labGroup_id.toString();
        }
          if(this.$v.$invalid )
          {
            this.$v.$touch();
            this.$refs.message.warning('please_fill')
            return false;
          }
           const requestOptions = {
            method: "POST",
            headers : { "Content-Type" : "application/json" },
            body : JSON.stringify({
              "id": this.id,
              "name" : this.name,
              "price": this.price,
              "calculateWithPersentage": this.status,
              "persantage": this.persint,
              "hospitalServiceTypeGroupId": this.group_id,
              "contragentPrice": this.contragent_price,
              "paymentable": this.unpaidToContragent,
              "link_str": this.link_str,
              "link_str1": this.labGroup_id,
              "link_str2": this.labGroup_name,
              "link_str3": this.labLink_link.toString(),
              "link_str4": this.labLink_name,

            })
          };
          this.loading = true;
           const response = await fetch(this.$store.state.hostname + "/ServiceTypes", requestOptions);
            const data = await response.json();
            this.loading = false;
            console.log(data)
          if(data.id){
            this.cls_wnd();
            this.fetch_service_type();
            this.$refs.message.success('Added_successfully')
          }
          else{
            this.$refs.message.success('Added_successfully')
          }
        

      },
      async addTagLab_group(name){
        const requestOptions = {
            method : "POST",
            headers : { "Content-Type" : "application/json" },
            body: JSON.stringify({
              "name_1" : name,
              "id" : 0,
            })

          };
          const response = await fetch(this.$store.state.hostname + "/AnalizGroupInfoes", requestOptions);
          const data = await response.text();
          console.log(response)
          if(response.status == 201 || response.status == 200)
          {
            await this.fetch_lab_group();
            this.$refs.message.success('Added_successfully')
            return true;
          }
          else{
            this.$refs.message.danger(data)
            return false;
          }
      }
    }
    

}
</script>

<style scoped>

</style>