<template>
<div>
    <loader v-if="loading" />
    <div v-else class=" box_connect_user">
        <div class="grafik_list border">
            <div class="item_user border-bottom mt-1" :class="{'activ_item_choosen': i==activ_smena, 'item_warning_different': item.reserved_value == 2, 'item_success_different': item.reserved_value == 1,}"
                v-for="(item, i) in this.get_salary_list.rows" :key="i" 
                @click="choose_salary(item, i)">
                
                <label style="font-weight:500; font-size:13px" class="m-0 px-2">
                    {{item.name}} 
                </label>
            </div>
            <div class="" style="position: absolute; bottom:2px;">
                <mdb-btn style="font-size: 10px;" class="py-2 px-3" :disabled="!salary_id" @click="connect" color="success">Добавить работников</mdb-btn>
            </div>
        </div>
        <div class="selected_user">
            <div class="header_center text-center border-bottom mt-2">
                <h6 @click="refresh_salary" class="text-primary">{{ salary_name }}</h6>
            </div>
            <div class="added_user_table w-100">
                <div class="search_user mt-2">
                  <div class="row mx-0">
                      <div class="col-12">
                          <mdb-input
                              type="text"
                              id="form1"
                              size="sm"
                              outline
                              class="form-icon-trailing mt-0 mb-2 "
                              :label="$t('search_client')"
                              v-model="searchUserA"
                              @input="searchUserAccept()"
                          >
                          </mdb-input>
                      </div>
                  </div>
                </div>
                <table class=" mb-0 bg-white w-100 " >
                  <thead class="bg-light border-bottom">
                    <tr>
                        <th width="30">№</th>
                        <th>{{$t('FIO')}}</th>
                        <th>ID</th>
                        <th>{{$t('salary')}}</th>
                        <th class="text-center">{{$t('action')}}</th>
                    </tr>
                  </thead>
                  <tbody v-if="Connected_userList.length">
                  <tr v-for="(row, index) in Connected_userList" :key="index" class="border-bottom">
                    <td>{{index+1}}</td>
                      <td>
                      <div class="d-flex align-items-center">
                          <div class="ms-3">
                          <p class="fw-bold mb-1" v-if="row.ism != null">{{ row.ism }}</p>
                          <p class="text-muted mb-0" style="font-size:10px;" v-if="row.familiya != null">{{ row.familiya }}</p>
                          </div>
                      </div>
                      </td>
                      <td style="width:60px;">
                        {{ row.userid }}
                      </td>
                      <td>{{row.skudOylik.value}}</td>
                      <td class="text-center " 
                        @click="del_user_from_oylik(row)">
                            <mdb-icon  icon="trash" class="text-danger"  style="cursor:pointer;"/>
                      </td>
                  </tr>
                  </tbody>
                  <tbody v-else>
                    <tr><td class="text-center" colspan="6">No result</td></tr>
                  </tbody>
                </table>
                <hr class="mt-4"/>
                <div class="d-flex justify-content-center mt-0"  v-if="user_list_connect_full.length">
                  <span style="font-size:14px;">Boshqa oylikga ulanganlar ruyxati</span>
                </div>
                <div>
                    <table class=" mb-0 mt-1 bg-white w-100" v-show="user_list_connect_full.length">
                        <thead class="alert-success border-bottom">
                          <tr>
                              <th width="30">№</th>
                              <th>{{$t('FIO')}}</th>
                              <th>ID</th>
                              <th>{{$t('salary')}}</th>
                              <th class="text-center">{{$t('action')}}</th>
                          </tr>
                        </thead>
                        <tbody>
                        <tr v-for="(row, index) in user_list_connect_full" :key="index" class="border-bottom">
                          <td>{{index+1}}</td>
                            <td>
                            <div class="d-flex align-items-center">
                                <div class="ms-3">
                                <p class="fw-bold mb-1" v-if="row.ism != null">{{ row.ism }}</p>
                                <p class="text-muted mb-0" v-if="row.familiya != null">{{ row.familiya }}</p>
                                </div>
                            </div>
                            </td>
                            <td style="width:60px;">
                              {{ row.userid }}
                            </td>
                            <td>{{ row.skudOylik.name }}  ({{row.skudOylik.value}})</td>
                            <td class="text-center " 
                              @click="del_user_from_oylik(row)">
                                  <mdb-icon  icon="trash" class="text-danger"  style="cursor:pointer;"/>
                            </td>
                        </tr>
                        </tbody>
                      </table>
                </div>
            </div>
        </div>
        <div class="user_list border">
            <div class="all_choose border-bottom d-flex align-items-center px-1">
                <input type="checkbox" style="width: 16px; height: 16px; margin-top: 3px; margin-right: 3px;" 
                    :value="all_check" 
                    id="all_select"
                    @click="check_all($event)" 
                />
                <label for="all_select" class="m-0 p-0 mt-1" style="font-weight:500; font-size:13px">Все выбрать</label>
            </div>
            <div class="search_user mt-2">
                <div class="row mx-0">
                    <div class="col-12">
                        <mdb-input
                            type="text"
                            id="form1"
                            size="sm"
                            outline
                            class="form-icon-trailing mt-0 mb-2"
                            :label="$t('search_client')"
                            v-model="search"
                            @input="searchUser()"
                        >
                        </mdb-input>
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
                <label style="font-weight:500; font-size:13px; margin-top: 2px !important;"
                    :for="item.ism" class="m-0 " >{{item.ism}}
                </label>
            </div>
        </div>
    </div>
