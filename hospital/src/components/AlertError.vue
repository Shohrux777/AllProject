<template>
  <div v-show="notes" class="ErrorAlerList">
    <transition name="v-transition-error">
      <div class="noteficationError" v-for="(nam,i) in notes" :key="nam.id">
          <div class="alertColor card"  >
            <div :class="[`bg-${nam.color}`]" class="card-header py-2 px-3">
              <div class="d-flex justify-content-between">
                <div><span class="text-white">SMR of ExtremeSoft</span></div>
                <div class="d-flex align-items-center">
                  <span class="text-white mr-2" style="font-size: 12px;">5 mins ago</span>
                   <svg @click="close(i)" xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-x" width="24" height="24" viewBox="0 0 24 24" stroke-width="1.5" stroke="#fff" fill="none" stroke-linecap="round" stroke-linejoin="round">
                    <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                    <line x1="18" y1="6" x2="6" y2="18" />
                    <line x1="6" y1="6" x2="18" y2="18" />
                  </svg>
                </div>
              </div>
            </div>
            <div class="sort card-body px-3" style="padding: 12px 10px; height: 150px;">
              <!-- <i :class="`fa fa-${nam.icon}`" style="font-size: 15px;" class="text-white"></i> -->
              <span style="font-family: 'Montserrat',sans-serif; font-size: 13.5px;" class="p-0 m-0 text-dark">{{$t(nam.name)}}</span>
            </div>
          </div>
      </div>
    </transition>
  </div>
</template>

<script>
export default {
  data () {
    return {
      notes: [],
      id: 0
    }
  },
  methods: {
    close(id){
      this.notes.splice(id, 1)
    },
    success (e) {
      this.id ++;
      const a = {
        name: e,
        color: 'success',
        icon: 'check',
        id:this.id
      }
      this.notes.unshift(a)
      setTimeout(() => {
        this.notes.splice(this.notes.length - 1, 1)
      }, 4800)
    },
    error (e) {
      this.id ++;
      const a = {
        name: e,
        color: 'danger',
        icon: 'exclamation-circle',
        id:this.id
      }
      this.notes.unshift(a)
      setTimeout(() => {
        this.notes.splice(this.notes.length - 1, 1)
      }, 4800)
    },
    info (e) {
      this.id ++;
      const a = {
        name: e,
        color: 'info',
        icon: 'exclamation-circle',
        id:this.id
      }
      this.notes.unshift(a)
      setTimeout(() => {
        this.notes.splice(this.notes.length - 1, 1)
      }, 4800)
    },
    warning (e) {
      this.id ++;
      const a = {
        name: e,
        color: 'warning',
        icon: 'exclamation-circle',
        id:this.id
      }
      this.notes.unshift(a)
      setTimeout(() => {
        this.notes.splice(this.notes.length - 1, 1)
      }, 4800)
    },
    dark (e) {
      this.id ++;
      const a = {
        name: e,
        color: 'dark',
        icon: 'exclamation-circle',
        id:this.id
      }
      this.notes.unshift(a)
      setTimeout(() => {
        this.notes.splice(this.notes.length - 1, 1)
      }, 4800)
    }

  }
}
</script>

<style lang="scss">
.ErrorAlerList{
  max-width: 500px;
  z-index: 1000000;
  min-width: 450px;
  position: fixed;
  right: 5%;
  top: 20px;
}
.alertColor{
  margin: 5px 0;
  border-radius: 3px;
}
.sort{
  display: flex;
  align-items: center;
  padding: 5px 10px;
}
.noteficationError{
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

.v-transition-error-enter {
  opacity: 0;
  transform: translateY(-500px);
}
.v-transition-error-enter-to {
  opacity: 1;
  // transform: translate(0px);
}
.v-transition-error-enter-active {
  transition: all .3s ease;
}

.v-transition-error-leave {
  transform: translate(0px);
  opacity: 1;
}
.v-transition-error-leave-to {
  transform: translateY(-500px);
  opacity: 0;
}
.v-transition-error-leave-active {
  transition: all .3s ease;
}
.toast-move {
  transition: all .5s ease;
}
</style>
