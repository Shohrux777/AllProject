<template>
    <div class="main_lab_add">
      <div class="header_lab text-center border-bottom" style="position:relative">
        <!-- <svg @click="$emit('closed')" xmlns="http://www.w3.org/2000/svg" style="position: absolute; left: 10px; top:5px; cursor:pointer;"
          class="icon icon-tabler icon-tabler-chevron-left" width="25" height="25" viewBox="0 0 24 24" stroke-width="2.5" stroke="#00abfb" fill="none" stroke-linecap="round" stroke-linejoin="round">
          <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
          <polyline points="15 6 9 12 15 18" />
        </svg> -->
          <mdb-btn style="position: absolute; left: 10px; top:5px; cursor:pointer; font-size: 9px;" 
            class="px-3 py-1 m-0" color="danger">
            Удалить
          </mdb-btn>
          <h5 class="m-0 p-0 py-2 text-info" style="font-size: 15.7px;">{{$t('Edit')}} лабораторию 
            <!-- <span @click="delLaboratory" class="text-danger" style="cursor:pointer;"> 
              <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-trash-filled" width="20" height="20" viewBox="0 0 24 24" stroke-width="1.5" stroke="#fd0061" fill="none" stroke-linecap="round" stroke-linejoin="round">
                <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                <path d="M20 6a1 1 0 0 1 .117 1.993l-.117 .007h-.081l-.919 11a3 3 0 0 1 -2.824 2.995l-.176 .005h-8c-1.598 0 -2.904 -1.249 -2.992 -2.75l-.005 -.167l-.923 -11.083h-.08a1 1 0 0 1 -.117 -1.993l.117 -.007h16z" stroke-width="0" fill="currentColor" />
                <path d="M14 2a2 2 0 0 1 2 2a1 1 0 0 1 -1.993 .117l-.007 -.117h-4l-.007 .117a1 1 0 0 1 -1.993 -.117a2 2 0 0 1 1.85 -1.995l.15 -.005h4z" stroke-width="0" fill="currentColor" />
              </svg>
            </span> -->
          </h5>
      </div>
      <div class="main_header_lab_added border-bottom px-3" >
        <div class="row pt-4">
          <div class="col-3 m-0 py-0">
              <mdb-input size="sm" class="m-0 py-1" type="text" v-model="main_value.menu_name" label="Название меню" />
          </div>
          <div class="col-3  m-0 py-0">
              <mdb-input size="sm" class="m-0 py-1" type="text" v-model="main_value.blanka_name" label="Название бланка" />
          </div>
          <div class="col-3  m-0 py-0">
              <mdb-input size="sm" class="m-0 py-1" type="text" v-model="main_value.pribor_name" label="Прибор"/>
          </div>
          <div class="col-3  m-0 py-0">
            <mdb-input size="sm"  type="text" class="w-100 m-0 py-1" v-model="main_value.size_top" placeholder="Нужен паспорт" :label="$t('passportSerialNumber')" />
          </div>
          <div class="col-3 m-0 py-0 d-flex">
            <div class="custom-control custom-checkbox" style="margin-top:15px;">
              <input  type="checkbox" class="custom-control-input" v-model="main_value.first_name_s" id="defaultChecked4" checked>
              <label class="custom-control-label" for="defaultChecked4"></label>
            </div>
              <mdb-input size="sm" type="text" class="w-100 m-0 py-1" v-model="main_value.first_name" label="Аббревиатура в начале" />
          </div>
          <div class="col-3 m-0 py-0 d-flex">
            <div class="custom-control custom-checkbox" style="margin-top:15px;">
              <input type="checkbox" class="custom-control-input" v-model="main_value.lab_name_s" id="defaultChecked2" checked>
              <label class="custom-control-label" for="defaultChecked2"></label>
            </div>
              <mdb-input size="sm" type="text" class="w-100 m-0 py-1" v-model="main_value.lab_name" label="Название" />
          </div>
          <div class="col-3 m-0 py-0 d-flex">
            <div class="custom-control custom-checkbox" style="margin-top:15px;">
              <input type="checkbox" class="custom-control-input" v-model="main_value.norma_s" id="defaultChecked3" checked>
              <label class="custom-control-label" for="defaultChecked3"></label>
            </div>
              <mdb-input size="sm" type="text" class="w-100 m-0 py-1" v-model="main_value.norma" label="Норма" />
          </div>
          <div class="col-3 m-0 py-0 d-flex">
            <div class="custom-control custom-checkbox" style="margin-top:15px;">
              <input type="checkbox" class="custom-control-input" v-model="main_value.edzm_s" id="defaultChecked5" checked>
              <label class="custom-control-label" for="defaultChecked5"></label>
            </div>
              <mdb-input size="sm" type="text" class="w-100 m-0 py-1" v-model="main_value.edzm" label="Название измерение" />
          </div>
          <div class="col-3 m-0 py-0 d-flex">
            <div class="custom-control custom-checkbox" style="margin-top:15px;">
              <input type="checkbox" class="custom-control-input" v-model="main_value.center_s" id="defaultChecked6" checked>
              <label class="custom-control-label" for="defaultChecked6"></label>
            </div>
              <mdb-input size="sm" type="text" class="w-100 m-0 py-1" v-model="main_value.center" label="Текстовый центр" />
          </div>
          <div class="col-3 m-0 py-0">
            <mdb-input size="sm" class=" m-0 py-1" type="text" v-model="main_value.anything" label="Название результата" />
          </div>
          <div class="col-3 m-0 py-0 d-flex">
            <div class="custom-control custom-checkbox" style="margin-top:15px;">
              <input type="checkbox" class="custom-control-input" v-model="main_value.tableWithout_s" id="defaultChecked8" checked>
              <label class="custom-control-label" for="defaultChecked8"></label>
            </div>
              <mdb-input  size="sm" type="text" disabled class="w-100 m-0 py-1" v-model="main_value.tableWithout" label="таблица" />
          </div>
          <div class="col-3 m-0 py-0">
          </div>
          
          <div class="col-3 m-0 py-0">
            <mdb-input size="sm" type="text" class="w-100 m-0 py-1" v-model="main_value.bottom_text" placeholder="Нижний текст" label="Нижний текст" />
        </div>
        <div class="col-3 m-0 py-0">
            <mdb-input size="sm"  type="text" class="w-100 m-0 py-1" v-model="main_value.result_info_bottom" placeholder="Нижний текст 2" label="Нижний текст" />
        </div>
        <div class="col-3 m-0 py-0">
            <mdb-input size="sm" type="text" class="w-100 m-0 py-1" v-model="main_value.doctor_name" placeholder="Доктор" label="Нижний текст" />
        </div>
        <div class="col-3 m-0 py-0">
        </div>
        <div class="col-3 m-0 mt-2 py-0">
            <mdb-input  size="sm"  type="text" class="w-100 m-0 py-1" v-model="main_value.info_another_hos" placeholder="" label="Еще одно сообщение из больницы" />
        </div>
        <div class="col-3 m-0 mt-2 py-0">
            <mdb-input  size="sm"  type="text" class="w-100 m-0 py-1" v-model="main_value.info_another_hos_continue" placeholder="" label="Еще одно сообщение из больницы, продолжение" />
        </div>
        </div>
        <div class="row">
          <div class="col-12">
            <accordion>
              <accordion-item>
                <template slot="accordion-trigger">
                  <span style="color:#597e8d; font-size: 13.5px;">Заголовок таблица</span>
                </template>
                <template slot="accordion-content" >
                  <table bordered class="w-100">
                    <tbody>
                      <tr>
                        <td class="px-4 ">
                          <mdb-input size="sm" type="text" class="w-100 m-0 py-1" v-model="table.name1"  />
                        </td>
                        <td class="px-4">
                          <mdb-input size="sm" type="text" class="w-100 m-0 py-1" v-model="table.name2"  />
                        </td>
                      </tr>
                      <tr>
                        <td class="px-4">
                          <mdb-input size="sm" type="text" class="w-100 m-0 py-1" v-model="table.name3"  />
                        </td>
                        <td class="px-4">
                          <mdb-input size="sm" type="text" class="w-100 m-0 py-1" v-model="table.name4"  />
                        </td>
                      </tr>
                      <tr>
                        <td class="px-4">
                          <mdb-input size="sm" type="text" class="w-100 m-0 py-1" v-model="table.name5"  />
                        </td>
                        <td class="px-4">
                          <mdb-input size="sm" type="text" class="w-100 m-0 py-1" v-model="table.name6"  />
                        </td>
                      </tr>
                      <tr>
                        <td class="px-4">
                          <mdb-input size="sm" type="text" class="w-100 m-0 py-1" v-model="table.name7"  />
                        </td>
                        <td class="px-4">
                          <mdb-input size="sm" type="text" class="w-100 m-0 py-1" v-model="table.name8"  />
                        </td>
                      </tr>
                    </tbody>
                  </table>
                </template>
              </accordion-item>
            </accordion>
          </div>
        </div>
        <div class="text-end pb-2">
          <mdb-btn class="py-2 px-3" @click="submitMain" style="font-size: 10px;" color="info">{{$t('Edit')}}</mdb-btn>
        </div>
      </div>
  
      <div class="lab_items_add  px-4" v-if="show_table">
        <div class="d-flex justify-content-center mt-2 border-bottom" >
          <svg xmlns="http://www.w3.org/2000/svg" @click="open_name_update" 
            class="icon icon-tabler icon-tabler-alert-triangle" style="cursor:pointer;"
            width="23" height="23" viewBox="0 0 24 24" stroke-width="1.5" stroke="#ffbf00" fill="none" stroke-linecap="round" stroke-linejoin="round">
            <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
            <path d="M12 9v2m0 4v.01" />
            <path d="M5 19h14a2 2 0 0 0 1.84 -2.75l-7.1 -12.25a2 2 0 0 0 -3.5 0l-7.1 12.25a2 2 0 0 0 1.75 2.75" />
          </svg>
          <span class="ml-3 mb-2" style="color:#ffbf00; font-size: 14px;">{{ main_value.lab_name }}  ning ketma ketligini uzgartirmang oldingi laboratoriya javoblari almashib ketadi</span>
        </div>
        <div class="container-fluid content_lab_item_last card mt-3"  v-for="(i, index) in number_list" :key="index">
          <span v-show="false">{{ i }}</span>
          <!-- v-show="false" -->
          <div class="row mt-3" v-if="main_value.center_s">
            <div class="col-4"></div>
            <div class="col-4">
              <mdb-input  type="text" class="w-100 m-0 py-1 mt-3" size="sm" v-model="center_list[i-1].name4" :label="main_value.center" />
            </div>
            <div class="col-4"></div>
          </div>
          <div class="row">
            <div class="col-1" v-if="main_value.first_name_s" >
              <mdb-input  type="text" class="w-100 m-0 py-1 mt-3" size="sm" v-model="first_name_list[i-1].name" :label="main_value.first_name" />
            </div>
            <div class="col-3" v-if="main_value.lab_name_s" :class="{'applied': show_number > i-1}">
              <mdb-input  type="text" class="w-100 m-0 py-1 mt-3" size="sm" v-model="lab_name_list[i-1].name1" :label="main_value.lab_name" />
            </div>
            <div class="col-3">
              <mdb-input  type="text" class="w-100 m-0 py-1 mt-3" size="sm" v-model="result_name_list[i-1].name5" :label="main_value.anything" />
            </div>
            <div class="col-3" v-if="main_value.norma_s">
              <vue-editor style="width:100%;" class="mb-1" 
                :placeholder="main_value.norma" 
                :editorToolbar="customToolbar"
                v-model="norma_list[i-1].name2"
                ></vue-editor>
              <!-- <mdb-input  type="text" class="w-100 m-0 py-1" size="sm" v-model="norma_list[i-1].name2" :label="main_value.norma" /> -->
            </div>
            <div class="col-2" v-if="main_value.edzm_s">
              <mdb-input  type="text" class="w-100 m-0 py-1 mt-3" size="sm" v-model="edzm_list[i-1].name3" :label="main_value.edzm" />
            </div>
          </div>
          <div class="content_lab_item_last_icons d-flex" v-if="i == number_list">
            <div class="bg-success rounded py-1 px-2 mr-1" v-if = "i < 50" @click="add_item_lab_item">
              <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-plus" width="15" height="15" viewBox="0 0 24 24" stroke-width="2" stroke="#fff" fill="none" stroke-linecap="round" stroke-linejoin="round">
                <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                <line x1="12" y1="5" x2="12" y2="19"/>
                <line x1="5" y1="12" x2="19" y2="12"/>
              </svg>
            </div>
            <div class="bg-danger rounded py-1 px-2 mr-1" v-if="number_list != 1 && show_number < i" @click="remove_item_lab_item(i-1)">
              <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-trash-x" width="15" height="15" viewBox="0 0 24 24" stroke-width="2" stroke="#ffffff" fill="none" stroke-linecap="round" stroke-linejoin="round">
                <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                <path d="M4 7h16"/>
                <path d="M5 7l1 12a2 2 0 0 0 2 2h8a2 2 0 0 0 2 -2l1 -12"/>
                <path d="M9 7v-3a1 1 0 0 1 1 -1h4a1 1 0 0 1 1 1v3" />
                <path d="M10 12l4 4m0 -4l-4 4" />
              </svg>
            </div>
          </div>
        </div>
        <div class="text-end pb-2 border-top mt-4" v-if="main_id != null">
          <mdb-btn class="py-2 px-3" @click="submitItems" style="font-size: 10px;" color="info">{{$t('Edit')}}</mdb-btn>
        </div>
      </div>
      <Toast ref="message"></Toast>
      <massage_box :hide="modal_status" :detail_info="modal_info"
        :m_text="$t('Failed_to_add')" @to_hide_modal = "modal_status= false"/>
    </div>
  </template>
  
  <script>
  import { VueEditor } from "vue2-editor";
  import { mdbInput, mdbBtn } from 'mdbvue';
  import {mapActions, mapGetters} from 'vuex'
  import Accordion from "../../../components/accordion/accordion.vue";
