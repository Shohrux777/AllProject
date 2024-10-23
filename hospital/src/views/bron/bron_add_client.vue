<template>
   <div class="client_add_show_bron d-flex w-100">
        <div style="width:55%">
            <div class="add_patient px-4">
                <div class="row mt-2">
                    <div class="col-12">
                        <div class="w-100 timePicer">
                        <label class="dayLabel">{{$t('patient')}}</label>
                        <mdb-input :value="patient.patient_name" disabled class="m-0 p-0 mt-2 "  size="sm" outline type="text" />
                        <!-- <p>{{ patient }}</p> -->
                        <!-- :value="patient.name" -->
                        </div>
                    </div>
                    </div>
                <loader v-if="loading"/>
                <form v-else v-on:submit.prevent="submit">
                    <div class="row mt-2">
                      <div class="col-6 pr-2">
                        <div style="position: relative; margin-top: 0px;"> 
                          <lineSelect_room_price
                          :options="get_bron_room_list_pagination"
                          :searchshow="true"
                          @select="select_bron_room"
                          :selected="bron_room_name"
                          :label="$t('bron_rooms')"
                          />
                        </div>
                      </div>
                      <div class="col-6 pl-2">
                        <div style="position: relative; margin-top: 0px;"> 
                          <lineSelect_room_price
                          :options="bron_room_beds_list"
                          :searchshow="true"
                          @select="select_bron_bed"
                          :selected="bron_bed_name"
                          :label="$t('bron_beds')"
                          />
                        </div>
                      </div>
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

                          <small v-if="$v.contragent_name.$dirty && contragent_id == null" style="position:absolute; bottom:-15px; left:5px;" 
                            class="invalid-text " >
                                {{$t('select_item')}}
                            </small>
                    </div>
                    </div>
                    <div class="mt-3">
                    <div style="position: relative; margin-top: 0px;"> 
                      <lineSelect_room_price
                      :options="price_type_room.rows"
                      :searchshow="true"
                      @select="select_room_price"
                      :selected="price_room_name"
                      :label="$t('price_room')"
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
                    <div class=" mt-3 d-flex" style="position: relative;">
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
                          <div style="width:120px;" v-show="operatsion_show" class="timePicer ml-3">
                            <label class="dayLabel">Бесп. день</label>
                            <mdb-input v-model="free_day" class="m-0 p-0 mt-2 "  size="sm" outline type="number" />
                          </div>
                      </div>
                      <small v-if="days == 0" style="position:absolute; bottom:-10px; left:5px;" 
                      class="invalid-text">
                          {{$t('select_item')}}
                      </small>
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
                        <mdb-btn color="danger" @click="close" class="ml-2 mt-2" style="font-size: 9px;" p="r4 l4 t2 b2">{{$t('close')}}</mdb-btn>  
                        <mdb-btn color="success"  type="submit" class="ml-1 mt-2" style="font-size: 9px;" p="r4 l4 t2 b2">{{$t('Send')}}</mdb-btn>  
                    </div>
                </form>
                    
                </div>
        </div>
        <div style="width:45%">
            <div class="item_list_patient_bron_info p-2 mt-2 w-100" v-for="(item, i) in get_bron_list_patient_id.list" :key="i">
              <!-- {{ get_bron_list_patient_id }} -->
              <div class="d-flex w-100 ">
                <div style="width:40%" >
                  <div class="alert-info rounded text-center">
                    <small class="font-weight-bold">Нач:</small>
                    <small class="ml-2">{{ item.begin_date_bron.slice(0,10) }}</small>
                  </div>
                </div>
                <div style="width:40%" >
                  <div class="alert-info rounded text-center">
                    <small class="font-weight-bold">Окон:</small>
                    <small class="ml-2">{{ item.end_date_bron.slice(0,10) }}</small>
                  </div>
                </div>
                <div style="width:20%" >
                  <div class="alert-info rounded text-center">
                    <small class="font-weight-bold">День:</small>
                    <small class="ml-2">{{ item.reserved_number_1 }}</small>
                  </div>
                </div>
              </div>
              <div class="pay_need_payed_patient d-flex w-100  ">
                <div style="width:33%">
                  <div class="alert-success rounded text-center">
                    <small class="font-weight-bold">Опл:</small>
                    <small class="ml-2">{{ item.payed_summ }}</small>
                  </div>
                </div>
                <div style="width:33%">
                  <div class="alert-danger rounded text-center">
                    <small class="font-weight-bold">Неопл:</small>
                    <small class="ml-2">{{ item.need_payed_summ }}</small>
                  </div>
                </div>
                <div style="width:34%">
                  <div class="alert-primary rounded text-center">
                    <small class="font-weight-bold">День:</small>
                    <small class="ml-2">{{ item.price_for_one_day }}</small>
                  </div>
                </div>
              </div>
              <div class="text-right">
                <mdb-btn v-show="item.need_payed_summ == 0 && item.finish_payment == false"  color="primary" @click="finish_patient_payment(item)" class="my-0"  style="font-size: 9px;" p="r4 l4 t1 b1">{{$t('finish')}}</mdb-btn>  
                <mdb-btn v-show="item.payed_summ == 0"  color="danger" @click="del_patient_payment(item)" class="my-0"  style="font-size: 9px;" p="r4 l4 t1 b1">{{$t('Delete')}}</mdb-btn>  
                <mdb-btn v-show="item.finish_payment == false"  color="success" @click="edit_patient_payment(item)" class="my-0" style="font-size: 9px;" p="r4 l4 t1 b1">{{$t('Edit')}}</mdb-btn>  
              </div>
            </div>
        </div>
        <Toast ref="message"></Toast>
        <AlertError ref="msg"></AlertError>

        <massage_box :hide="modal_status" :detail_info="modal_info"
            :m_text="$t('Failed_to_add')" @to_hide_modal = "modal_status= false"/>
   </div>
  </template>
  
  <script>
  import DatePicker from 'vue2-datepicker';
  import lineSelect from "../../components/lineSelectAuth";
  import lineSelect_room_price from "../../components/lineSelectPrice.vue";
  import hpSelect from "../../components/lineSelect.vue";
  import 'vue2-datepicker/index.css';
  import {mdbBtn, mdbInput} from 'mdbvue'
  import {mapActions, mapGetters} from 'vuex'
  import { required } from 'vuelidate/lib/validators'
  
  export default {
    components: { 
       mdbBtn, DatePicker,mdbInput, hpSelect,lineSelect, lineSelect_room_price
    },
    data(){
      return{
        modal_info : '',
        modal_status: false,
        loading: false,
        id: 0,
        patient_name: '',
        patient_id: null,
        price: 0,
        price_name: '',
        price_id: 0,
        price_activ: -1,
        price_list: [],

        Start_time: new Date(),
        End_time: new Date(),
        days: 0,
        note: '',
        price_type_name: '',
        price_type_id: null,
        price_room_name: '',
        price_room_id: null,
        ovqat_list: [],
        ovqat_price: 0,
        contragent_name: '',
        contragent_id: null,
  
        operatsion_show: false,
        payed_summ: 0,
        cash_summ: 0,
        card_summ: 0,
        free_day: 0,

        bron_room_name: '',
        bron_room_id: null,
        bron_bed_name: '',
        bron_bed_id: null,
        bron_room_beds_list: [],
        package_service_id: 0,
        package_payment_list: [],
      }
    },
    validations: {
      price_name: {required},
      contragent_name: { required },
    },
    props: {
        patient:
        {
          type: Object,
          default(){
            return {}
          }
        },
        
      },
    computed: mapGetters(['get_patient_list_last', 'get_bron_room_list_pagination', 'price_type_room', 'auth_user_list', 'get_bron_list_patient_id']),
    methods: {
      ...mapActions(['fetch_patient_list_last','fetch_patient_bron_payment', 'fetch_price_type_room','fetch_auth_list', 'fetch_bron_room_pagination']),
      select_bron_room(option){
        this.bron_room_name = option.data.name;
        this.bron_room_id = option.data.id;
        this.bron_room_beds_list = option.data.bedsList;
      },
      select_bron_bed(option){
        this.bron_bed_name = option.data.name;
        this.bron_bed_id = option.data.id;
      },
      selectPatient(option){
        this.patient_name = option.data.fio;
        this.patient_id = option.data.id;
      },
      select_room_price(option){
        console.log('option service type')
        console.log(option)
        this.price_room_name = option.data.name;
        this.price_room_id = option.data.id;
        this.price = 0;
        this.price_activ = -1;
        this.price_list = [];
        this.ovqat_list = [];
        this.price_list.push({name:'patient', id:1, price: option.data.room_bed_price, ovqat_price: option.data.ovqat_puli_patient})
        this.price_list.push({name:'for_room', id:2, price: option.data.room_price, ovqat_price: option.data.ovqat_puli_room})
        this.price_list.push({name:'care_patient', id:3, price: option.data.room_bed_price_not_patient, ovqat_price: option.data.ovqat_puli_qarovchi})
        this.price = option.data.room_bed_price;
        this.price_name = 'patient';
        this.price_id = 1;
        this.price_activ = 0;
        this.ovqat_price = option.data.ovqat_puli_patient;
        this.free_day = option.data.reserved_number_2;
        this.package_service_id = option.data.reserved_number_3;
        if(option.data.reserved_number_3 != 0 && option.data.reserved_number_3 != null){
          console.log('men kirmadim')
          this.fetchPaymentList(option.data.reserved_number_3);
        }
      },
      async fetchPaymentList(id){
        try{
          const response = await fetch(this.$store.state.hostname + "/HospitalServiceTypePacks/" + id);
          const data = await response.json();
          if(response.status == 200 || response.status == 201){
            console.log('package items');
            console.log(data);
            this.package_payment_list = [];
            for(let i=0; i<data.items.length; i++){
              var finish_payment_finish = false;
              if(data.items[i].serviceType.price == 0){
                finish_payment_finish = true;
              }
              let temp = {
                serviceTypeId: data.items[i].serviceTypeId,
                contragentId: 1,
                serviceName: data.items[i].serviceType.name,
                patientsId: this.patient.patient_id,
                summ: data.items[i].serviceType.price,
                name: '',
                patientName: localStorage.docName,
                authorizationId: data.items[i].authorizationId,
                creatorAuthId: localStorage.AuthId,
                discount_qty: 0,
                finishPayment: finish_payment_finish,
                discount_persantage_qty: 0,
                discount_card_qty: data.items[i].authorization.users.fio,
                discount_real_qty: 0,
              }
              this.package_payment_list.push(temp)
            }
            console.log('this.package_payment_list')
            console.log(this.package_payment_list)
          }
        }
        catch(error){
          console.log(error);
        }
      },
      async fetch_send_payment(){
        try{
          const requestOptions = {
            method: "POST",
            headers: { "Content-Type" : "application/json" },
            body: JSON.stringify(this.package_payment_list)
          };
          console.log(requestOptions)
          this.loading = true;
          const response = await fetch(this.$store.state.hostname + "/Payments/addPayments", requestOptions);
          const data = await response.json();
          if(response.status == 200 || response.status == 201){
            console.log('data payments sending')
            console.log(data)
          }
        }
        catch(error){
          console.log(error)
        }
      },
      refresh(roomType){
        console.log('refresh ishladi')
        console.log(roomType)
      },
      async edit_patient_payment(data){
        console.log(data)
        this.contragent_id = data.UsersId;
        this.contragent_name = data.reserved_name_3;
        this.id = data.id;
        this.price = data.price_for_one_day;
        this.Start_time = new Date(data.begin_date_bron);
        this.End_time = new Date(data.end_date_bron);
        this.price_room_id = data.reserved_number_3;
        this.price_room_name = data.reserved_name_2;
        this.price_id = data.reserved_number_4;
        this.price = data.price_for_one_day;
        this.price_activ = data.reserved_number_4 - 1;
        this.price_name = 'patient';
        this.ovqat_price = data.reserved_number_2;
        this.days = data.reserved_number_1;
        this.cash_summ = data.cash_summ;
        this.card_summ = data.card_summ;
        this.payed_summ = data.payed_summ;
        this.bron_room_name = data.reserved_name_5;
        this.bron_room_id = data.HospitalBronRoomNid;
        this.bron_bed_id = data.HospitalBronRoomItemNid;
        this.operatsion_show = data.reserved_status_1;
        if(data.reserved_status_1 == true){
          this.free_day = data.reserved_number_db_3;
        }
        else{
          this.free_day = 0;
        }
        for(let i=0; i<this.price_type_room.rows.length; i++){
          if(this.price_type_room.rows[i].id == this.price_room_id){
            this.price_list = [];
            this.price_list.push({name:'patient', id:1, price: this.price_type_room.rows[i].room_bed_price, ovqat_price: this.price_type_room.rows[i].ovqat_puli_patient})
            this.price_list.push({name:'for_room', id:2, price: this.price_type_room.rows[i].room_price, ovqat_price: this.price_type_room.rows[i].ovqat_puli_room})
            this.price_list.push({name:'care_patient', id:3, price: this.price_type_room.rows[i].room_bed_price_not_patient, ovqat_price: this.price_type_room.rows[i].ovqat_puli_qarovchi})
          }
        }
      },
      async del_patient_payment(item){
        const requestOptions = {
              method : "delete",
            };
            const response = await fetch(this.$store.state.hostname + "/HospitalBronRoomPayments/" + item.id, requestOptions);
            const data = await response.json();
            // console.log(data)
            if(data.id)
            {
              this.$refs.message.success('Successfully_removed')
              await this.fetch_patient_bron_payment(this.patient.patient_id);
            }
      },
      async finish_patient_payment(item){
        try{
          const res = await fetch(this.$store.state.hostname + '/HospitalBronRoomPayments/finishPayment?id=' + item.id);
          const res_data = await res.json();
          console.log('res_data')
          console.log(res_data)
          if(res_data.id){
              this.$refs.message.success('Successfully_added')
              await this.fetch_patient_bron_payment(this.patient.patient_id);
          }
        }
        catch(error){
          console.log(error)
        }
        
      },
      func_activ_price(i, data){
        console.log(data)
        this.price_activ = i;
        this.price = data.price;
        this.price_name = data.name;
        this.price_id = data.id;
        this.ovqat_price = data.ovqat_price;

      },
      selectPriceType(option){
        console.log('option service type')
        console.log(option)
        this.price_type_name = option.data.name;
        this.price_type_id = option.data.id;
        this.free_day = option.data.reserved_number_2;
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

        this.free_day = 0;
        this.bron_room_name = '';
        this.bron_room_id = null;
        this.bron_bed_name = '';
        this.bron_bed_id = null;
        this.bron_room_beds_list = [];
      },
      async submit(){
        if(this.bron_room_id == null || this.bron_room_id == ''){
          await this.submit_without_room()
        }
        else{
          if(this.bron_bed_id == null || this.bron_bed_id == ''){
            console.log(this.bron_room_beds_list)
            this.bron_bed_id = this.bron_room_beds_list[0].id;
            this.bron_bed_name = this.bron_room_beds_list[0].name;
          }
          await this.submit_with_room()
        }
      },
      async submit_without_room(){
        if(this.$v.$invalid || this.days == 0)
          {
            this.$v.$touch();
            this.$refs.message.warning('please_fill')
            return false;
          }
        if(this.free_day == '' || this.free_day == null){
          this.free_day = 0;
        }
        var need_summ = 0;
        var discount_summ = 0;
        var mount_day = 0;
        
        if(this.operatsion_show){
          mount_day = this.days - parseInt(this.free_day);
          discount_summ = this.price * parseInt(this.free_day);
          need_summ = mount_day * this.price;
        }
        else{
          need_summ = this.days*this.price;
        }
          need_summ = need_summ - this.payed_summ;
          console.log(this.Start_time)
          const requestOptions = {
            method : "POST",
            headers: { "Content-Type" : "application/json" },
            body: JSON.stringify({
            "patientsId" : this.patient.patient_id,
            "usersId" : this.contragent_id,
            "begin_date_bron" : this.Start_time.toISOString().slice(0,11) + this.Start_time.toLocaleTimeString('it-IT') + '.000Z',
            "end_date_bron" : this.End_time.toISOString().slice(0,11) + this.End_time.toLocaleTimeString('it-IT') + '.000Z',
            "price_for_one_day": this.price,
            "payed_summ": this.payed_summ,
            "card_summ": this.card_summ,
            "cash_summ": this.cash_summ,
            "need_payed_summ": need_summ,
            "discount_payed_summ": discount_summ,
            "dolg_payed_summ": 0,
            "discount_pesantage": 0,
            "reserved_status_1": this.operatsion_show,
            "reserved_number_1": this.days, // yotish kuni
            "reserved_number_2": this.ovqat_price,  // ovqatga ajratiladigan pul summasi
            "reserved_number_3": this.price_room_id, // startsionar narxi hospitalType id
            "reserved_number_4": this.price_id, // Narxlarni uzim idlagan buyicha
            "reserved_name_1": this.patient.patient_name, // bemor ismi
            "reserved_name_2": this.price_room_name, // startsionar narxi hospitalType name
            "reserved_name_3": this.contragent_name, // doctor ismi
            "reserved_number_db_1": localStorage.AuthId, // registratsiya qilgan id
            "reserved_number_db_3": this.free_day, // operatsiya free days
            "reserved_name_4": localStorage.docName, // registratsiya qilgan name
            "reserved_name_5": this.bron_room_name, 
            // "hospitalBronRoomNid":this.bron_room_id,
            // "hospitalBronRoomItemNid":this.bron_bed_id,
            "finish_payment": false,
                // reserved_number_db_5  reserved_number_db_2  -> ishlatib bulmaydi Begali aka ishlatayabdi
            // "authorizationId": localStorage.AuthId,
            // "note": this.note,
            "id" : this.id,
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
              this.$refs.message.success('Added_successfully');
              console.log(data)
              // await this.fetchAddPriceType(data.id);
              this.$emit('close')
              await this.fetch_patient_bron_payment(this.patient.patient_id);
              if(this.package_service_id != 0 && this.package_service_id != null){
                await this.fetch_send_payment();
              }
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
      async submit_with_room(){
        if(this.$v.$invalid || this.days == 0)
          {
            this.$v.$touch();
            this.$refs.message.warning('please_fill')
            return false;
          }
        if(this.free_day == '' || this.free_day == null){
          this.free_day = 0;
        }
          console.log('this.Start_time.toISOString()')
          console.log(this.Start_time.toISOString())
          console.log(this.End_time.toISOString())
        var need_summ = 0;
        var discount_summ = 0;
        var mount_day = 0;
        
        if(this.operatsion_show){
          mount_day = this.days - parseInt(this.free_day);
          discount_summ = this.price * parseInt(this.free_day);
          need_summ = mount_day * this.price;
        }
        else{
          need_summ = this.days*this.price;
        }
          need_summ = need_summ - this.payed_summ;
          console.log(this.Start_time)
          const requestOptions = {
            method : "POST",
            headers: { "Content-Type" : "application/json" },
            body: JSON.stringify({
            "patientsId" : this.patient.patient_id,
            "usersId" : this.contragent_id,
            "begin_date_bron" : this.Start_time.toISOString().slice(0,11) + this.Start_time.toLocaleTimeString('it-IT') + '.000Z',
            "end_date_bron" : this.End_time.toISOString().slice(0,11) + this.End_time.toLocaleTimeString('it-IT') + '.000Z',
            "price_for_one_day": this.price,
            "payed_summ": this.payed_summ,
            "card_summ": this.card_summ,
            "cash_summ": this.cash_summ,
            "need_payed_summ": need_summ,
            "discount_payed_summ": discount_summ,
            "dolg_payed_summ": 0,
            "discount_pesantage": 0,
            "reserved_status_1": this.operatsion_show,
            "reserved_number_1": this.days, // yotish kuni
            "reserved_number_2": this.ovqat_price,  // ovqatga ajratiladigan pul summasi
            "reserved_number_3": this.price_room_id, // startsionar narxi hospitalType id
            "reserved_number_4": this.price_id, // Narxlarni uzim idlagan buyicha
            "reserved_name_1": this.patient.patient_name, // bemor ismi
            "reserved_name_2": this.price_room_name, // startsionar narxi hospitalType name
            "reserved_name_3": this.contragent_name, // doctor ismi
            "reserved_number_db_1": localStorage.AuthId, // registratsiya qilgan id
            "reserved_number_db_3": this.free_day, // operatsiya free days
            "reserved_name_4": localStorage.docName, // registratsiya qilgan name
            "reserved_name_5": this.bron_room_name, 
            "hospitalBronRoomNid":this.bron_room_id,
            "hospitalBronRoomItemNid":this.bron_bed_id,
            "finish_payment": false,
                // reserved_number_db_5  reserved_number_db_2  -> ishlatib bulmaydi Begali aka ishlatayabdi
            // "authorizationId": localStorage.AuthId,
            // "note": this.note,
            "id" : this.id,
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
              this.$refs.message.success('Added_successfully');
              console.log(data)
              // await this.fetchAddPriceType(data.id);
              this.$emit('close')
              await this.fetch_patient_bron_payment(this.patient.patient_id);
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
        await this.fetch_bron_room_pagination();
        console.log('this.get_patient_list_last');
        console.log(this.get_bron_room_list_pagination);
  
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
  .item_list_patient_bron_info{
    box-shadow: rgba(0, 0, 0, 0.35) 0px 5px 15px;
  }
  </style>