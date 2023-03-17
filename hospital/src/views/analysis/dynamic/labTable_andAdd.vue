<template>
    <div class="bg-white"  style="height: calc(100vh - 55px); border-radius:3px; overflow: scroll;">
        
        <div >
            <div class="border-bottom d-flex justify-content-between bg-white" >
                <div>
                <h5 class="m-0 ml-4 d-flex text-primary" style="padding: 16px 0px">
                    {{main_name_title.name_title}}</h5>
                </div>
                <div>
                <div>
                    <mdb-btn @click="open_added_lab" color="success mt-3" m="r3" size="sm" p="r4 l4 t2 b2"><mdb-icon icon="plus" class="mr-2"/>{{$t('add')}}</mdb-btn>
                    </div>
                </div>
            
            </div>
            <loaderTable v-if="loading"/>
            <labTable v-else
                :datasource="m_product"
                @for_delete="for_delete"
                @for_edit="for_edit"
                @update_column="checklist"
                @page="page"
                @size="size"
                :analizPrint='analizPrint'
            />
        </div>
      
      <massage_box :hide="modal_status" :detail_info="modal_info"
        :m_text="$t('Failed_to_add')" @to_hide_modal="modal_status= false"/>
        <Toast ref="message"></Toast>
    </div>
  </template>
  
  <script>
  import {
    mdbIcon,
    mdbBtn,
  } from "mdbvue";
  import {mapActions, mapGetters, mapMutations} from 'vuex'
  import labTable from "../../../components/labTable"
  // import labAddResult from './labResultDynamicAdd.vue'
  export default {
    data(){
      return {
        modal_info: '',
        modal_status: false,
        loading: false,
        auth_id: localStorage.AuthId,
        analizPrint: true,
        show_lab_add: false,
  
        m_product :{
          rows: [],
          columns: ['name', 'create_date','extra1'],
          col : []
        },
      }
    },
    components: {
      labTable, mdbIcon,
      mdbBtn
    },
    props: {
       main_name_title:{
        type: Object,
        default(){
          return {}
        }
      },
      main_id:{
        type : Number,
        default : null
      }
    },
    watch: {
        main_id: function () {
            this.show_lab_add = false;
            this.loading = true;
            if(this.main_id){
              this.refresh();
            }
            this.loading = false;
        }
    },
    computed: mapGetters(['allProduct', 'get_pagination']),
    methods: {
      ...mapActions(['fetchProduct', 'fetchProduct_column']),
      ...mapMutations(['product_delete_row', 'update_pagination_first']),
      async checklist (){
          this.loading = true;
             if(this.main_id){
              this.refresh();
            }
          this.loading = false;
        },
        for_edit(edit_data)
        {
          this.$emit('update', edit_data.Id)
          // this.$router.push("/labUpdate/" + edit_data.Id);
        },
        async for_delete(del_data)
        {
           const requestOptions = {
              method : "delete",
            };
            const response = await fetch(this.$store.state.hostname + "/HospitalAnalizDynamicResults/" + del_data.id, requestOptions);
            const data = await response.text();
  
            if(response.status == 201 || response.status == 200)
            {
              this.$refs.message.success('Successfully_removed')
              this.alert_success = true;
              if(this.main_id){
                this.refresh();
              }
            }
            else{
              this.modal_info = data;
              this.modal_status = true;
            }
        },
         page(){
          if(this.main_id){
            this.refresh();
          }
        },
        size(){
          if(this.main_id){
            this.refresh();
          }
        },
        async refresh(){
          const res = await fetch(this.$store.state.hostname + '/HospitalAnalizDynamicResults/getPaginationByMainTableId?page=' + this.get_pagination.page + '&size=' + this.get_pagination.size + '&main_table_id=' + this.main_id);
          const res_data = await res.json();
          this.update_pagination_first({current_item_count: res_data.current_item_count, current_page: res_data.current_page+1, items_count: res_data.items_count});
          this.m_product.rows = res_data.items_list;
          this.loading = false;
        },
        open_added_lab(){
          this.$emit('opened')
            this.show_lab_add = true;
        }
    },
    async mounted(){
      this.loading = true;
      if(this.main_id){
        this.refresh();
      }
      this.loading = false;
      if(this.$store.state.alert){
        this.$refs.message.success('Added_successfully')
        this.$store.state.alert = false
      }
    }
  }
  </script>
  
  <style>
  
  </style>