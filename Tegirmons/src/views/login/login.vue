<template>
  <div class="login">
    <loader v-if="loading"/>
    <!-- <div class="logoTegirmon">
      <img width="150" src="../../assets/logo1.png" alt="">
    </div> -->
    <div class="backLogin">
      <div  style="position:fixed; top:0; right:0">
        <mdb-btn color="elegant" style="font-size: 9px;" class="py-2 px-3" @click="rus_btn">{{$t('ru')}}</mdb-btn>
        <mdb-btn color="elegant" style="font-size: 9px;" class="py-2 px-3" @click="en_btn">{{$t('en')}}</mdb-btn>
      </div>

      <div style="height:100vh; width:100%;" class="d-flex justify-content-center align-items-center ">
        <div >
          <div class="text-center">
            <h2 class="">{{$t('authorization')}}</h2>
          </div>
          <div class="mt-4 d-flex justify-content-center"> 
              <span class="m-0 p-0 text-danger">{{this.error}}</span>
          </div>
          <form v-on:submit.prevent="submit">
            <div class="form  mt-1 mb-5">
              <div class="username border-bottom d-flex justify-content-center align-items-center px-2">
                <div class="d-flex ">
                  <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-user" width="27" height="27" viewBox="0 0 24 24" stroke-width="1.0" stroke="#9e9e9e" fill="none" stroke-linecap="round" stroke-linejoin="round">
                    <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                    <circle cx="12" cy="7" r="4" />
                    <path d="M6 21v-2a4 4 0 0 1 4 -4h4a4 4 0 0 1 4 4v2" />
                  </svg>
                </div>
                <input type="text" v-model="login" :placeholder="$t('username')">
                <small class="invalid-text" style="margin-top: 115px; margin-left: -125px;"  v-if="$v.login.$dirty && !$v.login.required" >
                {{$t('name_invalid_text')}}
              </small>
              </div>
              <div class="password border-bottom d-flex justify-content-center align-items-center px-2">
                <div class="d-flex ">
                  <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-lock" width="27" height="27" viewBox="0 0 24 24" stroke-width="1.0" stroke="#9e9e9e" fill="none" stroke-linecap="round" stroke-linejoin="round">
                    <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                    <rect x="5" y="11" width="14" height="10" rx="2" />
                    <circle cx="12" cy="16" r="1" />
                    <path d="M8 11v-4a4 4 0 0 1 8 0v4" />
                  </svg>
                </div>
                <input type="password" v-model="pass"  @input="handleHashing($event.target.value)" :placeholder="$t('password')">
                <small class="invalid-text" style="margin-top: 115px; margin-left: -125px;"   v-if="$v.pass.$dirty && !$v.pass.required" >
                  {{$t('name_invalid_text')}}
                </small>
              </div>
              <div class="sub_login d-flex justify-content-center  align-items-center">
                <mdb-btn  color="primary" type="submit" p="r5 l5 t2 b2">{{$t('enter')}}</mdb-btn>  
              </div>
            </div>
          </form>
        </div>
      </div>
    </div>
  <Toast ref="message"></Toast>
  </div>  
</template>

<script>
import { required } from 'vuelidate/lib/validators'
import {mdbBtn,mdbInput } from "mdbvue";
import {mapActions, mapGetters} from "vuex"
 import md5 from 'js-md5'
