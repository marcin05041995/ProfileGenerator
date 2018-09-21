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
        <b-input class="mb-1" v-model="project.name" />
      </b-form-group>

      <b-form-group label="Tytuł">
        <b-input class="mb-1" v-model="project.title" />
      </b-form-group>

      <b-form-group label="Klient Sektor">
        <b-input class="mb-1" v-model="project.clientSector" />
      </b-form-group>

      <b-form-group label="Technologie">
          <multiselect v-model="selectedtechnologies" label="technologyName" track-by="id" placeholder="Wybierz technologie" :options="technologies"
            :multiple="true" :searchable="true" :internal-search="false" :clear-on-select="false" :close-on-select="false"
            :hide-selected="true">
            <span slot="noResult">No frameworks found.</span>
          </multiselect>
        </b-form-group>

      <b-form-group label="Data rozpoczęcia projektu">
        <b-input class="mb-1" v-model="project.startDate "></b-input>
      </b-form-group>
      <b-form-group label="Data zakończenia projektu">
        <b-input class="mb-1" v-model="project.endDate"> </b-input>
      </b-form-group>

        <b-form-group label="Pracownik">
          <multiselect v-model="selectedemployee" label="fullname"  track-by="id" placeholder="Wybierz pracownika" :options="employees"
            :multiple="true" :searchable="true" :internal-search="false" :clear-on-select="false" :close-on-select="false"
            :hide-selected="true">
            <span slot="noResult">No frameworks found.</span>
          </multiselect>
        </b-form-group>




        <button type="submit" class="btn btn-primary btn-outline">Dodaj</button>
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
import moment from "moment";
import Multiselect from "vue-multiselect";

export default {
  // name: "Edit-Project",
  metaInfo: {
    title: "Edit Project"
  },
  components: {
    Multiselect
  },
  data: () => ({
    tableData: [],
    project: {
      name: "",
      title: "",
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
    var id = this.getUrlParameter("id");
    axios
      .get(`http://localhost:4444/api/projects/GetProject?id=` + id)
      .then(response => {
        this.project.name = response.data.name;
        this.project.title = response.data.title;
        this.project.clientSector = response.data.clientSector;
        this.project.startDate = this.frontEndDateFormat(response.data.startDate);
        this.project.endDate = this.frontEndDateFormat(response.data.endDate);

        for (let index = 0; index < response.data.profiles.length; index++) {
          response.data.profiles[index].fullname =
            response.data.profiles[index].name +
            " " +
            response.data.profiles[index].lastName;
        }

        this.selectedemployee = response.data.profiles;

        for (let index = 0; index < response.data.technology.length; index++) {
          response.data.technology[index].technologyName =
            response.data.technology[index].name;
        }
        this.selectedtechnologies = response.data.technology;
      })
      .catch(e => {
        this.errors.push(e);
      });
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
    frontEndDateFormat: function(date) {
      return moment(date, "YYYY-MM-DD").format("DD/MM/YYYY");
    },
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
    editProject() {
          axios
            .post("http://localhost:4444/api/projects/EditProject", this.project)
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
