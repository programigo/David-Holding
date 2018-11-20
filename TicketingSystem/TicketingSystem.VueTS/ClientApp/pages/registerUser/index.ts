import Vue from "vue";
import Component from "vue-class-component";
import RegisterForm from '../../components/RegisterForm';

import VeeValidate from 'vee-validate';

Vue.use(VeeValidate);

@Component({
	components: {
		RegisterForm
	}
})

export default class RegisterUser extends Vue {
	
}