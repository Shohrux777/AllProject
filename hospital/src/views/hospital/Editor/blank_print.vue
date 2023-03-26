<template>
  <div>
    <div class="editor_blank allWidthLab" id="table" style="position:relative;">
      <input type="text" style="position:absolute; top: -500px; outline:none;border:none;" ref="print" @keyup.enter="send">
      <div class="img " style="text-align:center;" v-show="false">
        <img id="image" width="100%" height="25%" alt="">
      </div>
      <div class="img " style="text-align:center;" >
        <img src="../../../assets/lab1.png" width="100%" alt="">
      </div>
      <loader v-if="loading" />
      <div class="lab_answer upHeightShablon" >
        <div class=" " style="display:flex; justify-content:center; flex-direction:column;">
          <table border class="allWidthShablonTable">
            <thead>
              <tr>
                <th class="fs" style="width:45%;"  ><span class="font-weight-bold ">ПАЦИЕНТ:</span>  {{patient_name}}</th>
                <th class="fs"  ><span class="font-weight-bold ">ВРАЧ:</span>  {{doc_name}}</th>
              </tr>
              <tr>
                <th class="fs"   style="width:45%;"><span class="font-weight-bold ">ВОЗРАСТ:</span> {{borndate}} </th>
                <th class="fs"  ><span class="font-weight-bold ">ДАТА:</span> {{date}}</th>
              </tr>
            </thead>
          </table>
          <div class="mt-4" v-html="content[0]"></div>
        </div>
        
      </div>
    </div>
    <div class="d-flex justify-content-center">
      <div style="width:83%; text-align:right; position:fixed; bottom:10px; right:108px;" v-if="font_size == false">
        <mdb-btn color="success px-4 py-1" style="font-size:15px;" type="submit" @click="printed"><mdb-icon icon="print" /></mdb-btn>
        <mdb-btn color="primary px-4 py-1" style="font-size:15px;" v-if="option.sended_status==false" type="submit" @click="send"><mdb-icon color="white"  fab icon="telegram-plane" /></mdb-btn>
      </div>
    </div>
  </div>
</template>

<script>
import html2canvas from "html2canvas"
// import jsPDF from "jspdf"
// import domtoimage from "dom-to-image";
import { mdbBtn, mdbIcon} from 'mdbvue';

export default {
 async mounted() {
    this.printed()
  },
   components: {
      mdbBtn, mdbIcon
    },
    props: {
      content:
      {
        type: Array,
        default(){
          return []
        }
      },
      patient_name:
      {
        type: String,
        return: ''
      },
      doc_name:
      {
        type: String,
        return: ''
      },
      patient_id:
      {
        type: Number,
        return: null
      },
      borndate:
      {
        type: String,
        return: ''
      },
      title:
      {
        type: String,
        return: ''
      },

    },
  data(){
    return{
      option: {},
      id: this.$route.params.id,
      base64: '',
      loading: false,
      date: new Date().toLocaleDateString(),
      font_size: true,
      test: 0,
      
     
    }
  },
  // async created(){
  //   if(this.id>0){
  //     this.loading =true;
  //     const response = await fetch(this.$store.state.hostname + '/HospitalVSK/' + this.id)
  //     const data = await response.json();
  //     this.loading = false;
  //     console.log(data)
  //     this.option = data;
  //     this.borndate = this.option.patients.bornDate.slice(0,4);
  //     this.date = this.option.created_date_time.slice(0,10)

  //     await this.sendPdfTeleg();
  //     this.font_size = false
  //   }
  // },
  methods: {
    async printed(){
      await this.sendPdfTeleg();
    },
    async sendPdfTeleg(){
      console.log('ishladi')
        await this.$nextTick(() => {
             // captureId is the page that needs to be downloaded
         html2canvas(document.querySelector('#table')).then(canvas => {
            console.log(canvas.toDataURL())
            var img = document.querySelector("#image");
            img.src = canvas.toDataURL();
            img.onload = () => {
              console.log('dasdasdas')
              this.send()
            };
            // this.loading = false;

        })
      })

        
     
      // console.log(doc)
    },
    async send(){
      var baseImg = document.querySelector("#image");
      this.base64 = await baseImg.src
      console.log(this.base64)
      const requestOptions = {
        method : "POST",
        headers: { "Content-Type" : "application/json" },
        body: JSON.stringify({
        "patientRecipeTitle": this.title + ' ( ' + this.doc_name + ' )',
        "patinetRecipeStr": this.base64,
        "doctorId": localStorage.AuthId,
        "patientsId": this.patient_id,
        })
      };
      // this.loading = true;
      const response = await fetch(this.$store.state.hostname + '/PatientRecipeByDoctors', requestOptions)
      const data = await response.json()
      console.log('dasdasdas')
      console.log(data)
      if(data.id){
        // this.loading = false;
        window.print();
        this.$router.push('/doctor')
      }
      else{
        // this.$refs.msg.error('Error_successfully')
        this.loading = false;
        // this.modal_info = data.detail + "    (" + data.routine + ")";
        // this.modal_status = true;
        return false;
      }
    }

  },
}
</script>

<style >
@import url(../../../style/style.css);

th, td{
  padding: 2px 8px;
}
.fs{
  font-size: 29px;
}

.headerTitle{
  font-size:50px;
}
.editor_blank b{
  font-weight: bold !important;
}
.editor_blank p{
  margin: 0 !important;
  padding: 0 !important;
  padding-bottom: 5px !important;
  margin-bottom: 0px !important;
  font-size: 26px !important;
}
.editor_blank div{
  font-size: 26px !important;
}
.editor_blank h6{
  font-size: 26px !important;
}
.editor_blank h5{
  font-size: 30px !important;
}
.editor_blank h4{
  font-size: 35px !important;
}
.editor_blank h3{
  font-size: 40px !important;
}
.editor_blank h2{
  font-size: 45px !important;
}
.editor_blank h1{
  font-size: 50px !important;
}
</style>