<template>
    <div class="deparment_page">
      <navbar :title = "$t('user')" @add="addDept" @editFunc="editFunc" @deleteFunc="deleteFunc"/>
      
      <div class="mainpage">
        <div class="row">
            <div class="col-4">
                <MDBInput
                    type="text"
                    id="form1"
                    size="sm"
                    class="form-icon-trailing mt-0 mb-2"
                    :label="$t('search_client')"
                    v-model="search"
                    @input="searchUser()"
                >
                </MDBInput>
            </div>
            <div class="col-5">
              
            </div>
            <div class="col-3 d-flex justify-content-end">
            </div>
            <!-- <p>{{ excelItems }}</p> -->
        </div>
            
        <mdbtabled
        :options="get_user_list"
        @selectData="selectData"
        ></mdbtabled>
      </div>
      <MDBModal
          id="exampleModal"
          tabindex="-1"
          labelledby="exampleModalLabel"
          v-model="exampleModal"
          staticBackdrop
          size="lg"
      >
          <MDBModalHeader style="background: #475a65;" class="text-white px-3 py-2">
              <MDBModalTitle id="exampleModalLabel"> {{$t('user')}} </MDBModalTitle>
          </MDBModalHeader>
          <MDBModalBody>
              <dept_add :select_data="select_data" @close="exampleModal = false"></dept_add>
          </MDBModalBody>
          
      </MDBModal>
    </div>
  </template>
  
  <script>
    import {
      MDBBtn,
      MDBBadge,
      MDBIcon,
      MDBModal,
      MDBModalHeader,
      MDBModalTitle,
      MDBModalBody,MDBInput
    } from 'mdb-vue-ui-kit';
    import { ref } from 'vue';
    import axios from 'axios';
    // import readXlsFile from 'read-excel-file';
    import mdbtabled from '@/components/mdbtable.vue'
    import navbar from '@/components/navbar.vue'
    import dept_add from './user_add.vue'
    import {mapActions, mapGetters} from 'vuex'
    export default {
      setup() {
        const exampleModal = ref(false);
        return {
          exampleModal,
        };
      },
      components: {
          MDBBtn,
          MDBBadge,
          MDBIcon,
          MDBModal,
          MDBModalHeader, MDBInput,
          MDBModalTitle,
          MDBModalBody,
          navbar,
          dept_add,
          mdbtabled,
      },
      data(){
          return{
              show_dept: false,
              select_data: null,
              search: '',
              users_list: {},
          }
      },
      async mounted(){
          await this.fetch_user();
      },
      computed: {
      ...mapGetters(['get_user_list']),
      },
      methods:{
          ...mapActions(['fetch_user']),
          async searchUser(){
            await this.fetch_user();
            if(this.search){
                this.get_user_list.rows = this.get_user_list.rows.filter((item)=>{
                    return this.search.toLowerCase().split(' ').every(v => item.name.toLowerCase().includes(v))
                })
            }
          },

          addDept(){
              this.select_data = {};
              this.show_dept = true;
              this.exampleModal = true;
          },
          selectData(data){
            this.select_data = data // tanlanga userni malumotlarini saqlaydi
          },

          async deleteFunc(){
            const token = localStorage.getItem('auth_token');

            try {
              const response = await axios.delete(
                this.$store.state.hostname  + `/api/admin/users/${this.select_data.id}`,
                {
                  headers: {
                    Authorization: `Bearer ${token}`,
                    Accept: 'application/json'
                  }
                }
              );
              if (response.status === 200) {
                console.log('Foydalanuvchi o‘chirildi');
                this.fetch_user(); // Yana yangilab olish
              }
            } catch (error) {
              console.error('O‘chirishda xatolik:', error);
            }
          },
          async editFunc(){
            this.show_dept = true;
            this.exampleModal = true;
          }
      }
    };
  </script>
  
  <style>
  .mainpage{
      height: calc(100vh - 85px);
      overflow-y: scroll;
      padding:25px 15px;
  }
  .main_table{
      padding: 15px;
      box-shadow: rgba(0, 0, 0, 0.1) 0px 4px 12px;
      border-radius: 5px;
  }
  .table_header{
      /* position:sticky; 
      top:-25px;
      z-index: 2; */
      background:#e2e2e2;
  }
  th,td{
      padding: 5px !important;
      font-size: 12.5px;
  }
  </style>