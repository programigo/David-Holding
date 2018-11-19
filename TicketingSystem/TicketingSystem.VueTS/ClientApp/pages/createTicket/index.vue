<template>
	<div>
		<h1 class="form-title">
			Create Ticket
		</h1>
		<hr />
		<b-form @submit.prevent="validateBeforeCreate" class="form-inline">
			<fieldset id="create-ticket-fieldset" class="col-12">
				<b-alert show dismissible variant="danger" fade v-if="hasError">{{error}}</b-alert>
				<div class="form-group col-12 row justify-content-center">
					<label class="col-12" for="title">Title</label>
					<b-form-input type="text" class="form-control col-4" name="title" data-vv-as="Title" v-validate="'required'" v-model="createTicketViewModel.title" id="title">
					</b-form-input>
					<span v-show="errors.has('title')" class="text-danger col-12 text-center">{{ errors.first('title') }}</span>
				</div>
				<br />
				<div class="form-group col-12 row justify-content-center">
					<label class="col-12" for="description">Description</label>
					<b-form-textarea :rows="5" class="form-control col-4" name="description" data-vv-as="Description" v-validate="'required'" v-model="createTicketViewModel.description" id="description">
					</b-form-textarea>
					<span v-show="errors.has('description')" class="text-danger col-12 text-center">{{ errors.first('description') }}</span>
				</div>
				<br />
				<div class="form-group col-12 row justify-content-center">
					<label class="col-12" for="ticketType">Ticket Type</label>
					<select v-model="createTicketViewModel.ticketType" required>
						<option v-for="type in ticketTypes" v-bind:value="Number(type.value)">{{type.text}}</option>
					</select>
					<span v-show="errors.has('ticketType')" class="text-danger col-12 text-center">{{ errors.first('ticketType') }}</span>
				</div>
				<br />
				<div v-if="userRole === 'Administrator' || userRole === 'Support'" class="form-group col-12 row justify-content-center">
					<label class="col-12" for="ticketState">Ticket State</label>
					<select v-model="createTicketViewModel.ticketState" required>
						<option v-for="state in ticketStates" v-bind:value="Number(state.value)">{{state.text}}</option>
					</select>
					<span v-show="errors.has('ticketState')" class="text-danger col-12 text-center">{{ errors.first('ticketState') }}</span>
				</div>
				<br />
				<div class="form-group col-12 row justify-content-center">
					<label class="col-12" for="project">Project</label>
					<select v-model="createTicketViewModel.projectId" required>
						<option v-for="project in createTicketViewModel.projects" v-bind:value="project.value">{{project.text}}</option>
					</select>
					<span v-show="errors.has('project')" class="text-danger col-12 text-center">{{ errors.first('project') }}</span>
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