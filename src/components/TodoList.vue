<template>
    <v-container>
        <v-row justify="center">
            <v-col cols="12" sm="8">
                <v-card>
                    <v-card-title>
                        <v-text-field v-model="search" label="search" clearable outlined/>
                    </v-card-title>
                    <v-divider></v-divider>
                    <v-card-title>
                        <v-text-field v-model="newTask" label="add" clearable outlined @keyup.enter="addTask2"/>
                        <v-btn @click="addTask2">
                            add
                        </v-btn>      
                    </v-card-title>
                    <v-divider></v-divider>
                    <v-list>
                        <v-list-item v-for="(task, index) in filteredTasks" :key="index">
                            <v-list-item-content>
                                <v-text-field v-if="task.isEditing" v-model="task.text" outlined dense @blur="saveTask2(index)" @keyup.enter="saveTask2(index)"/>
                                <v-list-item-title v-else>
                                    {{task.text}}
                                </v-list-item-title>
                            </v-list-item-content>
                            <v-list-item-action>
                                <v-btn color="blue" @click="editTask(index)">
                                    <text v-if="!task.isEditing" >edit</text>
                                    <text v-else>save</text>
                                </v-btn>
                                <v-btn color="red" @click="removeTask2(index)">
                                    <text>delete</text>
                                </v-btn>
                            </v-list-item-action>
                        </v-list-item>
                    </v-list>
                </v-card>
            </v-col>
        </v-row>
        <v-icon icon="fas fa-plus" />
        <v-icon icon="mdi:mdi-minus" />
    </v-container>
</template>

<script>
import axios from 'axios';
import { v4 as uuidv4 } from 'uuid';
export default {
    name:'TodoList',
    data() {
        return {
            newTask:'',
            search:'',
            tasks:[],
        };
    },
    computed: {
        filteredTasks() {
            return this.tasks.filter((task) => task.text.includes(this.search))
        },
    },
    mounted() {
        this.getTodoList2()
    },
    methods: {
        // addTask() {
        //     if (this.newTask.trim()) {
        //         this.tasks.push({ id: uuidv4(), text: this.newTask, isEditing: false });
        //         this.newTask = '';
        //     }
        // },
        // removeTask(index) {
        //     this.tasks.splice(index, 1);
        // },
        editTask(index) {
            this.tasks[index].isEditing = !this.tasks[index].isEditing;
        },
        // saveTask(index) {
        //     if (!this.tasks[index].text.trim()) {
        //         this.removeTask(index);
        //     }else {
        //         this.tasks[index].isEditing = false;
        //     }
        // },

        // axios for json-server
        //---------------------------------------------------------------------------
        // async getTodoList() {
        //   try {
        //     const response = await axios.get("http://localhost:3000/listData");
        //     this.tasks = response.data;
        //   } catch (error) {
        //     console.error("Failed to fetch tasks:", error);
        //   }
        // },
        // async addTask1() {
        //   try {
        //     const response = await axios.post('http://localhost:3000/listData', {
        //       id: uuidv4()
        //       text: this.newTask,
        //       isEditing: false,
        //     });
        //     this.tasks.push(response.data);
        //     this.newTask = '';
        //   } catch (error) {
        //     console.error('Failed to add task:', error);
        //   }
        // },
        // async saveTask1(index) {
        //   try {
        //     const updatedTask = this.tasks[index];
        //     const response = await axios.put(`http://localhost:3000/listData/${updatedTask.id}`, updatedTask);
        //     this.tasks[index] = response.data;
        //   } catch (error) {
        //     console.error('Failed to save task:', error);
        //   }
        // },
        // async removeTask1(index) {
        //   try {
        //     const taskId = this.tasks[index].id;
        //     await axios.delete(`http://localhost:3000/listData/${taskId}`);
        //     this.tasks.splice(index, 1);
        //   } catch (error) {
        //     console.error('Failed to delete task:', error);
        //   }
        // },


        // axios for webApi
        //-------------------------------------------------------------------------
        async getTodoList2() {
          try {
            const response = await axios.get("https://localhost:7034/api/Todo");
            console.log("get data", response)
            this.tasks = response.data;
          } catch (error) {
            console.error("Failed to fetch tasks:", error);
          }
        },
        async addTask2() {
          try {
            const item = {
              Id: uuidv4(),
              text: this.newTask,
              IsEditing: false,
            }
            const response = await axios.post('https://localhost:7034/api/Todo', item);
            this.tasks.push(response.data);
            this.newTask = '';
          } catch (error) {
            console.error('Failed to add task:', error);
          }
        },
        async saveTask2(index) {
          try {
            const updatedTask = this.tasks[index];
            const response = await axios.put(`https://localhost:7034/api/Todo/${updatedTask.id}`, updatedTask);
            this.tasks[index] = response.data;
          } catch (error) {
            console.error('Failed to save task:', error);
          }
        },
        async removeTask2(index) {
          try {
            const taskId = this.tasks[index].id;
            await axios.delete(`https://localhost:7034/api/Todo/${taskId}`);
            this.tasks.splice(index, 1);
          } catch (error) {
            console.error('Failed to delete task:', error);
          }
        },
    }
}
</script>

<style scoped>
.v-list-item-title {
    display: flex;
    align-items: center;
}
</style>