<template>
    <div id='app-root'>
        <the-sidebar />
        <main role="main" class="container">
            <b-modal id="error-modal" size="lg" hide-footer hide-header>
                <button id="error-modal-button" type="button" class="close" data-dismiss="alert" aria-label="Close" @click="hideModal">
                    <span aria-hidden="true">&times;</span>
                </button>
                <div class="d-block">
                    <strong id="error-modal-title"></strong>
                    <div id="error-modal-message"></div>
                </div>
            </b-modal>
            
        </main>
    </div>
</template>

<script src="./App.ts"></script>