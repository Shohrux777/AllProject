<template>
  <div style="width: 350px;">
    <div class="calendar-controls">
        <select v-model="selectedYear" @change="refreshCalendar">
            <option v-for="year in years" :key="year" :value="year">{{ year }}</option>
        </select>

        <select v-model="selectedMonth" @change="refreshCalendar">
            <option v-for="month in months" :key="month.value" :value="month.value">
            {{ month.name }}
            </option>
        </select>
    </div>
    <loader v-if="loading"/>
    <table v-else class="calendar-table">
      <thead>
        <tr class="border-bottom">
          <th v-for="day in days" :key="day">{{day}}</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="(week, index) in calendar" :key="index" class="border-bottom">
          <td v-for="(day,i) in week" :key="day.date.toDateString()" @click="chooseDayInfo(day, index,i)"
            style="position: relative;" 
            :class="{
                    // 'is_Paid': day.isPaid,
                    'working': day.count>0 && day.last_day == false,
                    'last_working': day.count>0 && day.last_day == true,
                    'other-month': day.isOtherMonth,
                    'today': day.isToday,
                    'select_date': day.isSelect
                    }">
            <div class="calendar_date">{{ day.date.getDate() }}</div>
            <small class="calendar_hours font-weight-bold" v-if="day.count">{{ day.count }}</small>
            <i v-if="day.isYuqlama" class="fas fa-check text-warning font-weight-bold"
              style="position: absolute; top:2px; right: 3px; font-size: 11px;">
            </i>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>
<script>

