<template>
	<div>
		<h1 class="form-title">
			Change Password
		</h1>
		<hr />
		<b-form @submit.prevent="changePassword" class="form-inline">
			<fieldset id="change-password-fieldset" class="col-12">
				<b-alert show dismissible variant="danger" fade v-if="hasError">{{error}}</b-alert>
				<div class="form-group col-12 row justify-content-center">
					<label class="col-12" for="oldPassword">Old Password</label>
					<b-form-input type="password" class="form-control col-4" name="oldPassword" data-vv-as="Old Password" v-validate="'required'" v-model="changePasswordViewModel.oldPassword" id="oldPassword">
					</b-form-input>
					<span v-show="errors.has('oldPassword')" class="text-danger col-12 text-center">{{ errors.first('oldPassword') }}</span>
				</div>
				<br />
				<div class="form-group col-12 row justify-content-center">
					<label class="col-12" for="newPassword">New Password</label>
					<b-form-input type="password" class="form-control col-4" name="newPassword" data-vv-as="New Password" v-validate="'required'" v-model="changePasswordViewModel.newPassword" id="newPassword">
					</b-form-input>
					<span v-show="errors.has('newPassword')" class="text-danger col-12 text-center">{{ errors.first('newPassword') }}</span>
				</div>
				<br />
				<div class="form-group col-12 row justify-content-center">
					<label class="col-12" for="confirmPassword">Confirm Password</label>
					<b-form-input type="password" class="form-control col-4" name="confirmPassword" data-vv-as="Confirm Password" v-validate="'required'" v-model="changePasswordViewModel.confirmPassword" id="confirmPassword">
					</b-form-input>
					<span v-show="errors.has('confirmPassword')" class="text-danger col-12 text-center">{{ errors.first('confirmPassword') }}</span>
				</div>
				<br />
				<div class="form-group justify-content-center text-center col-12 row custom-margin-top">
					<b-button id="button-login" type="submit" class="col-2" variant="secondary">Change</b-button>
				</div>
			</fieldset>
		</b-form>
	</div>
</template>

<script src="./index.ts"></script>