<template>
  <div>
    <div class="media align-items-center py-3 mb-3">
      <td>
        <img src="../assets/profile.jpg" alt="" class="d-block ui-w-150 rounded-circle">
      </td>
      <div class="media-body ml-4">
        <td style="padding-top: 100px">
          <h1 class="font-weight-bold mb-0">{{employee.profile.name}}</h1>
        </td>
        <h2 class="font-weight-bold mb-0">{{employee.profile.specialization}}</h2>
        <table class="table-level">
          <table class="table table-hover" border="1">
            <td v-for="numb in 5" class="box-level" :class="{'box':numb <= employee.profile.rating}" :key="numb">
              <div v-if="numb <= employee.profile.rating">
              </div>
            </td>
            <h5>
              <p style="color:gray">Rating:{{employee.profile.rating}} / 5</p>
            </h5>
          </table>
        </table>
        <tr>
          <td>
            <h5>
              <p style="color:gray">Overall Tenure:{{employee.profile.overallTenure}}year</p>
            </h5>
          </td>
        </tr>
        <router-link to="/editemployee">
          <b-btn style=" background: #f64a35" size="sm">Edytuj</b-btn>&nbsp;
        </router-link>
      </div>
    </div>

    <v-client-table :data="employee.skills" :columns='columns' :options="options">
      <template slot="edit" slot-scope="props">
        <div>
          <b-btn variant="outline-success borderless icon-btn" class="btn-xs" @click.prevent="cardEmployee(props.row.id)">
            <i class="ion ion-md-filing"></i>
          </b-btn>
          <b-btn variant="outline-danger borderless icon-btn" class="btn-xs" @click.prevent="remove(props.row.id)">
            <i class="ion ion-md-close"></i>
          </b-btn>
        </div>
      </template>
    </v-client-table>

    <table class="table table-hover">
      <tr>

        <td>
          <table class="table table-hover" border="1" style="margin-bottom:0;">
            <td class="box"></td>
          </table>
        </td>
        <td>Junior</td>

        <td>
          <table class="table table-hover" border="1" style="margin-bottom:0;">
            <td class="box"></td>
            <td class="box"></td>
          </table>
        </td>
        <td>Middle</td>

        <td>
          <table class="table table-hover" border="1" style="margin-bottom:0;">
            <td class="box"></td>
            <td class="box"></td>
            <td class="box"></td>
          </table>
        </td>
        <td>Proficient</td>

        <td>
          <table class="table table-hover" border="1" style="margin-bottom:0;">
            <td class="box"></td>
            <td class="box"></td>
            <td class="box"></td>
            <td class="box"></td>
          </table>
        </td>
        <td>Senior</td>

        <td>
          <table class="table table-hover" border="1" style="margin-bottom:0;">
            <td class="box"></td>
            <td class="box"></td>
            <td class="box"></td>
            <td class="box"></td>
            <td class="box"></td>
          </table>
        </td>
        <td>Expert</td>
      </tr>
    </table>

    <hr class="border-light m-0">
    <b-card-body v-for="p in employee.projects" :key="p.id">
      <table class="table " border="1">
        <tbody>
          <tr class="table-tittle">
            <td align="left" class="text-color" colspan="2">
              <h4> {{p.name}}</h4>
              <h4>{{p.title}}</h4>
            </td>
          </tr>
          <tr>
            <td>Client Sector:</td>
            <td>{{p.clientSector}}</td>
          </tr>

          <tr>
            <td>Technologies:</td>
            <td>
              <span v-for="(t, index) in p.technology" :key="t.id">{{t.name}}{{index != p.technology.length-1 ? ', ' : ' ' }}</span>
            </td>
          </tr>

          <tr>
            <td>Start:</td>
            <td>{{p.startDate}}</td>
          </tr>
          <tr>
            <td>End:</td>
            <td>{{p.endDate}}</td>
          </tr>
        </tbody>
      </table>

    </b-card-body>

    <v-client-table :data="employee.languages" :columns='columns3' :options="options">
      <template slot="edit" slot-scope="props">
        <div>
          <b-btn variant="outline-success borderless icon-btn" class="btn-xs" @click.prevent="cardEmployee(props.row.id)">
            <i class="ion ion-md-filing"></i>
          </b-btn>
          <b-btn variant="outline-danger borderless icon-btn" class="btn-xs" @click.prevent="remove(props.row.id)">
            <i class="ion ion-md-close"></i>
          </b-btn>
        </div>
      </template>
    </v-client-table>

    <table class="table table-hover">
      <tr>

        <td>
          <table class="table table-hover" border="1" style="margin-bottom:0;">
            <td class="box"></td>
          </table>
        </td>
        <td>Basic</td>

        <td>
          <table class="table table-hover" border="1" style="margin-bottom:0;">
            <td class="box"></td>
            <td class="box"></td>
          </table>
        </td>
        <td>Good</td>

        <td>
          <table class="table table-hover" border="1" style="margin-bottom:0;">
            <td class="box"></td>
            <td class="box"></td>
            <td class="box"></td>
          </table>
        </td>
        <td>Fluent</td>
      </tr>
    </table>

  </div>
