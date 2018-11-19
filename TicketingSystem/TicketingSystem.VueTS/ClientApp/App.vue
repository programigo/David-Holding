<template>
	<div id='app-root'>
		<the-nav-menu />
		<main role="main" class="container">

			<router-view />
		</main>
	</div>
</template>

<style lang="scss">
	@import "./styles/custom-styles.css";
	@import "./styles/bootstrap";
</style>

<script src="./App.ts"></script>