<template>
  <div class="line-select">

     <input type="text" class="form-control form-control-sm" style="font-size: 13px;" :placeholder="label"
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

      <p v-for="option in filteredList" :key="option.id" style="font-size: 13px;"
          @click="selectOption(option)"
      >
        {{option.name}}
      </p>

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
                    return this.search.toLowerCase().split(' ').every(v => item.name.toLowerCase().includes(v))
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

  .options p{
    padding: 5px 5px 5px 10px;
    margin: 0;
  }

  .options p:hover{
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
