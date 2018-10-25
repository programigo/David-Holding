import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import ProjectsList from '../../components/ProjectsList';

@Component({
    components: {
        ProjectsList
    }
})

export default class Home extends Vue {
    private get isLoggedIn(): boolean {
        return this.$store.getters.isLoggedIn;
    }

    public async created(): Promise<void> {
        if (!this.isLoggedIn) {
            this.$router.push('/login');
        }
    }
}