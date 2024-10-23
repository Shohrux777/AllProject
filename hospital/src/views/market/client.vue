<template>
  <div>
    <loader v-if="loading"/>
    <div v-else class="m-0" >
      <div class="bg-white p-2  mb-0 " style="border-radius:5px; position:relative;">
        <div class="d-flex w-100">
          <div class="  border_green w-50">
            <p class="item_select_list text-center"><mdb-icon class="text-success" icon="user-check" /> Подключенный врач</p>
          </div>
          <div class=" border_red w-50">
            <p class="item_select_list text-center"><mdb-icon class="text-danger" icon="user-plus" /> Не подключенный врач</p>
          </div>
        </div>
        <div class="d-flex w-100">
          <div class="selected_doctor border-right w-50">
            <p class="item_select_list"> </p>
            <p v-for="(user,i) in selected_doctor_list" :key="i" @click="for_delete(user,i)" class="item_select_list border-bottom">
              <span class="mr-2 font-weight-bold">{{i+1}}.</span> 
              {{user.fio}}
            </p>
          </div>
          <div class="doctor_list w-50">
            <p v-for="(item,i) in auth_doctor_list" :key="i" v-show="item.userType==1 || item.userType==3" 
              class="item_select_list border-bottom" @click="getDoctor(item)">
              <span class="mr-2 font-weight-bold">{{i+1}}.</span> 
              {{item.users.fio}}({{item.userType}})
            </p>
          </div>
        </div>
      </div>
    </div>
      
    <Toast ref="message"></Toast>
     <massage_box :hide="modal_status" :detail_info="modal_info"
      :m_text="$t('Failed_to_delete')" @to_hide_modal = "modal_status= false"/>
  </div>

</template>

<script>
  // import ClientAdd from "./client_add"
  import { mdbIcon,} from 'mdbvue';
  import {mapActions, mapGetters, mapMutations} from 'vuex'
  export default {
    components: {
      mdbIcon,
      
      // ClientAdd
    },
    data(){
      return{
        show: false,
        snipper: true,
        editData: {},
        modal_info : '',
        modal_status: false,
        loading: false,

        selected_doctor_list: [],
        auth_doctor_list:[],
      }
    },
    async mounted(){
      await this.fetch_m_client();
      await this.fetch_auth_list();
    },
    computed: mapGetters(['get_m_client_list', 'auth_user_list', 'get_doctor_list_by_casher']),
    methods: {
      ...mapActions(['fetch_m_client', 'fetch_auth_list', 'fetch_get_doctor_list']),
      ...mapMutations(['m_client_by_row_delete']),

      for_edit(data){
        // this.$router.push('/m_client_add/'+ data.id)
        console.log(data)
      },
      async refresh(){
        console.log('refresh ishladi');
        await this.fetch_auth_list();
        this.synxronConnect();
      },
      async synxronConnect(){
        this.auth_doctor_list = [];
        console.log(this.get_doctor_list_by_casher.rows)
        console.log(this.auth_user_list)
        this.selected_doctor_list = this.get_doctor_list_by_casher.rows;
        for(let i=0; i<this.auth_user_list.length; i++){
          let s = 0;
          for(let j=0; j<this.get_doctor_list_by_casher.rows.length; j++){
            if(this.auth_user_list[i].id == this.get_doctor_list_by_casher.rows[j].DocAuthId){
              s++;
            }
          }
          if(s==0){
            this.auth_doctor_list.push(this.auth_user_list[i])
          }
        }
      },
      async getDoctor(option){
        try{
          const requestOptions = {
            method: "POST",
            headers: { "Content-Type" : "application/json" },
            body: JSON.stringify({
              "registraterAuthId": localStorage.UserIDFor,
              "doctorAuthId": option.id
            })  
          };
          this.loading = true;
          const response = await fetch(this.$store.state.hostname + "/HospitalRegistrationPermissionDoctors", requestOptions);          
          this.loading = false;
          if(response.status == 200 || response.status == 201){
            await this.fetch_get_doctor_list(localStorage.UserIDFor);
            // await this.fetch_auth_list();
            this.$refs.message.success('Added_successfully')
            this.synxronConnect();
          }
          else{
            this.$refs.message.warning('server_not_working')
          }
        }
        catch{
          this.$refs.message.warning('server_not_working')
        }
        
      },
      async for_delete(del_data,index){
          const requestOptions = {
            method : "delete",
          };
          const response = await fetch(this.$store.state.hostname + "/HospitalRegistrationPermissionDoctors/" + del_data.id, requestOptions);
          const data = await response.json();

          if(data)
          {
            // this.alert_text = localizeFilter('Successfully_removed');
            this.$refs.message.error('Successfully_removed')
            this.m_client_by_row_delete(index);
            this.synxronConnect();
          }
          else{
            this.modal_info = data.detail + "    (" + data.routine + ")";
            this.modal_status = true;
          }
      },
      add(){
        // this.show =! this.show
        // this.editData = {};
        this.show = true;
      }

    },
  };
</script>

<style lang="scss">


.add{
  position: fixed;
  background: rgba(0, 0, 0, 0.4);
  height: 100vh;
  top:0;
  width:85%;
  
}

.addxizmat{
  width: 470px;
  // height: 120px;
  background: #fff;
  position: relative;
  z-index: 5000;
}
.showing{
  display: none;
}
.wrap_chip{
  box-shadow: 2px 2px 15px rgb(225, 225, 225), -1px -1px 5px rgb(224, 224, 224);
  cursor: pointer;
  &:hover{
    // box-shadow: 2px 2px 10px rgb(193, 181, 251), -1px -1px 3px rgb(224, 224, 224);
    // border-bottom: 1px solid green;
    background-color: rgb(113, 222, 252);
  }
}
.item_select_list{
  padding: 6px 5px;
  margin: 0;
  font-size: 13.5px;
  cursor:pointer;
  color: rgb(74, 74, 74);
  &:hover{
    background-color: rgb(113, 222, 252);
  }
}
.selected_doctor{
  height: 70vh;
  overflow: hidden;
  overflow-y: scroll;
}
.doctor_list{
  height: 70vh;
  overflow: hidden;
  overflow-y: scroll;
  padding-left: 10px;
}
.border_green{
  border-bottom: 2px solid rgb(82, 255, 82);
}
.border_red{
  border-bottom: 2px solid rgb(254, 92, 92);
}
</style>