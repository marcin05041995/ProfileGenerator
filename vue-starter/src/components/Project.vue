<template>
  <div>
    <h4 class="font-weight-bold py-3 mb-4">
      <span class="text-muted font-weight-light"></span>Projekty
    </h4>
    <tr>
    <router-link to="/addproject">
    <b-btn style=" background: #f64a35" size="sm">Dodaj nowy</b-btn>&nbsp;
    </router-link>
 </tr>
<br>

    <v-client-table :data="projects" :columns='columns' :options="options">
      <template slot="edit" slot-scope="props">
        <div>

           <router-link to="/editproject">
          <b-btn variant="outline-success borderless icon-btn" class="btn-xs" @click.prevent="editproject(props.row.id)">
            <i class="ion ion-md-create"></i>
          </b-btn>
          </router-link>

          <b-btn variant="outline-danger borderless icon-btn" class="btn-xs" @click.prevent="remove(props.row.id)">
            <i class="ion ion-md-close"></i>
          </b-btn>
        </div>
      </template>


      <template slot="child_row" slot-scope="props">
        <tr>
          <td>
            <b>Nazwa:</b>
          </td>
          <td>{{props.row.name}}</td>
        </tr>
        <tr>
          <td>
            <b>Tytuł:</b>
          </td>
          <td> {{props.row.title}}</td>
        </tr>
        <tr>
          <td>
            <b>Klient Sektor:</b>
          </td>
          <td> {{props.row.clientSector}}</td>
        </tr>
        <tr>
          <td>
            <b>Technologie:</b>
          </td>
          <td>
            <template v-for="(tech, index) in props.row.technology">
              {{tech.name}}{{index != props.row.technology.length-1 ? ', ' : ' ' }}
              </template>
              </td>
        </tr>
        <tr>
          <td>
            <b>Data rozpoczęcia projektu:</b>
          </td>
          <td>{{props.row.startDate}}</td>
        </tr>
        <tr>
          <td>
            <b>Data zakończenia projektu:</b>
          </td>
          <td>
            {{props.row.endDate}}</td>
        </tr>
      </template>
    </v-client-table>
  </div>
</template>

<style src="@/vendor/libs/vue-data-tables/vue-data-tables.scss" lang="scss"></style>

<script>
import axios from "axios";
import Vue from "vue";
import router from "../router";
import moment from "moment";

import { ClientTable } from "vue-tables-2";

Vue.use(ClientTable);

export default {
  name: "tables-vue-tables-2",
  metaInfo: {
    title: "Projekty"
  },
  data: () => ({
    tableData: [],
    projects: [],
    errors: [],
    alert: "",
    columns: ["name", "title", "edit"],
    options: {
      pagination: {
        chunk: 5
      },
      sortIcon: {
        is: "fa-sort",
        base: "fas",
        up: "fa-sort-up",
        down: "fa-sort-down"
      }
    }
  }),
  created() {
    axios
      .get(`http://localhost:4444/api/projects/GetProjects`)
      .then(response => {
        // this.project.startDate = this.frontEndDateFormat(
        //   response.data.startDate
        // );
        // this.project.endDate = this.frontEndDateFormat(response.data.endDate);
        this.projects = response.data;
      })
      .catch(e => {
        this.errors.push(e);
      });
  },
  methods: {
    frontEndDateFormat: function(date) {
      return moment(date, "YYYY-MM-DD").format("DD/MM/YYYY");
    },
    edit(row) {
      alert(`Edit: ${row}`);
    },
    editproject(id) {
      router.push({
        path: `/editproject?id=` + id
      });
    },
    remove(row) {
      alert(`Remove: ${row}`);
    }
  }
};
</script>

<style scoped>
/* .btn-color {
  background: #f64a35;
} */
</style>