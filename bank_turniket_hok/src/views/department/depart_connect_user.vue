<template>
    <div>
        <loader v-if="loading" />
        <div v-else class=" box_connect_user">
            <div class="grafik_list border">
                <div class="item_user" :class="{'activ_item_choosen': i==activ_depart}"
                    v-for="(item, i) in this.get_deparment_list.rows" :key="i" 
                    @click="choose_department(item, i)">
                    <label style="font-weight:500; font-size:13.5px" class="m-0">
                        {{item.name}}
                    </label>
                </div>
                <div class="" style="position: absolute; bottom:5px;">
                    <MDBBtn style="font-size: 11px;" :disabled="!depart_id" @click="connect" color="success">Save</MDBBtn>
                </div>
            </div>
            <div class="selected_user">
                <div class="header_center text-center border-bottom mt-2">
                    <h5 class="text-primary">{{ depart_name }}</h5>
                </div>
                <div class="added_user_table">
                    <div class="search_user mt-2">
                    <div class="row mx-0">
                        <div class="col-12">
                            <MDBInput
                                type="text"
                                id="form1"
                                size="sm"
                                class="form-icon-trailing mt-0 mb-2"
                                :label="$t('search_client')"
                                v-model="searchUserA"
                                @input="searchUserAccept()"
                            >
                            </MDBInput>
                        </div>
                    </div>
                </div>
                    <MDBTable class="align-middle mb-0 bg-white">
                      <thead class="bg-light">
                        <tr>
                            <th width="30">№</th>
                            <th>{{$t('FIO')}}</th>
                            <th>ID</th>
                            <th width="50" class="text-center">{{$t('action')}}</th>
                        </tr>
                      </thead>
                      <tbody>
                      <tr v-for="(row, index) in Connected_userList" :key="index">
                        <td>{{index+1}}</td>
                          <td>
                          <div class="d-flex align-items-center">
                              <div class="ms-3">
                              <p class="fw-bold mb-1" v-if="row.ism != null">{{ row.ism }}</p>
                              <!-- <p class="text-muted mb-0" v-if="row.familiya != null">{{ row.familiya }}</p> -->
                              </div>
                          </div>
                          </td>
                          <td style="width:60px;">
                            {{ row.userid }}
                          </td>
                          <!-- <td>{{row.skudOylik.value}}</td> -->
                          <td class="text-center " 
                            @click="del_user_from_oylik(row)">
                                <MDBIcon   icon="trash" class="text-danger"  style="cursor:pointer;"/>
                          </td>
                      </tr>
                      </tbody>
                    </MDBTable>
                    <div class=" mt-4 user_not_depart_list bg-success border-bottom" style="height:3px;"></div>
                    
                    <MDBTable class="align-middle mb-0 bg-white" v-if="user_not_depart_list.length" user_not_depart_list>
                        <thead class="bg-light">
                          <tr>
                              <th width="30">№</th>
                              <th>{{$t('FIO')}}</th>
                              <th>Отдел</th>
                              <th>ID</th>
                              <th width="50" class="text-center">{{$t('action')}}</th>
                          </tr>
                        </thead>
                        <tbody>
                        <tr v-for="(row, index) in user_not_depart_list" :key="index" v-show="row.familiya">
                          <td>{{index+1}}</td>
                            <td>
                            <div class="d-flex align-items-center">
                                <div class="ms-3">
                                <p class="fw-bold mb-1" v-if="row.ism != null">{{ row.ism }}</p>
                                <!-- <p class="text-muted mb-0" v-if="row.familiya != null">{{ row.familiya }}</p> -->
                                </div>
                            </div>
                            </td>
                            <td>
                                <p class="text-muted mb-0" v-if="row.familiya != null">{{ row.familiya }}</p>
                            </td>
                            <td style="width:60px;">
                              {{ row.userid }}
                            </td>
                            <!-- <td>{{row.skudOylik.value}}</td> -->
                            <td class="text-center " 
                              @click="del_user_from_oylik(row)">
                                  <MDBIcon v-show="row.familiya" icon="trash" class="text-danger"  style="cursor:pointer;"/>
                            </td>
                        </tr>
                        </tbody>
                      </MDBTable>
                </div>
            </div>
            <div class="user_list border">
                <div class="all_choose border-bottom d-flex align-items-center px-1">
                    <input type="checkbox" style="width: 16px; height: 16px; margin-top: 3px; margin-right: 3px;" 
                        :value="all_check" 
                        id="all_select"
                        @click="check_all($event)" 
                    />
                    <label for="all_select" class="m-0 p-0 mt-1" style="font-weight:500; font-size:13.5px">Все выбрать</label>
                </div>
                <div class="search_user mt-2">
                    <div class="row mx-0">
                        <div class="col-12">
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
                    </div>
                </div>
                <div class="item_user d-flex" v-for="(item, i) in this.user_list_all" :key="i">
                    <input type="checkbox" style="width: 17px; height: 17px; margin-top: 3px; margin-right: 3px;" 
                        :value="item.userid" 
                        v-model="checkedUser[item.userid]" 
                        :id="item.ism"
                        @click="check_service($event, item)" 
                    />
                    <label style="font-weight:500; font-size:13.5px"
                        :for="item.ism" class="m-0">{{item.ism}}
                    </label>
                </div>
            </div>
        </div>
    </div>
    </template>
    
    <script>
    import loader from '@/components/loader.vue'
        import {mapActions, mapGetters} from 'vuex'
    import { MDBBtn, MDBTable, MDBIcon, MDBInput } from "mdb-vue-ui-kit";
    
    export default {
        components: {
          MDBBtn,
          MDBTable,
          loader,
          MDBIcon,
          MDBInput
        },
        data(){
            return{
                checkedUser: [
    
                ],
                user_id_list: [],
                all_check: false,
                activ_depart: -1,
                depart_name: '',
                depart_id: null,
                loading: false,
                Connected_userList: [],
                search: '',
                searchUserA: '',
    
                activ_item: {},
                user_list_all: [],
                user_not_depart_list: [],
            }
        },
        computed: mapGetters(['get_nosalary_user','get_salary_list', 'get_deparment_list', 'get_user_list']),
        async mounted(){
            await this.fetch_Department();
            await this.get_notDepart_user();
        },
        methods:{
              ...mapActions(['fetch_nosalary_user', 'fetch_Salary', 'fetch_Department', 'fetch_user']),
              async get_notDepart_user(){
                await this.fetch_user();
                let user_undepart_list = [];
                for(let i=0; i<this.get_user_list.rows.length; i++){
                    if(this.get_user_list.rows[i].departid == null){
                        user_undepart_list.push(this.get_user_list.rows[i]);
                    }
                }
                this.user_list_all = user_undepart_list;
              },
                async searchUser(){
                    await this.get_notDepart_user();
                    // this.user_list_all = this.get_nosalary_user;
                    if(this.search){
                        this.user_list_all = this.user_list_all.filter((item)=>{
                            return this.search.toLowerCase().split(' ').every(v => item.ism.toLowerCase().includes(v))
                        })
                    }
                },
                async searchUserAccept(){
                    await this.department_id_users(this.depart_id);
                    if(this.searchUserA){
                        this.Connected_userList = this.Connected_userList.filter((item)=>{
                            return this.searchUserA.toLowerCase().split(' ').every(v => item.ism.toLowerCase().includes(v))
                        })
                        this.user_not_depart_list = this.user_not_depart_list.filter((item)=>{
                            return this.searchUserA.toLowerCase().split(' ').every(v => item.ism.toLowerCase().includes(v))
                        })
                    }
                },
              async choose_department(item, i){
                this.activ_depart = i;
                this.activ_item = item;
                console.log(item);
                this.depart_name = item.name;
                this.depart_id = item.id;
                await this.department_id_users(item.id);
                // await this.fetch_get_oylik_user(item.id);
    
              },
              async department_id_users(id){
                await this.fetch_user();
                let user_depart_list = [];
                this.user_not_depart_list = [];
                for(let i=0; i<this.get_user_list.rows.length; i++){
                    if(this.get_user_list.rows[i].departid == id){
                        user_depart_list.push(this.get_user_list.rows[i]);
                    }
                    else{
                        this.user_not_depart_list.push(this.get_user_list.rows[i]);
                    }
                }
                this.Connected_userList = user_depart_list;
                console.log('this.Connected_userList')
                console.log(this.Connected_userList)
              },
              check_service: function(e, item){
                if (e.target.checked){
                    console.log('true', item)
                    console.log(this.checkedUser)
                    this.user_id_list.push(item);
                }
                else{
                    console.log('false')
                    console.log(this.user_id_list)
                    for(let i=0; i<this.user_id_list.length; i++){
                        if(this.user_id_list[i].userid == item.userid){
                            this.user_id_list.splice(i,1);
                        }
                    }
                    console.log('this.user_id_list')
                    console.log(this.user_id_list)
                }
              },
              check_all(e){
                if (e.target.checked){
                    for(let i=1; i<=500; i++){
                        this.checkedUser.push(true)
                    }
                    for(let j=0; j<this.user_list_all.length; j++){
                        this.user_id_list.push(this.user_list_all[j])
                    }
                    console.log(this.user_id_list)
                    console.log('ture')
                }
                else{
                    console.log('false')
                    this.checkedUser = [];
                    this.user_id_list = [];
                }
              },
    
              async connect(){
                this.loading = true;
                // console.log('this.user_id_list')
                // console.log(this.user_id_list)
                for( let i=0; i<this.user_id_list.length; i++){
                    await this.update_user(this.user_id_list[i], this.depart_id, this.depart_name)
                }
                this.loading = false;
                this.user_id_list = [];
                this.checkedUser = [];
                // this.all_check = false;
                // await this.fetch_nosalary_user();
                // this.user_list_all = this.get_nosalary_user;
                // await this.fetch_get_oylik_user(this.depart_id);
              },
    
            async update_user(user_data, dept_id, dept_name){
                // console.log(user_data)
                // console.log(dept_id)
                // console.log(dept_name)
                try{
                    const requestOptions = {
                    method: "POST",
                    headers: { "Content-Type" : "application/json" },
                    body: JSON.stringify({
                        "userid": user_data.userid,
                        "badgenumber": user_data.userid,
                        "ism" : user_data.ism,
                        "cardno" : user_data.cardno,
                        "departid" : dept_id,
                        "familiya": dept_name,
                        "image_url": user_data.image_url,
                        "res_badgenumber": user_data.res_badgenumber,
                        "without_gr_id": user_data.without_gr_id,
                        "gr": user_data.gr
                        })
                    };
                    const response = await fetch(this.$store.state.hostname + '/SkudMyUserinfoes/updateUserInfo', requestOptions);
                    const data = await response.json();
                    // console.log('data update')
                    // console.log(data)
                    if(data.userid != 0){
                        // console.log('update user')
                        await this.department_id_users(this.depart_id);
                        await this.get_notDepart_user();
                    }
                    }
                    catch(error){
                        console.log('error')
                        console.log(error)
                    }
             }, 
              async del_user_from_oylik(data){
                // console.log(data)
                let dept_name = '';
                let dept_id = null;
                await this.update_user(data, dept_id,dept_name)
              } 
          }
    }
    </script>
    
    <style>
    .box_connect_user{
        display: flex;
        width: 100%;
        height: 94vh;
    
    }
    .grafik_list{
        width: 25%;
        height: 94vh;
    }
    .user_list{
        width: 25%;
        height: 94vh;
        overflow-y: scroll;
    }
    .selected_user{
        width: 50%;
    }
    .user_list .item_user{
        padding: 3px 5px;
        
    }
    .user_list .item_user:hover{
        background: #cccccc;
    }
    
    .grafik_list .item_user{
        padding: 3px 5px;
    }
    .grafik_list .item_user:hover{
        background: #cccccc;
    }
    
    .activ_item_choosen{
        background: #8d8d8d;
        color:white;
    }
    .added_user_table th{
        font-size: 12px;
        padding: 4px 7px;
    }
    .added_user_table td{
        font-size: 12px;
        padding: 4px 7px;
    }
    .added_user_table{
        height: 88vh;
        overflow-y: scroll;
    }
    </style>