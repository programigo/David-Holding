<template>
	<div>
		<h1 v-if="$store.getters.sessionInfo.role === 'Administrator' || $store.getters.sessionInfo.role === 'Support'" class="form-title">All Tickets</h1>
		<h1 v-else class="form-title">My Tickets</h1>
		<tickets-list />
	</div>
</template>

<script src="./index.ts"></script>