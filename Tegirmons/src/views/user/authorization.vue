<template>
  <div class="auth-page">
    <loader v-if="loading" />
    <div v-else>
      <!-- Header -->
      <div class="auth-header-card d-flex align-items-center">
        <router-link to="/user" class="d-flex align-items-center auth-back-link">
          <svg
            xmlns="http://www.w3.org/2000/svg"
            class="icon icon-tabler icon-tabler-chevron-left mr-2"
            width="22"
            height="22"
            viewBox="0 0 24 24"
            stroke-width="2.5"
            stroke="#ffffff"
            fill="none"
            stroke-linecap="round"
            stroke-linejoin="round"
          >
            <path stroke="none" d="M0 0h24v24H0z" fill="none" />
            <polyline points="15 6 9 12 15 18" />
          </svg>
          <h5 class="m-0 auth-header-title">
            {{$t('Add_auth')}}
          </h5>
        </router-link>
      </div>

      <!-- Form card -->
      <div class="auth-card-wrapper mt-3">
        <div class="card auth-card">
          <div class="card-body px-3 py-3">
            <form @submit.prevent="submit">
              <mdb-row class="align-items-center mb-3">
                <mdb-col col="3">
                  <p class="form-label mb-0">{{$t('login')}}</p>
                </mdb-col>
                <mdb-col col="6">
                  <mdb-input
                    class="m-0"
                    v-model="login"
                    size="md"
                    outline
                    group
                    type="text"
                    validate
                    error="wrong"
                    success="right"
                  />
                  <small
                    class="invalid-text mt-1 ml-1"
                    v-if="$v.login.$dirty && !$v.login.required"
                  >
                    {{$t('name_invalid_text')}}
                  </small>
                  <small
                    class="helper-text mt-1 ml-1"
                    v-else
                  >
                    {{$t('write_company_name_to_chek')}}
                  </small>
                </mdb-col>
              </mdb-row>

              <mdb-row class="align-items-center mb-3">
                <mdb-col col="3">
                  <p class="form-label mb-0">{{$t('password')}}</p>
                </mdb-col>
                <mdb-col col="6" class="position-relative">
                  <mdb-input
                    class="m-0"
                    v-model="password"
                    outline
                    size="md"
                    v-show="!showPass"
                    group
                    type="password"
                    validate
                    error="wrong"
                    success="right"
                  />
                  <mdb-input
                    class="m-0"
                    v-model="password"
                    outline
                    size="md"
                    v-show="showPass"
                    group
                    type="text"
                    validate
                    error="wrong"
                    success="right"
                  />
                  <small
                    class="invalid-text mt-1 ml-1"
                    v-if="alert_danger"
                  >
                    {{$t('invalid_password')}}
                  </small>
                  <span @click="showPass = !showPass">
                    <i
                      class="fas fa-eye auth-eye-icon"
                      v-show="!showPass"
                    ></i>
                    <i
                      class="fas fa-eye-slash auth-eye-icon"
                      v-show="showPass"
                    ></i>
                  </span>
                </mdb-col>
              </mdb-row>

              <mdb-row class="align-items-center mb-3">
                <mdb-col col="3">
                  <p class="form-label mb-0">{{$t('confirm_p')}}</p>
                </mdb-col>
                <mdb-col col="6" class="position-relative">
                  <mdb-input
                    class="m-0"
                    v-model="password_m"
                    outline
                    size="md"
                    v-show="!showPasss"
                    group
                    type="password"
                    validate
                    error="wrong"
                    success="right"
                  />
                  <mdb-input
                    class="m-0"
                    v-model="password_m"
                    outline
                    size="md"
                    v-show="showPasss"
                    group
                    type="text"
                    validate
                    error="wrong"
                    success="right"
                  />
                  <small
                    class="invalid-text mt-1 ml-1"
                    v-if="alert_danger"
                  >
                    {{$t('invalid_password')}}
                  </small>
                  <span @click="showPasss = !showPasss">
                    <i
                      class="fas fa-eye auth-eye-icon"
                      v-show="!showPasss"
                    ></i>
                    <i
                      class="fas fa-eye-slash auth-eye-icon"
                      v-show="showPasss"
                    ></i>
                  </span>
                </mdb-col>
              </mdb-row>

              <mdb-row class="align-items-center mb-2">
                <mdb-col col="3">
                  <p class="form-label mb-0">{{$t('access_name')}}</p>
                </mdb-col>
                <mdb-col col="6">
                  <erpSelect
                    :options="AcsessType"
                    @select="selectOptionAccess"
                    :selected="access_name"
                    :label="$t('access_name')"
                  />
                  <small
                    v-if="$v.access_name.$dirty && access_id == null"
                    class="invalid-text mt-1 ml-1"
                  >
                    {{$t('select_item')}}
                  </small>
                </mdb-col>
              </mdb-row>

              <div class="blue-gradient">
                <hr class="mt-3 mb-2" />
              </div>

              <div class="d-flex justify-content-end mt-2">
                <mdb-btn
                  class="auth-save-btn"
                  type="submit"
                  p="r4 l4 t2 b2"
                >
                  <mdb-icon />
                  {{$t('save')}}
                </mdb-btn>
              </div>
            </form>

            <massage_box
              :hide="modal_status"
              :detail_info="modal_info"
              :m_text="$t('Failed_to_add')"
              @to_hide_modal="modal_status= false"
            />

            <Toast ref="message"></Toast>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import md5 from 'js-md5'
