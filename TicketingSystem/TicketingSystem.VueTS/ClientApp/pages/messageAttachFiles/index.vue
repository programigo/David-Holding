<template>
	<div>
		<h3 class="form-title">Attach file</h3>
		<b-form @submit.prevent="validateBeforeUpload" enctype="multipart/form-data">
			<b-alert show dismissible variant="danger" fade v-if="hasError">{{error}}</b-alert>
			<input type="file" @change="fileChange($event.target.files)" accept="application/zip" />
			<b-button type="submit" class="btn-secondary" @click="startUpload()">Attach File</b-button>
		</b-form>
	</div>
</template>

<script src="./index.ts"></script>