<template>
  <div class="payments_groups_lab">
    <div class="container-fluid mb-0 bg-white shadow result_lab_header" style="position:sticky; top:0; z-index:1;">
      <div class="row px-1 py-0">
        <div class="col-3">
          <mdb-input :label="$t('patient')" size="sm" class="m-0 p-0 header_input" outline v-model="patient_name" />
        </div>
        <div class="col-2 ">
          <mdb-input :label="$t('analiz_id')" size="sm" class="m-0 p-0 header_input" outline v-model="analiz_id" />
        </div>
        <div class="col-2 ">
          <mdb-input :label="$t('patient_id')" size="sm" class="m-0 p-0 header_input" outline v-model="patient_id" @keydown.enter.native="choosePatientID(patient_id)"/>
        </div>
        <div class="col-5">
          <div class="d-flex justify-content-end align-items-center h-100">
            <router-link to="/doctor">
              <div class="lab_update_icon lab_update_color_home">
                <mdb-icon icon="home" color="white" />
              </div>
            </router-link>
            <router-link to="/servecResultDynamic">
              <div class="lab_update_icon lab_update_color_analiz">
                <mdb-icon fab color="white" icon="cloudsmith" />
              </div>
            </router-link>
            <!-- <div class="lab_update_icon lab_update_color" @click="click_lab_update">
              <mdb-icon icon="flask" color="white" />
            </div> -->
            <div class="lab_update_icon lab_update_color_user">
              <mdb-icon icon="user" color="white" />
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
  import { mdbIcon, mdbInput } from 'mdbvue';
  import {mapActions, mapGetters} from 'vuex'
export default {
  components: {
    mdbIcon,
    mdbInput
  },
  data() {
    return {
      hostname1: this.$store.state.hostname1,
      search: '',
      loading: false,
      patient_id:null,
      patient_name:'',
      patient_born:null,
      analiz_id: null,
    }
  },
  async mounted() {
    console.log('localStorage.client_id_Lab')
    console.log(localStorage.client_id_Lab)
    this.patient_id = localStorage.client_id_Lab;
    if(this.patient_id != null && this.patient_id != ''){
      await this.choosePatientID(localStorage.client_id_Lab)
    } 
  },
  computed: mapGetters([ 'get_client_info']),
  methods:{
    ...mapActions(['fetch_client_info']),
    async choosePatientID(id){
      if(this.patient_id == null || this.patient_id == ''){return;}
      localStorage.client_id_Lab = id;
    try{
      this.loading = true;
      await this.fetch_client_info(id);
      console.log('this.get_client_info')
      console.log(this.get_client_info);
      this.patient_name = this.get_client_info.fio;
      this.patient_born = this.get_client_info.bornDate.slice(0,10);
      this.loading = false;
    }
    catch{
      this.loading = false;
      this.$refs.message.success('not_found')
    }
  },
  }
}
</script>

<style lang="scss" scoped>
.result_lab_header{
    height: 45px !important;
}
.result_lab_header .header_input{
    margin-top:10px !important;
}
.lab_update_color{
  background: linear-gradient(to right, rgb(242, 112, 156), rgb(255, 148, 114));
}
.lab_update_color_user{
  background-image: linear-gradient( 171.8deg,  rgba(5,111,146,1) 13.5%, rgba(6,57,84,1) 78.6% );
}
.lab_update_color_home{
  background-image: radial-gradient( circle farthest-corner at 10% 20%,  rgba(0,152,155,1) 0.1%, rgba(0,94,120,1) 94.2% );
}
.lab_update_color_analiz{
  background-image: linear-gradient( 83.2deg,  rgba(150,93,233,1) 10.8%, rgba(99,88,238,1) 94.3% );
}
.lab_update_icon{
  width:30px;
  height: 30px;
  display: flex;
  align-items: center;
  justify-content: center;
  border-radius: 50%;
  margin-left:10px;
  cursor:pointer;
}
</style>