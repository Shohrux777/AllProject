<template>
  <div class="skud-face-logs">
    <div class="page-header d-flex align-items-center justify-content-between flex-wrap">
      <div class="title-wrap">
        <router-link to="/skud_face_logs" class="page-title">
          <h5 class="m-0">{{$t('skud_face_logs')}}</h5>
        </router-link>
        <div v-if="start_date && end_date" class="range-chip">
          {{start_date}} — {{end_date}}
        </div>
      </div>
      <div class="filters d-flex align-items-center flex-wrap">
        <div class="date-field">
          <small class="text-muted d-block">Start</small>
          <MDBInput type="date" size="sm" class="m-0 p-0" v-model="start_date" />
        </div>
        <div class="date-field">
          <small class="text-muted d-block">End</small>
          <MDBInput type="date" size="sm" class="m-0 p-0" v-model="end_date" />
        </div>
        <MDBBtn color="info" size="sm" class="refresh-btn" @click="fetchLogs">
          <MDBIcon icon="sync" class="me-2"/>{{$t('refresh')}}
        </MDBBtn>
      </div>
    </div>
    <div class="content-card">
      <loader v-if="loading"/>
      <MDBTable v-else class="align-middle mb-0">
        <thead class="bg-light">
          <tr>
            <th>№</th>
            <th>{{$t('user_name')}}</th>
            <th>{{$t('userid')}}</th>
            <th>{{$t('door_name')}}</th>
            <th>{{$t('check_date')}}</th>
            <th>{{$t('check_time')}}</th>
            <th>{{$t('checktype')}}</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(row, index) in logs_t.rows" :key="index">
            <td>{{ index + 1 }}</td>
            <td>{{ row.user_name }}</td>
            <td>{{ row.userid }}</td>
            <td>{{ row.door_name }}</td>
            <td>{{ row.check_date }}</td>
            <td>{{ row.check_time }}</td>
            <td>{{ row.checktype }}</td>
          </tr>
        </tbody>
      </MDBTable>
    </div>
  </div>
</template>

<script>
import { MDBIcon, MDBBtn, MDBInput, MDBTable } from 'mdb-vue-ui-kit';
import loader from "../../components/loader.vue";

export default {
  data(){
    return {
      loading: false,
      start_date: '',
      end_date: '',
      logs_t: {
        rows: []
      },
    };
  },
  components: {
    MDBIcon,
    MDBBtn,
    MDBInput,
    MDBTable,
    loader
  },
  methods: {
    async fetchLogs(){
      this.loading = true;
      try{
        const response = await fetch(
          this.$store.state.hostname +
          `/SkudDoorCheckinouts/getFaceLogsByDate?start_date=${this.start_date}&end_date=${this.end_date}`
        );
        if(response.status == 201 || response.status == 200)
        {
          const items = await response.json();
          this.logs_t.rows = items.map(item => ({
            ...item,
            check_date: item.check_date || '',
            check_time: item.check_time || ''
          }));
        }
        else{
          const data_text = await response.text();
          console.log(data_text);
        }
      }
      catch{
        console.log(this.$i18n.t('network_ne_connect'));
      }
      this.loading = false;
    }
  },
  async mounted(){
    const today = new Date();
    const pad = (value) => String(value).padStart(2, "0");
    const y = today.getFullYear();
    const m = pad(today.getMonth() + 1);
    const d = pad(today.getDate());
    const yesterday = new Date(today.getTime() - 24 * 60 * 60 * 1000);
    const y2 = yesterday.getFullYear();
    const m2 = pad(yesterday.getMonth() + 1);
    const d2 = pad(yesterday.getDate());
    this.end_date = `${y}-${m}-${d}`;
    this.start_date = `${y2}-${m2}-${d2}`;
    // await this.fetchLogs();
  }
};
</script>

<style>
.skud-face-logs {
  padding: 14px 16px;
}

.page-header {
  background: #fff;
  border: 1px solid #eef1f4;
  border-radius: 8px;
  padding: 12px 14px;
  margin-bottom: 12px;
  box-shadow: 0 2px 8px rgba(15, 23, 42, 0.05);
}

.title-wrap {
  display: flex;
  align-items: center;
  gap: 10px;
}

.page-title {
  color: inherit;
  text-decoration: none;
}

.range-chip {
  background: #f1f5f9;
  color: #475569;
  border-radius: 999px;
  padding: 4px 10px;
  font-size: 12px;
}

.filters {
  gap: 12px;
}

.date-field {
  min-width: 170px;
}

.refresh-btn {
  margin-top: 16px;
  padding: 6px 16px;
}

.content-card {
  background: #fff;
  border: 1px solid #eef1f4;
  border-radius: 8px;
  padding: 12px;
  min-height: 140px;
  box-shadow: 0 2px 8px rgba(15, 23, 42, 0.04);
}

@media (max-width: 768px) {
  .filters {
    width: 100%;
  }

  .refresh-btn {
    margin-top: 4px;
  }
}
</style>
