<template>
  <div>
    <video ref="video" autoplay muted playsinline width="320" />
    <canvas ref="canvas" style="display:none;"></canvas>
    <div v-if="capturedImage">
      <p>Tanilgan yuz:</p>
      <img :src="capturedImage" width="200" />
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      capturedImage: null,
      isProcessing: false,
      faceDetected: false
    };
  },
  async mounted() {
    setTimeout(async () => {
    console.log("faceapi mavjudmi:", typeof faceapi); // "object" chiqishi kerak
    await this.loadModels(); // models ni yuklaydi
    await this.startCamera(); // kamera yoqiladi
    this.detectFaceLoop();   // yuz aniqlash boshlanadi
  }, 100);
  },
  methods: {
    async loadModels() {
        const MODEL_URL = "https://cdn.jsdelivr.net/npm/face-api.js@0.22.2/weights";
        await faceapi.nets.tinyFaceDetector.loadFromUri(MODEL_URL);
    },
    async startCamera() {
        try {
            const stream = await navigator.mediaDevices.getUserMedia({ video: true });
            this.$refs.video.srcObject = stream;
            this.stream = stream;
            console.log("✅ Kamera ishladi");
        } catch (err) {
            console.error("❌ Kamera yoqilmadi:", err);
        }
        },
    async detectFaceLoop() {
      const video = this.$refs.video;

      const interval = setInterval(async () => {
        if (this.isProcessing) return;

        this.isProcessing = true;
        const result = await faceapi.detectSingleFace(
          video,
          new faceapi.TinyFaceDetectorOptions()
        );

        if (result && !this.faceDetected) {
          this.faceDetected = true;
          console.log("Yuz aniqlandi!");
          this.capturePhoto();
          clearInterval(interval);
        }

        this.isProcessing = false;
      }, 1000); // Har 1 soniyada tekshiradi
    },
    capturePhoto() {
      const video = this.$refs.video;
      const canvas = this.$refs.canvas;
      const ctx = canvas.getContext("2d");

      canvas.width = video.videoWidth;
      canvas.height = video.videoHeight;
      ctx.drawImage(video, 0, 0, canvas.width, canvas.height);

      const base64 = canvas.toDataURL("image/jpeg");
      this.capturedImage = base64;

      this.stopCamera();

      this.sendToBackend(base64);
    },
    stopCamera() {
      if (this.stream) {
        this.stream.getTracks().forEach((t) => t.stop());
        this.$refs.video.srcObject = null;
      }
    },
    async sendToBackend(base64Image) {
      try {
        await this.$axios.post("http://localhost:5000/api/facerecognize", {
          imageBase64: base64Image
        });
        console.log("Rasm yuborildi");
      } catch (err) {
        console.error("Yuborishda xatolik:", err);
      }
    }
  }
};
</script>