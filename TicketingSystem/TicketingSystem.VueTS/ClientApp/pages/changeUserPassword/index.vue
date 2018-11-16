<template>
    <div>
        <h1 class="form-title">
            Change Password
        </h1>
        <hr />
        <b-form @submit.prevent="changeUserPassword" class="form-inline">
            <fieldset id="login-fieldset" class="col-12">

                <div class="form-group col-12 row justify-content-center">
                    <label class="col-12" for="new-password">New Password</label>
                    <b-form-input type="password" class="form-control col-4" name="password" data-vv-as="Password" v-validate="'required'" v-model="changePasswordViewModel.newPassword" id="new-password">
                    </b-form-input>
                    <span v-show="errors.has('new-password')" class="text-danger col-12 text-center">{{ errors.first('new-password') }}</span>
                </div>
                <br />
                <div class="form-group justify-content-center text-center col-12 row custom-margin-top">
                    <b-button id="button-edit" type="submit" class="col-2" variant="secondary">Change</b-button>
                </div>
            </fieldset>
        </b-form>
    </div>
</template>

<script src="./index.ts"></script>