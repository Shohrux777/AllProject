<template>
  <div class="bg-white">
   <loader v-if="loading"/>
    <div class="p-4">
      <div class="bg-white p-4 mb-5 pt-4 shadow" style="border-radius:5px; position:relative;">
        <form @submit.prevent="submit">
          <div style="height: 60px;" class="d-flex justify-content-between border-bottom align-items-center  ">
            <div class="title w-75 row align-items-center">
              <div class="col-4">
                <lineSelect
                  :options="get_contragent_list.rows"
                  :searchshow="true"
                  @select="selectcontragent"
                  :selected="contragent_name"
                  :label="$t('contragent')"
                />
                <div class="col-12">
                  <small v-if="$v.contragent_name.$dirty && contragent_id == null" class="invalid-text pt-4" >
                      {{$t('select_item')}}
                    </small>
                </div>
              </div>
              <div class="col-4">
                <mdb-input type="text" v-model="reason" size="sm" :label="$t('reason')" outline></mdb-input>
              </div>
              <div class="col-4">
                <mdb-input type="number" v-model="price" size="sm" :label="$t('price')" outline></mdb-input>
                <small class="invalid-text"  v-if="$v.price.$dirty && !$v.price.required" >
                  {{$t('name_invalid_text')}}
                </small>
              </div>
            </div>
            <div class="plus">
              <mdb-btn type="submit" color="primary py-2 px-4"  style="font-size:10px;" >
                {{$t('add')}}
              </mdb-btn>
            </div>
          </div>
        </form>
        <div class="TablePatientDocId p-3">
          <table class="myTable">
            <thead>
              <tr class="header ">
                <th width="60">№</th>
                <th >{{$t('contragent_name')}}</th>
                <th >{{$t('reason')}}</th>
                <th>{{$t('date')}}</th>
                <th width="100">{{$t('Summa')}}</th>
                <th v-if="!showen_Auth" width="100">{{$t('Action')}}</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="(row,rowIndex) in bonus_contragent_list" :key="rowIndex">
                <td> <span >{{rowIndex+1}}</span> </td>
                <td> <span >{{row.contragent.name}}</span> </td>
                <td> <span >{{row.payedUserInfo}}</span> </td>
                <td> <span >{{row.createdDateTime}}</span> </td>
                <td> <span >{{row.summa}}</span> </td>
                <td class="text-center" v-if="!showen_Auth">
                    <i class="fas fa-trash delIcon mask waves-effect m-0 pl-2" v-on:click="deleteRow(rowIndex, row)" :data-row="rowIndex"></i>
                </td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>
    </div>

     <vue-html2pdf ref='listlar'
        :show-layout="false"
        :float-layout="true"
        :enable-download="false"
        :preview-modal="true"
        :paginate-elements-by-height="1600"
        filename="hee hee"
        :pdf-quality="2"
        :manual-pagination="false"
        pdf-format="a4"
        pdf-orientation="landscape"
        pdf-content-width="100%"
        @hasStartedGeneration="hasStartedGeneration()"
        @hasGenerated="hasGenerated($event)"
      >
      <div slot="pdf-content">
       <div class="TablePatientDocId p-3">
          <table class="myTable">
            <thead>
              <tr class="header ">
                <th width="60">№</th>
                <th >{{$t('contragent_name')}}</th>
                <th >{{$t('reason')}}</th>
                <th>{{$t('date')}}</th>
                <th width="100">{{$t('Summa')}}</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="(row,rowIndex) in bonus_contragent_list" :key="rowIndex">
                <td> <span >{{rowIndex+1}}</span> </td>
                <td> <span >{{row.contragent.name}}</span> </td>
                <td> <span >{{row.payedUserInfo}}</span> </td>
                <td> <span >{{row.createdDateTime}}</span> </td>
                <td> <span >{{row.summa}}</span> </td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>

    </vue-html2pdf>

    <!-- <div :class="{'showing':show}">
      <div class="add d-flex justify-content-center align-items-center" >  
        <districtAdd/>
      </div>
    </div> -->
      <mdb-modal  :show="show"  @close="show = false"  light>
        <mdb-modal-header>
          <mdb-modal-title style="font-weight:  500;">{{$t('Добавить Район')}}</mdb-modal-title>
        </mdb-modal-header>
        <mdb-modal-body>
          <districtAdd :options="editData"/>
        </mdb-modal-body>
      </mdb-modal>
    <Toast ref="message"></Toast>
     <massage_box :hide="modal_status" :detail_info="modal_info"
      :m_text="$t('Failed_to_delete')" @to_hide_modal = "modal_status= false"/>
  </div>

