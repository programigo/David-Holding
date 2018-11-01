<template>
    <div>
        <h3 style="text-align:center">Attach file</h3>
        <b-form @submit.prevent="" enctype="multipart/form-data">
            <input type="file" />
            <b-button type="submit" class="btn-secondary" @click="sayhi()">Attach File</b-button>
        </b-form>
    </div>
</template>

<script src="./index.ts"></script>