</template>

<style src="@/vendor/libs/vue-data-tables/vue-data-tables.scss" lang="scss"></style>
<style src="@/vendor/styles/pages/users.scss" lang="scss"></style>

<script>
import axios from "axios";
import Vue from "vue";
import moment from "moment";

import { ClientTable } from "vue-tables-2";

Vue.use(ClientTable);

export default {
  name: "cartemployee",
  metaInfo: {
    title: "Karta Pracownika"
  },
  data: () => ({
    tableData: [],
    employee: [],
    employee: {},
    errors: [],
    alert: "",
    columns: ["name", "experience", "profficiency"],
    columns2: [
      "name",
      "title",
      "clientSector",
      "technology",
      "startDate",
      "endDate"
    ],
    columns3: ["name", "speaking", "writing", "reading"],
    options: {
      pagination: {
        chunk: 5
      },
      filterable: false,
      sortIcon: {
        is: "fa-sort",
        base: "fas",
        up: "fa-sort-up",
        down: "fa-sort-down"
      }
    }
  }),
  created() {
    var id = this.getUrlParameter("id");
    axios
      .get(`http://localhost:4444/api/employees/GetEmployee?id=` + id)
      .then(response => {
        // this.employee.project.startDate = this.frontEndDateFormat(
        //   response.data.startDate
        // );
        // this.employee.project.endDate = this.frontEndDateFormat(
        //   response.data.endDate
        // );
        this.employee = response.data;
      })
      .catch(e => {
        this.errors.push(e);
      });
  },
  methods: {
    // frontEndDateFormat: function(date) {
    //   return moment(date, "YYYY-MM-DD").format("DD/MM/YYYY");
    // },
    getUrlParameter(name) {
      var results = new RegExp("[?&]" + name + "=([^&#]*)").exec(
        window.location.href
      );
      if (results == null) {
        return null;
      } else {
        return decodeURI(results[1]) || 0;
      }
    },
    frontEndDateFormat: function(date) {
      return moment(date, "YYYY-MM-DD").format("DD/MM/YYYY");
    },
    edit(row) {
      alert(`Edit: ${row}`);
    },
    remove(row) {
      alert(`Remove: ${row}`);
    },
    colorBox: function() {
      numb <= employee.profile.rating;
    }
  }
};
</script>


<style scoped>
h1,
h2 {
  font-weight: normal;
}

ul {
  list-style-type: none;
  padding: 0;
}

li {
  display: inline-block;
  margin: 0 10px;
}

a {
  color: #42b983;
}

.table-tittle {
  background: #f64a35;
}

.text-color {
  color: white;
}

.table-hover {
  border-color: lightgrey;
  border: lightgrey;
}

.level {
  color: gray;
  background: #f64a35;
  width: 100%;
  height: 100%;
}

.box-level {
  width: 20px;
  height: 20px;
}

.box {
  height: 100%;
  background: #f64a35;
}

.text-name {
  padding-top: 70px;
}

table > thead > tr > th {
  background-color: #f64a35;
}
</style>
