<template>
  <div class="add_patient px-4">
    <div class="row mt-2">
        <div class="col-7">
          <div class="w-100 timePicer">
            <label class="dayLabel">{{$t('Room')}}</label>
            <mdb-input :value="room_info.name" disabled class="m-0 p-0 mt-2 "  size="sm" outline type="text" />
          </div>
        </div>
        <div class="col-5">
          <div class="w-100 timePicer">
            <label class="dayLabel">{{$t('Bed')}}</label>
            <mdb-input v-model="bed_info.name" disabled class="m-0 p-0 mt-2 "  size="sm" outline type="text" />
          </div>
        </div>
      </div>
    <loader v-if="loading"/>
    <form v-else v-on:submit.prevent="submit">
      <div>
        <lineSelectSearch
              class="mt-1"
              :options="get_patient_list_last.rows"
              :searchshow="true"
              @select="selectPatient"
              :selected="patient_name"
              url="/Patients/searchPatientsByFioList?FIO="
              :label="$t('patient list')"
          />
            <small v-if="$v.patient_name.$dirty && patient_id == null" class="invalid-text pt-4" >
              {{$t('select_item')}}
          </small>
      </div>
      <div class="mt-3">
        <div style="position: relative; margin-top: 0px;"> 
          <lineSelect
          :options="auth_user_list"
          :searchshow="true"
          @select="selectcontragent"
          :selected="contragent_name"
          :label="$t('contragent')"
          />
        </div>
      </div>
      <div class="d-flex  mt-4 w-100">
        <div class="timePicer mr-1 w-50">
          <label  class="timeLabel">{{$t('Start_time')}}</label>
          <date-picker class="timepiker " id="fordo" style="margin-top: 10px; width: 100%;" v-model="Start_time" format="DD-MM-YYYY HH:mm:ss"  type="datetime"></date-picker>
        </div>   
        
        <div class="timePicer ml-1 w-50">
          <label class="timeLabel">{{$t('End_time')}}</label>
          <date-picker class="timepiker" id="fordo" @input="endTimeChange" style="margin-top: 10px; width: 100%;" v-model="End_time" format="DD-MM-YYYY HH:mm:ss"  type="datetime"></date-picker>
        </div>   
        
      </div>
      <div class="d-flex timePicer mt-2">
       
      </div>
      <div class=" mt-3 d-flex">
        <div class="w-25 timePicer">
          <label class="dayLabel">{{$t('day_count')}}</label>
          <mdb-input v-model="days" @input="dayChange" class="m-0 p-0 mt-2 "  size="sm" outline type="number" />
        </div>
        <div class="w-75 d-flex ml-3">
          <input type="checkbox" class="mt-1" style="width: 15px; height: 15px;" v-model="operatsion_show" id="operatsion" />
          <div class="d-flex align-items-center" style="height: 25px;">
            <label style="font-weight:500; font-size:13.5px; cursor:pointer;"  for="operatsion" 
            class="mt-2 ml-2 text-primary">Операционный пациент</label>
          </div>
        </div>
      </div>
      <div class="w-100 timePicer mt-3">
        <label class="dayLabel">{{$t('note')}}</label>
        <mdb-input v-model="note"  class="m-0 p-0 mt-2 "  size="sm" outline type="textarea" />
      </div>

      <div class="row mt-3" style="position:relative;">
        <div class="col-4 cursor_h" v-for="(item_price, i) in price_list" :key="i">
          <div class=" timePicer mt-2 w-100" :class="{'bg-success text-white cursor_p' : price_activ == i}" @click="func_activ_price(i, item_price)">
            <label class="priceLabel" >{{$t(item_price.name)}}</label>
            <mdb-input disabled v-model="item_price.price" class="m-0 p-0 mt-2 "  size="sm" outline type="number" />
          </div>
        </div>
        <small v-if="$v.price_name.$dirty && price == 0" style="position:absolute; bottom:-10px; left:5px;" 
          class="invalid-text " >
              {{$t('select_item')}}
          </small>
      </div>

      <!-- <div class=" mt-3 d-flex w-100 justify-content-between">
        <div class=" timePicer mt-2" style="width:30%;">
          <label class="dayLabel">{{$t('patient')}}</label>
          <mdb-input disabled v-model="room_info.hospitalRoomType.room_bed_price" class="m-0 p-0 mt-2 "  size="sm" outline type="number" />
        </div>
        <div class=" timePicer mt-2" style="width:30%;">
          <label class="dayLabel">{{$t('Room')}}</label>
          <mdb-input disabled v-model="room_info.hospitalRoomType.room_price" class="m-0 p-0 mt-2 "  size="sm" outline type="number" />
        </div>
        <div class=" timePicer mt-2" style="width:30%;">
          <label class="dayLabel">{{$t('care_patient')}}</label>
          <mdb-input disabled v-model="room_info.hospitalRoomType.room_bed_price_not_patient" class="m-0 p-0 mt-2 "  size="sm" outline type="number" />
        </div>
        
        
      </div> -->
      
      <div class="mt-4" v-show="false">
        <hpSelect
              class="mt-1"
              :options="price_type_room.rows"
              :searchshow="true"
              @select="selectPriceType"
              :selected="price_type_name"
              :label="$t('price_type')"
          />
            <!-- <small v-if="$v.price_type_name.$dirty && price_type_id == null" class="invalid-text pt-4" >
              {{$t('select_item')}}
          </small> -->
      </div>
      

            
      <div class="text-right border-top mt-4">
          <mdb-btn   color="danger" @click="close" class="ml-2 mt-2" style="font-size: 9px;" p="r4 l4 t2 b2">{{$t('close')}}</mdb-btn>  
          <mdb-btn   color="success"  type="submit" class="ml-1 mt-2" style="font-size: 9px;" p="r4 l4 t2 b2">{{$t('save')}}</mdb-btn>  
      </div>
    </form>
      <Toast ref="message"></Toast>
      <AlertError ref="msg"></AlertError>

      <massage_box :hide="modal_status" :detail_info="modal_info"
          :m_text="$t('Failed_to_add')" @to_hide_modal = "modal_status= false"/>
  </div>
