<template>
  <div class="returnMoney">
   <loader v-if="loading"/>
    <form @submit.prevent="submit">
      <div class="row">
        <div class="col-5">
          <mdb-input type="text" class="m-0 p-0" v-model="search"  size="sm" label="Сотрудники лист" ></mdb-input>

          <div class=" last_unpay_patient">
            <div class="choosePatientLast px-1" v-for="option in filteredList" :key="option.id" 
                :class="{'activ_last_patient': activ_id == option.id}"
                @click="selectOption(option)"
            >
              <div class="d-flex align-items-center">
                <div class="picture_back_last">
                  <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-user" width="27" height="27" viewBox="0 0 24 24" stroke-width="1.5" stroke="#fff" fill="none" stroke-linecap="round" stroke-linejoin="round">
                    <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                    <circle cx="12" cy="7" r="4" />
                    <path d="M6 21v-2a4 4 0 0 1 4 -4h4a4 4 0 0 1 4 4v2" />
                  </svg>
                </div>
                <div class="ml-2">
                  <h6 style="font-size: 13px;" class="m-0 p-0">{{option.fio}}</h6>
                  <span style="font-size:11px; padding-top:-15px;" class="text-primary">{{option.phoneNumber}}</span>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="col-7">
          <!-- <lineSelect
            :options="get_users_list.rows"
            :searchshow="true"
            @select="selectUser"
            :selected="user_name"
            :label="$t('user')"
            /> -->
          <mdb-input type="text" disabled v-model="user_name" size="sm" class="mb-3" :label="$t('user')" outline></mdb-input>
          <small v-if="$v.user_name.$dirty && user_id == null" class="invalid-text mt-0" >
            {{$t('select_item')}}
          </small>

          <div>
            <mdb-input type="number" v-model="price" size="sm" class="mt-0 mb-3" :label="$t('price')" outline></mdb-input>
            <small class="invalid-text"  v-if="$v.price.$dirty && !$v.price.required" >
              {{$t('name_invalid_text')}}
            </small>
          </div>
          <div>
            <mdb-input type="text" style="margin-top:0px;" class="mt-0 mb-0" v-model="reason" size="sm" :label="$t('reason')" outline></mdb-input>
            <small class="invalid-text"  v-if="$v.reason.$dirty && !$v.reason.required" >
              {{$t('name_invalid_text')}}
            </small>
          </div>
          <div class="text-right ">
            <mdb-btn type="submit" color="success py-2 px-4" style="font-size:10px;">
              {{$t('add')}}
            </mdb-btn>
          </div>

          <div class="TablePatientRasxod mt-2">
            <table class="myTable">
              <thead class="rasxod_table_header">
                <tr class="header">
                  <!-- <th width="50">№</th> -->
                  <!-- <th width="270">{{$t('authorizationId')}}</th> -->
                  <th width="270">{{$t('usersId')}}</th>
                  <!-- <th >{{$t('registratedDate')}}</th> -->
                  <th>{{$t('price')}}</th>
                  <th >{{$t('reason')}}</th>
                </tr>
              </thead>
              <tbody class="rasxod_body">
                <tr v-for="(row,rowIndex) in bonus_contragent_list" :key="rowIndex">
                  <!-- <td> <span >{{rowIndex+1}}</span> </td> -->
                  <!-- <td> <span >{{row.authorization.users.fio}}</span> </td> -->
                  <td> <span >{{row.user.fio}}</span> </td>
                  <!-- <td> <span >{{row.registratedDate.slice(0,10) + ' |' + row.registratedDate.slice(11,16)}}</span> </td> -->
                  <td> <span class="bg-success rasxod_price  ">+ {{row.price}}</span> </td>   
                  <td> <span >{{row.reason}}</span> </td>
                </tr>
              </tbody>
            </table>
          </div>
        </div>
      </div>
    </form>
    <Toast ref="message"></Toast>

  </div>
</template>

<script>
  // import lineSelect from "../../components/hospital/UserlineSelect.vue";
  import { mdbBtn, mdbInput  } from 'mdbvue';
  import {mapActions, mapGetters} from 'vuex'
  import { required } from 'vuelidate/lib/validators'
