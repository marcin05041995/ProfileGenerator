<template>
  <div class="row">
    <div class="col-lg-4"></div>
    <div class="col-lg-4">

      <b-card-body class="pb-2">

        <b-form-group label="Imię">
          <b-input class="mb-1" v-model="inputEmployeeModel.name" />
        </b-form-group>

        <b-form-group label="Nazwisko">
          <b-input class="mb-1" v-model="inputEmployeeModel.lastName" />
        </b-form-group>

        <b-form-group label="Specjalizacja">
          <b-input class="mb-1"  v-model="inputEmployeeModel.specialization"/>
        </b-form-group>

        <!-- <b-form-group label="Ocena">
        <b-input class="mb-1" />
      </b-form-group> -->
        <b-form-group label="Rating">
          <b-radio-group v-model="multipleModel" :options="options3" />
        </b-form-group>

        <b-form-group label="Kadencja">
          <b-input class="mb-1" v-model="inputEmployeeModel.yearsOfWork" />
        </b-form-group>

        <!-- <b-form-group label="Projekt">
          <b-select v-model="selectedproject">
            <option v-for=" proj in projects">
              {{proj.name}}
            </option>
          </b-select>
        </b-form-group> -->
        <b-form-group label="Projekt">
          <multiselect v-model="selectedproject" label="name" track-by="id" placeholder="Wybierz projekt" :options="projects"
            :multiple="true" :searchable="true" :internal-search="false" :clear-on-select="false" :close-on-select="false"
            :hide-selected="true">
            <span slot="noResult">No frameworks found.</span>
          </multiselect>
        </b-form-group>

        <!-- <b-form-group label="Język" v-if="languages.length">
          <b-select v-model="selectedlanguage">
            <option v-for=" lang in languages">
              {{lang.languageName}}
            </option>
          </b-select>
        </b-form-group> -->

        <b-form-group label="Język">
          <multiselect v-model="selectedlanguage" label="languageName" track-by="id" placeholder="Wybierz język" :options="languages"
            :multiple="true" :searchable="true" :internal-search="false" :clear-on-select="false" :close-on-select="false"
            :hide-selected="true">
            <span slot="noResult">No frameworks found.</span>
          </multiselect>
        </b-form-group>


        <div class="row" v-for="language in selectedlanguage" :key="language.id">
          <div class="col-md-3">{{language.languageName}}</div>
          <div class="col-md-9">

            <div class="row">
              <b-form-group label="Stopień w mowie">
                <b-radio-group v-model="language.speak" :options="options" />
                <!-- <fieldset disabled>
            <b-radio-group v-model="singleModel" :options="options" />
          </fieldset> -->
              </b-form-group>
            </div>

            <div class="row">
              <b-form-group label="Stopień w czytaniu">
                <b-radio-group v-model="language.read" :options="options" />
              </b-form-group>
            </div>

            <div class="row">
              <b-form-group label="Stopień w piśmie">
                <b-radio-group v-model="language.write" :options="options" />
              </b-form-group>
            </div>
          </div>
        </div>

<!-- <b-form-group label="Umiejętności">
          <b-select v-model="selectedskill">
            <option v-for=" skil in skills">
              {{skil.skillName}}
            </option>
          </b-select>
        </b-form-group> -->


<b-form-group label="Umiejętności">
          <multiselect v-model="selectedskill" label="skillName" track-by="id" placeholder="Wybierz umiejętność" :options="skills"
            :multiple="true" :searchable="true" :internal-search="false" :clear-on-select="false" :close-on-select="false"
            :hide-selected="true">
            <span slot="noResult">No frameworks found.</span>
          </multiselect>
        </b-form-group>

        <b-form-group label="Doświadczenie w latach">
          <b-input class="mb-1" />
        </b-form-group>

        <b-form-group label="Stopień">
          <b-radio-group v-model="multipleModel2" :options="options2" />
        </b-form-group>

        <button type="submit" class="btn btn-primary btn-outline" v-on:click="addEmployee">Dodaj</button>
        <router-link to="/project">

          <button type="submit" class="btn btn-primary btn-outline">
            Wróć</button>

        </router-link>
      </b-card-body>
      <div class="col-lg-4"></div>
    </div>
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
  name: "Add-EMployee",
  metaInfo: {
    title: "Add Employee"
  },
  components: {
    Multiselect
  },
  data: () => ({
    inputEmployeeModel: {
      name: "",
      lastName: "",
      specialization: "",
      rating: 0,
      yearsOfWork: ""
    },
    employees: [],
    languages: [],
    skills: [],
    projects: [],
    errors: [],
    selectedskill: [],
    selectedlanguage: [],
    selectedproject: [],
    errors: [],
    alert: "",
    options2: [
      {
        text: "1",
        value: "1"
      },
      {
        text: "2",
        value: "2"
      },
      {
        text: "3",
        value: "3"
      },
      {
        text: "4",
        value: "4"
      },
      {
        text: "5",
        value: "5"
      }
    ],
    options3: [
      {
        text: "1",
        value: "1"
      },
      {
        text: "2",
        value: "2"
      },
      {
        text: "3",
        value: "3"
      },
      {
        text: "4",
        value: "4"
      },
      {
        text: "5",
        value: "5"
      }
    ],
    options: [
      {
        text: "1",
        value: "1"
      },
      {
        text: "2",
        value: "2"
      },
      {
        text: "3",
        value: "3"
      }
    ],
    multipleModel: ["1"],
    multipleModel2: ["1"],
    file: null
  }),
  created() {
    axios
      .get("http://localhost:4444/api/languages/GetLanguages")
      .then(response => {
        this.languages = response.data;
      })
      .catch(e => {
        this.errors.push(e);
      });

    axios
      .get("http://localhost:4444/api/skills/GetSkills")
      .then(response => {
        this.skills = response.data;
      })
      .catch(e => {
        this.errors.push(e);
      });
    axios
      .get("http://localhost:4444/api/projects/GetProjects")
      .then(response => {
        this.projects = response.data;
      })
      .catch(e => {
        this.errors.push(e);
      });
  },
    methods: {
        addEmployee() {
          axios
            .post("http://localhost:4444/api/projects/AddEmployee", this.inputEmployeeModel)
            .then(response => {
              console.log("response "+response.data)
            });
            // axios
            // .post("http://localhost:4444/api/projects/AddEmployeeToProject", {
            //   body: this.projects
            // })
            // .then(response => {});
            // axios
            // .post("http://localhost:4444/api/projects/AddTechnologyToProject", {
            //   body: this.projects
            // })
            // .then(response => {});
          },
    }
};
</script>
