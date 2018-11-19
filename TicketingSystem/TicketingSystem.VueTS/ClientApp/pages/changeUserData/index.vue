<template>
	<div>
		<h1 class="form-title">
			User {{changeDataViewModel.username}}
		</h1>
		<hr />
		<b-form @submit.prevent="changeUserData" class="form-inline">
			<fieldset id="login-fieldset" class="col-12">

				<div class="form-group col-12 row justify-content-center">
					<label class="col-12" for="name">Name</label>
					<b-form-input type="text" class="form-control col-4" name="name" data-vv-as="Name" v-validate="'required'" v-model="changeDataViewModel.name" id="name">
					</b-form-input>
					<span v-show="errors.has('name')" class="text-danger col-12 text-center">{{ errors.first('name') }}</span>
				</div>
				<br />
				<div class="form-group col-12 row justify-content-center">
					<label class="col-12" for="email">Email</label>
					<b-form-input type="text" class="form-control col-4" name="email" data-vv-as="Email" v-validate="'required'" v-model="changeDataViewModel.email" id="email">
					</b-form-input>
					<span v-show="errors.has('email')" class="text-danger col-12 text-center">{{ errors.first('email') }}</span>
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