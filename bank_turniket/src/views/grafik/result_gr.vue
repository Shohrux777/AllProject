<template>
  <div class="deparment_page">
    <navbar :title = "$t('smena')" />
    <!-- @add="addDept" -->
    <div class="mainpage">
        <mdbtabled
        :options = get_result_gr
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
            <MDBModalTitle id="exampleModalLabel"> {{$t('smena')}} </MDBModalTitle>
        </MDBModalHeader>
        <MDBModalBody>
            <smena_add @close="exampleModal = false"></smena_add>
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
  import smena_add from './smena_add.vue'
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
        smena_add,
        mdbtabled
    },
    data(){
        return{
            show_dept: false,
        }
    },
    async mounted(){
        await this.fetch_result_gr();
        console.log(this.get_result_gr)
        console.log('this.get_result_gr')
    },
    computed: mapGetters(['get_result_gr']),
    methods:{
        ...mapActions(['fetch_result_gr']),
        addDept(){
            console.log('dept')
            this.show_dept = true;
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