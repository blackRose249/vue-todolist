<template>
  <v-layout class="rounded rounded-md">
    <v-app-bar color="surface-variant" title="TodoList"></v-app-bar>
    <v-main class="d-flex align-center justify-center" style="min-height: 300px;">
      <todo-list/>
    </v-main>
  </v-layout>
</template>

<script>
import TodoList from './components/TodoList.vue';
export default {
  name:'App',
  components: {
    TodoList,
  }
}
</script>

<style>
</style>