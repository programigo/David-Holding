<template>
    <div>
        <h1 class="text-center login-padding-top">
            Create Project
        </h1>
        <hr />
        <b-form @submit.prevent="create" class="form-inline">
            <fieldset id="login-fieldset" class="col-12">

                <div class="form-group col-12 row justify-content-center">
                    <label class="col-12" for="name">Name</label>
                    <b-form-input type="text" class="form-control col-4" name="name" data-vv-as="Name" v-validate="'required'" v-model="addProjectViewModel.name" id="name">
                    </b-form-input>
                    <span v-show="errors.has('name')" class="text-danger col-12 text-center">{{ errors.first('name') }}</span>
                </div>
                <br />
                <div class="form-group col-12 row justify-content-center">
                    <label class="col-12" for="description">Description</label>
                    <b-form-textarea :rows="5" class="form-control col-4" name="description" data-vv-as="Description" v-validate="'required'" v-model="addProjectViewModel.description" id="description">
                    </b-form-textarea>
                    <span v-show="errors.has('description')" class="text-danger col-12 text-center">{{ errors.first('description') }}</span>
                </div>
                <br />
                <div class="form-group justify-content-center text-center col-12 row custom-margin-top">
                    <b-button id="button-create" type="submit" class="col-2" variant="secondary">Create</b-button>
                </div>
            </fieldset>
        </b-form>
    </div>
</template>

<script src="./index.ts"></script>