import AccordionItem from "../../../components/accordion/accordion_item_relative.vue";
  export default {
    components: {
      mdbInput,
      mdbBtn, VueEditor,
      Accordion, AccordionItem
    },
    props: {
      main_id:{
        type : Number,
        default : null
      }
    },
    data(){
      return{
        loading : false,
        modal_info : '',
        modal_status: false,
  
        number_list: 50,
        main_value:{
          menu_name: '',
          blanka_name: '',
          pribor_name: '',
          anything: 'Результат',
          lab_name: 'Показатель',
          lab_name_s: true,
          norma: 'Норма',
          norma_s: true,
          first_name: '',
          first_name_s: false,
          edzm: '',
          edzm_s: false,
          center: '',
          center_s: false,
          size_top: '',
          bottom_text: '',
          result_info_bottom: 'Результаты лабораторных исследований не являются, требуется консультация специалиста',
          doctor_name: 'Анализ проводил',
          tableWithout_s:false,
          tableWithout: 'Нет таблица',
          info_another_hos: '',
          info_another_hos_continue: ''
        },
        lab_name_list: [],
        result_name_list:[],
        norma_list: [],
        first_name_list: [],
        edzm_list: [],
        center_list: [],
        lab_name_id: 0,
        norma_id: 0,
        first_name_id: 0,
        edzm_id: 0,
        center_id: 0,
        result_id: 0,
        show_table: false,
        show_number: 0,
        customToolbar: [
        ["bold", "italic", "underline"],
        ],
        table:{
          name1: '',
          name2: '',
          name3: '',
          name4: '',
          name5: '',
          name6: '',
          name7: '',
          name8: '',
        },
        table_show:'',
        table_have: '',
      }
    },
    computed: mapGetters(['get_lab_main_list']),
    async mounted(){
      this.loading = true;
      await this.updateMain();
      
      // if(this.this.lab_name_list[0].name1 == ''){
      //   return;
      // }
      for(let i=0; i<50; i++){
        let obj_name = {
          name: ''
        }
        let obj_name1 = {
          name1: ''
        }
        let obj_name2 = {
          name2: ''
        }
        let obj_name3 = {
          name3: ''
        }
        let obj_name4 = {
          name4: ''
        }
        let obj_name5 = {
          name5: ''
        }
        this.first_name_list.push(obj_name);
        this.lab_name_list.push(obj_name1);
        this.norma_list.push(obj_name2);
        this.edzm_list.push(obj_name3);
        this.center_list.push(obj_name4);
        this.result_name_list.push(obj_name5);
      }
      if(this.main_value.lab_name_s){
        await this.updateLab_name();
        for(let i=49;i>=0; i--){
          // console.log(this.lab_name_list[i].name1)
          if(this.lab_name_list[i].name1 != ''){
            this.number_list = i+1;
            this.show_number = i+1;
            break;
          }
        }
      }
      if(this.main_value.norma_s){
        await this.updateNorma_name();
      }
      if(this.main_value.first_name_s){
        await this.updateFirst_name();
      }
      if(this.main_value.edzm_s){
        await this.updateEdzm_name();
      }
      if(this.main_value.center_s){
        await this.updateCenter_name();
      }
      await this.updateResult_name();
      
      console.log('hiy')
      this.show_table = true;
      this.loading = false;
    },
    methods:{
      ...mapActions(['fetch_lab_main_list']),
      add_item_lab_item(){
        this.number_list ++;
      },
      open_name_update(){
        this.show_number = 0;
      },
      remove_item_lab_item(i){
        console.log(i)
        this.first_name_list[i].name = '';
        this.lab_name_list[i].name1= '';
        this.norma_list[i].name2 = '';
        this.edzm_list[i].name3 = '';
        this.center_list[i].name4 = '';
        console.log(this.lab_name_list);
        console.log(this.norma_list);
        this.number_list --;
      },
      async updateMain(){
        try{
          this.loading =true;
          const response = await fetch(this.$store.state.hostname + '/HospitalAnalizDynamicmains/' + this.main_id)
          const data = await response.json();
          console.log('data')
          console.log(data)
          this.main_value.menu_name = data.name;
          this.main_value.blanka_name = data.name_title;
          this.main_value.pribor_name = data.name_1;
          this.main_value.anything = data.name_2;
          this.main_value.lab_name = data.lab_name;
          this.main_value.lab_name_s = data.lab_name_status;
          this.main_value.norma = data.norma;
          this.main_value.norma_s = data.norma_status;
          this.main_value.first_name = data.first_name;
          this.main_value.first_name_s = data.firs_name_status;
          this.main_value.edzm = data.unit_measurment;
          this.main_value.edzm_s = data.unit_measurment_status;
          this.main_value.center = data.extra;
          this.main_value.center_s = data.extra_status;
          this.main_value.size_top = data.name_3;
          this.main_value.bottom_text =  data.name_4;
          this.main_value.result_info_bottom =  data.name_13;
          this.main_value.doctor_name =  data.name_14;
          this.main_value.info_another_hos =  data.name_21;
          this.main_value.info_another_hos_continue =  data.name_22;
          this.table.name1 =  data.name_5;
          this.table.name2 =  data.name_6;
          this.table.name3 =  data.name_7;
          this.table.name4 =  data.name_8;
          this.table.name5 =  data.name_9;
          this.table.name6 =  data.name_10;
          this.table.name7 =  data.name_11;
          this.table.name8 =  data.name_12;
          this.table_show =  data.name_25;
          if(data.name_20 == '+'){
            this.main_value.tableWithout_s = true;
          }
          this.loading = false;
        }
        catch{
            this.$refs.message.error('server_not_working')
          // console.log('catch')
        }
      },
      async submitMain(){
        try{
          if(this.table.name1 || this.table.name2 || this.table.name3 || this.table.name4 ||  this.table.name5 || this.table.name6 ||
            this.table.name7 || this.table.name8){
              this.table_show = '+';
          }
          if(this.main_value.tableWithout_s == true){
            this.table_have = '+';
          }
          const requestOptions = {
            method : "POST",
            headers: { "Content-Type" : "application/json" },
            body: JSON.stringify({
            "name": this.main_value.menu_name,
            "name_title": this.main_value.blanka_name,
            "name_1": this.main_value.pribor_name,
            "name_2": this.main_value.anything,
            "name_3": this.main_value.size_top,
            "name_4": this.main_value.bottom_text,
            "name_13": this.main_value.result_info_bottom,
            "name_14": this.main_value.doctor_name,
            "name_5": this.table.name1,
            "name_6": this.table.name2,
            "name_7": this.table.name3,
            "name_8": this.table.name4,
            "name_9": this.table.name5,
            "name_10": this.table.name6,
            "name_11": this.table.name7,
            "name_12": this.table.name8,
            "name_25": this.table_show,
            "name_20": this.table_have,
            "name_21": this.main_value.info_another_hos,
            "name_22": this.main_value.info_another_hos_continue,
            "lab_name": this.main_value.lab_name,
            "lab_name_status": this.main_value.lab_name_s,
            "norma": this.main_value.norma,
            "norma_status": this.main_value.norma_s,
            "first_name": this.main_value.first_name,
            "firs_name_status": this.main_value.first_name_s,
            "unit_measurment": this.main_value.edzm,
            "unit_measurment_status": this.main_value.edzm_s,
            "extra": this.main_value.center,
            "extra_status":  this.main_value.center_s,
            "id": this.main_id,
            "name_50": localStorage.AuthId.toString(),
            })
          };
          this.loading = true;
          const response = await fetch(this.$store.state.hostname + '/HospitalAnalizDynamicmains', requestOptions)
          const data = await response.json()
          if(data.id){
            // this.cls_wnd()
            this.main_id = data.id;
            console.log('this.main_id')
            console.log(this.main_id)
            this.number_list ++;
            this.loading = false;
            this.$refs.message.success('Added_successfully')
            return true;
          }
          else{
            // this.$refs.msg.error('Error_successfully')
            this.loading = false;
            this.modal_info = data.detail + "    (" + data.routine + ")";
            this.modal_status = true;
            return false;
          }
        }
        catch{
          this.loading = false;
          this.modal_info = "Server no connect";
          this.modal_status = true;
        }
      },
      async updateLab_name(){
        try{
          this.loading =true;
          const response = await fetch(this.$store.state.hostname + '/HospitalAnalizDynamicLabTitles/getPaginationByMainTableId?page=0&size=1&main_table_id=' + this.main_id)
          const data = await response.json();
          console.log('data Lab')
          console.log(data.items_list[0])
          this.lab_name_id = data.items_list[0].Id
          this.lab_name_list[0].name1 = data.items_list[0].name_1;
          this.lab_name_list[1].name1 = data.items_list[0].name_2;
          this.lab_name_list[2].name1 = data.items_list[0].name_3;
          this.lab_name_list[3].name1 = data.items_list[0].name_4;
          this.lab_name_list[4].name1 = data.items_list[0].name_5;
          this.lab_name_list[5].name1 = data.items_list[0].name_6;
          this.lab_name_list[6].name1 = data.items_list[0].name_7;
          this.lab_name_list[7].name1 = data.items_list[0].name_8;
          this.lab_name_list[8].name1 = data.items_list[0].name_9;
          this.lab_name_list[9].name1 = data.items_list[0].name_10;
          this.lab_name_list[10].name1 = data.items_list[0].name_11;
          this.lab_name_list[11].name1 = data.items_list[0].name_12;
          this.lab_name_list[12].name1 = data.items_list[0].name_13;
          this.lab_name_list[13].name1 = data.items_list[0].name_14;
          this.lab_name_list[14].name1 = data.items_list[0].name_15;
          this.lab_name_list[15].name1 = data.items_list[0].name_16;
          this.lab_name_list[16].name1 = data.items_list[0].name_17;
          this.lab_name_list[17].name1 = data.items_list[0].name_18;
          this.lab_name_list[18].name1 = data.items_list[0].name_19;
          this.lab_name_list[19].name1 = data.items_list[0].name_20;
          this.lab_name_list[20].name1 = data.items_list[0].name_21;
          this.lab_name_list[21].name1 = data.items_list[0].name_22;
          this.lab_name_list[22].name1 = data.items_list[0].name_23;
          this.lab_name_list[23].name1 = data.items_list[0].name_24;
          this.lab_name_list[24].name1 = data.items_list[0].name_25;
          this.lab_name_list[25].name1 = data.items_list[0].name_26;
          this.lab_name_list[26].name1 = data.items_list[0].name_27;
          this.lab_name_list[27].name1 = data.items_list[0].name_28;
          this.lab_name_list[28].name1 = data.items_list[0].name_29;
          this.lab_name_list[29].name1 = data.items_list[0].name_30;
          this.lab_name_list[30].name1 = data.items_list[0].name_31;
          this.lab_name_list[31].name1 = data.items_list[0].name_32;
          this.lab_name_list[32].name1 = data.items_list[0].name_33;
          this.lab_name_list[33].name1 = data.items_list[0].name_34;
          this.lab_name_list[34].name1 = data.items_list[0].name_35;
          this.lab_name_list[35].name1 = data.items_list[0].name_36;
          this.lab_name_list[36].name1 = data.items_list[0].name_37;
          this.lab_name_list[37].name1 = data.items_list[0].name_38;
          this.lab_name_list[38].name1 = data.items_list[0].name_39;
          this.lab_name_list[39].name1 = data.items_list[0].name_40;
          this.lab_name_list[40].name1 = data.items_list[0].name_41;
          this.lab_name_list[41].name1 = data.items_list[0].name_42;
          this.lab_name_list[42].name1 = data.items_list[0].name_43;
          this.lab_name_list[43].name1 = data.items_list[0].name_44;
          this.lab_name_list[44].name1 = data.items_list[0].name_45;
          this.lab_name_list[45].name1 = data.items_list[0].name_46;
          this.lab_name_list[46].name1 = data.items_list[0].name_47;
          this.lab_name_list[47].name1 = data.items_list[0].name_48;
          this.lab_name_list[48].name1 = data.items_list[0].name_49;
          this.lab_name_list[49].name1 = data.items_list[0].name_50;
          this.loading = false;
        }
        catch{
            this.$refs.message.error('server_not_working')
          // console.log('catch')
        }
      },
      async updateNorma_name(){
        try{
          this.loading =true;
          const response = await fetch(this.$store.state.hostname + '/HospitalAnalizDynamicNormas/getPaginationByMainTableId?page=0&size=1&main_table_id=' + this.main_id)
          const data = await response.json();
          console.log('data Norma')
          if(data.items_list.length == 0)return;
          console.log(data.items_list[0])
          this.norma_id = data.items_list[0].Id;
          this.norma_list[0].name2 = data.items_list[0].name_1;
          this.norma_list[1].name2 = data.items_list[0].name_2;
          this.norma_list[2].name2 = data.items_list[0].name_3;
          this.norma_list[3].name2 = data.items_list[0].name_4;
          this.norma_list[4].name2 = data.items_list[0].name_5;
          this.norma_list[5].name2 = data.items_list[0].name_6;
          this.norma_list[6].name2 = data.items_list[0].name_7;
          this.norma_list[7].name2 = data.items_list[0].name_8;
          this.norma_list[8].name2 = data.items_list[0].name_9;
          this.norma_list[9].name2 = data.items_list[0].name_10;
          this.norma_list[10].name2 = data.items_list[0].name_11;
          this.norma_list[11].name2 = data.items_list[0].name_12;
          this.norma_list[12].name2 = data.items_list[0].name_13;
          this.norma_list[13].name2 = data.items_list[0].name_14;
          this.norma_list[14].name2 = data.items_list[0].name_15;
          this.norma_list[15].name2 = data.items_list[0].name_16;
          this.norma_list[16].name2 = data.items_list[0].name_17;
          this.norma_list[17].name2 = data.items_list[0].name_18;
          this.norma_list[18].name2 = data.items_list[0].name_19;
          this.norma_list[19].name2 = data.items_list[0].name_20;
          this.norma_list[20].name2 = data.items_list[0].name_21;
          this.norma_list[21].name2 = data.items_list[0].name_22;
          this.norma_list[22].name2 = data.items_list[0].name_23;
          this.norma_list[23].name2 = data.items_list[0].name_24;
          this.norma_list[24].name2 = data.items_list[0].name_25;
          this.norma_list[25].name2 = data.items_list[0].name_26;
          this.norma_list[26].name2 = data.items_list[0].name_27;
          this.norma_list[27].name2 = data.items_list[0].name_28;
          this.norma_list[28].name2 = data.items_list[0].name_29;
          this.norma_list[29].name2 = data.items_list[0].name_30;
          this.norma_list[30].name2 = data.items_list[0].name_31;
          this.norma_list[31].name2 = data.items_list[0].name_32;
          this.norma_list[32].name2 = data.items_list[0].name_33;
          this.norma_list[33].name2 = data.items_list[0].name_34;
          this.norma_list[34].name2 = data.items_list[0].name_35;
          this.norma_list[35].name2 = data.items_list[0].name_36;
          this.norma_list[36].name2 = data.items_list[0].name_37;
          this.norma_list[37].name2 = data.items_list[0].name_38;
          this.norma_list[38].name2 = data.items_list[0].name_39;
          this.norma_list[39].name2 = data.items_list[0].name_40;
          this.norma_list[40].name2 = data.items_list[0].name_41;
          this.norma_list[41].name2 = data.items_list[0].name_42;
          this.norma_list[42].name2 = data.items_list[0].name_43;
          this.norma_list[43].name2 = data.items_list[0].name_44;
          this.norma_list[44].name2 = data.items_list[0].name_45;
          this.norma_list[45].name2 = data.items_list[0].name_46;
          this.norma_list[46].name2 = data.items_list[0].name_47;
          this.norma_list[47].name2 = data.items_list[0].name_48;
          this.norma_list[48].name2 = data.items_list[0].name_49;
          this.norma_list[49].name2 = data.items_list[0].name_50;
          this.loading = false;
        }
        catch{
            this.$refs.message.error('server_not_working')
          // console.log('catch')
        }
      },
      async updateFirst_name(){
        try{
          this.loading = true;
          const response = await fetch(this.$store.state.hostname + '/HospitalAnalizDynamicFirstNames/getPaginationByMainTableId?page=0&size=1&main_table_id=' + this.main_id)
          const data = await response.json();
          console.log('data Norma')
          if(data.items_list.length == 0)return;
          console.log(data.items_list[0])
          this.first_name_id = data.items_list[0].Id;
          this.first_name_list[0].name = data.items_list[0].name_1;
          this.first_name_list[1].name = data.items_list[0].name_2;
          this.first_name_list[2].name = data.items_list[0].name_3;
          this.first_name_list[3].name = data.items_list[0].name_4;
          this.first_name_list[4].name = data.items_list[0].name_5;
          this.first_name_list[5].name = data.items_list[0].name_6;
          this.first_name_list[6].name = data.items_list[0].name_7;
          this.first_name_list[7].name = data.items_list[0].name_8;
          this.first_name_list[8].name = data.items_list[0].name_9;
          this.first_name_list[9].name = data.items_list[0].name_10;
          this.first_name_list[10].name = data.items_list[0].name_11;
          this.first_name_list[11].name = data.items_list[0].name_12;
          this.first_name_list[12].name = data.items_list[0].name_13;
          this.first_name_list[13].name = data.items_list[0].name_14;
          this.first_name_list[14].name = data.items_list[0].name_15;
          this.first_name_list[15].name = data.items_list[0].name_16;
          this.first_name_list[16].name = data.items_list[0].name_17;
          this.first_name_list[17].name = data.items_list[0].name_18;
          this.first_name_list[18].name = data.items_list[0].name_19;
          this.first_name_list[19].name = data.items_list[0].name_20;
          this.first_name_list[20].name = data.items_list[0].name_21;
          this.first_name_list[21].name = data.items_list[0].name_22;
          this.first_name_list[22].name = data.items_list[0].name_23;
          this.first_name_list[23].name = data.items_list[0].name_24;
          this.first_name_list[24].name = data.items_list[0].name_25;
          this.first_name_list[25].name = data.items_list[0].name_26;
          this.first_name_list[26].name = data.items_list[0].name_27;
          this.first_name_list[27].name = data.items_list[0].name_28;
          this.first_name_list[28].name = data.items_list[0].name_29;
          this.first_name_list[29].name = data.items_list[0].name_30;
          this.first_name_list[30].name = data.items_list[0].name_31;
          this.first_name_list[31].name = data.items_list[0].name_32;
          this.first_name_list[32].name = data.items_list[0].name_33;
          this.first_name_list[33].name = data.items_list[0].name_34;
          this.first_name_list[34].name = data.items_list[0].name_35;
          this.first_name_list[35].name = data.items_list[0].name_36;
          this.first_name_list[36].name = data.items_list[0].name_37;
          this.first_name_list[37].name = data.items_list[0].name_38;
          this.first_name_list[38].name = data.items_list[0].name_39;
          this.first_name_list[39].name = data.items_list[0].name_40;
          this.first_name_list[40].name = data.items_list[0].name_41;
          this.first_name_list[41].name = data.items_list[0].name_42;
          this.first_name_list[42].name = data.items_list[0].name_43;
          this.first_name_list[43].name = data.items_list[0].name_44;
          this.first_name_list[44].name = data.items_list[0].name_45;
          this.first_name_list[45].name = data.items_list[0].name_46;
          this.first_name_list[46].name = data.items_list[0].name_47;
          this.first_name_list[47].name = data.items_list[0].name_48;
          this.first_name_list[48].name = data.items_list[0].name_49;
          this.first_name_list[49].name = data.items_list[0].name_50;
          this.loading = false;
        }
        catch{
            this.$refs.message.error('server_not_working')
          // console.log('catch')
        }
      },
      async updateEdzm_name(){
        try{
          this.loading = true;
          const response = await fetch(this.$store.state.hostname + '/HospitalAnalizDynamicUnitMeasurments/getPaginationByMainTableId?page=0&size=1&main_table_id=' + this.main_id)
          const data = await response.json();
          console.log('data Norma')
          if(data.items_list.length == 0)return;
          console.log(data.items_list[0])
          this.edzm_id = data.items_list[0].Id;
          this.edzm_list[0].name3 = data.items_list[0].name_1;
          this.edzm_list[1].name3 = data.items_list[0].name_2;
          this.edzm_list[2].name3 = data.items_list[0].name_3;
          this.edzm_list[3].name3 = data.items_list[0].name_4;
          this.edzm_list[4].name3 = data.items_list[0].name_5;
          this.edzm_list[5].name3 = data.items_list[0].name_6;
          this.edzm_list[6].name3 = data.items_list[0].name_7;
          this.edzm_list[7].name3 = data.items_list[0].name_8;
          this.edzm_list[8].name3 = data.items_list[0].name_9;
          this.edzm_list[9].name3 = data.items_list[0].name_10;
          this.edzm_list[10].name3 = data.items_list[0].name_11;
          this.edzm_list[11].name3 = data.items_list[0].name_12;
          this.edzm_list[12].name3 = data.items_list[0].name_13;
          this.edzm_list[13].name3 = data.items_list[0].name_14;
          this.edzm_list[14].name3 = data.items_list[0].name_15;
          this.edzm_list[15].name3 = data.items_list[0].name_16;
          this.edzm_list[16].name3 = data.items_list[0].name_17;
          this.edzm_list[17].name3 = data.items_list[0].name_18;
          this.edzm_list[18].name3 = data.items_list[0].name_19;
          this.edzm_list[19].name3 = data.items_list[0].name_20;
          this.edzm_list[20].name3 = data.items_list[0].name_21;
          this.edzm_list[21].name3 = data.items_list[0].name_22;
          this.edzm_list[22].name3 = data.items_list[0].name_23;
          this.edzm_list[23].name3 = data.items_list[0].name_24;
          this.edzm_list[24].name3 = data.items_list[0].name_25;
          this.edzm_list[25].name3 = data.items_list[0].name_26;
          this.edzm_list[26].name3 = data.items_list[0].name_27;
          this.edzm_list[27].name3 = data.items_list[0].name_28;
          this.edzm_list[28].name3 = data.items_list[0].name_29;
          this.edzm_list[29].name3 = data.items_list[0].name_30;
          this.edzm_list[30].name3 = data.items_list[0].name_31;
          this.edzm_list[31].name3 = data.items_list[0].name_32;
          this.edzm_list[32].name3 = data.items_list[0].name_33;
          this.edzm_list[33].name3 = data.items_list[0].name_34;
          this.edzm_list[34].name3 = data.items_list[0].name_35;
          this.edzm_list[35].name3 = data.items_list[0].name_36;
          this.edzm_list[36].name3 = data.items_list[0].name_37;
          this.edzm_list[37].name3 = data.items_list[0].name_38;
          this.edzm_list[38].name3 = data.items_list[0].name_39;
          this.edzm_list[39].name3 = data.items_list[0].name_40;
          this.edzm_list[40].name3 = data.items_list[0].name_41;
          this.edzm_list[41].name3 = data.items_list[0].name_42;
          this.edzm_list[42].name3 = data.items_list[0].name_43;
          this.edzm_list[43].name3 = data.items_list[0].name_44;
          this.edzm_list[44].name3 = data.items_list[0].name_45;
          this.edzm_list[45].name3 = data.items_list[0].name_46;
          this.edzm_list[46].name3 = data.items_list[0].name_47;
          this.edzm_list[47].name3 = data.items_list[0].name_48;
          this.edzm_list[48].name3 = data.items_list[0].name_49;
          this.edzm_list[49].name3 = data.items_list[0].name_50;
          this.loading = false;
        }
        catch{
            this.$refs.message.error('server_not_working')
          // console.log('catch')
        }
      },
      async updateCenter_name(){
        try{
          this.loading = true;
          const response = await fetch(this.$store.state.hostname + '/HospitalAnalizDynamicCenterNames/getPaginationByMainTableId?page=0&size=1&main_table_id=' + this.main_id)
          const data = await response.json();
          console.log('data Norma')
          if(data.items_list.length == 0)return;
          console.log(data.items_list[0])
          this.center_id = data.items_list[0].Id;
          this.center_list[0].name4 = data.items_list[0].name_1;
          this.center_list[1].name4 = data.items_list[0].name_2;
          this.center_list[2].name4 = data.items_list[0].name_3;
          this.center_list[3].name4 = data.items_list[0].name_4;
          this.center_list[4].name4 = data.items_list[0].name_5;
          this.center_list[5].name4 = data.items_list[0].name_6;
          this.center_list[6].name4 = data.items_list[0].name_7;
          this.center_list[7].name4 = data.items_list[0].name_8;
          this.center_list[8].name4 = data.items_list[0].name_9;
          this.center_list[9].name4 = data.items_list[0].name_10;
          this.center_list[10].name4 = data.items_list[0].name_11;
          this.center_list[11].name4 = data.items_list[0].name_12;
          this.center_list[12].name4 = data.items_list[0].name_13;
          this.center_list[13].name4 = data.items_list[0].name_14;
          this.center_list[14].name4 = data.items_list[0].name_15;
          this.center_list[15].name4 = data.items_list[0].name_16;
          this.center_list[16].name4 = data.items_list[0].name_17;
          this.center_list[17].name4 = data.items_list[0].name_18;
          this.center_list[18].name4 = data.items_list[0].name_19;
          this.center_list[19].name4 = data.items_list[0].name_20;
          this.center_list[20].name4 = data.items_list[0].name_21;
          this.center_list[21].name4 = data.items_list[0].name_22;
          this.center_list[22].name4 = data.items_list[0].name_23;
          this.center_list[23].name4 = data.items_list[0].name_24;
          this.center_list[24].name4 = data.items_list[0].name_25;
          this.center_list[25].name4 = data.items_list[0].name_26;
          this.center_list[26].name4 = data.items_list[0].name_27;
          this.center_list[27].name4 = data.items_list[0].name_28;
          this.center_list[28].name4 = data.items_list[0].name_29;
          this.center_list[29].name4 = data.items_list[0].name_30;
          this.center_list[30].name4 = data.items_list[0].name_31;
          this.center_list[31].name4 = data.items_list[0].name_32;
          this.center_list[32].name4 = data.items_list[0].name_33;
          this.center_list[33].name4 = data.items_list[0].name_34;
          this.center_list[34].name4 = data.items_list[0].name_35;
          this.center_list[35].name4 = data.items_list[0].name_36;
          this.center_list[36].name4 = data.items_list[0].name_37;
          this.center_list[37].name4 = data.items_list[0].name_38;
          this.center_list[38].name4 = data.items_list[0].name_39;
          this.center_list[39].name4 = data.items_list[0].name_40;
          this.center_list[40].name4 = data.items_list[0].name_41;
          this.center_list[41].name4 = data.items_list[0].name_42;
          this.center_list[42].name4 = data.items_list[0].name_43;
          this.center_list[43].name4 = data.items_list[0].name_44;
          this.center_list[44].name4 = data.items_list[0].name_45;
          this.center_list[45].name4 = data.items_list[0].name_46;
          this.center_list[46].name4 = data.items_list[0].name_47;
          this.center_list[47].name4 = data.items_list[0].name_48;
          this.center_list[48].name4 = data.items_list[0].name_49;
          this.center_list[49].name4 = data.items_list[0].name_50;
          this.loading = false;
        }
        catch{
            this.$refs.message.error('server_not_working')
          // console.log('catch')
        }
      },
      async updateResult_name(){
        try{
          this.loading = true;
          const response = await fetch(this.$store.state.hostname + '/HospitalAnalizDynamicNameTitles/getPaginationByMainTableId?page=0&size=1&main_table_id=' + this.main_id)
          const data = await response.json();
          console.log('data Norma')
          if(data.items_list.length == 0)return;
          console.log(data.items_list[0])
          this.result_id = data.items_list[0].Id;
          this.result_name_list[0].name5 = data.items_list[0].name_1;
          this.result_name_list[1].name5 = data.items_list[0].name_2;
          this.result_name_list[2].name5 = data.items_list[0].name_3;
          this.result_name_list[3].name5 = data.items_list[0].name_4;
          this.result_name_list[4].name5 = data.items_list[0].name_5;
          this.result_name_list[5].name5 = data.items_list[0].name_6;
          this.result_name_list[6].name5 = data.items_list[0].name_7;
          this.result_name_list[7].name5 = data.items_list[0].name_8;
          this.result_name_list[8].name5 = data.items_list[0].name_9;
          this.result_name_list[9].name5 = data.items_list[0].name_10;
          this.result_name_list[10].name5 = data.items_list[0].name_11;
          this.result_name_list[11].name5 = data.items_list[0].name_12;
          this.result_name_list[12].name5 = data.items_list[0].name_13;
          this.result_name_list[13].name5 = data.items_list[0].name_14;
          this.result_name_list[14].name5 = data.items_list[0].name_15;
          this.result_name_list[15].name5 = data.items_list[0].name_16;
          this.result_name_list[16].name5 = data.items_list[0].name_17;
          this.result_name_list[17].name5 = data.items_list[0].name_18;
          this.result_name_list[18].name5 = data.items_list[0].name_19;
          this.result_name_list[19].name5 = data.items_list[0].name_20;
          this.result_name_list[20].name5 = data.items_list[0].name_21;
          this.result_name_list[21].name5 = data.items_list[0].name_22;
          this.result_name_list[22].name5 = data.items_list[0].name_23;
          this.result_name_list[23].name5 = data.items_list[0].name_24;
          this.result_name_list[24].name5 = data.items_list[0].name_25;
          this.result_name_list[25].name5 = data.items_list[0].name_26;
          this.result_name_list[26].name5 = data.items_list[0].name_27;
          this.result_name_list[27].name5 = data.items_list[0].name_28;
          this.result_name_list[28].name5 = data.items_list[0].name_29;
          this.result_name_list[29].name5 = data.items_list[0].name_30;
          this.result_name_list[30].name5 = data.items_list[0].name_31;
          this.result_name_list[31].name5 = data.items_list[0].name_32;
          this.result_name_list[32].name5 = data.items_list[0].name_33;
          this.result_name_list[33].name5 = data.items_list[0].name_34;
          this.result_name_list[34].name5 = data.items_list[0].name_35;
          this.result_name_list[35].name5 = data.items_list[0].name_36;
          this.result_name_list[36].name5 = data.items_list[0].name_37;
          this.result_name_list[37].name5 = data.items_list[0].name_38;
          this.result_name_list[38].name5 = data.items_list[0].name_39;
          this.result_name_list[39].name5 = data.items_list[0].name_40;
          this.result_name_list[40].name5 = data.items_list[0].name_41;
          this.result_name_list[41].name5 = data.items_list[0].name_42;
          this.result_name_list[42].name5 = data.items_list[0].name_43;
          this.result_name_list[43].name5 = data.items_list[0].name_44;
          this.result_name_list[44].name5 = data.items_list[0].name_45;
          this.result_name_list[45].name5 = data.items_list[0].name_46;
          this.result_name_list[46].name5 = data.items_list[0].name_47;
          this.result_name_list[47].name5 = data.items_list[0].name_48;
          this.result_name_list[48].name5 = data.items_list[0].name_49;
          this.result_name_list[49].name5 = data.items_list[0].name_50;
          this.loading = false;
        }
        catch{
            this.$refs.message.error('server_not_working')
          // console.log('catch')
        }
      },
      async submitItems(){
        if(this.main_value.first_name_s == true){
          await this.submitFirst();
        }
        if(this.main_value.lab_name_s == true){
          await this.submitLab_name();
        }
        if(this.main_value.norma_s == true){
          await this.submitNorma_name();
        }
        if(this.main_value.edzm_s == true){
          await this.submitEdzm_name();
        }
        if(this.main_value.center_s == true){
          await this.submitCenter_name();
        }
        await this.submitResult_name();
        this.$emit('close', this.main_id);
      },
      async submitFirst(){
        try{
          const requestOptions = {
            method : "POST",
            headers: { "Content-Type" : "application/json" },
            body: JSON.stringify({
            "hospitalAnalizDynamicmainId": this.main_id,
            "name": this.main_value.first_name,
            "name_1": this.first_name_list[0].name,
            "name_2": this.first_name_list[1].name,
            "name_3": this.first_name_list[2].name,
            "name_4": this.first_name_list[3].name,
            "name_5": this.first_name_list[4].name,
            "name_6": this.first_name_list[5].name,
            "name_7": this.first_name_list[6].name,
            "name_8": this.first_name_list[7].name,
            "name_9": this.first_name_list[8].name,
            "name_10": this.first_name_list[9].name,
            "name_11": this.first_name_list[10].name,
            "name_12": this.first_name_list[11].name,
            "name_13": this.first_name_list[12].name,
            "name_14": this.first_name_list[13].name,
            "name_15": this.first_name_list[14].name,
            "name_16": this.first_name_list[15].name,
            "name_17": this.first_name_list[16].name,
            "name_18": this.first_name_list[17].name,
            "name_19": this.first_name_list[18].name,
            "name_20": this.first_name_list[19].name,
            "name_21": this.first_name_list[20].name,
            "name_22": this.first_name_list[21].name,
            "name_23": this.first_name_list[22].name,
            "name_24": this.first_name_list[23].name,
            "name_25": this.first_name_list[24].name,
            "name_26": this.first_name_list[25].name,
            "name_27": this.first_name_list[26].name,
            "name_28": this.first_name_list[27].name,
            "name_29": this.first_name_list[28].name,
            "name_30": this.first_name_list[29].name,
            "name_31": this.first_name_list[30].name,
            "name_32": this.first_name_list[31].name,
            "name_33": this.first_name_list[32].name,
            "name_34": this.first_name_list[33].name,
            "name_35": this.first_name_list[34].name,
            "name_36": this.first_name_list[35].name,
            "name_37": this.first_name_list[36].name,
            "name_38": this.first_name_list[37].name,
            "name_39": this.first_name_list[38].name,
            "name_40": this.first_name_list[39].name,
            "name_41": this.first_name_list[40].name,
            "name_42": this.first_name_list[41].name,
            "name_43": this.first_name_list[42].name,
            "name_44": this.first_name_list[43].name,
            "name_45": this.first_name_list[44].name,
            "name_46": this.first_name_list[45].name,
            "name_47": this.first_name_list[46].name,
            "name_48": this.first_name_list[47].name,
            "name_49": this.first_name_list[48].name,
            "name_50": this.first_name_list[49].name,
            "id": this.first_name_id,
            })
          };
          this.loading = true;
          const response = await fetch(this.$store.state.hostname + '/HospitalAnalizDynamicFirstNames', requestOptions)
          const data = await response.json()
          if(data.id){
            // this.cls_wnd()
            // this.main_id = data.id;
            // this.number_list ++;
            this.loading = false;
            this.$refs.message.success('Added_successfully')
            return true;
          }
          else{
            // this.$refs.msg.error('Error_successfully')
            this.loading = false;
            this.modal_info = data.detail + "    (" + data.routine + ")";
            this.modal_status = true;
            return false;
          }
        }
        catch{
          this.loading = false;
          this.modal_info = "Server no connect";
          this.modal_status = true;
        }
      },
      async submitLab_name(){
        try{
          const requestOptions = {
            method : "POST",
            headers: { "Content-Type" : "application/json" },
            body: JSON.stringify({
            "hospitalAnalizDynamicmainId": this.main_id,
            "name": this.main_value.lab_name,
            "name_1": this.lab_name_list[0].name1,
            "name_2": this.lab_name_list[1].name1,
            "name_3": this.lab_name_list[2].name1,
            "name_4": this.lab_name_list[3].name1,
            "name_5": this.lab_name_list[4].name1,
            "name_6": this.lab_name_list[5].name1,
            "name_7": this.lab_name_list[6].name1,
            "name_8": this.lab_name_list[7].name1,
            "name_9": this.lab_name_list[8].name1,
            "name_10": this.lab_name_list[9].name1,
            "name_11": this.lab_name_list[10].name1,
            "name_12": this.lab_name_list[11].name1,
            "name_13": this.lab_name_list[12].name1,
            "name_14": this.lab_name_list[13].name1,
            "name_15": this.lab_name_list[14].name1,
            "name_16": this.lab_name_list[15].name1,
            "name_17": this.lab_name_list[16].name1,
            "name_18": this.lab_name_list[17].name1,
            "name_19": this.lab_name_list[18].name1,
            "name_20": this.lab_name_list[19].name1,
            "name_21": this.lab_name_list[20].name1,
            "name_22": this.lab_name_list[21].name1,
            "name_23": this.lab_name_list[22].name1,
            "name_24": this.lab_name_list[23].name1,
            "name_25": this.lab_name_list[24].name1,
            "name_26": this.lab_name_list[25].name1,
            "name_27": this.lab_name_list[26].name1,
            "name_28": this.lab_name_list[27].name1,
            "name_29": this.lab_name_list[28].name1,
            "name_30": this.lab_name_list[29].name1,
            "name_31": this.lab_name_list[30].name1,
            "name_32": this.lab_name_list[31].name1,
            "name_33": this.lab_name_list[32].name1,
            "name_34": this.lab_name_list[33].name1,
            "name_35": this.lab_name_list[34].name1,
            "name_36": this.lab_name_list[35].name1,
            "name_37": this.lab_name_list[36].name1,
            "name_38": this.lab_name_list[37].name1,
            "name_39": this.lab_name_list[38].name1,
            "name_40": this.lab_name_list[39].name1,
            "name_41": this.lab_name_list[40].name1,
            "name_42": this.lab_name_list[41].name1,
            "name_43": this.lab_name_list[42].name1,
            "name_44": this.lab_name_list[43].name1,
            "name_45": this.lab_name_list[44].name1,
            "name_46": this.lab_name_list[45].name1,
            "name_47": this.lab_name_list[46].name1,
            "name_48": this.lab_name_list[47].name1,
            "name_49": this.lab_name_list[48].name1,
            "name_50": this.lab_name_list[49].name1,
            "id": this.lab_name_id,
            })
          };
          this.loading = true;
          const response = await fetch(this.$store.state.hostname + '/HospitalAnalizDynamicLabTitles', requestOptions)
          const data = await response.json()
          if(data.id){
            // this.cls_wnd()
            // this.main_id = data.id;
            // this.number_list ++;
            this.loading = false;
            this.$refs.message.success('Added_successfully')
            return true;
          }
          else{
            // this.$refs.msg.error('Error_successfully')
            this.loading = false;
            this.modal_info = data.detail + "    (" + data.routine + ")";
            this.modal_status = true;
            return false;
          }
        }
        catch{
          this.loading = false;
          this.modal_info = "Server no connect";
          this.modal_status = true;
        }
      },
      async submitNorma_name(){
        try{
          const requestOptions = {
            method : "POST",
            headers: { "Content-Type" : "application/json" },
            body: JSON.stringify({
            "hospitalAnalizDynamicmainId": this.main_id,
            "name": this.main_value.norma,
            "name_1": this.norma_list[0].name2,
            "name_2": this.norma_list[1].name2,
            "name_3": this.norma_list[2].name2,
            "name_4": this.norma_list[3].name2,
            "name_5": this.norma_list[4].name2,
            "name_6": this.norma_list[5].name2,
            "name_7": this.norma_list[6].name2,
            "name_8": this.norma_list[7].name2,
            "name_9": this.norma_list[8].name2,
            "name_10": this.norma_list[9].name2,
            "name_11": this.norma_list[10].name2,
            "name_12": this.norma_list[11].name2,
            "name_13": this.norma_list[12].name2,
            "name_14": this.norma_list[13].name2,
            "name_15": this.norma_list[14].name2,
            "name_16": this.norma_list[15].name2,
            "name_17": this.norma_list[16].name2,
            "name_18": this.norma_list[17].name2,
            "name_19": this.norma_list[18].name2,
            "name_20": this.norma_list[19].name2,
            "name_21": this.norma_list[20].name2,
            "name_22": this.norma_list[21].name2,
            "name_23": this.norma_list[22].name2,
            "name_24": this.norma_list[23].name2,
            "name_25": this.norma_list[24].name2,
            "name_26": this.norma_list[25].name2,
            "name_27": this.norma_list[26].name2,
            "name_28": this.norma_list[27].name2,
            "name_29": this.norma_list[28].name2,
            "name_30": this.norma_list[29].name2,
            "name_31": this.norma_list[30].name2,
            "name_32": this.norma_list[31].name2,
            "name_33": this.norma_list[32].name2,
            "name_34": this.norma_list[33].name2,
            "name_35": this.norma_list[34].name2,
            "name_36": this.norma_list[35].name2,
            "name_37": this.norma_list[36].name2,
            "name_38": this.norma_list[37].name2,
            "name_39": this.norma_list[38].name2,
            "name_40": this.norma_list[39].name2,
            "name_41": this.norma_list[40].name2,
            "name_42": this.norma_list[41].name2,
            "name_43": this.norma_list[42].name2,
            "name_44": this.norma_list[43].name2,
            "name_45": this.norma_list[44].name2,
            "name_46": this.norma_list[45].name2,
            "name_47": this.norma_list[46].name2,
            "name_48": this.norma_list[47].name2,
            "name_49": this.norma_list[48].name2,
            "name_50": this.norma_list[49].name2,
            "id": this.norma_id,
            })
          };
          this.loading = true;
          const response = await fetch(this.$store.state.hostname + '/HospitalAnalizDynamicNormas', requestOptions)
          const data = await response.json()
          if(data.id){
            // this.cls_wnd()
            // this.main_id = data.id;
            // this.number_list ++;
            this.loading = false;
            this.$refs.message.success('Added_successfully')
            return true;
          }
          else{
            // this.$refs.msg.error('Error_successfully')
            this.loading = false;
            this.modal_info = data.detail + "    (" + data.routine + ")";
            this.modal_status = true;
            return false;
          }
        }
        catch{
          this.loading = false;
          this.modal_info = "Server no connect";
          this.modal_status = true;
        }
      },
      async submitEdzm_name(){
        try{
          const requestOptions = {
            method : "POST",
            headers: { "Content-Type" : "application/json" },
            body: JSON.stringify({
            "hospitalAnalizDynamicmainId": this.main_id,
            "name": this.main_value.edzm,
            "name_1": this.edzm_list[0].name3,
            "name_2": this.edzm_list[1].name3,
            "name_3": this.edzm_list[2].name3,
            "name_4": this.edzm_list[3].name3,
            "name_5": this.edzm_list[4].name3,
            "name_6": this.edzm_list[5].name3,
            "name_7": this.edzm_list[6].name3,
            "name_8": this.edzm_list[7].name3,
            "name_9": this.edzm_list[8].name3,
            "name_10": this.edzm_list[9].name3,
            "name_11": this.edzm_list[10].name3,
            "name_12": this.edzm_list[11].name3,
            "name_13": this.edzm_list[12].name3,
            "name_14": this.edzm_list[13].name3,
            "name_15": this.edzm_list[14].name3,
            "name_16": this.edzm_list[15].name3,
            "name_17": this.edzm_list[16].name3,
            "name_18": this.edzm_list[17].name3,
            "name_19": this.edzm_list[18].name3,
            "name_20": this.edzm_list[19].name3,
            "name_21": this.edzm_list[20].name3,
            "name_22": this.edzm_list[21].name3,
            "name_23": this.edzm_list[22].name3,
            "name_24": this.edzm_list[23].name3,
            "name_25": this.edzm_list[24].name3,
            "name_26": this.edzm_list[25].name3,
            "name_27": this.edzm_list[26].name3,
            "name_28": this.edzm_list[27].name3,
            "name_29": this.edzm_list[28].name3,
            "name_30": this.edzm_list[29].name3,
            "name_31": this.edzm_list[30].name3,
            "name_32": this.edzm_list[31].name3,
            "name_33": this.edzm_list[32].name3,
            "name_34": this.edzm_list[33].name3,
            "name_35": this.edzm_list[34].name3,
            "name_36": this.edzm_list[35].name3,
            "name_37": this.edzm_list[36].name3,
            "name_38": this.edzm_list[37].name3,
            "name_39": this.edzm_list[38].name3,
            "name_40": this.edzm_list[39].name3,
            "name_41": this.edzm_list[40].name3,
            "name_42": this.edzm_list[41].name3,
            "name_43": this.edzm_list[42].name3,
            "name_44": this.edzm_list[43].name3,
            "name_45": this.edzm_list[44].name3,
            "name_46": this.edzm_list[45].name3,
            "name_47": this.edzm_list[46].name3,
            "name_48": this.edzm_list[47].name3,
            "name_49": this.edzm_list[48].name3,
            "name_50": this.edzm_list[49].name3,
            "id": this.edzm_id,
            })
          };
          this.loading = true;
          const response = await fetch(this.$store.state.hostname + '/HospitalAnalizDynamicUnitMeasurments', requestOptions)
          const data = await response.json()
          if(data.id){
            // this.cls_wnd()
            // this.main_id = data.id;
            // this.number_list ++;
            this.loading = false;
            this.$refs.message.success('Added_successfully')
            return true;
          }
          else{
            // this.$refs.msg.error('Error_successfully')
            this.loading = false;
            this.modal_info = data.detail + "    (" + data.routine + ")";
            this.modal_status = true;
            return false;
          }
        }
        catch{
          this.loading = false;
          this.modal_info = "Server no connect";
          this.modal_status = true;
        }
      },
      async submitCenter_name(){
        try{
          const requestOptions = {
            method : "POST",
            headers: { "Content-Type" : "application/json" },
            body: JSON.stringify({
            "hospitalAnalizDynamicmainId": this.main_id,
            "name": this.main_value.center,
            "name_1": this.center_list[0].name4,
            "name_2": this.center_list[1].name4,
            "name_3": this.center_list[2].name4,
            "name_4": this.center_list[3].name4,
            "name_5": this.center_list[4].name4,
            "name_6": this.center_list[5].name4,
            "name_7": this.center_list[6].name4,
            "name_8": this.center_list[7].name4,
            "name_9": this.center_list[8].name4,
            "name_10": this.center_list[9].name4,
            "name_11": this.center_list[10].name4,
            "name_12": this.center_list[11].name4,
            "name_13": this.center_list[12].name4,
            "name_14": this.center_list[13].name4,
            "name_15": this.center_list[14].name4,
            "name_16": this.center_list[15].name4,
            "name_17": this.center_list[16].name4,
            "name_18": this.center_list[17].name4,
            "name_19": this.center_list[18].name4,
            "name_20": this.center_list[19].name4,
            "name_21": this.center_list[20].name4,
            "name_22": this.center_list[21].name4,
            "name_23": this.center_list[22].name4,
            "name_24": this.center_list[23].name4,
            "name_25": this.center_list[24].name4,
            "name_26": this.center_list[25].name4,
            "name_27": this.center_list[26].name4,
            "name_28": this.center_list[27].name4,
            "name_29": this.center_list[28].name4,
            "name_30": this.center_list[29].name4,
            "name_31": this.center_list[30].name4,
            "name_32": this.center_list[31].name4,
            "name_33": this.center_list[32].name4,
            "name_34": this.center_list[33].name4,
            "name_35": this.center_list[34].name4,
            "name_36": this.center_list[35].name4,
            "name_37": this.center_list[36].name4,
            "name_38": this.center_list[37].name4,
            "name_39": this.center_list[38].name4,
            "name_40": this.center_list[39].name4,
            "name_41": this.center_list[40].name4,
            "name_42": this.center_list[41].name4,
            "name_43": this.center_list[42].name4,
            "name_44": this.center_list[43].name4,
            "name_45": this.center_list[44].name4,
            "name_46": this.center_list[45].name4,
            "name_47": this.center_list[46].name4,
            "name_48": this.center_list[47].name4,
            "name_49": this.center_list[48].name4,
            "name_50": this.center_list[49].name4,
            "id": this.center_id,
            })
          };
          this.loading = true;
          const response = await fetch(this.$store.state.hostname + '/HospitalAnalizDynamicCenterNames', requestOptions)
          const data = await response.json()
          if(data.id){
            // this.cls_wnd()
            // this.main_id = data.id;
            // this.number_list ++;
            this.loading = false;
            this.$refs.message.success('Added_successfully')
            return true;
          }
          else{
            // this.$refs.msg.error('Error_successfully')
            this.loading = false;
            this.modal_info = data.detail + "    (" + data.routine + ")";
            this.modal_status = true;
            return false;
          }
        }
        catch{
          this.loading = false;
          this.modal_info = "Server no connect";
          this.modal_status = true;
        }
      },
      async submitResult_name(){
        try{
          const requestOptions = {
            method : "POST",
            headers: { "Content-Type" : "application/json" },
            body: JSON.stringify({
            "hospitalAnalizDynamicmainId": this.main_id,
            "name": 'result',
            "name_1": this.result_name_list[0].name5,
            "name_2": this.result_name_list[1].name5,
            "name_3": this.result_name_list[2].name5,
            "name_4": this.result_name_list[3].name5,
            "name_5": this.result_name_list[4].name5,
            "name_6": this.result_name_list[5].name5,
            "name_7": this.result_name_list[6].name5,
            "name_8": this.result_name_list[7].name5,
            "name_9": this.result_name_list[8].name5,
            "name_10": this.result_name_list[9].name5,
            "name_11": this.result_name_list[10].name5,
            "name_12": this.result_name_list[11].name5,
            "name_13": this.result_name_list[12].name5,
            "name_14": this.result_name_list[13].name5,
            "name_15": this.result_name_list[14].name5,
            "name_16": this.result_name_list[15].name5,
            "name_17": this.result_name_list[16].name5,
            "name_18": this.result_name_list[17].name5,
            "name_19": this.result_name_list[18].name5,
            "name_20": this.result_name_list[19].name5,
            "name_21": this.result_name_list[20].name5,
            "name_22": this.result_name_list[21].name5,
            "name_23": this.result_name_list[22].name5,
            "name_24": this.result_name_list[23].name5,
            "name_25": this.result_name_list[24].name5,
            "name_26": this.result_name_list[25].name5,
            "name_27": this.result_name_list[26].name5,
            "name_28": this.result_name_list[27].name5,
            "name_29": this.result_name_list[28].name5,
            "name_30": this.result_name_list[29].name5,
            "name_31": this.result_name_list[30].name5,
            "name_32": this.result_name_list[31].name5,
            "name_33": this.result_name_list[32].name5,
            "name_34": this.result_name_list[33].name5,
            "name_35": this.result_name_list[34].name5,
            "name_36": this.result_name_list[35].name5,
            "name_37": this.result_name_list[36].name5,
            "name_38": this.result_name_list[37].name5,
            "name_39": this.result_name_list[38].name5,
            "name_40": this.result_name_list[39].name5,
            "name_41": this.result_name_list[40].name5,
            "name_42": this.result_name_list[41].name5,
            "name_43": this.result_name_list[42].name5,
            "name_44": this.result_name_list[43].name5,
            "name_45": this.result_name_list[44].name5,
            "name_46": this.result_name_list[45].name5,
            "name_47": this.result_name_list[46].name5,
            "name_48": this.result_name_list[47].name5,
            "name_49": this.result_name_list[48].name5,
            "name_50": this.result_name_list[49].name5,
            "id": this.result_id,
            })
          };
          this.loading = true;
          const response = await fetch(this.$store.state.hostname + '/HospitalAnalizDynamicNameTitles', requestOptions)
          const data = await response.json()
          if(data.id){
            // this.cls_wnd()
            // this.main_id = data.id;
            // this.number_list ++;
            this.loading = false;
            this.$refs.message.success('Added_successfully')
            return true;
          }
          else{
            // this.$refs.msg.error('Error_successfully')
            this.loading = false;
            this.modal_info = data.detail + "    (" + data.routine + ")";
            this.modal_status = true;
            return false;
          }
        }
        catch{
          this.loading = false;
          this.modal_info = "Server no connect";
          this.modal_status = true;
        }
      },
      async delLaboratory(){
        const requestOptions = {
              method : "delete",
            };
            const response = await fetch(this.$store.state.hostname + "/HospitalAnalizDynamicmains/" + this.main_id, requestOptions);
            const data = await response.text();
  
            if(response.status == 201 || response.status == 200)
            {
              this.$refs.message.success('Successfully_removed')
              this.$emit('close', 0);
              await this.fetch_lab_main_list()
              this.$router.push('/laboratory_list/' + this.get_lab_main_list[0].id)

            }
            else{
              this.modal_info = data;
              this.modal_status = true;
            }
      }
      
  
    }
  
  }
  </script>
  
  <style>
  .main_header_lab_added{
    padding: 5px;
    background: rgb(234, 254, 255);
  }
  .applied{
      pointer-events: none;
      background:#ecffe6;
    }
  .content_lab_item_last{
    position:relative;
  }
  .content_lab_item_last_icons{
    position: absolute;
    right: 0px;
    top: 10px;
    cursor:pointer;
    /* border: 1px solid black; */
  }
  </style>