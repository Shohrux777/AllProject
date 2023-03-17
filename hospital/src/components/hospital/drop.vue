<template>
    <div class="uploader"
        @dragenter="OnDragEnter"
        @dragleave="OnDragLeave"
        @dragover.prevent
        @drop="onDrop"
        :class="{ dragging: isDragging }">
        
        
        <div class="upload-control">
            <div class="row w-100">
                <div class="col-4">
                    <mdb-input class="m-0 p-0" size="sm" type="text" v-model="patient_name" :label="$t('patient_name')"/>
                </div>
                <div class="col-4">
                    <mdb-input class="m-0 p-0" size="sm" type="text" v-model="doctor_name" :label="$t('doctor_name')"/>
                </div>
                <div class="col-4">
                    <mdb-input class="m-0 p-0" size="sm" type="text" v-model="file_name" :label="$t('file_name')"/>
                </div>
            </div>
        </div>
        <div class="upload-control_down" v-show="files.length">
            <label for="file">Выберите файл</label>
            <label class="bg-success" @click="upload">Сохранить файл</label>
        </div>


        <div v-show="!files.length">
            <i class="fa fa-cloud-upload"></i>
            <p>Перетащите ваши файлы сюда</p>
            <div>Или</div>
            <div class="file-input">
                <label for="file">Выберите файл</label>
                <input type="file" id="file" enctype="multipart/form-data" @change="onInputChange" multiple>
            </div>
        </div>

        <div class="images-preview" v-show="files.length">
            <div class="img-wrapper mb-4" v-for="(file, index) in files" :key="index">
                <div class="delete_file" @click="files.splice(index,1)">
                    <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-x" width="15" height="15" viewBox="0 0 24 24" stroke-width="2.5" stroke="#ff2825" fill="none" stroke-linecap="round" stroke-linejoin="round">
                    <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                    <line x1="18" y1="6" x2="6" y2="18" />
                    <line x1="6" y1="6" x2="18" y2="18" />
                    </svg>
                </div>
                <img v-if="file.type == 'audio/mpeg'" src="../../assets/types/mp3.jpg" :alt="`File Uplaoder ${index}`">
                <img v-if="file.type == 'application/msword' || file.type ==  'application/vnd.openxmlformats-officedocument.wordprocessingml.document'" src="../../assets/types/word.jpg" :alt="`File Uplaoder ${index}`">
                <img v-if="file.type == 'application/pdf'" src="../../assets/types/pdf.png" :alt="`File Uplaoder ${index}`">
                <div class="details">
                    <span class="name" style="font-size: 12px;" v-text="file.name"></span>
                    <span class="size text-primary" style="font-size: 11px;" v-text="getFileSize(file.size)"></span>
                </div>
            </div>
        </div>
        <!-- <div class="update">
            <button @click="pdf_save">Pdf</button>
        </div> -->
    </div>
</template>

<script>
import { jsPDF } from "jspdf";
import axios from 'axios'
import { mdbInput} from 'mdbvue';
export default {
    components:{mdbInput},
    data: () => ({
        isDragging: false,
        dragCount: 0,
        files: [],
        images: [],
        form: new FormData(),
        patient_name: '',
        doctor_name: localStorage.docName,
        doctor_id: localStorage.AuthId,
        file_name: '',
        
    }),
    props: {
        patient:
        {
            type: Object,
            default(){
            return {}
            }
        },
    },
    mounted(){
        this.patient_name = this.patient.fio
    },
    methods: {
        OnDragEnter(e) {
            e.preventDefault();
            this.dragCount++;
            this.isDragging = true;
            return false;
        },
        OnDragLeave(e) {
            e.preventDefault();
            this.dragCount--;
            if (this.dragCount <= 0)
                this.isDragging = false;
        },
        onInputChange(e) {
            const files = e.target.files;
            Array.from(files).forEach(file => this.addImage(file));
        },
        onDrop(e) {
            e.preventDefault();
            e.stopPropagation();
            this.isDragging = false;
            const files = e.dataTransfer.files;
            Array.from(files).forEach(file => this.addImage(file));
        },
        addImage(file) {
            console.log(file)
            this.files.push(file);
            this.file_name = this.files[0].name
            // if (!file.type.match('image.*')) {
            //     this.$toastr.e(`${file.name} is not an image`);
            //     return;
            // }

            // const img = new Image(),
            //     reader = new FileReader();
            // reader.onload = (e) => this.images.push(e.target.result);
            // reader.readAsDataURL(file);
            // console.log(img)
        },
        getFileSize(size) {
            const fSExt = ['Bytes', 'KB', 'MB', 'GB'];
            let i = 0;
            
            while(size > 900) {
                size /= 1024;
                i++;
            }
            return `${(Math.round(size * 100) / 100)} ${fSExt[i]}`;
        },
        async upload() {
            this.files.forEach(file => {
                this.form.append('files', file);
            });
            console.log(this.files)
            axios.post(this.$store.state.hostname + "/HospitalPatientAnalizHistory/uploadOneMoreFiles",
                this.form,
                {
                    headers: {
                        'Content-Type': 'multipart/form-data'
                    }
                }).then(response => {
                    console.log(response.data)
                    this.$emit('close', {data:response.data, file_name: this.file_name, patient:this.patient.id})
                    // alert(`Total Mock File Uploaded: ${response.data}`);
                }).catch(error => {
                    alert(`something went wrong: ${error}`);
                });
        },
        
        pdf_save(){
            const doc = new jsPDF('p', 'mm', 'a3')
            doc.text("Hello world!", 1, 1);
            // doc.addImage(image)
            console.log(doc)
            doc.save("two-by-four.pdf");
        }
    }
}
</script>

