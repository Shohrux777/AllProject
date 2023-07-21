<template>
  <div>
    <blank_print v-if="show_print" :content = "content" :selectServiceList="selectServiceList" 
      :patient_name = "patient_name" :tableNumber="tableNumber" 
    :patient_id="patient_id" :doc_name="doc_name" :borndate = "borndate" :title="titleHos"/>
    <div v-else class="main main_bg_word" >
      <!-- Top bar -->
      <vue-file-toolbar-menu :content="menu" class="bar text-white border-top" style="z-index: 2; background-color:#1565c0;" />
      <div class="row mx-0 py-1 pt-2" style="background-color:#1565c0;">
        <div class="col-3 ">
          <lineSelect
            class="ml-0"
            style="margin-top: 10px;"
            :options="get_patient_client_list" 
            :searchshow="true"
            @select="selectPatient"
            :selected="patient_name"
            :label="$t('patient list')"
          />
          <small class="invalid-text " style="margin-top: 5px; font-weight: bold;"  v-if="$v.patient_name.$dirty && !$v.patient_name.required" >
            {{$t('name_invalid_text')}}
          </small>
        </div>
        <div class="col-3">
          <input placeholder="Title" class=" p-0  text-dark rounded " 
             v-model="titleHos" style="outline: none; padding: 0px 7px !important; 
             width: 400px; height: 30px; margin-top: 10px; border:none;"/>
          <small class="invalid-text " style="margin-top: 0px; font-weight: bold;"  v-if="$v.titleHos.$dirty && !$v.titleHos.required" >
            {{$t('name_invalid_text')}}
          </small>
        </div>
        <div class="col-3 ">
          <lineSelect
            class="ml-0"
            style="margin-top: 10px;"
            :options="get_doctor_list_by_casher.rows" 
            :searchshow="true"
            @select="selectDocUser"
            :selected="doc_for_ser_name"
            :label="$t('Doctor')"
          />
          <!-- <small class="invalid-text " style="margin-top: 5px; font-weight: bold;"  v-if="$v.patient_name.$dirty && !$v.patient_name.required" >
            {{$t('name_invalid_text')}}
          </small> -->
        </div>
      </div>
      <!-- Document editor -->
      <vue-document-editor class="editor document_editor"  ref="editor"
        :content.sync="content"
        :zoom="zoom"
        :page_format_mm="page_format_mm"
        :page_margins="page_margins"
        :display="display"
        />

        <div v-if="!show_print" style="padding-bottom:120px;" class="mx-3 d-flex justify-content-center mb-5">
          <div class="form-group purple-border" style="width:85%">
            <label for="exampleFormControlTextarea4">Text formatini to'girlash</label>
            <textarea class="form-control" id="exampleFormControlTextarea4" rows="8"></textarea>
          </div>
        </div>
    </div>

    <div v-show="false"  class="serviceList_fixed px-3" v-if="!show_print">
      <div class="row">
        <div class="col-2 mb-1 border"  v-for="(servic,i) in service_list" :key="i" 
        @click="selectServiceDoc(i)" :class="{'bg_success_service': servic.activ}">
          <div class="d-flex  justify-content-center align-items-center"  
            style="height: 40px; cursor:pointer;">
            <span style="font-size: 13px;">{{servic.name}}</span>
          </div>
        </div>
      </div>
    </div>
    <div v-show="false" class="serviceNumber" v-if="!show_print">
      <input placeholder="День" type="number" class=" p-0  text-dark rounded " 
             v-model="tableNumber" style="outline: none; padding: 0px 7px !important; 
             width: 100%; height: 45px;  border:none;"/>
    </div>
    <Toast ref="message"></Toast>

  </div>
  
      <!-- :overlay="overlay" -->
</template>

<script>
import blank_print from './blank_print.vue'
import VueFileToolbarMenu from 'vue-file-toolbar-menu';
import VueDocumentEditor from './DocumentEditor.vue'
import lineSelect from "../../../components/hospital/cashUserSelect.vue";
// import shablonSelect from "../../../components/hospital/shablonSelect.vue";
import { required } from 'vuelidate/lib/validators'
import {mapActions, mapGetters, mapMutations} from 'vuex'
  // import {mdbInput   } from 'mdbvue';
