import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import TheNavMenu from './components/TheNavMenu';
import * as actions from './store/actions';
import * as api from './api';

@Component({
	components: {
		TheNavMenu
	}
})

export default class App extends Vue {
	public async beforeCreate(): Promise<void> {

		await api.account.isLoggedOn();
	}

	hideModal() {
		this.$root.$emit('bv::hide::modal', 'error-modal')
	}
}