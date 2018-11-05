<template>
    <div>
        <h1 class="text-center login-padding-top">Send Message</h1>
        <hr />
        <b-form @submit.prevent="create" class="form-inline">
            <fieldset id="create-message-fieldset" class="col-md-12">

                <div class="form-group col-12 row justify-content-center">
                    <label class="col-12" for="content">Content</label>
                    <b-form-textarea :rows="5" class="form-control col-4" name="content" data-vv-as="Content" v-validate="'required'" v-model="createMessageViewModel.content" id="content">
                    </b-form-textarea>
                    <span v-show="errors.has('content')" class="text-danger col-12 text-center">{{ errors.first('content') }}</span>
                </div>

                <div class="form-group col-12 row justify-content-center">
                    <label class="col-12" for="messageState">Message State</label>
                    <select v-model="createMessageViewModel.state">
                        <option v-for="state in messageStates" v-bind:value="Number(state.value)">{{state.text}}</option>
                    </select>
                </div>

                <div class="form-group col-12 row justify-content-center">
                    <label class="col-12" for="ticket">Ticket</label>
                    <select v-model="createMessageViewModel.ticketId">
                        <option v-for="ticket in createMessageViewModel.tickets" v-bind:value="ticket.value">{{ticket.text}}</option>
                    </select>
                </div>

                <div class="form-group justify-content-center text-center col-12 row custom-margin-top">
                    <b-button id="button-send" type="submit" class="col-2" variant="secondary">Send</b-button>
                </div>
            </fieldset>
        </b-form>
    </div>
</template>

<script src="./index.ts"></script>