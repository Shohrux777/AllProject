<template>
    <div class="deparment_page">
      <navbar :title = "$t('user_arxiv')" :added_status="false"/>
      <div class="mainpage">
        <div class="row">
            <div class="col-4">
                <select v-model="selectedYear" @change="fetch_noActiv_user" id="yearSelect" class="form-select shadow-sm p-0 p-1 px-2 border-primary">
                    <option v-for="year in years" :key="year" :value="year">{{ year }}</option>
                </select>
            </div>
            <div class="col-5">
              
            </div>
            <div class="col-3 d-flex justify-content-end">
            </div>
            <!-- <p>{{ excelItems }}</p> -->
        </div>
            
        <mdbtabled
        :options="noActivUser"
        @selectData="selectData"
        ></mdbtabled>
      </div>
      
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
        const now = new Date();
          return{
              show_dept: false,
              select_data: null,
              search: '',
              users_list: {},
              selectedYear: now.getFullYear(),
              noActivUser: {
                rows: [],
                columns: [ 'name','full_name', 'passport', 'phone', 'mobile_phone' ],
                col: []
              }
          }
      },
      async mounted(){
          await this.fetch_noActiv_user();
      },
      computed: {
      ...mapGetters(['get_user_list']),
      years() {
            const currentYear = new Date().getFullYear();
            const startYear = currentYear - 5;
            return Array.from({ length: 50 }, (_, i) => startYear + i);
        }
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
        async fetch_noActiv_user(){
            console.log(this.selectedYear)
            const token = localStorage.getItem('auth_token') // <<< bu yerga tokenni yozing

            try {
                const response = await axios.get(this.$store.state.hostname + '/api/by_year/', {
                params: {
                    year: this.selectedYear
                },
                headers: {
                    Authorization: `Bearer ${token}`
                }
                })

                this.noActivUser.rows = response.data
                console.log(response.data)
                console.log(response)
            } catch (err) {
                this.noActivUser.rows = [];
                this.error = err.response?.data?.message || err.message
            } finally {
                this.loading = false
            }
        },
        
          addDept(){
              this.select_data = {};
              this.show_dept = true;
              this.exampleModal = true;
          },
          selectData(data){
            this.select_data = data // tanlanga userni malumotlarini saqlaydi
            this.$router.push('/topshiriqlar/' + data.id);
          },

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
  .form-select {
  max-width: 100px;
  font-weight: 500;
  font-size: 15px;
  transition: border-color 0.3s, box-shadow 0.3s;
}

.form-select:focus {
  border-color: #0d6efd;
  box-shadow: 0 0 0 0.25rem rgba(13, 110, 253, 0.25);
}
  </style>