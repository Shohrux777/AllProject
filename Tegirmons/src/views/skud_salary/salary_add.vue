<template>
  <div>
    <loader v-if="loading"/>
    <div class="border-bottom ">
      <router-link to="/salary">
         <h5 class="m-0 ml-3 d-flex" style="padding: 16px 0px">
           <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-chevron-left mr-2" width="24" height="24" viewBox="0 0 24 24" stroke-width="2.5" stroke="#007BFF" fill="none" stroke-linecap="round" stroke-linejoin="round">
              <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
              <polyline points="15 6 9 12 15 18" />
            </svg>
            {{$t('Add_salary')}}</h5>
      </router-link>
     
    </div>
    <div class="company_add p-5">
      <form @submit.prevent="submit">
        <mdb-row>
          <mdb-col col="3">
            <p class="p-0 m-0 mt-2" style="font-size: 14px;">{{$t('name')}}</p>
          </mdb-col>
          <mdb-col col="5">
            <mdb-input class="m-0 p-0" v-model="salary_name" size="sm"  outline  group type="text" validate error="wrong" success="right"/>
            <small class="invalid-text pt-3" style="margin-left:5px; "  v-if="$v.salary_name.$dirty && !$v.salary_name.required" >
              {{$t('name_invalid_text')}}
            </small>
          </mdb-col>
        </mdb-row> 

        <mdb-row class="mt-2">
          <mdb-col col="3">
            <p class="p-0 m-0 mt-2" style="font-size: 14px;">{{$t('salary')}}</p>
          </mdb-col>
          <mdb-col col="5">
            <input type="text" v-model="money_str"  @keyup="funcCash($event.target.value)"   
            class="form-control  mt-2 pr-2" style="border:1px solid #BDBDBD; outline:none;font-size:13px; height:30px;" >
          </mdb-col>
        </mdb-row> 

        <mdb-row class="mt-3">
          <mdb-col col="3">
            <p class="p-0 m-0 mt-2" style="font-size: 14px;">{{$t('salary_type')}}</p>
          </mdb-col>
          <mdb-col col="5">
            <div class="form-check form-check-inline">
              <input class="form-check-input" type="radio" v-model="salary_type" name="inlineRadioOptions" id="inlineRadio1" :value="1" />
              <label class="form-check-label" style="font-size:15px;" for="inlineRadio1">Soatga tulov</label>
            </div>

            <div class="form-check form-check-inline">
              <input class="form-check-input" type="radio" v-model="salary_type" name="inlineRadioOptions" id="inlineRadio2" :value="2" />
              <label class="form-check-label" style="font-size:15px;" for="inlineRadio2">Oylik to'lov</label>
            </div>
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
      salary_name: '',
      money: null,
      money_str: '',
      salary_type: 2,
    }
  },
  components: {
    mdbInput, mdbRow, mdbCol, mdbIcon, mdbBtn
  },
  validations: {
      salary_name: {
        required
      }
    },
    async created()
    {
      if(this.id > 0)
      {
        const res = await fetch(this.$store.state.hostname + '/SkudOyliks/' + this.id);
        const data = await res.json();
        this.id = data.id;
        this.salary_name = data.name;
        this.money = data.value;
        this.salary_type = data.reserved_value;
        this.money_str = this.money.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      }
    },
  methods:{
    cls_wnd()
      {
        this.salary_name = '';
        this.money = null;
        this.money_str = '';
        this.salary_name = 1;
        this.id = 0;
      },
      funcCash(n){
        var tols = ''
        for(let i=0; i<n.length; i++){
          if(n[i] != ' '){
            tols += n[i];
          }
        }

        this.money_str = tols.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
        var temp = ''
        for(let i=0; i<this.money_str.length; i++){
          if(this.money_str[i] != ' '){
            temp += this.money_str[i];
          }
        }
        console.log(temp)
        if(temp != ''){
          this.money = parseFloat(temp);
        }
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
              "name" : this.salary_name,
              "value": this.money,
              "reserved_value": parseInt(this.salary_type),
            })
          };
          try{
            this.loading = true;
            const response = await fetch(this.$store.state.hostname + "/SkudOyliks", requestOptions);
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
          this.$router.push("/salary");
        }

      },
    async submit_continue()
      {
        if(await this.save_data())
        {
          if(this.id != 0){
            this.$router.push('/salary_add/0')
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