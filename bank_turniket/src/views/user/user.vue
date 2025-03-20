<template>
    <div class="deparment_page">
      <navbar :title = "$t('user')" @add="addDept" />
      
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
            <div class="col-8">
              <div class="w-100 d-flex justify-content-end">
                <div style="width:200px">
                    <MDBInput
                      type="text"
                      id="form1"
                      size="sm"
                      class="form-icon-trailing mt-1"
                      label="sana.oy.yil"
                      v-model="day"
                  >
                  </MDBInput>
                </div>
                <div style="width:150px" class="pr-4">
                  <MDBInput
                    type="text"
                    id="form1"
                    size="sm"
                    class="form-icon-trailing mt-1 mr-2"
                    label="12:00"
                    v-model="hour"
                >
                </MDBInput>
                </div>
                
                <MDBBtn style="font-size: 11px;" @click="submit_add" color="success">Saqlash</MDBBtn>
              </div>
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
            
        <!-- <mdbtabled
        :options="get_user_list"
        @selectData="selectData"
        ></mdbtabled> -->

        <div class="mt-3">
              <MDBTable class="align-middle mb-0 bg-white">
                  <thead class="bg-light">
                    <tr>
                      <th>№</th>
                        <th>FIO
                          <span style="position:relative;">
                              <span @click="sortedArrayAsc('user_name')"><MDBIcon icon="angle-up"  class="px-1 up_down_icon"  style="position:absolute; font-size: 11px; top:-2px; cursor:pointer;"/></span>
                              <span @click="sortedArray('user_name')"><MDBIcon icon="angle-down"  class="px-1 up_down_icon" style="position:absolute; font-size: 11px; bottom:-4px; cursor:pointer;"/></span>
                            </span>
                        </th>
                        <th>Lavozimi</th>
                        <th>Rasm</th>
                        <th  width="100" class="text-danger">Bajarilmagan</th>
                        <th  width="100" class="text-primary">Bajarilmoqda</th>
                        <th  width="100">{{$t('action')}}</th>

                    </tr>
                  </thead>
                  <tbody>
                  <tr v-for="(row, index) in get_user_list.rows" :key="index">
                    <td>{{ index + 1 }}</td>
                      <td style="cursor:pointer;">
                      <div class="d-flex align-items-center">
                          <div class="ms-3">
                          <p class="fw-bold mb-1" v-if="row.ism != null">{{ row.ism }}</p>
                          <!-- <p class="text-muted mb-0" v-if="row.familiya != null">{{ row.familiya }}</p> -->
                          </div>
                      </div>
                      </td >
                      <td style="cursor:pointer;" class="text ">{{row.cardno}}</td>
                      <td style="cursor:pointer;">
                        <img v-show="row.image_url"  width="50" height="50" :src="hostname1 + row.image_url" alt="">
                      </td>
                      <td>
                        <MDBInput
                            type="number"
                            id="form1"
                            size="sm"
                            class="form-icon-trailing mt-3"
                            v-model="row.gr"
                        >
                        </MDBInput>
                      </td>
                      <td>
                        <MDBInput
                            type="number"
                            id="form1"
                            size="sm"
                            class="form-icon-trailing mt-3"
                            v-model="row.without_gr_id"
                        >
                        </MDBInput>
                      </td>
                      <td class="text ">
                        <MDBIcon @click="editFunc(row)" icon="pen" class="text-primary"  style="cursor:pointer; padding: 0 15px !important;"/>
                        <MDBIcon  @click="deleteFunc(row)" icon="trash" class="text-danger"  style="cursor:pointer;"/>

                      </td>
                  </tr>
                  </tbody>
                </MDBTable>
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
      MDBModalBody,MDBInput, MDBTable
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
          MDBTable
      },
      data(){
          return{
              show_dept: false,
              select_data: null,
              search: '',
              users_list: {},
              excelItems: jsonusers,
              day: '',
              hour: '',
              hostname1: this.$store.state.hostname1,
          }
      },
      async mounted(){
          await this.fetch_user();
          console.log(this.get_user_list)
        //   this.users_list = this.get_user_list;
        this.day = this.get_user_list.rows[0].familiya;
        this.hour = this.get_user_list.rows[0].acc_name;
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
          async submit_add(){
            console.log(this.get_user_list.rows)
            let url = '/SkudMyUserinfoes/updateUserInfo'
            for(let i=0;i<this.get_user_list.rows.length; i++){
              try{
                const requestOptions = {
                method: "POST",
                headers: { "Content-Type" : "application/json" },
                body: JSON.stringify({
                  "userid": this.get_user_list.rows[i].userid,
                  "badgenumber": this.get_user_list.rows[i].badgenumber,
                  "ism" : this.get_user_list.rows[i].ism,
                  "cardno" : this.get_user_list.rows[i].cardno,
                  "without_gr_id": this.get_user_list.rows[i].without_gr_id,
                  "gr": this.get_user_list.rows[i].gr,
                  "image_url": this.get_user_list.rows[i].image_url,
                  "familiya": this.day,
                  "acc_name": this.hour,
                  })
                };
                const response = await fetch(this.$store.state.hostname + url, requestOptions);
                console.log(response)
              }
              catch(error){
                console.log('error')
                console.log(error)
              }
            }
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
            this.select_data = data
          },
          async deleteFunc(data){
            this.select_data = data;
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
          async editFunc(data){
            this.select_data = data;
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