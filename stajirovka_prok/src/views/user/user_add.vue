<template>
    <div class="container-fluid">
        <div class="row">
            <div class="col-9">
                <form @submit.prevent="submit_add" class="container">
                    <MDBInput
                        type="text"
                        id="form1"
                        size="sm"
                        class="form-icon-trailing"
                        label="Familiya"
                        v-model="user.full_name"
                    >
                    </MDBInput>

                    <MDBInput
                        type="text"
                        id="form1"
                        size="sm"
                        class="form-icon-trailing mt-3"
                        label="Ism"
                        v-model="user.name"
                    >
                    </MDBInput>

                    <MDBInput
                        type="text"
                        id="form1"
                        size="sm"
                        class="form-icon-trailing mt-3"
                        label="Telefon"
                        v-model="user.phone"
                    >
                    </MDBInput>

                    <MDBInput
                        type="text"
                        id="form1"
                        size="sm"
                        class="form-icon-trailing mt-3"
                        label="Mobil telefon"
                        v-model="user.mobile_phone"
                    >
                    </MDBInput>

                    <MDBTextarea label="Tavsif" 
                        class="mt-3"
                        size="sm"
                        rows="2" v-model="user.description" />

                        <hr class="mt-4">
                    <MDBInput
                        type="text"
                        id="form1"
                        size="sm"
                        class="form-icon-trailing mt-3"
                        label="Email"
                        v-model="user.email"
                    >
                    </MDBInput>
                    <MDBInput
                        type="password"
                        id="form1"
                        size="sm"
                        class="form-icon-trailing mt-3"
                        label="Password"
                        v-model="user.password"
                    >
                    </MDBInput>
                    <!-- :label="$t('Familiya')" -->
                </form>
            </div>
            <div class="col-3">
              <label for="inputGroupFile01" class="picure_down border">
              <img src="" id="prewImage" v-show="show_picture"  style="width:100%; height:190px;"  alt="">
                <svg v-show="!show_picture" xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-cloud-upload" width="44" height="44" viewBox="0 0 24 24" stroke-width="1.5" stroke="#717171" fill="none" stroke-linecap="round" stroke-linejoin="round">
                  <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                  <path d="M7 18a4.6 4.4 0 0 1 0 -9a5 4.5 0 0 1 11 2h1a3.5 3.5 0 0 1 0 7h-1" />
                  <polyline points="9 15 12 12 15 15" />
                  <line x1="12" y1="12" x2="12" y2="21" />
                </svg>
                <small v-show="!show_picture" style="color:#717171;">Upload</small>
              </label>
              <input  type="file" hidden  id="inputGroupFile01" v-on:change="previewFile" aria-describedby="inputGroupFileAddon01">

            </div>
        </div>
        <div>
            <MDBModalFooter>
            <MDBBtn style="font-size: 11px;" @click="$emit('close')" color="secondary" >Close</MDBBtn>
            <MDBBtn style="font-size: 11px;" @click="submit_add" color="success">Save</MDBBtn>
            </MDBModalFooter>
        </div>
    </div>
    
  </template>
  
  <script>
  import { MDBInput, MDBIcon, MDBModalFooter, MDBBtn, MDBSwitch, MDBTextarea  } from "mdb-vue-ui-kit";
  import erpSelect from '@/components/erpSelectAdd.vue'
  import {mapActions} from 'vuex'
  import axios from 'axios';
  
  export default {
    components: {
        MDBInput,
        MDBIcon,
        MDBModalFooter,
        erpSelect,
        MDBBtn,
        MDBSwitch,
        MDBTextarea
      },
      data(){
        return{
          user: {
            name: '',
            full_name: '',
            email: '',
            password: '',
            status: 1,
            phone: '',
            mobile_phone: '',
            description: '',
            image_base64: '',
          }

        }
      },
      props:{
        select_data:{
          type: Object,
          default: {}
        },
      },
    async mounted(){
      if(Object.keys(this.select_data).length != 0){
            this.user = {
              name: this.select_data.name,
              full_name: this.select_data.full_name,
              email: this.select_data.email,
              password: this.select_data.password,
              status: this.select_data.status,
              phone: this.select_data.phone,
              mobile_phone: this.select_data.mobile_phone,
              description: this.select_data.description,
              image_base64: this.select_data.image_base64,
            }
        }
      
    },

    methods:{
      ...mapActions([ 'fetch_user']),

      async submit_add(){
        if(Object.keys(this.select_data).length != 0){
          await this.fetch_user_update();
        }
        else{
          await this.fetch_user_new_add();
        }
      },

      async fetch_user_new_add(){
        var img = document.getElementById('prewImage');
        // console.log(img.src)
        this.user.image_base64 = img.src;
        try {
          const token = localStorage.getItem('auth_token'); // login paytida saqlangan token
          const response = await axios.post(
            this.$store.state.hostname + '/api/admin/users',
            this.user,
            {
              headers: {
                Authorization: `Bearer ${token}`
              }
            }
          );

          console.log('Yaratildi:', response.data);
          if(response.status == 200 || response.status == 201){
            this.$emit('close')
            await this.fetch_user();
          }
          // this.img_str = response.data.image_base64;
          alert("Foydalanuvchi qo‘shildi!");
        } catch (error) {
          console.error('Xatolik:', error.response?.data || error.message);
          alert("Xatolik yuz berdi!");
        }
      },

      async fetch_user_update (){
        var img = document.getElementById('prewImage');
          // console.log(img.src)
          this.user.image_base64 = img.src;
          try {
            const token = localStorage.getItem('auth_token'); // login paytida saqlangan token
            const response = await axios.put(
              this.$store.state.hostname + '/api/admin/users/' + this.select_data.id,
              this.user,
              {
                headers: {
                  Authorization: `Bearer ${token}`
                }
              }
            );

            console.log('Yaratildi:', response.data);
            if(response.status == 200 || response.status == 201){
              this.$emit('close')
              await this.fetch_user();
            }
            // this.img_str = response.data.image_base64;
            alert("Foydalanuvchi qo‘shildi!");
          } catch (error) {
            console.error('Xatolik:', error.response?.data || error.message);
            alert("Xatolik yuz berdi!");
          }
      },
      
      previewFile(){
        console.log('dsd')
        const preview = document.getElementById('prewImage');
        const file = document.querySelector('input[type=file]').files[0];
        const reader = new FileReader();
        reader.addEventListener("load", function () {
          preview.src = reader.result;
        }, false);
        if (file) {
          reader.readAsDataURL(file);
          this.show_picture = true;
        }
      }
    }
  }
  </script>
  
  <style>
  .picure_down{
    height: 190px;
    cursor: pointer;
    border-radius: 5px;
    display: flex;
    justify-content: center;
    align-items: center;
    flex-direction: column;
  }
  </style>