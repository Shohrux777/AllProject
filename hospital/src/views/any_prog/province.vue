<template>
  <div>
    <div class="p-4">
      <div class="bg-white p-4 mb-5 pt-4 shadow" style="border-radius:5px; position:relative;">
        <div style="height: 60px;" class="d-flex justify-content-between border-bottom align-items-center  ">
          <div class="title d-flex align-items-center">
            <h5 class="m-0 ">Область таблица</h5>
          </div>
          <div class="plus">
            <mdb-btn tag="a" @click="add" color="info py-2 px-4"   style="background-color: rgb(85, 172, 238); font-size:10px">
              <mdb-icon  icon="plus" class="mr-2"></mdb-icon>{{$t('add')}}
            </mdb-btn>
          </div>
        </div>
        <loader v-if="loading" />
        <anyTable  v-else
          :datasource="get_province_list"
          @for_edit="for_edit"
          @for_delete="for_delete"
          />
      </div>
    </div>
    <!-- <div :class="{'showing':show}">
      <div class="add d-flex justify-content-center align-items-center" >  
        <provinceAdd/>
      </div>
    </div> -->
      <mdb-modal  :show="show"  @close="show = false"  light>
        <mdb-modal-header>
          <mdb-modal-title style="font-weight:  500;">{{$t('Добавить Область')}}</mdb-modal-title>
        </mdb-modal-header>
        <mdb-modal-body>
          <provinceAdd :options="editData"/>
        </mdb-modal-body>
      </mdb-modal>
    <Toast ref="message"></Toast>
     <massage_box :hide="modal_status" :detail_info="modal_info"
      :m_text="$t('Failed_to_delete')" @to_hide_modal = "modal_status= false"/>
  </div>

</template>

<script>
  import provinceAdd from "../../components/new_prog_add/province_add"
  import anyTable from "../../components/anyTable"
  import { mdbBtn,  mdbIcon, mdbModal, mdbModalHeader, mdbModalTitle, mdbModalBody,   } from 'mdbvue';
  import {mapActions, mapGetters, mapMutations} from 'vuex'
  export default {
    components: {
      mdbBtn,
      mdbModal, mdbModalHeader, mdbModalTitle, mdbModalBody,
      mdbIcon,
      anyTable,
      provinceAdd
    },
    data(){
      return{
        loading: false,
        show: false,
        snipper: true,
        editData: {},
        modal_info : '',
        modal_status: false,
      }
    },
    async mounted(){
      this.loading = true;
      await this.fetch_province()
      this.loading =false;
    },
    computed: mapGetters(['get_province_list']),
    methods: {
      ...mapActions(['fetch_province']),
      ...mapMutations(['province_row_delete']),

      for_edit(data){
        this.show = true
        console.log(data)
        this.editData = data
      },
      async for_delete(del_data,index){
          const requestOptions = {
            method : "delete",
          };
          const response = await fetch(this.$store.state.hostname + "/Provinces/" + del_data.id, requestOptions);
          const data = await response.json();

          if(data)
          {
            // this.alert_text = localizeFilter('Successfully_removed');
            this.$refs.message.error('Successfully_removed')
            this.province_row_delete(index);
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

<style scoped>
@import "../../scss/tableAll.scss";

</style>