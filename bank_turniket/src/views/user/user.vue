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
              <!-- <MDBBtn style="font-size: 9px;" @click="del_all_user" color="danger">Все удалит</MDBBtn> -->
              <!-- <MDBBtn style="font-size: 9px;" @click="add_json_user" color="success">JSON</MDBBtn> -->
                <!-- <input
                    type="file"
                    id="formExcel"
                    class="form-icon-trailing mt-0 mb-2"
                    @change="inputExcel()"
                /> -->
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
    import readXlsFile from 'read-excel-file';
    import mdbtabled from '@/components/mdbtable.vue'
    import navbar from '@/components/navbar.vue'
    import dept_add from './user_add.vue'
    import {mapActions, mapGetters} from 'vuex'
    import jsonusers from '../../json/user.json'
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
              excelItems: jsonusers,
          }
      },
      async mounted(){
          await this.fetch_user();
          console.log(this.get_user_list)
        //   this.users_list = this.get_user_list;
          console.log(this.excelItems)
      },
      computed: {
      ...mapGetters(['get_user_list']),
      },
      methods:{
          ...mapActions(['fetch_user']),
          async inputExcel(){
            console.log('kirdi change ga')
            const input = document.getElementById("formExcel");
            console.log(input.file[0])
            readXlsFile(input.file[0]).then((rows)=>{
              this.excelItems = rows;
            })
            // console.log(this.excelItems)
          },
          async add_json_user(){
            
            for(let i=0; i<this.excelItems.length; i++){
              console.log(this.excelItems[i].name);
              await this.adduser(this.excelItems[i]);
            }
          },
          async adduser(data){
            let url = '/SkudMyUserinfoes';
            try{
              const requestOptions = {
              method: "POST",
              headers: { "Content-Type" : "application/json" },
              body: JSON.stringify({
                "userid": data.id,
                "badgenumber": data.id,
                "ism" : data.name,
                "gr": 1
                })
              };
              const response = await fetch(this.$store.state.hostname + url, requestOptions);
              const dataRes = await response.json();
              console.log(dataRes)
            }
            catch(error){
              console.log(error);
            }
          },
          async searchUser(){
            await this.fetch_user();
            if(this.search){
                this.get_user_list.rows = this.get_user_list.rows.filter((item)=>{
                    return this.search.toLowerCase().split(' ').every(v => item.ism.toLowerCase().includes(v))
                })
            }
            
          },
          addDept(){
              console.log('dept')
              this.select_data = {};
              this.show_dept = true;
              this.exampleModal = true;
          },
          selectData(data){
            console.log('select_date')
            console.log(data.userid)
            this.select_data = data
          },
          async deleteFunc(){
            try{
                console.log('deleteFunc')
                const requestOptions = {
                    method : "delete",
                };
                const response = await fetch(this.$store.state.hostname + "/SkudMyUserinfoes/" + this.select_data.userid, requestOptions);
                const data = await response.json();
                console.log('data')
                console.log(data)
                console.log(response)
                if(response.status == 200 || response.status == 201){
                    await this.fetch_user();
                }
            }
            catch(error){
                console.log(error)
            }
          },
          async editFunc(){
            this.show_dept = true;
            this.exampleModal = true;
            console.log('editFunc')
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