<template>
  <div class="editor_vue">
    <p>dasdasda dasdasd</p>
    <vue-editor style="width:300px; overflow: hidden; border: none;" placeholder="Enter your naznacheniya" :editorToolbar="customToolbar" v-model="content"></vue-editor>
    <!-- <div>
      {{content}}
    </div> -->
  </div>
</template>

<script>
import { VueEditor } from "vue2-editor";
export default {
  components: { VueEditor },
  data() {
    return {
      content: "",
      customToolbar: [
        ["bold", "italic", "underline"],
      ]
    };
  },
}
</script>

<style>

</style>