export default {
  components: {
    mdbBtn,mdbInput, 
  },
  validations: {
    login: {required},
    pass: {required},
  },
  data (){
      return{ 
        loading: false,

        pass: '',
        login: '',
        md: '',
        error: '',

      }
    },
  async mounted(){  
    await this.fetchUser();
    localStorage.groupIdInvoice = 0;
    localStorage.AuthId = 1;
    localStorage.CompId = 1;
    localStorage.CompName = "Company"
    localStorage.sidebar = true;
    localStorage.Login = "";
    localStorage.AccessType = 0;
    localStorage.device = '';
    localStorage.size_value = 10
    localStorage.numPage = 1
    localStorage.pageNum = 1
    localStorage.Items_count = 10
    if(this.allUser.rows.length == 0){
      localStorage.Login = '4';
      localStorage.AuthId = 1;
      localStorage.CompId = 1;
      localStorage.AccessType = 0;
      localStorage.CompName = "Company"
      localStorage.sidebar = true;
      this.$router.push('/user')
    }

    let time1 = new Date();
    localStorage.start = time1.toISOString().slice(0,10); 
    localStorage.end = time1.toISOString().slice(0,10);
    localStorage.setItem('begin_date', time1.toISOString().slice(0,10))
    localStorage.setItem('end_date', time1.toISOString().slice(0,10))
    localStorage.setItem('menu_item', 0)

    await this.nbuKurs();
    // console.log(localStorage.AuthId)
  },
  computed: mapGetters(['allUser']),
  // watch: {
  //   search_data: function(){
  //     this.search_click();
  //   }
  // },
  methods: {
    ...mapActions(['fetchUser']),
    handleHashing (data) {
      this.pass = data
      this.md = md5(data)
      // console.log(this.md)
    },
    async submit(){
      if(this.$v.$invalid )
      {
        this.$v.$touch();
        this.$refs.message.warning('please_fill')
        return false;
      }
      try{
        this.loading = true;
        console.log(this.login)
        console.log(this.md)
          const response = await fetch(this.$store.state.hostname + '/TegirmonAuth/checkAuthByLoginAndPassword?login=' + this.login + '&password=' + this.md)
          const data = await response.json()  
          this.loading = false;
          console.log(data)
        if(response.status == '200' || response.status == '201'){
          localStorage.AuthId = data.id;
          localStorage.Login = data.login;
          localStorage.AccessType = data.auth_user_creator_id;
          localStorage.user_name = data.user.fio;
          localStorage.user_id = data.user.id;
          localStorage.CompId = 1;
          this.$router.push('/getProduct')
        }
        else{
            this.$refs.message.error("error_login")
            this.error = "error_login"
        }
      }
      catch{
          this.loading = false;
          this.$refs.message.error("network_ne_connect")
      }
    },
    rus_btn(){
      this.$i18n.locale = 'ru'
    },
    en_btn(){
      this.$i18n.locale = 'en'
    },
    async nbuKurs(){
      try{
        const response = await fetch("https://cbu.uz/uz/arkhiv-kursov-valyut/json/");
        const data = await response.json();
        console.log('json valyuta')
        console.log(data)
        if(data.length>0){
          localStorage.dollor_kurs = parseInt(data[0].Rate);
        }
      }
      catch(error){
        localStorage.dollor_kurs = 0;
        console.log(error);
      }
    },
  }
}
</script>

<style lang="scss">
.login {
  background-image: linear-gradient( 109.6deg,  rgba(48,207,208,1) 11.2%, rgba(51,8,103,1) 92.5% );
  background-repeat: no-repeat;
  background-size: cover;
  .backLogin{
    background-color: rgba(0, 0, 0,0.5);
    h2{
      color:#fff;
      font-family: 'Ubuntu', sans-serif;
      font-weight: 500;
      font-size: 28px;
    }
    .form {
      height: 300px;
      width:390px;
      background-color: #fff;
      border-radius: 4px;
      .username, .password{
        height: 100px;

        input{
          height: 35px;
          font-family: 'Roboto', sans-serif;
          margin-left:15px;
          width: 300px;
          font-size: 18px;
          color: rgb(0, 0, 0);
          outline:none;
          border:none;
        }
      }
      .sub_login{
        height: 100px;
       
      }
    }
  }
}
.itemPrison{
  &:hover{
    background-color: #eee;
  }
}
.logoTegirmon{
  width: 100%;
  position: absolute;
  top: 0px;
  display: flex;
  justify-content: center;
}
</style>