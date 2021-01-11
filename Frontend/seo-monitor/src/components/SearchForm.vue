<template>
  <v-container>
    <v-alert
      dismissible
      type="error"
      v-model="didSearchFail"
    >
      Error retrieving search results
    </v-alert>
    <v-form>
      <v-row class="mb-4">
        <v-text-field
          label="Search Text"
          v-model="searchText"
          required>
        </v-text-field>
      </v-row>
      <v-row class="mb-4">
        <v-text-field
          label="Target Website"
          v-model="targetWebsite"
          required>
        </v-text-field>
      </v-row>
      <v-row class="mb-4">
        <v-btn
          type="submit"
          @click.prevent="getSearchResults"
          class="submit-button"
          color="primary"
        >Search</v-btn>
      </v-row>
    </v-form>
    <v-row>
      <v-col class="text-center">
        <div v-if="isSearchLoading">
          <LoadingSpinner />
        </div>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import axios from 'axios';
import config from '../config/config';
import LoadingSpinner from './LoadingSpinner';

  export default {
    name: 'SearchForm',
    props:['p_searchRankingsResponseHandler'],
    components: {
      LoadingSpinner
    },
    data: () => ({
      searchText: 'efiling integration',
      targetWebsite: 'www.infotrack.com',
      didSearchFail: false,
      isSearchLoading: false
    }),
    methods: {
      getSearchResults() {
        this.isSearchLoading = true;
        axios.get(config.seoMonitorBaseAddress + '/SearchRankings', {
          params: {
            searchText: this.searchText,
            website: this.targetWebsite
          }
        }).then(res => {
          this.didSearchFail = false;
          this.isSearchLoading = false;
          this.p_searchRankingsResponseHandler(res.data);
        })
        .catch(() => {
          this.isSearchLoading = false;
          this.didSearchFail = true;
        });
      }
    }
  }
</script>

<style scoped>
.submit-button{
  width: 100%;
}
</style>