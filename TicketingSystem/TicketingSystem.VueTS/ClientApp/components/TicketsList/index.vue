<template>
	<div>
		<div v-if="hasTickets">
			<div v-for="ticket in allTickets.tickets" class="row">
				<div>
					<b-card style="max-width: 25rem;" class="mb-2">
						<b-link :to="{ path: 'tickets/details/' + ticket.id }">
							<h3 style="text-align:center">{{ticket.title}}</h3>
						</b-link>
						<p style="text-align:center">Published on {{ticket.postTime}} by {{ticket.sender}}</p>
						<p style="text-align:center">Project: <b-link :to="{path: 'projects/details/' + ticket.projectId}">{{ticket.project}}</b-link></p>
						<hr />
						<p class="card-text">
							{{ticket.description}}
						</p>
						<hr />
						<div style="text-align:center">
							<b-button v-if="ticket.attachedFiles" v-on:click="downloadFile(ticket.id)" style="background-color:blue">Download Files</b-button>
							<b-button v-if="ticket.sender === $store.getters.sessionInfo.userName" :to="{ path: 'tickets/attachfiles/' + ticket.id }" style="background-color:blueviolet">Attach Files</b-button>
						</div>
					</b-card>
				</div>
			</div>
			<b-pagination-nav class="pagination-buttons" base-url="tickets#" :number-of-pages="allTickets.totalPages" v-model="currentPage" />
		</div>
		<p v-else style="text-align:center"><em>No active tickets.</em></p>
	</div>
</template>

<script src="./index.ts"></script>