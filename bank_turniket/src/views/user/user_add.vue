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
                        :label="$t('FIO')"
                        v-model="fio"
                    >
                    </MDBInput>

                    <MDBInput
                        type="number"
                        id="form1"
                        size="sm"
                        class="form-icon-trailing mt-3"
                        :label="$t('id')"
                        v-model="id"
                    >
                    </MDBInput>

                    <MDBInput
                        type="text"
                        id="form1"
                        size="sm"
                        class="form-icon-trailing mt-3"
                        :label="$t('cardno')"
                        v-model="card_number"
                    >
                        <MDBIcon icon="credit-card" class="trailing"></MDBIcon>
                    </MDBInput>
                
                    <erpSelect
                        :options = "get_deparment_list.rows" 
                        @select="sub_debt_select"
                        :selected="subdept_name"
                        :label="$t('otdel')"
                        class="mt-2"
                    />
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
  import { MDBInput, MDBIcon, MDBModalFooter, MDBBtn } from "mdb-vue-ui-kit";
  import erpSelect from '@/components/erpSelectAdd.vue'
  import {mapActions, mapGetters} from 'vuex'
  
  export default {
    components: {
        MDBInput,
        MDBIcon,
        MDBModalFooter,
        erpSelect,
        MDBBtn
      },
      data(){
        return{
          fio: '',
          card_number: '',
          id: 0,
          subdept_name: '',
          subdept_id: null,
          show_picture: false,
        }
      },
    computed: mapGetters(['get_deparment_list']),
    async mounted(){
        await this.fetch_Department();
    },

      methods:{
        ...mapActions(['fetch_Department', 'fetch_user']),
  
        sub_debt_select(option){
          console.log(option)
          this.subdept_name = option.name;
          this.subdept_id = option.id;
        },
        async submit_add(){
          try{
            const requestOptions = {
            method: "POST",
            headers: { "Content-Type" : "application/json" },
            body: JSON.stringify({
              "userid": this.id,
              "ism" : this.fio,
              "cardno" : this.card_number,
              "departid" : this.subdept_id,
              "familiya": this.subdept_name,
              })
            };
            const response = await fetch(this.$store.state.hostname + "/SkudMyUserinfoes", requestOptions);
            const data = await response.json();
            console.log(data)
            if(data.userid != 0){
              this.$emit('close')
              await this.fetch_user();
            }
          }
          catch{
            console.log('error')
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