export default {
  data() {
    const now = new Date();
    return {
      loading: false,
      selectedUserId: null,
      users: [{
        name: 'shohrux',
        id: 2
      }], // API dan user list
      workdays: 
        [], // API dan bir oyga qaytgan kunlik ishlaganlar
      calendar: [],
      yuqlama_list: [],
      user_puli_payed: [],
      selectedMonth: now.getMonth(), // 0-based
      selectedYear: now.getFullYear(), 
      months: [
        { value: 0, name: 'Yanvar' },
        { value: 1, name: 'Fevral' },
        { value: 2, name: 'Mart' },
        { value: 3, name: 'Aprel' },
        { value: 4, name: 'May' },
        { value: 5, name: 'Iyun' },
        { value: 6, name: 'Iyul' },
        { value: 7, name: 'Avgust' },
        { value: 8, name: 'Sentabr' },
        { value: 9, name: 'Oktabr' },
        { value: 10, name: 'Noyabr' },
        { value: 11, name: 'Dekabr' }
        ],
        days: ['Du', 'Se', 'Ch', 'Pa', 'Ju', 'Sh', 'Ya'],
        user_id: 0,

        old_index: 0,
        old_i: 0,
        order_list: [],
        choosen_day: '',
    };
  },
  props: {
    client_info:
    {
      type: Object,
      default(){
        return {
          fio: '',
          id: 0,
        }
      }
    },
  },
  watch: {
    client_info: {
      handler(newVal, oldVal) {
        if (newVal && newVal.id !== 0) {
          this.loadClientOrders();
        }
      },
      deep: true, // obyekt ichidagi qiymatlar ham kuzatilsin
    }
  },
  computed: {
    years() {
      const currentYear = new Date().getFullYear();
      const startYear = currentYear - 5;
      return Array.from({ length: 50 }, (_, i) => startYear + i);
    }
    },
  methods: {
    async loadClientOrders(){
      await this.fetchAllOrderList();
    },
    async fetchAllOrderList(){
      try{
        console.log(this.choosen_day)
        console.log(this.client_info)
        let client_id = 0;
        if(this.client_info.id){
          client_id =  this.client_info.id;
        }
        const response = await fetch(this.$store.state.hostname + "/TegirmonOrder/getByMonth?date=" + this.choosen_day + '&clientId=' + client_id);
        const data = await response.json();
        console.log('data_list order calenndar',data)
        if(response.status == 201 || response.status == 200)
        {
          this.order_list = data;
          this.generateCalendar(this.selectedYear, this.selectedMonth);
          return true;
        }
        else{
          this.generateCalendar(this.selectedYear, this.selectedMonth);
          // this.modal_info = data;
          // this.modal_status = true;
          return false;
        }
      }
      catch(error){
        console.log(error)
        this.generateCalendar(this.selectedYear, this.selectedMonth);
        // this.client_list = [];
        // this.modal_info = this.$i18n.t('network_ne_connect');
        // this.modal_status = true;
      }
    },
    async chooseDayInfo(day, index, i){
      this.calendar[ this.old_index][ this.old_i].isSelect = false;
      this.calendar[index][i].isSelect = true;
      this.old_index = index;
      this.old_i = i;
      this.$emit('choose_day', day);

      const kun = String(day.date.getDate()).padStart(2, "0");
      const month = String(day.date.getMonth() + 1).padStart(2, "0");
      const year = day.date.getFullYear();
      this.choosen_day = `${year}-${month}-${kun}`;
    },
    
    
   
    async loadWorkDays() {
      const res = await fetch(`/api/worked-days?userid=${this.selectedUserId}&month=2025-06`);
      this.workdays = await res.json();
      this.generateCalendar();
    },
    async refreshCalendar() {
          await this.fetchAllOrderList();
          // this.generateCalendar(this.selectedYear, this.selectedMonth);
    },
    generateCalendar(year, month) {
    const start = new Date(year, month, 1);
    const end = new Date(year, month + 1, 0);
    
    const weeks = [];
    let week = [];

    let date = new Date(start);
    date.setDate(date.getDate() - ((date.getDay() + 6) % 7)); // Dushanbadan boshlash

    const today = new Date();
    const todayStr = `${today.getFullYear()}-${(today.getMonth() + 1).toString().padStart(2, '0')}-${today.getDate().toString().padStart(2, '0')}`;

    while (date <= end || week.length > 0) {
        const isoDate = `${date.getFullYear()}-${(date.getMonth() + 1).toString().padStart(2, '0')}-${date.getDate().toString().padStart(2, '0')}`;
        const order_count = 0;
        if(this.order_list.length>0){
          order_count = this.order_list.filter(w => w.pickUpDate.slice(0, 10) === isoDate).length;
        }

        let select_day = new Date(isoDate);
        const last_d = false; // bugundan oldingi kunlar
        if(select_day<today){
          last_d = true;
        }
        
        const yuqlama_item = null;
        if(this.yuqlama_list.length>0){
          yuqlama_item = this.yuqlama_list.find(w => w.K_date.slice(0, 10) === isoDate);
        }
        

        week.push({
        date: new Date(date),
        last_day: last_d,
        isToday: isoDate === todayStr,
        isSelect: false,
        count: order_count,
        isYuqlama: yuqlama_item ? true : false,
        isOtherMonth: date.getMonth() !== month
        });

        if (week.length === 7) {
        weeks.push(week);
        week = [];
        }

        date.setDate(date.getDate() + 1);
    }
    this.calendar = weeks;
    console.log('calendar', this.calendar )
    }
  },
  mounted() {
    let time1 = new Date();
    this.choosen_day = time1.toISOString().slice(0,10); 
    // User listni yuklash
    // fetch('/api/user-list')
    //   .then(res => res.json())
    //   .then(data => (this.users = data));
    this.refreshCalendar();
  }
};
</script>
<style scoped>
.calendar-table {
  /* width: 100%; */
  text-align: center;
}
td {
  vertical-align: top;
}
td:hover{
    background: rgb(33, 255, 100);
    cursor: pointer;
}
tr:hover{
    background: white !important;
}
.select_date {
  position: relative;
  background: linear-gradient(135deg, #4facfe, #00f2fe);
  color: #fff;
  border-radius: 8px;
  animation: pulseBg 1.5s infinite;
}
.working{
  background-color: #b8fdc1;
}
.last_working{
  background-color: #fff1b2;
}
.today {
  background-color: #c7eefd;
}
.calendar-table td {
  width: 50px !important;
  height: 50px !important;
  vertical-align: top;
  padding: 5px;
  box-sizing: border-box;
  position: relative;
}
.calendar-table thead tr th{
  font-weight: 600 !important;
}
.other-month {
  color: #aaa; /* ochroq ko‘rinish uchun */
  background-color: #f9f9f9; /* yoki faqat rangni o‘zgartiring */
}
.calendar-controls{
  width: 100%;
  display: flex;
  justify-content: flex-start;
  padding: 5px 0;
}
.calendar-controls select{
  width: 120px;
  height: 30px;
  border: 1px solid rgb(204, 204, 204);
  border-radius: 5px;
  color: rgb(58, 57, 57);
  font-size: 14px;
  padding: 5px;
  margin-left: 5px;
}
.is_Paid{
  background-color: #b8cefd;
}


/* Fon pulsatsiya animatsiyasi */
@keyframes pulseBg {
  0% {
    box-shadow: 0 0 0 0 rgba(79, 172, 254, 0.7);
  }
  70% {
    box-shadow: 0 0 0 10px rgba(79, 172, 254, 0);
  }
  100% {
    box-shadow: 0 0 0 0 rgba(79, 172, 254, 0);
  }
}
</style>