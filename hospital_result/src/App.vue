<template>
  <div id="app">
    <router-view/>
  </div>
</template>

<style lang="scss">

@import "./scss/app.scss";
/* @import url("https://fonts.googleapis.com/css?family=Roboto:300,400,500,700&display=swap"); */
@font-face{
  font-family: "Montserrat";
  src: url("./font/Montserrat/Montserrat-Medium.ttf")
}

nav {
  padding: 30px;

  a {
    font-weight: bold;
    color: #2c3e50;

    &.router-link-exact-active {
      color: #42b983;
    }
  }
}
</style>
