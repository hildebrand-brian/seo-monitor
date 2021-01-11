<template>
  <v-app>
    <v-main>
      <v-container class="app-container">
        <v-row>
            <v-col>
              <h1 class="text-center">SEO Monitor</h1>
            </v-col>
        </v-row>
        
        <v-row>
          <v-col>
            <SearchForm :p_searchRankingsResponseHandler="handleSearchRankingsResults"/>
          </v-col>
        </v-row>
        <v-row v-if="rankings">
          <SearchResults :p_results="rankings" />
        </v-row>
      </v-container>
    </v-main>
  </v-app>
</template>

<script>
import SearchForm from './components/SearchForm';
import SearchResults from './components/SearchResults';

export default {
  name: 'App',

  components: {
    SearchForm,
    SearchResults
  },

  data: () => ({
    rankings: null
  }),
  methods: {
    handleSearchRankingsResults(res) {
      this.rankings = res;
    }
  }
};
</script>

<style scoped>
.app-container {
  max-width: 512px;  
}
</style>