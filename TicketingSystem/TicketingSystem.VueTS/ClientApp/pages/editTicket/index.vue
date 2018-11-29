<template>
	<div>
		<h1 class="form-title">
			Edit Ticket
		</h1>
		<hr />
		<b-form @submit.prevent="editTicket" class="form-inline">
			<fieldset id="login-fieldset" class="col-12">

				<div class="form-group col-12 row justify-content-center">
					<label class="col-12" for="title">Title</label>
					<b-form-input type="text" class="form-control col-4" name="name" data-vv-as="Title" v-model="editTicketModel.title" id="title">
					</b-form-input>
				</div>
				<br />
				<div class="form-group col-12 row justify-content-center">
					<label class="col-12" for="description">Description</label>
					<b-form-textarea :rows="5" class="form-control col-4" name="description" data-vv-as="Description" v-model="editTicketModel.description" id="description">
					</b-form-textarea>
				</div>
				<br />
				<div class="form-group col-12 row justify-content-center">
					<label class="col-12" for="ticketType">Ticket Type</label>
					<select v-model="editTicketModel.ticketType">
						<option v-for="type in ticketTypes" v-bind:value="Number(type.value)">{{type.text}}</option>
					</select>
				</div>
				<br />
				<div class="form-group col-12 row justify-content-center">
					<label class="col-12" for="ticketState">Ticket State</label>
					<select v-model="editTicketModel.ticketState">
						<option v-for="state in ticketStates" v-bind:value="Number(state.value)">{{state.text}}</option>
					</select>
				</div>
				<br />
				<div class="form-group justify-content-center text-center col-12 row custom-margin-top">
					<b-button id="button-edit" type="submit" class="col-2" variant="secondary">Edit</b-button>
				</div>
			</fieldset>
		</b-form>
	</div>
</template>

<script src="./index.ts"></script>