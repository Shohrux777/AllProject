<template>
  <div class="name_23ya bg-white" style="min-height: 100vh;">
    <div class="header text-center border-bottom py-2">
      <h4 class="text-primary">ОБЩИЙ АНАЛИЗ КРОВИ</h4>
    </div>
    <div class="row mt-2">
      <div class="col-4">
        <form v-on:submit.prevent="submit">
          <div class="d-flex " style="height:40px;">
            <mdb-input :label="$t('number')"  class="m-0 ml-4 w-75" v-model="numberEnter" outline/>
            <mdb-btn  color="primary" type="submit" class=" p-0" style="font-size: 9.8px; margin: 2px 5px;"   p="r3 l3 t0 b0">{{$t('enter')}}</mdb-btn>
          </div>
        </form>
      </div>
      <div class="col-4">
        <lineSelect
          class="mt-1 ml-3"
          :options="filteredList" 
          :searchshow="true"
          @select="selectPatient"
          :selected="patient_name"
          :label="$t('patient list')"
      />
      <small v-if="$v.patient_name.$dirty && patient_id == null" class="invalid-text mt-0 pl-4" >
        {{$t('select_item')}}
      </small>
      </div>
      <div class="col-4">
        <mdb-input :label="$t('doctor_name')" class="m-0" v-model="doctor_name" outline/>
        <small v-if="$v.doctor_name.$dirty && !$v.doctor_name.required" class="invalid-text  pl-5" style="position:absolute; bottom:-10px; left:-20px;"> 
          {{$t('select_item')}}
        </small>
      </div>
    </div>
   <loader v-if="loading"/>
    <div v-else class="mx-1 row">
      <div class="col-4" >
        <div style="position:relative;">
          <mdb-input :label="('Лейкоциты (WBC) ')"  v-model="name_1" outline/>
          <!-- <small v-if="$v.name_1.$dirty  && !$v.name_1.required" class="invalid-text mt-0 pl-4" style="position:absolute; bottom:-18px; left:-15px;"> 
            {{$t('select_item')}}
          </small> -->
          <small class="pos_absolute">4,0-10,0х10 <sup>9/л</sup></small>
        </div>
        <div style="position:relative;">
          <mdb-input :label="('Нейтрофилы (Neu#)')"  v-model="name_2" outline/>
          <!-- <small v-if="$v.name_2.$dirty   && !$v.name_2.required" class="invalid-text  pl-4" style="position:absolute; bottom:-18px; left:-15px;"> 
            {{$t('select_item')}}
          </small> -->
          <small class="pos_absolute">2,7-7,0х10 <sup>9/л</sup></small>
        </div>
        <div style="position:relative;">
          <mdb-input :label="('Лимфоциты (Lym#)')"  v-model="name_3" outline/>
          <!-- <small v-if="$v.name_3.$dirty && !$v.name_3.required" class="invalid-text  pl-4" style="position:absolute; bottom:-18px; left:-15px;"> 
            {{$t('select_item')}}
          </small> -->
          <small class="pos_absolute">0,8-4,0х10<sup>9/л</sup></small>
        </div>
        <div style="position:relative;">
          <mdb-input :label="('Моноциты (Mon#)')"  v-model="name_4" outline/>
          <!-- <small v-if="$v.name_4.$dirty && !$v.name_4.required" class="invalid-text  pl-4" style="position:absolute; bottom:-18px; left:-15px;"> 
            {{$t('select_item')}}
          </small> -->
          <small class="pos_absolute">0,12-1,20 х10 <sup>9/л</sup></small>
        </div>
        <div style="position:relative;">
          <mdb-input :label="('Эозонофилы ( Eos#)')"  v-model="name_5" outline/>
          <!-- <small v-if="$v.name_5.$dirty  && !$v.name_5.required" class="invalid-text  pl-4" style="position:absolute; bottom:-18px; left:-15px;"> 
            {{$t('select_item')}}
          </small> -->
          <small class="pos_absolute"> 0,02-0,50х10 <sup>9/л</sup></small>
        </div>
        <div style="position:relative;">
          <mdb-input :label="('Базофилы (Bas#)')"  v-model="name_6" outline/>
          <!-- <small v-if="$v.name_6.$dirty  && !$v.name_6.required" class="invalid-text  pl-4" style="position:absolute; bottom:-18px; left:-15px;"> 
            {{$t('select_item')}}
          </small> -->
          <small class="pos_absolute">0,00-0,10 х10 <sup>9/л</sup></small>
        </div>
        <div style="position:relative;">
          <mdb-input :label="('Нейтрофилы (Neu%)')"  v-model="name_7" outline/>
          <!-- <small v-if="$v.name_7.$dirty  && !$v.name_7.required" class="invalid-text  pl-4" style="position:absolute; bottom:-18px; left:-15px;"> 
            {{$t('select_item')}}
          </small> -->
          <small class="pos_absolute">50-70 %</small>
        </div>
        <div style="position:relative;">
          <mdb-input :label="('Лимфоциты (Lym%)')"  v-model="name_8" outline/>
          <!-- <small v-if="$v.name_8.$dirty  && !$v.name_8.required" class="invalid-text  pl-4" style="position:absolute; bottom:-18px; left:-15px;"> 
            {{$t('select_item')}}
          </small> -->
          <small class="pos_absolute">20-40 %</small>
        </div>
        <div style="position:relative;">
          <mdb-input :label="('Моноциты (Mon%)')"  v-model="name_9" outline/>
          <!-- <small v-if="$v.name_9.$dirty  && !$v.name_9.required" class="invalid-text  pl-4" style="position:absolute; bottom:-18px; left:-15px;"> 
            {{$t('select_item')}}
          </small> -->
          <small class="pos_absolute"> 3-12%</small>
        </div>
      </div>
      <div class="col-4" >
        <div style="position:relative;">
          <mdb-input :label="('Эозинофилы ( Eos%)')"  v-model="name_10" outline/>
          <!-- <small v-if="$v.name_10.$dirty && !$v.name_10.required" class="invalid-text  pl-4" style="position:absolute; bottom:-18px; left:-15px;"> 
            {{$t('select_item')}}
          </small> -->
          <small class="pos_absolute">1-6 %</small>
        </div>
        <div style="position:relative;">
          <mdb-input :label="('Базофилы (Bas%)')"  v-model="name_11" outline/>
          <!-- <small v-if="$v.name_11.$dirty && !$v.name_11.required" class="invalid-text  pl-4" style="position:absolute; bottom:-18px; left:-15px;"> 
            {{$t('select_item')}}
          </small> -->
          <small class="pos_absolute">0-1 %</small>
        </div>
        <div style="position:relative;">
          <mdb-input :label="('Эритроциты (RBS)')"  v-model="name_12" outline/>
          <!-- <small v-if="$v.name_12.$dirty  && !$v.name_12.required" class="invalid-text  pl-4" style="position:absolute; bottom:-18px; left:-15px;"> 
            {{$t('select_item')}}
          </small> -->
          <small class="pos_absolute">М:4,0-5,0 х10 <sup>9/л</sup>
            Ж: 3,9-4,7 х10 <sup>9/л</sup>
          </small>
        </div>
        <div style="position:relative;">
          <mdb-input :label="('Гемоглобин (HGB) ')"  v-model="name_13" outline/>
          <!-- <small v-if="$v.name_13.$dirty  && !$v.name_13.required" class="invalid-text  pl-4" style="position:absolute; bottom:-18px; left:-15px;"> 
            {{$t('select_item')}}
          </small> -->
          <small class="pos_absolute">
            М: 130-160 г/л
            Ж: 120-140 г/л/
          </small>
        </div>
        <div style="position:relative;">
          <mdb-input :label="('Гемотокрит (HCT)')"  v-model="name_14" outline/>
          <!-- <small v-if="$v.name_14.$dirty  && !$v.name_14.required" class="invalid-text  pl-4" style="position:absolute; bottom:-18px; left:-15px;"> 
            {{$t('select_item')}}
          </small> -->
          <small class="pos_absolute">М:40-48 %
            Ж:33-44 %
          </small>
        </div>
        <div style="position:relative;">
          <mdb-input :label="('Средний объем эритроцита в мкм/фл (MCV)')"  v-model="name_15" outline/>
          <!-- <small v-if="$v.name_15.$dirty  && !$v.name_15.required" class="invalid-text  pl-4" style="position:absolute; bottom:-18px; left:-15px;"> 
            {{$t('select_item')}}
          </small> -->
          <small class="pos_absolute">80,0-100,0 ф/л</small>
        </div>
        <div style="position:relative;">
          <mdb-input :label="('Средние содержание гемоглобина в  эритроците (MCH)')"  v-model="name_16" outline/>
          <!-- <small v-if="$v.name_16.$dirty  && !$v.name_16.required" class="invalid-text  pl-4" style="position:absolute; bottom:-18px; left:-15px;"> 
            {{$t('select_item')}}
          </small> -->
          <small class="pos_absolute">27.0-34.0</small>
        </div>
        <div style="position:relative;">
          <mdb-input :label="('Средняя концентрация гемоглобина в  эритроците (MCHС)')"  v-model="name_17" outline/>
          <!-- <small v-if="$v.name_17.$dirty   && !$v.name_17.required" class="invalid-text  pl-4" style="position:absolute; bottom:-18px; left:-15px;"> 
            {{$t('select_item')}}
          </small> -->
          <small class="pos_absolute">320-360</small>
        </div>
        <div style="position:relative;">
          <mdb-input :label="('Относительная ширина распределения эритроцитов по объёму, коффициент вариации (RDW-CV)')"  v-model="name_18" outline/>
          <!-- <small v-if="$v.name_18.$dirty && !$v.name_18.required" class="invalid-text  pl-4" style="position:absolute; bottom:-18px; left:-15px;"> 
            {{$t('select_item')}}
          </small> -->
          <small class="pos_absolute">11,0-16,0</small>
        </div>
        

        
      </div>
      <div class="col-4" >
        
        
        <div style="position:relative;">
          <mdb-input :label="('Относительнаяширинараспределенияэритроцитов по объему, стандартная (RDW-SD)')"  v-model="name_19" outline/>
          <!-- <small v-if="$v.name_19.$dirty  && !$v.name_19.required" class="invalid-text  pl-4" style="position:absolute; bottom:-18px; left:-15px;"> 
            {{$t('select_item')}}
          </small> -->
          <small class="pos_absolute">35,0-56,0 ф/л</small>
        </div>
        <div style="position:relative;">
          <mdb-input :label="('Тромбоциты (PLT)')"  v-model="name_20" outline/>
          <!-- <small v-if="$v.name_20.$dirty  && !$v.name_20.required" class="invalid-text  pl-4" style="position:absolute; bottom:-18px; left:-15px;"> 
            {{$t('select_item')}}
          </small> -->
          <small class="pos_absolute">180-320 х10<sup>9/л</sup></small>
        </div>
        <div style="position:relative;">
          <mdb-input :label="('Средний объём тромбоцитов (MPV)')"  v-model="name_21" outline/>
          <!-- <small v-if="$v.name_21.$dirty  && !$v.name_21.required" class="invalid-text  pl-4" style="position:absolute; bottom:-18px; left:-15px;"> 
            {{$t('select_item')}}
          </small> -->
          <small class="pos_absolute">7-11 ф/л</small>
        </div>
        <div style="position:relative;">
          <mdb-input :label="('Относительная ширина распределения тромбоцитов (RDW)')"  v-model="name_22" outline/>
          <!-- <small v-if="$v.name_22.$dirty  && !$v.name_22.required" class="invalid-text  pl-4" style="position:absolute; bottom:-18px; left:-15px;"> 
            {{$t('select_item')}}
          </small> -->
          <small class="pos_absolute">9,0-17,0</small>
        </div>
        <div style="position:relative;">
          <mdb-input :label="('Тромбоциты (PCT)')"  v-model="name_23" outline/>
          <!-- <small v-if="$v.name_23.$dirty  && !$v.name_23.required" class="invalid-text  pl-4" style="position:absolute; bottom:-18px; left:-15px;"> 
            {{$t('select_item')}}
          </small> -->
          <small class="pos_absolute">0,108-0,252%</small>
        </div>
        <div style="position:relative;">
          <mdb-input :label="('Скорость Оседания Эритроцитов (COЭ)')"  v-model="name_24" outline/>
          <!-- <small v-if="$v.name_24.$dirty  && !$v.name_24.required" class="invalid-text  pl-4" style="position:absolute; bottom:-18px; left:-15px;"> 
            {{$t('select_item')}}
          </small> -->
          <small class="pos_absolute">М:0-10Мм/час
            Ж: 0-15 Мм/час
          </small>
        </div>
        <!-- <div style="position:relative;">
          <mdb-input :label="('Коэффисент атерогенности')"  v-model="name_25" outline/>
   
          <small class="pos_absolute">≤ 3,5</small>
        </div>
        <div style="position:relative;">
          <mdb-input :label="('Натрий')"   v-model="name_26" outline/>

          <small class="pos_absolute">135-155 mmol/L</small>
        </div> -->

        
      </div>
    </div>
    <div class="text-right mx-4 py-2 border-top">
      <mdb-btn color="primary" class="m-0 p-0"   p="r4 l4 t2 b2" @click="send" style="font-size:10px;">{{$t('Send')}}</mdb-btn>  
    </div>
    <Toast ref="message"></Toast>
    <AlertError ref="msg"></AlertError>
   <massage_box :hide="modal_status" :detail_info="modal_info"
      :m_text="$t('Failed_to_add')" @to_hide_modal = "modal_status= false"/>
  </div>
