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
                        label="Ф.И.Ш"
                        v-model="name"
                    >
                    </MDBInput>

                    <MDBInput
                        type="text"
                        id="form1"
                        size="sm"
                        class="form-icon-trailing mt-3"
                        label="Лавозим"
                        v-model="full_name"
                    >
                    </MDBInput>

                    
                    <MDBInput
                        type="text"
                        id="form1"
                        size="sm"
                        class="form-icon-trailing mt-3"
                        label="ЖШШИР"
                        v-model="passport"
                    >
                    </MDBInput>

                    <MDBInput
                        type="text"
                        id="form1"
                        size="sm"
                        class="form-icon-trailing mt-3"
                        label="IP"
                        v-model="phone"
                    >
                    </MDBInput>

                    <MDBInput
                        type="text"
                        id="form1"
                        size="sm"
                        class="form-icon-trailing mt-3"
                        label="Телифон рақами"
                        v-model="mobile_phone"
                    >
                    </MDBInput>



                    <!-- <MDBTextarea label="Tavsif" 
                        class="mt-3"
                        size="sm"
                        rows="2" v-model="description" /> -->

                        <hr class="mt-4">
                    <MDBInput
                        type="text"
                        id="form1"
                        size="sm"
                        class="form-icon-trailing mt-3"
                        label="Email"
                        v-model="email"
                    >
                    </MDBInput>
                    <MDBInput
                        type="password"
                        id="form1"
                        size="sm"
                        class="form-icon-trailing mt-3"
                        label="Password"
                        v-model="password"
                    >
                    </MDBInput>
                    <!-- :label="$t('Familiya')" -->
                </form>
            </div>
            <div class="col-3">
              <label for="inputGroupFile01" class="picure_down border">
              <img :src="hostname2+image_base64" id="prewImage" v-show="show_picture"  style="width:100%; height:190px;"  alt="" />
                <svg v-if="!show_picture" xmlns="http://www.w3.org/2000/svg" 
                  class="icon icon-tabler icon-tabler-cloud-upload" width="44"
                  style="cursor:pointer;" 
                  height="44" viewBox="0 0 24 24" stroke-width="1.5" stroke="#717171" fill="none" stroke-linecap="round" stroke-linejoin="round">
                  <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                  <path d="M7 18a4.6 4.4 0 0 1 0 -9a5 4.5 0 0 1 11 2h1  a3.5 3.5 0 0 1 0 7h-1" />
                  <polyline points="9 15 12 12 15 15" />
                  <line x1="12" y1="12" x2="12" y2="21" />
                </svg>
              </label>
              <input  type="file" hidden  id="inputGroupFile01" v-on:change="previewFile" aria-describedby="inputGroupFileAddon01">
              <div class="d-flex justify-content-center">
                <small  style="color:#717171;">Фото</small>
              </div>
              
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
          
            name: '',
            full_name: '',
            email: '',
            password: '',
            passport: '',
            status: 1,
            phone: '',
            mobile_phone: '',
            description: 'Yaratildi',
            image_base64: '',
            picture_user: '',
            show_picture: false,
            hostname2: this.$store.state.hostname2,
          file: null,

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
            
              this.name = this.select_data.name;
              this.full_name = this.select_data.full_name;
              this.email= this.select_data.email;
              this.password= this.select_data.password;
              this.status= this.select_data.status;
              this.phone= this.select_data.phone;
              this.mobile_phone= this.select_data.mobile_phone;
              this.description= this.select_data.description;
              this.image_base64= this.select_data.image_base64;
              this.passport= this.select_data.passport;
              this.show_picture = true;
           
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
        try {
          const token = localStorage.getItem('auth_token'); // login paytida saqlangan token
          console.log(this.name)
          const formData = new FormData();
          formData.append('name', this.name);
          formData.append('full_name', this.full_name);
          formData.append('email', this.email);
          formData.append('password', this.password);
          formData.append('phone', this.phone);
          formData.append('mobile_phone', this.mobile_phone);
          formData.append('description', this.description);
          formData.append('passport', this.passport);
          formData.append('task_count', this.select_data.task_count);
          formData.append('task_id', this.select_data.task_id);
          formData.append('task_info', this.select_data.task_info);
          formData.append('dead_line', this.select_data.dead_line);
          formData.append('image_base64', this.file); // Fayl (PDF yoki JPG)
          console.log('formData', formData);
          const response = await axios.post(this.$store.state.hostname + '/api/admin/users', formData, {
            headers: {
              'Content-Type': 'multipart/form-data',
              'Authorization': `Bearer ${token}` // Agar token kerak bo‘lsa
            }
          })

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
        try {
          const token = localStorage.getItem('auth_token'); // login paytida saqlangan token
          const formData = new FormData();
          formData.append('name', this.name);
          formData.append('full_name', this.full_name);
          formData.append('email', this.email);
          formData.append('password', this.password);
          formData.append('phone', this.phone);
          formData.append('mobile_phone', this.mobile_phone);
          formData.append('description', this.description);
          formData.append('passport', this.passport);
          formData.append('image_base64', this.file); // Fayl (PDF yoki JPG)
          const response = await axios.put(this.$store.state.hostname + '/api/admin/users/' + this.select_data.id, formData, {
            headers: {
              'Content-Type': 'multipart/form-data',
              'Authorization': `Bearer ${token}` // Agar token kerak bo‘lsa
            }
          })
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
      
      previewFile(event){
        this.show_picture = true;
        console.log('dsd')
        this.file = event.target.files[0];
        const preview = document.getElementById('prewImage');
        const files = document.querySelector('input[type=file]').files[0];
        const reader = new FileReader();
        reader.addEventListener("load", function () {
          preview.src = reader.result;
          this.picture_user = reader.result;
        }, false);
        if (files) {
          reader.readAsDataURL(files);
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