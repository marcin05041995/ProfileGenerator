<template>
  <div class="row">
    <div class="offset-lg-4 col-lg-4">

      <b-card-body class="pb-2">

        <b-form-group label="Imię">
          <b-input class="mb-1" v-model="employees.name"></b-input>
        </b-form-group>

        <b-form-group label="Nazwisko">
          <b-input class="mb-1" v-model="employees.lastName"></b-input>
        </b-form-group>

        <b-form-group label="Specjalizacja">
          <b-input class="mb-1" v-model="employees.specialization"></b-input>
        </b-form-group>

        <b-form-group label="Rating">
          <b-radio-group v-model="multipleModelRating" :options="optionsRating" />
        </b-form-group>

        <b-form-group label="Kadencja">
          <b-input class="mb-1" />
        </b-form-group>

        <b-form-group label="Projekt">
          <multiselect v-model="selectedproject" label="name" track-by="id" placeholder="Wybierz projekt" :options="projects" :multiple="true"
            :searchable="true" :internal-search="false" :clear-on-select="false" :close-on-select="false" :hide-selected="true">
            <span slot="noResult">No frameworks found.</span>
          </multiselect>
        </b-form-group>

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
                <b-radio-group v-model="multipleModelSpeaking" :options="optionsSpeaking" />

              </b-form-group>
            </div>

            <div class="row">
              <b-form-group label="Stopień w czytaniu">
                <b-radio-group v-model="multipleModelReading" :options="optionsReading" />
              </b-form-group>
            </div>

            <div class="row">
              <b-form-group label="Stopień w piśmie">
                <b-radio-group v-model="multipleModelWriting" :options="optionsWriting" />
              </b-form-group>
            </div>
          </div>
        </div>

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
          <b-radio-group v-model="multipleModelLevel" :options="optionsLevel" />
        </b-form-group>

        <button type="submit" class="btn  btn-outline" style=" background: #f64a35">Dodaj</button>
        <router-link to="/project">
          <button type="submit" class="btn btn-outline" style=" background: #f64a35">
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
import moment from "moment";
import Multiselect from "vue-multiselect";

export default {
  name: "Edit-EMployee",
  metaInfo: {
    title: "Edit Employee"
  },
  components: {
    Multiselect
  },
  data: () => ({
    employees: {
      name: "",
      lastName: ""
    },
    languages: [],
    skills: [],
    projects: [],
    errors: [],
    selectedskill: [],
    selectedlanguage: [],
    selectedproject: [],
    errors: [],
    alert: "",

    optionsLevel: [{text: "1",value: "1" }, {text: "2", value: "2"}, {text: "3", value: "3"}, {text: "4", value: "4"}, {text: "5", value: "5"} ],
    optionsRating: [{text: "1",value: "1" }, {text: "2", value: "2"}, {text: "3", value: "3"}, {text: "4", value: "4"}, {text: "5", value: "5"} ],
    optionsSpeaking: [{text: "1",value: "1" }, {text: "2", value: "2"}, {text: "3", value: "3"} ],
    optionsReading: [{text: "1",value: "1" }, {text: "2", value: "2"}, {text: "3", value: "3"} ],
    optionsWriting: [{text: "1",value: "1" }, {text: "2", value: "2"}, {text: "3", value: "3"} ],
    multipleModelRating: ["1"],
    multipleModelLevel: ["1"],
    multipleModelSpeaking: ["1"],
    multipleModelReading: ["1"],
    multipleModelWriting: ["1"],
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
  methods:{
    frontEndDateFormat: function(date) {
      return moment(date, "YYYY-MM-DD").format("DD/MM/YYYY");
    }
  }
};
</script>
