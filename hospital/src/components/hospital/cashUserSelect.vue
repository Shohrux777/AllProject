<template>
  <div class="line-select">

     <input type="text" class="form-control form-control-sm text-dark" :placeholder="label"
     @click="select_input"
    :value="selected"/>

    <mdb-icon class="top-btn not-pointer" icon="caret-down" />
    <div class="card selectOutput options"  v-bind:class="{ 'fixed-column': !areOptionsVisible }" v-if="areOptionsVisible">
      <div class="container" >

        <mdb-input v-if="searchshow == true" size="sm" :placeholder="$t('search_here')" m="b0 t3" 
        group type="text"
        v-model="search" :ref="search"
        />
      </div>

      <div class="choosePatient" v-for="option in filteredList" :key="option.id"
          @click="selectOption(option)"
      >
        <div class="d-flex align-items-center">
          <div class="picture_back">
            <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-user" width="30" height="30" viewBox="0 0 24 24" stroke-width="1" stroke="#fff" fill="none" stroke-linecap="round" stroke-linejoin="round">
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
</template>

<script>
import { mdbInput,mdbIcon } from 'mdbvue';

export default {
  name: 'line-select',
  components:{mdbInput,mdbIcon },
  props: {
      options:{
        type: Array,

        default(){
          return []
        }
      },
      searchshow : {
        type : Boolean,
        default : false
      },
      selected:{
        type: String,
        default: ''
      },
      row_index:{
        type:Number,
        default:-1
      },
      label:{
        type:String,
        default:''
      }
    },
  data(){
    return{
      search:'',
      areOptionsVisible: false
    }
  },
  methods: {
    selectOption(option)
    {
      let x = {
        row : this.row_index,
        data : option
      }
      this.$emit('select',x);
      this.areOptionsVisible = false;
    },
    select_input(){
      this.num = 0
      this.areOptionsVisible = !this.areOptionsVisible;
      if(this.searchshow){
        this.$nextTick(function () {
        this.$refs[this.search].focus();
        this.search = '';
        })
      }
      document.addEventListener("click", this.add_fun);
    },
    add_fun (e){
        if(this.num!=0){
          if(e.target.closest(".selectOutput")) return
          this.areOptionsVisible = false;
          document.removeEventListener("click", this.add_fun)
        }
        this.num++
    }
  },
   computed:{
            filteredList: function(){
                if(this.search)
                {
                  return this.options.filter((item)=>{
                    return this.search.toLowerCase().split(' ').every(v => item.fio.toLowerCase().includes(v))
                  })
                }else
                {
                  return this.options;
                }
              }

    }
}
</script>


<style >

/* for scroll */
  /* width */
::-webkit-scrollbar {
  height: 4.5px;              /* height of horizontal scrollbar ← You're missing this */
  width: 4.5px;   
}

/* Track */
::-webkit-scrollbar-track {
  background: #f1f1f1;
}

/* Handle */
::-webkit-scrollbar-thumb {
  background: #888;
  border-radius: 2px;
}

/* Handle on hover */
::-webkit-scrollbar-thumb:hover {
  background: rgb(71, 71, 71);
}
/* ! for scroll */


.line-select{
  position: relative;
  width: 100%;
  cursor: pointer;
}


  .options{
    position: absolute  !important;
    z-index: 40000 !important;
    width:100%;
    max-height: 300px;
    overflow-y: scroll;
    overflow-x: scroll;
    margin-top: -32px;
    padding-bottom: 5px;
  }

  .options .choosePatient{
    padding: 5px 5px 5px 10px;
    margin: 0;
  }
  .options .picture_back{
    width: 35px;
    height: 35px;
    display: flex;
    justify-content: center;
    align-items: center;
    border-radius: 50%;
    background-image: radial-gradient( circle farthest-corner at 12.3% 19.3%,  rgba(85,88,218,1) 0%, rgba(95,209,249,1) 100.2% );
  }

  .options .choosePatient:hover{
    background: #e7e7e7;
  }

  .top-btn{
    position: absolute;
    top: 8px;
    right: 8px;
  }

  .not-pointer
  {
      pointer-events: none;
  }


</style>
