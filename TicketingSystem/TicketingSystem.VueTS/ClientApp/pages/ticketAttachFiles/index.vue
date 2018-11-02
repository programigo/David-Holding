<template>
    <div>
        <h3 style="text-align:center">Attach file</h3>
        <b-form @submit.prevent="" enctype="multipart/form-data">
            <input type="file" @change="fileChange($event.target.files)" accept="application/zip" />
            <b-button type="submit" class="btn-secondary" @click="startUpload()">Attach File</b-button>
        </b-form>
    </div>
</template>

<script src="./index.ts"></script>