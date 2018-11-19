<template>
	<div>
		<div v-if="hasProjects">
			<div v-for="project in renderProjects.projects">
				<div>
					<b-card style="max-width: 25rem;" class="mb-2">
						<b-link :to="{ path: 'projects/details/' + project.id }">
							<h3 style="text-align:center">{{project.name}}</h3>
						</b-link>
						<hr />
						<p class="card-text">
							{{project.description}}
						</p>
						<div v-if="userRole === 'Administrator'" style="text-align:center">
							<b-button :to="{ path: 'projects/edit/' + project.id }" variant="warning">Edit</b-button>
							<b-button :to="{ path: 'projects/delete/' + project.id }" variant="danger">Delete</b-button>
						</div>
					</b-card>
				</div>
			</div>
			<b-pagination-nav class="pagination-buttons" base-url="#" :number-of-pages="renderProjects.totalPages" v-model="currentPage" />
		</div>
		<p v-else><em>Loading...</em></p>
	</div>

</template>

<script src="./index.ts"></script>