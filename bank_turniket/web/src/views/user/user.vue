<template>
    <div class="deparment_page">
      <navbar :title = "$t('user')" @add="addDept" @editFunc="editFunc" @deleteFunc="deleteFunc"/>
      
      <div class="mainpage">
        <div class="main_table">
          <div class="row filter_card">
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
            
          <div class="table_wrap mt-3">
            <mdbtabled
            :options="get_user_list"
            @selectData="selectData"
            ></mdbtabled>
          </div>
        </div>
      </div>
      <MDBModal
          id="exampleModal"
          tabindex="-1"
          labelledby="exampleModalLabel"
          v-model="exampleModal"
          staticBackdrop
          size="lg"
          class="modern_modal"
      >
          <MDBModalHeader class="modern_modal_header text-white px-3 py-2">
              <MDBModalTitle id="exampleModalLabel" class="modern_modal_title"> {{$t('user')}} </MDBModalTitle>
          </MDBModalHeader>
          <MDBModalBody class="modern_modal_body">
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
  
  <style scoped>
  .deparment_page{
      /* min-height: 100vh; */
      background: linear-gradient(180deg, #f4f7fb 0%, #eef3f9 100%);
  }
  .mainpage{
      height: calc(100vh - 85px);
      overflow-y: auto;
      padding:10px 15px;
  }
  .main_table{
      padding: 16px;
      background: #ffffff;
      border: 1px solid #e8eef6;
      box-shadow: 0 10px 30px rgba(35, 64, 96, 0.08);
      border-radius: 14px;
  }
  .filter_card{
      background: #f8fbff;
      border: 1px solid #e4edf7;
      border-radius: 10px;
      padding: 10px 6px 2px;
  }
  .table_wrap{
      border: 1px solid #e4edf7;
      border-radius: 12px;
      overflow: auto;
      max-height: calc(100vh - 230px);
      background: #fff;
  }
  th,td{
      padding: 5px !important;
      font-size: 12.5px;
  }
  .modern_modal :deep(.modal-content){
      border: none;
      border-radius: 14px;
      overflow: hidden;
      box-shadow: 0 18px 40px rgba(27, 42, 52, 0.25);
  }
  .modern_modal_header{
      background: linear-gradient(135deg, #3d5663 0%, #2f4551 100%);
      border-bottom: none;
  }
  .modern_modal_title{
      font-size: 15px;
      font-weight: 600;
  }
  .modern_modal_body{
      background: linear-gradient(180deg, #f9fcff 0%, #f3f8ff 100%);
  }
  </style>