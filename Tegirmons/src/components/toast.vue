<template>
    <div v-show="notes" class="alertNotesList">
      <transition-group name="v-transition-animate">
        <div class="noteficationAlert" v-for="(nam,i) in notes" :key="nam.id">
            <div class="alertColorNotes card" :class="[`bg-${nam.color}`]" >
              <div class="card-header py-2 px-3">
                <div class="d-flex justify-content-between">
                  <div><span class="text-white">OQQO'RG'ON TEGIRMON</span></div>
                  <div class="d-flex align-items-center">
                    <span class="text-white mr-2" style="font-size: 12px;">3 mins ago</span>
                     <svg @click="close(i)" xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-x" width="24" height="24" viewBox="0 0 24 24" stroke-width="1.5" stroke="#fff" fill="none" stroke-linecap="round" stroke-linejoin="round">
                      <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                      <line x1="18" y1="6" x2="6" y2="18" />
                      <line x1="6" y1="6" x2="18" y2="18" />
                    </svg>
                  </div>
                </div>
              </div>
              <div class="sort card-body px-3" style="padding: 12px 10px;">
                <!-- <i :class="`fa fa-${nam.icon}`" style="font-size: 15px;" class="text-white"></i> -->
                <span style="font-family: 'Montserrat', sans-serif; font-size: 13.5px;" class="p-0 m-0 text-white">{{nam.name}}</span>
              </div>
            </div>
        </div>
      </transition-group>
    </div>
  </template>
  
  <script>
  export default {
    data () {
      return {
        notes: [],
        id:0,
      }
    },
    methods: {
      close(id){
        this.notes.splice(id, 1)
      },
      success (e) {
        this.id++;
        const a = {
          name: e,
          color: 'success',
          icon: 'check',
          id: this.id,
        }
        this.notes.unshift(a)
        setTimeout(() => {
          this.notes.splice(this.notes.length - 1, 1)
        }, 4000)
      },
      error (e) { 
        this.id++;
        const a = {
          name: e,
          color: 'danger',
          icon: 'exclamation-circle',
          id: this.id,
        }
        this.notes.unshift(a)
        setTimeout(() => {
          this.notes.splice(this.notes.length - 1, 1)
        }, 4000)
      },
      info (e) {
        this.id++;
        const a = {
          name: e,
          color: 'info',
          icon: 'exclamation-circle',
          id: this.id,
        }
        this.notes.unshift(a)
        setTimeout(() => {
          this.notes.splice(this.notes.length - 1, 1)
        }, 4000)
      },
      warning (e) {
        this.id++;
        const a = {
          name: e,
          color: 'warning',
          icon: 'exclamation-circle',
          id: this.id,
        }
        this.notes.unshift(a)
        setTimeout(() => {
          this.notes.splice(this.notes.length - 1, 1)
        }, 4000)
      },
      dark (e) {
        this.id++;
        const a = {
          name: e,
          color: 'dark',
          icon: 'exclamation-circle',
          id: this.id,
        }
        this.notes.unshift(a)
        setTimeout(() => {
          this.notes.splice(this.notes.length - 1, 1)
        }, 4000)
      }
    }
  }
  </script>
  
  <style lang="scss" scoped>
  .alertNotesList{
    max-width: 60%;
    z-index: 1000000;
    min-width: 350px;
    position: fixed;
    right: 50%;
    transform: translate(50%, 0);
    top: 10px;
  }
  .alertColorNotes{
    margin: 5px 0;
    border-radius: 3px;
  }
  .sort{
    display: flex;
    align-items: center;
    padding: 5px 10px;
  }
  .noteficationAlert{
    display: flex;
    flex-direction: column-reverse;
  }
  .sort p{
    font-size: 16px;
    padding: 0 15px;
  }
  .sort i{
    font-size: 20px;
    margin-top: 4px;
    margin-right: 10px;
  }
  
  .v-transition-animate-enter {
    opacity: 0;
    transform: translate(0,0);
  }
  .v-transition-animate-enter-to {
    opacity: 1;
    // transform: translate(0px);
  }
  .v-transition-animate-enter-active {
    transition: all .3s ease;
  }
  
  .v-transition-animate-leave {
    transform: translate(0px);
    opacity: 1;
  }
  .v-transition-animate-leave-to {
    transform: translate(0,0);
    opacity: 0;
  }
  .v-transition-animate-leave-active {
    transition: all .3s ease;
  }
  .toast-move {
    transition: all .5s ease;
  }
  </style>
  