</template>

<script>
import {mdbBtn, mdbInput} from "mdbvue";
import {mapActions, mapGetters} from 'vuex'
import { required } from 'vuelidate/lib/validators'
import lineSelect from "../../components/hospital/cashUserSelectForAnalysis.vue";
export default {
  components: {
    mdbBtn,mdbInput,lineSelect
  },
  validations: {
      patient_name : {required},
      doctor_name : {required}
    },
  data(){
    return {
      loading : false,
      modal_info : '',
      modal_status: false,

      patient_name: '',
      patient_id: null,
      name: '',
      id: this.$route.params.id,
      doctor_name: localStorage.docName,
      numberEnter: null,


      name_1: '',
      name_2: '',
      name_3: '',
      name_4: '',
      name_5: '',
      name_6: '',
      name_7: '',
      name_8: '',
      name_9: '',
      name_10: '',
      name_11: '',
      name_12: '',
      name_13: '',
      name_14: '',
      name_15: '',
      name_16: '',
      name_17: '',
      name_18: '',
      name_19: '',
      name_20: '',
      name_21: '',
      name_22: '',
      name_23: '',
      name_24: '',
      name_25: '',
      name_26: '',
      name_27: '',
    }
  },
  async created()
    {
      if(this.id > 0)
      {
        const res = await fetch(this.$store.state.hostname + '/HospitalAnaliz_4/' + this.id);
        const data = await res.json();
        console.log(data)
          this.name_1= data.name_1
          this.name_2= data.name_2
          this.name_3= data.name_3
          this.name_4= data.name_4
          this.name_9= data.name_9
          this.name_5= data.name_5
          this.name_6= data.name_6
          this.name_7= data.name_7
          this.name_8= data.name_8
          this.name_10= data.name_10
          this.name_11= data.name_11
          this.name_12= data.name_12
          this.name_13= data.name_13
          this.name_14= data.name_14
          this.name_15= data.name_15
          this.name_16= data.name_16
          this.name_17= data.name_17
          this.name_18= data.name_18
          this.name_19= data.name_19
          this.name_20= data.name_20
          this.name_21= data.name_21
          this.name_22= data.name_22
          this.name_23= data.name_23
          this.name_24= data.name_24
          this.name_25= data.name_25
          this.name_26= data.name_26
          this.name_27= data.name_27
          this.id = data.id
          this.patient_id =   data.patientsId;
          this.patient_name = data.patient_name;
          this.doctor_name =  data.doctor_name;
      }

    },
  computed:{
    ...mapGetters(['get_patient_client_list']),
    filteredList: function(){
      if(this.search)
      {
        return this.get_patient_client_list.filter((item)=>{
          return this.search.toLowerCase().split(' ').every(v => item.fio.toLowerCase().includes(v))
        })
      }else
      {
        return this.get_patient_client_list;
      }
      }
  },
  mounted(){
    this.fetch_patient_client();
  },
  methods:{
    ...mapActions(['fetch_patient_client']),
    selectPatient(option){
      console.log(option)
      this.patient_name = option.data.fio;
      this.patient_id = option.data.id;
    },
    cls_wnd(){
      this.name_1 = '';
      this.name_2 = '';
      this.name_3 = '';
      this.name_4 = '';
      this.name_5 = '';
      this.name_6 = '';
      this.name_7 = '';
      this.name_8 = '';
      this.name_9 = '';
      this.name_10 = '';
      this.name_11 = '';
      this.name_12 = '';
      this.name_13 = '';
      this.name_14 = '';
      this.name_15 = '';
      this.name_16 = '';
      this.name_17 = '';
      this.name_18 = '';
      this.name_19 = '';
      this.name_20 = '';
      this.name_21 = '';
      this.name_22 = '';
      this.name_23 = '';
      this.name_24 = '';
      this.name_25 = '';
      this.name_26 = '';
      this.name_27 = '';
    },
        async submit(){
      try{
        this.loading = true;
        const response = await fetch(this.$store.state.hostname + '/HospitalMarkSeviceTyesWithNumber/getFullInfoById?id=' + this.numberEnter)
        const data = await response.json()
        console.log('data')
        console.log(data)
        if(data.id){
          this.patient_name = data.patients.fio;
          this.patient_id = data.patients.id;
          this.loading = false;
          this.$refs.message.success('Added_successfully')
        }
        else{
          // this.$refs.msg.error('Error_successfully')
          this.loading = false;
          this.modal_info = data.detail + " (" + data.routine + ")";
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
    async sendFinish(){
      if(this.numberEnter){
        try{
          this.loading = true;
          const response = await fetch(this.$store.state.hostname + '/HospitalMarkSeviceTyesWithNumber/getCheckedStatusAnaliz?id=' + this.numberEnter)
          const data = await response.json()
          console.log('data')
          console.log(data)
          if(data.id){
            this.loading = false;
            this.$refs.message.success('Added_successfully')
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
      }
    },
    async send(){
      
      try{
        if(this.$v.$invalid )
        {
          this.$v.$touch();
          this.$refs.message.warning('please_fill')
          return false;
        }
        const requestOptions = {
          method : "POST",
          headers: { "Content-Type" : "application/json" },
          body: JSON.stringify({
          "name_1": this.name_1,
          "name_2": this.name_2,
          "name_3": this.name_3,
          "name_4": this.name_4,
          "name_9": this.name_9,
          "name_5": this.name_5,
          "name_6": this.name_6,
          "name_7": this.name_7,
          "name_8": this.name_8,
          "name_10": this.name_10,
          "name_11": this.name_11,
          "name_12": this.name_12,
          "name_13": this.name_13,
          "name_14": this.name_14,
          "name_15": this.name_15,
          "name_16": this.name_16,
          "name_17": this.name_17,
          "name_18": this.name_18,
          "name_19": this.name_19,
          "name_20": this.name_20,
          "name_21": this.name_21,
          "name_22": this.name_22,
          "name_23": this.name_23,
          "name_24": this.name_24,
          "name_25": this.name_25,
          "name_26": this.name_26,
          "name_27": this.name_27,

          "id": this.id,
          "patientsId":  this.patient_id,
          "device_name": "obshey_krov_print",
          "doctor_name": this.doctor_name,
          "creator_doctor_name":  localStorage.docName
          })
        };
        this.loading = true;
        const response = await fetch(this.$store.state.hostname + '/HospitalAnaliz_4', requestOptions)
        const data = await response.json()
          console.log(data)

        if(data.id){
          this.cls_wnd();
          this.sendFinish();
          this.loading = false;
          this.$refs.message.success('Added_successfully')
          this.$router.push('/obshey_krov_print/' + data.id)
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
      
    }
  }
}
</script>

<style>
.pos_relative{
  position: relative;
}
.pos_absolute{
  position: absolute;
  top:37px;
  left:5px;
  color:rgb(45, 25, 170);
}
</style>