</template>

<script>
  import VueHtml2pdf from 'vue-html2pdf'
import { required } from 'vuelidate/lib/validators'
  import lineSelect from "../../../components/lineSelect.vue";
  import districtAdd from "../../../components/new_prog_add/district_add"
  import { mdbBtn, mdbInput,  mdbModal, mdbModalHeader,  mdbModalTitle, mdbModalBody,   } from 'mdbvue';
  import {mapActions, mapGetters, mapMutations} from 'vuex'
  // import 'vue2-datepicker/index.css';
  export default {
    components: {
      mdbBtn,
      mdbModal, mdbModalHeader, mdbModalTitle, mdbModalBody, 
      districtAdd,
      VueHtml2pdf, lineSelect, mdbInput
    },
    data(){
      return{
        show: false,
        snipper: true,
        editData: {},
        modal_info : '',
        modal_status: false,
        contragent_name: '',
        contragent_id: null,
        Start_time: new Date(),
        End_time: new Date(),
        price: null,
        reason: '',
        loading: false,
        bonus_contragent_list: [],
        showen_Auth: false,
      }
    },
    validations: {
      price: {required},
      contragent_name : {required}
    },
    async mounted(){
      this.get_get_()
      if(localStorage.AuthId === 0){
        this.showen_Auth = true
        console.log(localStorage.Auth)
      } 
      // console.log('date')
      // console.log(this.End_time.toISOString())
      // let time2 = this.End_time.toISOString()
      //   let a = {
      //     time1: "2021-09-01T09:15:28.886Z",
      //     time2: null,
      //     contId: 0
      //   }
      //   a.time2 = time2
        // this.fetch_report_by_data_time(a)
        this.fetch_contragent()
    },
    computed: mapGetters(['get_contragent_list', 'get_report_by_data_time', 'get_report_by_time_card_cash']),
    methods: {
      ...mapActions(['fetch_contragent', 'fetch_report_by_data_time']),
      ...mapMutations(['district_row_delete']),
      cls_wnd(){
        this.price =  null;
        this.reason =  '';
        this.contragent_name = '';
        this.contragent_id = null;
      },
      async get_get_(){ 
        const response = await fetch(this.$store.state.hostname + '/ContragentAdditionalPaymentBefohands')
        const data = await response.json()
        this.bonus_contragent_list = data
        console.log(this.bonus_contragent_list)
      },

      async selectcontragent(option){
        console.log(option)
        this.contragent_id = option.data.id
        this.contragent_name = option.data.name
      },

      add(){
        this.show =! this.show
        this.editData = {};
      },
      print(){
        this.$refs.listlar.generatePdf()
      },
      async deleteRow(index,del_data){
          const requestOptions = {
            method : "delete",
          };
          const response = await fetch(this.$store.state.hostname + "/ContragentAdditionalPaymentBefohands/" + del_data.id, requestOptions);
          const data = await response.json();
          this.bonus_contragent_list.splice(parseInt(index), 1);
          if(data)
          {
            // this.alert_text = localizeFilter('Successfully_removed');
            this.$refs.message.error('Successfully_removed')
            // this.m_product_row_delete(index);
          }
          else{
            this.modal_info = data.detail + "    (" + data.routine + ")";
            this.modal_status = true;
          }
      },
      async submit(){
        var a = new Date();
        const time1 = a.toISOString();
        var b = time1.slice(11,13)
        console.log(b);
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
            "contragentId" : this.contragent_id,
            "payedUserInfo" : this.reason,
            "authorizationId" : localStorage.AuthId,
            "createdDateTime": time1,
            "summa": this.price,
            "noteStr": "",
          // "code" : 0
          })
        };
        this.loading = true;
        const response = await fetch(this.$store.state.hostname + '/ContragentAdditionalPaymentBefohands', requestOptions)
        const data = await response.json()
        this.loading = false;
        console.log(data)
        if(data.id){
          this.cls_wnd()
          this.get_get_()
          this.$refs.message.success('Added_successfully')
        }
        else{
          this.$refs.msg.error('Error_successfully')
          // this.modal_info = data.detail + "    (" + data.routine + ")";
          // this.modal_status = true;
          return false;
        } 
        
        
      },

    },
  };
</script>

<style scoped>
@import "../../../scss/tableAll.scss";
</style>