<template>
    <div>
        <h1 class="form-title">
            Register
        </h1>
        <hr />
        <b-form @submit.prevent="validateBeforeRegister" class="form-inline">
            <fieldset id="register-fieldset" class="col-12">
                <b-alert show dismissible variant="danger" fade v-if="hasError">{{error}}</b-alert>
                <div class="form-group col-12 row justify-content-center">
                    <label class="col-12" for="username">Username</label>
                    <b-form-input type="text" class="form-control col-4" name="username" data-vv-as="Username" v-validate="'required'" v-model="registerViewModel.username" id="username">
                    </b-form-input>
                    <span v-show="errors.has('username')" class="text-danger col-12 text-center">{{ errors.first('username') }}</span>
                </div>
                <br />
                <div class="form-group col-12 row justify-content-center">
                    <label class="col-12" for="name">Name</label>
                    <b-form-input type="text" class="form-control col-4" name="name" data-vv-as="Name" v-validate="'required'" v-model="registerViewModel.name" id="name">
                    </b-form-input>
                    <span v-show="errors.has('name')" class="text-danger col-12 text-center">{{ errors.first('name') }}</span>
                </div>
                <br />
                <div class="form-group col-12 row justify-content-center">
                    <label class="col-12" for="email">Email</label>
                    <b-form-input type="text" class="form-control col-4" name="email" data-vv-as="Email" v-validate="'required'" v-model="registerViewModel.email" id="email">
                    </b-form-input>
                    <span v-show="errors.has('email')" class="text-danger col-12 text-center">{{ errors.first('email') }}</span>
                </div>
                <br />
                <div class="form-group col-12 row justify-content-center">
                    <label class="col-12" for="password">Password</label>
                    <b-form-input type="password" class="form-control col-4" name="password" data-vv-as="Password" v-validate="'required'" v-model="registerViewModel.password" id="password">
                    </b-form-input>
                    <span v-show="errors.has('password')" class="text-danger col-12 text-center">{{ errors.first('password') }}</span>
                </div>
                <br />
                <div class="form-group col-12 row justify-content-center">
                    <label class="col-12" for="confirmPassword">Confirm password</label>
                    <b-form-input type="password" class="form-control col-4" name="confirmPassword" data-vv-as="ConfirmPassword" v-validate="'required'" v-model="registerViewModel.confirmPassword" id="confirmPassword">
                    </b-form-input>
                    <span v-show="errors.has('confirmPassword')" class="text-danger col-12 text-center">{{ errors.first('confirmPassword') }}</span>
                </div>
                <br />
                <div class="form-group justify-content-center text-center col-12 row custom-margin-top">
                    <b-button id="button-register" type="submit" class="col-2" variant="secondary">Register</b-button>
                </div>
            </fieldset>
        </b-form>
    </div>
</template>

<script src="./index.ts"></script>