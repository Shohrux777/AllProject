<template>
  <div class="deparment_page">
    <navbar :title = "$t('door')" @add="addDoor" @editFunc="editFunc" @deleteFunc="deleteFunc"/>
    <div class="mainpage">
        <mdbtabled
        :options = get_door_list
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
            <dept_add  :select_data="select_data" @close="exampleModal = false"></dept_add>
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
  import dept_add from './door_add.vue'
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
        dept_add,
        mdbtabled
    },
    data(){
        return{
            show_dept: false,
        }
    },
    async mounted(){
        await this.fetch_Door();
        console.log(this.get_door_list)
        console.log('this.get_door_list')
    },
    computed: mapGetters(['get_door_list']),
    methods:{
        ...mapActions(['fetch_Door']),
        addDoor(){
            console.log('dept')
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
                const response = await fetch(this.$store.state.hostname + "/SkudDoors/" + this.select_data.id, requestOptions);
                const data = await response.json();
                console.log('data')
                console.log(data)
                console.log(response)
                if(response.status == 200 || response.status == 201){
                    await this.fetch_Door();
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