import Vue from 'vue';
import { Component } from 'vue-property-decorator';

@Component({
    components: {
        MenuComponent: require('../Sidebar/index')
    }
})
export default class AppComponent extends Vue {
}
