import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import Sidebar from './components/Sidebar';
import * as api from './api';

@Component({
    components: {
        Sidebar
    }
})

export default class App extends Vue {
    public async beforeCreate(): Promise<void> {
        await api.account.isLoggedOn()
    }

    hideModal() {
		this.$root.$emit('bv::hide::modal', 'error-modal')
	}
}