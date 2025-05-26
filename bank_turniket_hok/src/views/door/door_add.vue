<template>
  <form @submit.prevent="submit_add" class="container">
    <MDBInput
      type="text"
      id="form1"
      class="form-icon-trailing"
      :label="$t('name')"
      v-model="acc_name"
    >
      <!-- <MDBIcon icon="exclamation-circle" class="trailing"></MDBIcon> -->
    </MDBInput>

    <MDBInput
      type="text"
      id="form1"
      class="form-icon-trailing mt-3"
      :label="$t('input')"
      v-model="input"
    >
      <!-- <MDBIcon icon="exclamation-circle" class="trailing"></MDBIcon> -->
    </MDBInput>
    <div class="row">
      <div class="col-6">
        <MDBInput
          type="text"
          id="form1"
          class="form-icon-trailing mt-3"
          :label="$t('dbname')"
          placeholder="192.168.1.100"
          v-model="dbname"
        >
          <!-- <MDBIcon icon="exclamation-circle" class="trailing"></MDBIcon> -->
        </MDBInput>
      </div>
      <div class="col-6">
        <MDBInput
          type="text"
          id="form1"
          disabled
          class="form-icon-trailing mt-3"
          placeholder="192.168.1.100"
          :label="$t('device')"
          v-model="dbname"
        >
          <!-- <MDBIcon icon="exclamation-circle" class="trailing"></MDBIcon> -->
        </MDBInput>
      </div>
    </div>
    
    <div class="row">
      <div class="col-6">
        <MDBInput
          type="text"
          id="form1"
          class="form-icon-trailing mt-3"
          :label="$t('login')"
          v-model="login"
        >
          <!-- <MDBIcon icon="exclamation-circle" class="trailing"></MDBIcon> -->
        </MDBInput>
      </div>
      <div class="col-6">
        <MDBInput
          type="text"
          id="form1"
          class="form-icon-trailing mt-3"
          :label="$t('password')"
          v-model="password"
        >
          <!-- <MDBIcon icon="exclamation-circle" class="trailing"></MDBIcon> -->
        </MDBInput>
      </div>
    </div>

    <div>
      <MDBModalFooter>
        <MDBBtn style="font-size: 11px;" @click="$emit('close')" color="secondary" >Close</MDBBtn>
        <MDBBtn style="font-size: 11px;" type="submit" color="success">Save</MDBBtn>
      </MDBModalFooter>
    </div>
  </form>
</template>

<script>
import { MDBInput, MDBIcon, MDBModalFooter, MDBBtn } from "mdb-vue-ui-kit";
import {mapActions, mapGetters} from 'vuex'

export default {
  components: {
      MDBInput,
      MDBIcon,
      MDBModalFooter,
      MDBBtn
    },
    data(){
      return{
        acc_name: '',
        input: '',
        dbname: '',
        device: '',
        login: '',
        password:'',
        id: 0,
      }
    },
    computed: mapGetters(['get_door_list']),
    async mounted(){
      this.id = this.select_data.id;
      this.acc_name = this.select_data.acc_name;
      this.dbname = this.select_data.dbname;
      this.device = this.select_data.device;
      this.login = this.select_data.login;
      this.password = this.select_data.password;
      this.input = this.select_data.inout;

    },
    props:{
      select_data:{
        type: Object,
        default: {}
      },
    },
    methods:{
      ...mapActions(['fetch_Door']),

      async submit_add(){
        try{
          const requestOptions = {
          method: "POST",
          headers: { "Content-Type" : "application/json" },
          body: JSON.stringify({
            "id": this.id,
            "acc_name" : this.acc_name,
            "inout" : this.input,
            "dbname" : this.dbname,
            "device" : this.dbname,
            "login" : this.login,
            "password" : this.password,
            })
          };
          const response = await fetch(this.$store.state.hostname + "/SkudDoors", requestOptions);
          const data = await response.json();
          console.log(data)
          if(data.id != 0){
            this.$emit('close')
            await this.fetch_Door();
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