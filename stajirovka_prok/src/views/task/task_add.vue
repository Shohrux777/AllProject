<template>
    <div class="container-fluid">
        <div class="row">
            <div class="col-12">
                <form @submit.prevent="submit_add" class="container">
                    <MDBInput
                        type="text"
                        id="form1"
                        size="sm"
                        class="form-icon-trailing"
                        label="Topshiriq nomi"
                        v-model="task.name"
                    >
                    </MDBInput>

                    <MDBInput
                        type="number"
                        id="form1"
                        size="sm"
                        class="form-icon-trailing mt-3"
                        label="Topshiriq soni"
                        v-model="task.count"
                    >
                    </MDBInput>


                    <MDBTextarea label="Tavsif" 
                        class="mt-3"
                        size="sm"
                        rows="2" v-model="task.note" />

                    <VueEditor 
                      class="mt-3"
                     :editorToolbar="customToolbar" 
                     v-model="task.description"
                     placeholder="Topshiriqqa joylanishi kerak bo'lgan hujjatlar"
                     ref="myEditor"
                     />
                    <hr class="mt-4">
                </form>
            </div>
        </div>
        <div>
            <MDBModalFooter>
            <!-- <MDBBtn style="font-size: 11px;" @click="$emit('close')" color="secondary" >Yopish </MDBBtn> -->
            <MDBBtn style="font-size: 11px;" @click="submit_add" color="success">Saqlash</MDBBtn>
            </MDBModalFooter>
        </div>
    </div>
    
  </template>
  
  <script>
  import { MDBInput, MDBIcon, MDBModalFooter, MDBBtn, MDBSwitch, MDBTextarea  } from "mdb-vue-ui-kit";
  import erpSelect from '@/components/erpSelectAdd.vue'
  import {mapActions} from 'vuex'
  import axios from 'axios';
  import { VueEditor, Quill } from "vue3-editor";
  
  export default {
    components: {
        MDBInput,
        MDBIcon,
        MDBModalFooter,
        erpSelect,
        MDBBtn,
        MDBSwitch,
        MDBTextarea,
        VueEditor,
        Quill
      },
      data(){
        return{
          task: {
            name: '',
            count: 50,
            status: "pending",
            note: '',
            description: '',
          },
          customToolbar: [
              ['bold', 'italic', 'underline', 'strike'],
              [{ 'direction': 'rtl' }],
              ['link', 'image', 'video'],
              [{ 'align': [] }],
              [{ 'list': 'ordered'}, { 'list': 'bullet' }],
              ['blockquote', 'code-block'], 
              ['link', 'image'],
              [{ 'color': [] }, { 'background': [] }], 
              [{ 'script': 'sub' }, { 'script': 'super' }],
            ]
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
            this.task = {
              name: this.select_data.name,
              count: this.select_data.count,
              note: this.select_data.note,
              description: this.select_data.description,
            }
        }      
    },

    methods:{
      ...mapActions([ 'fetch_task']),

      async submit_add(){
        if(Object.keys(this.select_data).length != 0){
          await this.fetch_task_update();
        }
        else{
          await this.fetch_task_new_add();
        }
      },

      async fetch_task_new_add(){
        console.log(this.task)
        try {
          const token = localStorage.getItem('auth_token'); // login paytida saqlangan token
          const response = await axios.post(
            this.$store.state.hostname + '/api/tasks',
            this.task,
            {
              headers: {
                Authorization: `Bearer ${token}`
              }
            }
          );

          console.log('Yaratildi:', response.data);
          if(response.status == 200 || response.status == 201){
            this.$emit('close')
            await this.fetch_task();
          }
          // this.img_str = response.data.image_base64;
          alert("Topshiriq qo‘shildi!");
        } catch (error) {
          console.error('Xatolik:', error.response?.data || error.message);
          alert("Xatolik yuz berdi!");
        }
      },

      async fetch_task_update (){
          try {
            const token = localStorage.getItem('auth_token'); // login paytida saqlangan token
            const response = await axios.put(
              this.$store.state.hostname + '/api/tasks/' + this.select_data.id,
              this.task,
              {
                headers: {
                  Authorization: `Bearer ${token}`
                }
              }
            );
            console.log('Yaratildi:', response.data);
            if(response.status == 200 || response.status == 201){
              this.$emit('close')
              await this.fetch_task();
            }
            // this.img_str = response.data.image_base64;
            alert("Topshiriq qo‘shildi!");
          } catch (error) {
            console.error('Xatolik:', error.response?.data || error.message);
            alert("Xatolik yuz berdi!");
          }
      },
      
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