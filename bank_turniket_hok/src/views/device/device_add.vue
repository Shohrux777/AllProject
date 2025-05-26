<template>
  <form @submit.prevent="submit_add" class="container">
    <MDBInput
      type="text"
      id="form1"
      class="form-icon-trailing"
      :label="$t('name')"
      v-model="device_name"
    >
      <!-- <MDBIcon icon="exclamation-circle" class="trailing"></MDBIcon> -->
    </MDBInput>
    <div class="row">
      <div class="col-6">
        <MDBInput
          type="text"
          id="form1"
          class="form-icon-trailing mt-3"
          :label="$t('ip_address')"
          placeholder="192.168.1.100"
          v-model="ip_address"
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
          :label="$t('mac_address')"
          v-model="ip_address"
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
        device_name: '',
        ip_address: '',
        mac_address: '',
        id: 0,
      }
    },
    computed: mapGetters(['get_device_list']),
    async mounted(){
      this.id = this.select_data.id;
      this.device_name = this.select_data.device_name;
      this.ip_address = this.select_data.ip_address;
      this.mac_address = this.select_data.mac_address;
    },
    props:{
      select_data:{
        type: Object,
        default: {}
      },
    },
    methods:{
      ...mapActions(['fetch_Device']),

      async submit_add(){
        try{
          const requestOptions = {
          method: "POST",
          headers: { "Content-Type" : "application/json" },
          body: JSON.stringify({
            "id": this.id,
            "device_name" : this.device_name,
            "mac_address" : this.ip_address,
            "ip_address" : this.ip_address,
            })
          };
          const response = await fetch(this.$store.state.hostname + "/SkudDevices", requestOptions);
          const data = await response.json();
          console.log(data)
          if(data.id != 0){
            this.$emit('close')
            await this.fetch_Device();
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