<template>
  <div class="home" id="table">
    <div class="container-fluid">
      <div class="img " style="text-align:center;" v-show="false">
        <img id="image" width="100%" height="25%" alt="">
      </div>
      <nav class="navbar navbar-expand-lg bg-body-tertiary">
        <div class="container-fluid">
          <a class="navbar-brand" href="#">ExtremeSoft</a>
          <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
          </button>
          <div class="collapse navbar-collapse" id="navbarSupportedContent">
            <ul class="navbar-nav me-auto mb-2 mb-lg-0">
              <!-- <li class="nav-item">
                <a class="nav-link active" aria-current="page" href="#">Home</a>
              </li> -->
            </ul>
            <form class="d-flex" role="search">
              <button class="btn btn-outline-danger" type="submit">Chiqish</button>
            </form>
          </div>
        </div>
      </nav>
      <div class="d-flex mt-3">
        <div class="form-floating mb-3">
          <input type="text" class="form-control" v-model="passport" id="floatingInput" placeholder="name@example.com">
          <label for="floatingInput">Tex passport</label>
        </div>
        <div class="form-floating">
          <input type="text" class="form-control" v-model="nomer" id="floatingPassword" placeholder="Password">
          <label for="floatingPassword">Moshina nomer</label>
        </div>
        <div class="form-floating">
          <input type="number" class="form-control" v-model="id" id="floatingPassword1" placeholder="ID">
          <label for="floatingPassword1">ID</label>
        </div>
        <div class="d-flex align-items-center justify-content-center">
          <button type="button" @click="sendPdfTeleghttp()" class="btn btn-primary ml-4">Send http</button>
          <button type="button" @click="sendPdfTeleghttps()" class="btn btn-success mr-4 ml-4">Send https</button>
        </div>
      </div>
      <div>
        <table class="table  ">
          <thead class="table-info">
            <tr>
              <th scope="col" style="width: 20px;">#</th>
              <th scope="col">Tex passport</th>
              <th scope="col">Mashina Nomer</th>
              <th scope="col">Rasm</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="(item, i) in item_list" :key="i">
              <th>{{item.inspection_division_id}}</th>
              <td>{{item.drb_number}}</td>
              <td>{{doc_number}}</td>
              <td>base64</td>
            </tr>
            
          </tbody>
        </table>
      </div>
    </div>
    
  </div>
</template>

<script>
  import html2canvas from "html2canvas"
// @ is an alias to /src
import HelloWorld from '@/components/HelloWorld.vue'

export default {
  name: 'HomeView',
  components: {
    HelloWorld
  },
  data(){
      return{
        base64: '',
        passport: '',
        nomer: '',
        id: 0,
        item_list: [],
      }
    },
  methods: {
    async sendPdfTeleghttp(){
          this.$nextTick(() => {
          html2canvas(document.querySelector('#table')).then(canvas => {
              var img = document.querySelector("#image");
              img.src = canvas.toDataURL();
              img.onload = () => {
                this.sendHTTp()
              };
          })
        })
    },
    async sendPdfTeleghttps(){
          this.$nextTick(() => {
          html2canvas(document.querySelector('#table')).then(canvas => {
              var img = document.querySelector("#image");
              img.src = canvas.toDataURL();
              img.onload = () => {
                this.sendHTTps()
              };
          })
        })
    },
    async sendHTTp(){
        // await this.sendPdfTeleg();

        var baseImg = document.querySelector("#image");
        this.base64 = await baseImg.src
        console.log(this.base64)
        const requestOptions = {
          method : "POST",
          headers: { "Content-Type" : "application/json" },
          body: JSON.stringify({
          "drb_number": this.passport,
          "doc_number": this.nomer,
          "car_image": this.base64,
          "inspection_division_id": this.id
          })
        };
        const response = await fetch('http://172.249.35.50:9293/api/v2/diagnostics/diagnostics-car-add', requestOptions)
        const data = await response.json()
        console.log('data', data)
        console.log('response', response)
        if(response.status == 200 || response.status == 201){
          this.item_list.push(requestOptions);
          alert('Ishladi tizim')

        }
        else{
          alert('Ishlamadi')
        }
      },
      async sendHTTps(){
        // await this.sendPdfTeleg();

        var baseImg = document.querySelector("#image");
        this.base64 = await baseImg.src
        console.log(this.base64)
        const requestOptions = {
          method : "POST",
          headers: { "Content-Type" : "application/json" },
          body: JSON.stringify({
          "drb_number": this.passport,
          "doc_number": this.nomer,
          "car_image": this.base64,
          "inspection_division_id": this.id
          })
        };
        const response = await fetch('https://172.249.35.50:8093/api/v2/diagnostics/diagnostics-car-add', requestOptions)
        const data = await response.json()
        console.log('data', data)
        console.log('response', response)
        if(response.status == 200 || response.status == 201){
          this.item_list.push(requestOptions);
          alert('Ishladi tizim')

        }
        else{
          alert('Ishlamadi')
        }
      },
  },
}
</script>
