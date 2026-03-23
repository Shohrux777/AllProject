<template>
  <div v-if="visible" class="image-modal " @click="close">
    <div class="image-modal-content " @click.stop>
        <img :src="imageUrl" class="image-modal-img" />
    </div>
    <button @click="close" class="image-modal-close">✕</button>
   </div>
</template>

<script>
export default {
  name: "ImageModal",
  data() {
    return {
      visible: false,
      imageUrl: ""
    };
  },
  methods: {
    open(url) {
      this.imageUrl = url;
      this.visible = true;
    },
    close() {
      this.visible = false;
      this.imageUrl = "";
    }
  }
};
</script>

<style lang="scss" scoped>

.image-modal {
  position: fixed;
  inset: 0; // top:0; right:0; bottom:0; left:0;
  display: flex;
  align-items: center;
  justify-content: center;
  background-color: rgba(0, 0, 0, 0.8);
  z-index: 99999999999999;

  &-content {
    position: relative;
  }

  &-img {
    max-width: 100%;
    max-height: 100vh;
    border-radius: 8px;
    box-shadow: 0 4px 20px rgba(0, 0, 0, 0.4);
  }

  &-close {
    position: absolute;
    background: none;
    top: 8px;
    right: 8px;
    border: none;
    padding: 4px 8px;
    font-size: 16px;
    font-weight: bold;
    cursor: pointer;
    color: rgb(255, 255, 255);

    &:hover {
    //   background-color: #eee;
    }
  }
}
</style>