</div>
</template>

<script>
import loader from '@/components/loader.vue'
import {mapActions, mapGetters} from 'vuex'
import { mdbBtn, mdbIcon, mdbInput } from "mdbvue";


export default {
    components: {
      mdbBtn,
      loader,
      mdbIcon,
      mdbInput
    },
    data(){
        return{
            checkedUser: [

            ],
            user_id_list: [],
            all_check: false,
            activ_smena: -1,
            salary_name: '',
            salary_id: null,
            loading: false,
            Connected_userList: [],
            search: '',
            searchUserA: '',

            activ_item: {},
            user_list_all: [],
            salary_type: 0,
            user_list_connect_full: [],
            salary_item_id: 0,
        }
    },
    computed: mapGetters(['get_nosalary_user','get_salary_list']),
    async mounted(){
        await this.fetch_nosalary_user();
        this.user_list_all = this.get_nosalary_user;
        await this.fetch_Salary();
        console.log('this.get_salary_list.rows')
        // await this.fetch_connect_list();
    },
    methods:{
          ...mapActions(['fetch_nosalary_user', 'fetch_Salary']),
          async refresh_salary(){
            console.log(this.Connected_userList);
            this.loading = true;
            for( let i=0; i<this.Connected_userList.length; i++){
                await this.update_user(this.Connected_userList[i]);
                await this.fetchConnectUser(this.Connected_userList[i].userid);
            }
            this.loading = false;
            await this.fetch_get_oylik_user(this.salary_id);
          },
          async fetch_connect_list(){
            this.user_list_connect_full = [];
            for(let i=0; i<this.get_salary_list.rows.length; i++){
                if(this.salary_item_id != this.get_salary_list.rows[i].id){
                    await this.fetch_get_oylik_user_by_id(this.get_salary_list.rows[i].id); 
                }
            }
            console.log('this.user_list_connect_full')
            console.log(this.user_list_connect_full)
          },
            async fetch_get_oylik_user_by_id(id){
                try{
                    // this.loading =true;
                    const response = await fetch(this.$store.state.hostname + "/SkudOyliks/getPaginationUsersByOylikId?page=0&size=1000&oylik_id=" + id);
                    const data = await response.json();
                    // this.loading =false;
                    // console.log('data oylik user')
                    console.log(data)
                    // console.log(response)
                    if(response.status == 200 || response.status == 201){
                        console.log('success')
                        for(let i=0; i<data.items_list.length; i++){
                            this.user_list_connect_full.push(data.items_list[i])
                        }
                        // this.Connected_userList = data.items_list;
                    }
                }
                catch(error){
                    // this.loading =false;
                    console.log(error)
                }
            },
            async searchUser(){
                await this.fetch_nosalary_user();
                this.user_list_all = this.get_nosalary_user;
                if(this.search){
                    this.user_list_all = this.user_list_all.filter((item)=>{
                        return this.search.toLowerCase().split(' ').every(v => item.ism.toLowerCase().includes(v))
                    })
                }
            },
            async searchUserAccept(){
                await this.fetch_get_oylik_userAccept(this.salary_id);
                if(this.searchUserA){
                    this.Connected_userList = this.Connected_userList.filter((item)=>{
                        return this.searchUserA.toLowerCase().split(' ').every(v => item.ism.toLowerCase().includes(v))
                    })
                }

                await this.fetch_nosalary_user();
                await this.fetch_connect_list();
                this.user_list_all = this.get_nosalary_user;
                if(this.searchUserA){
                    this.user_list_all = this.user_list_all.filter((item)=>{
                        return this.searchUserA.toLowerCase().split(' ').every(v => item.ism.toLowerCase().includes(v))
                    })
                    this.user_list_connect_full = this.user_list_connect_full.filter((item)=>{
                        return this.searchUserA.toLowerCase().split(' ').every(v => item.ism.toLowerCase().includes(v))
                    })

                }



            },
          async choose_salary(item, i){
            this.activ_smena = i;
            this.activ_item = item;
            console.log(item);
            this.salary_name = item.name;
            this.salary_id = item.id;
            this.salary_item_id = item.id;
            this.salary_type = item.reserved_value;
            await this.fetch_get_oylik_user(item.id);
            await this.fetch_connect_list();

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
                        this.user_id_list.splice(i,1)
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
                for(let j=0; j<this.get_nosalary_user.length; j++){
                    this.user_id_list.push(this.get_nosalary_user[j])
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
            for( let i=0; i<this.user_id_list.length; i++){
                await this.update_user(this.user_id_list[i]);
                await this.fetchConnectUser(this.user_id_list[i].userid);
            }
            this.loading = false;
            this.user_id_list = [];
            this.checkedUser = [];
            this.all_check = false;
            await this.fetch_nosalary_user();
            this.user_list_all = this.get_nosalary_user;
            await this.fetch_get_oylik_user(this.salary_id);
          },
          async update_user(user_data){
            // console.log(user_data)
                try{
                    const requestOptions = {
                    method: "POST",
                    headers: { "Content-Type" : "application/json" },
                    body: JSON.stringify({
                        "userid": user_data.userid,
                        "badgenumber": user_data.userid,
                        "ism" : user_data.ism,
                        "cardno" : user_data.cardno,
                        "departid" : user_data.departid,
                        "familiya": user_data.familiya,
                        "group_id": user_data.group_id,
                        "group_name": user_data.group_name,
                        "image_url": user_data.image_url,
                        "middle_name" : user_data.middle_name,
                        "gr": user_data.gr,
                        "res_badgenumber": user_data.res_badgenumber,
                        "without_gr_id": this.salary_type,
                        "address" : user_data.address,
                        "passport" : user_data.passport,
                        "phone_number": user_data.phone_number,
                        "born_date": user_data.born_date,
                        })
                    };
                    const response = await fetch(this.$store.state.hostname + '/SkudMyUserinfoes/updateUserInfo', requestOptions);
                    if(response.status == 200 || response.status == 201){
                    }
                    }
                    catch(error){
                        console.log('error')
                        console.log(error)
                    }
             }, 

          async fetchConnectUser(id){
            try{
                const response = await fetch(this.$store.state.hostname + "/SkudOyliks/addOylikToUsers?oylik_id="+ this.salary_id + '&user_id_list=' + id);
                // const data = await response.json();
                // console.log('data')
                // console.log(data)
                // console.log(response)
                if(response.status == '200' || response.status == '201'){
                    console.log('success')
                }
            }
            catch(error){
                console.log(error)
            }
          },
          async fetch_get_oylik_user(id){
            try{
                // this.loading =true;
                const response = await fetch(this.$store.state.hostname + "/SkudOyliks/getPaginationUsersByOylikId?page=0&size=1000&oylik_id=" + id);
                const data = await response.json();
                // this.loading =false;
                // console.log('data oylik user')
                console.log(data)
                // console.log(response)
                if(response.status == 200 || response.status == 201){
                    console.log('success')
                    this.Connected_userList = data.items_list;
                    
                }
            }
            catch(error){
                // this.loading =false;
                console.log(error)
            }
          },

          async fetch_get_oylik_userAccept(id){
            try{
                // this.loading =true;
                const response = await fetch(this.$store.state.hostname + "/SkudOyliks/getPaginationUsersByOylikId?page=0&size=1000&oylik_id=" + id);
                const data = await response.json();
                // this.loading =false;
                console.log('data oylik user')
                console.log(data)
                console.log(response)
                if(response.status == 200 || response.status == 201){
                    console.log('success')
                    this.Connected_userList = data.items_list;
                }
            }
            catch(error){
                // this.loading =false;
                console.log(error)
            }
          },

          async del_user_from_oylik(userData){
            console.log(userData)
            try{
                const response = await fetch(this.$store.state.hostname + "/SkudOyliks/delOylikFromUsers?user_id_list="+ userData.userid);
                const data = await response.json();
                console.log('data')
                console.log(data)
                console.log(response)
                if(response.status == '200' || response.status == '201'){
                    console.log('success')
                    await this.fetch_get_oylik_user(this.salary_id)
                    await this.fetch_nosalary_user();
                    this.user_list_all = this.get_nosalary_user;
                }
            }
            catch(error){
                console.log(error)
            }
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
    position: relative;
    
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
.item_success_different{
    position: relative;
}
.item_success_different::before{
    content: '';
    position: absolute;
    top: 0;
    left: 0;
    width: 4px;
    height: 100%;
    background: rgb(59, 147, 47);
}
.item_warning_different{
    position: relative;
}
.item_warning_different::before{
    content: '';
    position: absolute;
    top: 0;
    left: 0;
    width: 4px;
    height: 100%;
    background: rgb(245, 181, 44);
}
</style>