</template>

<script>
import DatePicker from 'vue2-datepicker';
import lineSelectSearch from "../../components/lineSelectSearch.vue";
import lineSelect from "../../components/lineSelectAuth.vue";
import hpSelect from "../../components/lineSelect.vue";
import 'vue2-datepicker/index.css';
import {mdbBtn, mdbInput} from 'mdbvue'
import {mapActions, mapGetters} from 'vuex'
import { required } from 'vuelidate/lib/validators'

export default {
  components: { 
     mdbBtn, DatePicker,mdbInput, lineSelectSearch, hpSelect,lineSelect
  },
  data(){
    return{
      modal_info : '',
      modal_status: false,
      loading: false,

      patient_name: '',
      patient_id: null,
      price: 0,
      price_name: '',
      price_activ: -1,
      price_list: [],
      Start_time: new Date(),
      End_time: new Date(),
      days: 0,
      note: '',

      price_type_name: '',
      price_type_id: null,

      contragent_name: '',
      contragent_id: 0,

      operatsion_show: false,
    }
  },
  validations: {
    patient_name: { required },
    price_name: {required}
    // price_type_name: { required },
  },
  props: {
      room_info:
      {
        type: Object,
        default(){
          return {}
        }
      },
      bed_info:
      {
        type: Object,
        default(){
          return {}
        }
      },
    },
  computed: mapGetters(['get_patient_list_last', 'price_type_room', 'auth_user_list']),
  methods: {
    ...mapActions(['fetch_patient_list_last', 'fetch_price_type_room','fetch_auth_list', 'fetch_bron_room_pagination']),
    selectPatient(option){
      this.patient_name = option.data.fio;
      this.patient_id = option.data.id;
    },
    refresh(roomType){
      console.log('refresh ishladi')
      console.log(roomType)
      this.price = 0;
      this.price_activ = -1;
      this.price_list = [];
      this.price_list.push({name:'patient', price: roomType.room_bed_price})
      this.price_list.push({name:'for_room', price: roomType.room_price})
      this.price_list.push({name:'care_patient', price: roomType.room_bed_price_not_patient})
    },
    func_activ_price(i, data){
      console.log(data)
      this.price_activ = i;
      this.price = data.price;
      this.price_name = data.name;
    },
    selectPriceType(option){
      this.price_type_name = option.data.name;
      this.price_type_id = option.data.id;
    },
    async selectcontragent(option){
        console.log('option')
        console.log(option)
        this.contragent_id = option.data.usersId
        this.contragent_name = option.data.users.fio
    },
    dayChange(){
      if(this.days == null || this.days == '')return;
      let LastDays = new Date(this.Start_time);
      let last_date_minuss = LastDays.setDate(LastDays.getDate()+parseInt(this.days));
      let last_date_str = new Date(last_date_minuss);
      console.log(last_date_str);
      this.End_time = new Date(last_date_minuss);
      console.log(this.Start_time.toISOString());
      console.log(this.End_time.toISOString());
      console.log(this.Start_time.toLocaleTimeString('it-IT'));
      console.log(this.End_time.toLocaleTimeString('it-IT'));
    },
    endTimeChange(){
      const oneDay = 24 * 60 * 60 * 1000; // hours*minutes*seconds*milliseconds
      console.log('this.Start_time')
      console.log(this.Start_time)
      console.log(this.End_time)
      const firstDate = new Date(this.Start_time);
      const secondDate = new Date(this.End_time);

      const diffDays = Math.round(Math.abs((firstDate - secondDate) / oneDay));
      console.log(diffDays)
      this.days = diffDays;

    },
    cls_wnd(){
      this.patient_name = '';
      this.patient_id = null;
      this.Start_time = new Date();
      this.End_time = new Date();
      this.days = 0;
      this.note = '';
      this.price_type_id = null;
      this.price_type_name = '';
    },
    async submit(){
      if(this.$v.$invalid)
        {
          this.$v.$touch();
          this.$refs.message.warning('please_fill')
          return false;
        }
        console.log('this.Start_time.toISOString()')
        console.log(this.Start_time.toISOString())
        console.log(this.End_time.toISOString())
      var need_summ = 0;
      var discount_summ = 0;
      var mount_day = 0;
      
      if(this.operatsion_show){
        mount_day = this.days - 0;
        discount_summ = this.price * 0;
        need_summ = mount_day * this.price;
      }
      else{
        need_summ = this.days*this.price;
      }
        console.log(this.Start_time)
        const requestOptions = {
          method : "POST",
          headers: { "Content-Type" : "application/json" },
          body: JSON.stringify({
          "hospitalBronRoomNid" : this.room_info.id,
          "hospitalBronRoomItemNid" : this.bed_info.id,
          "patientsId" : this.patient_id,
          "usersId" : this.contragent_id,
          "begin_date_bron" : this.Start_time.toISOString().slice(0,11) + this.Start_time.toLocaleTimeString('it-IT') + '.000Z',
          "end_date_bron" : this.End_time.toISOString().slice(0,11) + this.End_time.toLocaleTimeString('it-IT') + '.000Z',
          "price_for_one_day": this.price,
          "payed_summ": 0,
          "card_summ": 0,
          "cash_summ": 0,
          "need_payed_summ": need_summ,
          "discount_payed_summ": discount_summ,
          "dolg_payed_summ": 0,
          "discount_pesantage": 0,
          "reserved_status_1": this.operatsion_show,
          "reserved_number_1": this.days,
          "reserved_name_1": this.patient_name,
          "reserved_name_2": this.room_info.name,
          "finish_payment": false,
          // "authorizationId": localStorage.AuthId,
          // "note": this.note,
          "id" : 0,
          })
        };
        console.log(requestOptions.body)
        this.loading = true;
        try{
          const response = await fetch(this.$store.state.hostname + '/HospitalBronRoomPayments', requestOptions)
          const data = await response.json()
          this.loading = false;
          if(data.id){
            console.log('add bron room connect ')
            console.log(data)
            // await this.fetchAddPriceType(data.id);
            this.$emit('close')
            this.$refs.message.success('Added_successfully');
            this.cls_wnd()
          }
          else{
            this.$refs.msg.error('Error_successfully')
            // this.modal_info = data.detail + "    (" + data.routine + ")";
            // this.modal_status = true;
            return false;
          } 
        }
        catch{
          this.$refs.msg.error('Error_successfully')
        }
        
    },
    close(){
      this.$emit('close');
    },
    async fetchAddPriceType(id){
      const requestOptions = {
          method : "POST",
          headers: { "Content-Type" : "application/json" },
          body: JSON.stringify({
          "hospitalPatientBronBedsid" : id,
          "hospitalBedsTypeAndPriceid": this.price_type_id,
          "id" : 0,
          })
        };
        console.log(requestOptions.body)
        this.loading = true;
        try{
          const response = await fetch(this.$store.state.hostname + '/HospitalBedsBronPaymnetsInfoes', requestOptions)
          const data = await response.json()
          this.loading = false;
          if(data.id){
            console.log('add bron room connect ')
            console.log(data)
          }
          else{
            this.$refs.msg.error('Error_successfully')
            return false;
          } 
        }
        catch{
          this.$refs.msg.error('Error_successfully')
        }
    },
  },
  async mounted(){
      await this.fetch_auth_list();
      await this.fetch_patient_list_last();
      await this.fetch_price_type_room();
      console.log('this.get_patient_list_last');
      console.log(this.get_patient_list_last);

  },
}
</script>

<style lang="scss">
.timePicer{
  position: relative;
  .timeLabel{
    position: absolute;
    font-size: 12px;
    background-color: #fff;
    padding: 1px 3px;
    z-index: 1;
    left: 6px;
    top: -1px;
    color:#8E959B;
  }
  .dayLabel{
    position: absolute;
    font-size: 11px;
    background-color: #fff;
    padding: 0px 3px;
    z-index: 1;
    left: 6px;
    top: -10px;
    color:#8E959B;
  }
  .priceLabel{
    position: absolute;
    font-size: 11px;
    background-color: #fff;
    padding: 0px 3px;
    z-index: 1;
    left: 6px;
    top: -17px;
    color:#8E959B;
  }

}
.cursor_p{
  cursor:pointer;
}
.cursor_h:hover{
  cursor:pointer;
}
</style>