import Vue from "vue";
import Component from "vue-class-component";
import axios from 'axios';

import VeeValidate from 'vee-validate';

Vue.use(VeeValidate);

@Component

export default class MessageAttachFiles extends Vue {
    attachFileModel: FormData = new FormData();

    private get id(): number {
        return Number(this.$route.params.messageId);
    }

    private startUpload(): void {
        axios.post(`/api/messages/attachfiles/${this.id}`,
            this.attachFileModel,
            {
                headers: {
                    'Content-Type': 'multipart/form-data'
                }
            });

        this.$router.push('/tickets');
    }

    private fileChange(fileList: any): void {
        for (let i = 0; i < fileList.length; i++) {
            this.attachFileModel.append("file", fileList[i], fileList[i].name);
        }
    }
}