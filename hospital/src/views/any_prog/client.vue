<template>
  <div>
    <div  class="p-4">
      <div class="bg-white p-4 mb-5 pt-4 shadow" style="border-radius:5px; position:relative;">
        <div style="height: 60px;" class="d-flex justify-content-between border-bottom align-items-center  ">
          <div class="title d-flex align-items-center">
            <h5 class="m-0 ">{{$t('patient_list')}}</h5>
          </div>
          <div class="plus d-flex">
            <div style="width: 300px;">
              <mdb-input label="Поиск" v-model="search"  @input="search_func"/>
            </div>
            <div style="width:100px; cursor:pointer;" class="mr-1 ml-1">
              <download-excel
                class="bg-info rounded px-2"
                style="margin-top:35px; padding: 3.5px 5px;"
                :data   = "get_client_list.rows"
                :fields = "json_fields"
                name="Nomer.xls">
                <small class="text-white ml-1" style="font-size: 12px;">
                    <mdb-icon icon="file-excel" class="mr-1"></mdb-icon>
                    Excel
                </small>
              </download-excel>
            </div>
            <div>
              
              <mdb-btn tag="a" @click="add" color="info py-2 px-4 "   style="background-color: rgb(85, 172, 238); margin-top: 35px; font-size:10px;">
                <mdb-icon  icon="plus" class="mr-2"></mdb-icon>{{$t('add')}}
              </mdb-btn>
            </div>
          </div>
        </div>
        <loader v-if="loading" />
        <anyTable v-else
          :datasource="get_patient_list_last"
          @for_edit="for_edit"
          @for_delete="for_delete"
          />
      </div>
    </div>
    <!-- <div :class="{'showing':show}">
      <div class="add d-flex justify-content-center align-items-center" >  
        <clientAdd/>
      </div>
    </div> -->
      <mdb-modal  :show="show"  @close="show = false" size="lg" light>
        <mdb-modal-header>
          <mdb-modal-title style="font-weight:  500;">{{$t('add_patient')}} </mdb-modal-title>
        </mdb-modal-header>
        <mdb-modal-body>
          <clientAdd :options="editData"/>
        </mdb-modal-body>
      </mdb-modal>
    <Toast ref="message"></Toast>
     <massage_box :hide="modal_status" :detail_info="modal_info"
      :m_text="$t('Failed_to_delete')" @to_hide_modal = "modal_status= false"/>
  </div>

</template>

<script>
  import clientAdd from "../../components/new_prog_add/client_add"
  import anyTable from "../../components/anyTable"
  import { mdbBtn, mdbInput, mdbIcon, mdbModal, mdbModalHeader, mdbModalTitle, mdbModalBody} from 'mdbvue';
  import {mapActions, mapGetters, mapMutations} from 'vuex'
  export default {
    components: {
      mdbBtn,
      mdbModal, mdbModalHeader, mdbModalTitle, mdbModalBody,
      mdbIcon,
      anyTable,mdbInput,
      clientAdd
    },
    data(){
      return{
        show: false,
        snipper: true,
        editData: {},
        modal_info : '',
        modal_status: false,
        search: '',
        loading: false,
        json_fields: {
          'phone_number': 'phoneNumber',
        },
        filteredList: [],
      }
    },
    async mounted(){
      
      this.loading = true;
      await this.fetch_client()
      await this.fetch_patient_list_last()
      this.loading = false;
    
    },
    computed: mapGetters(['get_client_list', 'get_patient_list_last']),
    methods: {
      ...mapActions(['fetch_client', 'fetch_patient_list_last', 'fetch_search_patient_name']),
      ...mapMutations(['client_row_delete']),

      for_edit(data){
        this.show = true
        console.log('hse')
        console.log(data)
        this.editData = data
      },
      search_func(){
        if(this.search == ''){
          this.fetch_patient_list_last()
        }
        else{
          this.fetch_search_patient_name(this.search);
          // console.log(this.get_patient_client_list)
        }

      },
      async for_delete(del_data,index){
          const requestOptions = {
            method : "delete",
          };
          const response = await fetch(this.$store.state.hostname + "/Patients/" + del_data.id, requestOptions);
          const data = await response.json();

          if(data.id)
          {
            // this.alert_text = localizeFilter('Successfully_removed');
            this.$refs.message.error('Successfully_removed')
            this.client_row_delete(index);

          }
          else{
            this.modal_info = data.detail + "    (" + data.routine + ")";
            this.modal_status = true;
          }
      },
      add(){
        this.show =! this.show
        this.editData = {};
      }

    },
  };
</script>

<style lang="scss">
@import "../../scss/tableAll.scss";
</style>