<template>
    <div class="full">
        <v-row class="row">
            <v-col class="col">
                <h3>Matching Result Positions</h3>
            </v-col>
        </v-row>
        <ul>
            <li v-for="result in matchingResults" :key="result.rank">
                <span class="result">{{result.rank}}</span> : <span class="result-title"><a target="_blank" :href="result.link.uri">{{result.link.title}}</a></span>
            </li>
        </ul>
    </div>
</template>

<script>
export default {
    name: 'SearchResults',
    props: ['p_results'],
    computed: {
        matchingResults: function(){
            return this.p_results.filter(res => res.isMatch);
        }
    }
}
</script>

<style scoped>

h3 {
    width: 100%;
    text-align: center;
    font-size: 2rem;
}

.full {
    width: 100%;
}

li {
    list-style: none;
    width: 100%;
    text-align: center;
}

.result {
    font-size: 1.5rem;
    font-weight: 350;
}

</style>