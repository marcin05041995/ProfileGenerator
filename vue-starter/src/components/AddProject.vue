<template>
  <div class="addproject">
    <bt/>
    <h1></h1>
    <h2></h2>
    <div class="col-lg-4"></div>
    <div class="col-lg-4">
      <form>

        <b-card-body class="pb-2">



      <b-form-group label="Nazwa">
        <b-input class="mb-1"  v-model="inputProjectModel.name" />
      </b-form-group>

            <b-form-group label="Opis">
        <b-input class="mb-1"  v-model="inputProjectModel.description" />
      </b-form-group>

      <b-form-group label="Klient Sektor" >
        <b-input class="mb-1"  v-model="inputProjectModel.clientSector" />
      </b-form-group>

      <!-- <b-form-group label="Technologie">
        <b-select v-model="selectedtechnologies">
          <option v-for="techno in technologies">
            {{techno.technologyName}}
          </option>
        </b-select>
      </b-form-group> -->

      <b-form-group label="Technologie">
          <multiselect v-model="selectedtechnologies" label="technologyName" track-by="id" placeholder="Wybierz technologie" :options="technologies"
            :multiple="true" :searchable="true" :internal-search="false" :clear-on-select="false" :close-on-select="false"
            :hide-selected="true">
            <span slot="noResult">No frameworks found.</span>
          </multiselect>
        </b-form-group>

      <b-form-group label="Data rozpoczęcia projektu">
        <b-input class="mb-1"  v-model="inputProjectModel.startDate" />
      </b-form-group>

      <b-form-group label="Data zakończenia projektu">
        <b-input class="mb-1"  v-model="inputProjectModel.endDate" />
      </b-form-group>

        <!-- <b-form-group label="Pracownik">
        <b-select v-model="selectedemployee">
          <option v-for="emp in employees">
            {{ emp.profile.name }} {{ emp.profile.lastName }}
          </option>
        </b-select>
      </b-form-group> -->

<b-form-group label="Pracownik">
          <multiselect v-model="selectedemployee" label="fullname"  track-by="id" placeholder="Wybierz pracownika" :options="employees"
            :multiple="true" :searchable="true" :internal-search="false" :clear-on-select="false" :close-on-select="false"
            :hide-selected="true">
            <span slot="noResult">No frameworks found.</span>
          </multiselect>
        </b-form-group>



        <button type="submit" class="btn btn-primary btn-outline" v-on:click.prevent="addProject">Dodaj</button>
        <router-link to="/project">
          <button type="submit" class="btn btn-primary btn-outline">
            Wróć</button>
        </router-link>
        </b-card-body>
      </form>
    </div>
    <div class="col-lg-4"></div>
  </div>

</template>

<style src="vue-multiselect/dist/vue-multiselect.min.css"></style>
<style src="@/vendor/libs/vue-multiselect/vue-multiselect.scss" lang="scss"></style>

<!-- Page -->
<style src="@/vendor/styles/pages/users.scss" lang="scss"></style>

<script>
import axios from "axios";
import Vue from "vue";

import Multiselect from "vue-multiselect";

export default {
  name: "Add-Project",
  metaInfo: {
    title: "Add Project"
  },
  components: {
    Multiselect
  },
  data: () => ({
    inputProjectModel: {
      name: "",
      description: "",
      clientSector: "",
      startDate: "",
      endDate: ""
    },
    employees: [],
    technologies: [],
    errors: [],
    selectedtechnologies: [],
    selectedemployee: [],
    errors: [],
    alert: "",
    file: null
  }),
  created() {
    axios
      .get("http://localhost:4444/api/technologies/GetTechnologies")
      .then(response => {
        this.technologies = response.data;
      })
      .catch(e => {
        this.errors.push(e);
      });
    axios
      .get("http://localhost:4444/api/employees/GetEmployees")
      .then(response => {
        for (let index = 0; index < response.data.length; index++) {
          response.data[index].fullname =
            response.data[index].name + " " + response.data[index].lastName;
        }
        this.employees = response.data;
      })
      .catch(e => {
        this.errors.push(e);
      });
  },
  methods: {
        addProject() {
          axios
            .post("http://localhost:4444/api/projects/AddProject", this.inputProjectModel)
            .then(response => {
              console.log("response "+response.data)
            });
            axios
             .post("http://localhost:4444/api/projects/AddEmployeeToProject", this.selectedemployee)
               .then(response => {
              console.log("response "+response.data)
            });
             axios
             .post("http://localhost:4444/api/projects/AddTechnologyToProject", this.selectedtechnologies)
               .then(response => {
              console.log("response "+response.data)
            });

    }
  }
}
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
</style>
