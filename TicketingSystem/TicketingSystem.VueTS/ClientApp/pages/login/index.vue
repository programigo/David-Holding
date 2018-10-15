<template>
    <div>
        <h1 class="text-center login-padding-top">
            {{$t('login')}}
        </h1>
        <hr />
        <b-form @submit.prevent="validateBeforeLogin" class="form-inline">
            <fieldset id="login-fieldset" class="col-12">
                <div class="form-group col-12 row justify-content-center">
                    <label class="col-12" for="username">{{$t('userName')}}</label>
                    <b-form-input type="text" class="form-control col-4" name="username" data-vv-as="Username" v-validate="'required'" v-model="logOnViewModel.username" id="username">
                    </b-form-input>
                    <span v-show="errors.has('username')" class="text-danger col-12 text-center">{{ errors.first('username') }}</span>
                </div>
                <div class="form-group col-12 row justify-content-center">
                    <label class="col-12" for="password">{{$t('password')}}</label>
                    <b-form-input type="password" class="form-control col-4" name="password" data-vv-as="Password" v-validate="'required'" v-model="logOnViewModel.password" id="password">
                    </b-form-input>
                    <span v-show="errors.has('password')" class="text-danger col-12 text-center">{{ errors.first('password') }}</span>
                </div>
                <div class="form-group justify-content-center text-center col-12 row custom-margin-top">
                    <b-button type="submit" class="col-2" variant="secondary">{{$t('login')}}</b-button>
                </div>
            </fieldset>
        </b-form>
    </div>
</template>

<script src="./index.ts"></script>