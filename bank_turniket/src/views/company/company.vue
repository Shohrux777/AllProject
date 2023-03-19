<template>
  <div class="deparment_page">
    <navbar :title = "$t('company')" @add="addDept"/>
    <div class="mainpage">
        <mdbtabled
        :options = get_company_list
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
            <MDBModalTitle id="exampleModalLabel"> {{$t('otdel')}} </MDBModalTitle>
        </MDBModalHeader>
        <MDBModalBody>
            <dept_add @close="exampleModal = false"></dept_add>
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
  import dept_add from './company_add.vue'
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
        await this.fetch_Company();
        console.log(this.get_company_list)
        console.log('this.get_company_list')
    },
    computed: mapGetters(['get_company_list']),
    methods:{
        ...mapActions(['fetch_Company']),
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