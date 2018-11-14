<template>
    <div>
        <h1 v-if="$store.getters.sessionInfo.role === 'Administrator' || $store.getters.sessionInfo.role === 'Support'" style="text-align:center">All Tickets</h1>
        <h1 v-else style="text-align:center">My Tickets</h1>
        <tickets-list />
    </div>
</template>

<script src="./index.ts"></script>