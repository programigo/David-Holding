<template>
    <div>
        <h1 class="form-title">
            Log in
        </h1>
        <hr />
        <b-form @submit.prevent="validateBeforeLogin" class="form-inline">
            <fieldset id="login-fieldset" class="col-12">
                <b-alert show dismissible variant="danger" fade v-if="hasError">{{error}}</b-alert>
                <div class="form-group col-12 row justify-content-center">
                    <label class="col-12" for="username">Username</label>
                    <b-form-input type="text" class="form-control col-4" name="username" data-vv-as="Username" v-validate="'required'" v-model="loginViewModel.username" id="username">
                    </b-form-input>
                    <span v-show="errors.has('username')" class="text-danger col-12 text-center">{{ errors.first('username') }}</span>
                </div>
                <br />
                <div class="form-group col-12 row justify-content-center">
                    <label class="col-12" for="password">Password</label>
                    <b-form-input type="password" class="form-control col-4" name="password" data-vv-as="Password" v-validate="'required'" v-model="loginViewModel.password" id="password">
                    </b-form-input>
                    <span v-show="errors.has('password')" class="text-danger col-12 text-center">{{ errors.first('password') }}</span>
                </div>
                <br />
                <div class="form-group justify-content-center text-center col-12 row custom-margin-top">
                    <b-button id="button-login" type="submit" class="col-2" variant="secondary">Login</b-button>
                </div>
            </fieldset>
        </b-form>
        <hr />
            <h6 style="text-align:center">Don't have an account? <b-link to="/register">Register here</b-link></h6> 
    </div>
</template>

<script src="./index.ts"></script>