// import InvoiceTemplate from './InvoiceTemplate.vue';
export default {
  components: { VueDocumentEditor, VueFileToolbarMenu, lineSelect, blank_print },
  validations: {
    titleHos: {required},
    patient_name: {required}
  },
  data () {
    return {
      show_print: false,
      patient_name: '', 
      patient_id: null, 
      doc_name: localStorage.docName,
      shablon_name: '',
      borndate: '',
      shablon_id: null,
      // This is where the pages content is stored and synced
      content: [],
      zoom: 1,
      zoom_min: 0.10,
      zoom_max: 5.0,
      page_format_mm: [330, 460],
      page_margins: "10mm 15mm",
      display: "grid", // ["grid", "vertical", "horizontal"]
      mounted: false, // will be true after this component is mounted
      undo_count: -1, // contains the number of times user can undo (= current position in content_history)
      content_history: [], // contains the content states for undo/redo operations
      titleHos: '',
      doc_for_ser_name: localStorage.DocServFio,
      doc_for_ser_id: localStorage.DocServId,
      doc_for_ser_Docid: localStorage.DocServDocId,
      service_list: [],
      selectServiceList: [],
      tableNumber: null,
    }
  }, 
  created () {
    // Initialize gesture flags
    let start_zoom_gesture = false;
    let start_dist_touch = false;
    let start_zoom_touch = false;
    // Manage ctrl+scroll zoom (or trackpad pinch)
    window.addEventListener("wheel", (e) => {
      if(e.ctrlKey){
        e.preventDefault();
        this.zoom = Math.min(Math.max(this.zoom - e.deltaY * 0.01, this.zoom_min), this.zoom_max);
      }
    }, { passive: false });
    // Manage trackpad pinch on Safari
    window.addEventListener("gesturestart", (e) => {
      e.preventDefault();
      start_zoom_gesture = this.zoom;
    });
    window.addEventListener("gesturechange", (e) => {
      e.preventDefault();
      if(!start_zoom_touch){
        this.zoom = Math.min(Math.max(start_zoom_gesture * e.scale, this.zoom_min), this.zoom_max);
      }
    });
    window.addEventListener("gestureend", () => {
      start_zoom_gesture = false;
    });
    // Manage pinch to zoom for touch devices
    window.addEventListener("touchstart", (e) => {
      if (e.touches.length == 2) {
        e.preventDefault();
        start_dist_touch = Math.hypot(
          e.touches[0].pageX - e.touches[1].pageX,
          e.touches[0].pageY - e.touches[1].pageY
        );
        start_zoom_touch = this.zoom;
      }
    }, { passive: false });
    window.addEventListener("touchmove", (e) => {
      if (start_dist_touch && start_zoom_touch) {
        e.preventDefault();
        let zoom = start_zoom_touch * Math.hypot(
          e.touches[0].pageX - e.touches[1].pageX,
          e.touches[0].pageY - e.touches[1].pageY
        ) / start_dist_touch;
        this.zoom = Math.min(Math.max(zoom, this.zoom_min), this.zoom_max);
      }
    }, { passive: false });
    window.addEventListener("touchend", () => {
      start_dist_touch = false;
      start_zoom_touch = false;
    }, { passive: false });
    // Manage history undo/redo events
    const manage_undo_redo = (e) => {
      switch(e && e.inputType){
        case "historyUndo": e.preventDefault(); e.stopPropagation(); this.undo(); break;
        case "historyRedo": e.preventDefault(); e.stopPropagation(); this.redo(); break;
      }
    }
    window.addEventListener("beforeinput", manage_undo_redo);
    window.addEventListener("input", manage_undo_redo); // in case of beforeinput event is not implemented (Firefox)
    // If your component is susceptible to be destroyed, don't forget to
    // use window.removeEventListener in the Vue.js beforeDestroy handler
  },
  async mounted () { 
    console.log('dsdsdsdsdsd')
    await this.fetch_get_doctor_list(localStorage.AuthId);
    console.log('get_doctor_list_by_casher')
    console.log(this.get_doctor_list_by_casher)
    if(this.doc_for_ser_id != null){
      await this.fetch_users_service_list(this.doc_for_ser_id)
      console.log('this.get_user_service_list')
      console.log(this.get_user_service_list)
      this.service_list = [];
      for(let i=0; i<this.get_user_service_list.length; i++){
        let tempServic = {
          serviceTypeId: this.get_user_service_list[i].serviceTypeId,
          id: this.get_user_service_list[i].serviceType.id,
          name: this.get_user_service_list[i].serviceType.name,
          price: this.get_user_service_list[i].serviceType.price,
          activ: false,
        }
        this.service_list.push(tempServic)
      }
    }
    this.content = [];
    this.content.push(this.shablon_content.name)
    this.titleHos = this.shablon_content.title
    console.log(this.shablon_content)
    this.fetch_patient_client();
    this.mounted = true;
    console.log(this.getPatientInfo)
    if(this.shablon_content.title == ''){
      this.titleHos = this.getPatientInfo.reason;
    }
    if(Object.keys(this.getPatientInfo).length != 0){
      this.patient_id = this.getPatientInfo.id;
      this.patient_name = this.getPatientInfo.name;
      console.log(this.getPatientInfo)
      this.borndate = this.getPatientInfo.bornDate.slice(0,10);
    }
    

    this.updatePatientId(this.patient_id)
    

   },
  computed: {
    ...mapGetters(['get_patient_client_list', 'getPatientInfo', 'shablon_content','sablonSavepatient', 
      'get_doctor_list_by_casher', 'get_user_service_list']),
    // This is the menu content
    menu () {
      return [
        // Main commands
        { text: "New", title: "New", icon: "description", click: () => { if(confirm("This will create an empty document. Are you sure?")){ this.content = [""]; this.resetContentHistory(); } } },
        { text: "Print", title: "Print", icon: "print", click: () => this.printed() },
        { text: "Save", title: "Save", icon: "save", click: () => this.saved() },
        { is: "spacer" },
        // Undo / redo commands
        { title: "Undo", icon: "undo", disabled: !this.can_undo, hotkey: this.isMacLike ? "command+z" : "ctrl+z", click: () => this.undo() },
        { title: "Redo", icon: "redo", disabled: !this.can_redo, hotkey: this.isMacLike ? "shift+command+z" : "ctrl+y", click: () => this.redo() },
        { is: "spacer" },
        // Rich text menus
        { icon: "format_align_left", title: "Align left", active: this.isLeftAligned, disabled: !this.current_text_style, hotkey: this.isMacLike ? "shift+command+l" : "ctrl+shift+l", click: () => document.execCommand("justifyLeft") },
        { icon: "format_align_center", title: "Align center", active: this.isCentered, disabled: !this.current_text_style, hotkey: this.isMacLike ? "shift+command+e" : "ctrl+shift+e", click: () => document.execCommand("justifyCenter") },
        { icon: "format_align_right", title: "Align right", active: this.isRightAligned, disabled: !this.current_text_style, hotkey: this.isMacLike ? "shift+command+r" : "ctrl+shift+r", click: () => document.execCommand("justifyRight") },
        { icon: "format_align_justify", title: "Justify content", active: this.isJustified, disabled: !this.current_text_style, hotkey: this.isMacLike ? "shift+command+j" : "ctrl+shift+j", click: () => document.execCommand("justifyFull") },
        { is: "separator" },
        { icon: "format_bold", title: "Bold", active: this.isBold, disabled: !this.current_text_style, hotkey: this.isMacLike ? "command+b" : "ctrl+b", click: () => document.execCommand("bold") },
        { icon: "format_italic", title: "Italic", active: this.isItalic, disabled: !this.current_text_style, hotkey: this.isMacLike ? "command+i" : "ctrl+i", click: () => document.execCommand("italic") },
        { icon: "format_underline", title: "Underline", active: this.isUnderline, disabled: !this.current_text_style, hotkey: this.isMacLike ? "command+u" : "ctrl+u", click: () => document.execCommand("underline") },
        { icon: "format_strikethrough", title: "Strike through", active: this.isStrikeThrough, disabled: !this.current_text_style, click: () => document.execCommand("strikethrough") },
        { is: "button-color", type: "compact", menu_class: "align-center", disabled: !this.current_text_style, color: this.curColor, update_color: (new_color) => document.execCommand('foreColor', false, new_color.hex8) },
        { is: "separator" },
        { icon: "format_list_numbered", title: "Numbered list", active: this.isNumberedList, disabled: !this.current_text_style, click: () => document.execCommand("insertOrderedList") },
        { icon: "format_list_bulleted", title: "Bulleted list", active: this.isBulletedList, disabled: !this.current_text_style, click: () => document.execCommand("insertUnorderedList") },
        { html: "<b>H1</b>", title: "Header 1", active: this.isH1, disabled: !this.current_text_style, click: () => document.execCommand('formatBlock', false, '<h1>') },
        { html: "<b>H2</b>", title: "Header 2", active: this.isH2, disabled: !this.current_text_style, click: () => document.execCommand('formatBlock', false, '<h2>') },
        { html: "<b>H3</b>", title: "Header 3", active: this.isH3, disabled: !this.current_text_style, click: () => document.execCommand('formatBlock', false, '<h3>') },
        { html: "<b>H4</b>", title: "Header 4", active: this.isH4, disabled: !this.current_text_style, click: () => document.execCommand('formatBlock', false, '<h4>') },
        { html: "<b>H5</b>", title: "Header 5", active: this.isH5, disabled: !this.current_text_style, click: () => document.execCommand('formatBlock', false, '<h5>') },
        { html: "<b>H6</b>", title: "Header 6", active: this.isH6, disabled: !this.current_text_style, click: () => document.execCommand('formatBlock', false, '<h6>') },
        { icon: "format_clear", title: "Clear format", disabled: !this.current_text_style, click () { document.execCommand('removeFormat'); document.execCommand('formatBlock', false, '<p>'); } },
        { icon: "splitscreen", title: "Page break", disabled: !this.current_text_style, click: () => this.insertPageBreak() },
        
        { is: "spacer" },
        { // Format menu
          text: this.current_format_name,
          title: "Format",
          icon: "crop_free",
          chevron: true,
          menu: this.formats.map(([text, w, h]) => {
            return {
              text,
              active: (this.page_format_mm[0] == w && this.page_format_mm[1] == h),
              click: () => { this.page_format_mm = [w, h]; }
            }
          }),
          menu_width: 80,
          menu_height: 280
        },
        { // Margins menu
          text: this.current_margins_name,
          title: "Margins",
          icon: "select_all",
          chevron: true,
          menu: this.margins.map(([text, value]) => {
            return {
              text: text+" ("+value+")",
              active: (this.page_margins == value),
              click: () => { this.page_margins = value; }
            }
          }),
          menu_width: 200,
          menu_class: "align-center"
        },
        { // Zoom menu
          text: Math.floor(this.zoom * 100) + "%",
          title: "Zoom",
          icon: "zoom_in",
          chevron: true,
          menu: [
            ["200%", 2.0],
            ["150%", 1.5],
            ["125%", 1.25],
            ["100%", 1.0],
            ["75%", 0.75],
            ["50%", 0.5],
            ["25%", 0.25]
          ].map(([text, zoom]) => {
            return {
              text,
              active: this.zoom == zoom,
              click: () => { this.zoom = zoom; }
            }
          }),
          menu_width: 80,
          menu_height: 280,
          menu_class: "align-center"
        },
        { // Display mode menu
          title: "Display",
          icon: this.display == "horizontal" ? "view_column" : (this.display == "vertical" ? "view_stream" : "view_module"),
          chevron: true,
          menu: [{
            icon: "view_module",
            active: this.display == "grid",
            click: () => { this.display = "grid"; }
          }, {
            icon: "view_column",
            active: this.display == "horizontal",
            click: () => { this.display = "horizontal"; }
          }, {
            icon: "view_stream",
            active: this.display == "vertical",
            click: () => { this.display = "vertical"; }
          }],
          menu_width: 55,
          menu_class: "align-right"
        }
      ]
    },
    // Formats management
    current_format_name () {
      const format = this.formats.find(([, width_mm, height_mm]) => (this.page_format_mm[0] == width_mm && this.page_format_mm[1] == height_mm));
      return format ? format[0] : (this.page_format_mm[0]+"mm x "+this.page_format_mm[1]+"mm");
    },
    formats: () => [
      ["A0", 841, 1189],
      ["A0L", 1189, 841],
      ["A1", 594, 841],
      ["A1L", 841, 594],
      ["A2", 420, 594],
      ["A2L", 594, 420],
      ["A3", 297, 420],
      ["A3R", 250, 420],
      ["A3L", 420, 297],
      ["A4", 210, 297],
      ["A4L", 297, 210],
      ["A5", 148, 210],
      ["A5L", 210, 148],
      ["A6", 105, 148],
      ["A6L", 148, 105]
    ],
    // Margins management
    current_margins_name () {
      const margins = this.margins.find(([, margins]) => (this.page_margins == margins));
      return margins ? margins[0] : margins[1];
    },
    margins: () => [
      ["Medium", "20mm"],
      ["Small", "15mm"],
      ["Slim", "10mm 15mm"],
      ["Tiny", "5mm"]
    ],
    // Current text style management
    current_text_style () { return this.mounted ? this.$refs.editor.current_text_style : false; },
    isLeftAligned () { return ["start", "left", "-moz-left"].includes(this.current_text_style.textAlign); },
    isRightAligned () { return ["end", "right", "-moz-right"].includes(this.current_text_style.textAlign); },
    isCentered () { return ["center", "-moz-center"].includes(this.current_text_style.textAlign); },
    isJustified () { return ["justify", "justify-all"].includes(this.current_text_style.textAlign); },
    isBold () {
      const fontWeight = this.current_text_style.fontWeight;
      return fontWeight && (parseInt(fontWeight) > 400 || fontWeight.indexOf("bold") == 0);
    },
    isItalic () { return this.current_text_style.fontStyle == "italic"; },
    isUnderline () { // text-decoration is not overridden by children, so we query the parent stack
      const stack = this.current_text_style.textDecorationStack;
      return stack && stack.some(d => (d.indexOf("underline") == 0));
    },
    isStrikeThrough () { // text-decoration is not overridden by children, so we query the parent stack
      const stack = this.current_text_style.textDecorationStack;
      return stack && stack.some(d => (d.indexOf("line-through") == 0));
    },
    isNumberedList () { return this.current_text_style.isList && this.current_text_style.listStyleType == "decimal"; },
    isBulletedList () { return this.current_text_style.isList && ["disc", "circle"].includes(this.current_text_style.listStyleType); },
    isH1 () { return this.current_text_style.headerLevel == 1; },
    isH2 () { return this.current_text_style.headerLevel == 2; },
    isH3 () { return this.current_text_style.headerLevel == 3; },
    isH4 () { return this.current_text_style.headerLevel == 4; },
    isH5 () { return this.current_text_style.headerLevel == 5; },
    isH6 () { return this.current_text_style.headerLevel == 6; },
    curColor () { return this.current_text_style.color || "transparent"; },
    // Platform management
    isMacLike: () => /(Mac|iPhone|iPod|iPad)/i.test(navigator.platform),
    // Undo / redo flags
    can_undo () { return this.undo_count > 0; },
    can_redo () { return this.content_history.length - this.undo_count - 1 > 0; }
  },
  methods: {
    ...mapActions(['fetch_patient_client', 'fetch_get_doctor_list', 'fetch_users_service_list']),
    ...mapMutations(['updateShablonPatient', 'updatePatientId']),
    selectPatient(option){
      this.patient_name = option.data.fio;
      this.patient_id = option.data.id;
      console.log(option.data)
      this.borndate = option.data.bornDate.slice(0,10);
      this.updatePatientId(option.data.id);
    },
    async selectDocUser(option){
      console.log(option)
      this.doc_for_ser_name = option.data.fio;
      this.doc_for_ser_id = option.data.doctorAuth.users.id;
      this.doc_for_ser_Docid = option.data.DocAuthId;
      localStorage.DocServFio = option.data.fio;
      localStorage.DocServId = option.data.doctorAuth.users.id;
      localStorage.DocServDocId = option.data.DocAuthId;
      await this.fetch_users_service_list(this.doc_for_ser_id)
      console.log('this.get_user_service_list')
      console.log(this.get_user_service_list)
      this.service_list = [];
      for(let i=0; i<this.get_user_service_list.length; i++){
        let tempServic = {
          serviceTypeId: this.get_user_service_list[i].serviceTypeId,
          id: this.get_user_service_list[i].serviceType.id,
          name: this.get_user_service_list[i].serviceType.name,
          price: this.get_user_service_list[i].serviceType.price,
          activ: false,
        }
        this.service_list.push(tempServic)
      }
    },
    async selectServiceDoc(i){
      this.service_list[i].activ = !this.service_list[i].activ;
      this.selectServiceList = [];
      for(let j=0; j<this.service_list.length; j++){
        if(this.service_list[j].activ == true){
          this.selectServiceList.push(this.service_list[j])
        }
      }
      console.log(this.selectServiceList)
    },
    selectShablon(option){
      console.log(option)
      this.shablon_name = option.data.shablon_name;
      this.titleHos = option.data.shablon_name;
      this.shablon_id = option.data.id;
      this.content.push(option.data.shablon_content)
      // this.$router.push('/word')
      // console.log()

    },
    cls_wnd(){
      this.content = [];
      this.titleHos = '';
      this.patient_id = null;
      this.patient_name = '';
    },
    
    // Page overlays (headers, footers, page numbers)
    async printed(){
      // this.$refs.editor.printedForBlanka({id:this.patient_id, title:this.titleHos});
      if(this.tableNumber == null){
        this.tableNumber = 0;
      }
      else{
        this.tableNumber = parseInt(this.tableNumber)
      }
      this.show_print = true;
    },
    async saved(){
      console.log(this.content)
      console.log(localStorage.docId)
      console.log(localStorage.AuthId)
      console.log('this.titleHos')
      console.log(this.titleHos)
      
      if( this.$v.$invalid)
      {
        this.$v.$touch();
        this.$refs.message.warning('please_fill')
        return;
      }
      const requestOptions = {
        method: "POST",
        headers : { "Content-Type" : "application/json" },
        body : JSON.stringify({
          "shablon_content" : this.content[0],
          "shablon_name": this.titleHos,
          "authorizationId": localStorage.AuthId,
        })
      };
      const response = await fetch(this.$store.state.hostname + "/HospitalDoctorShablons", requestOptions);
      const data = await response.json();
      console.log(data);
      if(data.id)
      {
        this.$refs.message.success('Added_successfully')
        // this.cls_wnd();
        return true;
      }
      else{
        this.$refs.message.error('Error connect server')
        return false;
      }

      

                    // console.log( this.patient_id)
                    // if( this.$v.$invalid || this.patient_id == null )
                    //   {
                    //     this.$v.$touch();
                    //     this.$refs.message.warning('please_fill')
                    //     return;
                    //   }

                    //   const requestOptions = {
                    //       method: "POST",
                    //       headers : { "Content-Type" : "application/json" },
                    //       body : JSON.stringify({
                    //         "patinetRecipeStr" : this.content[0],
                    //         "patientRecipeTitle": this.titleHos,
                    //         "doctorId": localStorage.docId,
                    //         "patientsId": this.patient_id,
                    //         "registratedDate": "2021-09-03T09:46:14.326Z"
                    //       })
                    //     };

                    //     const response = await fetch(this.$store.state.hostname + "/PatientRecipeByDoctors", requestOptions);
                    //     const data = await response.json();
                    //     console.log(data);
                    //     if(data.id)
                    //     {
                    //       this.$refs.message.success('Added_successfully')
                    //       this.cls_wnd();
                    //       return true;
                    //     }
                    //     else{
                    //       this.$refs.message.error('Error connect server')
                    //       return false;
                    //     }
    },
    // Undo / redo functions examples
    undo () { if(this.can_undo){ this._mute_next_content_watcher = true; this.content = this.content_history[--this.undo_count]; } },
    redo () { if(this.can_redo){ this._mute_next_content_watcher = true; this.content = this.content_history[++this.undo_count]; } },
    resetContentHistory () { this.content_history = []; this.undo_count = -1; },
    // Insert page break function example
    async insertPageBreak () {
      // insert paragraph at caret position
      document.execCommand("insertParagraph");
      // insert a marker at caret position (start of the new paragraph)
      const marker = "###PB###"; // must be regex compatible
      document.execCommand("insertText", false, marker);
      // wait for DOM update
      await this.$nextTick();
      // find the marker inside content items and split this content item in two items between the two paragraphs
      // only match root tags (p, div, h1, h2...) to avoid non-root tags like <li>
      const regexp = new RegExp("<(p|div|h\\d)( [^/>]+)*>(<[^/>]+>)*"+marker);
      for(let i = 0; i < this.content.length; i++) {
        const item = this.content[i];
        if(typeof item != "string") continue;
        const match = regexp.exec(item);
        if(match) {
          const tags_open = match[0].slice(0, -marker.length);
          let content_plus_tags_close = item.substr(match.index + match[0].length);
          // insert <br> to empty pages that would not be handled correctly by contenteditable
          if(content_plus_tags_close.indexOf("</") == 0) content_plus_tags_close = "<br>" + content_plus_tags_close;
          this.content.splice(i, 1, item.substr(0, match.index), tags_open + content_plus_tags_close);
          return;
        }
      }
      // if the code didn't return before, the split didn't work (e.g. inside a <li>). just remove the marker from the content
      for(let i = 0; i < this.content.length; i++) {
        const item = this.content[i];
        if(typeof item != "string" || item.indexOf(marker) < 0) continue;
        this.content.splice(i, 1, item.replace(marker, ''));
        break;
      }
    }
  },
  watch: {
    content: {
      immediate: true,
      // Fill undo / redo history stack on user input
      handler (new_content) {
        if(!this._mute_next_content_watcher) { // only update the stack when content is changed by user input, not undo/redo commands
          this.content_history[++this.undo_count] = new_content;
          this.content_history.length = this.undo_count + 1; // remove all redo items
        }
        this._mute_next_content_watcher = false;
      }
    }
  }
}
</script>