export default {
components:{
   mdbInput,mdbBtn
  },
  data(){
    return{
      price: null,
      reason: '',
      user_name: '',
      user_id: null,
      loading: false,
      search: '',
      activ_id: -1,
      users_id: 0,
      bonus_contragent_list: [],
      Start_time: null,
      End_time: null,
    }
  },
  computed:{
    ...mapGetters(['get_users_list']),
    filteredList: function(){
        if(this.search)
        {
          return this.get_users_list.rows.filter((item)=>{
            return this.search.toLowerCase().split(' ').every(v => item.fio.toLowerCase().includes(v))
          })
        }else
        {
          return this.get_users_list.rows;
        }
      }
  },
  validations: {
    price: {required},
    reason: {required},
    user_name : {required}
  },
  async mounted(){
    await this.fetch_users();
    let time1 = new Date();
    this.Start_time = time1.toISOString().slice(0,10); 
    this.End_time = time1.toISOString().slice(0,10);
    await this.fetch_get_today_rasxod();
  },
  methods:{
    ...mapActions(['fetch_users']),
      selectOption(option){
        console.log(option)
        this.user_name = option.fio
        this.user_id = option.id;
        this.activ_id = option.id;
      },
      cls_wnd(){
        this.price =  null;
        this.reason =  '';
        this.user_name = '';
        this.user_id = null;
      },
      async fetch_get_today_rasxod(){
        let time = new Date();
        this.Start_time = time.toISOString().slice(0,10); 
        this.End_time = time.toISOString().slice(0,10);
        console.log(this.Start_time)
        console.log(this.End_time)
        var time1 = this.Start_time + 'T00:00:35.000Z'
        var time2 = this.End_time + 'T23:59:35.000Z'
        const response = await fetch(this.$store.state.hostname + '/ReturnMoneys/getReturnMoneyListBeatwenDateTimeAndKassirId?beginDate=' + time1 + 
        '&endDate=' + time2 + '&kassirId=' + this.users_id)
        const data = await response.json()
        this.bonus_contragent_list = data
        console.log(data)
      },
      async submit(){
        console.log('dasdas')
        if(this.$v.$invalid )
            {
              this.$v.$touch();
              this.$refs.message.warning('please_fill')
              return false;
            }
            // var time1 = new Date()
            const requestOptions = {
              method : "POST",
              headers: { "Content-Type" : "application/json" },
              body: JSON.stringify({
                "reason" : this.reason,
                "price" : this.price,
                "usersId" : this.user_id,
                // "createdDateTime": time1.toISOString(),
                "authorizationId": localStorage.AuthId,
                // "noteStr": "",
              // "code" : 0
              })
            };
            this.loading = true;
            const response = await fetch(this.$store.state.hostname + '/ReturnMoneys', requestOptions)
            const data = await response.json()
            this.loading = false;
            console.log(data)
            if(data.id){
              this.cls_wnd();
              await this.fetch_get_today_rasxod();
              this.$refs.message.success('Added_successfully');
            }
            else{
              this.$refs.msg.error('Error_successfully')
              return false;
            } 

      }
  }
}
</script>

<style>
.last_unpay_patient{
  height: 380px;
  overflow-y: scroll;
}
.choosePatientLast{
    padding: 5px 0px 5px 0px;
    margin: 0;
    cursor:pointer;
  }
.picture_back_last{
    width: 35px;
    height: 35px;
    display: flex;
    justify-content: center;
    align-items: center;
    border-radius: 50%;
    background-image: linear-gradient( 132.6deg,  rgba(71,139,214,1) 23.3%, rgba(37,216,211,1) 84.7% );
  }

.choosePatientLast:hover{
    background: #e7e7e7;
  }
  .activ_last_patient{
  position: relative;
  background-image: radial-gradient( circle farthest-corner at 1.3% 2.8%,  rgba(239,249,249,1) 0%, rgb(214, 228, 251) 100.2% );
}
.activ_last_patient::before{
  content: '';
  position: absolute;
  width: 2.5px;
  height: 100%;
  left: 0;
  top:0;
  background-image: linear-gradient(to right, rgb(182, 244, 146), rgb(51, 139, 147));
}
.TablePatientRasxod{
  height: 220px;
  overflow-y: scroll;
}
.TablePatientRasxod .myTable th, td{
  padding: 7px 4px !important;
}
.TablePatientRasxod .myTable .rasxod_table_header{
  background-image: radial-gradient( circle farthest-corner at 1.3% 2.8%,  rgb(207, 219, 238) 100.2%, rgb(198, 214, 241) 100.2% );
}
.rasxod_price{
  border-radius: 20px;
  font-size: 10.5px;
  font-weight: bold;
  color:white;
  padding: 2px 7px;
}
</style>