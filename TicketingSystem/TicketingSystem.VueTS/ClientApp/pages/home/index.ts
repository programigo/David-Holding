﻿import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import ProjectsList from '../../components/ProjectsList';

@Component({
	components: {
		ProjectsList
	}
})

export default class Home extends Vue {
	created() {
		this.$router.push('/projects');
	}
}