<style lang="scss" scoped>
.uploader {
    width: 100%;
    min-height: 70vh;
    overflow-y: scroll;
    background: #2196F3;
    color: #fff;
    padding: 40px 15px;
    text-align: center;
    border-radius: 10px;
    border: 3px dashed #fff;
    font-size: 20px;
    position: relative;
    display: flex;
    align-items: center;
    justify-content: center;
    &.dragging {
        background: #fff;
        color: #2196F3;
        border: 3px dashed #2196F3;
        .file-input label {
            background: #2196F3;
            color: #fff;
        }
    }
    i {
        font-size: 85px;
    }
    .file-input {
        width: 300px;
        margin: auto;
        height: 68px;
        position: relative;
        label,
        input {
            background: #fff;
            color: #2196F3;
            width: 100%;
            position: absolute;
            left: 0;
            top: 0;
            padding: 10px;
            border-radius: 4px;
            margin-top: 7px;
            cursor: pointer;
            font-size: 16px;
        }
        input {
            opacity: 0;
            z-index: -2;
        }
    }
    .images-preview {
        display: flex;
        flex-wrap: wrap;
        margin-top: 20px;
        .img-wrapper {
            width: 170px;
            position:relative;
            display: flex;
            flex-direction: column;
            margin: 10px;
            height: 170px;
            justify-content: space-between;
            background: #fff;
            box-shadow: 5px 5px 20px #3e3737;
            img {
                max-height: 145px;
            }
            .delete_file{
                position: absolute;
                top:-17px;
                right: -17px;
                background: #fff;
                border-radius: 50%;
                padding: 0px 7px;
                cursor:pointer;
            }
        }
        .details {
            font-size: 12px;
            background: #fff;
            color: #000;
            display: flex;
            flex-direction: column;
            // align-items: self-start;
            padding: 3px 6px;
            .name {
                overflow: hidden;
                height: 35px;
            }
        }
    }
    .upload-control {
        position: absolute;
        width: 100%;
        background: #fff;
        top: 0;
        left: 0;
        display: flex;
        justify-content: center;
        border-top-left-radius: 7px;
        border-top-right-radius: 7px;
        padding: 10px;
        padding-bottom: 4px;
        text-align: right;
        button, label {
            background: #2196F3;
            border: 2px solid #03A9F4;
            border-radius: 3px;
            color: #fff;
            font-size: 15px;
            cursor: pointer;
            padding:0;
            
        }
        label {
            padding: 2px 5px;
            margin-right: 10px;
        }
    }
    .upload-control_down{
        position: absolute;
        width: 100%;
        background: #fff;
        bottom: 0;
        left: 0;
        display: flex;
        justify-content: center;
        border-top-left-radius: 7px;
        border-top-right-radius: 7px;
        padding: 10px;
        padding-bottom: 4px;
        text-align: right;
        button, label {
            background: #2196F3;
            border: 2px solid #03A9F4;
            border-radius: 3px;
            color: #fff;
            font-size: 15px;
            cursor: pointer;
            padding:0;
            
        }
        label {
            padding: 2px 5px;
            margin-right: 10px;
        }
    }
    
}
</style>