<template>
  <div>
    <div class="progress-bar-wrapper">
        <div
            v-for="(status, index) in taskSegments"
            :key="index"
            :class="['task-segment', status]"
        ></div>
    </div>
  </div>
</template>

<script>
export default {
 
props:{
    totalTasks: {
        type: Number,
        default: 10
    },
    doneCount: {
        type: Number,
        default: 2
    },
    pendingCount: {
        type: Number,
        default: 3
    },
},
computed: {
  notStartedCount() {
    return Math.max(0, this.totalTasks - this.doneCount - this.pendingCount);
    },
  taskSegments() {
    const segments = [];
    for (let i = 0; i < this.doneCount; i++) segments.push('done');
    for (let i = 0; i < this.pendingCount; i++) segments.push('pending');
    for (let i = 0; i < this.notStartedCount; i++) segments.push('not_started');
    return segments;
  }
}
}
</script>

<style scoped>
.progress-bar-wrapper {
  display: flex;
  height: 5px;
  width: 100%;
  border-radius: 4px;
  overflow: hidden;
}

.task-segment {
  flex: 1;
  margin-right: 1px;
}

.task-segment.done {
  background-color: #4caf50; /* yashil */
}
.task-segment.pending {
  background-color: #ffc107; /* sariq */
}
.task-segment.not_started {
  background-color: #cccdce; /* siniy-kulrang */
}
</style>