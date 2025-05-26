<template>
  <div class="deparment_page">
    <navbar :title = "$t('door')" @add="addDoor" @editFunc="editFunc" @deleteFunc="deleteFunc"/>
    <div class="mainpage">
        <mdbtabled
        :options = get_device_list
        @selectData = "selectData"

        />
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
            <MDBModalTitle id="exampleModalLabel"> {{$t('door_add')}} </MDBModalTitle>
        </MDBModalHeader>
        <MDBModalBody>
            <device_add :select_data="select_data" @close="exampleModal = false"></device_add>
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
    MDBModalBody,
  } from 'mdb-vue-ui-kit';
  import { ref } from 'vue';
  import navbar from '@/components/navbar.vue'
  import mdbtabled from '@/components/mdbtable.vue'
  import device_add from './device_add.vue'
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
        MDBModalHeader,
        MDBModalTitle,
        MDBModalBody,
        navbar,
        device_add,
        mdbtabled
    },
    data(){
        return{
            show_dept: false,
            select_data: {},
        }
    },
    async mounted(){
        await this.fetch_Device();
        console.log(this.get_device_list)
        console.log('this.get_device_list')
    },
    computed: mapGetters(['get_device_list']),
    methods:{
        ...mapActions(['fetch_Device']),
        selectData(data){
          console.log('select_data')
          this.select_data = data;
          console.log(data)
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
                const response = await fetch(this.$store.state.hostname + "/SkudDevices/" + this.select_data.id, requestOptions);
                const data = await response.json();
                console.log('data')
                console.log(data)
                console.log(response)
                if(response.status == 200 || response.status == 201){
                    await this.fetch_Device();
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
          },
        
        addDoor(){
            console.log('dept')
            this.exampleModal = true;
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

</style>