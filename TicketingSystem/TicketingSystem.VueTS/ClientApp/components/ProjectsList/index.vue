﻿<template>
	<div>
		<div v-if="hasProjects">
			<div v-for="project in renderProjects.projects">
				<div>
					<b-card style="max-width: 25rem;" class="mb-2">
						<b-link :to="{ path: 'projects/details/' + project.id }">
							<h3 style="text-align:center">{{project.name}}</h3>
						</b-link>
						<hr />
						<p class="card-text" style="text-align:center">
							{{project.description}}
						</p>
					</b-card>
				</div>
			</div>
			<b-pagination-nav class="pagination-buttons" base-url="projects#" :number-of-pages="renderProjects.totalPages" v-model="currentPage" />
		</div>
		<p v-else style="text-align:center"><em>Loading...</em></p>
	</div>

</template>

<script src="./index.ts"></script>