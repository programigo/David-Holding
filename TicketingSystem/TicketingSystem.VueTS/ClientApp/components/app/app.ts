import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import TheNavMenu from '../../components/TheNavMenu';
import * as api from '../../api';

@Component({
    components: {
        //MenuComponent: require('../navmenu/navmenu.vue'),
        TheNavMenu: require('../../components/TheNavMenu/index.vue')
    }
})
export default class AppComponent extends Vue {
    public async beforeCreate(): Promise<void> {
        await api.account.isLoggedOn();
    }
}
