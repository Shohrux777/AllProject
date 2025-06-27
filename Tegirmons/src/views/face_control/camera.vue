<template>
  <div class="camera-capture">
    <!-- Kamera tanlash -->
    <label for="cameraSelect">📷 Kamerani tanlang:</label>
    <select v-model="selectedDeviceId" id="cameraSelect">
      <option v-for="device in videoDevices" :key="device.deviceId" :value="device.deviceId">
        {{ device.label || 'Kamera ' + device.deviceId }}
      </option>
    </select>

    <!-- Kamera ochish va rasmga olish -->
    <div style="margin-top: 10px;">
      <button @click="startCamera">▶️ Kamerani yoqish</button>
      <button @click="capturePhoto" :disabled="!stream">📸 Rasmga olish</button>
    </div>

    <!-- Kamera oynasi -->
    <video ref="video" autoplay playsinline style="width: 320px; margin-top: 10px;" />

    <!-- Olingan rasm -->
    <div v-if="capturedImage" style="margin-top: 10px;">
      <p>📷 Olingan rasm:</p>
      <img :src="capturedImage" style="width: 200px;" />
    </div>

    <!-- Yashirin canvas -->
    <canvas ref="canvas" style="display: none;"></canvas>
  </div>
</template>

<script>
export default {
  data() {
    return {
      videoDevices: [],
      selectedDeviceId: null,
      stream: null,
      capturedImage: null
    };
  },
  mounted() {
    this.getCameraDevices();
  },
  methods: {
    async getCameraDevices() {
      try {
        const devices = await navigator.mediaDevices.enumerateDevices();
        this.videoDevices = devices.filter(device => device.kind === 'videoinput');

        if (this.videoDevices.length > 0) {
          this.selectedDeviceId = this.videoDevices[0].deviceId;
        }
      } catch (err) {
        console.error('Kameralarni olishda xatolik:', err);
      }
    },
    async startCamera() {
      try {
        if (this.stream) {
          this.stopCamera();
        }

        this.stream = await navigator.mediaDevices.getUserMedia({
          video: { deviceId: { exact: this.selectedDeviceId } }
        });

        this.$refs.video.srcObject = this.stream;
      } catch (err) {
        console.error('Kamerani yoqishda xatolik:', err);
      }
    },
    capturePhoto() {
      const video = this.$refs.video;
      const canvas = this.$refs.canvas;
      const context = canvas.getContext('2d');

      canvas.width = video.videoWidth;
      canvas.height = video.videoHeight;
      context.drawImage(video, 0, 0, canvas.width, canvas.height);

      // Base64 formatga o'tkazish
      const base64 = canvas.toDataURL('image/jpeg');
      this.capturedImage = base64;

      // Kamera o'chiriladi
      this.stopCamera();

      // Bu yerda siz base64 ni backendga yuborishingiz mumkin
      // this.sendToBackend(base64);
    },
    stopCamera() {
      if (this.stream) {
        this.stream.getTracks().forEach(track => track.stop());
        this.stream = null;
        this.$refs.video.srcObject = null;
      }
    }
  },
  beforeDestroy() {
    this.stopCamera(); // component yopilganda kamerani tozalash
  }
};
</script>

<style scoped>
.camera-capture select,
.camera-capture button {
  margin: 5px 5px 5px 0;
}
</style>