<template>
  <div class="bronHospital">
    <div class="leftmenu border-right shadow">
      <div class="w-100" v-for="(link,index) in get_bron_room_list_pagination" :key="index">
        <!-- :class="{'active_linked': link.view}" -->
        <div
        :class="{ active_linked : active_el == index }" 
        active-class="active_linked"
        @click ="update_down(index, link)"
        class=" d-flex pl-2 px-3 menuitem justify-content-between" style="font-size:12.5px; padding: 8px 0; "
        >
        <span>{{link.name}}</span>
        <div>
          <span><span>{{ link.bed_bron_qty }}</span> <span>/</span> {{link.beds_count}}</span>
          <img src="../../assets/bed.png" class="ml-2" style="margin-right: -10px;" alt="" width="18px">
        </div>
        </div>
      </div>
    </div>
    <loader v-if="loading"/>
    <div v-else class="mainmenu" >
      <div class="shadowbackground" style="padding: 15px 70px;">
        <mdb-row>
        <mdb-col v-for="(bed,i) in bed_list_finished" :key="i" md="3" sm="4" class="bedcard">
          <div class="card">
            <div class="card-header px-2 text-center" style="padding: 10px 10px;">
              <h6 class="m-0 pt-1">{{bed.bedsItem.name}}</h6>
            </div>
            <div class="bed_bron_status" v-if="bed.client">
              <div class="text-center header_patient_name">
                <h6 class="pt-2 pb-1">{{ bed.bedsItem.not_finished_payment_list[bed.index].reserved_name_1 }}</h6>
              </div>
              <div class="border_gradiunt"></div>
              <div class="d-flex justify-content-between px-1 pt-1">
                <small class="text-success">Начала:</small>
                <small >{{  new Date(bed.bedsItem.not_finished_payment_list[bed.index].begin_date_bron).toString().slice(4,21) }}</small>
              </div>
              <div class="d-flex justify-content-between px-1 pt-1">
                <small class="text-danger">Окончание:</small>
                <small >{{  new Date(bed.bedsItem.not_finished_payment_list[bed.index].end_date_bron).toString().slice(4,21) }}</small>
              </div>
              <div class="d-flex justify-content-between px-1 pt-1">
                <small class="text-primary">День:</small>
                <small >{{ bed.bedsItem.not_finished_payment_list[bed.index].reserved_number_1 }} дней</small>
              </div>

              <div class="d-flex justify-content-between px-1 pt-1">
                <small>Операционный пациент</small>
                <svg v-if="bed.bedsItem.not_finished_payment_list[bed.index].reserved_status_1" xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-checkbox" width="18" height="18" viewBox="0 0 24 24" stroke-width="1.5" stroke="#00b341" fill="none" stroke-linecap="round" stroke-linejoin="round">
                  <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                  <polyline points="9 11 12 14 20 6" />
                  <path d="M20 12v6a2 2 0 0 1 -2 2h-12a2 2 0 0 1 -2 -2v-12a2 2 0 0 1 2 -2h9" />
                </svg>
                <svg v-else xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-square-x" width="18" height="18" viewBox="0 0 24 24" stroke-width="1.5" stroke="#ff2825" fill="none" stroke-linecap="round" stroke-linejoin="round">
                  <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                  <rect x="4" y="4" width="16" height="16" rx="2" />
                  <path d="M10 10l4 4m0 -4l-4 4" />
                </svg>
              </div>
              <div class="d-flex justify-content-between px-1 pt-1">
                <small>Не оплатет сумма:</small>
                <small class="text-danger font-weight-bold">
                  {{ bed.bedsItem.not_finished_payment_list[bed.index].need_payed_summ.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} сум
                </small>
              </div>
              <div class="d-flex justify-content-between px-1 pt-1">
                <small>Цена за один день</small>
                <small class="font-weight-bold">
                  {{ bed.bedsItem.not_finished_payment_list[bed.index].price_for_one_day.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} сум
                </small>
              </div>
              <div class="text-end border-top mt-1">
                <mdb-btn v-if="false" color="info" style="font-size: 10px;" class="px-3 py-1">
                  Инфо</mdb-btn>
                  <mdb-btn @click="fetch_finish_payment(bed.bedsItem.not_finished_payment_list[bed.index].id)" color="success" :disabled="bed.bedsItem.not_finished_payment_list[bed.index].need_payed_summ != 0" style="font-size: 10px;" class="px-3 py-1 ml-0">
                    Закончить брон</mdb-btn>
              </div>
            </div>
            <div v-else class="card-body  d-flex justify-content-center align-items-center ">
              <div class="bg-white d-flex align-items-center rounded card-body border px-3 py-2 addPatient" @click="add_patient(bed.bedsItem)">
                <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-circle-plus m-0 p-0" width="25" height="25" viewBox="0 0 24 24" stroke-width="1.5" stroke="#1266F1" fill="none" stroke-linecap="round" stroke-linejoin="round">
                  <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                  <circle cx="12" cy="12" r="9" />
                  <line x1="9" y1="12" x2="15" y2="12" />
                  <line x1="12" y1="9" x2="12" y2="15" />
                </svg>
                <h6 class="m-0 p-0 ml-2">Admit Patient</h6>
              </div>
            </div>
          </div>
        </mdb-col>
      </mdb-row>
      </div>
    </div>
    <div class="bed_add">
      <div class="add_plus" @click="add_bed_show = true">
        <i class="fas fa-plus text-white"></i>
      </div>
    </div>
    <Modal :show="show" headerbackColor="warning" closeColor="white" titlecolor="white" 
    :title="$t('Inpatient Admission')" @close="show = false" width="600px">
        <template v-slot:body>
          <patientAdd ref="patientAdd"  @close="closePatient_add" :room_info="room_info" :bed_info="bed_info" />
        </template>
      </Modal>
      <mdb-modal  :show="add_bed_show"  @close="add_bed_show = false"  light>
        <mdb-modal-header>
          <mdb-modal-title style="font-weight:  500;">{{$t('bronBeds')}}</mdb-modal-title>
        </mdb-modal-header>
        <mdb-modal-body>
          <bronBeds_add :options="editData" :roomId="roomId" @updateRoom="updateRoom"/>
        </mdb-modal-body>
      </mdb-modal>
      <Toast ref="message"></Toast>
      <AlertError ref="msg"></AlertError>

      <massage_box :hide="modal_status" :detail_info="modal_info"
          :m_text="$t('Failed_to_add')" @to_hide_modal = "modal_status= false"/>
  </div>
</template>

<script>
  import { mdbRow, mdbBtn, mdbCol, mdbModal, mdbModalHeader, mdbModalTitle, mdbModalBody,} from 'mdbvue';
  import patientAdd from "./patient_bed_add.vue";
  import Modal from '../../components/modal.vue'
  import {mapGetters, mapActions} from 'vuex'
  import bronBeds_add from "../../views/bron/bronBeds_add"
  export default {
    components: {
      mdbRow, mdbBtn,
      mdbCol,Modal,patientAdd, bronBeds_add,
      mdbModal, mdbModalHeader, mdbModalTitle, mdbModalBody,
    },
    data() {
      return {
        modal_info : '',
        modal_status: false,
        loading: false,

        show: false,
        active_el: 0,
        name: '',

        add_bed_show: false,
        editData: {},
        roomId: null,

        room_info:{
          id: null,
          name: '', 
          type: '',
          price_type: {},
        },
        bed_list: [],
        bed_list_finished: [],
        bed_info: {
          name: '',
          id: null,
        },
        choosen_click : false,
      }
    },
    async mounted(){
      await this.fetch_bron_room_pagination()
      console.log(this.get_bron_room_list_pagination)
      console.log('this.get_bron_room_list_pagination')
        console.log(this.get_bron_room_list_pagination)
        await this.refresh_page()
    },
    computed: mapGetters(['bron_room_list', 'get_bron_room_list_pagination']),
    methods: {
      ...mapActions(['fetch_bron_room', 'fetch_bron_room_pagination']),
      async refresh_page(){
        this.loading = true;
        await this.fetch_bron_room_pagination()
        if(this.get_bron_room_list_pagination.length>0){
          this.roomId = this.get_bron_room_list_pagination[this.active_el].id
          this.room_info.id = this.get_bron_room_list_pagination[this.active_el].id;
          this.room_info.name = this.get_bron_room_list_pagination[this.active_el].name;
          this.room_info.type = this.get_bron_room_list_pagination[this.active_el].reserved_name_1;
          this.room_info.price_type = this.get_bron_room_list_pagination[this.active_el].hospitalRoomType;
          this.bed_list = this.get_bron_room_list_pagination[this.active_el].bedsList;
          this.bed_list_finished = [];
          for(let i=0; i<this.bed_list.length; i++){
            var bed_finished = {
              bedsItem: this.bed_list[i], 
              client: false,
              index: 0,
            }
            for(let j =0; j<this.bed_list[i].not_finished_payment_list.length; j++){
              if(this.bed_list[i].not_finished_payment_list[j].finish_payment == false){
                bed_finished.client = true;
                bed_finished.index = j;
                break;
              }
            }
            this.bed_list_finished.push(bed_finished)
            console.log('this.bed_list_finished')
            console.log(this.bed_list_finished)
          }
          console.log(this.roomId)
          this.loading = false;
        }
      },
      async update_down(i, item) {
        this.active_el = i;
        console.log('item');
        console.log(item);
         this.bed_list = [];
         this.roomId = item.id;
         this.room_info.id = item.id;
         this.room_info.name = item.name;
         this.room_info.type = item.reserved_name_1;
         this.room_info.price_type = item.hospitalRoomType;

         this.bed_list = item.bedsList;
         this.bed_list_finished = [];
          for(let i=0; i<this.bed_list.length; i++){
            var bed_finished = {
              bedsItem: this.bed_list[i],
              client: false,
              index: 0,
            }
            for(let j =0; j<this.bed_list[i].not_finished_payment_list.length; j++){
              if(this.bed_list[i].not_finished_payment_list[j].finish_payment == false){
                bed_finished.client = true;
                bed_finished.index = j;
                break;
              }
            }
            this.bed_list_finished.push(bed_finished)
            console.log('this.bed_list_finished')
            console.log(this.bed_list_finished)
          }
      },
      async closePatient_add(){
        this.show = false;
        await this.refresh_page()
        
      },
      add_patient(item){
        console.log(item)
        this.show = true;
        this.bed_info.id = item.id;
        this.bed_info.name = item.name;
        this.$refs.patientAdd.refresh(this.room_info.price_type)
        console.log(this.room_info)
        console.log(this.bed_info)
      },
      async updateRoom(){
        // await this.fetchRoomsBeds(roomId)
        console.log('update room beds')
        console.log(this.active_el)
        await this.refresh_page()

      },
      async fetch_finish_payment(id){
        const res = await fetch(this.$store.state.hostname + '/HospitalBronRoomPayments/finishPayment?id=' + id);
        const res_data = await res.json();
        console.log('res_data')
        console.log(res_data)
        await this.refresh_page()
      },
    },
  }
</script>

<style lang="scss">
.bedcard{
  padding: 15px 15px;
}
.bedcard .card{
  height: 250px;
  overflow: hidden;
  background: rgba(255, 255, 255, 0.8);
}
.bedcard .card .card-header{
  background-color: #FBAB7E;
  background-image: linear-gradient(62deg, #FBAB7E 0%, #F7CE68 100%);
  color: #00183f;
}
.bronHospital{
  display: flex;
}
.bronHospital .leftmenu{
  width: 20%;
  background: #f0f0f0;
  color: #00183f;
}
.bronHospital .mainmenu{
  width: 80%;
  min-height: 80vh;
  background-image: url(../../assets/hospital_beds.jpg);
  background-position: center;
  background-repeat: no-repeat;
  background-size: cover;
  background-attachment: scroll local;
  
}
.bronHospital .mainmenu .shadowbackground{
  min-height: 100vh;
  background: rgba(232, 232, 232, 0.75);
}
.leftmenu{
  width: 100px;
  padding: 2px 0px;
  background: #fff;
}
.allContent{
  min-height: 100vh;
}
.active_linked{
  border-left: 2px solid rgb(1, 117, 166);
  background-color: #FBAB7E;
  background-image: linear-gradient(62deg, #FBAB7E 0%, #F7CE68 100%);
  p{
    color: #1266F1;
  }
  .ico{
    color: #1266F1;
  }
}
.menuitem:hover{
  background-image: radial-gradient( circle 400px at 6.8% 8.3%,  rgba(255,244,169,1) 0%, rgba(255,244,234,1) 100.2% );background-image: radial-gradient( circle farthest-corner at 10% 20%,  rgba(255,229,168,1) 0%, rgba(251,174,222,1) 100.7% );
  p{
    color: #1266F1;
  }
  .ico{
    color: #1266F1;
  }
}
.addPatient{
  cursor: pointer;
  margin-top: -10px;
}
.bed_add{
  position: fixed;
  bottom: 25px;
  right: 40px;

  .add_plus{
    cursor:pointer;
    padding: 10px 15px;
    border-radius:50%;
    background-image: radial-gradient( circle farthest-corner at 12.3% 19.3%,  rgba(85,88,218,1) 0%, rgba(95,209,249,1) 100.2% );
  }
}
.bed_bron_status{
  .header_patient_name{
    h6{
      font-size: 13px;
      margin: 0;
    }
  }
  .border_gradiunt{
    height: 1px;
    background-image: linear-gradient( 102.4deg,  rgba(253,189,85,1) 7.8%, rgba(249,131,255,1) 100.3% );
  }
}
</style>