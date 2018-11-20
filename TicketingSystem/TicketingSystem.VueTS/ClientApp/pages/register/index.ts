import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import RegisterForm from '../../components/RegisterForm';

import VeeValidate from 'vee-validate';

Vue.use(VeeValidate);

@Component({
	components: {
		RegisterForm
	}
})

export default class Register extends Vue {
	
}