import erpSelect from "../../components/erpSelect.vue";
import { mdbInput, mdbRow, mdbCol, mdbIcon, mdbBtn, } from "mdbvue"
import {mapActions,mapGetters} from 'vuex'
import { required } from 'vuelidate/lib/validators'
export default {
  naem: "companyAdd",
  
  data(){
    return{
      modal_info: '',
      modal_status: false,
      loading: false,
      id: 0,
      showPasss: false,
      showPass: false,
      alert_danger: false,
      user_id: this.$route.params.id,
      login: '',
      password: '',
      password_m: '',
      md: '',
      md_1: '',
      fio: '',
      dept_id: null,
      emp_number: '',
      phone_number: "",
      comp_name: '',
      comp_id: null,
      access_name: '',
      access_id: null,
      AcsessType: [
        {
          id: 0,
          name: 'Aдмин'
        },
        {
          id: 1,
          name: 'Кассир'
        },
        {
          id: 2,
          name: 'Тарозибон'
        }
      ],
      auth_list: [],
    }
  },
  components: {
    mdbInput, mdbRow, mdbCol, mdbIcon, mdbBtn, erpSelect
  },
  validations: {
      login: {
        required
      },
      access_name : {required}
    },
    // async created()
    // {
    //   if(this.user_id > 0)
    //   {
    //     const res = await fetch(this.$store.state.hostname + '/TegirmonUser/' + this.user_id);
    //     const data = await res.json();
    //     console.log('response data')
    //     console.log(data)
    //     this.login = data.login
    //     for(let i=0; i<this.AcsessType.length; i++){
    //       if(this.AcsessType[i].id == data.user_type){
    //         console.log('this.AcsessType[i]')
    //         console.log(this.AcsessType[i])
    //         this.access_name = this.AcsessType[i].name
    //         this.access_id = this.AcsessType[i].id
    //       }
    //     }
    //   }
    // },
    async mounted(){
      await this.fetch_auth();
      console.log(localStorage.UserIdForAuth)
      const res = await fetch(this.$store.state.hostname + '/TegirmonUser/' + this.user_id);
      const data = await res.json();
      if(res.status == 200 || res.status == 201){
        for(let i=0; i<this.auth_list.length; i++){
          if(data.id == this.auth_list[i].tegirmonUserid){
            this.login = this.auth_list[i].login;
            this.id = this.auth_list[i].id;
            for(let j=0; j<this.AcsessType.length; j++){
              if(this.AcsessType[j].id == this.auth_list[i].auth_user_creator_id){
                this.access_name = this.AcsessType[j].name
                this.access_id = this.AcsessType[j].id
              }
            }
          }
        }
      }
    },
    computed: mapGetters(['allCompany']),
  methods:{
    ...mapActions(['fetchCompany']),
    selectOptionAccess(option){
      this.access_name = option.name
      this.access_id = option.id
    },
    async fetch_auth(){
      const res = await fetch(this.$store.state.hostname + '/TegirmonAuth');
      const data = await res.json();
      if(res.status == 200 || res.status == 201){
        console.log('auth list');
        console.log(data);
        this.auth_list = data;
      }
    },
    cls_wnd()
      {
        this.login = ''
        this.password = ''
      },
    save_data :  async function(){
        if(this.$v.$invalid)
          {
            this.$v.$touch();
            this.$refs.message.warning('please_fill')
            return false;
          }
        if(this.password != ''){
          this.md = await md5(this.password)
          this.md_1 = await md5(this.password_m)
        }
        // console.log(this.md)
        if(this.md_1 !== this.md || this.md == ''){
          this.$refs.message.error('Error_password')
          this.alert_danger = true;
          return
        }else{
          this.alert_danger = false;
          const requestOptions = {
              method : "POST",
              headers: { "Content-Type" : "application/json" },
              body: JSON.stringify({
                "id": this.id,
                "tegirmonUserid": localStorage.UserIdForAuth,
                "login": this.login,
                "pasword": this.md,
                "auth_user_creator_id": this.access_id,
                // "fio": this.fio,
                // "emp_number": this.emp_number,
                // "archiveDepartmentid": this.dept_id,
                // "position": this.phone_number
              })
            };
            try{
              this.loading = true;
              const response = await fetch(this.$store.state.hostname + "/TegirmonAuth", requestOptions);
              const data = await response.text();
              console.log(data)
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
        }
    },
    async submit()
      {
        if(await this.save_data())
        {
          this.$store.state.alert = true;
          this.$router.push("/user");
        }

      },
    async submit_continue()
      {
        if(await this.save_data())
        {
          if(this.id != 0){
            this.$router.push('/user_add/0')
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

<style scoped>
.auth-page {
  padding: 8px 10px 14px 10px;
  background: #f5f7fb;
  min-height: 100%;
}

.auth-header-card {
  padding: 10px 14px;
  border-radius: 10px;
  background-image: linear-gradient(90deg, #64b0fb 0%, #7fd4ff 50%, #b5e3ff 100%);
  box-shadow: rgba(15, 35, 52, 0.25) 0px 8px 20px -12px;
}

.auth-back-link {
  color: #ffffff;
  text-decoration: none;
}

.auth-back-link:hover {
  text-decoration: none;
}

.auth-header-title {
  font-size: 18px;
  font-weight: 600;
  letter-spacing: 0.3px;
}

.auth-card-wrapper {
  padding: 0 2px;
}

.auth-card {
  border-radius: 10px;
  border: none;
  box-shadow: rgba(60, 64, 67, 0.3) 0px 1px 2px 0px,
              rgba(60, 64, 67, 0.15) 0px 1px 3px 1px;
  background: #ffffff;
}

.form-label {
  font-size: 13px;
  font-weight: 500;
  color: #455a64;
}

.helper-text {
  font-size: 11px;
  color: gray;
}

.invalid-text {
  font-size: 11px;
  color: #e53935;
}

.auth-eye-icon {
  position: absolute;
  top: 8px;
  right: 16px;
  cursor: pointer;
  font-size: 14px;
  color: #607d8b;
}

.auth-save-btn {
  font-size: 11px !important;
  padding: 4px 14px !important;
  border-radius: 16px;
  background-image: linear-gradient(90deg, #43a047 0%, #66bb6a 100%) !important;
  color: #ffffff !important;
  border: none;
  box-shadow: 0 2px 6px rgba(67, 160, 71, 0.4);
}

.auth-save-btn:hover {
  filter: brightness(1.05);
  box-shadow: 0 3px 8px rgba(67, 160, 71, 0.55);
}
</style>