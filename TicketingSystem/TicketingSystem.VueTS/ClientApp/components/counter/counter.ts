import Vue from 'vue';
import { Component } from 'vue-property-decorator';

@Component
export default class CounterComponent extends Vue {
    currentcount: number = 1;

    incrementCounter() {
        this.currentcount++;
    }
}