<style>
.document_editor b{
  font-weight: bold !important;
}
.document_editor p{
  padding-bottom: 5px !important;
  margin-bottom: 0px !important;
  font-size: 22px !important;
}
.document_editor div{
  font-size: 22px !important;
}
.document_editor h6{
  font-size: 22px !important;
}
.document_editor h5{
  font-size: 28px !important;
}
.document_editor h4{
  font-size: 33px !important;
}
.document_editor h3{
  font-size: 38px !important;
}
.document_editor h2{
  font-size: 43px !important;
}
.document_editor h1{
  font-size: 48px !important;
}
html {
  height: 100%;
}
body {
  margin: 0;
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  color: black;
  background: rgb(240, 240, 240);
}
::-webkit-scrollbar {
  width: 16px;
  height: 16px;
}
::-webkit-scrollbar-track, ::-webkit-scrollbar-corner {
  display: none;
}
::-webkit-scrollbar-thumb {
  background-color: rgba(0, 0, 0, 0.5);
  border: 5px solid transparent;
  border-radius: 16px;
  background-clip: content-box;
}
::-webkit-scrollbar-thumb:hover {
  background-color: rgba(0, 0, 0, 0.8);
}
</style>

<style scoped>
b{
  font-weight: bold;
}
  .main {
    width: fit-content;
    min-width: 100%;
  }
  .main_bg_word{
    background-image: radial-gradient( circle farthest-corner at 92.3% 71.5%,  rgba(83,138,214,1) 0%, rgba(134,231,214,1) 90% );
  }
  .bar {
    position: sticky;
    left: 0;
    top: 0;
    width: calc(100vw - 16px);
    z-index: 1000;
    background: rgba(114, 161, 208, 0.8);
    border-bottom: solid 1px rgb(248, 249, 250);
    backdrop-filter: blur(10px);
    --bar-button-active-color: #188038;
    --bar-button-open-color: #188038;
    --bar-button-active-bkg: #e6f4ea;
    --bar-button-open-bkg: #e6f4ea;
  }

  .serviceList_fixed{
    position: fixed;
    bottom: 0;
    left: 0;
    right: 0;
    width: 100%;
    height:90px;
    background: #e6f4ea;
    overflow-y: scroll;
  }
  .serviceNumber{
    position: fixed;
    bottom: 90px;
    left: 0;
    width: 200px;
    height:45px;
    background: #e6f4ea;
  }
  .serviceNumberTitle{
    position: absolute;
    top:-22px;
    left: 5px;
  }
  .bg_success_service{
    background: #9fcfff;
  }
</style>