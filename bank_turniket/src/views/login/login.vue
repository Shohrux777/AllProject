<template>
  <div class="loginPage">
    <form class="loginForm" @submit.prevent="Check_login">
        <div class="inputBox">
            <input class="w-100 inputUser" v-model="login" type="text" placeholder="User Name">
        </div>
        <div class="inputBox mt-4">
            <input class="w-100 inputUser" v-model="parol" type="password" placeholder="Password">
        </div>
        <div class="error text-center" v-if="invalid_status">
            <small class="text-danger" style="font-size: 12px;">{{$t('login_or_parol_invalid')}}</small>
        </div>
        <div class="buttonBox  d-flex justify-content-center">
            <MDBBtn type="submit" class="btn_style px-5">{{$t('submit')}}</MDBBtn>
        </div>
    </form>
  </div>
</template>

<script>
import { MDBBtn } from "mdb-vue-ui-kit";
export default {
    components: {
        MDBBtn
    },
    data(){
        return{
            login: '',
            parol: '',
            invalid_status: false,
        }
    },
    methods:{
        Check_login(){
            // localStorage.sidebar = true;
            // localStorage.Name = 'Firdavs'
            if(this.login == 'admin' && this.parol == 'admin'){
                this.$router.push('/dashboard')
                this.invalid_status = false;
            }
            else{
                this.invalid_status = true;
            }
        }
    }
};
</script>

<style>
.loginPage{
    height: calc(100vh - 52px);
    display: flex;
    justify-content: center;
    align-items: center;
}
.loginForm{
    width: 520px;
    height: 290px;
    background: rgba(2, 2, 2, 0.3);
    padding: 70px 50px;
}
.loginForm .inputBox{
    border-bottom: 1px solid #b1b0b0;
    padding: 5px;
}
.loginForm .inputUser{
    border:none;
    outline: none;
    background: rgba(255, 255, 255, 0);
    color:white;
    font-size: 14px;

}
.loginForm .inputUser::placeholder{
    color:white;
}
.btn_style{
    background-image: linear-gradient(to right, #ed6ea0 0%, #ec8c69 100%);
    color:white !important;
    font-size: 12px !important;

}
.btn_style:hover{
    background-image: linear-gradient(to top, #0ba360 0%, #3cba92 100%);
}
.buttonBox{
    margin-top:35px;
}
</style>