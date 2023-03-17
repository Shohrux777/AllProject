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
                        <MDBIcon icon="exclamation-circle" class="trailing"></MDBIcon>
                    </MDBInput>

                    <MDBInput
                        type="number"
                        id="form1"
                        size="sm"
                        class="form-icon-trailing mt-3"
                        :label="$t('User ID')"
                        v-model="id"
                    >
                        <MDBIcon icon="sort-numeric-down" class="trailing"></MDBIcon>
                    </MDBInput>

                    <MDBInput
                        type="text"
                        id="form1"
                        size="sm"
                        class="form-icon-trailing mt-3"
                        :label="$t('Card number')"
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
  
        }
      }
  }
  </script>
  
  <style>
  
  </style>