<template>
  <div>
   <!--Navbar-->
    
    <!--/.Navbar-->
    <main class="" style="border-radius: 7px;">
      <form @submit.prevent="submit">
        <div class="container ">

          <mdb-row >
            <mdb-col class="col-12 col-sm-12 col-md-12 col-lg-12">
              <mdb-input v-model="x_name"  size="sm" :label="$t('InputName')" icon="file-signature" group type="text" validate error="wrong" success="right"/>
              <small  class="invalid-text pt-2"  v-if="$v.x_name.$dirty && !$v.x_name.required" >
                {{$t('name_invalid_text')}}
              </small>

            </mdb-col>
          </mdb-row>
           <mdb-row>
            <mdb-col col="2">
              <!-- Default checked -->
              <!-- <div class="custom-control custom-switch pt-4 pl-5">
                <input v-model="status" type="checkbox" class="custom-control-input" id="customSwitch1" >
                <label class="custom-control-label " for="customSwitch1">{{$t('status')}}</label>
              </div> -->
            </mdb-col>
          </mdb-row>



          <hr class="mt-5"/>
          <mdb-btn  @click="submit_continue" color="primary" m="r3" style="font-size:10px;"
            p="r4 l4 t2 b2">  {{$t('Add_and_continue')}}
          </mdb-btn>
          <mdb-btn type="submit" color="success" m="r3" style="font-size:10px;"
            p="r4 l4 t2 b2">
            <mdb-icon icon="check" class="mr-2"/>{{$t('save')}}</mdb-btn>
        </div>
      </form>
    </main>


    <!-- Central Info Modal -->

    <mdb-modal  :show="ed_izmAddBtn"  @close="ed_izmAddBtn = false" size="lg" info>
      <mdb-modal-header>
        <mdb-modal-title>{{$t('AddIzm')}}</mdb-modal-title>
      </mdb-modal-header>
      <mdb-modal-body>
           <m_izm_add/>
      </mdb-modal-body>
    </mdb-modal>
    

    <div>
      <massage_box :hide="modal_status" :detail_info="modal_info"
      :m_text="$t('Failed_to_add')" @to_hide_modal = "modal_status= false"/>
    </div>
    <Toast ref="message"></Toast>
    

  </div>
</template>



<script>
import m_izm_add from "../../components/market_add/m_izm_add"
import { required } from 'vuelidate/lib/validators'
import {mapActions, mapMutations} from 'vuex'


  import {
    mdbModal, mdbModalHeader, mdbModalTitle, mdbModalBody,
    mdbInput,
    mdbIcon,
    mdbRow,
    mdbBtn,
    mdbCol,

  } from "mdbvue";

  export default {
    components: {
      mdbModal, mdbModalHeader, mdbModalTitle, mdbModalBody,
      mdbInput,
      mdbIcon,
      mdbRow,
      mdbBtn,
      mdbCol, 
      m_izm_add
    },
    validations: {
      x_name: { required },

    },
    data(){
      return{
        modal_info : '',
        modal_status: false,
        x_name : '',
        id: 0,
        price:  null,
        image: '',
        mf_name: '',
        fp_name : '',
        code: '',
        specs:'',
        barecode : null,
        status : true,
        ed_izm_data_id : null,
        ed_izm_data_name : '',
        note : '',
        ed_izmAddBtn: false,
        showfor_print : false,
        PicShow: true,
        base64: '',
        qty: null,
        // kg : '',
        // brutto :'',
        // litr : '',
        // metr : '',
        // soni : '',
      }
    },
    // async created(){
    //   if(this.id > 0)
    //   {
    //       const res = await fetch(this.$store.state.hostname + '/MarketProducts/' + this.id);
    //       const data = await res.json();
    //       console.log(data);
    //       this.x_name = data.name;
    //   }
    // },
    methods : {
      ...mapActions(['fetch_m_Measure', 'fetch_lab_group']),
      ...mapMutations(['onSelect_pTList','onSelect_selected_pTList','Clear_selected_plan_list']),

      onS_edizm(data){
        this.ed_izm_data_name = data.name;
        this.ed_izm_data_id = data.id;
      },

      async submit_continue()
      
      {
        if(await this.save_data())
        {
          this.cls_wnd();
        }
      },
      async cls_wnd()
      {
        this.x_name = '';
        this.id = 0
      },
     
      
      save_data :  async function()
      {
        if(this.$v.$invalid)
        {
          this.$v.$touch();
          this.$refs.message.warning('please_fill')
          return false;
        }
        const requestOptions = {
          method: "POST",
          headers: { "Content-Type" : "application/json" },
          body: JSON.stringify({
            "id": this.id,
            "name_1" : this.x_name,
            // "note" : this.note,
            // "selected_xr_id" : this.datasource.map(({xr_tool_id}) => xr_tool_id),
            // "pl_type_id" : this.selectedPTList.map(({id}) => id),

          })
        };
        const response = await fetch(this.$store.state.hostname + "/AnalizGroupInfoes", requestOptions);
        const data = await response.json();
        console.log(data)
        if(data.id)
        {
          // this.$refs.message.success('Added_successfully')
          this.$emit('close');
          this.fetch_lab_group();
          return true;
        }
        else{
          this.modal_info = data.detail + "    (" + data.routine + ")";
          this.modal_status = true;
          return false;
        }
        

         
      },

       async submit(){

        if(await this.save_data())
        {
          // this.$refs.message.success('Added_successfully')
           
        }
      },

    },
    // computed:mapGetters(['m_measure_list']),
    props:{
     options:{
        type: Object,
        default(){
          return {}
        }
      }
    },
    async mounted(){
    await this.options;
    console.log(this.options)
    if(Object.keys(this.options).length != 0){
      this.id = this.options.id;
      this.x_name = this.options.name_1;
    }
    else{
      this.cls_wnd();
    }
  },
    // computed : mapGetters(['category_list','ed_izm_list','XrToolJson',
    //   'productionType_list','planning_type_list','selectedPTList']),

  }
</script>


<style scoped>
main {
  background-color: #fff;
}
.flexible-navbar {
  transition: padding-left 0.5s;
  padding-left: 270px;
}

/* for select */
input[type=text], select, textarea {
  width: 100%;
  padding: 12px;
  border: 1px solid #ccc;
  border-radius: 4px;
  resize: vertical;
}

@media (max-width: 1199.98px) {

  .flexible-navbar {
    padding-left: 10px;
  }
}

.myTable th, .myTable td {
  text-align: left;
  padding: 5px;

}
.user_img{
  /* position: absolute; */
  /* height: 500px; */
  border: none;
}
.user_img img{
  max-width: 100%;
  max-height: 350px;
}
.user_img_test{
  min-height: 220px;
  width: 100%;
  border: none;
}
</style>
