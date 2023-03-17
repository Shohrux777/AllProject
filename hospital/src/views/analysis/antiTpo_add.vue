<template>
  <div class="bakteriya bg-white" style="min-height: 100vh;">
    <div class="header text-center border-bottom py-2">
      <h4 class="text-primary">Кондаги гормонларни текшириш</h4>
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
      <small v-if="$v.patient_name.$dirty && patient_id == null" class="invalid-text pt-4 pl-4" >
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
      <div class="col-6" >
        <div style="position:relative;">
          <mdb-input :label="('Тиретроп гормон /ТТГ/')"  v-model="ttg" outline/>
          <small v-if="$v.ttg.$dirty  && !$v.ttg.required" class="invalid-text pl-4" style="position:absolute; bottom:-18px; left:-15px;"> 
            {{$t('select_item')}}
          </small>
          <small class="pos_absolute">0,35-5,1  мкМЕ/мл</small>
        </div>
        <div style="position:relative;">
          <mdb-input :label="('ДЕАС (Дегидроепиандростерон)')"  v-model="deac" outline/>
          <small v-if="$v.deac.$dirty   && !$v.deac.required" class="invalid-text pl-4" style="position:absolute; bottom:-18px; left:-15px;"> 
            {{$t('select_item')}}
          </small>
          <small class="pos_absolute">Мужчины : 15-498  мкг/дл</small>
        </div>
        <div style="position:relative;">
          <mdb-input :label="('Общий трийодтиронин/Т3/')"  v-model="obshiy_t3" outline/>
          <small v-if="$v.obshiy_t3.$dirty && !$v.obshiy_t3.required" class="invalid-text pl-4" style="position:absolute; bottom:-18px; left:-15px;"> 
            {{$t('select_item')}}
          </small>
          <small class="pos_absolute"></small>
        </div>
        <!-- <div style="position:relative;">
          <mdb-input :label="('Трийодтиронин свободный /Т3/ ')"  v-model="svabodniy_t3" outline/>
          <small v-if="$v.svabodniy_t3.$dirty && !$v.svabodniy_t3.required" class="invalid-text pl-4" style="position:absolute; bottom:-18px; left:-15px;"> 
            {{$t('select_item')}}
          </small>
          <small class="pos_absolute"></small>
        </div> -->
        <!-- <div style="position:relative;">
          <mdb-input :label="('Тироксин /Т4/ ')"  v-model="t_4" outline/>
          <small v-if="$v.t_4.$dirty && !$v.t_4.required" class="invalid-text pl-4" style="position:absolute; bottom:-18px; left:-15px;"> 
            {{$t('select_item')}}
          </small>
          <small class="pos_absolute">4,7-12,4 mkg/dl</small>
        </div> -->
        <div style="position:relative;">
          <mdb-input :label="('Тироксин /Т4 свободного/')"  v-model="t_4_svabodno" outline/>
          <small v-if="$v.t_4_svabodno.$dirty && !$v.t_4_svabodno.required" class="invalid-text pl-4" style="position:absolute; bottom:-18px; left:-15px;"> 
            {{$t('select_item')}}
          </small>
          <small class="pos_absolute">0,5-1,4 нг/мл</small>
        </div>
        
        <div style="position:relative;">
          <mdb-input :label="('Тестостерон')"  v-model="testosteron" outline/>
          <small v-if="$v.testosteron.$dirty  && !$v.testosteron.required" class="invalid-text pl-4" style="position:absolute; bottom:-18px; left:-15px;"> 
            {{$t('select_item')}}
          </small>
          <small class="pos_absolute">Мужчины :2,27-9,76 нг/мл
            Женщины  ≤1,23 нг/мл</small>
        </div>
      </div>
      <div class="col-6">
        <div style="position:relative;">
          <mdb-input :label="('Эстрадиол')"  v-model="estradiol" outline/>
          <small v-if="$v.estradiol.$dirty  && !$v.estradiol.required" class="invalid-text pl-4" style="position:absolute; bottom:-18px; left:-15px;"> 
            {{$t('select_item')}}
          </small>
          <small class="pos_absolute">Мужчины  28-84 пг/мл Женщины:фолликулярная20-138 пг/мл</small>
        </div>
        <div style="position:relative;">
          <mdb-input :label="('Пролактин ')"  v-model="prolaktin" outline/>
          <small v-if="$v.prolaktin.$dirty  && !$v.prolaktin.required" class="invalid-text pl-4" style="position:absolute; bottom:-18px; left:-15px;"> 
            {{$t('select_item')}}
          </small>
          <small class="pos_absolute">Мужчины:  3,0-16,5 нг/мл
            Женщины: в пре-менопауза 3,8-30,7нг/мл</small>
        </div>
        <div style="position:relative;">
          <mdb-input :label="('ФСГ')"  v-model="fcg" outline/>
          <small v-if="$v.fcg.$dirty  && !$v.fcg.required" class="invalid-text pl-4" style="position:absolute; bottom:-18px; left:-15px;"> 
            {{$t('select_item')}}
          </small>
          <small class="pos_absolute">Мужчины:  1,5-12,4  мМЕ/мл
            Женщины : фолликулярная 3,5-12,5 мМЕ/мл</small>
        </div>
        <div style="position:relative;">
          <mdb-input :label="('ЛГ')"  v-model="lg" outline/>
          <small v-if="$v.lg.$dirty  && !$v.lg.required" class="invalid-text pl-4" style="position:absolute; bottom:-18px; left:-15px;"> 
            {{$t('select_item')}}
          </small>
          <small class="pos_absolute">Мужчины:  1,4-7,7 мМЕ/мл
            Женщины : фолликулярная  1,9-11,6 мМЕ/мл</small>
        </div>
        <div style="position:relative;">
          <mdb-input :label="('Anti-TPO')"  v-model="progesteron" outline/>
          <small v-if="$v.progesteron.$dirty  && !$v.progesteron.required" class="invalid-text pl-4" style="position:absolute; bottom:-18px; left:-15px;"> 
            {{$t('select_item')}}
          </small>
          <small class="pos_absolute">9 - ME/мл</small>
        </div>
        <div style="position:relative;" class="mb-5">
          <mdb-input :label="('Прогестерон')"  v-model="name_11" outline/>
          <!-- <small v-if="$v.progesteron.$dirty  && !$v.progesteron.required" class="invalid-text pl-4" style="position:absolute; bottom:-18px; left:-15px;"> 
            {{$t('select_item')}}
          </small> -->
          <small class="pos_absolute">Мужчины:  0,1-2,1 нг/мл
            Женщины :не беременные :  
            фолликулярная 0,2- 1,6 нг/мл
            овуляция 0,3-2,1 нг/мл
            созревания желтова тела 1,8-22,5 нг/мл
          </small>
        </div>
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
      deac : {required},
      ttg : {required},
      // t_4 : {required},
      t_4_svabodno : {required},
      obshiy_t3: {required},
      // svabodniy_t3: {required},
      testosteron : {required},
      estradiol : {required},
      prolaktin : {required},
      fcg: {required},
      lg: {required},
      progesteron: {required},
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
      doctor_name: localStorage.docName,
      id: this.$route.params.id,

      ttg: '',
      deac: '',
      obshiy_t3: '',
      svabodniy_t3: '',
      t_4: '',
      t_4_svabodno: '',
      testosteron: '',
      estradiol: '',
      prolaktin: '',
      fcg: '',
      lg: '',
      progesteron: '',
      name_11: '',

      numberEnter: null,
    }
  },
  async created()
    {
      console.log(this.id)
      if(this.id > 0)
      {
        const res = await fetch(this.$store.state.hostname + '/HospitalAnaliz_2/getFullInfoById?id=' + this.id);
        const data = await res.json();
        console.log(data)
          this.deac= data.name_2, 
          this.ttg= data.name_1,
          this.obshiy_t3 = data.name_3
          // this.svabodniy_t3 = data.svabodniy_t3
          // this.t_4= data.t_4,
          this.t_4_svabodno= data.name_4,
          this.testosteron= data.name_5,
          this.estradiol= data.name_6,
          this.prolaktin= data.name_7,
          this.fcg = data.name_8,
          this.lg = data.name_9,
          this.progesteron = data.name_10,
          this.id = data.id
          this.patient_id =   data.patientsId;
          this.patient_name = data.patient_name
          this.doctor_name =  data.doctor_name;
          this.name_11 = data.name_11;
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
      this.ttg = '';
      this.deac = '';
      this.t_4= '';
      this.t_4_svabodno= '';
      this.testosteron= '';
      this.estradiol= '';
      this.prolaktin= '';
      this.fcg = '';
      this.lg = '';
      this.progesteron = '';
      this.obshiy_t3 = '';
      this.svabodniy_t3 = '';
      this.name_11 = '';
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
          "name_2": this.deac,
          "name_1": this.ttg,
          "name_3": this.obshiy_t3,
          "name_4": this.t_4_svabodno,
          "name_5": this.testosteron,
          "name_6": this.estradiol,
          "name_7": this.prolaktin,
          "name_8" : this.fcg,
          "name_9" : this.lg,
          "name_10" : this.progesteron,
          "name_11" : this.name_11,

          "id": this.id,
          "patientsId":  this.patient_id,
          "device_name": "antiTpo_print",
          "doctor_name": this.doctor_name,
          "creator_doctor_name": localStorage.docName
          })
        };
        this.loading = true;
        const response = await fetch(this.$store.state.hostname + '/HospitalAnaliz_2', requestOptions)
        const data = await response.json()
        console.log(data)
        if(data.id){
          this.cls_wnd()
          this.sendFinish()
          this.loading = false;
          this.$refs.message.success('Added_successfully')
          this.$router.push('/antiTpo_print/' + data.id)
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