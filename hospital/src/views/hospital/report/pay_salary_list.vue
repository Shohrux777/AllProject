<template>
    <div class="bg-white">
      <div class="p-2">
        <div class="bg-white p-4 mb-5 pt-4 shadow" style="border-radius:5px; position:relative;">
          <form @submit.prevent="submit">
            <div style="height: 60px;" class="d-flex justify-content-between border-bottom align-items-center  ">
              
              <div class="plus">
                <mdb-btn @click="$router.back()" color="primary py-2 px-4" style="font-size:10px;">
                  <mdb-icon style="font-size:10.5px;" icon="arrow-circle-left" class="m-0 p-0 mr-1" />
                  Назад
                </mdb-btn>
              </div>
            </div>
          </form>
          <div class="TablePatientDocId p-3">
            <table class="myTable">
              <thead>
                <tr class="header alert-success">
                  <th  width="40" class="text-left">№</th>
                  <th width="200">{{$t('fio')}}</th>
                  <th>Начала</th>
                  <th>Конец</th>
                  <th>Услуга %</th>
                  <th>Услуга $</th>
                  <th >Операцион %</th>
                  <th >Операцион $</th>
                  <th >Стационар %</th>
                  <th >Стационар $</th>
                  <th >Общий</th>
                  <th>{{$t('note')}}</th>

                </tr>
              </thead>
              <tbody>
                <tr v-for="(row,rowIndex) in oylik_list" :key="rowIndex" v-show="row.usersId == user_id_oylik">
                  <td> <small style="font-size: 12.5px;">{{rowIndex+1}}</small> </td>
                  <td> <small style="font-size: 12.5px;">{{row.reserved_3}}</small> </td>
                  <td> <small style="font-size: 12.5px;">{{row.reserved_1}}</small> </td>
                  <td> <small style="font-size: 12.5px;">{{row.reserved_2}}</small> </td>
                  <td> <small style="font-size: 12.5px;">{{row.service_percentage}}</small> </td>
                  <td> <small style="font-size: 12.5px;">{{row.service_price.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</small> </td>
                  <td> <small style="font-size: 12.5px;">{{row.reserved_1_nuber}}</small> </td>
                  <td> <small style="font-size: 12.5px;">{{row.reserved_2_nuber.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</small> </td>
                  <td> <small style="font-size: 12.5px;">{{row.starsionar_percentage}}</small> </td>
                  <td> <small style="font-size: 12.5px;">{{row.starsionar_price.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</small> </td>
                  <td> <small style="font-size: 12.5px;">{{row.reserved_3_nuber.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</small> </td>
                  <td> <small style="font-size: 12.5px;">{{row.reserved_4}}</small> </td>
                </tr>
                
              </tbody>
            </table>
          </div>
        </div>
      </div>
  
       
     
        <mdb-modal :show="show" @close="show = false" size="sm" class="text-center" danger>
          <mdb-modal-header center :close="false">
            <p class="heading">{{$t('Are_you_sure')}}</p>
          </mdb-modal-header>
          <mdb-modal-body>
            <mdb-icon icon="times" size="4x" class="animated rotateIn"/>
          </mdb-modal-body>
          <mdb-modal-footer center>
            <mdb-btn outline="danger" >{{$t('Yes')}}</mdb-btn>
            <mdb-btn color="danger" @click="show = false">{{$t('No')}}</mdb-btn>
          </mdb-modal-footer>
      </mdb-modal>
      <Toast ref="message"></Toast>
       <massage_box :hide="modal_status" :detail_info="modal_info"
        :m_text="$t('Failed_to_delete')" @to_hide_modal = "modal_status= false"/>
    </div>
  </template>
  
  <script>
  
    import { mdbBtn, mdbIcon,  mdbModal, mdbModalHeader,   mdbModalBody,mdbModalFooter   } from 'mdbvue';
    import {mapActions, mapGetters, mapMutations} from 'vuex'
    // import 'vue2-datepicker/index.css';
    export default {
      components: {
        mdbBtn,
        mdbModal, mdbModalHeader, mdbIcon, mdbModalBody, mdbModalFooter,
        
        
      },
      data(){
        return{
          user_id: this.$route.params.id,
          show_otchert: false,

          show: false,
          admin:false,
          snipper: true,
          loading:false,
          editData: {},
          modal_info : '',
          modal_status: false,
          contragent_name: '',
          contragent_id: 0,
          Start_time: null,
          End_time: null,
          group_name: '',
          group_id: null,
          get_payment_list: [],
          index: null,
          service_id: null,

          user_id_oylik: localStorage.oylik_user_id,
          oylik_list: [],
         
        }
      },
      async mounted(){
        await this.fetch_all_oylik_list();
      },
      computed: mapGetters(['get_contragent_list', 'get_report_by_data_time', 'get_report_by_time_card_cash', 'get_service_group_list']),
      methods: {
        ...mapActions(['fetch_contragent', 'fetch_report_by_data_time', 'fetch_service_group']),
        ...mapMutations(['district_row_delete', 'ochred_add_check']),

        print(){
          this.$refs.listlar.generatePdf()
        },
        async fetch_all_oylik_list(){
            try{
                const response = await fetch(this.$store.state.hostname + "/HospitalDoktorOyliks");
                const data = await response.json();
                if(response.status == 200 || response.status == 201)
                {
                  console.log(data)
                  this.oylik_list = data;
                }
                else{
                    this.$refs.message.warning('server_not_working')
                }
                }
                catch{
                    this.$refs.message.warning('server_not_working')
                }
        }
        
        
        
      },
    };
  </script>
  
  <style lang="scss">
  
  
  .add{
    position: fixed;
    background: rgba(0, 0, 0, 0.4);
    height: 100vh;
    top:0;
    width:85%;
  }
  
  .addxizmat{
    width: 470px;
    // height: 120px;
    background: #fff;
    position: relative;
    z-index: 5000;
  }
  .showing{
    display: none;
  }
  .timePicer{
    position: relative;
    margin-top: -10px;
    .timeLabel{
      position: absolute;
      font-size: 12px;
      background-color: #fff;
      padding: 1px 3px;
      z-index: 1;
      left: 6px;
      top: -1px;
    }
    .dayLabel{
      position: absolute;
      font-size: 12px;
      background-color: #fff;
      padding: 0px 3px;
      z-index: 1;
      left: 6px;
      top: -8px;
    }
  }
  .TablePatientDocId{
      // height: 400px;
      // overflow: hidden;
      // overflow-y: auto;
      // border: 1px solid #ddd;
    }
    .myTable {
    /* border-collapse: collapse; */
    table-layout:fixed;
    width: 100%;
    overflow: hidden;
    // border: 1px solid #ddd;
    font-size: 18px;
    max-height:80px; overflow-x:auto
  }
  .myTable th{
    font-weight: 600;
    font-size:12px;
  }
  .myTable td{
    font-size:13px;
  }
  .myTable th, .myTable td {
    text-align: left;
    padding: 10px;
  }
  
  .myTable tr {
    border-bottom: 1px solid rgb(240, 240, 240);
  }
  
  .myTable tr.header, .myTable tr:hover {
    // background-color: #f1f1f1;
  }
  .delIcon{
    color: rgb(251, 70, 70);
    font-size: 13px;
  }
  </style>