<template>
  <div>
    <loader v-if="loading"/>
    <div class="border-bottom ">
      <router-link to="/doors">
         <h5 class="m-0 ml-3 d-flex" style="padding: 16px 0px">
           <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-chevron-left mr-2" width="24" height="24" viewBox="0 0 24 24" stroke-width="2.5" stroke="#007BFF" fill="none" stroke-linecap="round" stroke-linejoin="round">
              <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
              <polyline points="15 6 9 12 15 18" />
            </svg>
            {{$t('Add_doors')}}</h5>
      </router-link>
     
    </div>
    <div class="company_add p-5">
      <form @submit.prevent="submit">
        <mdb-row>
          <mdb-col col="3">
            <p class="p-0 m-0 mt-2" style="font-size: 14px;">{{$t('name')}}</p>
          </mdb-col>
          <mdb-col col="5">
            <mdb-input class="m-0 p-0" v-model="acc_name" size="md"  outline  group type="text" validate error="wrong" success="right"/>
            <small class="invalid-text pt-4" style="margin-left:5px; "  v-if="$v.acc_name.$dirty && !$v.acc_name.required" >
              {{$t('name_invalid_text')}}
            </small>
          </mdb-col>
        </mdb-row> 
        <mdb-row class="mt-4">
          <mdb-col col="3">
            <p class="p-0 m-0 mt-2" style="font-size: 14px;">{{$t('inout')}}</p>
          </mdb-col>
          <mdb-col col="5">
            <mdb-input class="m-0 p-0" placeholder="I or O"
              v-model="inout" size="md" outline  group type="text" 
              validate error="wrong" success="right"/>
          </mdb-col>
        </mdb-row>
        <mdb-row class="mt-4">
          <mdb-col col="3">
            <p class="p-0 m-0 mt-2" style="font-size: 14px;">{{$t('dbname')}}</p>
          </mdb-col>
          <mdb-col col="5">
            <mdb-input class="m-0 p-0" placeholder="192.168.1.100"
              v-model="dbname" size="md" outline  group type="text" 
              validate error="wrong" success="right"/>
          </mdb-col>
        </mdb-row> 

        <mdb-row class="mt-4">
          <mdb-col col="3">
            <p class="p-0 m-0 mt-2" style="font-size: 14px;">{{$t('login')}}</p>
          </mdb-col>
          <mdb-col col="5">
            <mdb-input class="m-0 p-0" placeholder="Login"
              v-model="login" size="md" outline  group type="text" 
              validate error="wrong" success="right"/>
          </mdb-col>
        </mdb-row>
        <mdb-row class="mt-4">
          <mdb-col col="3">
            <p class="p-0 m-0 mt-2" style="font-size: 14px;">{{$t('password')}}</p>
          </mdb-col>
          <mdb-col col="5">
            <mdb-input class="m-0 p-0" placeholder="Password"
              v-model="password" size="md" outline  group type="text" 
              validate error="wrong" success="right"/>
          </mdb-col>
        </mdb-row>
         
        
        <div class="blue-gradient">
          <hr class="mt-5 "/>
        </div>
        <mdb-row class="mt-4">
          <mdb-col col="8">
            <div class="mt-2 text-right">
          <mdb-btn  @click="submit_continue" color="primary" m="r3" style="font-size: 10.5px"
            p="r4 l4 t2 b2">  {{$t('Add_and_continue')}}
          </mdb-btn>
          <mdb-btn color="success"  type="submit" style="font-size: 10.5px"
            p="r4 l4 t2 b2">
            <mdb-icon  />{{$t('add')}}</mdb-btn>
        </div>
          </mdb-col>
        </mdb-row>
        
      </form>
      <massage_box :hide="modal_status" :detail_info="modal_info"
      :m_text="$t('Failed_to_add')" @to_hide_modal="modal_status= false"/>

      <Toast ref="message"></Toast>
    </div>
  </div>
</template>

<script>
import { mdbInput, mdbRow, mdbCol, mdbIcon, mdbBtn, } from "mdbvue"
import { required } from 'vuelidate/lib/validators'
export default {
  naem: "companyAdd",
  
  data(){
    return{
      modal_info: '',
      modal_status: false,
      loading: false,
      id: this.$route.params.id,
      acc_name: '',
      inout: '',
      dbname: '',
      device: '',
      login: '',
      password:'',
    }
  },
  components: {
    mdbInput, mdbRow, mdbCol, mdbIcon, mdbBtn
  },
  validations: {
      acc_name: {
        required
      }
    },
    async created()
    {
      if(this.id > 0)
      {
        const res = await fetch(this.$store.state.hostname + '/SkudDevices/' + this.id);
        const data = await res.json();
        this.id = data.id;
        this.acc_name = data.acc_name;
        this.dbname = data.dbname;
        this.login = data.login;
        this.password = data.password;
        this.inout = data.inout;
      }
    },
  methods:{
    cls_wnd()
      {
        this.acc_name = '';
        this.dbname = '';
        this.login = '';
        this.password = '';
        this.inout = '';
        this.id = 0;
      },
    save_data :  async function(){
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
              "id": this.id,
              "acc_name" : this.acc_name,
              "inout" : this.inout,
              "dbname" : this.dbname,
              "device" : this.dbname,
              "login" : this.login,
              "password" : this.password,
            })
          };
          try{
            this.loading = true;
            const response = await fetch(this.$store.state.hostname + "/SkudDoors", requestOptions);
            const data = await response.text();
            this.loading = false;
            if(response.status == 201 || response.status == 200)
            {
              this.$refs.message.success('Added_successfully')
              return true;
            }
            else{
              this.modal_info = data;
              this.modal_status = true;
              return false;
            }
          }
          catch{
            this.loading = false;
            this.modal_info = this.$i18n.t('network_ne_connect'); 
            this.modal_status = true;
          }
    },
    async submit()
      {
        if(await this.save_data())
        {
          this.$store.state.alert = true;
          this.$router.push("/door");
        }

      },
    async submit_continue()
      {
        if(await this.save_data())
        {
          if(this.id != 0){
            this.$router.push('/doors_add/0')
            this.cls_wnd();
          }
          else{
            this.cls_wnd();
          }
        }
      },
  }
}
</script>

<style>

</style>